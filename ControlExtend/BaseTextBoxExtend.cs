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
    /// 单行文本框的扩展
    /// </summary>
    public class BaseTextBoxExtend : ControlExt
    {
        #region 属性

        #region 添加、修改的时候的显示宽度_modWidth
        private readonly int _modWidth = 200;
        /// <summary>
        /// 文本框在添加、修改的时候的显示宽度，单位:px。
        /// </summary>
        public int ModWidth
        {
            get { return _modWidth; }
        }
        #endregion

        #region 添加、修改的时候的最大字符数 _ModMaxLength
        private readonly int _modMaxLength;
        /// <summary>
        /// 文本框在添加、修改的时候的最大字符数，单行文本框有效
        /// </summary>
        public int ModMaxLength
        {
            get { return _modMaxLength; }
        }
        #endregion
         
        #region 查询的时候的“宽度”_findWidth
        private readonly int _findWidth = 10;
        /// <summary>
        /// 文本框在查询的时候的显示宽度，以字符为单位。密码框无效
        /// </summary>
        public int FindWidth
        {
            get { return _findWidth; }
        }
        #endregion


        #region 文本框在查询的时候的最大字符数 _FindMaxLength
        private readonly int _findMaxLength = 10;
        /// <summary>
        /// 文本框在查询的时候的最大字符数。密码框无效
        /// </summary>
        public int FindMaxLength
        {
            get { return _findMaxLength; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 单行文本框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public BaseTextBoxExtend(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            string key = "modWidth";
            if (dicControlExt.ContainsKey(key))
            {
                if (!Functions.IsInt(dicControlExt[key]))
                    Functions.MsgBox( key + "的格式不正确！必须是整数<BR>", true);

                _modWidth = int.Parse(dicControlExt[key]);
            }
            else
                Functions.MsgBox("没有找到单行文本框配置信息的" + key + "！<BR>", true);

            key = "modMaxLen";
            if (dicControlExt.ContainsKey(key))
            {
                if (!Functions.IsInt(dicControlExt[key]))
                    Functions.MsgBox(key + "的格式不正确！必须是整数<BR>", true);
                _modMaxLength = int.Parse(dicControlExt[key]);
            }
            else
                Functions.MsgBox("没有找到单行文本框配置信息的" + key + "！<BR>", true);

            key = "findWidth";
            if (dicControlExt.ContainsKey(key))
            {
                if (!Functions.IsInt(dicControlExt[key]))
                    Functions.MsgBox(key + "的格式不正确！必须是整数<BR>", true);
                _findWidth = int.Parse(dicControlExt[key]);
            }
            else
                Functions.MsgBox("查询状态下没有找到文本框配置信息的" + key + "！<BR>", true);

            key = "findMaxLen";
            if (dicControlExt.ContainsKey(key))
            {
                if (!Functions.IsInt(dicControlExt[key]))
                    Functions.MsgBox(key + "的格式不正确！必须是整数<BR>", true);
                _findMaxLength = int.Parse(dicControlExt[key]);
            }
            else
                Functions.MsgBox("查询状态下没有找到文本框配置信息的" + key + "！<BR>", true);


        }

        #endregion

    }
}
