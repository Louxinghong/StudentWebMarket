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

    $("#btncancel").click(function () {
        console.log("lgg");
        $.ajax({
            type: "POST",
            data: {
                resid: resid,
                collector: student,
                collectorphone: studentphone
            },
            url: "/Collection/CancelCollection",
            success: function (result) {
                if (result == "true") {
                    alert("已取消收藏~");
                    window.location.href = "/Collection/CollectionInformation";
                }
                    
            }
        });
    });

    
});