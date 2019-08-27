
$(function () {

    $("#adminout").click(function () {
        $.ajax({
            type: "POST",
            url: "/Admin/DelCookie",
            success: function (result) {
            }
        });
    });

    $("#dialog").dialog({
        height: "500",
        width: "780",
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

    $("#webbriefconfirm").click(function () {
        var modifytheme = $("#modifytheme").val();
        var modifypicture = $("#modifypicture").val();
        var modifypurpose = $("#modifypurpose").val();
        var modifycontext = $("#modifycontext").val();
        $.ajax({
            type: "POST",
            data: {
                modifytheme: modifytheme,
                modifypicture: modifypicture,
                modifypurpose: modifypurpose,
                modifycontext: modifycontext
            },
            url: "/Admin/ModifyWebBrief",
            success: function (result) {
                if (result == "true") {
                    alert("修改成功~");
                    window.location.reload();
                }
            }
        });
    });
    $(".noticeadd").click(function () {
        $("#dialog").dialog("open");
        $(".id").hide();
        $(".time").hide();
    });
});

function showthingdetails(resname,
    rescategorymain,
    rescategorysecond,
    resprice,
    respicture,
    resintroduction,
    rescolnum,
    regname,
    regphone,
    regidcard,
    regschool,
    shelltime,
    whethersell,
    whetherdown) {
    $("#resname").html(resname);
    $("#rescategorymain").html(rescategorymain);
    $("#rescategorysecond").html(rescategorysecond);
    $("#resprice").html(resprice);
    $("#respicture").attr('src', respicture);
    $("#resintroduction").html(resintroduction);
    $("#rescolnum").html(rescolnum);
    $("#regname").html(regname);
    $("#regphone").html(regphone);
    $("#regidcard").html(regidcard);
    $("#regschool").html(regschool);
    $("#shelltime").html(shelltime);
    if (whethersell == "0")
        $("#whethersell").html("未出售");
    else
        $("#whethersell").html("已出售");
    if (whetherdown == "0")
        $("#whetherdown").html("未下架");
    else
        $("#whetherdown").html("已下架");
    $("#dialog").dialog("open");
}

function showcollectiondetails(resname,
    rescategorymain,
    rescategorysecond,
    resprice,
    respicture,
    seller,
    sellerphone,
    sellerschool,
    shelltime,
    whethersell,
    whetherdown,
    collector,
    collectorphone,
    collectorschool,
    whethercancel) {
    $("#resname").html(resname);
    $("#rescategorymain").html(rescategorymain);
    $("#rescategorysecond").html(rescategorysecond);
    $("#resprice").html(resprice);
    $("#respicture").attr('src', respicture);
    $("#seller").html(seller);
    $("#sellerphone").html(sellerphone);
    $("#sellerschool").html(sellerschool);
    $("#shelltime").html(shelltime);
    if (whethersell == "0")
        $("#whethersell").html("未出售");
    else
        $("#whethersell").html("已出售");
    if (whetherdown == "0")
        $("#whetherdown").html("未下架");
    else
        $("#whetherdown").html("已下架");
    $("#collector").html(collector);
    $("#collectorphone").html(collectorphone);
    $("#collectorschool").html(collectorschool);
    if (whethercancel == "0")
        $("#whethercancel").html("收藏中");
    else
        $("#whethercancel").html("已取消收藏");

    $("#dialog").dialog("open");
}

function showorderdetails(resname,
    rescategorymain,
    rescategorysecond,
    resprice,
    respicture,
    seller,
    sellerphone,
    sellerschool,
    buyer,
    buyerphone,
    buyerschool,
    buytime,
    whethercancelbuy) {
    $("#resname").html(resname);
    $("#rescategorymain").html(rescategorymain);
    $("#rescategorysecond").html(rescategorysecond);
    $("#resprice").html(resprice);
    $("#respicture").attr('src', respicture);
    $("#seller").html(seller);
    $("#sellerphone").html(sellerphone);
    $("#sellerschool").html(sellerschool);
    $("#buyer").html(buyer);
    $("#buyerphone").html(buyerphone);
    $("#buyerschool").html(buyerschool);
    $("#buytime").html(buytime);
    if (whethercancelbuy == "0")
        $("#whethercancelbuy").html("未取消订单");
    else
        $("#whethercancelbuy").html("已取消订单");

    $("#dialog").dialog("open");
}


function modifywebbrief(modifytheme,
    modifypicture,
    modifypurpose,
    modifycontext) {
    $("#modifytheme").val(modifytheme);
    $("#modifypicture").val(modifypicture);
    $("#modifypurpose").val(modifypurpose);
    $("#modifycontext").val(modifycontext);
    $("#dialog").dialog("open");
}

function modifynoticedetails(noticeid,
    noticetitle,
    noticepicture,
    noticecontext,
    noticetime) {
    $("#noticeid").val(noticeid);
    $("#noticetitle").val(noticetitle);
    $("#noticepicture").attr('src', noticepicture);
    $("#picturepath").val(noticepicture);
    $("#noticecontext").val(noticecontext);
    $("#noticetime").text(noticetime);
    $("#dialog").dialog("open");
    
}

function deletenotice(noticeid) {
    $.ajax({
        type: "POST",
        data: {
            id: noticeid
        },
        url: "/Admin/NoticeDel",
        success: function (result) {
            if (result == "true") {
                alert("删除成功~");
                window.location.reload();
            }
        }
    });
}


var picturename;

//获取文件url
function createObjectURL(blob) {
    if (window.URL) {
        return window.URL.createObjectURL(blob);
    } else if (window.webkitURL) {
        return window.webkitURL.createObjectURL(blob);
    } else {
        return null;
    }
}

var box = document.querySelector("#noticepicture");    //显示图片box
var file = document.querySelector("#file"); //file对象
var domFragment = document.createDocumentFragment();   //文档流优化多次改动dom

//触发选中文件事件
file.onchange = function (e) {
    box.innerHTML = "";  //清空上一次显示图片效果
    e = e || event;
    var file = this.files;  //获取选中的文件对象

    for (var i = 0, len = 1; i < len; i++) {
        var imgTag = document.createElement("img");
        imgTag.setAttribute("width", "100%");
        imgTag.setAttribute("height", "100%");
        var fileName = file[i].name;    //获取当前文件的文件名 
        picturename = file[i].name;
        var url = createObjectURL(file[i]); //获取当前文件对象的URL

        //忽略大小写
        var jpg = (fileName.indexOf(".jpg") > -1) || (fileName.toLowerCase().indexOf(".jpg") > -1);
        var png = (fileName.indexOf(".png") > -1) || (fileName.toLowerCase().indexOf(".png") > -1);
        var jpeg = (fileName.indexOf(".jpeg") > -1) || (fileName.toLowerCase().indexOf(".jpeg") > -1);

        //判断文件是否是图片类型
        if (jpg || png || jpeg) {
            imgTag.src = url;
            $("#submit").removeAttr("disabled");
            $("#submit").css({ "cursor": "default" });
            $("#picturename").val(picturename);
            domFragment.appendChild(imgTag);
        } else {
            $("#submit").attr({ "disabled": "disabled" });
            $("#submit").css({ "cursor": "not-allowed" });
            alert("请选择图片类型文件！");
        }
    }

    //最佳显示
    $("#noticepicture").attr('src', url);
    $("#picturepath").val('../images/' + picturename);

}