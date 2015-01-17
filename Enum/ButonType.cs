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
 * function: 表单控件、查询控件 的类别
 * history:  created by 金洋 
 *           2011-4-11 整理
 * **********************************************
 */

namespace Nature.MetaData.Enum
{
    #region 表单控件数据操作方式

    /// <summary>
    /// Manage_ButtonList 表里的 BtnTypeID 按钮类型 
    /// 1：查看；2：添加数据；3：修改数据；4：添加后修改；5：查询；6：删除；7：导出Excel；8：导出Access；11：超链接
    /// </summary>
    public enum ButonType
    {
        /// <summary>
        /// 查看数据
        /// </summary>
        ViewData = 401,
        
        /// <summary>
        /// 添加数据
        /// </summary>
        AddData = 402,

        /// <summary>
        /// 修改数据
        /// </summary>
        UpdateData = 403,

        /// <summary>
        /// 删除数据
        /// </summary>
        /// user:jyk
        /// time:2012/9/12 16:24
        DeleteData = 404,
        
        /// <summary>
        /// 查询
        /// </summary>
        FindData = 405,
        
        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// user:jyk
        /// time:2012/9/12 16:24
        OutputExcel = 406,

        /// <summary>
        /// 导出到Access
        /// </summary>
        /// user:jyk
        /// time:2012/9/12 16:24
        OutpuAccess = 407,
        
        /// <summary>
        /// 如果没有数据，则先添加一条空数据，然后修改数据
        /// </summary>
        AddUpdateData = 408,
        
        /// <summary>
        /// 超链接
        /// </summary>
        Hyperlinks = 411,

    }

    #endregion

}
