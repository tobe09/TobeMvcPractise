using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TobeMvcPractise.Models;

namespace TobeMvcPractise.Services
{
    public class EmployeeServices
    {
        private List<Employee> _employees = Employee.Employees;

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> employees = from emp in _employees
                                          where emp.Age >= 18
                                          orderby emp.Id
                                          select emp;

            return employees;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee = _employees.SingleOrDefault(m => m.Id == id);

            return employee;
        }

        public bool AddEmployee(Employee emp)
        {
            try
            {
                emp.Id = NewEmployeeId();
                _employees.Add(emp);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private int NewEmployeeId()
        {
            int id = _employees.Max(e => e.Id) + 1;

            return id;
        }

        public bool UpdateEmployee(int id, FormCollection collection)
        {
            try
            {
                Employee emp = GetEmployee(id);
                emp.Name = collection["Name"];
                emp.Age = int.Parse(collection["Age"]);
                emp.JoiningDate = DateTime.Parse(collection["JoiningDate"]);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveEmployee(int id)
        {
            try
            {
                Employee emp = GetEmployee(id);
                _employees.Remove(emp);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}