/**
 * 自然框架之服务器控件扩展
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
 * 自然框架之服务器控件扩展 is free software. You are allowed to download, modify and distribute 
 * the source code in accordance with LGPL 2.1 license, however if you want to use 
 * 自然框架之服务器控件扩展 on your site or include it in your commercial software, you must  be registered.
 * http://www.natureFW.com/registered
 */

/* ***********************************************
 * author :  金洋（金色海洋jyk）
 * email  :  jyk0011@live.cn  
 * function: 表单控件和查询控件的基类
 * history:  created by 金洋 
 *           2011-4-11 整理
 * **********************************************
 */

/* ***********************************************
* author :  Administrator
* email  :  jyk0011@live.cn 
* function: 文本框的特殊属性
* history:  created by Administrator 2009-12-29 10:18:59 
* ***********************************************/

using System.Collections.Generic;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 日期控件的扩展
    /// </summary>
    public class TextBoxTimeExtend : BaseTextBoxExtend
    {
        #region 属性
        
        #region 触发日期控件的事件
     
        /// <summary>
        /// 触发日期控件的事件
        /// </summary>
        public string EventName { get; set; }

        #endregion

        #region 触发日期控件的事件的参数值

        /// <summary>
        /// 触发日期控件的事件的参数值
        /// </summary>
        public string Parameter { get; set; }

        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 单行文本框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public TextBoxTimeExtend(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            string key = "event";
            EventName = dicControlExt.ContainsKey(key) ? dicControlExt[key] : "onClick";

            key = "parameter";
            Parameter = dicControlExt.ContainsKey(key) ? dicControlExt[key] : "";

        }

        #endregion

    }
}
