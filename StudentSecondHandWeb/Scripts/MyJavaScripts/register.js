$(document).ready(function () {
    $("#btnreg").click(function () {
        var status = "true";
        var isnum = /^[0-9]+.?[0-9]*$/;
        var falsename = /[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im;
        var isemail = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        var falsepass = /[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im;

        var username = $("#username").val();
        var sex = $("#sex").val();
        var phone = $("#phone").val();
        var idcard = $("#idcard").val();
        var email = $("#email").val();
        var address = $("#address").val();
        var school = $("#school").val();
        var passid = $("#passid").val();
        var password = $("#password").val();
        var password_twice = $("#password_twice").val();

        if (username == "" || phone == "" || idcard == "" || email == "" || address == "" || school == "" || passid == "" || password == "" || password_twice == "") {
            alert("请填写所有信息！");
            status = "false";
        }
        else if (falsename.test(username)) {
            alert("请输入合法名称！");
            status = "false";
        }
        else if (!isnum.test(phone)) {
            alert("号码必须为全数字！");
            status = "false";
        }
        else if (falsename.test(idcard)) {
            alert("身份证号码不能含有非法字符！");
            status = "false";
        }
        else if (!isemail.test(email)) {
            alert("邮箱格式不正确！");
            status = "false";
        }
        else if (falsepass.test(passid)) {
            alert("帐号不能含有非法字符！");
            status = "false";
        }
        else if (falsepass.test(password)) {
            alert("密码不能含有非法字符！");
            status = "false";
        }
        else if (status == "true") {
            $.ajax({
                type: "POST",
                data: {
                    username: username,
                    sex: sex,
                    phone: phone,
                    idcard: idcard,
                    email: email,
                    address: address,
                    school: school,
                    passid: passid,
                    password: password
                },
                url: "/Register/CheckUser",
                success: function (result) {
                    if (result == "success") {
                        alert("注册成功！前往登录！");
                        window.location.href = "/Login/Login";
                    }
                    else if (result == "falsename") {
                        alert("该名称已被注册！请重新输入！");
                    }
                    else if (result == "falsepassid") {
                        alert("该帐号已被注册！请重新输入！");
                    }
                    else if (result == "falsephone") {
                        alert("该号码已被注册！请重新输入！");
                    }
                    else if (result == "falseidcard") {
                        alert("该身份证已被注册！请重新输入");
                    }
                    else if (result == "falseemail") {
                        alert("该邮箱已被注册！请重新输入！");
                    }
                }
            });
        }

        if (password != password_twice) {
            alert("请输入一致的密码!");
        }
    });

});

