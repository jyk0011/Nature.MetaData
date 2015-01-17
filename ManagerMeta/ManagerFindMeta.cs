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
* function: 查询控件用的，提取元数据
* history:  created by Administrator 2009-8-27 10:18:59 
* ***********************************************/

using System.Data;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.MetaControl;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 提取查询控件需要的属性
    /// </summary>
    public class ManagerFindMeta : ManagerMeta
    {
        #region 钩子
        /// <summary>
        /// 设置SQL语句
        /// </summary>
        /// <returns></returns>
        protected override string GetSql()
        {
            string sql = "select * from V_Frame_List_BaseFormCol where PVID=" + PageViewID + "  order by DisOrder";
            return sql;
        }
        /// <summary>
        /// 创建需要的实例
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected override IColumn CreatNewEntity(DataRow dr)
        {
            return new FindColumnMeta(dr); 
        }
        #endregion

        #region 从配置信息里面加载表单控件和查询控件需要的信息，填充到 Dictionary 里面。
        ///// <summary>
        ///// 从配置信息里面加载表单控件和查询控件需要的信息，填充到 Dictionary 里面。
        ///// </summary>
        ///// <returns></returns>
        //protected override Dictionary<int, IColumn> LoadMetaData()
        //{
        //    //提取
        //    string sql = "select * from V_Frame_List_BaseFormCol where PVID=" + PageViewID + "  order by DisOrder";

        //    DataTable dt = DalCollection.DalMetadata.ExecuteFillDataTable(sql);
        //    if (DalCollection.DalMetadata.ErrorMessage.Length > 2)
        //        return null;

        //    if (dt.Rows.Count == 0)
        //        return null;

        //    var dicBaseCols = new Dictionary<int, IColumn>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ColumnMeta columnMeta = new FindColumnMeta(dr);
        //        //遍历记录集，把字段的配置信息加载到字典里面。
        //        dicBaseCols.Add(columnMeta.ColumnID, columnMeta);
        //    }


        //    return dicBaseCols;

        //}

        #endregion
        
    }
}
