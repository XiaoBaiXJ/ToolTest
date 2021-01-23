using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace 依赖导致原则
{
    class Program
    {
        static IReading motherreading;
        static IReading fatherreading;
        static void Main(string[] args)
        {

            Datetimedemo datetimedemo = new Datetimedemo() { Nullable = null };

            var date = DateTime.TryParse(datetimedemo.Nullable?.ToString(),out var sb);
            if (sb == default)
            {

            }
            TaskDemo();

            Sleep();

            Console.WriteLine("Hello World!");
            motherreading = new Mother();
            fatherreading = new Father();
            我是中间层 调用 = new 我是中间层();
            motherreading.Reading();
            调用.Readed(motherreading, "阿里巴巴与四十大盗");
            fatherreading.Reading();
            调用.Readed(fatherreading, "围城");
            GetData(Qrcode);
            SendMailUseZj();
           
            while (true)
            {
                Task.Run(() =>
                {

                });

                Task.Run(() =>
                {

                });
            }
        }

        public static void Sleep()
        {
            Thread.Sleep(10000);
            var sb = new Mother();
        }


        private static void TaskDemo()
        {
            List<Task> tasklist = new List<Task>();
            CancellationTokenSource source = new CancellationTokenSource();
            var factory = new TaskFactory();
            try
            {
                for (int i = 0; i < 40; i++)
                {
                    var name = string.Format("我是{0}号", i.ToString());
                    Action<object> action = (t) =>
                    {
                        Thread.Sleep(200);
                        if (t.ToString().Equals("我是0号"))
                        {
                            throw new Exception(string.Format("执行失败{0}", t));
                        }
                        if (source.IsCancellationRequested)
                        {
                            Console.WriteLine("我取消了{0}", t);
                            return;
                        }
                        Console.WriteLine("我执行成功了{0}", t);
                    };
                    tasklist.Add(factory.StartNew(action, name, source.Token));
                }
                Task.WaitAll(tasklist.ToArray());
            }
            catch (AggregateException exception)
            {
                source.Cancel();
                Console.WriteLine("我阻止了一次");
                Console.Read();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string Qrcode { get; set; } = "FCN45G3EHSTP2019110700026016";

        public static void GetData(string code)
        {
            string 公司 = code.Substring(0, 3);
            string 客户料号 = code.Substring(3, 11);
            string 制造日期 = code.Substring(14, 8)+ code.Substring(25, 3);
            string 交货总数 = code.Substring(22,3);
        }

        public static void SendMailUseZj()
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add("571682689@qq.com");
            //msg.To.Add(b@b.com);
            /*   
             msg.To.Add("b@b.com");   
            * msg.To.Add("b@b.com");   
            * msg.To.Add("b@b.com");可以发送给多人   
            */
            //msg.CC.Add(c@c.com);
            /*   
            * msg.CC.Add("c@c.com");   
            * msg.CC.Add("c@c.com");可以抄送给多人   
            */
            msg.From = new MailAddress("571682689@qq.com", "dulei", System.Text.Encoding.UTF8);
            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.Subject = "这是测试邮件";//邮件标题    
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码    
            msg.Body = "邮件内容";//邮件内容    
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码    
            msg.IsBodyHtml = false;//是否是HTML邮件    
            msg.Priority = MailPriority.High;//邮件优先级    
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("571682689@qq.com", "xujiang6953208");
            //上述写你的GMail邮箱和密码    
            client.Port = 587;//Gmail使用的端口    
            client.Host = "www.qq.com";
            client.EnableSsl = true;//经过ssl加密    
            try
            {
                client.Send(msg);
                //简单一点儿可以client.Send(msg);    
                //MessageBox.Show("发送成功");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //MessageBox.Show(ex.Message, "发送邮件出错");
            }
        }


    }


    public class Datetimedemo
    {
        public DateTime? Nullable { get; set; }
    }



    public class 释放 : IDisposable
    {
        public static 释放 释 => new 释放();
        public bool IsShow { get; set; }
        public void Dispose()
        {

        }
    }

    public interface IReading
    {
        void Reading();
    }

    public class Mother : IReading
    {
        public void Reading()
        {
            Console.WriteLine("妈妈正在看书");
        }
    }

    public class Father : IReading
    {
        public void Reading()
        {
            Console.WriteLine("爸爸正在看书");
        }
    }

    public class 我是中间层
    {
        public void Readed(IReading reading,string book)
        {
            Console.WriteLine(reading.GetType()+book);
        }
    }

    public enum Geta
    {
        a=1,b=2,c=3
    }
}
