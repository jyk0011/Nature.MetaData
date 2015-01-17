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
    /// Manage_PageView表里的 T_SQLType 操作方式
    /// SQL、参数化SQL、存储过程
    /// </summary>
    /// user:jyk
    /// time:2012/8/29 16:13
    public enum SQLType
    {
        /// <summary>
        /// SQL语句
        /// </summary>
        SQL = 601,
         /// <summary>
        /// 参数化SQL语句
        /// </summary>
        ParameterSQL = 602,
        /// <summary>
        /// 存储过程
        /// </summary>
        StoredProcedure = 603 
        
    }
}
