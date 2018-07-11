import java.lang.System;
import java.io.*;
import java.net.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
public class GetWebData {
    public static void main(String []args) {
        System.out.println(System.getProperty("user.dir"));
       
        URL url = null;
        URLConnection urlconn = null;
        BufferedReader br = null;
        PrintWriter pw = null;
        String regex = "https://[\\w+\\.?/?]+\\.[A-Za-z]+";//url匹配规则
        Pattern p = Pattern.compile(regex);
        try {
            url = new URL("https://www.rndsystems.com/cn");//爬取的网址、这里爬取的是一个生物网站
            urlconn = url.openConnection();
            String Pathurl=System.getProperty("java.class.path")+"/txt/SiteURL.txt";
            File f=new File(Pathurl);
            File fp=f.getParentFile();
            if(!fp.exists()){
                fp.mkdirs();
            }
            if(!f.exists()){
                f.createNewFile();
            }
            pw = new PrintWriter(new FileWriter(Pathurl), true);//将爬取到的链接放到D盘的SiteURL文件中
            br = new BufferedReader(new InputStreamReader(
                    urlconn.getInputStream()));
            String buf = null;
            while ((buf = br.readLine()) != null) {
                Matcher buf_m = p.matcher(buf);
                while (buf_m.find()) {
                    pw.println(buf_m.group());
                }
            }
            System.out.println("爬取成功^_^");
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                br.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
            pw.close();
        }
    }
}