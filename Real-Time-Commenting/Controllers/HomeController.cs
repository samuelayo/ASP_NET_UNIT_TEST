using PusherServer;
using Real_Time_Commenting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Real_Time_Commenting.Controllers
{
    public class HomeController : Controller
    {
        private readonly IrealtimeContext db;

        public HomeController() {
            db = new ApplicationDbContext();
        }
       
        public HomeController(IrealtimeContext context)

        {
            db = context;
        }
        public ActionResult Index()
        {

            return View(db.BlogPost.AsQueryable());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BlogPost post)
        {
            db.BlogPost.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return View(db.BlogPost.Find(id));
        }

        public ActionResult Comments(int? id)
        {
            var comments = db.Comment.Where(x => x.BlogPostID == id).ToArray();
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Comment(Comment data)
        {
            db.Comment.Add(data);
            db.SaveChanges();
            var options = new PusherOptions();
            options.Cluster = "mt1";
            var pusher = new Pusher("321765", "ec75c89294f1617ae16e", "1d93c1bc7b6c6d0cd03f", options);
            ITriggerResult result = await pusher.TriggerAsync("asp_channel", "asp_event", data);
            return Content("ok");
        }
    }
}
