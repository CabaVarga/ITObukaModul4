using RadProjSecondGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadProjSecondGo.Services
{
    public interface IProjectsService
    {
        IEnumerable<Project> GetAllProjects();

        Project CreateProject(Project project);

        Project UpdateProject(int id, Project project);

        Project DeleteProject(int id);

        Project GetProject(int id);
    }
}