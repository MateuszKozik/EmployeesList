using System;

namespace Employees
{
    public class EmployeeService
    {
        private EmployeeContext @object;

        public EmployeeService(EmployeeContext @object)
        {
            this.@object = @object;
        }

        public void AddUser(string v1, string v2, int v3)
        {
            throw new NotImplementedException();
        }
    }
}