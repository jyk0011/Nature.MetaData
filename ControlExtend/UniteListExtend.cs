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

/* ***********************************************
* author :  Administrator
* email  :  jyk0011@live.cn 
* function: 联动下拉列表框的信息
* history:  created by Administrator 2010-05-23 
* ***********************************************/


using System.Collections.Generic;
using Nature.Common;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 联动下拉列表框的个性信息
    /// </summary>
    public class UniteListExtend : BaseListExpand 
    {
        #region 属性

        #region 是否是第一个下拉列表框
        private readonly bool _isFristList = true;
        /// <summary>
        /// true：第一个下拉列表框
        /// false：不是第一个下拉列表框
        /// </summary>
        public bool IsFristList
        {
            get { return _isFristList; }
        }
        #endregion

        #region 联动级数
        private readonly int _listCount = 2;
        /// <summary>
        /// 下拉列表框的个数，即联动的级数
        /// </summary>
        public int ListCount
        {
            get { return _listCount; }
        }
        #endregion

        #region 联动的其他字段的ID
        private readonly int[] _listOtherColumnIDs  ;
        /// <summary>
        /// 联动的其他字段的ID
        /// </summary>
        public int[] ListOtherColumnIDs
        {
            get { return _listOtherColumnIDs; }
        }
        #endregion

        #region 下拉列表框的修饰
        private readonly string[] _listHtml  ;
        /// <summary>
        /// 下拉列表框的修饰
        /// 用数组的方式来存放修饰
        /// 比如 {"省份：","＜BR＞", "城市 ：","＜BR＞"}
        /// </summary>
        public string[] ListHtml
        {
            get { return _listHtml; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 选择框的特有属性的初始化
        /// sql|sql|0|
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public UniteListExtend(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            //sql|sql语句|标志|宽度|级数|colIDs|HTML间隔

            string key = "listCount";
            if (dicControlExt.ContainsKey(key))
                _listCount = int.Parse(dicControlExt[key]);
            //else
            //    Functions.MsgBox("没有找到联动下拉列表框配置信息的" + key + "！<BR>", true);

            key = "isFristList";
            if (dicControlExt.ContainsKey(key))
                _isFristList = dicControlExt[key] == "true" ;
            //else
            //    Functions.MsgBox("没有找到联动下拉列表框配置信息的" + key + "！<BR>", true);

            string tmp;
            key = "colIDs";
            if (dicControlExt.ContainsKey(key))
            {
                tmp = dicControlExt[key];
                //获取联动的其他字段的ID
                string[] tmpID = tmp.Split(',');

                _listOtherColumnIDs = new int[tmpID.Length];
                for (int i = 0; i < tmpID.Length; i++)
                {
                    _listOtherColumnIDs[i] = int.Parse(tmpID[i]);
                }
            }
            //else
            //    Functions.MsgBox("没有找到联动下拉列表框配置信息的" + key + "！<BR>", true);

            key = "listHtml";
            if (dicControlExt.ContainsKey(key))
            {
                tmp = dicControlExt[key];
                string[] tmpID = tmp.Split(',');

                _listHtml = new string[tmpID.Length];
                for (int i = 0; i < tmpID.Length; i++)
                {
                    _listHtml[i] = tmpID[i];
                }
            }
            //else
            //    Functions.MsgBox("没有找到联动下拉列表框配置信息的" + key + "！<BR>", true);



        }
        #endregion


    }
}
