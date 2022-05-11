using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace EFDbHelper
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public class DBHelper<T>: IDBHelper<T> where T :DbContext,new ()
    {
        /// <summary>
        /// 析构函数
        /// </summary>
        //~DBHelper()
        //{
        //    this.Dispose();
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        public DBHelper()
        {

        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public Exception ExceptionMessage { get; private set; }

        //private string nameOrConnectionString = dbConnection?.ConnectionString;

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        public string NameOrConnectionString => DbConnection?.ConnectionString;

        /// <summary>
        /// 数据库连接类型 多种数据库都可以
        /// </summary>
        public DbConnection DbConnection { get; private set; }

        /// <summary>
        /// 能否连接数据
        /// </summary>
        public bool IsCanConnection { get; private set; }


        private T dbContextmodel = new T();

        /// <summary>
        /// 数据库连接具体信息
        /// </summary>
        public T DbContextModel => CreateDbContext();

        /// <summary>
        /// 创建一个 数据库连接对象 DbContext
        /// </summary>
        /// <typeparam name="T">数据库连接对象</typeparam>
        /// <returns></returns>
        public T CreateDbContext()
        {
            try
            {
                ///此处检测 当前 数据库连接对象是否被 Dispose掉，如果被Dispose掉则重新New一个新的
                DbConnection = dbContextmodel.Database.Connection;
            }
            catch (Exception ex)
            {
                dbContextmodel = new T();
                ExceptionMessage = ex;
            }
            IsCanConnection = true;
            return dbContextmodel;
        }

        /// <summary>
        /// 获取对应的连接数据实体 操作数据库方法
        /// </summary>
        /// <returns></returns>
        //private bool GetDbContext(out DbContext dbcon)
        //{
        //    if (!string.IsNullOrEmpty(NameOrConnectionString))
        //    {
        //        dbcon = new DbContext(NameOrConnectionString);
        //        return true;
        //    }
        //    else if (DbConnection != null)
        //    {
        //        dbcon = new DbContext(DbConnection, false);
        //        return true;
        //    }
        //    isCanConnection = false;
        //    dbcon = null;
        //    return false;
        //}

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        //public bool ConnectDbTest(string nameOrConnectionString)
        //{
        //    isCanConnection = false;
        //    if (string.IsNullOrEmpty(nameOrConnectionString))
        //    {
        //        return false;
        //    }
        //    this.nameOrConnectionString = nameOrConnectionString;
        //    if (!GetDbContext(out DbContext dbcon))
        //    {
        //        this.nameOrConnectionString = string.Empty;
        //        return false;
        //    }
        //    try
        //    {
        //        dbcon.Database.Connection.Open();
        //    }
        //    catch (Exception)
        //    {
        //        this.nameOrConnectionString = string.Empty;
        //        return false;
        //    }
        //    switch (dbcon.Database.Connection.State)
        //    {
        //        case ConnectionState.Closed:
        //        case ConnectionState.Executing:
        //        case ConnectionState.Fetching:
        //        case ConnectionState.Broken:
        //            this.nameOrConnectionString = string.Empty;
        //            return false;
        //        default:
        //            isCanConnection = true;
        //            dbcon.Database.Connection.Close();
        //            return true;
        //    }
        //}

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        //public bool ConnectDbTest(DbConnection dbConnection)
        //{
        //    isCanConnection = false;
        //    if (dbConnection is null)
        //    {
        //        return false;
        //    }
        //    this.dbConnection = dbConnection;
        //    try
        //    {
        //        dbConnection.Open();
        //    }
        //    catch (Exception)
        //    {
        //        dbConnection = null;
        //        return false;
        //    }
        //    switch (dbConnection.State)
        //    {
        //        case ConnectionState.Closed:
        //        case ConnectionState.Executing:
        //        case ConnectionState.Fetching:
        //        case ConnectionState.Broken:
        //            this.dbConnection = dbConnection;
        //            return false;
        //        default:
        //            dbConnection.Close();
        //            isCanConnection = true;
        //            return true;
        //    }
        //}

        /// <summary>
        /// 根据id 主键查询
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public TEntity GetT<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : class
        {
            ExceptionMessage = null ;
            try
            {
                var queryable = DbContextModel.Set<TEntity>().AsQueryable();
                var rtslist= queryable.SingleOrDefault(whereLambda);
                DbContextModel.Dispose();
                return rtslist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel?.Dispose();
            return null;
        }

        /// <summary>
        /// 根据表id 获取一个表实体 主键
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <typeparam name="TType">主键类型</typeparam>
        /// <returns></returns>
        public TEntity GetT<TEntity, TType>(TType id) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                var dellist = DbContextModel.Set<TEntity>().Find(id);
                DbContextModel.Dispose();
                return dellist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel?.Dispose();
            return null;
        }

        /// <summary>
        /// 获取一个DbSet对象 方便直接从外部得到数据
        /// </summary>
        /// <typeparam name="TEntity">表对象</typeparam>
        /// <returns>数据库表对象</returns>
        public DbSet<TEntity> GetT<TEntity>() where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                var rtsentity = DbContextModel.Set<TEntity>();
                return rtsentity;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// 创建一条集合数据
        /// </summary>
        /// <typeparam name="TEntity">数据库表对象</typeparam>
        /// <param name="t">集合对象</param>
        /// <returns></returns>
        public bool Add<TEntity>(List<TEntity> t) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                DbContextModel.Set<TEntity>().AddRange(t);
                DbContextModel.SaveChanges();
                DbContextModel.Dispose();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel?.Dispose();
            return false;
        }

        /// <summary>
        /// 创建实体 增加  泛型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add<TEntity>(TEntity t) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                DbContextModel.Set<TEntity>().Add(t);
                DbContextModel.SaveChanges();
                DbContextModel.Dispose();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel?.Dispose();
            return false;
        }

        /// <summary>
        /// 删除全部信息公用方法 传入集合 泛型
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Delete<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                var dellist = DbContextModel.Set<TEntity>().AsQueryable();
                var rtslist = dellist.Where(whereLambda);
                foreach (var item in rtslist)
                {
                    DbContextModel.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Deleted;
                }
               
                DbContextModel.SaveChanges();
                DbContextModel.Dispose();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel?.Dispose();
            return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity">删除的表</typeparam>
        /// <param name="entity">需要删除的对象</param>
        /// <returns></returns>
        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                DbContextModel.Set<TEntity>().Attach(entity);
                //DbContextModel.Set<TEntity>().Remove(entity);
                //DbContextModel.Entry(entity).State= EntityState.Deleted;
                DbContextModel.Entry(entity).State = EntityState.Deleted;
                DbContextModel.SaveChanges();
                DbContextModel.Dispose();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel?.Dispose();
            return false;
        }
        /// <summary>
        /// 删除一个集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Delete<TEntity>(IEnumerable<TEntity> list) where TEntity : class
        {
            try
            {
                using (var tarn = DbContextModel.Database.BeginTransaction())
                {
                    DbContextModel.Set<TEntity>().RemoveRange(list);
                    //foreach (var item in list)
                    //{
                    //    DbContextModel.Entry(item).State= EntityState.Deleted;
                    //}
                    tarn.Commit();
                    DbContextModel.SaveChanges();
                    DbContextModel.Dispose();
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            return false;
        }

        /// <summary>
        /// 更新实体 泛型 数据库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="t">更新对象</param>
        /// <returns></returns>
        public bool Update<TEntity>(Expression<Func<TEntity, bool>> whereLambda, TEntity t) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                var model = DbContextModel.Set<TEntity>().AsQueryable().SingleOrDefault(whereLambda);
                if (model is null)
                    return false;
                DbContextModel.Entry(model).CurrentValues.SetValues(t);
                DbContextModel.Entry(model).State = System.Data.Entity.EntityState.Modified;
                DbContextModel.SaveChanges();
                DbContextModel.Dispose();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return false;
        }

        /// <summary>
        /// 获取全部值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public List<TEntity> GetAllTs<TEntity>() where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                List<TEntity> rtslist = DbContextModel.Set<TEntity>().AsEnumerable().ToList();
                dbContextmodel.Dispose();
                //DbConnection comm = new NpgsqlConnection("Host=192.168.17.124;Database=gnss;Username=postgres;Password=123");
                return rtslist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            dbContextmodel.Dispose();
            return null;
        }

        /// <summary>
        /// 根据条件获取集合对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public List<TEntity> GetAllTs<TEntity>(List<Expression<Func<TEntity, bool>>> whereLambda) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                foreach (var item in whereLambda)
                {
                    dataWhere = dataWhere.Where(item);
                }
                var rtslist = dataWhere.ToList();
                DbContextModel.Dispose();
                return rtslist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
                //throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// 更具体案件获取值集合  并且排序
        /// </summary>
        /// <typeparam name="TEntity">表</typeparam>
        /// <typeparam name="OrdeyKey">排序键</typeparam>
        /// <param name="whereLambda">条件集合</param>
        /// <param name="ordeyLambda">排序</param>
        /// <param name="isAsc">是否升序还是降序</param>
        /// <returns></returns>
        public List<TEntity> GetAllTs<TEntity, OrdeyKey>(List<Expression<Func<TEntity, bool>>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                foreach (var item in whereLambda)
                {
                    dataWhere = dataWhere.Where(item);
                }
                if (isAsc)
                    dataWhere = dataWhere.OrderBy(ordeyLambda);
                else
                    dataWhere = dataWhere.OrderByDescending(ordeyLambda);
                var rtslist = dataWhere.ToList();
                DbContextModel.Dispose();
                return rtslist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity t) where TEntity : class
        {
            ExceptionMessage = null;
            try
            {
                DbContextModel.Entry(t).State = EntityState.Modified;
                DbContextModel.SaveChanges();
                DbContextModel.Dispose();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return false;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// 根据查询你条件获取参数 并且带有排序 分页
        /// </summary>
        /// <typeparam name="TEntity">数据库表内容</typeparam>
        /// <typeparam name="OrdeyKey">排序标识</typeparam>
        /// <param name="pageIndex">当前页索引 不能小于1</param>
        /// <param name="pagesize">当前页容量 不能小于1</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns></returns>
        public List<TEntity> GetPageListCustom<TEntity, OrdeyKey>(int pageIndex, int pagesize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class
        {
            ExceptionMessage = null;
            if (pageIndex < 1 || pagesize < 1)
            {
                ExceptionMessage = new Exception("pageIndex和pagesize------不能小于1");
                return null;
            }
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                dataWhere = dataWhere.Where(whereLambda);
                if (isAsc)
                    dataWhere = dataWhere.OrderBy(ordeyLambda);
                else
                    dataWhere = dataWhere.OrderByDescending(ordeyLambda);
                var rtslist = dataWhere.Skip((pageIndex-1) * pagesize).Take(pagesize).ToList();
                DbContextModel.Dispose();
                return rtslist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// 根据查询你条件获取参数 并且带有排序 分页 多种条件
        /// </summary>
        /// <typeparam name="TEntity">数据库表内容</typeparam>
        /// <typeparam name="OrdeyKey">排序标识</typeparam>
        /// <param name="pageIndex">当前页索引 不能小于1</param>
        /// <param name="pagesize">当前页容量 不能小于1</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns></returns>
        public List<TEntity> GetPageListCustom<TEntity, OrdeyKey>(int pageIndex, int pagesize, List<Expression<Func<TEntity, bool>>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class
        {
            ExceptionMessage = null;
            if (pageIndex < 1 || pagesize < 1)
            {
                ExceptionMessage = new Exception("pageIndex和pagesize------不能小于1");
                return null;
            }
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                foreach (var item in whereLambda)
                {
                    dataWhere = dataWhere.Where(item);
                }
                if (isAsc)
                    dataWhere = dataWhere.OrderBy(ordeyLambda);
                else
                    dataWhere = dataWhere.OrderByDescending(ordeyLambda);
                var rtslist = dataWhere.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
                DbContextModel.Dispose();
                return rtslist;
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// IPagedList 采用 IPagedList 插件来做分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pagesize">当前页显示行数</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns></returns>
        public IPagedList<TEntity> GetPageList<TEntity, OrdeyKey>(int pageIndex, int pagesize, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc = true) where TEntity : class
        {
            ExceptionMessage = null;
            if (pageIndex < 1 || pagesize < 1)
            {
                ExceptionMessage = new Exception( "pageIndex和pagesize------不能小于1");
                return null;
            }
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                if (isAsc)
                    dataWhere = dataWhere.OrderBy(ordeyLambda);
                else
                    dataWhere = dataWhere.OrderByDescending(ordeyLambda);
                return dataWhere.ToPagedList(pageIndex, pagesize);
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// IPagedList 采用 IPagedList 插件来做分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pagesize">当前页显示行数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns></returns>
        IPagedList<TEntity> IDBHelper<T>.GetPageList<TEntity, OrdeyKey>(int pageIndex, int pagesize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc)
        {
            ExceptionMessage = null;
            if (pageIndex < 1 || pagesize < 1)
            {
                ExceptionMessage = new Exception("pageIndex和pagesize------不能小于1");
                return null;
            }
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                dataWhere = dataWhere.Where(whereLambda);
                if (isAsc)
                    dataWhere = dataWhere.OrderBy(ordeyLambda);
                else
                    dataWhere = dataWhere.OrderByDescending(ordeyLambda);
                return dataWhere.ToPagedList(pageIndex, pagesize);
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }

        /// <summary>
        /// IPagedList 采用 IPagedList 插件来做分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pagesize">当前页显示行数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="ordeyLambda">排序字段</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <returns></returns>
        IPagedList<TEntity> IDBHelper<T>.GetPageList<TEntity, OrdeyKey>(int pageIndex, int pagesize, List<Expression<Func<TEntity, bool>>> whereLambda, Expression<Func<TEntity, OrdeyKey>> ordeyLambda, bool isAsc)
        {
            ExceptionMessage = null;
            if (pageIndex < 1 || pagesize < 1)
            {
                ExceptionMessage = new Exception("pageIndex和pagesize------不能小于1");
                return null;
            }
            try
            {
                var dataWhere = DbContextModel.Set<TEntity>().AsQueryable();
                foreach (var item in whereLambda)
                {
                    dataWhere = dataWhere.Where(item);
                }
                if (isAsc)
                    dataWhere = dataWhere.OrderBy(ordeyLambda);
                else
                    dataWhere = dataWhere.OrderByDescending(ordeyLambda);
                return dataWhere.ToPagedList(pageIndex, pagesize);
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionMessage = ex;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex;
            }
            DbContextModel.Dispose();
            return null;
        }



    }
}