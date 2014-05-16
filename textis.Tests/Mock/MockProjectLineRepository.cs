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
    /// Mock ProjectLineRepository
    /// </summary>
    class MockProjectLineRepository : IProjectLineRepository
    {
        private List<ProjectLine> m_TestList = new List<ProjectLine>();

        /// <summary>
        /// Data creation should be moved to ...ControllerTest
        /// </summary>
        public MockProjectLineRepository()
        {
            DateTime testDate = DateTime.Now;

            Project project1 = new Project();
            project1.Name = "Test Project 1";
            project1.Id = 1;
            project1.Status = "status";
            project1.ProjectLine = new List<ProjectLine>();
            project1.User = "username";
            project1.Date = DateTime.Now;
            project1.Comment = new List<Comment>();
            project1.CategoryId = 2;
            project1.Category = new Category { Id = 1, Name = "cat 1" };

            Project project2 = new Project();
            project2.Name = "Test Project 2";
            project2.Id = 2;
            project2.Status = "status";
            project2.ProjectLine = new List<ProjectLine>();
            project2.User = "username";
            project2.Date = DateTime.Now;
            project2.Comment = new List<Comment>();
            project2.CategoryId = 2;
            project2.Category = new Category { Id = 1, Name = "cat 1" };

            
            for (int i = 1; i <= 6; i++)
            {
                ProjectLine TestProjectLine = new ProjectLine();
                TestProjectLine.Id = i;
                TestProjectLine.Date = testDate;
                TestProjectLine.User = "User " + i.ToString();
                TestProjectLine.ProjectId = 1;
                TestProjectLine.Project = project1;
                TestProjectLine.Language = "EN";
                TestProjectLine.TextLine1 = "text1." + i.ToString();
                TestProjectLine.TextLine2 = "text2." + i.ToString();
                TestProjectLine.TimeFrom = testDate.AddHours(-i);
                TestProjectLine.TimeTo = testDate.AddHours(i);
                m_TestList.Add(TestProjectLine);
            }
            for (int i = 7; i <= 10; i++)
            {
                ProjectLine TestProjectLine = new ProjectLine();
                TestProjectLine.Id = i;
                TestProjectLine.Date = testDate;
                TestProjectLine.User = "User " + i.ToString();
                TestProjectLine.ProjectId = 2;
                TestProjectLine.Project = project2;
                TestProjectLine.Language = "IS";
                TestProjectLine.TextLine1 = "text1." + i.ToString();
                TestProjectLine.TextLine2 = "text2." + i.ToString();
                TestProjectLine.TimeFrom = testDate.AddHours(-i);
                TestProjectLine.TimeTo = testDate.AddHours(i);
                m_TestList.Add(TestProjectLine);
            }
        }

        public List<ProjectLine> GetAll()
        {
            var returnList = (from item in m_TestList
                              select item).ToList();
            return returnList;
        }

        public List<ProjectLine> GetByProjectId(int? id)
        {
            List<ProjectLine> TestList = new List<ProjectLine>();
            var returnList = (from item in GetAll()
                              where item.ProjectId == id
                              select item).ToList();
            return returnList;
        }

        public ProjectLine GetSingle(int? id)
        {
            ProjectLine TestProjectLine = new ProjectLine();
            var returnProjectLine = (from item in GetAll()
                              where item.Id == id
                              select item).SingleOrDefault();
            return returnProjectLine;
        }

        public void Create(ProjectLine ProjectLine)
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

            ProjectLine TestProjectLine = new ProjectLine();
            TestProjectLine.Id = 12;
            TestProjectLine.Date = TestDate;
            TestProjectLine.User = "User single ";
            TestProjectLine.ProjectId = 1;
            TestProjectLine.Project = pro;
            TestProjectLine.Language = "EN";
            TestProjectLine.TextLine1 = "text_single1";
            TestProjectLine.TextLine2 = "text_single2";
            TestProjectLine.TimeFrom = TestDate.AddHours(-1);
            TestProjectLine.TimeTo = TestDate.AddHours(1);
            m_TestList.Add(TestProjectLine);
        }

        public void Update(ProjectLine ProjectLine)
        {
            ProjectLine.User = "ChangeUser";
        }

        public void Delete(int? id)
        {
            ProjectLine TestProjectLine = new ProjectLine();
            TestProjectLine = GetSingle(id);
            m_TestList.Remove(TestProjectLine);
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
