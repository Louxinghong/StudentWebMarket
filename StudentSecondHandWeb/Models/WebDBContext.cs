using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class WebDBContext:DbContext
    {
        public WebDBContext()
            :base("DataConn")
        {

        }
        public DbSet<WebBriefInfo> WebBriefs { get; set; }
    }

    [Table("WebBriefInfoes")]
    public class WebBriefInfo
    {
        public long ID { get; set; }
        public string WebTheme { get; set; }
        public string WebPicture { get; set; }
        public string WebPurpose { get; set; }
        public string WebContext { get; set; }
    }
}