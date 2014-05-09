using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.Repository
{
    interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetSingle(int? id);
        void Create(Category category);
        void Update(Category category);
        void Delete(int? id);

        void Dispose(bool disposing);
    }
}