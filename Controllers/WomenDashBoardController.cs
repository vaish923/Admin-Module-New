using AdminModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminModule.Controllers
{
    public class WomenDashBoardController : Controller
    {
        // GET: WomenDashBoard
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Im in WomenDashboard";
            return View();
        }

        [HttpGet]
        public ActionResult GetAllEnrollments()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            using (var db = new Women_EmpowerementEntities())
            {
                var enrollments = db.getAllEnrollments().ToList();
                return View(enrollments);
            }

        }

        [HttpGet]
        public ActionResult GetEnrollmentById(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            using (var db = new Women_EmpowerementEntities())
            {
                var enrollments = db.getAllEnrollmentsById(id).ToList().FirstOrDefault();
                var enroll = db.enrollments.SingleOrDefault(w => w.women_id == id);
                
                ViewBag.Status =Convert.ToString(enroll.approval_status);
                return View(enrollments);
            }

        }

       [HttpPost]
        public ActionResult GetEnrollmentById(int id,getAllEnrollmentsById_Result enrollmentsById)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            using (var db = new Women_EmpowerementEntities())
            {
                
                var enroll = db.enrollments.SingleOrDefault(w => w.women_id == id);
               
                
                if (Request.Form["Approved"] != null)
                {
                    enroll.approval_status = "approved";
                    enroll.approval_date = DateTime.Today;
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Application Approved";
                    return View();

                }
                else if (Request.Form["Reject"] != null)
                {
                    enroll.approval_status = "rejected";
                    enroll.approval_date = DateTime.Today;
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


