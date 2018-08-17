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
var Person = /** @class */ (function () {
    function Person(name) {
        this.name = name;
        this.name = name;
        console.log("haha");
    }
    // 类的构造函数，只在实例化时被调用，而且被调用一次； 
    Person.prototype.eat = function () { console.log("im eating"); };
    return Person;
}());
//子类拥有父类的属性和方法,还可以自定义自己属性和方法 
var Emplyee = /** @class */ (function (_super) {
    __extends(Emplyee, _super);
    function Emplyee(name, code) {
        var _this = _super.call(this, name) || this;
        console.log("xixi");
        _this.code = code;
        return _this;
    }
    Emplyee.prototype.work = function () {
        _super.prototype.eat.call(this); //调用父类方法 
        this.doWork();
    };
    Emplyee.prototype.doWork = function () {
        console.log("im working");
    };
    return Emplyee;
}(Person));
//类的实例化 
var e1 = new Emplyee("name", "1");
e1.work();
var p1 = new Person("batman");
p1.eat();
var p2 = new Person("superman");
p2.eat();
//实现接口的类必须要实现接口中的方法； 
var Sheep = /** @class */ (function () {
    function Sheep() {
    }
    Sheep.prototype.eat = function () {
        console.log("im eat grass");
    };
    return Sheep;
}());
var Tiger = /** @class */ (function () {
    function Tiger() {
    }
    Tiger.prototype.eat = function () {
        console.log("im eat meat");
    };
    return Tiger;
}());
//----------------
var Startup = /** @class */ (function () {
    function Startup() {
    }
    Startup.main = function () {
        console.log('Hello World');
        return 0;
    };
    return Startup;
}());
Startup.main();
