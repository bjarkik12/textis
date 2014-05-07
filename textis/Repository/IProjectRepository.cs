using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface IProjectRepository
    {
        List<Project> GetAll();
        Project GetSingle(int id);
        //List<Project> GetBy();
        void Dispose(bool disposing);
    }
}
