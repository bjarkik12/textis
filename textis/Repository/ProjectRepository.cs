using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TextisModelContainer context = new TextisModelContainer();

        public List<Project> GetAll()
        {
            IQueryable<Project> query = context.Project; 
            return query.ToList();
        }

        public void Create(Project project)
        {
            context.Project.Add(project);
            
        }

        public Project GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(Project project)
        {
            context.Entry(project).State = EntityState.Modified;
            //context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Project project = this.GetSingle(id);
            context.Project.Remove(project);
            //context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
        
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}