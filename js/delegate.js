Function.prototype.bind=function(context){
    var self=this;
    return function(){
        return self.apply(context,arguments);
    }
}
function BaseObject(element){
    this.element= element;
    this.currentTarget=null;
  }
function a(name){
    var element = document.querySelector(name);
    return new BaseObject(element);
}
BaseObject.prototype.delegate=function(eventType, selector, fn){
    function run(ev){
        var target = ev.target || ev.srcElement;
        //console.log(target);
        var currentTarget=ev ? ev.currentTarget : ev;
        this.currentTarget=currentTarget;
        while(target !== this.element ){
            if(target.tagName.toLowerCase() == selector.toLowerCase()){
                fn.call(currentTarget,target);
                console.log('---target--- ');
                console.log(target);
                console.log('click~');
                break;
            }
            target = target.parentNode;
        }
    }
    var evt = window.event ? window.event : this;
    if(document.addEventListener){
        this.element.addEventListener(eventType,run.bind(this));
    }else{
        this.element.attach('on'+eventType,run.bind(this));
    }
}
a('#maincontent').delegate('click','pre',function(e){console.log(e);console.log('ok')})