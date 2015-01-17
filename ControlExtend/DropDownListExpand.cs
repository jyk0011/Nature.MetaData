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
 * function: 列表框的特殊属性
 * history:  created by 金洋 2009-12-29 10:18:59 
 *           2011-4-11 整理
 * **********************************************
 */

using System;
using System.Collections.Generic;
using Nature.Common;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 列表框的特殊属性
    /// 包括下拉列表框、列表框、复选框组、单选框组
    /// </summary>
    public class DropDownListExpand : BaseListExpand
    {
        #region 属性
     
        #region 在添加的时候是否显示“请选择”
        private readonly int _isShowChoose;
        /// <summary>
        /// Item在添加的时候是否显示“请选择”
        /// True：显示；False：不显示
        /// </summary>
        public int IsShowChoose
        {
            get { return _isShowChoose; }
        }
        #endregion
      
        #endregion

        #region 初始化
        /// <summary>
        /// 列表框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public DropDownListExpand(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            const string key = "isChange";
            //if (dicControlExt.ContainsKey(key))
            //    _isShowChoose = Convert.ToInt32(dicControlExt[key]) ;
            //else
            //    Functions.MsgBox("没有找到下拉列表框配置信息的" + key + "！<BR>", true);
        }

        #endregion

    }
}

