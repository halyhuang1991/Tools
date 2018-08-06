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

a('#kw').change(function(val){console.log(val)})