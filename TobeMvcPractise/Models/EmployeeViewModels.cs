using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using TobeMvcPractise.Attributes;

namespace TobeMvcPractise.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "Name length must be between 3 to 60 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [ValidDate("1/2/2018", ErrorMessage = "Please enter a date between 1/2/2018 and today")]
        public DateTime JoiningDate { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        //private backing singleton instance in memory
        private static List<Employee> _employees;

        public static List<Employee> Employees
        {
            get
            {
                if (_employees == null) _employees = GetEmployees();
                return _employees;
            }
        }

        private static List<Employee> GetEmployees()
        {
            List<Employee> empList = new List<Employee>();
            empList.Add(new Employee { Id = 1, Name = "Employee 1", JoiningDate = DateTime.Today, Age = 18 });
            empList.Add(new Employee { Id = 2, Name = "Employee 2", JoiningDate = DateTime.Now.Date, Age = 19 });
            empList.Add(new Employee { Id = 3, Name = "Employee 3", JoiningDate = DateTime.Today, Age = 20 });
            empList.Add(new Employee { Id = 4, Name = "Employee 4", JoiningDate = DateTime.Now.Date, Age = 21 });
            empList.Add(new Employee { Id = 5, Name = "Employee 5", JoiningDate = DateTime.Parse(DateTime.Today.ToString()), Age = 22 });
            empList.Add(new Employee { Id = 6, Name = "Employee 6", JoiningDate = DateTime.Parse(DateTime.Now.Date.ToString()), Age = 23 });
            empList.Add(new Employee { Id = 7, Name = "Employee 7", JoiningDate = DateTime.Parse(DateTime.Now.ToString()), Age = 24 });

            return empList;
        }
    }

    //for entity framework
    public class EmpDbContext : DbContext
    {
        public EmpDbContext()
        {

        }

        private static EmpDbContext _empDbContext;

        public static EmpDbContext EmpDbContextSingleton
        {
            get
            {
                if (_empDbContext == null)
                {
                    lock(new object())
                    {
                        if (_empDbContext == null) _empDbContext = new EmpDbContext();
                    }
                }
                return _empDbContext;
            }
        }

        public DbSet<Employee> Employees { get; set; }
    }
}