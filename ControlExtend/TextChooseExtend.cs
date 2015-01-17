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
 * function: 单击文本框，选择记录，保存记录ID
 * history:  created by 金洋 2009-12-29 10:18:59
 *           2011-4-11 整理
 * **********************************************
 */

using System.Collections.Generic;
using Nature.Common;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 单击文本框选择记录，保存记录ID的控件的属性
    /// </summary>
    public class TextChooseExpand : BaseTextBoxExtend
    {
        #region 属性

        #region 单击文本框后打开的网页的网址 URL
        private readonly string _url = "";
        /// <summary>
        /// 单击文本框后打开的网页的网址
        /// </summary>
        public string URL
        {
            get { return _url; }
        }
        #endregion

        #region 传递的viewID _pageViewID
        private readonly int _pageViewID ;
        /// <summary>
        /// 传递的viewID
        /// </summary>
        public int PageViewID
        {
            get { return _pageViewID; }
        }
        #endregion

        #region 打开窗口的宽度 _Width
        private readonly int _width ;
        /// <summary>
        /// 打开窗口的宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
        }
        #endregion

        #region 打开窗口的高度 _Height
        private readonly int _height  ;
        /// <summary>
        /// 打开窗口的宽度
        /// </summary>
        public int Height
        {
            get { return _height; }
        }
        #endregion


        #endregion

        #region 初始化
        /// <summary>
        /// 选择框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public TextChooseExpand(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {

            string key = "url";
            //if (dicControlExt.ContainsKey(key))
            //    _url = dicControlExt[key];
            //else
            //    Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);

            //key = "url";
            //if (dicControlExt.ContainsKey(key))
            //    _url = dicControlExt[key];
            //else
            //    Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);

            //key = "openWidth";
            //if (dicControlExt.ContainsKey(key))
            //    _width = int.Parse(dicControlExt[key]);
            //else
            //    Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);

            //key = "openHeight";
            //if (dicControlExt.ContainsKey(key))
            //    _height = int.Parse(dicControlExt[key]);
            //else
            //    Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);

            //key = "_pageViewID";
            //if (dicControlExt.ContainsKey(key))
            //    _pageViewID = int.Parse( dicControlExt[key]);
            //else
            //    Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);

        }
        #endregion

    }
}
