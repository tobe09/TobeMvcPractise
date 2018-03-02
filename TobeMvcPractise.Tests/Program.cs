using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobeMvcPractise.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Controllers.EmployeeEntityControllerTest().EmployeeEntity_Details_With_Wrong_Id_Mock();
        }
    }
}
