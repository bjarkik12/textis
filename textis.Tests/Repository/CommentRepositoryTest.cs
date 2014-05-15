using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textis.Repository;

namespace textis.Tests.Repository
{
    class CommentRepositoryTest : ICommentRepository
    {
        private List<Comment> m_TestList = new List<Comment>();

        public CommentRepositoryTest ()
        {
            DateTime TestDate = DateTime.Now;
            for (int i = 1; i <= 6; i++)
            {
                Comment TestComment = new Comment();
                TestComment.Id = i;
                TestComment.Date = TestDate.AddDays(i);
                TestComment.User = "User " + i.ToString();
                TestComment.Text = "Test text " + i.ToString();
                TestComment.ProjectId = 1;
                m_TestList.Add(TestComment);
            }
            for (int i = 7; i <= 10; i++)
            {
                Comment TestComment = new Comment();
                TestComment.Id = i;
                TestComment.Date = TestDate.AddDays(i*-1);
                TestComment.User = "Another User " + i.ToString();
                TestComment.Text = "Test text Test Text " + i.ToString();
                TestComment.ProjectId = 2;
                m_TestList.Add(TestComment);
            }
        }

        public List<Comment> GetAll()
        {
            List<Comment> TestList = new List<Comment>();
            var returnList = (from item in GetAll()
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
                              where item.ProjectId == id
                              select item).SingleOrDefault();
            return returnComment;
        }

        public void Create(Comment comment)
        {
            Comment TestComment = new Comment();
            TestComment.Id = 12;
            TestComment.Date = DateTime.Now;
            TestComment.User = "CreateSingleUser";
            TestComment.Text = "Test from Single user";
            TestComment.ProjectId = 3;
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
