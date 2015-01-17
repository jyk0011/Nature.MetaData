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
 * function: 字段的基本信息
 * history:  created by 金洋 
 *           2011-4-11 整理
 * **********************************************
 */

using System;
using System.Data;

namespace Nature.MetaData.Entity
{
    /// <summary>
    /// 字段的基本信息的元数据
    /// 添加、修改使用。
    /// </summary>
    public class ColumnMeta : IColumn
    {
        #region 属性——字段的基本信息的描述
        /// <summary>
        /// 加入的集合里的key——模块ID
        /// </summary>
        public int Key { get; set; }

        #region 字段编号 _ColumnID

        private readonly int _columnID;

        /// <summary>
        /// 配置信息里面的字段的标识
        /// 表ID + 四位序号 组成。一旦生成不建议修改。
        /// </summary>
        public int ColumnID
        {
            get { return _columnID; }
        }

        #endregion

        #region 字段所在的表的编号 _TableID

        private readonly int _tableID;

        /// <summary>
        /// 字段所在的表的编号
        /// </summary>
        public int TableID
        {
            get { return _tableID; }
        }

        #endregion

        #region 字段类型 _ColumnKind

        private readonly int _columnKind = 1;

        /// <summary>
        /// <para>字段类型</para>
        /// <para>11	主键	</para>
        /// <para>12	外键	</para>
        /// <para>13	索引	</para>
        /// <para>14	一般	</para>
        /// <para>15	扩展 数据库的表里没有该字段</para>
        /// </summary>
        public int ColumnKind
        {
            get { return _columnKind; }
        }

        #endregion

        #region 字段名 _ColSysName

        private readonly string _colSysName = "";

        /// <summary>
        /// 数据库里的字段名称
        /// </summary>
        public string ColSysName
        {
            get { return _colSysName; }
        }

        #endregion

        #region 显示给客户看的字段名 _ColName

        private readonly string _colName = "";

        /// <summary>
        /// 显示给客户看的名称
        /// </summary>
        public string ColName
        {
            get { return _colName; }
        }

        #endregion

        #region 字段类型 _ColType

        private readonly string _colType = "";

        /// <summary>
        /// 字段类型，int、nvarchar、datetime 等
        /// </summary>
        public string ColType
        {
            get { return _colType; }
        }

        #endregion

        #region 属性类型 _PropertyType

        /// <summary>
        /// 字段对应的属性的类型，int、string、datetime 等
        /// 用于代码生成器。把数据库字段类型，变成.net类型
        /// </summary>
        public string PropertyType { get; set; }

        #endregion

        #region 字段大小 _ColSize

        private readonly Int32 _colSize;

        /// <summary>
        /// 字段大小
        /// </summary>
        public Int32 ColSize
        {
            get { return _colSize; }
        }

        #endregion

        //表单控件专用

        #region 是否保存，表单控件用 _IsSave

        private int _isSave = 1;

        /// <summary>
        /// 是否要把控件的值保存到数据库里面。1：保存；2：不保存但是加载信息；3：不保存也不加载信息
        /// 2013-8-19 由bool改成int。增加一个状态：不保存也不加载信息。jyk
        /// </summary>
        public int IsSave
        {
            get { return _isSave; }
            set { _isSave = value; }
        }

        #endregion

        #endregion

        #region 初始化

        /// <summary>
        /// 通过传递过来的DataTable来给属性赋值
        /// </summary>
        public ColumnMeta(DataRow dr)
        {
            //从配置文件里面加载配置信息
            _columnID = Int32.Parse(dr["ColumnID"].ToString());
            _tableID = Int32.Parse(dr["TableID"].ToString());
            _columnKind = Int32.Parse(dr["ColumnKind"].ToString());
            _colSysName = dr["ColSysName"].ToString();

            //如果设置了ColTitle 则提取ColTitle。
            if (dr["ColTitle"] is DBNull || dr["ColTitle"].ToString().Length == 0)
                _colName = dr["ColName"].ToString();
            else
                _colName = dr["ColTitle"].ToString();

            _colType = dr["ColType"].ToString();
            _colSize = (Int32) dr["ColSize"];
            _isSave = (Int32)dr["Ser_IsSave"] ;

            Key = _columnID;
        }

        #endregion
    }
}
