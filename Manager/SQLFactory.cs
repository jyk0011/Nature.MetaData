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
* function: 根据元数据和客户录入的数据，处理SQL语句，实现添加、修改、提取数据（以供修改和浏览）
* history:  created by Administrator 
 *          2012-9-14 整理
* ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Nature.Data;
using Nature.Common;
using Nature.MetaData.Entity;

namespace Nature.MetaData.Manager
{
    /// <summary>
    /// 把字典里的数据保存到数据库里面。从数据库里提取记录，填充到字典里面。
    /// </summary>
    public class SqlFactory
    {
        #region 属性

        #region 访问客户数据库的实例

        /// <summary>
        /// 访问客户数据库的实例
        /// </summary>
        public DataAccessLibrary Dal { get; set; }

        #endregion

        #region 要修改的记录的主键字段值

        /// <summary>
        /// 要修改的记录的主键字段值
        /// </summary>
        public string DataID { get; set; }

        #endregion

        #region 存放表单用的字段的描述信息

        private Dictionary<int, IColumn> _dicFormColumnMeta;

        /// <summary>
        /// 存放表单用的字段的描述信息，key：字段ID，value：ColumnsInfo
        /// </summary>
        public Dictionary<int, IColumn> FormColumnMeta
        {
            set { _dicFormColumnMeta = value; }
            get { return _dicFormColumnMeta; }
        }
        #endregion

        #region 存放字段值

        private Dictionary<int, object> _dicColumnsValue;
        /// <summary>
        /// 存放字段值的字典，key：字段ID，value：字段值
        /// </summary>
        public Dictionary<int, object> ColumnsValue
        {
            set { _dicColumnsValue = value; }
            get { return _dicColumnsValue; }
        }

        #endregion

        #endregion
       
        //方法
        #region 拼接添加记录用的SQL语句

        /// <summary>
        /// 拼接添加记录用的SQL语句或者参数化SQL语句。
        /// insert into table ( ...) value (...)
        /// </summary>
        /// <param name="tableName">添加数据的表名</param>
        /// <param name="isUseParameter">是否使用参数化的方式。true：使用；false：不使用</param>
        /// <returns></returns>
        public string CreateInsertSql(string tableName , bool isUseParameter)
        {
            string parameterPrefix = Dal.ParameterPrefix();
            string left = Dal.ColumnLeft();
            string right = Dal.ColumnRight();

            var sql1 = new StringBuilder(900); //value前面的SQL
            var sql2 = new StringBuilder(900); //value后面的SQL

            //开始拼接SQL语句
            sql1.Append("insert into  ");
            sql1.Append(tableName); //表名
            sql1.Append(" ( ");

            #region 遍历 dic_FormColumnMeta

            foreach (KeyValuePair<int, IColumn> dic in _dicFormColumnMeta)
            {
                var colMeta = (ColumnMeta)dic.Value; //字段的描述信息
                if (colMeta.IsSave != 1)
                {
                    //不用保存该字段，遍历下一个。
                    continue;
                }

                #region 字段和字段值

                //添加字段名 [colName],
                sql1.Append(left);
                sql1.Append(colMeta.ColSysName);
                sql1.Append(right);
                sql1.Append(",");

                //添加字段值
                if (isUseParameter)
                {
                    //拼接参数化SQL语句，value后面的。 @colName,
                    sql2.Append(parameterPrefix); //参数名前缀
                    sql2.Append(colMeta.ColSysName);
                    sql2.Append(",");

                    //使用参数化的SQL语句，添加存储过程的参数
                    AddParameter(colMeta, _dicColumnsValue[colMeta.ColumnID].ToString());
                }
                else
                {
                    #region 拼接SQL语句，value后面的。 'colValue',
                    switch (colMeta.ColType)
                    {
                        case "real":
                        case "float":
                        case "numeric":
                        case "smallmoney":
                        case "money":
                        case "decimal":

                        case "bigint":
                        case "tinyint":
                        case "smallint":
                        case "int":
                            sql2.Append(_dicColumnsValue[colMeta.ColumnID]);
                            sql2.Append(",");
                            break;

                        default:
                            sql2.Append("'");
                            sql2.Append(_dicColumnsValue[colMeta.ColumnID]);
                            sql2.Append("',");
                            break;
                    }
                    #endregion
                }

                #endregion

            }
            sql1.Remove(sql1.Length - 1, 1); //去掉最后的 , 号
            sql2.Remove(sql2.Length - 1, 1); //去掉最后的 , 号

            #endregion

            sql1.Append(" ) values (");
            sql1.Append(sql2);
            sql1.Append(" ) ");

            string sql = sql1.ToString();
            sql1.Length = 0;
            sql2.Length = 0;

            return sql;
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 拼接修改数据用的SQL语句或者参数化SQL语句。
        /// update table set ...  where ...
        /// </summary>
        /// <param name="tableName">添加数据的表名</param>
        /// <param name="isUseParameter">是否使用参数化的方式。true：使用；false：不使用</param>
        /// <param name="pkColumnName">主键字段名，用于修改记录的where后的语句</param>
        /// <param name="pkColumnValue">主键字段的值，用于修改记录的where后的语句</param>
        /// <returns></returns>
        public string CreateUpdateSql(string tableName, bool isUseParameter, string pkColumnName, string pkColumnValue)
        {
            string parameterPrefix = Dal.ParameterPrefix();
            string left = Dal.ColumnLeft();
            string right = Dal.ColumnRight();

            var sbSql = new StringBuilder(900); //value前面的SQL

            //开始拼接SQL语句 update tableName set 
            sbSql.Append("update ");
            sbSql.Append(tableName); //表名
            sbSql.Append(" set ");

            //遍历 dic_FormColumnMeta 
            foreach (KeyValuePair<int, IColumn> dic in _dicFormColumnMeta)
            {
                var colMeta = (ColumnMeta)dic.Value; //字段的描述信息

                #region 判断是否保存该字段

                if (colMeta.IsSave != 1)
                {
                    //不用保存该字段，遍历下一个。
                    continue;
                }

                #endregion

                #region 字段和字段值

                //添加字段名 [colName] = 
                sbSql.Append(left);
                sbSql.Append(colMeta.ColSysName);
                sbSql.Append(right);
                sbSql.Append("=");

                //添加字段值
                if (isUseParameter)
                {
                    //拼接参数化SQL语句
                    sbSql.Append(parameterPrefix); //参数名前缀
                    sbSql.Append(colMeta.ColSysName);
                    sbSql.Append(",");

                    //使用参数化的SQL语句，添加存储过程的参数
                    AddParameter(colMeta, _dicColumnsValue[colMeta.ColumnID].ToString());
                }
                else
                {
                    #region 拼接SQL语句，value后面的。
                    switch (colMeta.ColType)
                    {
                        case "real":
                        case "float":
                        case "numeric":
                        case "smallmoney":
                        case "money":
                        case "decimal":

                        case "bigint":
                        case "tinyint":
                        case "smallint":
                        case "int":
                            sbSql.Append(_dicColumnsValue[colMeta.ColumnID]);
                            sbSql.Append(",");
                            break;

                        default:
                            sbSql.Append("'");
                            sbSql.Append(_dicColumnsValue[colMeta.ColumnID]);
                            sbSql.Append("',");
                            break;
                    }
                    #endregion
                }

                #endregion

            }
            sbSql.Remove(sbSql.Length - 1, 1); //去掉作后的 , 号

            sbSql.Append(" where ");

            //判断是否能够找到主键字段对应的控件，如果能够找到则使用控件里的字段名和值进行修改

            #region where 后面的语句

            if (isUseParameter)
            {
                //拼接参数化SQL语句的修改依据
                sbSql.Append(pkColumnName);
                sbSql.Append(" =  ");
                sbSql.Append(parameterPrefix); //参数名前缀 需要约定参数名称
                sbSql.Append("ThisDataID");

                if (Functions.IsInt(pkColumnValue))
                    Dal.ManagerParameter.AddNewInParameter("ThisDataID", Int32.Parse(pkColumnValue));
                else
                    Dal.ManagerParameter.AddNewInParameter("ThisDataID", pkColumnValue);
            }

            else
            {
                //拼接SQL语句的修改依据
                sbSql.Append(pkColumnName);
                if (Functions.IsNumeric(pkColumnValue))
                {
                    sbSql.Append(" = ");
                    sbSql.Append(pkColumnValue);
                }
                else
                {
                    sbSql.Append(" = '");
                    sbSql.Append(pkColumnValue);
                    sbSql.Append("'");
                }
            }

            #endregion

            string sql = sbSql.ToString();
            sbSql.Length = 0;

            return sql;

        }

        #endregion

        #region 创建存储过程用的参数

        /// <summary>
        /// 使用存储过程添加数据的时候使用
        /// 创建存储过程需要的参数
        /// </summary>
        /// <returns></returns>
        public string CreateParameter()
        {
            #region 遍历 dic_FormColumnMeta

            foreach (KeyValuePair<int, IColumn> dic in _dicFormColumnMeta)
            {
                var info = (ColumnMeta)dic.Value; //字段的描述信息
                if (info.IsSave != 1)
                {
                    //不用保存该字段，遍历下一个。
                    continue;
                }
                

                //使用参数化的SQL语句，添加存储过程的参数
                AddParameter(info, _dicColumnsValue[info.ColumnID].ToString());
            }

            #endregion

            return "";
        }

        #endregion

        #region 添加参数

        /// <summary>
        /// 添加存储过程的参数
        /// </summary>
        /// <param name="baseInfo">字段描述信息</param>
        /// <param name="colValue">字段值</param>
        public bool AddParameter(ColumnMeta baseInfo, object colValue)
        {
            //根据配置信息添加存储过程（参数化SQL语句）需要的参数。

            //dal.LoadParameter();
            bool isNull = false;

            if (Dal.Command.Parameters.Contains(baseInfo.ColSysName))
            {
                //已经有这个参数名称了，修改参数值
                Dal.ManagerParameter[baseInfo.ColSysName].Value = colValue;
                return false;
            }

            switch (baseInfo.ColType)
            {
                case "bigint":
                    //1
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, Convert.ToInt64(colValue));
                    break;

                case "tinyint":
                case "smallint":
                case "int":
                    //2
                    if (!Functions.IsInt(colValue.ToString()))
                    {
                        Functions.MsgBox(baseInfo.ColName + "的输入值：【" + colValue + "】不能转换成int！", false);
                        return false;
                    }
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, Convert.ToInt32(colValue));
                    break;

                case "ids": // 1,2,3的形式
                case "uniqueidentifier":
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    //3
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, colValue.ToString(), baseInfo.ColSize);
                    break;

                case "text":
                case "ntext":
                    //4
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, colValue.ToString());
                    break;

                case "real":
                case "float":
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, Convert.ToDouble(colValue));
                    break;


                case "numeric":
                case "smallmoney":
                case "money":
                case "decimal":
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, Convert.ToDecimal(colValue));
                  
                    //SqlParameter par = new SqlParameter(Dal.ParameterPrefix() + baseInfo.ColSysName, SqlDbType.Decimal);
                    //par.Precision = 18;
                    //par.Scale = 4;
                    //par.Value = Convert.ToDecimal(colValue);
                    //Dal.Command.Parameters.Add(par);
 
                   break;

                case "smalldatetime":
                case "datetime":
                    if (colValue.ToString().Length == 0)
                    {
                        //没有值
                        //dal.ParameterManager.AddNewInParameter(baseInfo.ColSysName, DateTime.Parse("1900-1-1"));
                        //dal.ParameterManager.AddNewInParameter<Nullable<DateTime>>(baseInfo.ColSysName, null);
                        //DbParameter par = CommonFactory.CreateParameter(baseInfo.ColSysName, dal.DatabaseProvider);
                        //par.Direction = System.Data.ParameterDirection.Input;
                        //par.DbType = System.Data.DbType.DateTime;
                        //par.Value = null;   // DateTime.Parse("1900-1-1");
                        //dal.DataCommand.Parameters.Add(par);

                        isNull = true;
                    }
                    else
                        //有值
                        Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, Convert.ToDateTime(colValue));

                    break;

                case "bit":
                    bool tmpValue = true;
                    switch (colValue.ToString().ToLower())
                    {
                        case "0":
                        case "false":
                            tmpValue = false;
                            break;
                    }
                    Dal.ManagerParameter.AddNewInParameter(baseInfo.ColSysName, tmpValue);
                    break;

            }

            return isNull;
        }

        #endregion


        #region 创建提交的数据

        /// <summary>
        /// 使用存储过程添加数据的时候使用
        /// 创建存储过程需要的参数
        /// </summary>
        /// <returns></returns>
        public string CreateSubmitValueToJson()
        {
            #region 遍历 dic_FormColumnMeta

            var sb = new StringBuilder(_dicFormColumnMeta.Count * 600);

            sb.Append("{ ");

            foreach (KeyValuePair<int, IColumn> dic in _dicFormColumnMeta)
            {
                var info = (ColumnMeta)dic.Value; //字段的描述信息
                if (info.IsSave != 1)
                {
                    //不用保存该字段，遍历下一个。
                    continue;
                }

                sb.Append("\"");
                sb.Append(info.ColumnID);
                sb.Append("\":");
                Json.ObjectToJson(_dicColumnsValue[info.ColumnID] ,sb);
                sb.Append(",");

            }

            sb[sb.Length - 1] = '}';

            #endregion

            return sb.ToString();
        }

        #endregion
    }
}
