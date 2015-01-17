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
 * function: 按钮列表的元数据
 * history:  created by 金洋 2012-8-29
 * **********************************************
 */

using System;
using System.Data;
using Nature.MetaData.Enum;

namespace Nature.MetaData.Entity.MetaControl
{
    /// <summary>
    /// 按钮列表的元数据
    /// </summary>
    public class ButtonListMeta : IColumn
    {
        #region 属性

        /// <summary>
        /// 加入的集合里的key——按钮ID
        /// </summary>
        public int Key { get; set; }

        #region 按钮ID
        /// <summary>
        /// 按钮ID
        /// </summary>
        public int ButtonID { get; set; }
        #endregion

        #region 所在模块ID
        /// <summary>
        /// 所在模块ID
        /// </summary>
        public int ModuleID { get; set; }
        #endregion

        #region 要打开的模块 OpenModuleID
        /// <summary>
        /// 要打开的模块ID
        /// </summary>
        /// <value>
        /// 模块ID
        /// </value>
        /// user:jyk
        /// time:2012/9/15 14:10
        public int OpenModuleID { get; set; }
        #endregion

        #region 要打开的视图 OpenPageViewID
        /// <summary>
        /// 要打开的视图ID
        /// </summary>
        /// <value>
        /// 模块ID
        /// </value>
        /// user:jyk
        /// time:2012/9/15 14:10
        public int OpenPageViewID { get; set; }
        #endregion

        #region 要打开的查询视图 FindPageViewID
        /// <summary>
        /// 要打开的查询视图ID
        /// </summary>
        /// <value>
        /// 模块ID
        /// </value>
        /// user:jyk
        /// time:2012/9/15 14:10
        public int FindPageViewID { get; set; }
        #endregion

        #region 按钮的标题 _title
        /// <summary>
        /// 按钮的标题
        /// </summary>
        public string Title { get; set; }

        #endregion

        #region 控件类型 _butonType
        /// <summary>
        /// 按钮的类型，添加、修改、查看等
        /// </summary>
        public ButonType ButonType { get; set; }
        #endregion

        #region 控件形式 _butonType
        /// <summary>
        /// 按钮形式，按钮、超链接
        /// </summary>
        public int BtnKind { get; set; }
        #endregion

        #region 打开的URL _URL
        /// <summary>
        /// 打开的URL
        /// </summary>
        public string URL { get; set; }

        #endregion

        #region 宽度 _webWidth
        /// <summary>
        /// 打开的宽度
        /// </summary>
        public Int32 WebWidth { get; set; }
        #endregion

        #region 打开的高度 _webHeight
        /// <summary>
        /// 打开的高度
        /// </summary>
        public Int32 WebHeight { get; set; }
        #endregion

        #region 是否需要选择记录
        /// <summary>
        /// 是否需要选择记录。 0：不需要；1：需要
        /// </summary>
        public bool IsNeedSelect { get; set; }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 通过传递过来的DataTable——DataRow来给属性赋值
        /// </summary>
        public ButtonListMeta(DataRow dr)
        {
            ButtonID = (int)dr["ButtonID"];
            ModuleID = (int)dr["ModuleID"];
            OpenModuleID = (int)dr["OpenModuleID"];
            OpenPageViewID = (int)dr["OpenPageViewID"];
            FindPageViewID = (int)dr["FindPageViewID"];

            Title = dr["BtnTitle"].ToString();
            ButonType = (ButonType) (Int32) dr["BtnTypeID"];

            BtnKind = int.Parse(dr["BtnKind"].ToString());
            URL = dr["URL"].ToString();
            WebWidth = (int) dr["WebWidth"];
            WebHeight = (int) dr["WebHeight"];
            IsNeedSelect = dr["IsNeedSelect"].ToString() == "True";

            Key = ButtonID;
        }

        #endregion
    }
}
