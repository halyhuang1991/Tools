public class Puppy {
    int puppyAge;

    public Puppy(String name) {
        // 这个构造器仅有一个参数：name
        System.out.println("Passed Name is :" + name);
    }

    public void setAge(int age) {
        puppyAge = age;
    }

    public int getAge() {
        System.out.println("Puppy's age is :" + puppyAge);
        return puppyAge;
    }
}