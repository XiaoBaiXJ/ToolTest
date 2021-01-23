using System;

namespace 里式代换原则
{
    class Program
    {
        static I基类 基类;
        static I我是父亲 我是父亲;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            基类 = new 我是儿子();
            我负责调用 调用 = new 我负责调用();
            我是父亲 = new 我是孙子();
            调用.调用(基类);
            调用.调用父亲(我是父亲);
            基类.我要实现();
            我是父亲.我要实现();


        }
    }

    public interface I基类
    {
        void 我要实现();
    }

    public class 我是儿子 : I基类
    {
        public void 我要实现()
        {
            Console.WriteLine("我已经实现了,哈哈");
        }
    }

    public interface I我是父亲 
    {
        void 我要实现();
    }

    public class 我是孙子 : I我是父亲
    {
        public void 我要实现()
        {
            Console.WriteLine("我也会实现了,哈哈");
        }
    }

    public class 我负责调用
    {
        public void 调用(I基类 基类)
        {
            Console.WriteLine("我调用了" + 基类.GetType());
        }

        public void 调用父亲(I我是父亲 我是父亲)
        {
            Console.WriteLine("我调用了" + 我是父亲.GetType());
        }
    }
}
