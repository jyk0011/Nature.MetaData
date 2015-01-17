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
 * function: 功能模块里的用于分页的属性
 * history:  created by 金洋  2010-1-7 9:02:51
 *           2011-4-11 整理
 * **********************************************
 */

using System;
using System.Data;
using Nature.MetaData.Enum;

namespace Nature.MetaData.Entity.QuickPager
{
    /// <summary>
    /// 功能模块里的用于分页的属性
    /// </summary>
    public class PageTurnMeta
    {
        #region 属性
        #region 显示数据用的表、视图 _TableNameList
        /// <summary>
        /// 显示数据用的表、视图
        /// </summary>
        public string TableNameList { get; set; }
        #endregion

        #region 列表页面的主键字段名 _PKColumn
        private string _pkColumn = "";
        /// <summary>
        /// 列表页面的主键字段名
        /// </summary>
        public string PKColumn
        {
            set { _pkColumn = value; }
            get { return _pkColumn; }
        }
        #endregion

        #region 要显示的字段 _ShowColumns
        private string _showColumns = "*";
        /// <summary>
        /// 要显示的字段
        /// </summary>
        public string ShowColumns
        {
            set { _showColumns = value; }
            get { return _showColumns; }
        }
        #endregion

        #region 排序字段 _OrderColumns
        /// <summary>
        /// 排序字段，比如 id ，或者 id desc ，或者 addDate desc ,id desc 
        /// </summary>
        public string OrderColumns { get; set; }

        #endregion

        #region 一页显示多少条记录 _PageSize
        private int _pageSize = 20;
        /// <summary>
        /// 一页显示多少条记录
        /// </summary>
        public int PageSize
        {
            set { _pageSize = value; }
            get { return _pageSize; }
        }
        #endregion

        #region 查询条件 _Query
        private string _query = "";
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Query
        {
            set { _query = value; }
            get { return _query; }
        }
        #endregion

        #region 固定查询条件 _QueryAlways
        private string _queryAlways = "";
        /// <summary>
        /// 固定查询条件
        /// </summary>
        public string QueryAlways
        {
            set { _queryAlways = value; }
            get { return _queryAlways; }
        }
        #endregion

        #region 页号导航的数量 _NaviCount
        private int _naviCount = 10;
        /// <summary>
        /// 页号导航的数量
        /// </summary>
        public int NaviCount
        {
            set { _naviCount = value; }
            get { return _naviCount; }
        }
        #endregion

        #region 分页算法 _SQLKindID
        private PageTurnType _pageTurnType = PageTurnType.Auto ;
        /// <summary>
        /// 分页算法
        /// </summary>
        public PageTurnType PageTurnType
        {
            set { _pageTurnType = value; }
            get { return _pageTurnType; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="drView">页面视图的记录集</param>
        /// <param name="drPageTurn">分页信息的记录集 </param>
        public PageTurnMeta(DataRow drView, DataRow drPageTurn)
        {
            if (drPageTurn["OrderColumns"] is DBNull)
            {
                return;
            }
            //分页控件需要的信息
            TableNameList = drView["Table_DataSource"].ToString();
            _pkColumn = drView["PKColumn"].ToString();
            _showColumns = "*"; // dr["ShowColumns"].ToString();
            OrderColumns = drPageTurn["OrderColumns"].ToString();
            _pageSize = (Int32)drPageTurn["PageSize"];
            _query = drPageTurn["Query"].ToString();
            _queryAlways = drPageTurn["QueryAlways"].ToString();
            _naviCount = (Int32)drPageTurn["NaviCount"];

            _pageTurnType = (PageTurnType)(int)drPageTurn["PageTurnTypeID"];

        }

        #endregion

    }
}
