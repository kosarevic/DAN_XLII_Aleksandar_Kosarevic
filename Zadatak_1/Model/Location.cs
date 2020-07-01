using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    class Location
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }

        public Location()
        {
        }

        public Location(int id, string adress, string town, string country)
        {
            Id = id;
            Adress = adress;
            Town = town;
            Country = country;
        }
    }
}
