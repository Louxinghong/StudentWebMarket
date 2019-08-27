using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSecondHandWeb.Models;

namespace StudentSecondHandWeb.Controllers
{
    public class LoginController : Controller
    {
        private StuDBContext db = new StuDBContext();

        // GET: Login

        public ActionResult Login()
        {
            return View();
        }

        //判断登录并保存登录信息cookie
        [HttpPost]
        public ActionResult CheckUser(string passid,string password)
        {
            var checkuser = db.Students.ToList();
            string passidresult = "false";
            string passwordresult = "false";
            string result = string.Empty;
            int number = 0;
            for(int i = 0; i < checkuser.Count; i++)
            {
                if (checkuser[i].StuPassID == passid)
                {
                    passidresult = "true";
                    number = i;
                    break;
                } 
            }
            
            if(checkuser[number].StuPassword == password)
            {
                passwordresult = "true"; 
                   
            }

            if(passidresult == "true" && passwordresult == "true")
            {
                result = "true";
            }
            else if(passidresult == "false" || passwordresult == "false")
            {
                result = "false";
            }

            if(result == "true")
            {
                HttpCookie cookieforusername = new HttpCookie("cookieusername", checkuser[number].StuName);
                HttpCookie cookieforuserphone = new HttpCookie("cookieuserphone", checkuser[number].StuPhone);
                HttpCookie cookieforuserschool = new HttpCookie("cookieuserschool", checkuser[number].StuSchool);
                HttpCookie cookieforuseridcard = new HttpCookie("cookieuseridcard", checkuser[number].StuIdCard);
                Response.Cookies.Add(cookieforusername);
                Response.Cookies.Add(cookieforuserphone);
                Response.Cookies.Add(cookieforuserschool);
                Response.Cookies.Add(cookieforuseridcard);
            }

            return Content(result);
        }
    }
}