using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public String User { get; set; }
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
            User = comment.User;
            Text = comment.Text;
            Date = comment.Date;
        }
    }
}