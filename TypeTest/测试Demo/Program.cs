using System;

namespace 测试Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var entityDemo1 = EntityInstance.GetInstance;

            entityDemo1.EntityDemo2Model = new EntityDemo2();
            Console.WriteLine("准备开始让实体无效");

            entityDemo1.EntityDemo2Model = null;

            Console.WriteLine("实体已经无效");

            entityDemo1.EntityDemo2Model = new EntityDemo2();

            Console.WriteLine("实体有效");
            entityDemo1.EntityDemo2Model.Name = "关羽";

            EntityInstance.Start(() => { Console.WriteLine("开启"); });
            EntityInstance.GetEpc((str) => { Console.WriteLine("获取到EPC:" + str); });
            EntityInstance.GetEpc((str) => { Console.WriteLine("获取到EPC:" + str); });
            EntityInstance.GetEpc((str) => { Console.WriteLine("获取到EPC:" + str); });
            EntityInstance.GetEpc((str) => { Console.WriteLine("获取到EPC:" + str); });
            EntityInstance.GetEpc((str) => { Console.WriteLine("获取到EPC:" + str); });
            EntityInstance.Read(() => { Console.WriteLine("读取"); });
            EntityInstance.Stop(() => { Console.WriteLine("停止"); });
           
        }
    }

    public class EntityDemo1
    {
        public string Name { get; set; }

        public EntityDemo2 EntityDemo2Model { get; set; }
    }

    public class EntityDemo2
    {
        public string Name { get; set; }
    }

    public delegate void BoilerLogHandler(string epc);
    public class FidHelper
    {
        public FidHelper()
        {
        }

        public void GetEpc(string epc)
        {
            GetEpcEvent(epc);
        }

        public void 开启() 
        { }

        public void 读取()
        {
            获取Epc();
        }

        public void 停止()
        { }

        public void 获取Epc()
        {
            /// 实现委托 事件调用  得到epc 触发EPC
            GetEpc("123456");
        }

        /// <summary>
        /// 获取到Epc
        /// </summary>
        public event BoilerLogHandler GetEpcEvent;
    }

    public class EntityInstance
    {
        private static object Jobjec = new object();

        private static EntityDemo1 entityDemo1 = null;

        private EntityInstance() { }

        public static EntityDemo1 GetInstance
        {
            get 
            {
                if (entityDemo1 is null)
                {
                    lock (Jobjec)
                    {
                        if (entityDemo1 is null)
                        {
                            entityDemo1 = new EntityDemo1();
                            RfidHelperProp = new FidHelper();  //构造Rfid 访问
                        }
                    }
                   
                }
                return entityDemo1;
            }
        }

        private static FidHelper RfidHelperProp { get; set; }

        public static void Read(Action sead)
        {
            sead.Invoke();
            RfidHelperProp.读取();
        }

        public static void Stop(Action stop)
        {
            RfidHelperProp.停止();
            stop.Invoke();
        }

        public static void Start(Action start)
        {
            RfidHelperProp.开启();
            start.Invoke();
        }

        public static void GetEpc(Action<string>getepc)
        {
           RfidHelperProp.GetEpcEvent += (rFIDEpc) =>
            {
                getepc.Invoke(rFIDEpc);
                //var endinvoke = getepc.BeginInvoke(rFIDEpc, a => { },null);
                //getepc.EndInvoke(endinvoke);  //阻塞线程  .Net Core 不支持
            };
        }
    }
}
