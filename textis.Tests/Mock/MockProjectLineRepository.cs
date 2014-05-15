using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textis.Repository;
using textis.Tests.Repository;

namespace textis.Tests.Mock
{
    class MockProjectLineRepository : IProjectLineRepository
    {
        private List<ProjectLine> m_TestList = new List<ProjectLine>();

        public MockProjectLineRepository()
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
                ProjectLine TestProjectLine = new ProjectLine();
                TestProjectLine.Id = i;
                TestProjectLine.Date = TestDate;
                TestProjectLine.User = "User " + i.ToString();
                TestProjectLine.ProjectId = 1;
                TestProjectLine.Project = pro1;
                TestProjectLine.Language = "EN";
                TestProjectLine.TextLine1 = "text1." + i.ToString();
                TestProjectLine.TextLine2 = "text2." + i.ToString();
                TestProjectLine.TimeFrom = TestDate.AddHours(-i);
                TestProjectLine.TimeTo = TestDate.AddHours(i);
                m_TestList.Add(TestProjectLine);
            }
            for (int i = 7; i <= 10; i++)
            {
                ProjectLine TestProjectLine = new ProjectLine();
                TestProjectLine.Id = i;
                TestProjectLine.Date = TestDate;
                TestProjectLine.User = "User " + i.ToString();
                TestProjectLine.ProjectId = 2;
                TestProjectLine.Project = pro2;
                TestProjectLine.Language = "IS";
                TestProjectLine.TextLine1 = "text1." + i.ToString();
                TestProjectLine.TextLine2 = "text2." + i.ToString();
                TestProjectLine.TimeFrom = TestDate.AddHours(-i);
                TestProjectLine.TimeTo = TestDate.AddHours(i);
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
            //ProjectLine TestProjectLine = new ProjectLine();
            //TestProjectLine = GetSingle(ProjectLine.Id);
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
