using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface IProjectRepository
    {
        List<Project> GetAll();
        Project GetSingle(int? id);
        //List<Project> GetBy();
        void Create(Project project);
        void Update(Project project);
        void Delete(int? id);

        void Dispose(bool disposing);
        
    }
}
