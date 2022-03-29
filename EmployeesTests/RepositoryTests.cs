using Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

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

        [TestMethod]
        public void TestUserAge()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employee = new Employee()
            {
                Name = "Jan",
                Surname = "Nowak",
                Age = 14
            };
            try
            {
                service.AddUser(employee);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestAddUserRange()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employees = new List<Employee> {
                new Employee(){ Name = "Jan",Surname = "Nowak", Age = 23 },
                new Employee(){ Name = "Tadeusz",Surname = "Kowalski", Age = 17 },
                new Employee(){ Name = "Zbigniew",Surname = "Kolano", Age = 21 }
            };

            service.AddUserRange(employees);

            mockSet.Verify(x => x.Add(It.IsAny<Employee>()), Times.Exactly(3));
            mockContext.Verify(x => x.SaveChanges(), Times.Exactly(3));
        }
    }
}
