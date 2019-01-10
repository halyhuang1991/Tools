
import java.lang.*;

public class test {
    public static void main(String[] args) {
        list.Getlist();
        String a = "a";
        String b = "a";
        String c = new String("a");
        System.out.println(a == b);// true
        System.out.println(a.equals(b));// true
        System.out.println(a == c);// false
        System.out.println(a.equals(c));// true
        System.out.println(a.hashCode() + b.hashCode() + "|" + c.hashCode());
        Puppy myPuppy = new Puppy("tommy");
        myPuppy.setAge(2);
        System.out.println(myPuppy.getAge());
        Puppy myPuppy1 = new Puppy("tommy");
        myPuppy1.setAge(2);
        System.out.println(myPuppy == myPuppy1);
        System.out.println(myPuppy.equals(myPuppy1));
        System.out.println(myPuppy.hashCode() + "|" + myPuppy1.hashCode());

    }
}