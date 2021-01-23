using System;

namespace 设计模式_简单工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            工人 师傅 = new 工人();
            师傅.炒菜(new 西红柿炒蛋());
            师傅.炒菜(new 宫保鸡丁());
        }
    }

    public interface I菜
    {
        void 做菜();
    }

    public class 西红柿炒蛋 : I菜
    {
        public void 做菜()
        {
            Console.WriteLine("做西红柿炒蛋这道菜");
        }
    }

    public class 宫保鸡丁 : I菜
    {
        public void 做菜()
        {
            Console.WriteLine("做宫保鸡丁这道菜");
        }
    }

    public class 工人
    {
        public void 炒菜(I菜 菜)
        {
            菜.做菜();
        }
    }
}
