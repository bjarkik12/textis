using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textis.Repository;
using textis.Tests.Repository;

namespace textis.Tests.Mock
{
    class MockCommentRepository : ICommentRepository
    {
        private List<Comment> m_TestList = new List<Comment>();

        public MockCommentRepository()
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
                Comment TestComment = new Comment();
                TestComment.Id = i;
                TestComment.Date = TestDate;
                TestComment.User = "User " + i.ToString();
                TestComment.ProjectId = 1;
                TestComment.Project = pro1;
                TestComment.Text = "CommentText " + i;
                m_TestList.Add(TestComment);
            }
            for (int i = 7; i <= 10; i++)
            {
                Comment TestComment = new Comment();
                TestComment.Id = i;
                TestComment.Date = TestDate;
                TestComment.User = "Another User " + i.ToString();
                TestComment.ProjectId = 2;
                TestComment.Project = pro2;
                TestComment.Text = "CommentText " + i;
                m_TestList.Add(TestComment);
            }
        }

        public List<Comment> GetAll()
        {
            var returnList = (from item in m_TestList
                              select item).ToList();
            return returnList;
        }

        public List<Comment> GetByProjectId(int? id)
        {
            List<Comment> TestList = new List<Comment>();
            var returnList = (from item in GetAll()
                              where item.ProjectId == id
                              select item).ToList();
            return returnList;
        }

        public Comment GetSingle(int? id)
        {
            Comment TestComment = new Comment();
            var returnComment = (from item in GetAll()
                              where item.Id == id
                              select item).SingleOrDefault();
            return returnComment;
        }

        public void Create(Comment comment)
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

            Comment TestComment = new Comment();
            TestComment.Date = DateTime.Now;
            TestComment.Id = 12;
            TestComment.User = "CreateSingleUser";
            TestComment.ProjectId = 3;
            TestComment.Project = pro;
            TestComment.Text = "CommentText new";
            m_TestList.Add(TestComment);
        }

        public void Update(Comment comment)
        {
            //Comment TestComment = new Comment();
            //TestComment = GetSingle(comment.Id);
            comment.User = "ChangeUser";
        }

        public void Delete(int? id)
        {
            Comment TestComment = new Comment();
            TestComment = GetSingle(id);
            m_TestList.Remove(TestComment);
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
