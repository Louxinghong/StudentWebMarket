using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSecondHandWeb.Models;

namespace StudentSecondHandWeb.Controllers
{
    public class RegisterController : Controller
    {
        private StuDBContext data = new StuDBContext();
        // GET: Register

        public ActionResult Register()
        {
            return View();
        }
        
        //判断注册并保存信息
        [HttpPost]
        public ActionResult CheckUser(string username,string sex,string phone,string idcard,string email,string address,string school,string passid,string password)
        {     
            var newuser = new StudentInfo();
            var check = data.Students.ToList();
            string result = "success";

            newuser.StuName = username;
            newuser.StuSex = sex;
            newuser.StuPhone = phone;
            newuser.StuIdCard = idcard;
            newuser.StuEmail = email;
            newuser.StuAddress = address;
            newuser.StuSchool = school;
            newuser.StuPassID = passid;
            newuser.StuPassword = password;

            for (int i = 0; i < check.Count; i++)
            {
                if (username == check[i].StuName)
                {
                    result = "falsename";
                    return Content(result);
                }
            }

            for (int i = 0;i < check.Count; i++)
            {
                if(passid == check[i].StuPassID)
                {
                    result = "falsepassid";
                    return Content(result);
                }
            }
            for (int i = 0; i < check.Count; i++)
            {
                if (phone == check[i].StuPhone)
                {
                    result = "falsephone";
                    return Content(result);
                }
            }
            for (int i = 0; i < check.Count; i++)
            {
                if (idcard == check[i].StuIdCard)
                {
                    result = "falseidcard";
                    return Content(result); ;
                }
            }
            for (int i = 0; i < check.Count; i++)
            {
                if (email == check[i].StuEmail)
                {
                    result = "falseemail";
                    return Content(result);
                }
            }

            data.Students.Add(newuser);
            data.SaveChanges();

            return Content(result);
        }
    }
}