var sex="boy";
var echo=function(value){
　　console.log(value)
}

//export {sex,echo} 
exports.sex = sex;//留出接口
exports.echo = echo;