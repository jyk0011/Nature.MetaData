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
 * function: 控件信息的抽象基类
 * history:  created by 金洋 
 *           2011-4-11 整理
 * **********************************************
 */

using System.Collections.Generic;

namespace Nature.MetaData
{
    /// <summary>
    /// 控件信息的抽象基类
    /// </summary>
    public abstract class ControlExt
    {
        /// <summary>
        /// 控件信息的抽象基类
        /// 记录控件的个性的属性
        /// </summary>
        /// <param name="dicControlExt">字典形式的控件信息</param>
        public ControlExt(Dictionary<string, string> dicControlExt)
        {
        }

    }
}
