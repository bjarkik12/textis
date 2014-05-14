using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public String User { get; set; }
        [DataType(DataType.MultilineText)]
        public String Text { get; set; }
        public DateTime Date { get; set; }

        public CommentViewModel()
        {
            //empty
        }

        public CommentViewModel( Comment comment )
        {
            Id = comment.Id;
            ProjectId = comment.ProjectId;
            // these lines cause crash of comment posting...!
            //ProjectId = comment.Project.Id;
            //ProjectName = comment.Project.Name;
            User = comment.User;
            Text = comment.Text;
            Date = comment.Date;
        }

        public Comment CastViewModelToModel()
        {
            Comment m_comment = new Comment();
            m_comment.Id = Id;
            m_comment.User = User;
            m_comment.Date = Date;
            m_comment.ProjectId = ProjectId;
            m_comment.Text = Text;
            return m_comment;
        }
    }
}