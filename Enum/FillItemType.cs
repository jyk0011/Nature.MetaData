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
 * function: 操作方式
 * history:  created by 金洋 2012-8-29
 * **********************************************
 */


namespace Nature.MetaData.Enum
{
    /// <summary>
    /// 填写列表框的item的类型
    /// SQL、自定义、列表自带
    /// </summary>
    /// user:jyk
    /// time:2012/8/30 13:15
    public enum FillItemType
    {
        /// <summary>
        /// SQL语句
        /// </summary>
        SQL = 1,
         /// <summary>
        /// 参数化SQL语句
        /// </summary>
        Customer = 2,
        /// <summary>
        /// cache
        /// </summary>
        Cache = 3, 
        /// <summary>
        /// 自带
        /// </summary>
        Listself = 4
        
    }
}
