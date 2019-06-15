using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RadProjSecondGo.Models;
using RadProjSecondGo.Repositories;

namespace RadProjSecondGo.Services
{
    public class ProjectsService : IProjectsService
    {
        private IUnitOfWork db;

        public ProjectsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public Project CreateProject(Project project)
        {
            db.ProjectsRepository.Insert(project);
            db.Save();

            return project;
        }

        public Project DeleteProject(int id)
        {
            Project project = db.ProjectsRepository.GetByID(id);

            if (project != null)
            {
                db.ProjectsRepository.Delete(project);
                db.Save();
            }

            return project;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return db.ProjectsRepository.Get();
        }

        public Project GetProject(int id)
        {
            return db.ProjectsRepository.GetByID(id);
        }

        public Project UpdateProject(int id, Project project)
        {
            Project updatedProject = db.ProjectsRepository.GetByID(id);

            if (updatedProject != null)
            {
                updatedProject.Code = project.Code;
                updatedProject.Title = project.Title;
                updatedProject.ProjectManager = project.ProjectManager;
                updatedProject.Contractor = project.Contractor;

                db.Save();
            }

            return updatedProject;
        }
    }
}