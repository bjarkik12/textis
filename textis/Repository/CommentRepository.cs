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


        public void Dispose(bool disposing)
        {
            // empty
        }

        public List<Comment> GetAll()
        {
            IQueryable<Comment> query = context.Comment;
            return query.ToList();
        }

        public void Create(Comment comment)
        {
            context.Comment.Add(comment);
            context.SaveChanges();
        }

        public Comment GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(Comment comment)
        {
            context.Entry(comment).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Comment comment = this.GetSingle(id);
            context.Comment.Remove(comment);
            context.SaveChanges();
        }


    }
}