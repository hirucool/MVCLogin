﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Login1.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l)
        {
            if (ModelState.IsValid)
            {
                using (mvcLoginEntities data = new mvcLoginEntities())
                {
                    var values = data.Logins.Where(a => a.Username.Equals(l.Username) && a.Password.Equals(l.Password)).FirstOrDefault();
                    if (values != null)
                    {
                        Session["LogedUserID"] = values.Username.ToString();
                        return RedirectToAction("AfterLogin");
                     }
                    else
                    {
                        return RedirectToAction("FailedLogin");
                    }
                }
            }
            return View(l);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                Session.Clear();
                return View();
            }
            else
            {
                Session.Clear();
                return RedirectToAction("FailedLogin");
                
            }
        }

        public ActionResult FailedLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return RedirectToAction("AfterLogin");
            }
            else
            {
                return View();
            }
        }
    }
}