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

        public Upvote CastViewModelToModel()
        {
            Upvote m_upvote = new Upvote();
            m_upvote.Id = Id;
            m_upvote.ProjectId = ProjectId;
            m_upvote.User = User;
            m_upvote.Date = Date;
            return m_upvote;
        }
    }
}