using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class NotDBContext : DbContext
    {
        public NotDBContext()
            : base("DataConn")
        {

        }
        public DbSet<NoticeInfo> Notices { get; set; }
    }

    [Table("NoticeInfoes")]
    public class NoticeInfo
    {
        public long ID { get; set; }
        public string NotTitle { get; set; }
        public string NotPicture { get; set; }
        public string NotContext { get; set; }
        public DateTime NotTime { get; set; }
    }
    
}