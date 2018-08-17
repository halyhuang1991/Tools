"use strict";
exports.__esModule = true;
var Triangle = /** @class */ (function () {
    function Triangle(name) {
        this.name = name;
        this.name = name;
    }
    // 类的构造函数，只在实例化时被调用，而且被调用一次； 
    Triangle.prototype.log = function () {
        console.log(this.name);
    };
    return Triangle;
}());
exports.Triangle = Triangle;
var Square = /** @class */ (function () {
    function Square() {
    }
    return Square;
}());
exports.Square = Square;
