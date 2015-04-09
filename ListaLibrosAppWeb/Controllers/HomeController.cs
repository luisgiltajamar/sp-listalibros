using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListaLibrosAppWeb.Controllers
{
    public class HomeController : Controller
    {
       // [SharePointContextFilter]
        public ActionResult Index()
        {
            if (Session["SPAppUrl"] == null && Session["SPHostUrl"] == null)
            {
                if (Request.QueryString["SPAppWebUrl"] != null)
                {
                    Session["SPAppUrl"] = Request.QueryString["SPAppWebUrl"];
                }
                if (Request.QueryString["SPHostUrl"] != null)
                {
                    Session["SPHostUrl"] = Request.QueryString["SPHostUrl"];
                }
            }
            return RedirectToAction("Index", "Libros");

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
    }
}
