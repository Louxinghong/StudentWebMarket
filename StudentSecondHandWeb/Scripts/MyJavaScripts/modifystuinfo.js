
$(function () {

    $("#dialog").dialog({
        height: "330",
        width: "330",
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000,
        }
    });

    
    $(".modstu").click(function () {
        window.location.href = "/Student/StudentInfoModify";
    });

    $(".modconfirm").click(function () {
        var id = $("#id").text();
        var name = $("#name").val();
        var school = $("#school").val();
        var address = $("#address").val();


        if (name == "" || school == "" || address == "")
            alert("信息不能为空！")
        else if (name != "" && school != "" && address != "") {
            $.ajax({
                type: "POST",
                data: {
                    id: id,
                    name: name,
                    school: school,
                    address: address
                },
                url: "/Student/ConfirmModify",
                success: function (result) {
                    if (result == "success") {
                        alert("修改成功!请重新登录!");
                        window.location.href = "/Login/Login";
            
                    }
                }
            });
        }
    });
    $(".modback").click(function () {
        window.history.back(-1);
    });

    $("#stuconfirm").click(function () {
        var status = "true";
        var id = $("#regid").val();
        var oldpassword = $("#oldpassword").val();
        var newpassword = $("#newpassword").val();
        var passwordagain = $("#passwordagain").val();
        var falsepass = /[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im;
        if (oldpassword == "" || newpassword == "" || passwordagain == ""){
            alert("密码不能为空！");
            status = "false";
        }
        else if (falsepass.test(newpassword)) {
            alert("密码不能含有非法字符！");
            status = "false";
        }
        else if (status == "true") {
            $.ajax({
                type: "POST",
                data: {
                    id: id,
                    oldpassword: oldpassword,
                    newpassword: newpassword,
                    passwordagain: passwordagain
                },
                url: "/Student/ModifyPassword",
                success: function (result) {
                    if (result == "success") {
                        alert("修改成功!请重新登录!");
                        window.location.href = "/Login/Login";

                    }
                    else if (result == "twonotsame") {
                        $("#context").text("请保持两次输入的新密码一致！");
                        $("#disappare").show(500).delay(1500).hide(300);
                        
                    }
                    else if (result == "oldpasnottrue") {
                        $("#context").text("请输入正确的原始密码！");
                        $("#disappare").show(500).delay(1500).hide(300);
                        
                    }
                }
            });
        }

    });

});

function modifypassword(id, name) {
    $("#regid").val(id);
    $("#name").val(name);
    $("#dialog").dialog("open");
}