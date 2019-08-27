using PagedList;
using StudentSecondHandWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentSecondHandWeb.Controllers
{
    public class AdminController : Controller
    {
        private ResDBContext thing = new ResDBContext();
        private AreDBContext area = new AreDBContext();
        private ColDBContext collection = new ColDBContext();
        private OrdDBContext order = new OrdDBContext();
        private MesDBContext message = new MesDBContext();
        private NotDBContext notice = new NotDBContext();
        private WebDBContext webbrief = new WebDBContext();
        private StuDBContext student = new StuDBContext();
        private AdmDBContext admin = new AdmDBContext();

        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult AdminMain()
        {
            return View();
        }

        //商品信息
        public ActionResult AdminThingInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var commodities = GetCommodities(pageIndex, pageSize, ref totalCount);
            var thingasipagedlist = new StaticPagedList<ThingInfo>(commodities, pageIndex, pageSize, totalCount);

            return View(thingasipagedlist);
        }

        public List<ThingInfo> GetCommodities(int pageIndex, int pageSize, ref int totalCount)
        {
            var thingcount = thing.Things.ToList();

            var things = (from c in thing.Things orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = thingcount.Count;
            return things.ToList();
        }

        //收藏信息
        public ActionResult AdminCollectionInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var collections = GetCollections(pageIndex, pageSize, ref totalCount);
            var collectionasipagedlist = new StaticPagedList<CollectionInfo>(collections, pageIndex, pageSize, totalCount);

            return View(collectionasipagedlist);
        }

        public List<CollectionInfo> GetCollections(int pageIndex, int pageSize, ref int totalCount)
        {
            var collectioncount = collection.Collections.ToList();

            var cols = (from c in collection.Collections orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = collectioncount.Count;
            return cols.ToList();
        }

        //订单信息
        public ActionResult AdminOrderInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var orders = GetOrders(pageIndex, pageSize, ref totalCount);
            var orderasipagedlist = new StaticPagedList<OrderInfo>(orders, pageIndex, pageSize, totalCount);

            return View(orderasipagedlist);
        }

        public List<OrderInfo> GetOrders(int pageIndex, int pageSize, ref int totalCount)
        {
            var ordercount = order.Orders.ToList();

            var ords = (from c in order.Orders orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = ordercount.Count;
            return ords.ToList();
        }

        //留言信息
        public ActionResult AdminMessageInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var messages = GetMessages(pageIndex, pageSize, ref totalCount);
            var messageasipagedlist = new StaticPagedList<MessageInfo>(messages, pageIndex, pageSize, totalCount);

            return View(messageasipagedlist);
        }

        public List<MessageInfo> GetMessages(int pageIndex, int pageSize, ref int totalCount)
        {
            var messagecount = message.Messages.ToList();

            var mes = (from c in message.Messages orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = messagecount.Count;
            return mes.ToList();
        }

        //网站信息
        public ActionResult AdminWebBriefInfoes()
        {
            var webinfo = webbrief.WebBriefs.ToList();
            return View(webinfo);
        }

        //公告栏信息
        public ActionResult AdminNoticeInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var notices = GetNotices(pageIndex, pageSize, ref totalCount);
            var noticeasipagedlist = new StaticPagedList<NoticeInfo>(notices, pageIndex, pageSize, totalCount);

            return View(noticeasipagedlist);
        }

        public List<NoticeInfo> GetNotices(int pageIndex, int pageSize, ref int totalCount)
        {
            var noticecount = notice.Notices.ToList();

            var not = (from c in notice.Notices orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = noticecount.Count;
            return not.ToList();
        }

        //用户信息
        public ActionResult AdminStudentInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var students = GetStudents(pageIndex, pageSize, ref totalCount);
            var studentasipagedlist = new StaticPagedList<StudentInfo>(students, pageIndex, pageSize, totalCount);

            return View(studentasipagedlist);
        }

        public List<StudentInfo> GetStudents(int pageIndex, int pageSize, ref int totalCount)
        {
            var studentcount = student.Students.ToList();

            var stu = (from c in student.Students orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = studentcount.Count;
            return stu.ToList();
        }

        //管理员信息
        public ActionResult AdministratorInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var admins = GetAdmins(pageIndex, pageSize, ref totalCount);
            var adminasipagedlist = new StaticPagedList<AdminInfo>(admins, pageIndex, pageSize, totalCount);

            return View(adminasipagedlist);
        }

        public List<AdminInfo> GetAdmins(int pageIndex, int pageSize, ref int totalCount)
        {
            var admincount = admin.Admins.ToList();

            var adm = (from c in admin.Admins orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = admincount.Count;
            return adm.ToList();
        }

        //地区信息
        public  ActionResult AdminAreaInfoes(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var areainfoes = GetArea(pageIndex, pageSize, ref totalCount);
            var areaasipagedlist = new StaticPagedList<AreaInfo>(areainfoes, pageIndex, pageSize, totalCount);

            return View(areaasipagedlist);
        }

        public List<AreaInfo> GetArea(int pageIndex, int pageSize, ref int totalCount)
        {
            var zonecount = area.Areas.ToList();

            var areas = (from c in area.Areas orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = zonecount.Count;
            return areas.ToList();
        }

        //判断管理员登录
        [HttpPost]
        public ActionResult CheckAdminLogin(string adminpassid, string adminpassword)
        {
            var checkadmin = admin.Admins.ToList();
            string passidresult = "false";
            string passwordresult = "false";
            string result = string.Empty;
            int number = 0;
            for (int i = 0; i < checkadmin.Count; i++)
            {
                if (checkadmin[i].AdmPassId == adminpassid)
                {
                    passidresult = "true";
                    number = i;
                    break;
                }
            }

            if (checkadmin[number].AdmPassword == adminpassword)
            {
                passwordresult = "true";
            }

            if (passidresult == "true" && passwordresult == "true")
            {
                result = "true";
            }
            else if (passidresult == "false" || passwordresult == "false")
            {
                result = "false";
            }

            if (result == "true")
            {
                HttpCookie cookieforadminname = new HttpCookie("cookieadminname", checkadmin[number].AdmName);
                Response.Cookies.Add(cookieforadminname);
            }

            return Content(result);
        }

        //修改网站简介信息
        public ActionResult ModifyWebBrief(string modifytheme, string modifypicture, string modifypurpose, string modifycontext)
        {
            var modify = webbrief.WebBriefs.Find(1);
            string result = "true";

            modify.WebTheme = modifytheme;
            modify.WebPicture = modifypicture;
            modify.WebPurpose = modifypurpose;
            modify.WebContext = modifycontext;

            webbrief.Entry(modify).State = EntityState.Modified;
            webbrief.SaveChanges();

            return Content(result);
        }

        //修改公告信息
        [HttpPost]
        public ActionResult ModifyOrAddNotice()
        {
            string result = "";
            if (Request.Form["noticeid"] != "")
            {
                long id = long.Parse(Request.Form["noticeid"]);

                var modify = notice.Notices.Find(id);

                modify.NotTitle = Request.Form["noticetitle"];
                modify.NotPicture = Request.Form["picturepath"];
                modify.NotContext = Request.Form["noticecontext"];

                HttpPostedFileBase f = Request.Files["file"];

                if (f.FileName != "")
                {
                    f.SaveAs(@"F:\毕设程序（不断书写、更新）\StudentSecondHandWeb\StudentSecondHandWeb\images\" + f.FileName);
                }

                notice.Entry(modify).State = EntityState.Modified;
                notice.SaveChanges();

                result = "<script>alert('修改成功~');window.location.href = '/Admin/AdminNoticeInfoes';</script>";
            }
            else
            {
                var addnotice = new NoticeInfo();
                addnotice.NotTitle = Request.Form["noticetitle"];
                addnotice.NotPicture = Request.Form["picturepath"];
                addnotice.NotContext = Request.Form["noticecontext"];
                addnotice.NotTime = DateTime.Now;

                HttpPostedFileBase f = Request.Files["file"];
                f.SaveAs(@"F:\毕设程序（不断书写、更新）\StudentSecondHandWeb\StudentSecondHandWeb\images\" + f.FileName);

                notice.Notices.Add(addnotice);
                notice.SaveChanges();

                result = "<script>alert('新增成功~');window.location.href = '/Admin/AdminNoticeInfoes';</script>";
            }

            return Content(result);
        }

        //删除公告
        [HttpPost]
        public ActionResult NoticeDel(long id)
        {
            string result = "true";
            var del = notice.Notices.Find(id);
            notice.Notices.Remove(del);
            notice.SaveChanges();

            return Content(result);
        }

        //删除管理员登录后的cookie信息
        [HttpPost]
        public ActionResult DelCookie()
        {
            string result = string.Empty;
            HttpCookie delcookieforadminname = Request.Cookies["cookieadminname"];
            delcookieforadminname.Expires = DateTime.Now.AddDays(-1);
            Response.AppendCookie(delcookieforadminname);
            result = "true";
            return Content(result);
        }
    }
}