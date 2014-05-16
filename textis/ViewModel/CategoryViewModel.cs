using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    /// <summary>
    /// Constructs a ViewModel for the Category Entity to the sent to the View
    /// </summary>
    public class CategoryViewModel{

        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryViewModel()
        {
            //empty
        }

        public CategoryViewModel(Category category)
        {
           Id = category.Id;
           Name = category.Name;
        }

        /// <summary>
        /// Used to shuffle data from a ViewModel to Model
        /// </summary>
        public Category CastViewModelToModel()
        {
            Category m_category = new Category();
            m_category.Id = Id;
            m_category.Name = Name;         
            return m_category;
        }
    }
}