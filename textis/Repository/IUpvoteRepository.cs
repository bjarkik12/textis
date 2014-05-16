using System;
using System.Collections.Generic;

namespace textis.Repository
{
    public interface IUpvoteRepository : IDisposable
    {
        List<Upvote> GetAll();
        Upvote GetSingle(int? id);
        List<Upvote> GetByProjectId(int? id);
        void Create(Upvote upvote);
        void Update(Upvote upvote);
        void Delete(int? id);
        void Save();        
    }
}
