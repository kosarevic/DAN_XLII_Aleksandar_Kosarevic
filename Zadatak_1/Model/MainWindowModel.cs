using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    class MainWindowModel
    {

        public Employee Employee { get; set; }
        public Location Location { get; set; }
        public Sector Sector { get; set; }
        public int ManagerId { get; set; }

        public MainWindowModel()
        {
        }

        public MainWindowModel(Employee employee, Location location, Sector sector, int managerId)
        {
            Employee = employee;
            Location = location;
            Sector = sector;
            ManagerId = managerId;
        }
    }
}
