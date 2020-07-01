using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.LogFile
{
    /// <summary>
    /// Class made to preform loging of activities into text file LogFile.txt
    /// </summary>
    static class LogActions
    {
        //method logs creation of the User.
        public static void LogDeleteEmployee(Employee e)
        {
            File.AppendAllText(@"..\\..\Files\LogFile.txt", "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "]" + " Employee: " + e.FirstName + " " + e.LastName + ", has been deleted" + Environment.NewLine);
        }
    }
}
