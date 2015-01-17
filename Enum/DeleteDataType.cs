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
 * function: 删除数据方式的类型
 * history:  created by 金洋 2012-8-29
 * **********************************************
 */


namespace Nature.MetaData.Enum
{
    /// <summary>
    /// Manage_PageView 表里的 DeleteDataKindID字段
    /// 删除数据的方式。1：简单删除；2：存储过程；3：自定义删除
    /// </summary>
    public enum DeleteDataKind
    {
        /// <summary>
        /// 简单删除，使用SQL语句物理删除数据
        /// </summary>
        SqlPhysical = 301,

        /// <summary>
        /// 简单删除，使用SQL语句逻辑删除数据
        /// </summary>
        SqlLogic= 302,

        /// <summary>
        /// 使用储存过程删除数据
        /// </summary>
        StoredProcedure = 303,

        /// <summary>
        /// 自定义删除，使用子类删除数据
        /// </summary>
        Customer = 304
    }
}
