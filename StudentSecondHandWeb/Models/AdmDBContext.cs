using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class AdmDBContext:DbContext
    {
        public  AdmDBContext()
            : base("DataConn")
        {

        }
        public DbSet<AdminInfo> Admins { get; set; }
    }

    [Table("AdminInfoes")]
    public class AdminInfo
    {
        public long ID { get; set; }
        public string AdmName { get; set; }
        public string AdmPhone { get; set; }
        public string AdmPassId { get; set; }
        public string AdmPassword { get; set; }
    }
}