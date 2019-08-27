$(document).ready(function () {
    $("#btnadminlogin").click(function () {
        var adminpassid = $("#adminpassid").val();
        var adminpassword = $("#adminpassword").val();

        console.log(adminpassid);
        if (adminpassid == "")
            alert("帐号不能为空！");
        else if (adminpassword == "")
            alert("密码不能为空！");

        else if (adminpassid != "" && adminpassword != "") {
            $.ajax({
                type: "POST",
                data: { adminpassid: adminpassid, adminpassword: adminpassword },
                url: "/Admin/CheckAdminLogin",
                success: function (result) {
                    if (result == "true") {
                        window.location.href = "/Admin/AdminMain";
                    }
                    else {
                        alert("帐号、密码有误！");
                    }
                }
            });
        }
    });
});