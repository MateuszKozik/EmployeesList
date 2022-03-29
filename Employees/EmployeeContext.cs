using Microsoft.EntityFrameworkCore;

namespace Employees
{
    public class EmployeeContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
    }
}