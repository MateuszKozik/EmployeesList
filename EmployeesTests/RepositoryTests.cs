using Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmployeesTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void AddUserToDb()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employee = new Employee()
            {
                Name = "Jan",
                Surname = "Nowak",
                Age = 23
            };
            service.AddUser(employee);

            mockSet.Verify(x => x.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
