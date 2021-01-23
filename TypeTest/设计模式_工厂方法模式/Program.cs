using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace 设计模式_工厂方法模式
{
    class Program
    {
        static I工人 炒菜工人;
        static I工人 做菜工人;
        static I工人 茄子工人;
        private static IConfiguration _configuration;
        static void Main(string[] args)
        {
             q :Console.WriteLine("Hello World!");
            宫保鸡丁工人 人 = new 宫保鸡丁工人();
            炒菜工人 = new 西红柿炒蛋工人();
            做菜工人 = new 宫保鸡丁工人();
            茄子工人 = new 肉末茄子工人();
            炒菜工人.炒菜().做菜();
            做菜工人.炒菜().做菜();
            茄子工人.炒菜().做菜();
            decimal sb = 121212.991212M;
            var str = string.Format("{0}", Math.Round(sb, 2));
            //int i = 0;
            //if (i<10)
            //{
            //    i++;
            //    goto q;
            //}
          
        }
    }

    public class 西红柿炒蛋1 : 西红柿炒蛋
    {
        public override void 做菜()
        {
            base.做菜();
        }
    }



    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        static ConfigHelper()
        {
            //在当前目录或者根目录中寻找appsettings.json文件
            var fileName = "appsettings.json";

            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);

            _configuration = builder.Build();
        }

        public static string GetSectionValue(string key)
        {
            return _configuration.GetSection(key).Value;
        }
    }



    public interface I菜
    {
        void 做菜();
    }

    public abstract class 西红柿炒蛋 : I菜
    {
        public virtual void 做菜()
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

    public class 肉末茄子 : I菜
    {
        public void 做菜()
        {
            Console.WriteLine("做肉末茄子这道菜");
        }
    }

    public interface I工人
    {
        I菜 炒菜();
    }

    public class 宫保鸡丁工人 : I工人
    {

        public I菜 炒菜()
        {
            return new 宫保鸡丁();
        }

        private decimal dec;
    }
        
    public class 西红柿炒蛋工人 : I工人
    {
        public I菜 炒菜()
        {
            return new 西红柿炒蛋1();
        }
    }

    public class 肉末茄子工人 : I工人
    {
        public I菜 炒菜()
        {
            return new 肉末茄子();
        }
    }


}
