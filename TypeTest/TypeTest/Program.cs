using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TypeTest
{
    public class Program
    {

        public static void Main(string[] args)
        {

            Product product = new Product();
            product.Context = "马超";
            Product product11 = new Product();
            product11.Id =1;
            var mergedObject = MergeObjects(product, product11);

            List<Product> list = new List<Product>();
            List<Product> strlist = new List<Product>
            {
                new Product{ Context="1" }, new Product{  Context="2"},new Product{  Context="3"}
            };
            var sb = strlist.Select(m => { m.Id = strlist.IndexOf(m) + 1; return m; }).ToList();


            Cat cat = new Cat("加菲猫");
            Mouse mouse = new Mouse("米奇");
            cat.CatCome += mouse.MouseCom;
            cat.CatGo();
           //AppPermission appPermission = new AppPermission(new TestOne());
            //int i = 9;
            //object obj = i;
            //int b = (int)obj;
            //Console.WriteLine(b);
            //var sb = i.GetType(); 
            //var sc = typeof(ITest);
            //StringBuilder sb=new StringBuilder();
            //sb.Append("sb");
            //var ab = sb.ToString();

            //string str = "sss";
            //var IsSb = str.CompareTo("ssa");

            ////string str = "Hello Word Nxet One";
            ////var one = str.StartsWith("Hel",StringComparison.OrdinalIgnoreCase);
            ////var end = str.EndsWith("One");
            //ITestOne testOne = new ITestOne();
            //Type type = testOne.GetType();
            //var name = type.Name;
            //var name1 = type.Namespace;
            //var name2 = type.Assembly;
            //var name3 = type.GetFields();
            //testOne.GoCome();
            //Test1 test = new Test1();
            //test.GetmodelSet();
            //test.SetModel();
            //var int1 = test.IsOut(i,out int a);
            //var int2 = test.IsRef(39, ref i);
            //var int3 =  test.GetTaskAsync().GetAwaiter().GetResult();




            //TheradTest theradTest = new TheradTest();

            //Mydelegate mydelegate = new Mydelegate(theradTest.Fun); //委托
            //mydelegate += theradTest.Fun;
            //mydelegate.Invoke();

            //Thread thread = new Thread(theradTest.Fun);
            //thread.Start();
            //var state = thread.ThreadState;
            //Console.ReadKey();

            //Typetest typetest = new Typetest();
            //Typetestsub typetestsub = new Typetestsub();
            //if (typetestsub is Typetest)
            //{
            //    var istypetestsub = typetestsub.GetType();
            //}

            //string[] arr = {"出库/出库绑定","入库/入库绑定","出库/取消","入库/取消" };
            //foreach (var item in arr)
            //{
            //    var splitarr = item.Split('/');
            //    if (!AppPermissionlist.ContainsKey(splitarr[0]))
            //    {
            //        var model = new AppPermission { Name = splitarr[0], list = new List<AppPermission>() };
            //        var endmodel = new List<AppPermission>();
            //        for (int i = 1; i < splitarr.Length; i++)
            //        {
            //            //var iscun = model.list.Find((l) => l.Name.Equals(splitarr[i]));
            //            var modeltest = new AppPermission { Name = splitarr[i], list = new List<AppPermission>() };
            //            model.list.Add(modeltest);
            //            endmodel = new List<AppPermission> { model };
            //            model = modeltest;
            //        }
            //       // AppPermissionlist[splitarr[0]] = endmodel;
            //        AppPermissionlist.Add(splitarr[0], endmodel) ;
            //    }
            //    else
            //    {
            //        var lv0 = AppPermissionlist[splitarr[0]];
            //        var model = new AppPermission { Name = splitarr[0], list = new List<AppPermission>() };
            //        var endmodel = new List<AppPermission>();
            //        for (int i = 1; i < splitarr.Length; i++)
            //        {
            //            var modeltest = new AppPermission { Name = splitarr[i], list = new List<AppPermission>() };
            //            model.list.Add(modeltest);
            //            endmodel = new List<AppPermission> { model };
            //            model = modeltest;
            //        }
            //        lv0.Add(model);
            //    }

            //}

            //var sb1 = AppPermissionlist;
        }
        //public static BasicBasicData.BasicDictionary<string, List<AppPermission>> AppPermissionlist
        //{
        //    get; set;
        //} = new BasicBasicData.BasicDictionary<string, List<AppPermission>>();


        public static T MergeObjects<T>(T obj1, T obj2)
        {
            var objResult = Activator.CreateInstance(typeof(T));

            var allProperties = typeof(T).GetProperties().Where(x => x.CanRead && x.CanWrite);
            foreach (var pi in allProperties)
            {
                object defaultValue;
                if (pi.PropertyType.IsValueType)
                {
                    defaultValue = Activator.CreateInstance(pi.PropertyType);
                }
                else
                {
                    defaultValue = null;
                }

                var value = pi.GetValue(obj2, null);

                if (value != defaultValue)
                {
                    pi.SetValue(objResult, value, null);
                }
                else
                {
                    value = pi.GetValue(obj1, null);

                    if (value != defaultValue)
                    {
                        pi.SetValue(objResult, value, null);
                    }
                }
            }

            return (T)objResult;
        }

    }


    public class Product
    {
        public int Id { get; set; }

        public string Context { get; set; }
    }

    public class Cat
    {
        public Cat(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void CatGo()
        {

            if (CatCome != null)
                CatCome();
        }

        public Action CatCome;
    }

    public class Mouse
    {
        public string Name { get; set; }
        public Mouse(string Name)
        {
           this.Name = Name;
        }

        public void MouseCom() { }
    }

    [ComImport, CoClass(typeof(TestOne))]
    [Guid("d60908eb-fd5a-4d3c-9392-8646fcd1edce")]
    public interface ITestOne
    {
        void GoCome();
    }

    public class TestOne : ITestOne
    {
        public void GoCome()
        {
           
        }
    }
    public class AppPermission
    {
        public ITestOne testOne1;
        public AppPermission(ITestOne testOne)
        {
            testOne1 = testOne;
            testOne1.GoCome();
        }

        public string Name { get; set; }

        public List<AppPermission> list { get; set; }
    }


    public abstract class AbstractTest
    {
        public virtual void Getmodel()
        {
        }

        public abstract void SetModel();
    }

    public interface ITest
    {
        void GetmodelSet();
    }

    public class Test1: AbstractTest, ITest
    {
        public Test1()
        {
        }

        public int IsOut( int sb, out int a)
        {
            a = sb;
            return a;
        }

        public int IsRef(int sb, ref int a)
        {

            return sb;
        }

        public async Task<int> GetVAsync()
        {
            await Task.Delay(100);
            return 1;
        }

        public async Task<Test2> GetTaskAsync()
        {
            Test2 test2 = new Test2();
           var sb = await GetVAsync();
            test2.Xuhao = sb;
            return test2;
        }

        public override void SetModel()
        {
        }

        public void GetmodelSet()
        {
        }
    }
    public class Test2:Test1
    {
        public int Xuhao { get; set; }

        public override void SetModel()
        {
            base.SetModel();
        }

        public override void Getmodel()
        {
            base.Getmodel();
        }
    }

    public class TheradTest
    {
        int i = 0;


        public void Fun()
        {
            Console.WriteLine("第"+i+"次") ;
            i++;
            var sb = Fun1(Geti, i);
        }

        public int Geti(int b)
        {
            b++;
            return b;
        }

        

        public int Fun1(MyDome my,int b)
        {
            Console.WriteLine("第" + b + "次");
            i++;
            return i;
        }
    }

    public delegate void Mydelegate();  //委托事件

    public class Typetest
    {

    }

    public class Typetestsub: Typetest
    {

    }

    public delegate int MyDome(int i);


    public class MoAttack
    {
        public MoAttack()
        {
        }

        public void CityGateAsk()
        {
            LiuDeHua ldh = new LiuDeHua();// ① 演员直接侵入剧本
            ldh.responseAsk();
        }
    }
    /// <summary>
    /// 刘德华
    /// </summary>
    public class LiuDeHua:IGeLi
    {
        public void responseAsk()
        {
            IGeLi geli = new LiuDeHua(); //① 引入革离角色接口
            geli.responseAsk("墨者革离！");// ② 通过接口开展剧情
        }

        public void responseAsk(string str)
        {

        }
    }

    /// <summary>
    /// 饰演角色
    /// </summary>
    public interface IGeLi
    {
        void responseAsk(string str);
    }
}
