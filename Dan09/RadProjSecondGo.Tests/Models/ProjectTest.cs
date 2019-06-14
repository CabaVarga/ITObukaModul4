using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadProjSecondGo.Models;

namespace RadProjSecondGo.Tests.Models
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void CreateNewProjectObject()
        {
            Project project = new Project();

            Assert.IsInstanceOfType(project, typeof(Project));
        }
    }
}
