using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class AreDBContext:DbContext
    {
        public AreDBContext()
            : base("DataConn")
        {

        }
        public DbSet<AreaInfo> Areas { get; set; }
    }

    [Table("AreaInfoes")]
    public class AreaInfo
    {
        public long ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string FullName { get; set; }
        public string GbCode { get; set; }
    }

}