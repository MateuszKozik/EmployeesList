using System;
using System.Collections.Generic;
using System.Linq;

namespace Employees
{
    public class EmployeeService
    {
        private readonly EmployeeContext _context;

        public EmployeeService(EmployeeContext context)
        {
            _context = context;
        }

        public void AddUser(Employee employee)
        {
            if(employee.Age < 15)
            {
                throw new ArgumentOutOfRangeException("Too young.");
            }
            _context.Employees.Add(employee);

            _context.SaveChanges();
        }

        public void AddUserRange(List<Employee> employees)
        {
            employees.ForEach(employee => { AddUser(employee); });        
        }

        public List<Employee> GetAllUsers()
        {
            var employees = _context.Employees.OrderBy(x => x.Surname).ToList();
            if (employees.Count == 0)
                throw new InvalidOperationException("Sequence contains no elements");

            return employees;
        }

        public bool IsCapitalized(string data)
        {
            throw new NotImplementedException();
        }
    }
}