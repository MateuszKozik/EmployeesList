using Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void TestGetUsers()
        {
            var data = new List<Employee> {
                new Employee(){ Name = "Jan",Surname = "Nowak", Age = 23 },
                new Employee(){ Name = "Tadeusz",Surname = "Kowalski", Age = 17 },
                new Employee(){ Name = "Zbigniew",Surname = "Kolano", Age = 21 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employees = service.GetAllUsers();

            Assert.AreEqual(3, employees.Count);
        }

        [TestMethod]
        public void TestIsUsersExists()
        {
            var data = new List<Employee>().AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);
            var service = new EmployeeService(mockContext.Object);

            try
            {
                var employees = service.GetAllUsers();
            }
            catch (System.InvalidOperationException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestUsersAreSorted()
        {
            var data = new List<Employee> {
                new Employee(){ Name = "Jan",Surname = "Nowak", Age = 23 },
                new Employee(){ Name = "Tadeusz",Surname = "Kowalski", Age = 17 },
                new Employee(){ Name = "Zbigniew",Surname = "Kolano", Age = 21 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employees = service.GetAllUsers();

            Assert.AreEqual("KOLANO", employees[0].Surname);
            Assert.AreEqual("KOWALSKI", employees[1].Surname);
            Assert.AreEqual("NOWAK", employees[2].Surname);
        }

        [TestMethod]
        public void TestStringIsCapitalize()
        {
            var mockContext = new Mock<EmployeeContext>();
            var service = new EmployeeService(mockContext.Object);

            var test = "testString";
            var isCapitalized = service.IsCapitalized(test);

            Assert.IsFalse(isCapitalized);
        }

        [TestMethod]
        public void TestCapitalizeMethod()
        {
            var mockContext = new Mock<EmployeeContext>();
            var service = new EmployeeService(mockContext.Object);

            var test = "testString";
            var capitalized = service.Capitalize(test);

            Assert.AreEqual("TESTSTRING", capitalized);
        }

        [TestMethod]
        public void TestNamesAreCapitalized()
        {
            var data = new List<Employee> {
                new Employee(){ Name = "Jan",Surname = "Nowak", Age = 23 },
                new Employee(){ Name = "Tadeusz",Surname = "Kowalski", Age = 17 },
                new Employee(){ Name = "Zbigniew",Surname = "Kolano", Age = 21 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employees = service.GetAllUsers();

            Assert.AreEqual("ZBIGNIEW", employees[0].Name);
            Assert.AreEqual("TADEUSZ", employees[1].Name);
            Assert.AreEqual("JAN", employees[2].Name);

            Assert.AreEqual("KOLANO", employees[0].Surname);
            Assert.AreEqual("KOWALSKI", employees[1].Surname);
            Assert.AreEqual("NOWAK", employees[2].Surname);
        }

        [TestMethod]
        public void CheckUserAge()
        {
            var mockContext = new Mock<EmployeeContext>();
            var service = new EmployeeService(mockContext.Object);

            Employee employee = new()
            {
                Name = "Jan",
                Surname = "Nowak",
                Age = 19
            };

            var ageIsValid = service.CheckAgeIsLargerThan(18, employee);
            Assert.IsTrue(ageIsValid);
        }

        [TestMethod]
        public void CheckMultipleUserAge()
        {
            var data = new List<Employee> {
                new Employee(){ Name = "Jan",Surname = "Nowak", Age = 23 },
                new Employee(){ Name = "Tadeusz",Surname = "Kowalski", Age = 17 },
                new Employee(){ Name = "Zbigniew",Surname = "Kolano", Age = 21 }
            };

            var mockContext = new Mock<EmployeeContext>();
            var service = new EmployeeService(mockContext.Object);

            Employee employee = new()
            {
                Name = "Jan",
                Surname = "Nowak",
                Age = 19
            };

            var employees = service.CheckMultipleUserAge(18, data);

            Assert.AreEqual(2, employees.Count);
        }

        [TestMethod]
        public void GetFinalUserList()
        {
            var data = new List<Employee> {
                new Employee(){ Name = "Jan",Surname = "Nowak", Age = 23 },
                new Employee(){ Name = "Tadeusz",Surname = "Kowalski", Age = 17 },
                new Employee(){ Name = "Zbigniew",Surname = "Kolano", Age = 21 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);
            var employees = service.GetAllUsers();
            employees = service.CheckMultipleUserAge(18, employees);

            Assert.AreEqual(2, employees.Count);
            Assert.AreEqual("ZBIGNIEW", employees[0].Name);
            Assert.AreEqual("JAN", employees[1].Name);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);

            var employee = new Employee() { Name = "Jan", Surname = "Nowak", Age = 23 };

            employee = service.UpdateUser(employee);

            mockSet.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }


        [TestMethod]
        public void TestDeleteUser()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var service = new EmployeeService(mockContext.Object);

            var employee = new Employee() { Name = "Jan", Surname = "Nowak", Age = 23 };

            bool success = service.DeleteUser(employee);

            Assert.IsTrue(success);
        }
    }
}
