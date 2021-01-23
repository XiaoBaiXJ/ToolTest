using System;
using System.Collections.Generic;
using System.Text;

namespace IOCMin
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionConstructorAttribute : Attribute
    {
        public InjectionConstructorAttribute()
        {
        }
    }
}
