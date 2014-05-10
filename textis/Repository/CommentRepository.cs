using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TextisModelContainer context = new TextisModelContainer();

        public List<Comment> GetAll()
        {
            IQueryable<Comment> query = context.Comment;
            return query.ToList();
        }

        public List<Comment> GetByProjectId(int? id)
        {
            IQueryable<Comment> query = context.Comment;
            var query2 = (from x in query
                          where x.ProjectId == id
                          select x);
            return query2.ToList();
        }

        public void Create(Comment comment)
        {
            context.Comment.Add(comment);
            //context.SaveChanges();
        }

        public Comment GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(Comment comment)
        {
            context.Entry(comment).State = EntityState.Modified;
            //context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Comment comment = this.GetSingle(id);
            context.Comment.Remove(comment);
            //context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}