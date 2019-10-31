using System.Collections.Generic;
using System.Web.Mvc;
using TutorWebApp.InMemoryData;
using TutorWebApp.Models;

namespace TutorWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static int Id;

        static HomeController()
        {
            Id = 1;
            //Adding Intial Record
            CacheStore.Add(Id.ToString(), new Tutor { Id = 1, Name = "Rahul Malviya", Email = "rahul0456@gmail.com", Topics = "AngularJS Assignment", Price = 1000 });
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        [HttpPost]
        public JsonResult AddEditDetails(Tutor model)
        {
            string status = string.Empty;
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    CacheStore.Remove<Tutor>(model.Id.ToString());
                    CacheStore.Add<Tutor>(model.Id.ToString(), model);
                    status = "updated";
                }
                else
                {
                    model.Id = ++Id;
                    CacheStore.Add(Id.ToString(), model);
                    status = "saved";
                }
            }
            string message = $"Tutor has been { status } successfully!";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            CacheStore.Remove<Tutor>(id.ToString());
            string message = $"User has been removed successfully!";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<Tutor> tutors = CacheStore.GetAll<Tutor>();
            return Json(tutors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            Tutor result = CacheStore.Get<Tutor>(id.ToString());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}