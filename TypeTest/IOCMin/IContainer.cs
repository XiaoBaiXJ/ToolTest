using System;

namespace IOCMin
{
    public interface IContainer
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="Entity"></typeparam>
        void RegisterType<IEntity, Entity>();

        /// <summary>
        /// 取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEntity Resolve<IEntity>();

        /// <summary>
        /// 注册类型 采用委托的方式
        /// </summary>
        /// <param name="action"></param>
        void RegisterType(Action<IContainer> action);
    }


}
