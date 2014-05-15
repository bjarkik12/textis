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
    [TestClass]
    public class UpvoteControllerTest
    {
        [TestMethod]
        public void TestUpvoteGetAll()
        {
            // Arrange:
            MockUpvoteRepository reposiotory = new MockUpvoteRepository();
            UpvoteController controller = new UpvoteController(reposiotory);

            // Act:
            var result = controller.Index();
            
            // Assert:
            var viewResult = (ViewResult)result;
            //var model = viewResult.Model;
            List<UpvoteViewModel> model = viewResult.Model as List<UpvoteViewModel>;
            Assert.IsTrue(model.Count == 10);
        }

        [TestMethod]
        public void TestUpvoteGetSingle()
        {
            // Arrange:
            MockUpvoteRepository upvoteReposiotory = new MockUpvoteRepository();
            MockProjectRepository projectReposiotory = new MockProjectRepository();
            UpvoteController controller = new UpvoteController(upvoteReposiotory, projectReposiotory);

            // Act:
            var result = controller.Edit(1);

            // Assert:
            var viewResult = (ViewResult)result;
            //var model = viewResult.Model;
            UpvoteViewModel model = viewResult.Model as UpvoteViewModel;
            Assert.IsTrue(model.Id == 1);
        }
    
    }

    [TestClass]
    public class CommentControllerTest
    {
        [TestMethod]
        public void TestUpvoteGetAll()
        {
            // Arrange:
            MockCommentRepository reposiotory = new MockCommentRepository();
            CommentController controller = new CommentController(reposiotory);

            // Act:
            var result = controller.Index();

            // Assert:
            var viewResult = (ViewResult)result;
            //var model = viewResult.Model;
            List<CommentViewModel> model = viewResult.Model as List<CommentViewModel>;
            Assert.IsTrue(model.Count == 10);
        }
    }
}
