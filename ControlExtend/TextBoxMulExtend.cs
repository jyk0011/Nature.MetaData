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
using Nature.Common;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 多行文本框的扩展
    /// </summary>
    public class TextBoxMulExtend : BaseTextBoxExtend
    {
        #region 属性
        
        #region 文本框在添加、修改的时候的行数 _ModRows
        private readonly int _rows ;
        /// <summary>
        /// 文本框在添加、修改的时候的行数，多行文本框有效
        /// </summary>
        public int Rows
        {
            get { return _rows; }
        }
        #endregion
 
        #endregion

        #region 初始化
        /// <summary>
        /// 单行文本框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public TextBoxMulExtend(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            const string key = "rows";
            if (dicControlExt.ContainsKey(key))
                _rows = int.Parse(dicControlExt[key]);
            else
                Functions.MsgBox("没有找到多行文本框配置信息的" + key + "！<BR>", true);
        }

        #endregion

    }
}
