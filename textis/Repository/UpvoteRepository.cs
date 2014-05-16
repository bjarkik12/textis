using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    /// <summary>
    /// Actions for Upvote, Implements IUpvoteRepository
    /// </summary>
    public class UpvoteRepository : IUpvoteRepository
    {
        private readonly TextisModelContainer m_context = new TextisModelContainer();
        private bool m_disposed = false;

        /// <summary>
        /// Get all Upvotes
        /// </summary>
        /// <returns>List of Upvote object</returns>
        public List<Upvote> GetAll()
        {
            IQueryable<Upvote> query = m_context.Upvote;
            return query.ToList();
        }

        /// <summary>
        /// Create Upvote
        /// </summary>
        /// <param name="upvote"></param>
        public void Create(Upvote upvote)
        {
            m_context.Upvote.Add(upvote);
        }

        /// <summary>
        /// Get all upvotes for one Project
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns>List of Upvote object</returns>
        public List<Upvote> GetByProjectId(int? id)
        {
            IQueryable<Upvote> query = m_context.Upvote;
            var query2 = (from x in query
                          where x.ProjectId == id
                          select x);
            return query2.ToList();
        }

        /// <summary>
        /// Get one Upvote
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Upvote object</returns>
        public Upvote GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// Update Upvote
        /// </summary>
        /// <param name="upvote"></param>
        public void Update(Upvote upvote)
        {
            m_context.Entry(upvote).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete Upvote
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            Upvote upvote = this.GetSingle(id);
            m_context.Upvote.Remove(upvote);
        }

        /// <summary>
        /// Save Upvote
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