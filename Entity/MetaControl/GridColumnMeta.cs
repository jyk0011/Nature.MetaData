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
 * function: 列表里的描述信息
 * history:  created by 金洋 2009-12-29 10:18:59
 *           2011-4-11 整理
 * **********************************************
 */

using System;
using System.Data;

namespace Nature.MetaData.Entity.MetaControl
{
    /// <summary>
    /// 列表里的描述信息
    /// </summary>
    public class GridColumnMeta : ColumnWebMeta
    {
        #region 属性
        #region 列的类型 _kind
        private readonly int _kind;
        /// <summary>
        /// 列的类型。L 1：数据；2：按钮；3：复选
        /// </summary>
        public int Kind
        {
            get { return _kind; }
        }
        #endregion

        #region 是否可以对字段进行排序 _isSort
        private readonly bool _isSort;
        /// <summary>
        /// L 是否可以对字段进行排序。
        /// </summary>
        public bool IsSort
        {
            get { return _isSort; }
        }
        #endregion

        #region 格式化 _Format
        private readonly string _format = "";
        /// <summary>
        /// 格式化的方式。空字符串表示不格式化
        /// </summary>
        public string Format
        {
            get { return _format; }
        }
        #endregion

        #region 最多显示的字符数
        private readonly int _maxLength;
        /// <summary>
        /// 最多显示多少个字符。0：不限制。注：一个汉字占两个字符
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
        }
        #endregion
        #endregion

        #region 初始化
        /// <summary>
        /// 通过传递过来的DataTable来给属性赋值
        /// </summary>
        public GridColumnMeta(DataRow dr):base(dr)
        {
            _kind = int.Parse(dr["Kind"].ToString());
            _format = dr["Format"].ToString();
            _maxLength = (Int32)dr["MaxLength"];

            //_isSort = Convert.ToBoolean(dr["IsSort"].ToString());
            switch (dr["IsSort"].ToString())
            {
                case "1":
                case "True":
                case "true":
                    _isSort = true;
                    break;
                    
                default :
                    _isSort = false ;
                    break;


            }
            
        }
        #endregion

    }

}
