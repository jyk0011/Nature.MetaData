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
 * function: 记录数据变化情况。添加、修改、删除
 * history:  created by 金洋 2013-09-26 09:08:34 
 *           
 * **********************************************
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using Nature.Common;
using Nature.Data;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.WebPage;
using Nature.MetaData.Enum;
using Nature.MetaData.ManagerMeta;

namespace Nature.MetaData.Manager
{
    /// <summary>
    /// 记录数据的变更情况
    /// </summary>
    public class ManagerLogDataChange
    {

        #region 属性

        #region 要操作的记录的主键字段的值
        /// <summary>
        /// 要操作的记录的主键字段的值
        /// </summary>
        public string DataID { get; set; }
        #endregion

        #region 要操作的数据库的访问实例
        /// <summary>
        /// 要操作的数据库的访问实例的集合
        /// </summary>
        /// user:jyk
        /// time:2012/9/5 17:06
        public DalCollection Dal { get; set; }
        #endregion

        #region 表里的所有字段的元数据
        /// <summary>
        /// 页面视图的元数据
        /// </summary>
        /// user:jyk
        /// time:2012/9/5 17:00
        public PageViewMeta PageViewMeta { set; get; }
        #endregion

        #region 存放表单用的字段的描述信息

        /// <summary>
        /// 存放表单用的字段的描述信息，key：字段ID，value：ColumnsInfo
        /// </summary>
        /// user:jyk
        /// time:2012/9/5 17:00
        public Dictionary<int, IColumn> DictFormColumnMeta { get; set; }

        #endregion

        #region 存放字段值

        /// <summary>
        /// 存放字段值的字典，key：字段ID，value：字段值
        /// </summary>
        /// user:jyk
        /// time:2012/9/5 17:00
       // public Dictionary<int, object> DictColumnsValue { get; set; }

        #endregion

       

        #region 操作数据的方式
        private ButonType _typeOperationData;
        /// <summary>
        /// 页面视图对应的按钮的类型：查看、添加、修改等
        /// </summary>
        public ButonType TypeOperationData
        {
            set { _typeOperationData = value; }
            get { return _typeOperationData; }
        }
        #endregion

        #region 添加人ID
        /// <summary>
        /// 添加人ID
        /// </summary>
        public int AddUserID { get; set; }
        #endregion

        #region 操作日志ID
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public string OperateLogID { get; set; }
        #endregion

        #region 修改前的记录
        /// <summary>
        /// 修改前的记录
        /// </summary>
        public string OldDataJson { get; set; }
        #endregion

        #region 修改后的记录
        /// <summary>
        /// 修改后的记录
        /// </summary>
        public string NewDataJson { get; set; }
        #endregion

        #region 提交的记录
        /// <summary>
        /// 提交的记录
        /// </summary>
        public string SubmitDataJson { get; set; }
        #endregion



        #endregion

        //=============数据变更日志======================================

        #region 获取指定表的数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="debugInfoList"></param>
        /// <returns></returns>
        public string GetDataToJson(IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "save里面添加操作日志" };
          
          
            //根据主键提取记录
            string sql = Functions.IsInt(DataID) ? "select top 1 * from {0} where {1} = {2} " : "select top 1 * from {0} where {1} = '{2}'";

            DataTable dt = Dal.DalCustomer.ExecuteFillDataTable(string.Format(sql, PageViewMeta.ModiflyTableName, PageViewMeta.PKColumn, DataID));

            if (dt.Rows.Count == 0)
                return "没有记录！";

            DataRow dr = dt.Rows[0];

            StringBuilder sb = new StringBuilder(dt.Rows.Count * 500);

            sb.Append("{ ");

            var managerTableColumnMeta = new ManagerTableColumnMeta
                                             {
                                                 DalCollection = Dal,
                                                 PageViewID = PageViewMeta.ModiflyTableID
                                             };

            DictFormColumnMeta = managerTableColumnMeta.GetMetaData(debugInfo.DetailList);

            //遍历元数据，给dic_ColumnsValue赋值——字段值
            foreach (KeyValuePair<int, IColumn> info in DictFormColumnMeta)
            {
                try
                {
                    //dr[colMeta.ColSysName] 因为这个东东可能取不出来值，时间紧迫没想出来更换的方法，先try一下。然后在优化
                    var colMeta = (ColumnMeta)info.Value;
                    var colValue = dr[colMeta.ColSysName];
                    
                    if (colMeta.ColumnKind != 15)
                    {
                        sb.Append("\"");
                        sb.Append(colMeta.ColumnID);
                        sb.Append("\":");

                        if (colMeta.ColSysName == null)
                            Json.ObjectToJson("该字段没有 ColSysName", sb);
                        else
                            Json.ObjectToJson(colValue, sb);

                        sb.Append(",");
                    }
                }
                catch (Exception)
                {
                     
                }
               
            }

            //添加人，添加日期等
            GetColumnsValue(dt, sb);

            sb[sb.Length - 1] = '}';

            return sb.ToString();

        }
        #endregion


        #region 添加数据变更日志
        /// <summary>
        /// 
        /// </summary>
        /// <param name="debugInfoList"></param>
        /// <returns></returns>
        public string WriteDataChangeLog(IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "添加数据变更日志" };
            debugInfoList.Add(debugInfo);

            #region 获取原来的记录，
            
            #endregion


            const string sql = "insert into Manage_Log_DataChange (OperateID,TableID,DataID,OldDataJson,NewDataJson,SubmitDataJson,DataUrl,IP,AddUserid) values (@OperateID,@TableID,@DataID,@OldDataJson,@NewDataJson,@SubmitDataJson,@DataUrl,@IP,@AddUserid) select scope_identity() as newID";

            DataAccessLibrary dal = DalFactory.CreateDal(Dal.DalCustomer.ConnectionString, Dal.DalCustomer.ProviderName);

            if (SubmitDataJson == null ) SubmitDataJson = "";

            if (!Functions.IsInt(OperateLogID))
                OperateLogID = "-2";
            //
            dal.ManagerParameter.AddNewInParameter("OperateID", int.Parse(OperateLogID)); //)
            dal.ManagerParameter.AddNewInParameter("TableID", PageViewMeta.ModiflyTableID);
            dal.ManagerParameter.AddNewInParameter("DataID", int.Parse(DataID));
            dal.ManagerParameter.AddNewInParameter("OldDataJson", OldDataJson);
            dal.ManagerParameter.AddNewInParameter("NewDataJson", NewDataJson);
            dal.ManagerParameter.AddNewInParameter("SubmitDataJson", SubmitDataJson);
            dal.ManagerParameter.AddNewInParameter("DataUrl", HttpContext.Current.Request.Url.ToString(),200);
            dal.ManagerParameter.AddNewInParameter("IP", HttpContext.Current.Request.UserHostAddress,15);
            dal.ManagerParameter.AddNewInParameter("AddUserid", AddUserID);

            OperateLogID = dal.ExecuteString(sql);

            debugInfo.Stop();

            return "";

        }
        #endregion


        #region 判断DataReader 里有哪些规定的字段（WebList1），把包含的字段放到字典里面
        private void GetColumnsValue(DataTable dt, StringBuilder sb)
        {
            string colName = "adduserid";
            if (dt.Columns.Contains(colName))
            {
                sb.Append("\"1000150\":\"");
                sb.Append(dt.Rows[0][colName]);
                sb.Append("\",");
            }

            colName = "addtime";
            if (dt.Columns.Contains(colName))
            {
                sb.Append("\"1000160\":\"");
                sb.Append(dt.Rows[0][colName]);
                sb.Append("\",");
            }

            colName = "disorder";
            if (dt.Columns.Contains(colName))
            {
                sb.Append("\"1000140\":\"");
                sb.Append(dt.Rows[0][colName]);
                sb.Append("\",");
            }

            colName = "isdel";
            if (dt.Columns.Contains(colName))
            {
                sb.Append("\"1000170\":\"");
                sb.Append(dt.Rows[0][colName]);
                sb.Append("\",");
            }
             
        }
        #endregion
    }
}
