using System;
using System.Windows.Input;
/// <summary>
/// Class added for delete functionality in application. Found solution online requiering it, so i added it.
/// </summary>
public class DelegateCommand : ICommand
{
    private readonly Predicate<object> _canExecute;
    private readonly Action<object> _execute;

    public event EventHandler CanExecuteChanged;

    public DelegateCommand(Action<object> execute, object p)
        : this(execute, null)
    {
    }

    public DelegateCommand(Action<object> execute,
                   Predicate<object> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        if (_canExecute == null)
        {
            return true;
        }

        return _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        if (CanExecuteChanged != null)
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}