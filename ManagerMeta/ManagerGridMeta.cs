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
* author :  jyk
* email  :  jyk0011@live.cn 
* function: 数据表格用的，提取元数据
* history:  created by Administrator 2009-8-27 10:11:23 
* ***********************************************/


using System.Data;
using Nature.MetaData.Entity;
using Nature.MetaData.Entity.MetaControl;

namespace Nature.MetaData.ManagerMeta
{
    /// <summary>
    /// 管理表格的元数据
    /// </summary>
    public class ManagerGridMeta : ManagerMeta
    {
        #region 钩子
        /// <summary>
        /// 设置SQL语句
        /// </summary>
        /// <returns></returns>
        protected override string GetSql()
        {
            string sql = "select *  from V_Frame_List_DataGridListCol where PVID=" + PageViewID + " order by DisOrder";
            return sql;
        }

        /// <summary>
        /// 创建需要的实例
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected override IColumn CreatNewEntity(DataRow dr)
        {
            return new GridColumnMeta(dr);
        }
        #endregion

        #region 获取指定角色可以访问指定节点里的列表的字段ID。
        /// <summary>
        /// 获取指定用户可以访问指定节点里的列表的字段ID。
        /// </summary>
        /// <param name="roleIDs">角色，可以是多个角色</param>
        /// <returns></returns>
        public int[] GetUserListColumns(string roleIDs)
        {
            string sql;
            //sql = "select ColumnIDs from Role_RoleColumn where RoleID in (" + roleIDs + ") and FunctionID=" + PageViewID + " and Kind = 1 ";
            //string[] str = DALMetadata.ExecuteStringsByColumns(sql);

            int[] re;

            //if (str != null && str.Length > 0 && str[0].Length > 0)
            //{
            //    //设置了权限到列表字段
            //    string[] tmp = str[0].Split(',');
            //    re = new int[tmp.Length];
            //    for (int i = 0; i < tmp.Length; i++)
            //        re[i] = int.Parse(tmp[i]);

            //    return re;

            //}
            //else
            //{
            //没有设置权限到字段，提取列表里的全部字段
            sql = "select ColumnID from V_Frame_List_DataGridListCol where PVID = " + PageViewID + " order by DisOrder ";
            string[] tmp = DalCollection.DalMetadata.ExecuteStringsByColumns(sql);

            re = new int[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
                re[i] = int.Parse(tmp[i]);

            return re;
            //}

        }

        #endregion

    }
     
}
