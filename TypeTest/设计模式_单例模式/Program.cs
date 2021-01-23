using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 设计模式_单例模式
{
    class Program
    {
        static bool sb = true;
        static void Main(string[] args)
        {
            var list1j = new TestList2();
            list1j.List = new List<TestList3>();
            var sb2 = ClassIsNull<TestList2>.GetPropertbool(list1j);





            Task.Run(() =>
            {

            });
            Console.WriteLine("Hello World!");
            #region Task 异步调用
            for (int i = 0; i < 2; i++)
            {
                var cancelTokenSource = new CancellationTokenSource();
                Task.Run(() =>
                {
                    
                    while (!cancelTokenSource.IsCancellationRequested)
                    {
                        Console.WriteLine(DateTime.Now);
                        Console.ReadLine();
                        //await Gettask();
                    }
                }, cancelTokenSource.Token);
                cancelTokenSource.Cancel();
                Console.WriteLine("Done");
            }
          

            #endregion



            var a = 0;
            var c = a++ + ++a;
            string stri = "xujiang";
            string stri1 = "22";
            GetOut(out stri,out stri1);

            List<Items1> listsb = new List<Items1> { new Items1 {  Name="1"} };
            Items1 str11 = null;
            foreach (var item in listsb)
            {
                str11= item;
            }
            str11.Name = "张三";


            var str1 = "190827900040,1908279000400,自行打印,600 - 0100 - 02,HI2D,19 - 08 - 27,20.00,PCS,19 - 08 - 27,*,2019 - 08 - 27,W,WY1456684444";
            byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(str1);
            string str = "";
            foreach (byte b in buffer) str += string.Format("%{0:X}", b);


            单例模式懒汉.Get我是单例模式方法().Get单例模式();
            单例模式懒汉.Get我是单例模式方法().GetTwo单例模式();
            单例模式饿汉.Get单例模式();
            GetFalse();
            GetFalse();
            Zi zi = new Zi();
            int[] arr = { 1, 2, 3, 4, 0 };
            int[] arr1 = { 0, 1, 2, 3, 1 };

            ObservableCollection<string> list = new ObservableCollection<string>();
            List<int> listint = new List<int> { 1, 2, 3, 1, 2, 3, 4, 2 };
            var sb1 = listint.OrderBy(m => m).ToList();
            listint.Clear();
            listint = sb1;
            var listto = new List<string>() { "11", "22", "33" };
            list.ToList().AddRange(listto);

            var intsb = (100.0 / 120.0) * 100;
            
            TestList1 testList = new TestList1();
            testList.List = new List<TestList2>() { new TestList2 { List = new List<TestList3> { new TestList3 { Name = "张三" } } } };
            foreach (var item in testList.List)
            {
                foreach (var item1 in item.List)
                {
                    item1.Name = "asa";
                }
               
            }
           

            int sb = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]==0)
                {
                    sb = arr[i];
                }
                else
                {

                }
            }
        }


        private static Task Gettask()
        {
            return Task.Run(() =>
            {
                int b = 0;
                for (int i = 0; i < 100; i++)
                {
                    b += i;
                }
                Console.WriteLine(DateTime.Now);
                Console.ReadLine();
            });
        }
        private static void GetFalse()
        {
            sb = true;
            if (sb)
            {
               
            }
            sb = false;
            string str = "单例";
            switch (str)
            {
                case"单例":break;
                default:
                    break;
            }
        }

        private static void GetOut(out string s,out string go1)
        {
            s = "徐江";
            go1 = "11";
        }

      
    }

    public static class ClassIsNull<T> where T:class
    {
        public static string GetClassPropert(T t)
        {
            var type = t.GetType();
            var props = type.GetProperties();
            return (props.Where(prop => prop.GetValue(t) == null).Select(prop => $"{prop.Name}").FirstOrDefault());
        }

        public static bool GetPropertbool(T t)
        {
            bool b = false;
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.GetValue(t, null) is null|| p.GetValue(t, null).ToString() is "")
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
    }

    public class TestList1
    {
        public List<TestList2> List { get; set; }
    }

    public class TestList2
    {
     
        public List<TestList3> List { get; set; }
    }

    public class TestList3
    {
        public string Name { get; set; }
    }
    
    public class Fu1
    {
        public Fu1()
        {
            Isbb(Isaa);
        }

        protected virtual bool Isaa { get; } = false;

        public void Isbb(bool isac)
        {
            
        }
    }

    public class Items1
    {
        public string Name { get; set; }
    }

    public class Zi : Fu1
    {
        protected override bool Isaa => true;

        public Zi()
        {
            Isbb(Isaa);
        }
    }


    public enum Enumtest
    {
        单例=0,多态=1
    }

    /// <summary>
    /// 懒汉单例   进来再实例化 缺报线程安全 多线程
    /// </summary>
    public class 单例模式懒汉
    {
        private static 单例模式懒汉 单例 =null;

        private readonly static object lockS = new object();

        private 单例模式懒汉()
        {

        }

        public static 单例模式懒汉 Get我是单例模式方法()
        {
            if (单例 is null)
            {
                lock (lockS)
                {
                    if (单例 is null)
                    {
                         单例 = new 单例模式懒汉();
                    }
                }
            }
            return 单例;
        }

        public void Get单例模式()
        {
            Console.WriteLine("懒汉单例！");
        }

        public void GetTwo单例模式()
        {
            Console.WriteLine("第二次调用了懒汉单例！");
        }
    }

    /// <summary>
    /// 饿汉 调用这个类才会有用
    /// </summary>
    public class 单例模式饿汉
    {
        private static 单例模式饿汉 饿汉 = null;
        private static 单例模式饿汉 单例
        {
            get
            {
                if (饿汉 is null)
                {
                    饿汉 = new 单例模式饿汉();
                }
                return 饿汉;
            }
        }

        private 单例模式饿汉()
        {

        }
        public static void Get单例模式()
        {
            var sb = 单例;
            Console.WriteLine("饿汉单例！");
        }

    }
}
