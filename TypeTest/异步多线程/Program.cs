using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace 异步多线程
{

    /// <summary>
    /// Task 线程
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TaskDemo();
        }

        private static void TaskDemo()
        {
            List<Task> tasklist = new List<Task>();
            CancellationTokenSource source = new CancellationTokenSource();
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
                            throw new Exception(string.Format("执行失败{0}",t));
                        }
                        if (source.IsCancellationRequested)
                        {
                            Console.WriteLine("我取消了{0}",t);
                            return;
                        }
                        Console.WriteLine("我执行成功了{0}",t );
                    };
                    tasklist.Add(Task.Factory.StartNew(action, name, source.Token));
                }
                Task.WhenAll(tasklist.ToArray());
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
    }
}
