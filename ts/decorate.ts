//------------------------------------------
function ClassDecorator() {
    return function (target) {
        console.log("I am class decorator");
    }
}
function MethodDecorator() {
    return function (target, methodName: string, descriptor: PropertyDescriptor) {
        console.log("I am method decorator",methodName);
    }
}
function Param1Decorator() {
    return function (target, methodName: string, paramIndex: number) {
        console.log("I am parameter1 decorator",paramIndex);
    }
}
function Param2Decorator() {
    return function (target, methodName: string, paramIndex: number) {
        console.log("I am parameter2 decorator");
    }
}
function PropertyDecorator() {
    return function (target, propertyName: string) {
        console.log("I am property decorator");
    }
}

@ClassDecorator()
class Hello {
    @PropertyDecorator()
    greeting: string;


    @MethodDecorator()
    greet( @Param1Decorator() p1: string, @Param2Decorator() p2: string) { }
}
class test extends Hello{
    greeting: string;
    greet(p1:string,p2:string){
        super.greet(p1,p2);
        console.log('test greet');
    }
}
var t=new test();
t.greet('1','2');