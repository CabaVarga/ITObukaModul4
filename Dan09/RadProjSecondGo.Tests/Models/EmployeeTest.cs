using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadProjSecondGo.Models;

namespace RadProjSecondGo.Tests.Models
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void CreateNewEmployeeObject()
        {
            // arrange
            Employee employee = new Employee();

            // act


            // assert
            Assert.IsInstanceOfType(employee, typeof(Employee));
        }
    }
}
