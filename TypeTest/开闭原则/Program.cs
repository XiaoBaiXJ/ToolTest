using System;

namespace 开闭原则
{
    class Program
    {
        static I画图 画图;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            画图 = new 柱状图();
            画图.画图();
        }
    }

    public interface I画图
    {
        void 画图();
    }

    public class 饼图 : I画图
    {
        public void 画图()
        {
            Console.WriteLine("饼图!");
        }
    }

    public class 柱状图 : I画图
    {
        public void 画图()
        {
            Console.WriteLine("柱状图!");
        }
    }

    public class 折线图 : I画图
    {
        public void 画图()
        {
            Console.WriteLine("折线图!");
        }
    }
}
