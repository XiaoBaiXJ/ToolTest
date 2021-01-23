using System;
using System.Collections;
using System.Collections.Specialized;
using Unity;

namespace IOCMin
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = Container.SingleInstance;

            //var container = new Container();

            container.RegisterType<IEntityBase, StudentClass>();

            //container.RegisterType(action => 
            //{
            //    action.RegisterType<IEntityBase, StudentClass>();
            //});

            IEntityBase aa = container.Resolve<IEntityBase>();
            IEntityBase bb = container.Resolve<IEntityBase>();

            if (aa==bb)
            {

            }
            if (aa.Equals(bb))
            {

            }

            Console.WriteLine("Hello World!");
            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<IEntityBase, Student>();
            //container.RegisterType<IEntityBase, StudentClass>("adasd");
            //container.RegisterType<IEntityBase, StudentClass>("1212");
            //var student = container.Resolve<IEntityBase>("adasd");
            //IEntityBase studentclass = container.Resolve<IEntityBase>("1212");

            string[] a1 = "the quick brown fox".Split();
            string[] a2 = "THE QUICK BROWN FOX".Split();
            IStructuralEquatable se1 = a1;
            bool isTrue = se1.Equals(a2, StringComparer.InvariantCultureIgnoreCase);

            string s = "1212";
            dynamic sb = s;
            var sb1 = s;
            Test1(s, sb1);
            IEntityBase entityBase = new Student();

            Queue queue = new Queue();

            OrderedDictionary dictionary = new OrderedDictionary();


            Stack stack = new Stack();

            //IContainer container = new Container();
            //container.RegisterType<IEntityBase, Student>();
            //container.RegisterType<IEntityBase, StudentClass>();
            ////container.RegisterType<IEntityBase, StudentClass>();
            //IEntityBase student = container.Resolve<IEntityBase>();
            //var studentclass = container.Resolve<StudentClass>();
        }


        private static void Test1(object obj1,object obj2)
        {
#if DeBug
            global::System.Console.WriteLine("asda");
#endif
        }

        private static void Test1(object obj1, string obj2)
        {
#if DEBUG
            Console.WriteLine("asda");
#endif
        }
    }





    public class StudentClass: IEntityBase
    {
        public StudentClass()
        {
            Console.WriteLine("{0}被构造了!", GetType().Name);
        }
        public int Id { get; set; }
    }
}
