using System;
using System.Collections.Generic;
using System.Text;

namespace EFDbHelper
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionConstructorAttribute : Attribute
    {
        public InjectionConstructorAttribute()
        {
        }
    }
}
