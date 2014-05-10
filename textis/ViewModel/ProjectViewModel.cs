using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using textis.Repository;
using textis.ViewModel;

namespace textis.ViewModel
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProjectLineViewModel> SourceProjectLines { get; set; }
        public List<ProjectLineViewModel> DestinationProjectLines { get; set; }
        public List<CommentViewModel> CommentLines{ get; set; }

        public List<Upvote> UpvoteLines { get; set; }

        public ProjectViewModel()
        {
            //empty
        }

        public ProjectViewModel(Project project)
        {
            ProjectLineRepository m_ProjectLine = new ProjectLineRepository();
            CommentRepository m_Comment = new CommentRepository();
            Id = project.Id;
            User = project.User;
            Date = project.Date;
            Name = project.Name;
            Status = project.Status;
            Url = project.Url;
            CategoryId = project.CategoryId;
            //CategoryName = "bull";
            SourceProjectLines = new List<ProjectLineViewModel>();
            DestinationProjectLines = new List<ProjectLineViewModel>();
            CommentLines = new List<CommentViewModel>();

            foreach(ProjectLine x in m_ProjectLine.GetByProjectId(Id) )
            {
                if (x.Language == "EN" )
                {
                    SourceProjectLines.Add(new ProjectLineViewModel(x));
                }
            }

            foreach (ProjectLine x in m_ProjectLine.GetByProjectId(Id))
            {
                if (x.Language == "IS")
                {
                    DestinationProjectLines.Add(new ProjectLineViewModel(x));
                }
            }

            foreach (Comment x in m_Comment.GetByProjectId(Id))
            {
                CommentLines.Add(new CommentViewModel(x));
            }

        }

        public Project CastViewModelToModel()
        {
            Project m_project = new Project();
            m_project.Id = Id;
            m_project.User = User;
            m_project.Date = Date;
            m_project.Name = Name;
            m_project.Status = Status;
            m_project.Url = Url;
            m_project.CategoryId = CategoryId;
            
            return m_project;
        }

    }
}