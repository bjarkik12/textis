using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using textis.Tests.Mock;
using textis.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using textis.ViewModel;
using textis.Tests.Controllers;

namespace textis.Tests.Controllers
{
    /// <summary>
    /// Testing ProjectController
    /// </summary>
    [TestClass]
    public class ProjectControllerTest
    {
        /// <summary>
        /// Test ProjectController.Details
        /// </summary>
        [TestMethod]
        public void TestProjectDetails()
        {
            // Arrange:
            MockProjectRepository projectReposiotory = new MockProjectRepository();
            MockCategoryRepository categoryRepository = new MockCategoryRepository();
            MockCommentRepository commentRepository = new MockCommentRepository();
            MockUpvoteRepository upvoteRepository = new MockUpvoteRepository();
            MockProjectLineRepository projectLineRepository = new MockProjectLineRepository();
            ProjectController controller = new ProjectController(projectReposiotory, categoryRepository, 
                                                                 commentRepository, upvoteRepository, projectLineRepository);

            // Act:
            var result = controller.Details(1);

            // Assert:
            var viewResult = (ViewResult)result;
            ProjectViewModel model = viewResult.Model as ProjectViewModel;
            Assert.IsTrue(model.Id == 1);
        }
    }
    
}
