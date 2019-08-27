$(document).ready(function () {
    $("#btnreg").click(function () {
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

        if (password == password_twice) {
            $.ajax({
                type: "POST",
                data: { username: username, sex: sex, phone: phone, idcard: idcard, email: email, address: address, school: school, passid: passid, password: password},
                url: "/Register/CheckUser",
                success: function (result) {
                    if (result === "true") {
                        window.location.href = "/Login/LoginView";
                    }
                    else {
                        $("#message").text("注册失败");
                    }
                }
            });
        }

        if (password != password_twice) {
            $("#message").text("请输入一致的密码！");
        }
    });

    $("#btnback").click(function () {
        window.location.href = "/Login/LoginView";
    });
});

