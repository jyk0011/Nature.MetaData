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
* function: 模块信息的加载
* history:  created by Administrator 2012-9-15
* ***********************************************/

using System.Data;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.WebPage;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 提取查询控件需要的属性
    /// </summary>
    public class ManagerModule : ManagerMeta
    {
        #region 钩子

        /// <summary>
        /// 设置SQL语句
        /// </summary>
        /// <returns></returns>
        protected override string GetSql()
        {
            string sql = "select * from Manage_Module {0} order by DisOrder";

            sql = string.IsNullOrEmpty(Query) ? string.Format(sql, "") : string.Format(sql, "where " + Query);

            return sql;

        }

        /// <summary>
        /// 创建需要的实例
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected override IColumn CreatNewEntity(DataRow dr)
        {
            return new ModuleEntity(dr);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 提取节点使用的查询条件
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        /// user:jyk
        /// time:2012/9/15 10:10
        public string Query { get; set; }

        #endregion


    }
}
