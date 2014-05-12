using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    public class UpvoteViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
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
            User = upvote.User;
            Date = upvote.Date;
        }
    }
}