using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TextisModelContainer context = new TextisModelContainer();

        public List<Category> GetAll()
        {
            IQueryable<Category> query = context.Category;
            return query.ToList();
        }

        public void Create(Category category)
        {
            context.Category.Add(category);
            //context.SaveChanges();
        }

        public Category GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        public void Update(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            //context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Category category = this.GetSingle(id);
            context.Category.Remove(category);
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