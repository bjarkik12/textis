using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using textis.Repository;
using textis.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace textis.ViewModel
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        [Required (ErrorMessage = "Vinsamlegast fyllið inn titil myndefnis")]
        public string Name { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        [Required(ErrorMessage = "Vinsamlegast fyllið inn flokk myndefnis")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UpvoteCount { get; set; }
        public List<ProjectLineViewModel> SourceProjectLines { get; set; }
        public List<ProjectLineViewModel> DestinationProjectLines { get; set; }
        public List<CommentViewModel> CommentLines{ get; set; }
        public List<UpvoteViewModel> UpvoteLines { get; set; }

        public ProjectViewModel()
        {
            //empty
        }

        public ProjectViewModel(Project project)
        {
            Id = project.Id;
            User = project.User;
            Date = project.Date;
            Name = project.Name;
            Status = project.Status;
            Url = project.Url;
            CategoryId = project.CategoryId;
            UpvoteCount = 0;
            CategoryName = project.Category.Name;
            SourceProjectLines = new List<ProjectLineViewModel>();
            DestinationProjectLines = new List<ProjectLineViewModel>();
            CommentLines = new List<CommentViewModel>();
            UpvoteLines = new List<UpvoteViewModel>();
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