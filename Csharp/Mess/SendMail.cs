using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace Csharp.Mess
{
    public class SendMail
    {
         public static void sendingMail(){
          
            sendMail("halyhuang@*.com","","IT TEST","Mail Body");
         }
         /// <summary>
        ///发送邮件
        /// </summary>
        /// <param name="receive">接收人</param>
        /// <param name="sender">发送人</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="attachments">附件</param>
        /// <returns></returns>
        public static bool sendMail(string receive, string sender, string subject, string body, byte[] attachments = null)
        {
            string displayName ="halyhuang";
            string from = "*@outlook.com";
            var fromMailAddress = new MailboxAddress(displayName, from);
            var toMailAddress = new MailboxAddress(receive);
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(fromMailAddress);
            mailMessage.To.Add(toMailAddress);
            if (!string.IsNullOrEmpty(sender))
            {
                var replyTo = new MailboxAddress(displayName, sender);
                mailMessage.ReplyTo.Add(replyTo);
            }
            var bodyBuilder = new BodyBuilder() { HtmlBody = body };
            mailMessage.Body = bodyBuilder.ToMessageBody();
            mailMessage.Subject = subject;
            return sendMail(mailMessage);

        }
         private static bool sendMail(MimeMessage mailMessage)
        {
            try
            {
                var smtpClient = new SmtpClient();
                smtpClient.Timeout = 10 * 1000;   //设置超时时间
                string host = "smtp.office365.com";
                int port = int.Parse("587");//587
                string address = "*@outlook.com";
                string password = "*";//outlook 网站 更多设置=》邮件=》同步设置=》允许pop
                smtpClient.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);//连接到远程smtp服务器
                smtpClient.Authenticate(address, password);
                smtpClient.Send(mailMessage);//发送邮件
                smtpClient.Disconnect(true);
                return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

    }
}