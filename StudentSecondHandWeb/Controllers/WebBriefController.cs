using StudentSecondHandWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentSecondHandWeb.Controllers
{
    public class WebBriefController : Controller
    {

        private WebDBContext web = new WebDBContext();
        // GET: WebBrief
        public ActionResult WebBrief()
        {
            var brief = web.WebBriefs.Find(1);
            return View(brief);
        }
    }
}