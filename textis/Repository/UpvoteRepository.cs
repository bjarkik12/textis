using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    public class UpvoteRepository : IUpvoteRepository
    {
        private readonly TextisModelContainer context = new TextisModelContainer();


        public void Dispose(bool disposing)
        {
            // empty
        }

        public List<Upvote> GetAll()
        {
            IQueryable<Upvote> query = context.Upvote;
            return query.ToList();
        }

        public void Create(Upvote upvote)
        {
            context.Upvote.Add(upvote);
            context.SaveChanges();
        }

        public Upvote GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(Upvote upvote)
        {
            context.Entry(upvote).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Upvote upvote = this.GetSingle(id);
            context.Upvote.Remove(upvote);
            context.SaveChanges();
        }


    }
}