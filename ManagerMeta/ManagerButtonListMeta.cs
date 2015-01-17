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
* function: 功能按钮的元数据的加载
* history:  created by Administrator 2012-8-29
* ***********************************************/

using System.Collections.Generic;
using System.Data;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.MetaControl;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 提取查询控件需要的属性
    /// </summary>
    public class ManagerButtonListMeta : ManagerMeta
    {
        #region 从配置信息里面加载表单控件和查询控件需要的信息，填充到 Dictionary 里面。
        /// <summary>
        /// 从配置信息里面加载表单控件和查询控件需要的信息，填充到 Dictionary 里面。
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<int, IColumn> LoadMetaData(IList<NatureDebugInfo> debugInfoList)
        {
            //提取
            string sql = "select * from Manage_ButtonBar where ModuleID=" + ModuleID + "  order by DisOrder";

            DataTable dt = DalCollection.DalMetadata.ExecuteFillDataTable(sql);
            if (DalCollection.DalMetadata.ErrorMessage.Length > 2)
                return null;

            if (dt.Rows.Count == 0)
                return null;

            var dicBaseCols = new Dictionary<int, IColumn>();

            foreach (DataRow dr in dt.Rows)
            {
                var buttonMeta = new ButtonListMeta(dr);
                //遍历记录集，把字段的配置信息加载到字典里面。
                dicBaseCols.Add(buttonMeta.ButtonID, buttonMeta);
            }

            return dicBaseCols;

        }

        #endregion
        
    }
}
