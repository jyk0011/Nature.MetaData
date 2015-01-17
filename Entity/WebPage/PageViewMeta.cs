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
 * function: 增删改用的元数据
 * history:  created by 金洋 2010-1-7 9:02:51 
 *           2011-4-11 整理
 *           2012-8-27 比较大的修改
 * **********************************************
 */

using System;
using System.Collections.Generic;
using System.Data;
using Nature.MetaData.Entity.QuickPager;
using Nature.MetaData.Enum;

namespace Nature.MetaData.Entity.WebPage
{
    /// <summary>
    /// 页面视图的元数据
    /// </summary>
    public class PageViewMeta
    {
        #region 属性

        #region 页面视图ID
        private readonly int _pvid;
        /// <summary>
        /// 页面视图ID
        /// </summary>
        public int PageViewID
        {
            get { return _pvid; }
        }

        #endregion

        #region 页面视图的标题 _title

        private string _title = "";

        /// <summary>
        /// 页面视图的标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        #endregion

        #region 页面视图的类型 _type

        private PageViewType _pvType = PageViewType.DataList;

        /// <summary>
        /// 页面视图的类型：列表、查询、添加、修改、删除、导出、选择记录、上传等
        /// </summary>
        public PageViewType PageViewType
        {
            set { _pvType = value; }
            get { return _pvType; }
        }

        #endregion

        #region 获取数据的表名或者视图名 _dataSourceTableName

        private string _dataSourceTableName = "";

        /// <summary>
        /// 获取数据的表名或者视图名
        /// </summary>
        public string DataSourceTableName
        {
            set { _dataSourceTableName = value; }
            get { return _dataSourceTableName; }
        }

        #endregion

        #region 维护数据的表名 _modiflyTableName

        private string _modiflyTableName = "";

        /// <summary>
        /// 添加、修改、删除数据的表名
        /// </summary>
        public string ModiflyTableName
        {
            set { _modiflyTableName = value; }
            get { return _modiflyTableName; }
        }

        #endregion

        #region 维护数据的表ID _modiflyTableID

        private int _modiflyTableID ;

        /// <summary>
        /// 添加、修改、删除数据的表名
        /// </summary>
        public int ModiflyTableID
        {
            set { _modiflyTableID = value; }
            get { return _modiflyTableID; }
        }

        #endregion

        #region 主键字段ID _pkColumnID

        /// <summary>
        /// 主键字段名称
        /// </summary>
        public int PKColumnID { get; set; }

        #endregion

        #region 主键字段名称 _pkColumn

        private string _pkColumn = "";

        /// <summary>
        /// 主键字段名称
        /// </summary>
        public string PKColumn
        {
            set { _pkColumn = value; }
            get { return _pkColumn; }
        }

        #endregion

        #region 外键字段名称 _foreignColumn

        private string _foreignColumn = "";

        /// <summary>
        /// 外键字段名称
        /// </summary>
        public string ForeignColumn
        {
            set { _foreignColumn = value; }
            get { return _foreignColumn; }
        }

        #endregion

        #region 处理数据的操作方式 _ModKind

        /// <summary>
        /// 处理数据的操作方式：SQL、参数化SQL、存储过程
        /// </summary>
        public SQLType SQLType { get; set; }

        #endregion

        #region 表单控件的列数 _columnCount

        private int _columnCount = 3;

        /// <summary>
        /// 表单控件的列数
        /// </summary>
        public int ColumnCount
        {
            set { _columnCount = value; }
            get { return _columnCount; }
        }

        #endregion

        #region 提交表单的网址

        private string _viewExtend = "";
        /// <summary>
        /// 提交表单的网址
        /// </summary>
        public string ViewExtend
        {
            set { _viewExtend = value; }
            get { return _viewExtend; }
        }

        #endregion

        #region 锁定行列，要锁定的行数

        /// <summary>
        /// 锁定行列，要锁定的行数（上面的行）。0表示不锁定
        /// </summary>
        public int LockRows { get; set; }

        #endregion

        #region 锁定行列，要锁定的列数

        /// <summary>
        /// 锁定行列，要锁定的列数（左面的列）。0表示不锁定
        /// </summary>
        public int LockColumns { get; set; }

        #endregion

        #region 锁定行列，div里面的表格的宽度

        /// <summary>
        /// 锁定行列，要锁定的列数（左面的列）。 
        /// </summary>
        public int TableWidth { get; set; }

        #endregion

        #endregion

        #region 其他属性
        #region 分页信息的元数据

        /// <summary>
        /// 分页信息的元数据
        /// </summary>
        public PageTurnMeta PageTurnMeta { get; set; }

        #endregion

        #region 列的信息

        /// <summary>
        /// 列的信息
        /// </summary>
        public Dictionary<int,ColumnMeta> ColumnMetas { get; set; }

        #endregion

        #endregion

        #region 初始化

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="drView">页面视图的记录集</param>
        /// <param name="drPageTurn">分页信息的记录集 </param>
        public PageViewMeta(DataRow drView,DataRow drPageTurn)
        {
            _pvid = (Int32) drView["PVID"];

            _title = drView["PVTitle"].ToString();

            //外键。列表、查询、添加、修改、删除、导出、选择记录、上传等
            _pvType = (PageViewType) (int) drView["PVTypeID"];

            _dataSourceTableName = drView["Table_DataSource"].ToString();
            _modiflyTableName = drView["Table_Modifly"].ToString();

            _modiflyTableID = int.Parse(drView["TableID_Modifly"].ToString());

            
            PKColumnID = int.Parse(drView["PKColumnID"].ToString());
            _pkColumn = drView["PKColumn"].ToString();
            _foreignColumn = drView["ForeignColumn"].ToString();

            //外键。操作方式：SQL、参数化SQL、存储过程
            SQLType = (SQLType) (int) drView["T_SQLTypeID"];

            //表单列数
            _columnCount = (Int32) drView["ColumnCount"];

            //锁定行列的信息
            LockRows = int.Parse(drView["LockRowCount"].ToString());
            LockColumns = int.Parse(drView["LockColumnCount"].ToString());  
            TableWidth = (Int32) drView["TableWidth"];

            _viewExtend = drView["ViewExtend"].ToString();

            //数据列表、选择记录的View，需要分页信息
            if (drPageTurn != null)
                PageTurnMeta = new PageTurnMeta(drView,drPageTurn);

        }

        #endregion
    }
}
