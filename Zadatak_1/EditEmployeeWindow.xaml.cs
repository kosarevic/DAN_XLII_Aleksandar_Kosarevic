using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.Model;
using Zadatak_1.Validation;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        EditEmployeeViewModel eevm = new EditEmployeeViewModel();

        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            eevm.Employee = employee;
            DataContext = eevm;
        }

        private void Btn_Ok(object sender, RoutedEventArgs e)
        {
            if (AddEmployeeValidation.Validate(eevm.Employee))
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += eevm.EditEmployee;
                worker.RunWorkerAsync();
                Thread.Sleep(2000);
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Employee successfully updated.", "Notification");
                MainWindow MainPage = new MainWindow();
                MainPage.Show();
                this.Close();
            }
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow MainPage = new MainWindow();
            MainPage.Show();
            this.Close();
        }
    }
}
