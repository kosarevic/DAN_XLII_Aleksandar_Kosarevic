using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    public class Employee
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string RegistrationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }
        public Sector Sector { get; set; }
        public Employee Manager { get; set; }

        public Employee()
        {
        }

        public Employee(int id, string firstName, string lastName, string jMBG, DateTime dateOfBirth, char gender, string registrationNumber, string phoneNumber, Location location, Sector sector, Employee manager)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JMBG = jMBG;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            RegistrationNumber = registrationNumber;
            PhoneNumber = phoneNumber;
            Location = location;
            Sector = sector;
            Manager = manager;
        }
    }
}
