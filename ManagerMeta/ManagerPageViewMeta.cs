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
 * function: 单击文本框，选择记录，保存记录ID
 * history:  created by 金洋 2009-12-29 10:18:59
 *           2011-4-11 整理
 * **********************************************
 */
/* ***********************************************
* author :  Administrator
* email  :  jyk0011@live.cn 
* function: 提取模块信息的元数据
* history:  created by Administrator 2009-12-23 10:18:59 
* ***********************************************/


using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using Nature.Common;
using Nature.DebugWatch;
using Nature.MetaData.Entity.WebPage;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 提取页面视图需要的元数据
    /// </summary>
    public class ManagerPageViewMeta : ManagerMeta
    {
        #region 加载页面视图的信息
        /// <summary>
        /// 从数据库里面提取配置信息，填充到 Dictionary 里面。页面视图
        /// </summary>
        /// <param name="debugInfoList">子步骤的列表</param>
        /// <returns></returns>
        protected PageViewMeta LoadPageViewMeta(IList<NatureDebugInfo> debugInfoList)
        {
            //DalCollection.DalMetadata.ManagerTran.TranBegin() ;

            var debugInfo = new NatureDebugInfo { Title = "[Nature.MetaData.ManagerMeta.ManagerPageViewMeta.LoadPageViewMeta]获取页面视图PageViewID:" + PageViewID };
            
            //打开连接
            DalCollection.DalMetadata.ConnectionOpen();

            #region 获取页面视图基础数据
            string sql = "select top 1 *,'' as ForeignColumn,'' as Table_DataSource,'' as Table_Modifly  from  V_Frame_List_PageView where PVID=" + PageViewID;
            DataTable dt = DalCollection.DalMetadata.ExecuteFillDataTable(sql);

            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            debugInfo = new NatureDebugInfo { Title = " 获取外键字段名称ForeignColumn" };

            if (dt == null || dt.Rows.Count == 0)
            {
                //throw new Exception("没有设置功能节点的描述信息。FunctionID：" + base.FunctionID);
                Functions.MsgBox("没有设置页面视图的描述信息。PVID：" + PageViewID, true);
                return null;
            }

            DataRow drView = dt.Rows[0];
             
            #region 根据ForeignColumnID 获取ForeignColumn
            string tmpID = drView["ForeignColumnID"].ToString();
            if (tmpID != "0")
            {
                sql = "select top 1 [ColSysName] from [Manage_Columns] where [ColumnID] = " + tmpID;
                drView["ForeignColumn"] = DalCollection.DalMetadata.ExecuteString(sql);
            }
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            debugInfo = new NatureDebugInfo { Title = " 获取读取数据的表名/视图名Table_DataSource" };

            #region 根据TableID_DataSource 获取Table_DataSource
            tmpID = drView["TableID_DataSource"].ToString();
            if (tmpID != "0")
            {
                sql = "select top 1 [TableName] from [Manage_Table] where [TableID] = " + tmpID;
                drView["Table_DataSource"] = DalCollection.DalMetadata.ExecuteString(sql);
            }
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            debugInfo = new NatureDebugInfo { Title = " 获取修改数据的表名/视图名Table_Modifly" };

            #region 根据TableID_Modifly 获取Table_Modifly
            tmpID = drView["TableID_Modifly"].ToString();
            if (tmpID != "0")
            {
                sql = "select top 1 [TableName] from [Manage_Table] where [TableID] = " + tmpID;
                drView["Table_Modifly"] = DalCollection.DalMetadata.ExecuteString(sql);
            }
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            debugInfo = new NatureDebugInfo { Title = " 获取分页信息到DataTable" };

            #region 获取分页信息
            // pager.OrderColumns, pager.PageSize, pager.QueryAlways, pager.Query, pager.PageTurnTypeID, pager.NaviCount, pager.DisOrder
            sql = "select top 1 *  from  Manage_Pagination where PVID=" + PageViewID;

            DataTable dtPageTurn = DalCollection.DalMetadata.ExecuteFillDataTable(sql);

            //DalCollection.DalMetadata.ManagerTran.TranCommit();

            DalCollection.DalMetadata.ConnectionClose();
             
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            debugInfo = new NatureDebugInfo { Title = " 填充实体类" };

            #region 填充实体类
            PageViewMeta viewMeta = null;
            
            if (dt.Rows.Count > 0)
            {
                DataRow drPageTurn =null;
                if (dtPageTurn.Rows.Count > 0)
                    drPageTurn= dtPageTurn.Rows[0];

                viewMeta = new PageViewMeta(drView, drPageTurn);
            }
           
            debugInfo.Stop();
            debugInfoList.Add(debugInfo);
            #endregion

            return viewMeta;

        }
        #endregion

        #region 功能节点信息的缓存
        /// <summary>
        /// 依据 PageViewID 获取模块需要的元数据
        /// </summary>
        /// <param name="debugInfoList">子步骤的列表</param>
        /// <returns></returns>
        public PageViewMeta GetPageViewMeta(IList<NatureDebugInfo> debugInfoList)
        {
            if (debugInfoList == null)
                debugInfoList = new List<NatureDebugInfo>();

            var debugInfo = new NatureDebugInfo { Title = "暂时不用缓存" };
 
            //模块信息，列表页面、表单页面需要的信息
            string key = "PV_" + PageViewID;

            //暂时不用缓存了
            PageViewMeta pvMeta;// =  LoadPageViewMeta(debugInfo.DetailList);

            //检查Cache里面是否有记录
            //if (HttpContext.Current.Cache[key] == null)
            //{
                debugInfo.Title = "没有缓存，从数据库获取";
                pvMeta = LoadPageViewMeta(debugInfo.DetailList);
                HttpContext.Current.Cache.Insert(key, pvMeta, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));  //CacheItemPriority.Default, 
            //}
           // else
           // {
           //     debugInfo.Title = "有缓存，从cache获取";
           //     pvMeta = (PageViewMeta)HttpContext.Current.Cache[key];
           // }

            debugInfo.Stop();
            debugInfoList.Add(debugInfo);

            return pvMeta;

        }
        #endregion


       

    }
}
