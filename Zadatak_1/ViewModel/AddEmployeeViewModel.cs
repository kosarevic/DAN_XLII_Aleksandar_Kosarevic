using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.LogFile;
using Zadatak_1.Model;
using Zadatak_1.Validation;
using Zadatak_1.ViewModel;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class made for displaying Add Employee Window features of the application
    /// </summary>
    class AddEmployeeViewModel : INotifyPropertyChanged
    {
        static readonly string ConnectionString = @"Data Source=(local);Initial Catalog=Zadatak_1;Integrated Security=True;";
        //Class specific collection is determined below.
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<Sector> Sectors { get; set; }

        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set
            {
                if (employee != value)
                {
                    employee = value;
                    OnPropertyChanged("Employee");
                }
            }
        }

        //Gender collection is made with predifined values.
        private List<string> genders;

        public List<string> Genders
        {
            get { return new List<string> { "M", "F", "X" }; }
            set { genders = value; }
        }

        private List<Employee> employees;

        public List<Employee> Employees
        {
            get { return MainWindowViewModel.employees; }
            set { employees = value; }
        }

        public AddEmployeeViewModel()
        {
            FillList();
            Employee = new Employee();
            Employee.Sector = new Sector();
            Employee.Manager = new Employee();
        }
        /// <summary>
        /// Method for filling out previously mentioned collection
        /// </summary>
        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand query = new SqlCommand("select * from tblLocation", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Locations == null)
                    Locations = new ObservableCollection<Location>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Location l = new Location
                    {
                        Id = int.Parse(row[0].ToString()),
                        Adress = row[1].ToString(),
                        Town = row[2].ToString(),
                        Country = row[3].ToString()
                    };
                    Locations.Add(l);
                }
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand query = new SqlCommand("select * from tblSector", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Sectors == null)
                    Sectors = new ObservableCollection<Sector>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Sector s = new Sector
                    {
                        Id = int.Parse(row[0].ToString()),
                        Title = row[1].ToString(),
                    };
                    Sectors.Add(s);
                }
            }
        }
        /// <summary>
        /// Method enables adding employee to the database.
        /// </summary>
        public void AddEmployee()
        {
            foreach (Sector s in Sectors)
            {
                if (Employee.Sector.Title == s.Title)
                {
                    Employee.Sector = s;
                }
                else
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        var cmd = new SqlCommand(@"insert into tblSector values (@Title); SELECT SCOPE_IDENTITY();", conn);
                        cmd.Parameters.AddWithValue("@Title", Employee.Sector.Title);
                        conn.Open();
                        Employee.Sector.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }
            }

            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(@"insert into tblEmployee values (@Name, @Surname, @JMBG, @DateOfBirth, @Gender, @RegNum, @PhoneNumber, @LocId, @SectorID, @ManagerID);", conn);
                cmd.Parameters.AddWithValue("@Name", Employee.FirstName);
                cmd.Parameters.AddWithValue("@Surname", Employee.LastName);
                cmd.Parameters.AddWithValue("@JMBG", Employee.JMBG);
                cmd.Parameters.AddWithValue("@DateOfBirth", AddEmployeeValidation.dateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", Employee.Gender);
                cmd.Parameters.AddWithValue("@RegNum", AddEmployeeValidation.registrationNumber);
                cmd.Parameters.AddWithValue("@PhoneNumber", Employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@LocId", Employee.Location.Id);
                cmd.Parameters.AddWithValue("@SectorID", Employee.Sector.Id);
                cmd.Parameters.AddWithValue("@ManagerID", Employee.Manager.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Employee successfully created.", "Notification");
            LogActions.LogAddEmployee(Employee);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
