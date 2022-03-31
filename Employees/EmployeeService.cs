using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            employees.ForEach(employee =>
            {
                if (!IsCapitalized(employee.Name))
                   employee.Name = Capitalize(employee.Name);

                if (!IsCapitalized(employee.Surname))
                    employee.Surname = Capitalize(employee.Surname);
            });

            return employees;
        }

        public bool IsCapitalized(string data)
        {
            foreach (var letter in data.ToCharArray())
            {
                if (!Char.IsUpper(letter))
                {
                    return false;
                }
            }
            return true;
        }

        public string Capitalize(string data)
        {
            var final = new StringBuilder();

            foreach (var letter in data.ToCharArray())
            {
                final.Append(Char.ToUpper(letter));
            }

            return final.ToString();
        }

        public bool CheckAgeIsLargerThan(int age, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}