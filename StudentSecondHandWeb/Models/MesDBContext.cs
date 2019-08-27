using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class MesDBContext:DbContext
    {
        public MesDBContext()
            : base("DataConn")
        {

        }

        public DbSet<MessageInfo> Messages { get; set; }
    }

    [Table("MessageInfoes")]
    public class MessageInfo
    {
        public long ID { get; set; }
        public long ResID { get; set; }
        public string ResName { get; set; }
        public string Seller { get; set; }
        public string SellerPhone { get; set; }
        public string MessagePerson { get; set; }
        public string MessagePersonPhone { get; set; }
        public string MessageContext { get; set; }
        public DateTime MessageTime { get; set; }
    }
}