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

    public class CollectionController : Controller
    {
        // GET: Collection
        
        private ResDBContext res = new ResDBContext();
        private ColDBContext collection = new ColDBContext();

        public ActionResult CollectSuccess()
        {
            return View();
        }

        //获取登录者的收藏信息
        public ActionResult CollectionInformation(int? page)
        {
            var studentallcollection = collection.Collections.ToList();
            int pageIndex = page ?? 1;
            int pageSize = 4;
            int totalCount = 0;
            var collections = GetCollection(pageIndex, pageSize, ref totalCount);
            var collectionasipagedlist = new StaticPagedList<CollectionInfo>(collections, pageIndex, pageSize, totalCount);

            return View(collectionasipagedlist);
        }

        public List<CollectionInfo> GetCollection(int pageIndex, int pageSize, ref int totalCount)
        {
            var ownercolcount = collection.Collections.ToList();
            var cookieuserphone = Request.Cookies["cookieuserphone"].Value;
            var number = 0;
            for (var i = 0; i < ownercolcount.Count; i++)
            {
                if (ownercolcount[i].CollectorPhone == cookieuserphone && ownercolcount[i].WhetherCancel == "0")
                    number++;
            }
            var ownercols = (from c in collection.Collections where c.CollectorPhone == cookieuserphone && c.WhetherCancel == "0" orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = number;
            return ownercols.ToList();
        }

        //收藏信息详细界面
        public ActionResult CollectionDetail(long? id)
        {
            ThingInfo resdetail = res.Things.Find(id);
            return View(resdetail);
        }

        //物品收藏
        [HttpPost]
        public ActionResult MakeCollection(long resid, string resname, string rescategorymain, string rescategorysecond, string resprice, string seller, string sellerphone, string sellerschool, string collector, string collectorphone, string collectorschool)
        {
            var newcollection = new CollectionInfo();
            var updatethingcolnum = res.Things.Find(resid);
            var ownercollection = collection.Collections.ToList();

            string result = "true";
            var findthing = res.Things.ToList();
            var collectionpic = "";
            var whethersell = "";
            var whetherdown = "";
            long thingcolnum = 0; ;

            for (var x = 0; x < ownercollection.Count; x++)
            {
                if (ownercollection[x].ResID == resid && ownercollection[x].Collector == collector && ownercollection[x].WhetherCancel == "0")
                {
                    result = "false";
                    break;
                }
            }
            if(result == "true")
            {
  
                collectionpic = updatethingcolnum.ResPicture;
                whethersell = updatethingcolnum.WhetherSell;
                whetherdown = updatethingcolnum.WhetherDown;
                thingcolnum = updatethingcolnum.ResColNum + 1;

                updatethingcolnum.ResColNum = thingcolnum;
                res.Entry(updatethingcolnum).State = EntityState.Modified;//点击收藏后收藏数量加1
                res.SaveChanges();

                newcollection.ResID = resid;
                newcollection.ResName = resname;
                newcollection.ResCategoryMain = rescategorymain;
                newcollection.ResCategorySecond = rescategorysecond;
                newcollection.ResPrice = resprice;
                newcollection.ResPicture = collectionpic;
                newcollection.Seller = seller;
                newcollection.SellerPhone = sellerphone;
                newcollection.SellerSchool = sellerschool;
                newcollection.ShelfTime = updatethingcolnum.ShelfTime;
                newcollection.WhetherSell = whethersell;
                newcollection.WhetherDown = whetherdown;
                newcollection.Collector = collector;
                newcollection.CollectorPhone = collectorphone;
                newcollection.CollectorSchool = collectorschool;
                newcollection.WhetherCancel = "0";

                collection.Collections.Add(newcollection);//保存收藏信息
                collection.SaveChanges();

            }
            
            return Content(result);
        }


        //取消收藏
        [HttpPost]
        public ActionResult CancelCollection(long resid, string collector, string collectorphone)
        {
            var updatethingcolnum = res.Things.Find(resid);
            var ownercollection = collection.Collections.ToList();
            string result = "true";

            updatethingcolnum.ResColNum = updatethingcolnum.ResColNum - 1;

            for (var i = 0; i < ownercollection.Count; i++)
            {
                if (ownercollection[i].ResID == resid && ownercollection[i].Collector == collector && ownercollection[i].CollectorPhone == collectorphone && ownercollection[i].WhetherCancel == "0")
                {
                    ownercollection[i].WhetherCancel = "1";
                    collection.Entry(ownercollection[i]).State = EntityState.Modified;
                    collection.SaveChanges();
                    break;
                }
                    
            }

            res.Entry(updatethingcolnum).State = EntityState.Modified;//点击取消收藏后收藏数量减1
            res.SaveChanges();
            

            return Content(result);
        }

        
    }
}