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
    public class OrderController : Controller
    {
        // GET: Order

        private ResDBContext res = new ResDBContext();
        private OrdDBContext order = new OrdDBContext();
        private ColDBContext collection = new ColDBContext();

        public ActionResult OrderSuccess()
        {
            return View();
        }

        //登录者的订单信息
        public ActionResult OrderInformation(int? page)
        {
            var studentallorder = order.Orders.ToList();
            int pageIndex = page ?? 1;
            int pageSize = 4;
            int totalCount = 0;
            var orders = GetOrder(pageIndex, pageSize, ref totalCount);
            var collectionasipagedlist = new StaticPagedList<OrderInfo>(orders, pageIndex, pageSize, totalCount);

            return View(collectionasipagedlist);
        }

        public List<OrderInfo> GetOrder(int pageIndex, int pageSize, ref int totalCount)
        {
            var ownerordcount = order.Orders.ToList();
            var cookieuserphone = Request.Cookies["cookieuserphone"].Value;
            var number = 0;
            for (var i = 0; i < ownerordcount.Count; i++)
            {
                if (ownerordcount[i].BuyerPhone == cookieuserphone)
                    number++;
            }
            var ownerords = (from c in order.Orders where c.BuyerPhone == cookieuserphone orderby c.ID ascending select c).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = number;
            return ownerords.ToList();
        }


        //物品下单
        [HttpPost]
        public ActionResult MakeOrder(long resid, string resname, string rescategorymain, string rescategorysecond, string resprice, string seller, string sellerphone, string sellerschool, string buyer, string buyerphone, string buyerschool)
        {
            var neworder = new OrderInfo();

            string result = string.Empty;

            var findthing = res.Things.Find(resid);
            var orderpic = findthing.ResPicture;
            var colwhethersell = collection.Collections.ToList();

            findthing.WhetherSell = "1";
            res.Entry(findthing).State = EntityState.Modified;
            res.SaveChanges();

            for (var i = 0; i < colwhethersell.Count; i++)
            {
                if (colwhethersell[i].ResID == findthing.ID)
                {
                    colwhethersell[i].WhetherSell = "1";
                    collection.Entry(colwhethersell[i]).State = EntityState.Modified;
                    collection.SaveChanges();
                }

            }

            neworder.ResID = resid;
            neworder.ResName = resname;
            neworder.ResCategoryMain = rescategorymain;
            neworder.ResCategorySecond = rescategorysecond;
            neworder.ResPrice = resprice;
            neworder.ResPicture = orderpic;
            neworder.Seller = seller;
            neworder.SellerPhone = sellerphone;
            neworder.SellerSchool = sellerschool;
            neworder.Buyer = buyer;
            neworder.BuyerPhone = buyerphone;
            neworder.BuyerSchool = buyerschool;
            neworder.BuyTime = DateTime.Now;
            neworder.WhetherCancelBuy = "0";

            order.Orders.Add(neworder);//保存订单信息
            order.SaveChanges();
            result = "true";
            return Content(result);
        }

        //取消订单
        public ActionResult CancelOrder(long id)
        {
            var cancelorder = order.Orders.Find(id);
            var thing = res.Things.Find(cancelorder.ResID);
            var colwhethersell = collection.Collections.ToList();

            cancelorder.WhetherCancelBuy = "1";
            order.Entry(cancelorder).State = EntityState.Modified;//将订单状态改为取消订单状态
            order.SaveChanges();

            thing.WhetherSell = "0";
            res.Entry(thing).State = EntityState.Modified;
            res.SaveChanges();

            for (var i = 0; i < colwhethersell.Count; i++)
            {
                if (colwhethersell[i].ResID == cancelorder.ResID)
                {
                    colwhethersell[i].WhetherSell = "0";
                    collection.Entry(colwhethersell[i]).State = EntityState.Modified;
                    collection.SaveChanges();
                    break;
                }

            }

            return Content("<script>alert('成功取消订单~');window.location.href = '/Order/OrderInformation';</script>");
        }
    }
}