using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using textis.Repository;
using textis.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace textis.ViewModel
{
    /// <summary>
    /// Constructs a ViewModel for the Project Entity to the sent to the View
    /// </summary>
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

        /// <summary>
        /// Used to shuffle data from a ViewModel to Model
        /// </summary>
        public Project CastViewModelToModel()
        {
            Project project = new Project();
            project.Id = Id;
            project.User = User;
            project.Date = Date;
            project.Name = Name;
            project.Status = Status;
            project.Url = Url;
            project.CategoryId = CategoryId; 
            return project;
        }
    }
}