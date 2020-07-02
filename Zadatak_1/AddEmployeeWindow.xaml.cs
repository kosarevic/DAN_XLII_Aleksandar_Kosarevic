using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        AddEmployeeViewModel aevm = new AddEmployeeViewModel();

        public AddEmployeeWindow()
        {
            InitializeComponent();
            DataContext = aevm;
            aevm.Employee.FirstName = "";
            aevm.Employee.LastName = "";
            aevm.Employee.JMBG = "";
            aevm.Employee.PhoneNumber = "";
            aevm.Employee.Sector.Title = "";
        }

        private void LostFocus_TextBox(object sender, RoutedEventArgs e)
        {
        }

        private void Btn_Ok(object sender, RoutedEventArgs e)
        {
            if (AddEmployeeValidation.Validate(aevm.Employee))
            {
                if (CmbGender.Text == "")
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Please chose Gender.", "Notification");
                }
                else if (CmbLocation.Text == "")
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Please chose Location.", "Notification");
                }
                else
                {
                    aevm.AddEmployee();
                    MainWindow MainPage = new MainWindow();
                    MainPage.Show();
                    this.Close();
                }
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
