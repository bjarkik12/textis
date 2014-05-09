using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface ICommentRepository : IDisposable
    {
        List<Comment> GetAll();
        Comment GetSingle(int? id);
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(int? id);
        void Save();
        
    }
}
