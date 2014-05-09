using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface IUpvoteRepository
    {
        List<Upvote> GetAll();
        Upvote GetSingle(int? id);
        //List<Project> GetBy();
        void Create(Upvote upvote);
        void Update(Upvote upvote);
        void Delete(int? id);

        void Dispose(bool disposing);
        
    }
}
