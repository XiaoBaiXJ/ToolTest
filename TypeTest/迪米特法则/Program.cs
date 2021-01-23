using System;

namespace 迪米特法则
{
    /// <summary>
    /// 迪米特法则  类于类之间的联系要变小
    /// </summary>
    class Program
    {
        static I商店 商店;
        static I菜 菜;
        static I肉 肉;
        static I面 面;
        static I人 男人;
        static I人 女人;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            商店 = new KFC();
            菜 = new 蔬菜();
            肉 = new 牛肉();
            面 = new 面包();
            男人 = new 张三();
            女人 = new 李四();
            中间调用 调用 = new 中间调用();
            调用.调用(商店, 菜, 肉, 面, 男人);
            调用.调用完成();
        }
    }

    public class KFC : I商店
    {
        public void Make()
        {
            Console.WriteLine("我开始制作汉堡包了");
        }
    }

    public class 中间调用
    {
        public void 调用(I商店 商店, I菜 菜,I肉 肉, I面 面,I人 人)
        {
            商店.Make();
            Console.WriteLine("正在制作中");
        }

        public void 调用完成()
        {
            Console.WriteLine("制作完成,开始吃吧！");
        }
    }

    public class 蔬菜: I菜 { }

    public class 牛肉: I肉 { }

    public class 面包: I面 { }

    public interface I肉 { }
    public interface I菜 { }
    public interface I人{}
    public interface I面 { }

    public class 张三 : I人 { }

    public class 李四 : I人 { }

    public interface I商店
    {
        void Make();
    }
}
