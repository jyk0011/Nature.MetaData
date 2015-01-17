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
 * function: 功能模块（节点）的描述信息
 * history:  created by 金洋 2010-1-7 9:02:51 
 *           2011-4-11 整理
 * **********************************************
 */


using System;
using System.Collections.Generic;
using System.Data;

namespace Nature.MetaData.Entity.WebPage
{
    /// <summary>
    /// 一个页面的实体，包含页面视图的元数据、分页元数据和列表的元数据
    /// </summary>
    public class PageEntity
    {
        #region 属性

        #region 模块ID _moduleID

        private readonly int _moduleID;
        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID 
        {
            get { return _moduleID; }
        }

        #endregion

        #region 页面视图的元数据

        /// <summary>
        /// 页面里的各个视图
        /// </summary>
        public Dictionary<int, PageViewMeta> DicPageView { get; set; }

        #endregion

        #region 列表、表单里需要的字段和字段信息

        /// <summary>
        /// 列表、表单里需要的字段和字段信息
        /// int：字段编号
        /// ColumnMeta：字段元数据。根据View的类型而加载不同的子类
        /// </summary>
        public Dictionary<int, ColumnMeta> ColumnList { get; set; }

        #endregion

        #endregion

        #region 初始化

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="dr">记录集</param>
        public PageEntity(DataRow dr)
        {
            _moduleID = (Int32) dr["ModuleID"];

           

            //加载字段的详细信息
             
            

        }

        #endregion
    }
}
