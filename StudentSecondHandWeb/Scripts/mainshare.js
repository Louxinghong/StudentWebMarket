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