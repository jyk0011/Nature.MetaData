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
 * function: 页面里的字段的元数据，列表、表单共用信息
 * history:  created by 金洋 
 *           2011-4-11 整理
 * **********************************************
 */


using System;
using System.Data;

namespace Nature.MetaData.Entity
{
    /// <summary>
    /// 页面里的字段的元数据
    /// 列表、表单共用信息
    /// </summary>
    public class ColumnWebMeta : ColumnMeta
    {
        #region 属性

        #region 显示的标题 _title

        private readonly string _title = "";

        /// <summary>
        /// 显示的标题 
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        #endregion

        #region 帮助信息 ControlColHelp

        private readonly string _controlColHelp = "";

        /// <summary>
        /// 帮助信息 
        /// </summary>
        public string ControlColHelp
        {
            get { return _controlColHelp; }
        }

        #endregion

        #region 帮助信息的位置 _ControlHelpStation

        private readonly int _controlHelpStation = 1;

        /// <summary>
        /// 帮助信息的位置。1：不显示；2：最面；3：右面
        /// </summary>
        public int ControlHelpStation
        {
            get { return _controlHelpStation; }
        }

        #endregion

        #region TD的宽度 _colWidth

        private readonly int _colWidth = 1;

        /// <summary>
        /// TD的宽度。0：不设置宽度自动调整；其他：指定宽度px
        /// </summary>
        public int ColWidth
        {
            get { return _colWidth; }
        }

        #endregion

        #region  TD的对齐方式 _colAlign

        private readonly string _align;

        /// <summary>
        /// TD的对齐方式：left、middle、right
        /// </summary>
        public string Align
        {
            get { return _align; }
        }

        #endregion

        #endregion

        #region 初始化

        /// <summary>
        /// 通过传递过来的DataTable来给属性赋值
        /// </summary>
        public ColumnWebMeta(DataRow dr)
            : base(dr)
        {
            //页面里的字段的共用信息
            _controlColHelp = dr["ColHelp"].ToString();
            _controlHelpStation = (Int32) dr["HelpStation"];
            _colWidth = Int32.Parse(dr["ColWidth"].ToString());
            _align = dr["ColAlign"].ToString();

            _title = dr["ColName"].ToString().Length == 0 ? dr["ColTitle"].ToString() : dr["ColName"].ToString();
        }

        #endregion

    }
}
