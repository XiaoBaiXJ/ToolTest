using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDbCoreHelper
{
    /// <summary>
    /// 对外访问 不用直接操作IOC 接口或者类 最上层操作
    /// </summary>
    public static class ContainerManager
    {
        /// <summary>
        /// 针对IOC的单例
        /// </summary>
        private static IContainer SingleInstance { get; } = new Container();

        /// <summary>
        /// 错误日志信息
        /// </summary>
        public static Exception ExceptionMessage => SingleInstance.ExceptionMessage;

        /// <summary>
        /// 根据委托去对象  多个可以一次性创建  注册
        /// </summary>
        public static void RegisterType(Action<IContainer> action)
        {
            action?.Invoke(SingleInstance);
        }

        /// <summary>
        /// 只注册一个对象
        /// </summary>
        public static void RegisterType<IEntity, Entity>() where Entity:IEntity
        {
            SingleInstance.RegisterType<IEntity, Entity>();
        }

        /// <summary>
        /// 取对象  根据对应的接口 读取到对应的实体
        /// </summary>
        /// <typeparam name="IEntity">接口</typeparam>
        /// <returns></returns>
        public static IEntity Resolve<IEntity>()
        {
            return SingleInstance.Resolve<IEntity>();
            //return func.Invoke(SingleInstance);
        }
    }
}
