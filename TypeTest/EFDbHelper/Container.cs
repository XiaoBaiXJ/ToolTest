using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EFDbHelper
{
    /// <summary>
    /// IOC容器
    /// </summary>
    internal class Container : IContainer
    {
        /// <summary>
        /// 线程保护字典
        /// </summary>
        private ConcurrentDictionary<string, Type> ContainerDictionary = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public Container()
        {

        }

        public Exception ExceptionMessage { get; private set; }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                ExceptionMessage = ex;
                return null;
            }
        }

        /// <summary>
        /// 存对象
        /// </summary>
        /// <typeparam name="IEntity">借口抽象类</typeparam>
        /// <typeparam name="Entity">实体</typeparam>
        void IContainer.RegisterType<IEntity, Entity>()
        {
            ExceptionMessage = null;
            try
            {
                ContainerDictionary[typeof(IEntity).FullName] = typeof(Entity);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
        }

        /// <summary>
        /// 取对象
        /// </summary>
        /// <typeparam name="IEntity">借口</typeparam>
        /// <returns></returns>
        IEntity IContainer.Resolve<IEntity>()
        {
            ExceptionMessage = null;
            try
            {
                if (!ContainerDictionary.ContainsKey(typeof(IEntity).FullName))
                {
                    ExceptionMessage = new Exception("当前关键字不存在于字典中,请先存执");
                    return default;
                }
                var type = ContainerDictionary[typeof(IEntity).FullName];
                var rtsobj = CreateObject(type);
                if (rtsobj is IEntity entity)
                {
                    return entity;
                }
                return default;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
                return default;
            }
        }
    }
}
