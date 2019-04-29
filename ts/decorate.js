var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
//------------------------------------------
function ClassDecorator() {
    return function (target) {
        console.log("I am class decorator");
    };
}
function MethodDecorator() {
    return function (target, methodName, descriptor) {
        console.log("I am method decorator", methodName);
    };
}
function Param1Decorator() {
    return function (target, methodName, paramIndex) {
        console.log("I am parameter1 decorator", paramIndex);
    };
}
function Param2Decorator() {
    return function (target, methodName, paramIndex) {
        console.log("I am parameter2 decorator");
    };
}
function PropertyDecorator() {
    return function (target, propertyName) {
        console.log("I am property decorator");
    };
}
var Hello = /** @class */ (function () {
    function Hello() {
    }
    Hello.prototype.greet = function (p1, p2) { };
    __decorate([
        PropertyDecorator()
    ], Hello.prototype, "greeting");
    __decorate([
        MethodDecorator(),
        __param(0, Param1Decorator()), __param(1, Param2Decorator())
    ], Hello.prototype, "greet");
    Hello = __decorate([
        ClassDecorator()
    ], Hello);
    return Hello;
}());
var test = /** @class */ (function (_super) {
    __extends(test, _super);
    function test() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    test.prototype.greet = function (p1, p2) {
        _super.prototype.greet.call(this, p1, p2);
        console.log('test greet');
    };
    return test;
}(Hello));
var t = new test();
t.greet('1', '2');
