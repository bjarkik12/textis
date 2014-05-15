using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textis.Repository;
using textis.Tests.Repository;

namespace textis.Tests.Mock
{
    class MockCategoryRepository : ICategoryRepository
    {
        private List<Category> m_TestList = new List<Category>();

        public MockCategoryRepository()
        {
            DateTime TestDate = DateTime.Now;

            Project pro1 = new Project();
            pro1.Name = "Test Project 1";
            pro1.Id = 1;
            pro1.Status = "status";
            pro1.ProjectLine = new List<ProjectLine>();
            pro1.User = "username";
            pro1.Date = DateTime.Now;
            pro1.Comment = new List<Comment>();
            pro1.CategoryId = 2;
            pro1.Category = new Category { Id = 1, Name = "cat 1" };

            Project pro2 = new Project();
            pro2.Name = "Test Project 2";
            pro2.Id = 2;
            pro2.Status = "status";
            pro2.ProjectLine = new List<ProjectLine>();
            pro2.User = "username";
            pro2.Date = DateTime.Now;
            pro2.Comment = new List<Comment>();
            pro2.CategoryId = 2;
            pro2.Category = new Category { Id = 1, Name = "cat 1" };

            
            for (int i = 1; i <= 6; i++)
            {
                Category TestCategory = new Category();
                TestCategory.Id = i;
                TestCategory.Name = "name " + i.ToString();
                TestCategory.Project.Add(pro1);
                m_TestList.Add(TestCategory);
            }
            for (int i = 7; i <= 10; i++)
            {
                Category TestCategory = new Category();
                TestCategory.Id = i;
                TestCategory.Project.Add(pro2);
                m_TestList.Add(TestCategory);
            }
        }

        public List<Category> GetAll()
        {
            var returnList = (from item in m_TestList
                              select item).ToList();
            return returnList;
        }

        public Category GetSingle(int? id)
        {
            Category TestCategory = new Category();
            var returnCategory = (from item in GetAll()
                              where item.Id == id
                              select item).SingleOrDefault();
            return returnCategory;
        }

        public void Create(Category Category)
        {
            Project pro = new Project();
            pro.Name = "Test Project";
            pro.Id = 2;
            pro.Status = "status";
            pro.ProjectLine = new List<ProjectLine>();
            pro.User = "username";
            pro.Date = DateTime.Now;
            pro.Comment = new List<Comment>();
            pro.CategoryId = 2;
            pro.Category = new Category { Id = 1, Name = "cat 1" };
            DateTime TestDate = DateTime.Now;

            Category TestCategory = new Category();
            TestCategory.Id = 12;
            TestCategory.Project.Add(pro);
            m_TestList.Add(TestCategory);
        }

        public void Update(Category Category)
        {
            //Category TestCategory = new Category();
            //TestCategory = GetSingle(Category.Id);
            Category.Name = "ChangeName";
        }

        public void Delete(int? id)
        {
            Category TestCategory = new Category();
            TestCategory = GetSingle(id);
            m_TestList.Remove(TestCategory);
        }

        public void Save()
        {
            // empty
        }

        public void Dispose()
        {
            // empty
        }


    }
}
