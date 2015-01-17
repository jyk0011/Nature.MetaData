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
 * function: 设置查询条件
 * history:  created by 金洋 2010-1-6 16:56:34 
 *           2011-4-11 整理
 * **********************************************
 */
/* ***********************************************
* author :  Administrator
* email  :  jyk0011@live.cn 
* function: 
* history:  created by Administrator 
 *          2012-10-31 拼接查询条件，直接拼接，以后要增加参数化功能
* ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Nature.Common;
using Nature.Data;
using Nature.MetaData.ControlExtend;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.MetaControl;

namespace Nature.MetaData.Manager
{
    /// <summary>
    /// 实现 处理查询条件的功能
    /// </summary>
    public static  class ManagerFind
    {
        #region 属性

        /// <summary>
        /// 定义查询方式
        /// </summary>
        private static readonly Dictionary<Int32, string> SearchKind = new Dictionary<Int32, string>();
         
        #endregion

        
        //===================================================
        #region 设置查询条件
        /// <summary>
        /// 设置查询条件。根据设置好的配置信息，和传递过来的关键字，拼接查询用的SQL语句里的查询条件
        /// </summary>
        /// <returns></returns>
        public static void SetQuery(Dictionary<int, IColumn> dicBaseCols, Dictionary<int, object> dicColumnsValue,StringBuilder sql,DataAccessLibrary dal, out string tableName   )
        {
            tableName = null;

            #region 添加查询方式

            if (SearchKind.Count == 0)
            {
                // 字段的查询方式。1：= int；2：=string； 3:like %n%；  4:like n%； 5:like %n ；6：like n；11：> int；12：< int；13：>= int；14: <= int

                //加载查询方式
                //单目查询
                SearchKind.Add(2001, "[{0}]={1}"); //1：= int
                SearchKind.Add(2002, "[{0}]='{1}'"); //2：=string
                SearchKind.Add(2003, "[{0}]<>{1}"); //1：<> int
                SearchKind.Add(2004, "[{0}]<>'{1}'"); //2：<>string

                SearchKind.Add(2005, "[{0}] like '%{1}%'"); //3:like %n%；
                SearchKind.Add(2006, "[{0}] not like '%{1}%'"); //6：not like n；
                SearchKind.Add(2007, "{0} like '{1}%'"); //4:like n%；
                SearchKind.Add(2008, "[{0}] like '%{1}'"); //5:like %n ；

                SearchKind.Add(2011, "[{0}] > {1}"); //11：> int；
                SearchKind.Add(2012, "[{0}] < {1}"); //12：< int；
                SearchKind.Add(2013, "[{0}] >= {1}"); //13：>= int；
                SearchKind.Add(2014, "[{0}] <= {1}"); //14: <= int

                //双目查询
                SearchKind.Add(2101, "[{0}] between '{1}' and '{2}'"); //21      'between string
                SearchKind.Add(2102, "[{0}] between  {1}  and {2}  "); //22      'between int
                SearchKind.Add(2103, "[{0}] > {1}  and [{0}] <={2}"); //22      'between int
                SearchKind.Add(2104, "[{0}] >= {1} and [{0}] < {2}"); //22      'between int
                SearchKind.Add(2105, "[{0}] > {1}  and [{0}] < {2}"); //22      'between int

                //多目查询
                SearchKind.Add(2201, "[{0}] in ({1})"); //23     'col in () 多选查询 数字方式
                SearchKind.Add(2202, "[{0}] in ('{1}')"); //24   'col in () 多选查询 字符串方式

                //特殊查询
                SearchKind.Add(2301, ""); //30     '不生成查询条件

            }

            #endregion

            foreach (KeyValuePair<int, IColumn> info in dicBaseCols)
            {
                //根据查询条件，拼接SQL语句。
                var bInfo = (FindColumnMeta)info.Value;
                var colValue = (string)dicColumnsValue[bInfo.ColumnID];//.ToString();

                if (string.IsNullOrEmpty(colValue) || colValue == "`" || colValue == "-99999")
                    //不需要查询
                    continue;

                //关键字有效，拼接查询语句
                if (bInfo.FindKind < 2100)
                {
                    //单目查询
                    DanMu(colValue, bInfo, sql);
                    continue;

                }

                if (bInfo.FindKind < 2200)
                {
                    //双目查询
                    ShuangMu(colValue, bInfo, sql);
                    continue;
                }

                if (bInfo.FindKind < 2300)
                {
                    //多目查询
                    DuoMu(colValue, bInfo, sql);
                    continue;

                }

                if (bInfo.FindKind == 2302)
                {
                    //获取表名
                    var ext = bInfo.ControlExtend as DropDownListExpand;
                    if (ext != null)
                    {
                        string sqlList = ext.Sql;
                        DataTable dt = dal.ExecuteFillDataTable(sqlList);
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "id=" + colValue;
                        tableName = "[" + dv[0]["txt"].ToString() + "] WITH(NOLOCK) ";

                    }
                    continue;
                }

                //特殊查询，看看是否有自定义查询。没有的话就不处理了
                if (!string.IsNullOrEmpty(bInfo.CustomerFindKind))
                {
                    //有自定义的查询条件
                    if (sql.Length > 0)
                        sql.Append(" and ");

                    string tmpQuery = bInfo.CustomerFindKind;

                    //只有两个的话，视为 {0} 字段名，{1}查询key
                    if (tmpQuery.IndexOf("{1}", StringComparison.Ordinal) >0)
                        sql.Append(String.Format(tmpQuery, bInfo.ColSysName, colValue));
                    //只有一个的话，视为 {0}  查询key
                    else if (tmpQuery.IndexOf("{0}", StringComparison.Ordinal) > 0)
                        sql.Append(String.Format(tmpQuery, colValue));
                    //没有的话，直接加
                    else    
                        sql.Append( tmpQuery );


                }

            }

            
        }
        #endregion

        #region 单目
        private static void DanMu(string colValue, FindColumnMeta bInfo, StringBuilder sql)
        {
            if (sql.Length > 0)
                sql.Append(" and ");

            sql.Append(String.Format(SearchKind[bInfo.FindKind], bInfo.ColSysName, colValue));

        }
        #endregion

        #region 双目
        private static void ShuangMu(string colValue, FindColumnMeta bInfo, StringBuilder sql)
        {
            string[] str = colValue.Split('`');
            if (str.Length >= 2)
            {
                switch (bInfo.ColType)
                {
                    case "datetime":
                    case "smalldatetime":
                        //设置日期
                        SetDay(str, bInfo, sql);
                        break;

                    default:
                        //非日期
                        SetNotDay(str, bInfo, sql);
                        break;
                }
            }
        }

        //设置日期
        private static void SetDay(string[] str, FindColumnMeta bInfo, StringBuilder sql)
        {
            if (str[0].Length == 0) //第一个条件没有输入，不考虑这个查询条件。
                return;

            DateTime dt1 = DateTime.Parse(str[0]);

            //第二个条件没有输入，按照“第一个条件+1天”计算
            DateTime dt2 = str[1].Length == 0 ? dt1.AddDays(1) : DateTime.Parse(str[1]);


            if (sql.Length > 0)
                sql.Append(" and ");

            sql.Append(String.Format(SearchKind[bInfo.FindKind], bInfo.ColSysName, dt1.ToString("yyyy-MM-dd HH:mm:ss"), dt2.ToString("yyyy-MM-dd HH:mm:ss")));

        }

        //设置非日期
        private static void SetNotDay(string[] str, FindColumnMeta bInfo, StringBuilder sql)
        {
            if (str[0].Length > 0 && str[1].Length > 0)
            {
                if (sql.Length > 0)
                    sql.Append(" and ");

                sql.Append(String.Format(SearchKind[bInfo.FindKind], bInfo.ColSysName, str[0], str[1]));
            }
            else if (str[0].Length > 0)
            {
                if (sql.Length > 0)
                    sql.Append(" and ");

                sql.Append(String.Format(SearchKind[bInfo.FindKind], bInfo.ColSysName, str[0], str[0]));
            }

        }
        #endregion

        #region 多目
        private static void DuoMu(string colValue, FindColumnMeta bInfo, StringBuilder sql)
        {
            string query2 = colValue;
            if (bInfo.FindKind == 302)
                query2 = query2.Replace(",", "','");

            if (sql.Length > 0)
                sql.Append(" and ");

            sql.Append(String.Format(SearchKind[bInfo.FindKind], bInfo.ColSysName, query2));

        }
        #endregion

      
    }
}
