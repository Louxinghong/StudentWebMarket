using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentSecondHandWeb.Models
{
    public class StuDBContext:DbContext
    {
        public StuDBContext()
            : base("DataConn")
        {

        }
        public DbSet<StudentInfo> Students { get; set; }
    }

    [Table("StudentInfoes")]
    public class StudentInfo
    {
        //[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public long ID { get; set; }
        public string StuName { get; set; }
        public string StuSex { get; set; }
        public string StuPhone { get; set; }
        public string StuIdCard { get; set; }
        public string StuEmail { get; set; }
        public string StuAddress { get; set; }
        public string StuSchool { get; set; }
        public string StuPassID { get; set; }
        public string StuPassword { get; set; }
    }

}