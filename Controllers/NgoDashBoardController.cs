using AdminModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminModule.Controllers
{
    public class NgoDashBoardController : Controller
    {
        // GET: NgoDashBoard
        public ActionResult Index()
        {
            if(Session["username"]==null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Im in NgoDashboard";
            return View();
        }

        [HttpGet]
        public ActionResult GetAllNgoDetails()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            using (Women_EmpowerementEntities womeEmpoweremetEntities = new Women_EmpowerementEntities())
            {
                var ngodata = womeEmpoweremetEntities.ngoes.ToList();
                return View(ngodata);
            }
        }

        [HttpGet]
        public ActionResult GetNgoById(int id, ngo ngo)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            using (Women_EmpowerementEntities womeEmpoweremetEntities = new Women_EmpowerementEntities())
            {
                var ngodata = womeEmpoweremetEntities.ngoes.Find(id);
                ViewBag.Status = ngodata.approvedstatus;
                return View(ngodata);
            }
        }
        [HttpPost]
        public ActionResult GetNgoById(ngo ngo, int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            using (Women_EmpowerementEntities womeEmpoweremetEntities = new Women_EmpowerementEntities())
            {
                ngo ngodata = womeEmpoweremetEntities.ngoes.Find(id);
                ViewBag.Status = ngodata.approvedstatus;
                if (Request.Form["Approved"] != null)
                {
                    ngodata.approvedstatus = "approved";
                    womeEmpoweremetEntities.SaveChanges();
                    ViewBag.SuccessMessage = "Application Approved";
                    return View(ngodata);

                }
                else if (Request.Form["Reject"] != null)
                {
                    ngodata.approvedstatus = "rejected";
                    womeEmpoweremetEntities.SaveChanges();
                    ViewBag.SuccessMessage = "Application Rejected";
                    return View(ngodata);
                }
                else
                {
                    return View(ngodata);
                }
            }

        }
    }
}
