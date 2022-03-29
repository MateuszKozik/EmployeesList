using System;

namespace Employees
{
    public class EmployeeService
    {
        private EmployeeContext _context;

        public EmployeeService(EmployeeContext context)
        {
            _context = context;
        }

        public void AddUser(string name, string surname, int age)
        {
            _context.Employees.Add(
                new Employee {
                    Name = name, 
                    Surname = surname,
                    Age = age 
                });

            _context.SaveChanges();
        }
    }
}