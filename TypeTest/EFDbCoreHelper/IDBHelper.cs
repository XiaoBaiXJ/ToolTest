using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFDbCoreHelper
{
    /// <summary>
    /// 数据库操作帮助接口
    /// </summary>
    public interface IDBHelper<T>: IDisposable where T : DbContext, new()
    {
        /// <summary>
        /// 数据库连接具体信息
        /// </summary>
        public T DbContextModel{ get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public Exception ExceptionMessage { get; }

        /// <summary>
        /// 创建一个 数据库连接对象 DbContext
        /// </summary>
        /// <returns></returns>
        //public T CreateDbContext();

        /// <summary>
        /// 能否连接数据库
        /// </summary>
        bool IsCanConnection { get; }

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        string NameOrConnectionString { get; }

        /// <summary>
        /// 数据库连接类型 多种数据库都可以
        /// </summary>
        DbConnection DbConnection { get; }

        /// <summary>
        /// 根据表id 获取一个表实体
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="whereLambda">主键查询</param>
        /// <returns>表实体</returns>
        TEntity GetT<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : class;

        /// <summary>
        /// 根据表id 获取一个表实体
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <typeparam name="TType">主键类型</typeparam>
        /// <returns>表实体</returns>
        TEntity GetT<TEntity,TType>(TType id) where TEntity : class;

        /// <summary>
        /// 获取一个DbSet对象 方便直接从外部得到数据
        /// </summary>
        /// <typeparam name="TEntity">表对象</typeparam>
        /// <returns>表实体 DbSet对象</returns>
        DbSet<TEntity> GetT<TEntity>() where TEntity : class;

        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="t">表对象</param>
        /// <returns></returns>
        bool Add<TEntity>(TEntity t) where TEntity : class;

        /// <summary>
        /// 创建一条集合数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="t">集合对象</param>
        /// <returns>创建是否成功</returns>
        bool Add<TEntity>(List<TEntity> t) where TEntity : class;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity">删除的表</typeparam>
        /// <param name="entity">待删除对象</param>
        /// <returns>删除是否成功</returns>
        bool Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>删除是否成功</returns>
        bool Delete<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : class;

        /// <summary>
        /// 删除 集合中所有数据 
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="list">主键集合</param>
        /// <returns>删除是否成功</returns>
        bool Delete<TEntity>(IEnumerable<TEntity> list) where TEntity : class;

        /// <summary>
        /// 更新表数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="whereLambda">需要更新的对象</param>
        /// <param name="t">待更新的对象</param>
        /// <returns>更新是否成功</returns>
        bool Update<TEntity>(Expression<Func<TEntity, bool>> whereLambda, TEntity t) where TEntity : class;

        /// <summary>
        /// 更新表数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="t">待更新的对象</param>
        /// <returns>更新是否成功</returns>
        bool Update<TEntity>(TEntity t) where TEntity : class;

        /// <summary>
        /// 获取表中所有数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <returns>得到一个具体List表对象</returns>
        List<TEntity> GetAllTs<TEntity>() where TEntity : class;

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="whereLambda">条件集合</param>
        /// <returns>表集合对象</returns>
        List<TEntity> GetAllTs<TEntity>(List<Expression<Func<TEntity, bool>>> whereLambda) where TEntity : class;


        /// <summary>
        /// 根据查询你条件获取参数 并且带有排序
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <typeparam name="OrdeyKey">排序字段</typeparam>
        /// <param name="whereLambda">查询条件集合</param>
        /// <param name="ordeyLambda">排序字段 true 是升序 false 是降序</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns><表集合对象/returns>
        List<TEntity> GetAllTs<TEntity, OrdeyKey>(List<Expression<Func<TEntity, bool>>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class;

        /// <summary>
        /// 根据查询你条件获取参数 并且带有排序 分页
        /// </summary>
        /// <typeparam name="TEntity">数据库表内容</typeparam>
        /// <typeparam name="OrdeyKey">排序标识</typeparam>
        /// <param name="pageIndex">当前页缩影</param>
        /// <param name="pagesize">当前页容量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段 true 是升序 false 是降序</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns>表集合对象</returns>
        List<TEntity> GetPageListCustom<TEntity, OrdeyKey>(int pageIndex, int pagesize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class;

        /// <summary>
        /// 根据查询你条件获取参数 并且带有排序 分页 多种条件
        /// </summary>
        /// <typeparam name="TEntity">数据库表内容</typeparam>
        /// <typeparam name="OrdeyKey">排序标识</typeparam>
        /// <param name="pageIndex">当前页缩影</param>
        /// <param name="pagesize">当前页容量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段 true 是升序 false 是降序</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns>表集合对象</returns>
        List<TEntity> GetPageListCustom<TEntity, OrdeyKey>(int pageIndex, int pagesize, List<Expression<Func<TEntity, bool>>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class;

        /// <summary>
        /// IPagedList 采用 IPagedList 插件来做分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pagesize">当前页显示行数</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序 true 是升序 false 是降序</param>
        /// <returns>分页集合对象</returns>
        IPagedList<TEntity> GetPageList<TEntity, OrdeyKey>(int pageIndex, int pagesize, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class;

        /// <summary>
        /// IPagedList 采用 IPagedList 插件来做分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pagesize">当前页显示行数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序 true 是升序 false 是降序</param>
        /// <returns>分业绩和对象</returns>
        IPagedList<TEntity> GetPageList<TEntity, OrdeyKey>(int pageIndex, int pagesize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class;

        /// <summary>
        /// IPagedList 采用 IPagedList 插件来做分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pagesize">当前页显示行数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序 true 是升序 false 是降序</param>
        /// <returns>分页绩和对象</returns>
        IPagedList<TEntity> GetPageList<TEntity, OrdeyKey>(int pageIndex, int pagesize, List<Expression<Func<TEntity, bool>>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class;

        Task<bool> UpdateModel<TEntity>(TEntity t);
    }
}
