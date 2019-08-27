using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class OrdDBContext:DbContext
    {
        public OrdDBContext()
            :base("DataConn")
        {

        }
        public DbSet<OrderInfo> Orders { get; set; }
    }

    [Table("OrderInfoes")]
    public class OrderInfo
    {
        public long ID { get; set; }
        public long ResID { get; set; }
        public string ResName { get; set; }
        public string ResCategoryMain { get; set; }
        public string ResCategorySecond { get; set; }
        public string ResPrice { get; set; }
        public string ResPicture { get; set; }
        public string Seller { get; set; }
        public string SellerPhone { get; set; }
        public string SellerSchool { get; set; }
        public string Buyer { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerSchool { get; set; }
        public DateTime BuyTime { get; set; }
        public string WhetherCancelBuy { get; set; }
    }
}