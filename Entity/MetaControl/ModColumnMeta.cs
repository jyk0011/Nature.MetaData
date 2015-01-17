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
 * function: 添加、修改的描述信息
 * history:  created by 金洋 2009-8-27 9:02:51 
 *           2011-4-11 整理
 * **********************************************
 */

using System;
using System.Data;

namespace Nature.MetaData.Entity.MetaControl
{
    /// <summary>
    /// 添加、修改的描述信息
    /// </summary>
    public class ModColumnMeta : FormColumnMeta
    {
        #region 属性
      
        #region 验证方式，_ControlCheckKind
        private readonly int _controlCheckKind = 101;
        /// <summary>
        /// 验证方式 
        /// </summary>
        public int ControlCheckKind
        {
            get { return _controlCheckKind; }
        }
        #endregion

        #region 自定义验证方式，表单控件用 _CustomerCheckKind
        private readonly string _customerCheckKind = "";
        /// <summary>
        /// 自定义验证方式，即正则表达式
        /// </summary>
        public string CustomerCheckKind
        {
            get { return _customerCheckKind; }
        }
        #endregion

        #region 验证信息，表单控件用 _CheckTip
        private readonly string _checkTip = "";
        /// <summary>
        /// 未通过验证的提示信息 
        /// </summary>
        public string CheckTip
        {
            get { return _checkTip; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 通过传递过来的DataTable来给属性赋值
        /// </summary>
        public ModColumnMeta(DataRow dr)
            : base(dr)
        {
            //添加、修改专用
            _controlCheckKind = (Int32)dr["CheckTypeID"];
            _customerCheckKind = dr["CheckUserDefined"].ToString();
            _checkTip = dr["CheckTip"].ToString();

        }
        #endregion
    }
}
