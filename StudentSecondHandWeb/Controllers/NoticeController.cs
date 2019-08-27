using PagedList;
using StudentSecondHandWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentSecondHandWeb.Controllers
{
    public class NoticeController : Controller
    {

        private NotDBContext notice = new NotDBContext();
        // GET: Notice

        public ActionResult AllNotices(int? page)
        {
            var noticecount = notice.Notices.ToList();
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var notices = GetNotices(pageIndex, pageSize, ref totalCount);
            var noticeasipagedlist = new StaticPagedList<NoticeInfo>(notices, pageIndex, pageSize, totalCount);

            return View(noticeasipagedlist);
        }

        public List<NoticeInfo> GetNotices(int pageIndex, int pageSize, ref int totalCount)
        {
            var noticenum = (from c in notice.Notices orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = notice.Notices.ToList().Count;
            return noticenum.ToList();
        }

        public ActionResult ShowNotice(long id)
        {
            var not = notice.Notices.Find(id);
            return View(not);
        }
    }
}