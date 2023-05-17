using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuvenTur_CRM.Models;

namespace GuvenTur_CRM.Controllers
{
    public class AuthenticationController : Controller
    {
        GuvenTurDBModel db = new GuvenTurDBModel();
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult TryLogin(string emailOrTc, string password)
        {
            try
            {
                string userName = "";
                string userPassword = "";
                int userId = 0;
                int userLevel = 0;
                string userEmail = "";

                List<Users> users = db.Users.OrderBy(o => o.Id).ToList();
                foreach (var item in users)
                {
                    if (emailOrTc == item.Email || emailOrTc == item.User_TC && password == item.Password)
                    {
                        userName = item.User_First_Name + " " + item.User_Last_Name;
                        userId = item.Id;
                        userPassword = item.Password;
                        userLevel = item.Levels.Id;
                        userEmail = item.Email;
                    }
                }

                if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(userPassword))
                {
                    Session["UserName"] = userName;
                    Session["Password"] = userPassword;
                    Session["UserId"] = userId;
                    Session["UserLevel"] = userLevel;
                    Session["UserEmail"] = userEmail;

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Giriş Esnasında Beklenmeyen Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LockScreen()
        {
            ViewBag.UserName = Session["UserName"];
            ViewBag.UserEmail = Session["UserEmail"];

            return View(ViewBag);
        }

        public ActionResult OpenLockScreen(string email, string password)
        {
            if (email.Equals(Session["UserEmail"]) && password.Equals(Session["Password"]))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();

            Response.Redirect("/Authentication/Login");

            return View();
        }
    }
}