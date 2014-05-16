using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textis.Repository;
using textis.Tests.Repository;

namespace textis.Tests.Mock
{
    /// <summary>
    /// Mock ProjectRepository
    /// </summary>
    class MockProjectRepository : IProjectRepository
    {
        private List<Project> m_TestList = new List<Project>();

        /// <summary>
        /// data creation should not be here, move to ...ControllerTest
        /// </summary>
        public MockProjectRepository()
        {
            DateTime testDate = DateTime.Now;

            for (int i = 1; i <= 6; i++)
            {
                Project TestProject = new Project();
                TestProject.Id = i;
                TestProject.Date = testDate.AddDays(i);
                TestProject.Status = "status";
                TestProject.User = "User " + i.ToString();
                TestProject.Name = "Project " + i.ToString();
                TestProject.ProjectLine = new List<ProjectLine>();
                TestProject.Comment = new List<Comment>();
                TestProject.CategoryId = 1;
                TestProject.Category = new Category { Id = 1, Name = "cat 1" };
                m_TestList.Add(TestProject);
            }
            for (int i = 7; i <= 10; i++)
            {
                Project TestProject = new Project();
                TestProject.Id = i;
                TestProject.Date = testDate.AddDays(-i);
                TestProject.Status = "status";
                TestProject.User = "User " + i.ToString();
                TestProject.Name = "Project " + i.ToString();
                TestProject.ProjectLine = new List<ProjectLine>();
                TestProject.Comment = new List<Comment>();
                TestProject.CategoryId = 2;
                TestProject.Category = new Category { Id = 2, Name = "cat 2" };
                m_TestList.Add(TestProject);
            }
        }

        public List<Project> GetAll()
        {
            var returnList = (from item in m_TestList
                              select item).ToList();
            return returnList;
        }

        public List<Project> GetByProjectId(int? id)
        {
            List<Project> TestList = new List<Project>();
            var returnList = (from item in GetAll()
                              where item.Id == id
                              select item).ToList();
            return returnList;
        }

        public Project GetSingle(int? id)
        {
            Project TestProject = new Project();
            var returnProject = (from item in GetAll()
                              where item.Id == id
                              select item).SingleOrDefault();
            return returnProject;
        }

        public void Create(Project Project)
        {
            DateTime TestDate = DateTime.Now;

            Project TestProject = new Project();
            TestProject.Id = 20;
            TestProject.Date = TestDate;
            TestProject.Status = "status";
            TestProject.User = "User ";
            TestProject.Name = "Project create single ";
            TestProject.ProjectLine = new List<ProjectLine>();
            TestProject.Comment = new List<Comment>();
            TestProject.CategoryId = 2;
            TestProject.Category = new Category { Id = 2, Name = "cat 2" };

            m_TestList.Add(TestProject);
        }

        public void Update(Project Project)
        {
            Project.User = "ChangeUser";
        }

        public void Delete(int? id)
        {
            Project TestProject = new Project();
            TestProject = GetSingle(id);
            m_TestList.Remove(TestProject);
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
