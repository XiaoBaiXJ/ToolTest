using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace 代理模式
{

    class Program
    {
        private static readonly object lockObject = new object();

        //private static volatile AtomicBoolean _isScanning;
        static void Main(string[] args)
        {
            I火车站 火车站 = new 车票零售店();
            var tasklist = new List<Task>();
            RandomHelper helper = new RandomHelper();
            CancellationTokenSource source = new CancellationTokenSource();
            for (int i = 0; i < 10000; i++)
            {
                var name = "零售店购买";
                tasklist.Add(Task.Factory.StartNew((t) =>
                {
                    lock (lockObject)
                    {
                        var threadId = Thread.CurrentThread.ManagedThreadId;
                        var state = string.Format("当前线程{0}", threadId);
                        Console.WriteLine("{0}开始", state);
                        if (!火车站.查询(threadId))
                        {
                            火车站.去火车站购买();
                        }
                        else
                        {
                            火车站.购买();
                        }
                        //if (BasicData.BasicDictionary.ContainsKey(threadId))
                        //{
                        //    火车站.购买结束(BasicData.BasicDictionary[threadId]);
                        //}
                        //else
                        //{
                        //    火车站.购买结束(false);
                        //}
                        火车站.购买结束(BasicData.BasicDictionary[threadId]);
                        Console.WriteLine("{0}结束", state);
                    }
                }, name, source.Token));
            }
            Task.WaitAll(tasklist.ToArray());
        }
    }

    /// <summary>
    /// 随机数 获取
    /// </summary>
    public class RandomHelper
    {
        Random rd = new Random();
        public bool GetBoolRandom()
        {
            int x = rd.Next();
            return (x % 2 == 0);
        }
        public int GetIntRandom()
        {
            return rd.Next();
        }
    }

    public interface I火车站
    {
        bool 查询(int threadId);
        void 购买();

        void 去火车站购买();

        void 购买结束(bool state);
    }

    public class 车票零售店 : I火车站
    {
        public void 去火车站购买()
        {
            Console.WriteLine("正在乘坐交通工具去火车站....");
            火车站.Instance.去火车站购买();
        }

        public bool 查询(int threadId)
        {
            Console.WriteLine("正在查询车票....");
            if (BasicData.BasicDictionary.ContainsKey(threadId))
            {
                if (BasicData.BasicDictionary[threadId])
                {
                    var state = 火车站.Instance.查询(threadId);
                    BasicData.BasicDictionary[threadId] = state;
                    return state;
                }
                Console.WriteLine("当前车票已经售完,请注意其他期间的放票.....");
                return false;
            }
            var state1 = 火车站.Instance.查询(threadId);
            BasicData.BasicDictionary.Add(threadId, state1);
            return state1;
        }

        public void 购买()
        {
            Console.WriteLine("正在购买车票....");
            火车站.Instance.购买();
        }

        public void 购买结束(bool state)
        {
            if (state)
            {
                Console.WriteLine("正在查询结果....");
                火车站.Instance.购买结束(state);
                return;
            }
            Console.WriteLine("当前车票已经售完,请注意其他期间的放票.....");
        }
    }

    public class 火车站 : I火车站
    {
        private 火车站() 
        {
            Console.WriteLine("我被构造了");
        }
        private static 火车站 instance;

        public readonly static object obj = new object();

        /// <summary>
        /// 单例   确定是是谁 用这个  不确定 用构造传入
        /// </summary>
        public static 火车站 Instance
        {
            get 
            {
                if (instance is null)
                {
                    lock (obj)
                    {
                        if (instance is null)
                        {
                            instance = new 火车站();
                        }
                    }
                }
                return instance;
            }
        }

        RandomHelper helper = new RandomHelper();


        public bool 查询(int threadId)
        {
            Thread.Sleep(2000);
            var state = helper.GetBoolRandom()?"成功购买车票":"当前车票已经购买完了";
            Console.WriteLine(state);
            return helper.GetBoolRandom();
        }

        public void 购买()
        {
            Thread.Sleep(2000);
            Console.WriteLine("正在购买车票....");
        }

        public void 购买结束(bool state)
        {
            Thread.Sleep(2000);
            var sure = state ? "太简单了,成功" : "我太难了,失败";
            Console.WriteLine("购买结束,{0}", sure);
        }

        public void 去火车站购买()
        {
            Thread.Sleep(2000);
            Console.WriteLine(helper.GetBoolRandom() ? "火车站还有余票" : "当前票已全部被购买");
        }
    }

    public static class BasicData
    {
        public static Dictionary<int, bool> BasicDictionary { get; set; }  = new Dictionary<int, bool>();
    }
}
