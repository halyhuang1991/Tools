import java.util.*;

public class list {
    public static void Getlist(){
        ArrayList arr=new ArrayList();
        arr.add("as");
        arr.add("ssa");
        for(int i=0;i<arr.size();i++){
            System.out.println(arr.get(i));
            if(arr.contains("as")){
                System.out.println(arr.get(i));
            }
        }
    }
}