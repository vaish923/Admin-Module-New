using AdminModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminModule.Controllers
{
    public class CourseDashBoardController : Controller
    {
        // GET: CourseDashBoard
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Im in CourseDashboard";
            return View();
        }

        public ActionResult GetAllCourses()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            using (Women_EmpowermentEntities db=new Women_EmpowermentEntities())
            {
                var coursedata = db.getAllCourses().ToList();
                return View(coursedata);
            }
           
        }

        [HttpGet]
        public ActionResult GetCourseById(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            using (Women_EmpowermentEntities db = new Women_EmpowermentEntities())
            {
                var coursedata = db.getCourseById(id).FirstOrDefault();
                ViewBag.Status =Convert.ToString(coursedata.approvedstatus);
                return View(coursedata);
            }
        }

        [HttpPost]
        public ActionResult GetCourseById(int id,getCourseById_Result getCourseById)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            using(Women_EmpowermentEntities db=new Women_EmpowermentEntities())
            {
                var course = db.courses.Find(id);


                if (Request.Form["Approved"] != null)
                {
                    course.approvedstatus= "approved";
                   // enroll.approval_date = DateTime.Today;
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Application Approved";
                    return View();

                }
                else if (Request.Form["Reject"] != null)
                {
                    course.approvedstatus = "rejected";
                 
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Application Rejected";
                    return View();
                }
                else
                {
                    return View();
                }
            }
           
        }
    }
}