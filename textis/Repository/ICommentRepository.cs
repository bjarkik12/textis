using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface ICommentRepository : IDisposable
    {
        List<Comment> GetAll();
        List<Comment> GetByProjectId(int? id);
        Comment GetSingle(int? id);
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(int? id);
        void Save();
    }
}
