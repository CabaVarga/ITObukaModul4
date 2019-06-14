using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadProjSecondGo.Models;

namespace RadProjSecondGo.Tests.Models
{
    [TestClass]
    public class ProjectContextTest
    {
        [TestMethod]
        public void CreateNewProjectContextObject()
        {
            ProjectContext projectContext = new ProjectContext();

            // what can I test?
            // Does it have all the neccessary DbSets added?
            // projectContext.Employees
            // ...

            // projectContext.Projects. -- Needs Entity Framework Reference from here
            // the Type DbSet is declared in EF dlls

            // Assert.IsInstanceOfType(projectContext, typeof(DbContext)); // -- does not work because of DbContext is also an 'alien' type...
        }
    }
}
