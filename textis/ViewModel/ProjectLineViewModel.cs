using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    public class ProjectLineViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUser { get; set; }
        public String User { get; set; }
        public String Text1 { get; set; }
        public String Text2 { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public String Language { get; set; }
        public DateTime Date { get; set; }
        public int LineId { get; set; }

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
            Text1 = projectLine.TextLine1;
            Text2 = projectLine.TextLine2;
            TimeFrom = projectLine.TimeFrom;
            TimeTo = projectLine.TimeTo;
            Language = projectLine.Language;
            Date = projectLine.Date;
            LineId = projectLine.Id;
        }

        public ProjectLine CastViewModelToModel()
        {
            ProjectLine m_ProjectLine = new ProjectLine();
            m_ProjectLine.ProjectId = ProjectId;
            //m_ProjectLine.Project.Name = ProjectName;
            //m_ProjectLine.Project.User = ProjectUser;
            m_ProjectLine.User = User;
            m_ProjectLine.TextLine1 = Text1;
            m_ProjectLine.TextLine2 = Text2;
            m_ProjectLine.TimeFrom = TimeFrom;
            m_ProjectLine.TimeTo = TimeTo;
            m_ProjectLine.Language = Language;
            m_ProjectLine.Date = Date;
            m_ProjectLine.Id = LineId;
            return m_ProjectLine;

        }
    }
}