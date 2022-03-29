using System;
using System.Collections.Generic;

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
            for(int i = 0; i < employees.Count; i++)
            {
                AddUser(employees[i]);
            }
        }
    }
}