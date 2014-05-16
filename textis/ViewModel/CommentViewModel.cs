using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    /// <summary>
    /// Constructs a ViewModel for the Comment Entity to the sent to the View
    /// </summary>
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
            User = comment.User;
            Text = comment.Text;
            Date = comment.Date;
        }

        /// <summary>
        /// Used to shuffle data from a ViewModel to Model
        /// </summary>
        public Comment CastViewModelToModel()
        {
            Comment comment = new Comment();
            comment.Id = Id;
            comment.User = User;
            comment.Date = Date;
            comment.ProjectId = ProjectId;
            comment.Text = Text;
            return comment;
        }
    }
}