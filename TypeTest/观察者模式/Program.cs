using System;
using System.Collections.Generic;

namespace 观察者模式
{
    /// <summary>
    /// 观察者模式  猫捉老鼠 老鼠跑 狗叫  男人说  女人笑  孩子惊叫  老人打猫  猫结束
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GoCatch();
        }

        private static void GoCatch()
        {
            {
                Cat cat = new Cat();
                cat.Add(new Mouse());
                cat.Add(new Dog());
                cat.Add(new WoMen());
                cat.Add(new Men());
                cat.Add(new OldPeople());
                cat.Add(new Children());
                cat.MiaoObserver();
            }
            {
                Cat cat = new Cat();
                cat.MiaoHandler += new Mouse().Run;
                cat.MiaoHandler += new Dog().Wang;
                cat.MiaoHandler += new Men().Say;
                cat.MiaoHandler += new WoMen().Laugh;
                cat.MiaoHandler += new Children().Call;
                cat.MiaoHandler += new OldPeople().Hit;
                cat.MiaoEvent();
            }
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

    public interface IObserver
    {
        void Action();
    }

    public class Cat 
    {
        private List<IObserver> _ObserverList = new List<IObserver>();
        public void Add(IObserver observer)
        {
            this._ObserverList.Add(observer);
        }
        public void Remove(IObserver observer)
        {
            this._ObserverList.Remove(observer);
        }

        public void Miaou()
        {
            Console.WriteLine("猫猫：喵--------------呜-----------");
        }

        public void Grab()
        {
            RandomHelper helper = new RandomHelper();
            var state = helper.GetBoolRandom() ? "猫猫：哈哈--------------抓到你了-----------" : "猫猫：呜呜--------------难受----------";
            Console.WriteLine(state);
        }

        /// <summary>
        /// 通过继承接口触发观察者
        /// </summary>
        public void MiaoObserver()
        {
            Miaou();
            foreach (var observer in this._ObserverList)
            {
                observer.Action();
            }
            Grab();
        }



        /// <summary>
        ///通过事件触发 观察者
        /// </summary>

        public event Action MiaoHandler;
        public void MiaoEvent()
        {
            Miaou();
            if (MiaoHandler != null)
                foreach (Action item in this.MiaoHandler.GetInvocationList())
                {
                    item.Invoke();
                }
            Grab();
            //this.MiaoHandler.Invoke();
        }
    }

    public class Dog : IObserver
    {
        public void Action()
        {
            Wang();
        }

        public void Wang()
        {
            Console.WriteLine("狗狗：旺--------------旺-----------");
        }
    }

    public class Mouse : IObserver
    {
        public void Action()
        {
            Run();
        }

        public void Run()
        {
            Console.WriteLine("老鼠：嘶--------------嘶-----------");
        }
    }


    public class WoMen : IObserver
    {
        public void Action()
        {
            Laugh();
        }

        public void Laugh()
        {
            Console.WriteLine("女人：好--------------可爱-----------");
        }
    }

    public class Men : IObserver
    {
        public void Action()
        {
            Say();
        }

        public void Say()
        {
            Console.WriteLine("男人：快--------------抓住-----------");
        }
    }


    public class Children : IObserver
    {
        public void Action()
        {
            Call();
        }

        public void Call()
        {
            Console.WriteLine("孩子：惊--------------吓-----------");
        }
    }


    public class OldPeople : IObserver
    {
        public void Action()
        {
            Hit();
        }

        public void Hit()
        {
            Console.WriteLine("老人：别--------------跑-----------");
        }
    }
}
