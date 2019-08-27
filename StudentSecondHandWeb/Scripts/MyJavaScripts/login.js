var code;
var checkCode;

createCode();

document.getElementById("myCanvas").onclick = function () {
    createCode();
}

function createCode() {
    code = "";
    checkCode = "";
    var codelength = 4;
    var selectCode = new Array(
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    );

    var canvas = document.getElementById("myCanvas");
    var width = canvas.width;
    var height = canvas.height;
    var drawContext = canvas.getContext('2d');
    drawContext.textBaseline = 'bottom';

    drawContext.fillStyle = randomColor(180, 240);
    drawContext.fillRect(0, 0, width, height);

    for (var i = 0; i < codelength; i++) {
        var codeIndex = Math.floor(Math.random() * 61);
        code = selectCode[codeIndex];
        // console.log(code);
        // console.log(codeIndex);
        checkCode += selectCode[codeIndex];
        drawContext.fillStyle = randomColor(50, 160);
        drawContext.font = randomNum(65, 110) + 'px SimHei';

        var x;
        if (i == 0) {
            x = randomNum(50, 20);
        }
        if (i == 1) {
            x = randomNum(110, 80);
        }
        if (i == 2) {
            x = randomNum(170, 140);
        }
        if (i == 3) {
            x = randomNum(230, 200);
        }
        var y = randomNum(80, 100);
        var deg = randomNum(-45, 45);

        drawContext.translate(x, y);
        drawContext.rotate(deg * Math.PI / 180);
        drawContext.fillText(code, 0, 0);

        drawContext.rotate(-deg * Math.PI / 180);
        drawContext.translate(-x, -y);
    }

    for (var i = 0; i < 12; i++) {
        drawContext.strokeStyle = randomColor(0, 255);
        drawContext.beginPath();
        drawContext.moveTo(randomNum(0, width), randomNum(0, height));
        drawContext.lineTo(randomNum(0, width), randomNum(0, height));
        drawContext.stroke();
    }

    for (var j = 0; j < 200; j++) {
        drawContext.fillStyle = randomColor(0, 255);
        drawContext.beginPath();
        drawContext.arc(randomNum(0, width), randomNum(0, height), 2, 0, 2 * Math.PI);
        drawContext.fill();
    }
}

function randomColor(min, max) {
    var r = randomNum(min, max);
    var g = randomNum(min, max);
    var b = randomNum(min, max);
    return "rgb(" + r + "," + g + "," + b + ")";
}

function randomNum(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}



$(document).ready(function () {
    $("#btnlogin").click(function () {
        var passid = $("#passid").val();
        var password = $("#password").val();
        var writecode = $("#writecode").val();

        $("#message").text("");

        if (passid == "")
            $("#message-passid").text("账号不能为空！");
        if (passid != "")
            $("#message-passid").text("");

        if (password == "")
            $("#message-password").text("密码不能为空！");
        if (password != "")
            $("#message-password").text("");

        if (writecode == "")
            $("#message-code").text("验证码不能为空！");
        if (writecode != "")
            $("#message-code").text("");

        if (passid != "" && password != "" && writecode != "") {
            $.ajax({
                type: "POST",
                data: { passid: passid, password: password },
                url: "/Login/CheckUser",
                success: function (result) {
                    if (result == "true") {
                        if (writecode.toUpperCase() == checkCode.toUpperCase()) {
                            window.location.href = "/Home/Main";
                        }
                        else {
                            alert("验证码错误！");
                            $("#writecode").val("");
                            createCode();
                        }                           
                    }
                    else {
                        alert("账号或密码错误！");
                        $("#writecode").val("");
                        createCode();
                    }
                }
            });
        }
        
    });
;
});

