using System;
using System.Collections.Generic;
using System.Text;

namespace IOCMin
{
    public class Student : IEntityBase
    {
        //public Student()
        //{
        //    Console.WriteLine("{0}被构造了!", GetType().Name);
        //}

        //public Student(StudentClass studentClass)
        //{
        //    Console.WriteLine("{0}被构造了!", GetType().Name);
        //}

        //public string Name { get; set; }

        //public string Password { get; set; }
        public int Id { get; set; }
    }
}
