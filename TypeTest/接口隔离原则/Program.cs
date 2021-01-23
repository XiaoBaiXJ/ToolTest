using System;

namespace 接口隔离原则
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            中间调用 调用 = new 中间调用();
            调用.调用帅哥(new 搜索());
            调用.调用美女(new 搜索());
            new 搜索().伴随();
            new 搜索().寻找();
            new 搜索().找到();
            只要美女 美女 = new 只要美女();
            美女.调用美女(new 只搜索美女());
        }
    }

    public interface I美女
    {
        void 寻找();

        void 找到();
    }

    public interface I帅哥
    {
        void 伴随();
    }

    public abstract class Serch : I美女, I帅哥
    {
        public virtual void 伴随()
        {
            Console.WriteLine("我正在伴随我的帅哥");
        }

        public virtual void 寻找()
        {
            Console.WriteLine("我正在寻找我的美女");
        }

        public virtual void 找到()
        {
            Console.WriteLine("我找到我的美女了");
        }
    }

    public class 搜索 : Serch
    {

    }

    public class 只搜索美女 : I美女
    {
        public void 寻找()
        {
            Console.WriteLine("我正在寻找我的美女");
        }

        public void 找到()
        {
            Console.WriteLine("我找到我的美女了");
        }
    }


    public class 中间调用
    {
        public void 调用美女(Serch 美女)
        {
            Console.WriteLine("我正在调用美女");
        }

        public void 调用帅哥(Serch 帅哥)
        {
            Console.WriteLine("我正在调用帅哥");
        }
    }

    public class 只要美女
    {
        public void 调用美女(I美女 美女)
        {
            Console.WriteLine("我正在调用美女");
        }
    }
}
