using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public Project GetSingle(int id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }
    }
}