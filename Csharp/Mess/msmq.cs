
using System;
using System.Messaging;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace Csharp.Mess
{
    public class msmq
    {
        public static void test(){
            msmq msmq = new msmq();
            Task[] tasks = new Task[11];
            for (int i = 0; i < 10; i++)
            {
                Task task = new Task(msmq.MSMQServer, "a" + i);
                tasks[i] = task;
                task.Start();

            }
            Task task1 = new Task(msmq.MSMQClient, "b");
            tasks[10]=task1;
            task1.Start();
            Task.WaitAll(tasks);
        }
        private int count=0;
        public void MSMQServer(object name){
// 创建一个公共队列,公共队列只能创建在域环境里
            // if (!MessageQueue.Exists(@".\LearningHardMSMQ")) // 判断此路径下是否已经有该队列
            // {
            //    using (MessageQueue mq = MessageQueue.Create(@".\LearningHardMSMQ"))
            //    {
            //        mq.Label = "LearningHardQueue"; // 设置队列标签
            //        Console.WriteLine("已经创建了一个公共队列");
            //        Console.WriteLine("路径为:{0}", mq.Path);
            //        Console.WriteLine("队列名字为:{0}", mq.QueueName);
            //        mq.Send("MSMQ Message", "Leaning Hard"); // 发送消息
            //    }
            // }

            //if (MessageQueue.Exists(@".\Private$\LearningHardMSMQ"))
            //{
                  // 删除消息队列
            //    MessageQueue.Delete(@".\Private$\LearningHardMSMQ");
            //}
            // 创建一个私有消息队列
            if (!MessageQueue.Exists(@".\Private$\LearningHardMSMQ"))
            {
                using (MessageQueue mq = MessageQueue.Create(@".\Private$\LearningHardMSMQ"))
                {
                    mq.Label = "LearningHardPrivateQueue"; 
                    Console.WriteLine("已经创建了一个私有队列");
                    Console.WriteLine("路径为:{0}", mq.Path);
                    Console.WriteLine("私有队列名字为:{0}", mq.QueueName);
                    string tag="Leaning Hard1";
                    mq.Send("MSMQ Private Message "+tag, tag); // 发送消息
                }
            }

            // 遍历所有的公共消息队列
            //foreach (MessageQueue mq in MessageQueue.GetPublicQueues())
            //{
            //    mq.Send("Sending MSMQ public message" + DateTime.Now.ToLongDateString(), "Learning Hard");
            //    Console.WriteLine("Public Message is sent to {0}", mq.Path);
            //}
           
            if (MessageQueue.Exists(@".\Private$\LearningHardMSMQ")) 
            {
                // 获得私有消息队列
                MessageQueue mq = new MessageQueue(@".\Private$\LearningHardMSMQ");
                string tag="Leaning Hard";
                mq.Send(tag+count +" Sending MSMQ private message" + DateTime.Now.ToString(), tag);
                count++;
                Console.WriteLine("Private Message is sent to {0}", mq.Path);
            }
        }
        public void MSMQClient(object name){
            if (MessageQueue.Exists(@".\Private$\LearningHardMSMQ"))
            {
                // 创建消息队列对象
                using (MessageQueue mq = new MessageQueue(@".\Private$\LearningHardMSMQ"))
                {
                    // 设置消息队列的格式化器
                    mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                    int i=0;
                    foreach (Message msg in mq.GetAllMessages())
                    {
                        Console.WriteLine(name +"   Received Private Message is: {0}", msg.Body);
                        Console.WriteLine("number "+i+" id:"+ msg.Id);
                       // mq.ReceiveById(msg.Id);//获取删除
                        i++;
                    }

                    Message firstmsg = mq.Receive(); // 获得消息队列中第一条消息，没有就等待
                    Console.WriteLine("Received The first Private Message is: {0}", firstmsg.Body);
                  
                }
            }
        }
        
    }
}