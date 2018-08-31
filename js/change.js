var ua=navigator.userAgent.toLowerCase();
var s=null;
var browser={  
  msie:(s=ua.match(/msie\s*([\d\.]+)/))?s[1]:false,  
  firefox:(s=ua.match(/firefox\/([\d\.]+)/))?s[1]:false,  
  chrome:(s=ua.match(/chrome\/([\d\.]+)/))?s[1]:false,  
  opera:(s=ua.match(/opera.([\d\.]+)/))?s[1]:false,  
  safari:(s=ua.match(/varsion\/([\d\.]+).*safari/))?s[1]:false  
}; 
function change(element,callback){
    if(element.tagName.toLowerCase()!="input")return;
    if(document.all){
        element.onpropertychange=function(){
            callback(element.value);
        }
     }
     else{
        element.onchange=function(){
            callback(element.value);
       }
     }
}
var w = document.querySelector('#kw');
change(w, function (val) {
    console.log(val)
});
//----------------------------------
function BaseObject(element){
    this.elements= element;
    this.GetIeEvent = function (ev) {
        var ret = "";
        switch (ev) {
            case "click":
                ret = "onlick";
                break;
            case "change":
                 ret="onporpertychange";
                 break;
            default:
                ret = "on" + ev;
                break;
        }
        return ret;
    }
  }
function a(name){
    var element = document.querySelectorAll(name);
    return new BaseObject(element);
}
BaseObject.prototype.change=function(callback){
   
    for(var i =0;i<this.elements.length;i++){
        var element=this.elements[i];
        if(document.all){
            element.onpropertychange=function(){
                callback(element.value);
            }
         }
         else{
            element.onchange=function(){
                callback(element.value);
           }
         }
    }
  
    
}
BaseObject.prototype.bind=function(event,callback){
   
    for(var i =0;i<this.elements.length;i++){
        var element=this.elements[i];
        if(document.all){
            try {
                element.attachEvent(this.GetIeEvent(event),callback(element.value))
            } catch (error) {
                element[this.GetIeEvent(event)]=callback(element.value);
            }
            
         }
         else{
            element.addEventListener(event,callback(element.value),false);//不会覆盖
         }
    }
  
    
}
BaseObject.prototype.unbind=function(event,callback,useCapture){
   
    for(var i =0;i<this.elements.length;i++){
        var element=this.elements[i];
        if(document.all){
            try {
                element.detachEvent(this.GetIeEvent(event),callback(element.value))
            } catch (error) {
                element[this.GetIeEvent(event)]=function(){};
            }
            
         }
         else{
            element.removeEventListener(event,callback(element.value),!!useCapture);//不会覆盖
         }
    }  
}
BaseObject.prototype.trigger=function(event,args){
    var ev;
    for(var i =0;i<this.elements.length;i++){
        var element=this.elements[i];
        if (element.dispatchEvent) { 
            ev = document.createEvent("HTMLEvents");
            ev.initEvent(event, true, true);
        } else {
           ev = document.createEventObject();
        }
        for (var i in args) if (args.hasOwnProperty(i)) {
            ev[i] = args[i]
        }
        if (element.dispatchEvent) {
            element.dispatchEvent(event);
        } else {
            element.fireEvent(this.GetIeEvent(event) , ev)
        }
    }
}

a('#kw').change(function(val){console.log(val)})