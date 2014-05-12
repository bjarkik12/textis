using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface IUpvoteRepository : IDisposable
    {
        List<Upvote> GetAll();
        Upvote GetSingle(int? id);
        List<Upvote> GetByProjectId(int? id);
        //List<Project> GetBy();
        void Create(Upvote upvote);
        void Update(Upvote upvote);
        void Delete(int? id);
        //void Dispose();
        //void Dispose(bool disposing);
        void Save();
        
    }
}
