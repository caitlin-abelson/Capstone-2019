using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayer;
using DataObjects;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    /// Author: Jacob Miller
    /// Created 2019/2/14
    /// Unit tests for PerformanceManager
    /// </summary>
    [TestClass]
    public class PerformanceManagerTests
    {
        private IPerformanceManager _performanceManager;
        private List<Performance> _performances;
        private PerformanceAccessorMock _mockAccessor;

        [TestInitialize]
        public void testSetup()
        {
            _mockAccessor = new PerformanceAccessorMock();
            _performanceManager = new PerformanceManager(_mockAccessor);
            _performances = new List<Performance>();
            _performances = _performanceManager.RetrieveAllPerformance();
        }

        [TestMethod]
        public void TestRetrieveAllPerformances()
        {
            List<Performance> performances = null;
            performances = _performanceManager.RetrieveAllPerformance();
            CollectionAssert.Equals(_performances, performances);
        }

        [TestMethod]
        public void TestCreatePerformanceValidInput()
        {
            DateTime date = new DateTime(2019, 3, 25);
            _performanceManager.AddPerformance(new Performance(111111, "Test Name 5", date, "Test Description 5"));
            _performances = _performanceManager.RetrieveAllPerformance();
            Assert.IsNotNull(_performances.Find(x => x.Name.Equals("Test Name 5") &&
                                                x.Date.Day == date.Day &&
                                                x.Description.Equals("Test Description 5")));
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void TestCreatePerformanceInValidDate()
        {
            DateTime date = new DateTime(2010, 1, 1);
            _performanceManager.AddPerformance(new Performance(222222, "Test Name 6", date, "Test Description 6"));
            Assert.Fail("Allowed an invalid date to be set.");
        }
    }
}
