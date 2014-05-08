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


        public void Dispose(bool disposing)
        {
            // empty
        }

        public List<Project> GetAll()
        {
            IQueryable<Project> query = context.Project; 
            return query.ToList();
        }

        public void Create(Project project)
        {
            context.Project.Add(project);
            context.SaveChanges();
        }

        public Project GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(Project project)
        {
            context.Entry(project).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Project project = this.GetSingle(id);
            context.Project.Remove(project);
            context.SaveChanges();
        }
    }
}