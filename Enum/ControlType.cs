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
    /// Manage_Columns 表里的 ControlTypeID 控件类型
    /// ControlKindID	ControlKind
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 单行文本
        /// </summary>
        SingleTextBox = 201,
         /// <summary>
        /// 多行文本
        /// </summary>
        MultipleTextBox = 202,
        /// <summary>
        /// 密码
        /// </summary>
        PasswordTextBox = 203,
        /// <summary>
        /// 日期
        /// </summary>
        DateTimeTextBox = 204,

        /// <summary>
        /// 上传文件
        /// </summary>
        UpdateFile = 205,
        /// <summary>
        /// 上传图片
        /// </summary>
        UpdatePicture = 206,

         /// <summary>
        /// 选择记录
        /// </summary>
        SelectRecords = 207,

        /// <summary>
        /// HTML_FCK
        /// </summary>
        FckEditor = 208,

        /// <summary>
        /// 下拉列表
        /// </summary>
        DropDownList = 250,
        
        /// <summary>
        /// 登录人
        /// </summary>
        Logon = 251,
        /// <summary>
        /// 级联列表
        /// </summary>
        UniteList = 252,
       
        /// <summary>
        /// 单选组
        /// </summary>
        RadioButtonList = 253,
        /// <summary>
        /// 多选组
        /// </summary>
        CheckBoxList = 254,
        
        /// <summary>
        /// CheckBox
        /// </summary>
        CheckBox = 255,

        /// <summary>
        /// 列表框
        /// </summary>
        ListBox = 256
        
    }
}
