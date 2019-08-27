using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class ResDBContext : DbContext
    {
        public ResDBContext()
            :base("DataConn")
        {

        }
        public DbSet<ThingInfo> Things { get; set; }
    }

    [Table("ThingInfoes")]
    public class ThingInfo
    {
        public long ID { get; set; }
        public string ResName { get; set; }
        public string ResCategoryMain { get; set; }
        public string ResCategorySecond { get; set; }
        public string ResPrice { get; set; }
        public string ResPicture { get; set; }
        public string ResIntroduction { get; set; }
        public long ResColNum { get; set; }
        public string RegName { get; set; }
        public string RegPhone { get; set; }
        public string RegIdCard { get; set; }
        public string RegSchool { get; set; }
        public string WhetherSell { get; set; }
        public string WhetherDown { get; set; }
        public DateTime ShelfTime { get; set; }
    }


}