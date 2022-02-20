using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminModule.Models;

namespace AdminModule.Controllers
{
    public class DashBoardController : Controller
    {
        
        // GET: DashBoard
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Session = Session["username"];
            return View();
        }

        [Route("DashBoard/GetWomenList")]
        public ActionResult GetWomenList()
        {
            using(Women_EmpowerementEntities women_Empowerement=new Women_EmpowerementEntities())
            {
                var womenlist = women_Empowerement.women.ToList();
                return View(womenlist);
            }
            
        }

        [HttpGet]
        
        public ActionResult Details(int id)
        {
            using(Women_EmpowerementEntities women=new Women_EmpowerementEntities())
            {
                var womendata =women.women.Find(id);
                 
                return View(womendata);
            }
            
        }

        

        [HttpPost]
        public ActionResult Details(woman s,int id)
        {
            using(Women_EmpowerementEntities women = new Women_EmpowerementEntities())
            {
                woman womandata = women.women.Find(id);
                if (Request.Form["Approved"] != null)
                {
                    womandata.status = true;
                    women.SaveChanges();
                    ViewBag.SuccessMessage = "Application Approved";
                    return View();

                }
                else if (Request.Form["Reject"] != null)
                {
                    womandata.status = false;
                    women.SaveChanges();
                    ViewBag.SuccessMessage = "Application Rejected";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            

        }


        [HttpGet]
        public ActionResult GetAllEnrollments()
        {
            using(var db=new  Women_EmpowerementEntities()){
                var enrollments = db.getAllEnrollments().ToList();
                return View(enrollments);
            }
         
        }


    }
}