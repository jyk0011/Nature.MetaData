/**
 * 自然框架之元数据
 * http://www.natureFW.com/
 *
 * @author
 * 金洋（金色海洋jyk）
 * 
 * @copyright
 * Copyright (C) 2005-2013 金洋.
 *
 * Licensed under a GNU Lesser General Public License.
 * http://creativecommons.org/licenses/LGPL/2.1/
 *
 * 自然框架之元数据 is free software. You are allowed to download, modify and distribute 
 * the source code in accordance with LGPL 2.1 license, however if you want to use 
 * 自然框架之元数据 on your site or include it in your commercial software, you must  be registered.
 * http://www.natureFW.com/registered
 */

/* ***********************************************
 * author :  金洋（金色海洋jyk）
 * email  :  jyk0011@live.cn  
 * function: 处理Dictionary相关的事情
 * history:  created by 金洋 2009-8-27 9:02:51 
 *           2011-4-11 整理
 * **********************************************
 */


using System.Collections.Generic;
using System.Data;
using Nature.Common;
using Nature.Data;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.MetaControl;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 处理Dictionary相关的事情
    /// 设置公用的属性
    /// 处理和数据库相关的操作
    /// 不能直接实例化
    /// </summary>
    public abstract class ManagerMeta
    {
        #region 属性

        #region 访问元数据的数据库

        /// <summary>
        /// 设置数据访问层的实例
        /// 访问元数据的数据库
        /// </summary>
        public DalCollection DalCollection { set; get; }

        #endregion

        #region 模块ID

        /// <summary>
        /// 模块视图ID 
        /// </summary>
        public int ModuleID { get; set; }

        #endregion

        #region 页面视图ID

        /// <summary>
        /// 页面视图ID 
        /// </summary>
        public int PageViewID { get; set; }

        #endregion

        #region 设置权限，当前用户对于指定的节点可以使用的字段ID

        private string _roleColumnID;

        /// <summary>
        /// 设置权限，当前用户对于指定的节点可以使用的字段ID
        /// </summary>
        public string RoleColumnID
        {
            set { _roleColumnID = value; }
            get
            {
                if (_roleColumnID == null)
                    return "";
                return _roleColumnID;
            }
        }

        #endregion

        #endregion

        #region 钩子

        /// <summary>
        /// 返回提取数据用的SQL
        /// </summary>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/9/15 10:32
        protected virtual string GetSql()
        {
            return "";
        }
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="dr">DataRow，数据库里提取出来的数据</param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/9/15 10:32
        protected virtual IColumn CreatNewEntity(DataRow dr)
        {
            return null;
        }
        #endregion

        #region 函数

        #region 从配置信息里面加载表单控件和查询控件需要的信息，填充到 Dictionary 里面。

        /// <summary>
        /// 从配置信息里面加载表单控件和查询控件需要的信息，填充到 Dictionary 里面。
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<int, IColumn> LoadMetaData(IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "[Nature.MetaData.ManagerMeta.ManagerMeta.LoadMetaData]从数据库加载，提取SQL"};
          
            //提取
            string sql = GetSql();

            debugInfo.Stop();
            debugInfoList.Add(debugInfo);

            debugInfo = new NatureDebugInfo { Title = "获取数据" };
            #region 获取数据
            DataTable dt = DalCollection.DalMetadata.ExecuteFillDataTable(sql);
          
            if (DalCollection.DalMetadata.ErrorMessage.Length > 2)
            {
                //throw new Exception("没有设置功能节点的描述信息。FunctionID：" + base.FunctionID);
                Functions.MsgBox("没有设置"+GetType()+"的描述信息。PVID：" + PageViewID, true);
                debugInfo.Stop();
                debugInfoList.Add(debugInfo);
                
                return null;
            }
            
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            if (dt.Rows.Count == 0)
                return null;

            debugInfo = new NatureDebugInfo { Title = "生成记录集实例" };
        
            var dicBaseCols = new Dictionary<int, IColumn>();
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);

            debugInfo = new NatureDebugInfo { Title = "遍历记录集，把字段的配置信息加载到字典里面" };
          
            foreach (DataRow dr in dt.Rows)
            {
                var entity = CreatNewEntity(dr); // new ModuleEntity(dr);
                //遍历记录集，把字段的配置信息加载到字典里面。
                dicBaseCols.Add(entity.Key, entity);
            }

            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
         
            return dicBaseCols;
        }

        #endregion

        #region 获取表单控件需要的字段的描述信息

        /// <summary>
        /// 获取表单需要的字段的描述信息，打算启用缓存
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, IColumn> GetMetaData(IList<NatureDebugInfo> debugInfoList)
        {
            if (debugInfoList == null)
                debugInfoList = new List<NatureDebugInfo>();

            var debugInfo = new NatureDebugInfo { Title = "[Nature.MetaData.ManagerMeta.ManagerMeta.GetMetaData]获取页面视图PageViewID:" + PageViewID };
          
            //模块信息，列表页面、表单页面需要的信息
            //string key = GetDictionaryKey();
            //string keyID = key + PageViewID;

            //加载
            Dictionary<int, IColumn> dicColumnMeta = LoadMetaData(debugInfo.DetailList);

            ////检查Cache里面是否有记录
            //if (HttpContext.Current.Cache[keyID] == null)
            //{
            //    //加载
            //    info = LoadMetaData();
            //    //20分钟后失效（平滑失效）
            //    HttpContext.Current.Cache.Insert(keyID, info, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));  //CacheItemPriority.Default, 
            //}
            //else
            //{
            //    info = (Dictionary<int, ColumnsInfo>)HttpContext.Current.Cache[keyID];
            //}

            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
          
            return dicColumnMeta;

        }

        #endregion

        #region 创建字段值的容器

        /// <summary>
        /// 创建字段值的容器
        /// </summary>
        /// <param name="dicBaseCols">字段描述信息，表单里需要的字段的字典</param>
        /// <returns></returns>
        public Dictionary<int, object> CreateDicColumnValue(Dictionary<int, IColumn> dicBaseCols)
        {
            var dicValue = new Dictionary<int, object>();

            foreach (KeyValuePair<int, IColumn> info in dicBaseCols)
            {
                var bInfo = (FormColumnMeta) info.Value;
                dicValue.Add(bInfo.ColumnID, null);
            }
            return dicValue;

        }

        #endregion

        #region 取两个数组的交集

        /// <summary>
        /// 以arr1为基准，在arr2里面查找是否有对应记录，有则保留，没有则去掉。
        /// 返回交集，并且保留arr1的存放顺序。
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public int[] ArrorJiaoJi(int[] arr1, int[] arr2)
        {
            var tmpArr = new int[arr1.Length];
            int index = 0;

            for (int a = 0; a < arr1.Length; a++)
            {
                for (int b = 0; b < arr2.Length; b++)
                {
                    if (arr1[a] == arr2[b])
                    {
                        //有相同的，填充tmpArr，并且退出本循环
                        tmpArr[index] = arr1[a];
                        index++;
                        break;
                    }
                }
            }

            var re = new int[index];

            for (int a = 0; a < index; a++)
            {
                re[a] = tmpArr[a];
            }

            return re;

        }

        #endregion

        #endregion

        #region 静态函数 清空Cache缓存

        /// <summary>
        /// 清空全部的Cache
        /// </summary>
        public static void ClearCache()
        {
            //遍历Cache
            //IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            //while (CacheEnum.MoveNext())
            //{
            //   Cache.Remove(CacheEnum.Key.ToString());
            //}

            //遍历Cache
            //System.Collections.IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            //while (cacheEnum.MoveNext())
            //{
            //    HttpRuntime.Cache.Remove(cacheEnum.Key.ToString());
            //}
        }

        #endregion

    }
}
