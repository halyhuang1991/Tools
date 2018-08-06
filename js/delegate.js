if(!Function.prototype.bind){
    Function.prototype.bind=function(context){
        var self=this;
        return function(){
            return self.apply(context,arguments);
        }
    }
}

if (!Element.prototype.matches) {
    Element.prototype.matches =
        Element.prototype.matchesSelector ||
        Element.prototype.mozMatchesSelector ||
        Element.prototype.msMatchesSelector ||
        Element.prototype.oMatchesSelector ||
        Element.prototype.webkitMatchesSelector ||
        function(s) {
            var matches = (this.document || this.ownerDocument).querySelectorAll(s),
                i = matches.length;
            while (--i >= 0 && matches.item(i) !== this) {}
            return i > -1;            
        };
}
function BaseObject(element){
    this.element= element;
    this.currentTarget=null;
    this.event={"change":"onporpertychange","click":"onclick"}
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
            if(target.matches(selector)){
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
        this.element.attach(this.event[eventType],run.bind(this));
    }
}
a('#maincontent').delegate('click', 'pre', function (e) { 
    console.log(e); console.log('ok') 
})