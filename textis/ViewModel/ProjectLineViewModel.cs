using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    /// <summary>
    /// Constructs a ViewModel for the ProjectLine Entity to the sent to the View
    /// </summary>
    public class ProjectLineViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUser { get; set; }
        public String User { get; set; }
        public String TextLine1 { get; set; }
        public String TextLine2 { get; set; }
        public DateTime TimeFrom { get; set; }
        public string TimeFromString { get; set; }
        public DateTime TimeTo { get; set; }
        public string TimeToString { get; set; }
        public String Language { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }

        public ProjectLineViewModel()
        {
            //empty
        }

        public ProjectLineViewModel(ProjectLine projectLine)
        {
            ProjectId = projectLine.ProjectId;
            ProjectName = projectLine.Project.Name;
            ProjectUser = projectLine.Project.User;
            User = projectLine.User;
            TextLine1 = projectLine.TextLine1;
            TextLine2 = projectLine.TextLine2;
            TimeFrom = projectLine.TimeFrom;
            TimeFromString = TimeFrom.ToString("HH:mm:ss:fff");
            TimeTo = projectLine.TimeTo;
            TimeToString = TimeTo.ToString("HH:mm:ss:fff");
            Language = projectLine.Language;
            Date = projectLine.Date;
            Id = projectLine.Id;
        }

        /// <summary>
        /// Used to shuffle data from a ViewModel to Model
        /// </summary>
        public ProjectLine CastViewModelToModel()
        {
            ProjectLine projectLine = new ProjectLine();
            projectLine.ProjectId = ProjectId;
            projectLine.User = User;
            projectLine.TextLine1 = TextLine1;
            projectLine.TextLine2 = TextLine2;
            projectLine.TimeFrom = TimeFrom;
            projectLine.TimeTo = TimeTo;
            projectLine.Language = Language;
            projectLine.Date = Date;
            projectLine.Id = Id;
            return projectLine;
        }
    }
}