using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    /// <summary>
    /// Actions for ProjectLines, Implements IProjectLineRepository
    /// </summary>
    public class ProjectLineRepository : IProjectLineRepository
    {
        private readonly TextisModelContainer m_context = new TextisModelContainer();
        private bool m_disposed = false;

        /// <summary>
        /// Get all projectlines
        /// </summary>
        /// <returns>List of projectline object</returns>
        public List<ProjectLine> GetAll()
        {
            IQueryable<ProjectLine> query = m_context.ProjectLine; 
            return query.ToList();
        }

        /// <summary>
        /// Get all projectlines for one project
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns>List of projectline object</returns>
        public List<ProjectLine> GetByProjectId(int? id)
        {
            IQueryable<ProjectLine> query = m_context.ProjectLine;
            var query2 = (from x in query
                          where x.ProjectId == id
                          select x);
            return query2.ToList();
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="projectLine"></param>
        public void Create(ProjectLine projectLine)
        {
            m_context.ProjectLine.Add(projectLine);
        }

        /// <summary>
        /// Get single projectline
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProjectLine object</returns>
        public ProjectLine GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// Update projectline
        /// </summary>
        /// <param name="projectLine"></param>
        public void Update(ProjectLine projectLine)
        {
            m_context.Entry(projectLine).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete projectline
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            ProjectLine projectLine = this.GetSingle(id);
            m_context.ProjectLine.Remove(projectLine);
        }

        /// <summary>
        /// Save projectline
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