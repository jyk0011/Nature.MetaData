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

using System.Collections.Generic;
using Nature.Common;
using Nature.MetaData.Enum;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 列表框的特殊属性
    /// 包括下拉列表框、列表框、复选框组、单选框组
    /// </summary>
    public class BaseListExpand : ControlExt
    {
        #region 属性

        #region Item的填充方式 ItemKind
        private readonly FillItemType _fillItemType ;
        /// <summary>
        /// Item的填充方式
        /// cus：自定义
        /// sql：从数据库里提取
        /// </summary>
        public FillItemType FillItemType
        {
            get { return _fillItemType; }
        }
        #endregion

        #region 提取item的SQL语句
        private readonly string _sql = "";
        /// <summary>
        /// Item的内容或者提取的SQL语句
        /// </summary>
        public string Sql
        {
            get { return _sql; }
        }
        #endregion

        #region 自定义Item的内容
        private readonly string _item = "";
        /// <summary>
        /// Item的内容或者提取的SQL语句
        /// </summary>
        public string Item
        {
            get { return _item; }
        }
        #endregion


        #region 列表框的宽度 _Width
        private readonly int _width;
        /// <summary>
        /// 列表框的宽度。
        /// 0：自动调整
        /// 其他：指定宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 列表框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public BaseListExpand(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            string key = "itemType"; //item的填充方式
            if (dicControlExt.ContainsKey(key))
            {
                switch (dicControlExt[key].ToLower())
                {
                    case "sql":
                        _fillItemType = FillItemType.SQL;
                        break;
                    case "cus":
                        _fillItemType = FillItemType.Customer;
                        break;
                    case "cache":
                        _fillItemType = FillItemType.Cache;
                        break;
                    case "self":
                        _fillItemType = FillItemType.Listself;
                        break;
                }
            }
            else
                Functions.MsgBox("没有找到列表框配置信息的" + key + "！<BR>", true);
 
            key = "width";
            _width = dicControlExt.ContainsKey(key) ? int.Parse(dicControlExt[key]) : 0;
            
            switch (_fillItemType)
            {
                case FillItemType.SQL:
                    key = "sql";
                    if (dicControlExt.ContainsKey(key))
                        _sql =  dicControlExt[key];
                    else
                        Functions.MsgBox("没有找到列表框配置信息的" + key + "！<BR>", true);
                    break;

                case FillItemType.Customer:
                    key = "item";
                    if (dicControlExt.ContainsKey(key))
                        _item = dicControlExt[key];
                    else
                        Functions.MsgBox("没有找到列表框配置信息的" + key + "！<BR>", true);
                    break;
            }
        }

        #endregion

    }
}

