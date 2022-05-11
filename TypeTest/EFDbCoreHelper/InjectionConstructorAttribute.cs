using System;
using System.Collections.Generic;
using System.Text;

namespace EFDbCoreHelper
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionConstructorAttribute : Attribute
    {
        public InjectionConstructorAttribute()
        {
        }
    }
}
