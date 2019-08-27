using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSecondHandWeb.Models;

namespace StudentSecondHandWeb.Controllers
{
    public class ViewModelForMain
    {
        public List<ThingInfo> Thing { get; set; }
        public List<NoticeInfo> Notice { get; set; }
        public List<WebBriefInfo> WebBrief { get; set; }

        public ViewModelForMain(List<Models.ThingInfo> thing, List<Models.NoticeInfo> notice, List<Models.WebBriefInfo> webbrief)
        {
            Thing = thing;
            Notice = notice;
            WebBrief = webbrief;
        }
    }

    public class HomeController : Controller
    {
        private ResDBContext res = new ResDBContext();
        private NotDBContext not = new NotDBContext();
        private WebDBContext web = new WebDBContext();

        public ActionResult Main()
        {

            var notices = (from c in not.Notices orderby c.ID descending select c).Skip(0).Take(not.Notices.ToList().Count);

            var viewmodel = new ViewModelForMain(res.Things.ToList(), notices.ToList(), web.WebBriefs.ToList())
            {
                Thing = res.Things.ToList(),
                Notice = notices.ToList(),
                WebBrief = web.WebBriefs.ToList()
            };
            return View(viewmodel);
        }


        //删除cookie
        [HttpPost]
        public ActionResult DelCookie()
        {
            string result = string.Empty;
            HttpCookie delcookieforusername = Request.Cookies["cookieusername"];
            HttpCookie delcookieforuserphone = Request.Cookies["cookieuserphone"];
            HttpCookie delcookieforuserschool = Request.Cookies["cookieuserschool"];
            HttpCookie delcookieforuseridcard = Request.Cookies["cookieuseridcard"];
            delcookieforusername.Expires = DateTime.Now.AddDays(-1);
            delcookieforuserphone.Expires = DateTime.Now.AddDays(-1);
            delcookieforuserschool.Expires = DateTime.Now.AddDays(-1);
            delcookieforuseridcard.Expires = DateTime.Now.AddDays(-1);

            Response.AppendCookie(delcookieforusername);
            Response.AppendCookie(delcookieforuserphone);
            Response.AppendCookie(delcookieforuserschool);
            Response.AppendCookie(delcookieforuseridcard);

            result = "true";
            return Content(result);
        }
    }
}