using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using textis.Tests.Mock;
using textis.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using textis.ViewModel;

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
        public void TestDelete()
        {
            // Arrange:
            MockUpvoteRepository reposiotory = new MockUpvoteRepository();
            UpvoteController controller = new UpvoteController(reposiotory);

            // Act:
            var result = controller.Delete(1);

            // Assert:
            var viewResult = (ViewResult)result;
            //var model = viewResult.Model;
            List<UpvoteViewModel> model = viewResult.Model as List<UpvoteViewModel>;
            Assert.IsTrue(model.Count == 9);
        }

        [TestMethod]
        public void TestUpvoteGetSingle()
        {
            // Arrange:
            MockUpvoteRepository reposiotory = new MockUpvoteRepository();
            UpvoteController controller = new UpvoteController(reposiotory);

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
