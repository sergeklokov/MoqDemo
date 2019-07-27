using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Collections.Generic;
using Moq;
using System.Linq;
using ConsoleDatabaseOps;

namespace UnitTestWithMoq
{
    /// <summary>
    /// This example is reworked Moq example from MSDN
    /// 
    /// Serge Klokov, wrote in 2019
    /// </summary>

    [TestClass]
    public class UnitTest1 
    {
        //Dummy data to mock table People
        IQueryable<Person> data = new List<Person>
        {
            new Person { PersonID = 1, Name = "John"},
            new Person { PersonID = 2, Name = "Bill"},
            new Person { PersonID = 3, Name = "Jennifer"},
        }.AsQueryable();

        //Mocking the EF db context with data above
        private Mock<AdventureWorks2016CTP3Entities> GetMockContext(IQueryable<Person> data, out Mock<DbSet<Person>> mockSet)
        {
            mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AdventureWorks2016CTP3Entities>();
            mockContext.Setup(c => c.People).Returns(mockSet.Object);

            return mockContext;
        }


        // Let's unit test name
        // If we get name "Serge" from the DB, then it would be incorrect, and it would be integration test
        [TestMethod]
        public void TestPersonName()
        {
            var mockContext = GetMockContext(data, out _);
            var s = Program.GetPersonName(1, mockContext.Object);
            Assert.AreEqual(s, "John");
        }

        // Let's unit test name
        // If we get name "Serge" from the DB, then it would be incorrect, and it would be integration test
        [TestMethod]
        public void TestPeopleCount()
        {
            var mockContext = GetMockContext(data, out _);
            var i = Program.GetPeopleCount(mockContext.Object);
            Assert.AreEqual(i, data.Count());
        }

        [TestMethod]
        public void TestAddingAPerson()
        {
            Mock<DbSet<Person>> mockSet; // we need mockSet in this case
            var mockContext = GetMockContext(data, out mockSet);
            var newPerson = Program.AddAPerson("Unit Test Name", mockContext.Object);

            Assert.AreEqual(newPerson, null); // it's fake operation, so it is always null

            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Note: it will be no "Unit Test Name" saved in the actual database table
        }
    }
}
