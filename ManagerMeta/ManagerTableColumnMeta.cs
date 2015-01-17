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
 * function: 表里的全部字段的元数据
 * history:  created by 金洋 2013-09-26 15:01:59
 *           2011-4-11 整理
 * **********************************************
 */


using System.Data;
using Nature.MetaData.Entity;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 获取表里的全部字段的元数据
    /// </summary>
    public class ManagerTableColumnMeta : ManagerMeta
    {
        #region 钩子
        /// <summary>
        /// 设置SQL语句
        /// </summary>
        /// <returns></returns>
        protected override string GetSql()
        {
            //此处 PageViewID  相当于 tableID
            string sql = "select *,'' as ColTitle,1 as Ser_IsSave from Manage_Columns where TableID = " + PageViewID + "  order by ColumnID";
            return sql;
        }
        /// <summary>
        /// 创建需要的实例
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected override IColumn CreatNewEntity(DataRow dr)
        {
            return new ColumnMeta(dr);
        }
        #endregion
    }
}
