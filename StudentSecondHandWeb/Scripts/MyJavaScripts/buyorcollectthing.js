$(document).ready(function () {

    var resid = $("#resid").text();
    var resname = $("#resname").text();
    var rescategorymain = $("#rescategorymain").text();
    var rescategorysecond = $("#rescategorysecond").text();
    var resprice = $("#resprice").text();
    var seller = $("#regname").text();
    var sellerphone = $("#regphone").text();
    var sellerschool = $("#regschool").text();

    var student = "", studentphone = "", studentschool = "";
    var cookies = document.cookie;

    var list = cookies.split(";");
    for (var i = 0; i < list.length; i++) {
        var arr = list[i].split("=");

        if (arr[0] == " cookieusername")
            student = arr[1];
        if (arr[0] == " cookieuserphone")
            studentphone = arr[1];
        if (arr[0] == " cookieuserschool")
            studentschool = arr[1];
    }

    $("#btnbuything").click(function () {
        if (student == "") {
            alert("请您先登录哟！");
        }
        else if (studentschool != sellerschool)
            alert("您非该校人员，无法购买，但是可以收藏哟~")
        else if (student != "" && studentschool == sellerschool) {
            $.ajax({
                type: "POST",
                data: {
                    resid: resid,
                    resname: resname,
                    rescategorymain: rescategorymain,
                    rescategorysecond: rescategorysecond,
                    resprice: resprice,
                    seller: seller,
                    sellerphone: sellerphone,
                    sellerschool: sellerschool,
                    buyer: student,
                    buyerphone: studentphone,
                    buyerschool: studentschool
                },
                url: "/Order/MakeOrder",
                success: function (result) {
                    if (result == "true")
                        window.location.href = "/Order/OrderSuccess";
                }
            });
        }      
    });

    $("#btncollectthing").click(function () {
        if (student == "") {
            alert("请您先登录哟！")
        }
        else {
            $.ajax({
                type: "POST",
                data: {
                    resid: resid,
                    resname: resname,
                    rescategorymain: rescategorymain,
                    rescategorysecond: rescategorysecond,
                    resprice: resprice,
                    seller: seller,
                    sellerphone: sellerphone,
                    sellerschool: sellerschool,
                    collector: student,
                    collectorphone: studentphone,
                    collectorschool: studentschool
                },
                url: "/Collection/MakeCollection",
                success: function (result) {
                    if (result == "true")
                        window.location.href = "/Collection/CollectSuccess";
                    else if (result == "false")
                        alert("你已收藏了该物品");
                }
            });
        }
    });

    $("#btnsendmessage").click(function () {

        var messagecontext = $("#message").val();

        if (student == "") {
            alert("请您先登录哟！")
        }
        else {
            $.ajax({
                type: "POST",
                data: {
                    resid: resid,
                    resname: resname,
                    seller: seller,
                    sellerphone: sellerphone,
                    messageperson: student,
                    messagepersonphone: studentphone,
                    messagecontext: messagecontext
                },
                url: "/Thing/SendMessage",
                success: function (result) {
                    if (result == "true") {
                        alert("留言成功！");
                        window.location.reload();
                    }
                        
                        
                }
            });
        }
    });


});