using System;
using System.Collections.Generic;

namespace textis.Repository
{
    public interface IProjectRepository : IDisposable
    {
        List<Project> GetAll();
        Project GetSingle(int? id);
        void Create(Project project);
        void Update(Project project);
        void Delete(int? id);
        void Save();       
    }
}
