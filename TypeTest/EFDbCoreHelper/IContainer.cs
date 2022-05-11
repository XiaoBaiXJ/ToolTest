using System;

namespace EFDbCoreHelper
{
    /// <summary>
    /// IOC容器接口
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="IEntity">借口</typeparam>
        /// <typeparam name="Entity">实体</typeparam>
        void RegisterType<IEntity, Entity>() where  Entity : IEntity;

        /// <summary>
        /// 取对象
        /// </summary>
        /// <typeparam name="IEntity">对应借口</typeparam>
        /// <returns></returns>
        internal IEntity Resolve<IEntity>();

        /// <summary>
        /// 错误信息
        /// </summary>
        Exception ExceptionMessage { get; }
    }
}
