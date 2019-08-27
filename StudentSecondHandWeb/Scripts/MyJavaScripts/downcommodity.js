$(document).ready(function () {

    var resid = $("#resid").text();

    var student = "", studentphone = "";
    var cookies = document.cookie;

    var list = cookies.split(";");
    for (var i = 0; i < list.length; i++) {
        var arr = list[i].split("=");

        if (arr[0] == " cookieusername")
            student = arr[1];
        if (arr[0] == " cookieuserphone")
            studentphone = arr[1];
    }
    $("#btndown").click(function () {
        $.ajax({
            type: "POST",
            data: {
                resid: resid,
                owner: student,
                ownerphone: studentphone
            },
            url: "/Thing/DownCommodity",
            success: function (result) {
                if (result == "true") {
                    alert("已下架该商品~");
                    window.location.href = "/Thing/OwnerCommodity";
                }

            }
        });
    });


});