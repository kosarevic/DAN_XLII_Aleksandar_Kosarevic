using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    public class Sector
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Sector()
        {
        }

        public Sector(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
