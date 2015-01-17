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
    #region 文件命名方式

    /// <summary>
    /// 文件命名方式
    /// </summary>
    public enum FileNameKind
    {
        /// <summary>
        /// 使用原来的文件名，如果有重名的后面加（1）的形式加以区别
        /// </summary>
        OriginalName = 1,

        /// <summary>
        /// 用户ID + _ + 日期时间的形式命名
        /// 1_20100101090508
        /// 2010年1月1日9点5分，08秒。
        /// </summary>
        UserIDTime = 2
    }

    #endregion

}
