using AdminModule.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminModule.Controllers
{
    public class HomeController : Controller
    {
        private string constring = @"Data Source=DESKTOP-VEA4621\SQLEXPRESS;Initial Catalog=Women_Empowerement;Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login(int id=0)
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModal loginModal)
        {
            bool result = validateuser(loginModal.username, loginModal.password);
            if (result)
            {
                Session["username"] = loginModal.username.ToString();
                return RedirectToAction("Index", "DashBoard");
            }
            return View();
        }



        public bool validateuser(string username,string password)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select top 1 admin_password from admin where admin_id=@username",con);
                cmd.Parameters.AddWithValue("@username", username);
                string pass = Convert.ToString(cmd.ExecuteScalar());
                if (pass == password)
                    return true;
                return false;
                
            }
        }
    }
}