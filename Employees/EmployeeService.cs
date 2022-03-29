using System;

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
            _context.Employees.Add(employee);

            _context.SaveChanges();
        }
    }
}