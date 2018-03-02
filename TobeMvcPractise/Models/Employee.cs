using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TobeMvcPractise.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace TobeMvcPractise.Models
{
    //[Table("EmployeeInfo")]   //to generate a different name for the actual database table
    public class Employee
    {
        //[Key]     //not needed since property name is Id
        //[Column("EmployeeId")]   //sets the column name to EmployeeId 
        public int Id { get; set; }

        //[Key, Column(Order = 2)]      //composite key, the order must be explicitly stated e.g. Id=1, Name=2, etc.
        [StringLength(60, ErrorMessage = "Name length must be between 3 to 60 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [ValidDate("1/2/2018", ErrorMessage = "Please enter a date between 1/2/2018 and today")]
        public DateTime JoiningDate { get; set; }

        //[Timestamp] //attribute is used for concurrency checking can only be attacjed to a single column which is a byte array type
        //[ConcurrencyCheck]  //to initiate optimistic concurrency check for updates
        //[Required]  //declares that the property is needed i.e. NotNull in db
        //[Index(IsUnique =true)]     //introduces a unique index named IX_Age (i.e. IX_ColumnName)
        //[NotMapped]     //not added as a column
        [Range(18, 60)]
        public int Age { get; set; }
        
        public virtual ICollection<Course> Courses { get; set; }        //list of courses for which this employee is the foreign key


        //private backing singleton instance in memory for in-memory employee class
        private static List<Employee> _employees;
        public static List<Employee> Employees
        {
            get
            {
                if (_employees == null) _employees = GetEmployees();
                return _employees;
            }
        }

        //used for in memory collection practise
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
}