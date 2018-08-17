class Person { 
    constructor(public name: string) {
        this.name = name;
        console.log("haha");
    }
    // 类的构造函数，只在实例化时被调用，而且被调用一次； 
    eat() { console.log("im eating"); }
}
//子类拥有父类的属性和方法,还可以自定义自己属性和方法 
class Emplyee extends Person {
    code: string;
    constructor(name: string, code: string) {
        super(name);//子类构造函数必选调用父类构造函数 
        console.log("xixi"); this.code = code;
    }
    work() {
        super.eat();//调用父类方法 
        this.doWork();
    } 
    private doWork() { 
        console.log("im working"); 
    }
}
//类的实例化 
var e1 = new Emplyee("name", "1");
e1.work();
var p1 = new Person("batman");
p1.eat();
var p2 = new Person("superman");
p2.eat();
//------------------------
interface Animal { 
    eat()
}
//实现接口的类必须要实现接口中的方法； 
class Sheep implements Animal { 
    eat() {
         console.log("im eat grass"); 
    }
} 
class Tiger implements Animal { 
    eat() { 
        console.log("im eat meat"); 
    } 
}

//----------------
class Startup {
    public static main(): number {
        console.log('Hello World');
        return 0;
    }
}
Startup.main();
