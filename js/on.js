var event=function(element){
    this.element=element||"";
    this.json={};
    this.arg={};
    this.on=function(name,fn){
        
        if(typeof fn =="function"){
            this.json[name]=fn;
              
        }
    };
    this.trigger=function(name){
        console.log('element is'+this.element)
        if(arguments[0]==undefined){
            for(var key in this.json){
                this.json[key]();
            }
        }else{
            
            if(this.json[name]!=undefined){
                var arr=[];
                for(var key in arguments){
                    arr.push(arguments[key])
                }
                if(arr==[]){
                    this.json[name].apply(this.json[name]);
                    return;
                }
                arr.splice(0,1);
                this.json[name].apply(this.json[name],arr);
               
            }
        }
        
        
    };
}
var Event=new event();
Event.on('one',function(){
    console.log('ok');
})
Event.on('two',function(data){
    console.log('ok'+data);
})
// Event.trigger('one');
// Event.trigger('two','sd');

var ext=(function(eve){
    return function(target){
        var e=new eve(target);
        return e;
    }
})(event);
e=ext();
e.on('one',function(){
    console.log('ok');
})
//e.trigger('one');
//--------------
var s=ext('#sd');
s.on('o',function(){
    console.log('ok');
})
s.trigger('o');
//------------------------------观察者模式
var events = {
    a: {
        arr: []
    },
    indexOf :function (val) {
        for (var i = 0; i < this.a.arr.length; i++) {
            if (this.a.arr[i] == val) return i;
        }
        return -1;
    },
    add: function (val) {
        this.a.arr.push(val);
    },
    remove : function (val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.a.arr.splice(index, 1);
        }
    },
    update:function(){
        console.log(this.a.arr);
    }
}
events.add({username:'halyhuang',a:'sds',subscribe:function(){}})
events.add({username:'haly',a:'sds',subscribe:function(){}})
events.update();