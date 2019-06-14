using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadProjSecondGo.Models;

namespace RadProjSecondGo.Tests.Models
{
    [TestClass]
    public class EmployeeProjectTest
    {
        [TestMethod]
        public void CreateNewEmployeeProjectObject()
        {
            EmployeeProject employeeProject = new EmployeeProject();

            Assert.IsInstanceOfType(employeeProject, typeof(EmployeeProject));
        }
    }
}
