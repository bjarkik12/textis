using System;
using System.Collections.Generic;

namespace textis.Repository
{
    interface ICommentRepository
    {
        List<Comment> GetAll();
        Comment GetSingle(int? id);
        //List<Project> GetBy();
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(int? id);

        void Dispose(bool disposing);
        
    }
}
