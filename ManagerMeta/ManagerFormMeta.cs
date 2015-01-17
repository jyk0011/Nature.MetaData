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
 * function: 获取表单的元数据
 * history:  created by 金洋 2009-12-29 10:18:59
 *           2011-4-11 整理
 * **********************************************
 */

/* ***********************************************
* author :  Administrator
* email  :  jyk0011@live.cn 
* function: 
* history:  created by Administrator 2009-8-27 8:55:50 
* ***********************************************/


using System.Data;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.MetaControl;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 提取表单控件需要的属性
    /// </summary>
    public class ManagerFormMeta : ManagerMeta
    {
        #region 钩子
        /// <summary>
        /// 设置SQL语句
        /// </summary>
        /// <returns></returns>
        protected override string GetSql()
        {
            string sql = "select * from V_Frame_List_BaseFormCol where PVID = " + PageViewID + "  order by DisOrder";
            return sql;
        }
        /// <summary>
        /// 创建需要的实例
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected override IColumn CreatNewEntity(DataRow dr)
        {
            return new ModColumnMeta(dr);
        }
        #endregion

     
    }
}
