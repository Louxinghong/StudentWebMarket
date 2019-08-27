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

var box = document.querySelector(".imgbox");    //显示图片box
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
    box.appendChild(domFragment);

}

var arr_rescategorymain = ["请选择物件主级", "电器", "数码", "日用", "体育", "衣物"];
var arr_rescategorysecond = [
    ["请选择物件次级"],
    ["电磁炉", "电热水壶", "小电锅", "小冰箱", "小洗衣机", "其他"],
    ['手机', '平板', '电脑', '相机', '配件', '其他'],
    ['洗发液', '洗衣液', '沐浴露', '热水壶', '化妆品', '其他'],
    ['篮球', '足球', '球衣', '鞋子', '其他'],
    ['短袖', '长袖', '衬衫', '毛衣', '裤子', '棉袄', '其他']
];
//网页加载完成，初始化菜单
window.onload = init;//传入函数地址
function init() {
    //首先获取对象
    var rescategorymain = document.newres.rescategorymain;
    var rescategorysecond = document.newres.rescategorysecond;

    //指定省份中<option>标记的个数
    rescategorymain.length = arr_rescategorymain.length;

    //循环将数组中的数据写入<option>标记中
    for (var i = 0; i < arr_rescategorymain.length; i++) {
        rescategorymain.options[i].text = arr_rescategorymain[i];
        rescategorymain.options[i].value = arr_rescategorymain[i];
    }

    //修改省份列表的默认选择项
    var index = 0;
    rescategorymain.selectedIndex = index;

    //指定城市中<option>标记的个数
    rescategorysecond.length = arr_rescategorysecond[index].length;

    //循环将数组中的数据写入<option>标记中
    for (var j = 0; j < arr_rescategorysecond[index].length; j++) {
        rescategorysecond.options[j].text = arr_rescategorysecond[index][j];
        rescategorysecond.options[j].value = arr_rescategorysecond[index][j];
    }

}

function changeSelect(index) {
    //选择对象
    var rescategorysecond = document.newres.rescategorysecond;
    //修改省份列表的选择项
    rescategorymain.selectedIndex = index;

    //指定城市中<option>标记的个数
    rescategorysecond.length = arr_rescategorysecond[index].length;

    //循环将数组中的数据写入<option>标记中
    for (var j = 0; j < arr_rescategorysecond[index].length; j++) {
        rescategorysecond.options[j].text = arr_rescategorysecond[index][j];
        rescategorysecond.options[j].value = arr_rescategorysecond[index][j];
    }
}
