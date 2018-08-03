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
//--------------------------
var ww=function(){};
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
    return new ww();
}
var w=new ww();
w.a();
ww.instance().b('ok');
var rr=ww.instance().c;
rr.add(233);
console.log(rr.arr)
