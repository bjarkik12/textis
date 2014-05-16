using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    /// <summary>
    /// Actions for Comment, implements ICommentRepository
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly TextisModelContainer m_context = new TextisModelContainer();
        private bool m_disposed = false;

        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns>List of comment objects</returns>
        public List<Comment> GetAll()
        {
            IQueryable<Comment> query = m_context.Comment;
            return query.ToList();
        }

        /// <summary>
        /// Get comments for one project
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns>List of comment objects</returns>
        public List<Comment> GetByProjectId(int? id)
        {
            IQueryable<Comment> query = m_context.Comment;
            var query2 = (from x in query
                          where x.ProjectId == id
                          select x);
            return query2.ToList();
        }

        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="comment"></param>
        public void Create(Comment comment)
        {
            m_context.Comment.Add(comment);
        }

        /// <summary>
        /// Get single comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>comment object</returns>
        public Comment GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="comment"></param>
        public void Update(Comment comment)
        {
            m_context.Entry(comment).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            Comment comment = this.GetSingle(id);
            m_context.Comment.Remove(comment);
        }

        /// <summary>
        /// Save comment
        /// </summary>
        public void Save()
        {
            m_context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                if (disposing)
                {
                    m_context.Dispose();
                }
            }
            this.m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}