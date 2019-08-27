using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class SchDBContext:DbContext
    {
        public SchDBContext()
            : base("DataConn")
        {

        }
        public DbSet<SchoolInfo> Schools { get; set; }
    }

    [Table("SchoolInfoes")]
    public class SchoolInfo
    {
        public long ID { get; set; }
        public string SchName { get; set; }
        public string SchProvince { get; set; }
        public string SchCity { get; set; }
        public string SchArea { get; set; }
        public string SchGrade { get; set; }
    }
}