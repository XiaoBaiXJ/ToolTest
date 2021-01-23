using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IOCMin
{
    public class Container : IContainer
    {
        /// <summary>
        /// 线程保护字典
        /// </summary>
        private ConcurrentDictionary<string, Type> ContainerDictionary = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 不能被初始化
        /// </summary>
        private Container()
        {
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static IContainer SingleInstance => new Container();


        /// <summary>
        /// 存对象
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        public void RegisterType<IEntity, Entity>()
        {
            ContainerDictionary[typeof(IEntity).FullName] = typeof(Entity);
        }


        public void RegisterType(Action<IContainer> action)
        {
            action?.Invoke(SingleInstance);
        }

        /// <summary>
        /// 取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEntity Resolve<IEntity>()
        {
            var type = ContainerDictionary[typeof(IEntity).FullName];
            var rtsobj = CreateObject(type);
            if (rtsobj is IEntity entity)
            {
                return entity;
            }
            return default;
        }


        private object CreateObject(Type type)
        {
            try
            {
                ConstructorInfo[] ctorArray = type.GetConstructors();
                ConstructorInfo ctor = null;
                if (ctorArray.Count(c => c.IsDefined(typeof(InjectionConstructorAttribute), true)) > 0)
                {
                    ctor = ctorArray.FirstOrDefault(c => c.IsDefined(typeof(InjectionConstructorAttribute), true));
                }
                else
                {
                    ctor = ctorArray.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
                }
                List<object> paraList = new List<object>();
                foreach (var parameter in ctor.GetParameters())
                {
                    Type paraType = parameter.ParameterType;
                    Type targetType = this.ContainerDictionary[paraType.FullName];
                    object para = this.CreateObject(targetType);
                    //递归：隐形的跳出条件，就是GetParameters结果为空，targetType拥有无参数构造函数
                    paraList.Add(para);
                }
                return Activator.CreateInstance(type, paraList.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
          
        }
    }
}
