using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托异步调用
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> action = (a) => 
            {
                Console.WriteLine(a);
                Console.ReadLine();
            };

       


            var invoke = action.BeginInvoke("BeginInvoke",e => { }, null);
            action.Invoke("Invoke");
            action.EndInvoke(invoke);

            

        }
    }
}
