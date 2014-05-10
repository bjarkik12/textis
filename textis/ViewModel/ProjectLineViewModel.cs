using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textis.ViewModel
{
    public class ProjectLineViewModel
    {
        public int ProjectId { get; set; }
        public String User { get; set; }
        public String Text1 { get; set; }
        public String Text2 { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public String Language { get; set; }
        public int LineId { get; set; }

        public ProjectLineViewModel()
        {
            //empty
        }
        public ProjectLineViewModel(ProjectLine projectLine)
        {
            ProjectId = projectLine.ProjectId;
            User = projectLine.User;
            Text1 = projectLine.TextLine1;
            Text2 = projectLine.TextLine2;
            TimeFrom = projectLine.TimeFrom;
            TimeTo = projectLine.TimeTo;
            Language = projectLine.Language;
            LineId = projectLine.Id;
        }
    }
}