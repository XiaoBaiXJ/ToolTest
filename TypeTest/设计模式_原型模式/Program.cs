using System;
using System.Collections.Generic;

namespace 设计模式_原型模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            爷爷 new爷爷 = new 爷爷()
            {
                Name = "jack",
                 Ctry1=new Ctiy() { Name="成都" },
                Age = 20,
                Address = "青岛"
            };
            Console.WriteLine("克隆前：" + new爷爷.Address);
            //一个克隆人，通过clone方法替代了new方法
            爷爷 clonePerson = (爷爷)new爷爷.Clone();
            clonePerson.Address = "武昌";

            var ctry2 = new Ctiy { Name = "重庆" };

            clonePerson.Ctry1.Name="daaaa";

            Console.WriteLine("克隆后：" + clonePerson.Address);


            爸爸 new爸爸 = new 爸爸()
            {
                Name = "jack",
                Age = 20,
                Address = "青岛"
            };
            爸爸 clonePerson1 = (爸爸)new爸爸.Clone();
            
            
            爷爷 yey = new 爷爷()
            {
                Name = "jack",
                Age = 20,
                Address = "青岛"
            };
            Console.WriteLine("赋值前：" + yey.Address);
            爷爷 yey1 = yey;
            yey1.Address = "北京";
            Console.WriteLine("赋值后：" + yey.Address);
        }
    }

    public abstract class 祖宗
    {
        public abstract object Clone();
    }

    public interface I祖宗
    {
        public object Clone();
    }

    public class 爷爷 : 祖宗
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Ctiy Ctry1 { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Ctiy
    {
        public string Name { get; set; }
    }

    public class 爸爸 : I祖宗
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
