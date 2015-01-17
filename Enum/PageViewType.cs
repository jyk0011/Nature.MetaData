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
 * function: 页面视图类型
 * history:  created by 金洋 2012-8-29
 * **********************************************
 */


namespace Nature.MetaData.Enum
{
    /// <summary>
    /// Manage_PageView表里的 PVTypeID 页面视图类型
    /// 列表、查询、添加、修改、删除、导出、选择记录、上传等
    /// </summary>
    public enum PageViewType
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        DataList = 701,
         /// <summary>
        /// 查询表单
        /// </summary>
        FindForm = 702,
        /// <summary>
        /// 添加、修改表单
        /// </summary>
        DataForm = 703,
        /// <summary>
        /// 删除
        /// </summary>
        DeleteData = 704,
        /// <summary>
        /// 导出
        /// </summary>
        OutputData = 705,
        /// <summary>
        /// 选择记录
        /// </summary>
        SelectRecords = 706,
        /// <summary>
        /// 上传文件
        /// </summary>
        UploadFiles = 707,
         /// <summary>
        /// 上传图片
        /// </summary>
        UploadPicture = 708

        
    }
}
