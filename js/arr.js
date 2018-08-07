function t() {
    this.arr = [];
    this.indexOf = function (val) {
        for (var i = 0; i < this.arr.length; i++) {
            if (this.arr[i] == val) return i;
        }
        return -1;
    };
    this.add = function (val) {
        this.arr.push(val);
    };
    this.remove = function (val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.arr.splice(index, 1);
        }
    };
    this.removeAll = function (val) {
        var index = this.indexOf(val);
        while(this.indexOf(val) != -1) {
            this.arr.splice(index, 1);
        }
    }
}

var r=new t();
r.add(1);
r.add('as');
console.log(r.arr);
r.remove('as');
console.log(r.arr);
r.add(1);
r.add('as');
console.log(r.arr);
r.removeAll(1);
console.log(r.arr);
//--------------------------单例模式
var ww=function(){
    this.element=null;
};
ww.extend=function(funcjson){
  for(var key in funcjson){
    ww.prototype[key]=funcjson[key];
  }
}
ww.extend({ 
    a: function () { console.log('ok') },
    b:function(val){console.log('ok b '+val)},
    c:new t()
})
ww.instance=function(){
    if(this.element==null){
        this.element=new ww();
    }
    return this.element;
    
}
var w=new ww();
w.a();
ww.instance().b('ok');
var rr=ww.instance().c;
rr.add(233);
console.log(rr.arr)
//----------------------
var getSingle = function(fn){ //参数为创建对象的方法
    var result;
    return function(){ //判断是Null或赋值
        return result || (result = fn.apply(this,arguments));
    };
};
//--------------------
var mult = function () {
    var a = 1;
    for (var i = 0, l = arguments.length; i < l; i++) {
        a = a * arguments[i];
    }
    return a;
}
//不使用缓存mult(2,3);
var proxyMult = (function () {
    var cache = {};
    return function () {
        var args = Array.prototype.join.call(arguments, ','); //把参数放在一个字符串里
        if (args in cache) {
            return cache[args];
        }
        return cache[args] = mult.apply(this,arguments);
    };
})();
//使用缓存proxyMult(2,3)
//------------------------------状态模式
var Light = function () {
    this.state = "off";
    this.button = null;
};
var LightEvent = {
    on:function(){
        console.log("请关灯");
        this.state = "off";
    },
    off:function(){
        console.log("请开灯");
        this.state = "on";
    }
};
Light.prototype.press=function(){
    LightEvent[this.state].apply(this,arguments);
    console.log(this.state)
}
var l=new Light("on",null);
l.press();
l.press();

