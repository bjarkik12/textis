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
    /// Testing UpvoteController actions
    /// </summary>
    [TestClass]
    public class UpvoteControllerTest
    {
        /// <summary>
        /// Test UpvoteController.Index
        /// </summary>
        [TestMethod]
        public void TestUpvoteIndex()
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

        /// <summary>
        /// Test UpvoteController.Edit
        /// </summary>
        [TestMethod]
        public void TestUpvoteEdit()
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
}
