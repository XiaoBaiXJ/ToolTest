using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTest
{
    class Program
    {
        /// <summary>
        /// Unicode字符串转为正常字符串
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public static string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(1, 6).Substring(2);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }
              /// <summary>
        /// 字符串转为UniCode码字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("//u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }
        static void Main(string[] args)
        {

            byte[] bytes = { 01,02};

            string sb111 =System.Text.Encoding.UTF8.GetString(bytes);
            var aa =  StringToUnicode(sb111);
            UnicodeToString(aa);
            DaysBetweenDates("2020-01-15", "2019-06-30");


            //AddValue();
            var str1 = string.Empty;

            var list2 = new List<int> { 1, 2, 3, 4 };


            var strsb = "1" + list2.ToArray().ToString();

            var list4 = new List<SysUser>() { new SysUser()};

            int int1 = 2;

            var sb2 = list2.Where(m =>
            {
                if (int1>2)
                {
                   return m == int1;
                }
                if (int1<2)
                {
                    return m == int1;
                }
                return m == int1;
            }).FirstOrDefault();

            var model1 = list2[4];
            var list3 = new List<int> { 1,2, 3, 4 };
            var reslut = list2.Except(list3).ToList();


            Console.WriteLine(str1==null);

            var list1 = new List<SysUser>()
            {
                 new SysUser{ Password="12", UserName="1" },
                 new SysUser{ Password="12", UserName="1" },
                 new SysUser{ Password="12", UserName="2" },
                 new SysUser{ Password="12", UserName="2" },
            };
            var sb1 = list1.GroupBy(m => m.UserName).ToList();

            var list = new List<string>() { "1", "2" };
            object obj = (object)list;
            var objlist = (List<string>)obj;

            SocketUp();
            Task.Run(() =>
            {
                var isbool = Thread.CurrentThread.IsBackground;
            });

            var isbool1 = Thread.CurrentThread.IsBackground;

            Test2 test2 = new Test2();

            Gettime();

            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            //var appServer = new AppServer();
            /////注册回话新建事件处理方法
            //appServer.NewSessionConnected += AppServer_NewSessionConnected;
            //appServer.SessionClosed += AppServer_SessionClosed;
            //appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(AppServer_NewRequestReceived);
            ////Setup the appServer
            //if (!appServer.Setup(2012)) //Setup with listening port
            //{
            //    Console.WriteLine("Failed to setup!");
            //    Console.ReadKey();
            //    return;
            //}

            //Console.WriteLine();

            ////Try to start the appServer
            //if (!appServer.Start())
            //{
            //    Console.WriteLine("Failed to start!");
            //    Console.ReadKey();
            //    return;
            //}

            //Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            //while (Console.ReadKey().KeyChar != 'q')
            //{
            //    Console.WriteLine();
            //    continue;
            //}

            ////Stop the appServer
            //appServer.Stop();

            //Console.WriteLine("The server was stopped!");
            //Console.ReadKey();
        }


        /// <summary>
        /// 输出日期的差数
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int DaysBetweenDates(string date1, string date2)
        {
            var date3 = date1.ToDateTime();
            var date4 = date2.ToDateTime();
            if (date4>date3)
            {
                var sb = date4 - date3;

                return sb.Days;
            }

            TimeSpan sb1 = date3 - date4;

           return  sb1.Days;
        }


        public IList<string> AmbiguousCoordinates(string S)
        {
            var arr = new List<string>();
            for (int i = 0; i < S.Length; i++)
            {
                arr.Add(S[i].ToString());
            }
            return arr;
        }

        static void AddValue()
        {
            //动态类，可以作为基类被继承


            Dictionary<string, object> temp = new Dictionary<string, object>();
            temp.Add("Name", "金朝钱");
            temp["Age"] = 31;
            temp["Birthday"] = DateTime.Now;

            dynamic obj = new System.Dynamic.ExpandoObject();

            foreach (KeyValuePair<string, object> item in temp)
            {
                ((IDictionary<string, object>)obj).Add(item.Key, item.Value);
            }

            Console.Write(string.Format("Name:{0}", obj.GetType().GetProperty("name").GetValue(obj, null).ToString()));


            dynamic model = new ExpandoObject();
            var t1 = "t1".ToString();
            AddProperty(model, t1, 1212);
            var sb = model.t1;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        private static void Gettime()
        {
            Console.WriteLine($"当前时间{DateTime.Now.ToString()}");
            Task.Delay(2000).Wait();
            Console.WriteLine($"当前时间{DateTime.Now.ToString()}");
        }

        static Socket ReceiveSocket;
        private static void SocketUp()
        {
            int port = 8885;
            IPAddress ip = IPAddress.Any;  // 侦听所有网络客户接口的客活动
            ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议   <br>            ReceiveSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,true);  //有关套接字设置
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            ReceiveSocket.Bind(new IPEndPoint(ip, port)); //绑定IP地址和端口号
            ReceiveSocket.Listen(10);  //设定最多有10个排队连接请求
            Console.WriteLine("建立连接");
            Socket socket = ReceiveSocket.Accept();

            while (true)
            {
                byte[] receive = new byte[1024];
                socket.Receive(receive);
                var data = Encoding.ASCII.GetString(receive);
                Console.WriteLine("接收到消息：" + data);

                var sb = Newtonsoft.Json.JsonConvert.DeserializeObject<SocketRequest<SysUser>>(data);


                var json = Newtonsoft.Json.JsonConvert.SerializeObject(sb);
                byte[] send = Encoding.ASCII.GetBytes(json);
                socket.Send(send);
                Console.WriteLine("发送消息为：" + Encoding.ASCII.GetString(send));
            }
        }
        

        private static void AppServer_SessionClosed(AppSession session, CloseReason value)
        {

        }

        private static void AppServer_NewRequestReceived(AppSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            switch (requestInfo.Key.ToUpper())
            {
                case ("ECHO"):
                    session.Send(requestInfo.Body);
                    break;

                case ("ADD"):
                    session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
                    break;

                case ("MULT"):

                    var result = 1;

                    foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
                    {
                        result *= factor;
                    }

                    session.Send(result.ToString());
                    break;
            }
        }

        private static void AppServer_NewSessionConnected(AppSession session)
        {
            session.Send("Welcome to SuperSocket Telnet Server");
        }
    }


    public interface ITest
    {
        void Get1();
    }

    public interface ITest1: ITest
    {
        
    }

    public class Test : DynamicObject, ITest
    {
        public void Get1()
        {
            Console.WriteLine("调用");
        }
        Dictionary<string, object> Properties = new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                //在此可以做一些小动作
                //if (binder.Name == "Col")
                //　　Properties.Add(binder.Name + (Properties.Count), value.ToString());
                //else
                //　　Properties.Add(binder.Name, value.ToString());


                Properties.Add(binder.Name, value.ToString());
            }
            return true;
        }
    }
    public class Test2 : Test, ITest1
    {
        public Test2()
        {
            Get1();
        }
    }

    public class SysUser
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class SocketRequest<T>
    {
        public ClientOperateType ClientOperateType { get; set; }

        public T FData { get; set; }
    }
    public enum ClientOperateType
    {
        用户信息,
        产品信息,
        入库,
        出库,
        盘点
    }
}
