using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    /// <summary>
    /// Actions for Project, implements IProjectRepository
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly TextisModelContainer m_context = new TextisModelContainer();
        private bool m_disposed = false;

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>List of Project object</returns>
        public List<Project> GetAll()
        {
            IQueryable<Project> query = m_context.Project; 
            return query.ToList();
        }

        /// <summary>
        /// Create Project
        /// </summary>
        /// <param name="project"></param>
        public void Create(Project project)
        {
            m_context.Project.Add(project);
        }

        /// <summary>
        /// Get one project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project object</returns>
        public Project GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project"></param>
        public void Update(Project project)
        {
            m_context.Entry(project).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete project
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            Project project = this.GetSingle(id);
            m_context.Project.Remove(project);
        }

        /// <summary>
        /// Save project
        /// </summary>
        public void Save()
        {
            m_context.SaveChanges();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                if (disposing)
                {
                    m_context.Dispose();
                }
            }
            this.m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}