using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Real_Time_Commenting.Controllers;
using Real_Time_Commenting.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Time_Commenting.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIndex()
        {
            var context = new FakedbContext { BlogPost = { new BlogPost { Title = "test", Body="test" } } };
            HomeController HomeController = new HomeController(context);
            ViewResult result = HomeController.Index() as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<BlogPost>));
            var posts = (IEnumerable<BlogPost>)result.ViewData.Model;
            Assert.AreEqual("test", posts.ElementAt(0).Title);
            Assert.AreEqual("test", posts.ElementAt(0).Body);
        }
        [TestMethod]
        public void TestDetails()
        {
            var context = new FakedbContext { BlogPost = { new BlogPost { BlogPostID=1, Title = "test", Body = "test" } } };
            HomeController HomeController = new HomeController(context);
            ViewResult result = HomeController.Details(1) as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(BlogPost));
            var post = (BlogPost)result.ViewData.Model;
            Assert.AreEqual(1, post.BlogPostID);

        }
       
        [TestMethod]
        public void CreateGet()
        {
            var context = new FakedbContext { };
            HomeController HomeController = new HomeController(context);
            ViewResult result = HomeController.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }
        [TestMethod]
        public void CreatePost()

        {
            var context = new FakedbContext{};
             BlogPost Post = new BlogPost();
             Post.Title = "Test Post";
             Post.Body = "Test Body";
             HomeController HomeController = new HomeController(context);
            RedirectToRouteResult result = HomeController.Create(Post) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        
             Assert.IsNotNull(result.ToString());

   

        }
        [TestMethod]
        public void TestComments()
        {
            var context = new FakedbContext { Comment = { new Comment { BlogPostID = 1, CommentID = 1, Name = "test", Body = "test" }, new Comment { BlogPostID = 1, CommentID = 1, Name = "test", Body = "test" } } };
            HomeController HomeController = new HomeController(context);
            JsonResult result = HomeController.Comments(1) as JsonResult;
            var list = (IList<Comment>)result.Data;
            Assert.AreEqual(list.Count, 2);
       
   

        }

        [TestMethod]
        public async Task TestComment()
        {
            var context = new FakedbContext { };
            HomeController HomeController = new HomeController(context);
            var comment = new Comment { BlogPostID = 1, CommentID = 1, Name = "test", Body = "test" };
            ContentResult result = await HomeController.Comment(comment) as ContentResult;
            Assert.AreEqual(result.Content, "ok");
        }

    }
}
