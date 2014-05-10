using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.Repository
{
    interface IProjectLineRepository : IDisposable
    {
        List<ProjectLine> GetAll();
        List<ProjectLine> GetByProjectId(int? id);
        ProjectLine GetSingle(int? id);
        void Create(ProjectLine projectLine);
        void Update(ProjectLine projectLine);
        void Delete(int? id);
        void Save();
        //void Dispose(bool disposing);
    }
}