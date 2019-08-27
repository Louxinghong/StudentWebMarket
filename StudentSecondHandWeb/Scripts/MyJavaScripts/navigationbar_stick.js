$(document).ready(function () {
    $(".logout").click(function () {
        $.ajax({
            type: "POST",
            url: "/Home/DelCookie",
            success: function (result) {
                if (result == "true")
                    window.location.href = "/Home/Main";
            }
        });
    });

    $("#btnsearch").click(function () {

        var searchcontext = $("#search_input").val();
        var forwhat = $("#forwhat").val();

        if (searchcontext == "")
            alert("搜索内容不能为空！");
        else {
            $.ajax({
                type: "POST",
                data: { searchcontext: searchcontext, forwhat: forwhat },
                url: "/Thing/SearchSomething",
                success: function (result) {
                    if (result == "findthing" || result == "findschool") {
                        window.location.href = "/Thing/ThingFromSearch";
                    }

                    else if (result == "notfind")
                        alert("未找到相关信息");
                }
            });
        }
        
    });

});

window.onload = function () {
    var backtoptop = document.getElementById("backtotop");
    var timer = null;
    var pageheight = document.documentElement.clientHeight;

    window.onscroll = function () {
        var scrolldistance = document.documentElement.scrollTop;
        if (scrolldistance >= pageheight) {
            backtotop.style.display = "block";
        }
        else
            backtoptop.style.display = "none";
    }

    backtotop.onclick = function () {
        timer = setInterval(function () {
            var allscroll = document.documentElement.scrollTop;
            var speedtop = allscroll / 6;
            document.documentElement.scrollTop = allscroll - speedtop;
            if (allscroll == 0)
                clearInterval(timer);
        }, 30)
    }
}