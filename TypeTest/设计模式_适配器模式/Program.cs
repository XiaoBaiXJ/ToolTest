using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace 设计模式_适配器模式
{
    /// <summary>
    /// 结构模式 组合优于继承  组合灵活多变 继承只为一个类型服务
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //var sb = Container.Instance;

            适配器Demo<Student>();
        }

        public static void 适配器Demo<T>()
        {

            IDbFactory factory = new ChildDbFactory();

            #region 反射
            {
                var helper = factory.CreateDbEntity(typeof(SqlDbHelper).FullName);
                helper.Add<T>();
                helper.Delete<T>();
                helper.Select<T>();
                helper.Update<T>();
                helper.Create<T>();
            }
            #endregion

            //Sql
            {
                var helper = factory.CreateDbEntity(ClassType.SqlDbHelper);
                helper.Add<T>();
                helper.Delete<T>();
                helper.Select<T>();
                helper.Update<T>();
                helper.Create<T>();
            }
            //Oracle
            {
                var helper = factory.CreateDbEntity(ClassType.OracleDbHelper);
                helper.Add<T>();
                helper.Delete<T>();
                helper.Select<T>();
                helper.Update<T>();
                helper.Create<T>();
            }
            //MySql
            {
                var helper = factory.CreateDbEntity(ClassType.MySqlDbHelper);
                helper.Add<T>();
                helper.Delete<T>();
                helper.Select<T>();
                helper.Update<T>();
                helper.Create<T>();
            }
            //NewClassSql
            {
                var helper = factory.CreateDbEntity(ClassType.NewClassDbHelper);
                helper.Add<T>();
                helper.Delete<T>();
                helper.Select<T>();
                helper.Update<T>();
                helper.Create<T>();
            }
            //NewObjSql
            {
                var helper = factory.CreateDbEntity(ClassType.NewObjDbHelper);
                helper.Add<T>();
                helper.Delete<T>();
                helper.Select<T>();
                helper.Update<T>();
                helper.Create<T>();
            }
            Console.Read();
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Age { get; set; }

        public string Sex { get; set; }
    }


    public interface IDbHelper
    {
        void Add<T>();

        void Delete<T>();

        void Update<T>();

        void Select<T>();

        void Create<T>();
    }

    public class SqlDbHelper : IDbHelper
    {
        public void Add<T>()
        {
            Console.WriteLine("Sql新增一条数据");
        }

        public void Create<T>()
        {
            Console.WriteLine("Sql创建一张表");
        }

        public void Delete<T>()
        {
            Console.WriteLine("Sql删除一条数据");
        }

        public void Select<T>()
        {
            Console.WriteLine("Sql查询一条数据");
        }

        public void Update<T>()
        {
            Console.WriteLine("Sql修改一条数据");
        }
    }

    public class OracleDbHelper : IDbHelper
    {
        public void Add<T>()
        {
            Console.WriteLine("Oracle新增一条数据");
        }

        public void Create<T>()
        {
            Console.WriteLine("Oracle创建一张表");
        }

        public void Delete<T>()
        {
            Console.WriteLine("Oracle删除一条数据");
        }

        public void Select<T>()
        {
            Console.WriteLine("Oracle查询一条数据");
        }

        public void Update<T>()
        {
            Console.WriteLine("Oracle修改一条数据");
        }
    }


    public class MySqlDbHelper : IDbHelper
    {
        public void Add<T>()
        {
            Console.WriteLine("MySql新增一条数据");
        }

        public void Create<T>()
        {
            Console.WriteLine("MySql创建一张表");
        }

        public void Delete<T>()
        {
            Console.WriteLine("MySql删除一条数据");
        }

        public void Select<T>()
        {
            Console.WriteLine("MySql查询一条数据");
        }

        public void Update<T>()
        {
            Console.WriteLine("MySql修改一条数据");
        }
    }

    public interface IDbNewHelper
    {
        void NewAdd<T>();

        void NewDelete<T>();

        void NewUpdate<T>();

        void NewSelect<T>();

        void NewCreate<T>();
    }

    public class MiddleHelper : IDbNewHelper
    {
        public void NewAdd<T>()
        {
            Console.WriteLine("NewClassSql增加一条数据");
        }

        public void NewCreate<T>()
        {
            Console.WriteLine("NewClassSql创建一张表");
        }

        public void NewDelete<T>()
        {
            Console.WriteLine("NewClassSql删除一条数据");
        }

        public void NewSelect<T>()
        {
            Console.WriteLine("NewClassSql查询一条数据");
        }

        public void NewUpdate<T>()
        {
            Console.WriteLine("NewClassSql修改一条数据");
        }
    }


    /// <summary>
    /// 类适配器
    /// </summary>
    public class NewClassDbHelper : MiddleHelper, IDbHelper
    {
        public void Add<T>()
        {
            base.NewAdd<T>();
        }

        public void Create<T>()
        {
            base.NewCreate<T>();
        }

        public void Delete<T>()
        {
            base.NewDelete<T>();
        }

        public void Select<T>()
        {
            base.NewSelect<T>();
        }

        public void Update<T>()
        {
            base.NewUpdate<T>();
        }
    }

    /// <summary>
    /// 对象适配器
    /// </summary>
    public class NewObjDbHelper :IDbHelper
    {
        MiddleHelper helper = new MiddleHelper();
        public void Add<T>()
        {
            helper.NewAdd<T>();
        }

        public void Create<T>()
        {
            helper.NewCreate<T>();
        }

        public void Delete<T>()
        {
            helper.NewDelete<T>();
        }

        public void Select<T>()
        {
            helper.NewSelect<T>();
        }

        public void Update<T>()
        {
            helper.NewUpdate<T>();
        }
    }

    /// <summary>
    /// 工厂 
    /// </summary>
    public interface IDbFactory
    {
        IDbHelper CreateDbEntity(ClassType type);

        IDbHelper CreateDbEntity(string classtype);
    }

    /// <summary>
    /// 实现工厂
    /// </summary>
    public class DbFactory : IDbFactory
    {
        /// <summary>
        /// 不反射
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual IDbHelper CreateDbEntity(ClassType type)
        {
            IDbHelper helper;
            switch (type)
            {
                case ClassType.SqlDbHelper:
                    helper = new SqlDbHelper();
                    break;
                case ClassType.MySqlDbHelper:
                    helper = new MySqlDbHelper();
                    break;
                case ClassType.OracleDbHelper:
                    helper = new OracleDbHelper();
                    break;
                case ClassType.NewObjDbHelper:
                    helper = new NewObjDbHelper();
                    break;
                case ClassType.NewClassDbHelper:
                    helper = new NewClassDbHelper();
                    break;
                default:
                    helper = default;
                    break;
            }
            return helper;
        }

        /// <summary>
        /// 反射  需要完全限定名
        /// </summary>
        /// <returns></returns>
        public virtual IDbHelper CreateDbEntity(string fullname)
        {
            //获取当前程序集
            Assembly ass = Assembly.GetCallingAssembly();
            //获取程序集中的类
            Type t = ass.GetType(fullname);
           // var t = ass.GetTypes().FirstOrDefault(m => m.Name == classtype);
            if (!(t is null))
            {
                //创建类的实例对象
                if (Activator.CreateInstance(t) is IDbHelper helper)
                {
                    return helper;
                }
            }
            return default;
        }
    }

    /// <summary>
    /// 为IOC做准备
    /// </summary>
    public class ChildDbFactory : DbFactory
    {
        public override IDbHelper CreateDbEntity(ClassType type)
        {
            Console.WriteLine("我创建了一次{0},直接简单工厂 传入类型创建", type.ToString());
            return base.CreateDbEntity(type);
        }

        public override IDbHelper CreateDbEntity(string classtype)
        {
            Console.WriteLine("我创建了一次反射创建");
            return base.CreateDbEntity(classtype);
        }
    }

    public enum ClassType
    {
        SqlDbHelper, MySqlDbHelper, OracleDbHelper, NewClassDbHelper, NewObjDbHelper
    }
}
