using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    public class ProjectLineRepository : IProjectLineRepository
    {
        private readonly TextisModelContainer context = new TextisModelContainer();


        public void Dispose(bool disposing)
        {
            // empty
        }

        public List<ProjectLine> GetAll()
        {
            IQueryable<ProjectLine> query = context.ProjectLine; 
            return query.ToList();
        }

        public void Create(ProjectLine projectLine)
        {
            context.ProjectLine.Add(projectLine);
            context.SaveChanges();
        }

        public ProjectLine GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(ProjectLine projectLine)
        {
            context.Entry(projectLine).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            ProjectLine projectLine = this.GetSingle(id);
            context.ProjectLine.Remove(projectLine);
            context.SaveChanges();
        }
    }
}