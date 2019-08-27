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
    //viewModel使一个view返回多个数据信息，例如返回所有物品信息、其中一件物品的详细信息、物品留言信息等
    public class ViewModelForThingDetail
    {
        public List<ThingInfo> Thing { get; set; }
        public ThingInfo Thingdetail { get; set; }
        public List<MessageInfo> Message { get; set; }

        public ViewModelForThingDetail(List<Models.ThingInfo> thing, ThingInfo thingdetail, List<Models.MessageInfo> message)
        {
            Thing = thing;
            Thingdetail = thingdetail;
            Message = message;
        }
    }

    public class ThingController : Controller
    {
        // GET: Thing
        private ResDBContext res = new ResDBContext();
        private ColDBContext collection = new ColDBContext();
        private MesDBContext mes = new MesDBContext();

        private static string ResCategory;
        private IQueryable<ThingInfo> ownercols;

        //判断搜索内容时存下搜索内容和搜索类别
        private static string SearchContext;
        private static string Result;

        private IQueryable<ThingInfo> findthings;

        // 显示商品
        public ActionResult ThingMain(string rescategory, int? page)
        {
            var commodity = res.Things.ToList();

            int pageIndex = page ?? 1;

            if (pageIndex == 1 && rescategory != null)
            {
                ResCategory = rescategory;
            }

            int pageSize = 16;
            int totalCount = 0;
            var wantcommodity = GetWantCommodity(pageIndex, pageSize, ref totalCount, ResCategory);
            var commodityasipagedlist = new StaticPagedList<ThingInfo>(wantcommodity, pageIndex, pageSize, totalCount);

            return View(commodityasipagedlist);
        }

        public List<ThingInfo> GetWantCommodity(int pageIndex, int pageSize, ref int totalCount, string ResCategory)
        {
            var comdcount = res.Things.ToList();
            var number = 0;
            int level = 0;

            for (var i = 0; i< comdcount.Count; i++)
            {
                if (comdcount[i].ResCategoryMain == ResCategory)
                {
                    number++;
                    level = 0;
                }
                    
                else if (comdcount[i].ResCategorySecond == ResCategory)
                {
                    number++;
                    level = 1;
                }   
            }
            if (level == 0)
                ownercols = (from c in res.Things where c.ResCategoryMain == ResCategory && c.WhetherSell == "0" && c.WhetherDown == "0" orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (level == 1)
                ownercols = (from c in res.Things where c.ResCategorySecond == ResCategory && c.WhetherSell == "0" && c.WhetherDown == "0" orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = number;
            return ownercols.ToList();

        }

        //物品详细信息
        public ActionResult ThingDetail(long id)
        {
            ThingInfo thingInfo = res.Things.Find(id);
            var ownermescount = mes.Messages.ToList();
            var number = 0;
            for (var i = 0; i < ownermescount.Count; i++)
            {
                if (ownermescount[i].ResID == id)
                    number++;
            }
            var ownermes = (from c in mes.Messages where c.ResID == id orderby c.ID ascending select c).Skip(0).Take(number);


            var viewmodel = new ViewModelForThingDetail(res.Things.ToList(), res.Things.Find(id), ownermes.ToList())
            {
                Thing = res.Things.ToList(),
                Thingdetail = res.Things.Find(id),
                Message = ownermes.ToList()
            };

            return View(viewmodel);
        }

        //新增商品
        public ActionResult AddNewThing()
        {
            return View();
        }

        //个人商品详细信息
        public ActionResult OwnerCommodityDetail(long? id)
        {
            var ownerdetail = res.Things.Find(id);
            return View(ownerdetail);
        }

        //新增商品成功
        public ActionResult AddNewThingSuccess()
        {
            ThingInfo newthing = new ThingInfo();

            newthing.ResName = Request.Form["resname"];
            newthing.ResCategoryMain = Request.Form["rescategorymain"];
            newthing.ResCategorySecond = Request.Form["rescategorysecond"];
            newthing.ResPrice = Request.Form["resprice"];
            newthing.ResIntroduction = Request.Form["resintroduction"];
            newthing.ResColNum = 0;
            newthing.ResPicture = "../images/" + Request.Form["respicture"]; ;
            newthing.RegName = Request.Form["regname"];
            newthing.RegPhone = Request.Form["regphone"];
            newthing.RegIdCard = Request.Form["regidcard"];
            newthing.RegSchool = Request.Form["regschool"];
            newthing.WhetherSell = "0";
            newthing.WhetherDown = "0";
            newthing.ShelfTime = DateTime.Now;

            HttpPostedFileBase f = Request.Files["file"];
            f.SaveAs(@"F:\毕设程序（不断书写、更新）\StudentSecondHandWeb\StudentSecondHandWeb\images\" + f.FileName);

            res.Things.Add(newthing);
            res.SaveChanges();

            return Content("<script>alert('物品登记成功~');window.location.href = '/Thing/OwnerCommodity';</script>");
        }

        //个人商品
        public ActionResult OwnerCommodity(int? page)
        {
            var commodity = res.Things.ToList();

            int pageIndex = page ?? 1;
            int pageSize = 4;
            int totalCount = 0;
            var ownercommodity = GetCommodity(pageIndex, pageSize, ref totalCount);
            var commodityasipagedlist = new StaticPagedList<ThingInfo>(ownercommodity, pageIndex, pageSize, totalCount);

            return View(commodityasipagedlist);
        }

        public List<ThingInfo> GetCommodity(int pageIndex, int pageSize, ref int totalCount)
        {
            var ownercomdcount = res.Things.ToList();
            var cookieuserphone = Request.Cookies["cookieuserphone"].Value;
            var number = 0;
            for (var i = 0; i < ownercomdcount.Count; i++)
            {
                if (ownercomdcount[i].RegPhone == cookieuserphone)
                    number++;
            }
            var ownercols = (from c in res.Things where c.RegPhone == cookieuserphone orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = number;
            return ownercols.ToList();
        }

        //搜索商品
        public ActionResult ThingFromSearch(int? page)
        {
            int pageIndex = page ?? 1;

            int pageSize = 16;
            int totalCount = 0;
            var wantcommodity = GetSearchCommodity(pageIndex, pageSize, ref totalCount, SearchContext, Result);
            var commodityasipagedlist = new StaticPagedList<ThingInfo>(wantcommodity, pageIndex, pageSize, totalCount);

            return View(commodityasipagedlist);
        }

        public List<ThingInfo> GetSearchCommodity(int pageIndex, int pageSize, ref int totalCount, string SearchContext, string Result)
        {
            var comdcount = res.Things.ToList();

            var number = 0;
            for (var i = 0; i < comdcount.Count; i++)
            {
                if (comdcount[i].ResName.Contains(SearchContext) && Result == "findthing")
                    number++;
                else if (comdcount[i].RegSchool.Contains(SearchContext) && Result == "findschool")
                    number++;
            }
            if (Result == "findthing")
                findthings = (from c in res.Things where c.ResName.Contains(SearchContext) && c.WhetherSell == "0" && c.WhetherDown == "0" orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (Result == "findschool")
                findthings = (from c in res.Things where c.RegSchool.Contains(SearchContext) && c.WhetherSell == "0" && c.WhetherDown == "0" orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            totalCount = number;
            return findthings.ToList();
        }

        [HttpPost]
        public ActionResult DownCommodity(long resid, string owner, string ownerphone)
        {
            var updatewhetherdown = res.Things.Find(resid);
            var othercollection = collection.Collections.ToList();
            string result = "true";

            updatewhetherdown.WhetherDown = "1";
            res.Entry(updatewhetherdown).State = EntityState.Modified;//将商品状态改为下架
            res.SaveChanges();

            for (var i = 0; i < othercollection.Count; i++)
            {
                if (othercollection[i].ResID == resid && othercollection[i].Seller == owner && othercollection[i].SellerPhone == ownerphone)
                {
                    othercollection[i].WhetherDown = "1";
                    collection.Entry(othercollection[i]).State = EntityState.Modified;
                    collection.SaveChanges();
                }

            }
            return Content(result);
        }

        [HttpPost]
        public ActionResult SendMessage(long resid, string resname, string seller, string sellerphone, string messageperson, string messagepersonphone, string messagecontext)
        {
            var message = new MessageInfo();
            string result = "true";

            message.ResID = resid;
            message.ResName = resname;
            message.Seller = seller;
            message.SellerPhone = sellerphone;
            message.MessagePerson = messageperson;
            message.MessagePersonPhone = messagepersonphone;
            message.MessageContext = messagecontext;
            message.MessageTime = DateTime.Now;

            mes.Messages.Add(message);
            mes.SaveChanges();

            return Content(result);
        }

        [HttpPost]
        public ActionResult SearchSomething(string searchcontext, string forwhat)
        {
            var things = res.Things.ToList();

            string result = "notfind";
            for (var i = 0; i < things.Count; i++)
            {
                if (things[i].ResName.Contains(searchcontext) && forwhat == "物品")
                {
                    result = "findthing";
                    break;
                }
                else if (things[i].RegSchool.Contains(searchcontext) && forwhat == "学校")
                {
                    result = "findschool";
                    break;
                }

            }

            SearchContext = searchcontext;
            Result = result;

            return Content(result);
        }
    }
}