using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace 装饰器模式
{
    /// <summary>
    /// 装饰器  组合和继承都能运用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Buy();
        }

        private static void Buy()
        {
            {
                var tasklist = new List<Task>();
                for (int i = 0; i < 1000; i++)
                {
                    tasklist.Add(Task.Factory.StartNew(() =>
                        {

                            Random random = new Random();
                            var index = random.Next(0, 12);
                            var shoppers = GetList()[index];
                            IPeople people = new People(shoppers);
                            ISupermarket supermarket = new Yonghui();
                            people.GoBuy(supermarket);
                            people.GoBuy(new Personal(supermarket));
                            people.GoBuy(new Kiosk(supermarket));
                            people.GoBuy(new Supermarket(supermarket));
                            if (shoppers.Sex)
                            {
                                IPeople people2 = new Man(shoppers);
                                people2.GoBuy(supermarket);
                                people2.GoBuy(new Personal(supermarket));
                                people2.GoBuy(new Kiosk(supermarket));
                                people2.GoBuy(new Supermarket(supermarket));
                            }
                            else
                            {
                                IPeople people1 = new Woman(shoppers);
                                people1.GoBuy(supermarket);
                                people1.GoBuy(new Personal(supermarket));
                                people1.GoBuy(new Kiosk(supermarket));
                                people1.GoBuy(new Supermarket(supermarket));
                            }

                        }));
                }
                Task.WaitAll(tasklist.ToArray());
            }
        }

        private static List<Shoppers> GetList()
        {
            var list = new List<Shoppers>();
            list.Add(new WeShoppers() { Name = "张三", Product = "麦片", Sex = true });
            list.Add(new WeShoppers() { Name = "张九", Product = "牛奶", Sex = false });
            list.Add(new WeShoppers() { Name = "张二", Product = "奶茶", Sex = true });
            list.Add(new WeShoppers() { Name = "张一", Product = "泡面", Sex = false });
            list.Add(new WeShoppers() { Name = "张八", Product = "茶叶蛋", Sex = false });
            list.Add(new WeShoppers() { Name = "张七", Product = "矿泉水", Sex = true });
            list.Add(new WeShoppers() { Name = "张六", Product = "棒棒糖", Sex = true });
            list.Add(new WeShoppers() { Name = "张五", Product = "雪糕", Sex = false });
            list.Add(new WeShoppers() { Name = "张四", Product = "火腿肠", Sex = true });
            list.Add(new WeShoppers() { Name = "李三", Product = "牛肉面", Sex = true });
            list.Add(new WeShoppers() { Name = "李四", Product = "小面", Sex = true });
            list.Add(new WeShoppers() { Name = "李五", Product = "酸辣粉", Sex = false });
            list.Add(new WeShoppers() { Name = "李六", Product = "肠粉", Sex = true });
            return list;
        }
    }


    public interface ISupermarket
    {
        void Buy(string product);
    }

    public class Supermarket : ISupermarket
    {
        private ISupermarket supermarket = null;

        public Supermarket(ISupermarket _supermarket)
        {
            supermarket = _supermarket;
        }

        /// <summary>
        /// 买东西
        /// </summary>
        public virtual void Buy(string product)
        {
            Console.WriteLine("{0}在卖{1}", GetType().Name,product);
        }
    }

    public class Kiosk : Supermarket
    {
        public Kiosk(ISupermarket _supermarket) : base(_supermarket)
        {
        }

        public override void Buy(string product)
        {
            Console.WriteLine("{0}在卖{1}", GetType().Name, product);
        }
    }

    public class Personal : Kiosk
    {
        public Personal(ISupermarket _supermarket) : base(_supermarket)
        {

        }
        public override void Buy(string product)
        {
            Console.WriteLine("{0}在卖{1}", GetType().Name, product);
        }
    }

    public class Yonghui : ISupermarket
    {
        public void Buy(string product)
        {
            Console.WriteLine("{0}在卖{1}", GetType().Name, product);
        }
    }

    public interface IPeople
    {
        void GoBuy(ISupermarket supermarket);
    }

    public class People : IPeople
    {
        protected Shoppers shoppers = null;

        public People(Shoppers _shoppers)
        {
            shoppers = _shoppers;
        }
        public virtual void GoBuy(ISupermarket supermarket)
        {
            Console.WriteLine("{0}去{1}买东西", shoppers.Name, supermarket.GetType().Name);
            Thread.Sleep(2000);
            supermarket.Buy(shoppers.Product);
        }
    }

    public class Man : People
    {
        public Man(Shoppers _shoppers) : base(_shoppers)
        {
        }

        public override void GoBuy(ISupermarket supermarket)
        {
            Console.WriteLine("{0}去{1}买东西", shoppers.Name, supermarket.GetType().Name);
            Thread.Sleep(2000);
            supermarket.Buy(shoppers.Product);
        }
    }

    public class Woman : People
    {
        public Woman(Shoppers _shoppers) : base(_shoppers)
        {
        }

        public override void GoBuy(ISupermarket supermarket)
        {
            Console.WriteLine("{0}去{1}买东西", shoppers.Name, supermarket.GetType().Name);
            Thread.Sleep(2000);
            supermarket.Buy(shoppers.Product);
        }
    }

    public abstract class Shoppers
    {
        public string Name { get; set; }

        public bool Sex { get; set; }

        public string Product { get; set; }

        public abstract void Buy();
    }

    public class WeShoppers : Shoppers
    {
        public override void Buy()
        {
            Console.WriteLine("我要去购买");
        }
    }
}
