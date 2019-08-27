using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class ColDBContext:DbContext
    {
        public ColDBContext()
            : base("DataConn")
        {

        }
        public DbSet<CollectionInfo> Collections { get; set; }
    }

    [Table("CollectionInfoes")]
    public class CollectionInfo
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
        public DateTime ShelfTime { get; set; }
        public string WhetherSell { get; set; }
        public string WhetherDown { get; set; }
        public string Collector { get; set; }
        public string CollectorPhone { get; set; }
        public string CollectorSchool { get; set; }
        public string WhetherCancel { get; set; }
    }
}