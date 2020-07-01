﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<MainWindowModel> MainWindowViewModels { get; set; }
        static readonly string ConnectionString = @"Data Source=(local);Initial Catalog=Zadatak_1;Integrated Security=True;";

        public DelegateCommand ExecuteDeleteCommand { get; set; }

        public MainWindowViewModel()
        {
            FillList();
            ExecuteDeleteCommand = new DelegateCommand(DeleteRow, (x) => true);
        }

        private MainWindowModel row;

        public MainWindowModel Row
        {
            get { return row; }
            set
            {
                if (row != value)
                {
                    row = value;
                    OnPropertyChanged("Row");
                }
            }
        }

        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand query = new SqlCommand("select * from tblEmployee e " +
                            "join tblLocation l on e.LocationID = l.LocationID " +
                            "join tblSector s on e.SectorID = s.SectorID", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (MainWindowViewModels == null)
                    MainWindowViewModels = new ObservableCollection<MainWindowModel>();

                foreach (DataRow row in dataTable.Rows)
                {
                    MainWindowModel m = new MainWindowModel
                    {
                        Employee = new Employee
                        {
                            Id = int.Parse(row[0].ToString()),
                            FirstName = row[1].ToString(),
                            LastName = row[2].ToString(),
                            JMBG = row[3].ToString(),
                            DateOfBirth = DateTime.Parse(row[4].ToString()),
                            Gender = char.Parse(row[5].ToString()),
                            RegistrationNumber = row[6].ToString(),
                            PhoneNumber = row[7].ToString(),
                            ManagerId = int.Parse(row[10].ToString())
                        },
                        Location = new Location
                        {
                            Id = int.Parse(row[11].ToString()),
                            Adress = row[12].ToString(),
                            Town = row[13].ToString(),
                            Country = row[14].ToString()
                        },
                        Sector = new Sector
                        {
                            Id = int.Parse(row[15].ToString()),
                            Title = row[16].ToString()
                        }
                    };
                    m.Employee.Location = m.Location;
                    m.Employee.Sector = m.Sector;
                    MainWindowViewModels.Add(m);
                }
            }
        }

        public void DeleteRow(object id)
        {
            //Condition added to handle exeption if delete button is pressed without any previous selection.
            if (row == null) { return; }
            var con = new SqlConnection(ConnectionString);
            con.Open();
            var cmd = new SqlCommand("delete from tblUser where UserID = @UserId;" +
                "delete from tblIdentityCard where IdentityCardID = @IdCardId;", con);
            //cmd.Parameters.AddWithValue("@UserId", row.User.Id);
            //cmd.Parameters.AddWithValue("@IdCardId", row.IdentityCard.Id);
            cmd.ExecuteNonQuery();
            //LogActions.LogDeleteUser(row.User);
            //Object is removed from ongoing list.
            MainWindowViewModels.Remove(row);
            con.Close();
            con.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}