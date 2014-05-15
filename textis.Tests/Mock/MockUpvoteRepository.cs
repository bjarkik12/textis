using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textis.Repository;
using textis.Tests.Repository;

namespace textis.Tests.Mock
{
    class MockUpvoteRepository : IUpvoteRepository
    {
        private List<Upvote> m_TestList = new List<Upvote>();

        public MockUpvoteRepository()
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
            for (int i = 1; i <= 6; i++)
            {
                Upvote TestUpvote = new Upvote();
                TestUpvote.Id = i;
                TestUpvote.Date = TestDate;
                TestUpvote.User = "User " + i.ToString();
                TestUpvote.ProjectId = 2;
                TestUpvote.Project = pro;
                m_TestList.Add(TestUpvote);
            }
            for (int i = 7; i <= 10; i++)
            {
                Upvote TestUpvote = new Upvote();
                TestUpvote.Id = i;
                TestUpvote.Date = TestDate;
                TestUpvote.User = "Another User " + i.ToString();
                TestUpvote.ProjectId = 2;
                TestUpvote.Project = pro;
                m_TestList.Add(TestUpvote);
            }
        }

        public List<Upvote> GetAll()
        {
            var returnList = (from item in m_TestList
                              select item).ToList();
            return returnList;
        }

        public List<Upvote> GetByProjectId(int? id)
        {
            List<Upvote> TestList = new List<Upvote>();
            var returnList = (from item in GetAll()
                              where item.ProjectId == id
                              select item).ToList();
            return returnList;
        }

        public Upvote GetSingle(int? id)
        {
            Upvote TestUpvote = new Upvote();
            var returnUpvote = (from item in GetAll()
                              where item.ProjectId == id
                              select item).SingleOrDefault();
            return returnUpvote;
        }

        public void Create(Upvote upvote)
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

            Upvote TestUpvote = new Upvote();
            TestUpvote.Date = DateTime.Now;
            TestUpvote.Id = 12;
            TestUpvote.User = "CreateSingleUser";
            TestUpvote.ProjectId = 3;
            TestUpvote.Project = pro;
            m_TestList.Add(TestUpvote);
        }

        public void Update(Upvote upvote)
        {
            //Upvote TestUpvote = new Upvote();
            //TestUpvote = GetSingle(upvote.Id);
            upvote.User = "ChangeUser";
        }

        public void Delete(int? id)
        {
            Upvote TestUpvote = new Upvote();
            TestUpvote = GetSingle(id);
            m_TestList.Remove(TestUpvote);
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
