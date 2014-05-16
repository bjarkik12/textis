using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    /// <summary>
    /// Constructs a ViewModel for the Upvote Entity to the sent to the View
    /// </summary>
    public class UpvoteViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public String User { get; set; }
        public DateTime Date { get; set; }

        public UpvoteViewModel()
        {
            //empty
        }

        public UpvoteViewModel( Upvote upvote )
        {
            Id = upvote.Id;
            ProjectId = upvote.ProjectId;
            ProjectName = upvote.Project.Name;
            User = upvote.User;
            Date = upvote.Date;
        }

        /// <summary>
        /// Used to shuffle data from a ViewModel to Model
        /// </summary>
        public Upvote CastViewModelToModel()
        {
            Upvote upvote = new Upvote();
            upvote.Id = Id;
            upvote.ProjectId = ProjectId;
            upvote.User = User;
            upvote.Date = Date;
            return upvote;
        }
    }
}