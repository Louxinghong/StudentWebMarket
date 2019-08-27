using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentSecondHandWeb.Models;

namespace StudentSecondHandWeb.Controllers
{
    public class StudentController : Controller
    {
        private StuDBContext student = new StuDBContext();
        private ResDBContext thing = new ResDBContext();
        private OrdDBContext order = new OrdDBContext();
        private ColDBContext collection = new ColDBContext();
        private MesDBContext message = new MesDBContext();

        // GET: Student

        //学生信息
        public ActionResult StudentInformation()
        {
            var studentinformation = student.Students.ToList();
            return View(studentinformation);
        }

        //修改信息页面
        public ActionResult StudentInfoModify()
        {
            var cookieuserphone = Request.Cookies["cookieuserphone"].Value;
            long id = 0;
            var stu = student.Students.ToList();
            for (var i = 0; i < stu.Count; i++)
            {
                if (stu[i].StuPhone == cookieuserphone)
                {
                    id = stu[i].ID;
                    break;
                }
                    
            }
            return View(student.Students.Find(id));
        }

        //修改信息
        [HttpPost]
        public ActionResult ConfirmModify(long id, string name, string school, string address)
        {
            string result = "success";

            var allstudent = student.Students.ToList();
            var allthing = thing.Things.ToList();
            var allorder = order.Orders.ToList();
            var allcollection = collection.Collections.ToList();
            var allmessage = message.Messages.ToList();

            for (var i = 0; i < allstudent.Count; i++)
            {
                if (allstudent[i].ID == id)
                {
                    allstudent[i].StuName = name;
                    allstudent[i].StuSchool = school;
                    allstudent[i].StuAddress = address;
                    student.Entry(allstudent[i]).State = EntityState.Modified;
                    student.SaveChanges();
                    break;
                }
            }
            for (var i = 0; i < allthing.Count; i++)
            {
                if (allthing[i].RegPhone == Request.Cookies["cookieuserphone"].Value)
                {
                    allthing[i].RegName = name;
                    allthing[i].RegSchool = school;
                    thing.Entry(allthing[i]).State = EntityState.Modified;
                    thing.SaveChanges();
                }
            }
            for (var i = 0; i < allorder.Count; i++)
            {
                if (allorder[i].BuyerPhone == Request.Cookies["cookieuserphone"].Value)
                {
                    allorder[i].Buyer = name;
                    allorder[i].BuyerSchool = school;
                    order.Entry(allorder[i]).State = EntityState.Modified;
                    order.SaveChanges();
                }
            }
            for (var i = 0; i < allcollection.Count; i++)
            {
                if (allcollection[i].CollectorPhone == Request.Cookies["cookieuserphone"].Value)
                {
                    allcollection[i].Collector = name;
                    allcollection[i].CollectorSchool = school;
                    collection.Entry(allcollection[i]).State = EntityState.Modified;
                    collection.SaveChanges();
                }
            }
            for (var i = 0; i < allmessage.Count; i++)
            {
                if (allmessage[i].MessagePersonPhone == Request.Cookies["cookieuserphone"].Value)
                {
                    allmessage[i].MessagePerson = name;
                    message.Entry(allmessage[i]).State = EntityState.Modified;
                    message.SaveChanges();
                }
            }

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

            return Content(result);
        }

        //修改密码
        [HttpPost]
        public ActionResult ModifyPassword(long id, string oldpassword, string newpassword, string passwordagain)
        {
            string result = "";

            var stu = student.Students.Find(id);

            if(oldpassword == stu.StuPassword)
            {
                if (newpassword == passwordagain)
                {
                    result = "success";
                    stu.StuPassword = newpassword;
                    student.Entry(stu).State = EntityState.Modified;
                    student.SaveChanges();

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
                }
                else
                {
                    result = "twonotsame";
                }
            }
            else
            {
                result = "oldpasnottrue";
            }
    
            return Content(result);
        }

    }
}
