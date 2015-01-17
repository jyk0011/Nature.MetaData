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
 * function: 查询的描述信息
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
    public class FindColumnMeta : FormColumnMeta
    {
        #region 属性

        #region 查询方式，_FindKind
        private readonly Int32 _findKind;
        /// <summary>
        /// 查询方式
        /// </summary>
        public Int32 FindKind
        {
            get { return _findKind; }
        }
        #endregion

        #region 自定义查询方式，_CustomerFindKind
        private readonly string _customerFindKind = "";
        /// <summary>
        /// 自定义查询方式，
        /// 比如 id in (select id from link where Mobile like '{0}%')
        /// </summary>
        public string CustomerFindKind
        {
            get { return _customerFindKind; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 通过传递过来的DataTable来给属性赋值
        /// </summary>
        public FindColumnMeta(DataRow dr)
            : base(dr)
        {
            //查询方式
            _findKind = (Int32)dr["Ser_FindKindID"];
            //自定义查询方式
            _customerFindKind = dr["Ser_CustomerFindKind"].ToString();
        }
        #endregion

    }
}
