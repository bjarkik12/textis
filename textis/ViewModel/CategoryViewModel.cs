using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    public class CategoryViewModel{

        public int Id { get; set; }
        public string Name { get; set; }
//        public string Project { get; set; }

        public CategoryViewModel()
        {
            //empty
        }

        public CategoryViewModel(Category category)
        {
           Id = category.Id;
           Name = category.Name;
  //         Project = category.Project;
        }

        public Category CastViewModelToModel()
        {
            Category m_category = new Category();
            m_category.Id = Id;
            m_category.Name = Name;         
            return m_category;
        }

        public CategoryViewModel CastModelToViewModel(Category category)
        {
            CategoryViewModel m_categoryViewModel = new CategoryViewModel(category);
            m_categoryViewModel.Id = category.Id;
            m_categoryViewModel.Name = category.Name;
            return m_categoryViewModel;
        }


    }
}