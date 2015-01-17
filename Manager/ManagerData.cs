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
 * function: 把字典里的数据保存到数据库里面。从数据库里提取记录，填充到字典里面。
 * history:  created by 金洋 2010-1-6 16:56:34 
 *           2011-4-11 整理
 * **********************************************
 */
/* ***********************************************
* author :  Administrator
* email  :  jyk0011@live.cn 
* function: 
* history:  created by Administrator 
 *          2012-9-14 整理
* ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nature.Common;
using Nature.Data;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.WebPage;
using Nature.MetaData.Enum;

namespace Nature.MetaData.Manager
{
    /// <summary>
    /// 实现添加、修改、提取数据的功能
    /// </summary>
    public class ManagerData 
    {
        #region 属性

        #region 要操作的记录的主键字段的值
        /// <summary>
        /// 要操作的记录的主键字段的值
        /// </summary>
        public ManagerLogDataChange ManagerLogDataChange { get; set; }
        #endregion

        #region 要操作的记录的主键字段的值
        /// <summary>
        /// 要操作的记录的主键字段的值
        /// </summary>
        public string DataID { get; set; }
        #endregion

        #region 要操作的数据库的访问实例
        /// <value>
        /// 要操作的数据库的访问实例
        /// </value>
        /// user:jyk
        /// time:2012/9/5 17:06
        public DataAccessLibrary Dal { get; set; }
        #endregion

        #region 页面视图的元数据
        /// <summary>
        /// 页面视图的元数据
        /// </summary>
        /// <value>
        /// 页面视图的元数据
        /// </value>
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
        public Dictionary<int, object> DictColumnsValue { get; set; }

        #endregion

        #region 保存数据的方式

        //private SQLType _typeSaveData;
        ///// <summary>
        ///// 保存数据的方式，参数化SQL语句，SQL语句，存储过程
        ///// </summary>
        //public SQLType TypeSaveData
        //{
        //    set { _typeSaveData = value; }
        //    get { return _typeSaveData; }
        //}
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

        #endregion

        //=============保存数据======================================
        #region 保存数据
        /// <summary>
        /// 保存数据。如果保存成功则返回空字符串，如果不成功，返回说明信息。
        /// 如果是添加数据，成功的话，可以使用 DataID 获得新纪录的主键值（限于SQL数据库、自增ID）
        /// </summary>
        /// <returns></returns>
        public string SaveData(ManagerLogOperate operateLog ,IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "save里面添加操作日志" };
            debugInfoList.Add(debugInfo);

            switch (PageViewMeta.SQLType)
            {
                case SQLType.SQL:    //SQL语句的方式
                    return SaveDataBySql(false,operateLog, debugInfo.DetailList);

                case SQLType.ParameterSQL:   //参数化SQL语句的方式
                    return SaveDataBySql(true,operateLog, debugInfo.DetailList);

                case SQLType.StoredProcedure:        //存储过程的方式
                    return SaveDataByStoreProc(operateLog, debugInfo.DetailList);
            }

            return "";

        }
        #endregion

        #region 用参数化的SQL语句方式实现添加、修改的功能
        private string SaveDataBySql(bool isUseParameter,ManagerLogOperate operateLog, IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "用参数化的SQL语句方式实现添加、修改的功能" };
            debugInfoList.Add(debugInfo);

            debugInfo.Remark = "";

            string sql;

            //清空command的参数
            Dal.ManagerParameter.ClearParameter();

            var sqlFac = new SqlFactory
                             {
                                 Dal = Dal,
                                 FormColumnMeta = DictFormColumnMeta,
                                 ColumnsValue = DictColumnsValue
                             };

            switch (_typeOperationData)
            {
                case ButonType.AddData: //添加数据

                    #region 添加数据

                    sql = sqlFac.CreateInsertSql(PageViewMeta.ModiflyTableName, isUseParameter);

                    //判断驱动类型
                    if (Dal.ProviderName == "System.Data.SqlClient")
                        sql += "  select scope_identity() as a1";

                    debugInfo.Remark += "<br>sql:" + sql;
                    //添加记录
                    string newID = Dal.ExecuteString(sql);
                    if (Dal.ErrorMessage.Length > 0)
                    {
                        debugInfo.Remark += "<br>添加数据时出现异常：" + Dal.ErrorMessage;
                    
                        operateLog.UpdateOperateLogState(4, debugInfo.DetailList);
                        //有异常，
                        return "添加数据时出现异常！";
                    }

                    
                    if (newID.Length > 0)
                    {
                        debugInfo.Remark += "<br>自增字段，记录新记录值：" + newID;
                    
                        //自增字段，记录新记录值
                        sqlFac.DataID = newID;
                        DataID = newID;
                        ManagerLogDataChange.DataID = newID;
                    }
                    else
                    {
                        if (ManagerLogDataChange != null)
                        {
                            //没有自增字段，看看是否提交了主键字段值
                            if (string.IsNullOrEmpty(ManagerLogDataChange.DataID))
                            {
                                sqlFac.DataID = ManagerLogDataChange.DataID;
                                DataID = ManagerLogDataChange.DataID;
                            }
                        }
                    }

                    #endregion

                    //添加，没有原记录
                    ManagerLogDataChange.OldDataJson = "";
                    //获取添加的记录
                    ManagerLogDataChange.NewDataJson = ManagerLogDataChange.GetDataToJson(debugInfo.DetailList);
                    
                    //拼接提交的数据
                    ManagerLogDataChange.SubmitDataJson = sqlFac.CreateSubmitValueToJson();

                    //记录日志
                    if (string.IsNullOrEmpty(ManagerLogDataChange.DataID))
                        ManagerLogDataChange.DataID = "0";

                    ManagerLogDataChange.WriteDataChangeLog(debugInfo.DetailList);

                    break;

                case ButonType.UpdateData:
                case ButonType.AddUpdateData:

                    //获取修改前记录
                    ManagerLogDataChange.OldDataJson = ManagerLogDataChange.GetDataToJson(debugInfo.DetailList);
 
                    #region 修改数据，启用事务的方式修改

                    //修改表的记录
                    sql = sqlFac.CreateUpdateSql(PageViewMeta.ModiflyTableName, isUseParameter, PageViewMeta.PKColumn,DataID);
                    Dal.ExecuteNonQuery(sql);
                    if (Dal.ErrorMessage.Length > 0)
                    {
                        operateLog.UpdateOperateLogState(5, debugInfo.DetailList);
                        //有异常，自动回滚事务，退出循环
                        return "修改数据时出现异常！";
                    }

                    #endregion

                    //获取修改后记录
                    ManagerLogDataChange.NewDataJson = ManagerLogDataChange.GetDataToJson(debugInfo.DetailList);

                    //拼接提交的数据
                    ManagerLogDataChange.SubmitDataJson = sqlFac.CreateSubmitValueToJson();

                    //记录日志
                    ManagerLogDataChange.WriteDataChangeLog(debugInfo.DetailList);

                    break;

            }
            return "";
        }

        #endregion

        #region 使用存储过程的方式实现添加、修改
        private string SaveDataByStoreProc(ManagerLogOperate operateLog, IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "使用存储过程的方式实现添加、修改" };
            debugInfoList.Add(debugInfo);
            
            string spName = PageViewMeta.ModiflyTableName;
            debugInfo.Remark = "spName:" + spName + "<br>";

            //添加特定参数，新纪录ID、信息反馈（出错的描述信息）
            //添加字段的参数
            //执行存储过程。

            //清除存储过程的参数
            Dal.ManagerParameter.ClearParameter();

            switch (_typeOperationData)
            {
                case ButonType.AddData:    //添加数据
                    break;

                case ButonType.UpdateData:
                case ButonType.AddUpdateData:
                    //修改数据
                    //设置主键字段参数，用于修改数据
                    //Dal.ManagerParameter.AddNewInParameter("DataID", DataID);
                    Dal.ManagerParameter.AddNewInParameter("ID", DataID);
                    break;

            }

            var sqlFac = new SqlFactory
            {
                Dal = Dal,
                FormColumnMeta = DictFormColumnMeta,
                ColumnsValue = DictColumnsValue
            };

            //添加字段参数
            sqlFac.CreateParameter();

            //添加固定参数，output类型，记录执行存储过程的时候是否正确。
            //Dal.ManagerParameter.AddNewOutParameter("ErrorMesssage", DbType.Single);

            //记录参数和参数值
            foreach (DbParameter param in Dal.Command.Parameters)
            {
                debugInfo.Remark += param.ParameterName + ":" + param.Value + "<br>";
            }
            //执行存储过程
            Dal.ExecuteNonQuery(spName);

            //判断是否出现异常
            if (Dal.ErrorMessage.Length > 0)
            {
                operateLog.UpdateOperateLogState(4, debugInfo.DetailList);
                return "修改数据时出现异常！";
            }

            //检查存储过程返回的参数，判断存储过程是否正确执行
            var err = "";// Dal.ManagerParameter.GetParameter<string>("ErrorMesssage");

            return err;
           
        }
        #endregion

        #region 从数据库里加载记录数据
        /// <summary>
        /// 从数据库里加载记录数据，用于修改数据和显示数据
        /// </summary>
        /// <param name="dicBaseCols">字段的描述信息</param>
        /// <param name="dicColumnsValue">字段的值</param>
        /// <returns></returns>
        public string LoadDataFillColumnsValue(Dictionary<int, IColumn> dicBaseCols, Dictionary<int, object> dicColumnsValue)
        {
            //根据主键提取记录
            string sql = Functions.IsInt(DataID) ? "select top 1 * from {0} where {1} = {2}" : "select top 1 * from {0} where {1} = '{2}'";

            DataTable dt = Dal.ExecuteFillDataTable(string.Format(sql,PageViewMeta.ModiflyTableName,PageViewMeta.PKColumn,DataID));

            if (dt.Rows.Count == 0)
                return "没有记录！";

            DataRow dr = dt.Rows[0];

            //遍历元数据，给dic_ColumnsValue赋值——字段值
            foreach (KeyValuePair<int, IColumn> info in dicBaseCols)
            {
                var colMeta = (ColumnMeta)info.Value;
                dicColumnsValue[colMeta.ColumnID] = dr[colMeta.ColSysName];
            }

            return "";

        }
        #endregion

     

    }
}
