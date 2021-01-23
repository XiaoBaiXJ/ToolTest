using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace 缓存
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int> func = () =>
            {
                return 1;
            };
            func.Invoke();
            CancellationToken token = new CancellationToken();
            token.Register(() =>
            {

            });
            Task.Run(() => { }, token);

            Console.WriteLine("Hello World!");
            var ent = new Factory();
            foreach (var item in ent.GetType().GetProperties())
            {
                var v = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var descriptionName = v[0].Description;

                item.SetValue(ent, descriptionName + ":1");
            }


            List<string> list = new List<string>();
            var sb1 = list.Take(100);

            bool isContinue = true;
            new Thread(() =>
            {
                while (isContinue)
                {
                    StartThread();
                }
            }).Start();
            for (int i = 0; i < 100; i++)
            {
                if (i / 2 == 0)
                {
                    isContinue = false;
                    thread?.Abort();
                    Thread.Sleep(100);
                    StartThread();
                    isContinue = true;
                }
            }
        }

        /// <summary>
        /// 传入泛型委托  传出委托 异步调用等待线程结束 又要结果既不卡界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private static Func<T> ThreadWithReturn<T>(Func<T>func)
        {
            T t = default(T);
            var thread = new Thread(() =>
            {
                t = func.Invoke();
            });
            thread.Start();
            return () =>
            {
                while (thread.ThreadState!= ThreadState.Stopped)
                {
                    Thread.Sleep(200);
                }
               // thread.Join(); 一直等待线程结束。。
                return t;
            };
        }

        public static void GetUint(ushort value1)
        {
            
        }

        static Thread thread;

        private static void StartThread()
        {
            thread = new Thread(() =>
            {
                Console.WriteLine("我睡觉了");
                Console.WriteLine("我睡醒了");
            });
            if (thread.ThreadState != ThreadState.Running)
            {
                thread.Start();
            }
           
        }


        /// <summary>
        /// 获取枚举值上的Description特性的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetEnumDescription<T>(T obj)
        {
            var type = obj.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, obj));
            DescriptionAttribute descAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }

            return descAttr.Description;
        }
    }

    public class NewTask : Task
    {
        public NewTask(Action action) : base(action)
        {
        }
    }

    public class DetailAttribute : Attribute
    {
        public string AttrName { set; get; }
    }
    public class Factory
    {
        [Detail(AttrName = "宽度")]
        public string Width { set; get; }

        [Detail(AttrName = "高度")]
        public string Height { set; get; }

        [Detail(AttrName = "状态")]
        public string Status { set; get; }

        [Detail(AttrName = "Tag值")]
        public string Tag { set; get; }
    }

}
