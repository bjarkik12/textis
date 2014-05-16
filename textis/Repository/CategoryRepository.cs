using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace textis.Repository
{
    /// <summary>
    /// Actions for Category, implements ICategoryRepository
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TextisModelContainer m_context = new TextisModelContainer();
        private bool m_disposed = false;

        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>List of Categorie objects</returns>
        public List<Category> GetAll()
        {
            IQueryable<Category> query = m_context.Category;
            return query.ToList();
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="category"></param>
        public void Create(Category category)
        {
            m_context.Category.Add(category);
            //context.SaveChanges();
        }

        /// <summary>
        /// Get single Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category object</returns>
        public Category GetSingle(int? id)
        {
            var query = this.GetAll().FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category"></param>
        public void Update(Category category)
        {
            m_context.Entry(category).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            Category category = this.GetSingle(id);
            m_context.Category.Remove(category);
            //context.SaveChanges();
        }

        /// <summary>
        /// Save Category
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