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
 * function: 表单的描述信息
 * history:  created by 金洋 2009-8-27 9:02:51 
 *           2011-4-11 整理
 * **********************************************
 */

using System;
using System.Collections.Generic;
using System.Data;
using Nature.Common;
using Nature.MetaData.ControlExtend;
using Nature.MetaData.Enum;

namespace Nature.MetaData.Entity.MetaControl
{
    /// <summary>
    /// 表单的描述信息
    /// </summary>
    public class FormColumnMeta : ColumnWebMeta
    {
        #region 属性
        //添加修改、查询共用的属性
        #region 控件类型 _ControlKind
        private readonly ControlType _controlKind = ControlType.SingleTextBox ;
        /// <summary>
        /// 字段对应的控件类型(编号)，比如 201（表示单行文本）
        /// </summary>
        public ControlType ControlKind
        {
            get { return _controlKind; }
        }
        #endregion

        #region 控件的扩展信息 _controlExt
        private readonly ControlExt _controlExt;
        /// <summary>
        /// 控件的扩展信息，宽、高、最大字符数等。
        /// </summary>
        public ControlExt ControlExtend
        {
            get { return _controlExt; }
        }
        #endregion

        #region 默认值 _DefaultValue
        private readonly string _defaultValue = "";
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get { return _defaultValue; }
        }
        #endregion

        #region 控件状态 _ControlState
        private readonly string _controlState = "";
        /// <summary>
        /// 控件的状态。1：正常；2：只读；3：不可用；4：隐藏
        /// </summary>
        public string ControlState
        {
            get { return _controlState; }
        }
        #endregion


        //布局，也是共用的
        #region 是否合并到上一个TD _TDStart
        private readonly Int32 _tdStart;
        /// <summary>
        /// 是否合并到上一个TD。0：不合并；其他：合并后添加几个空格（用于占位）;
        /// </summary>
        public Int32 TdStart
        {
            get { return _tdStart; }
        }
        #endregion

        #region 是否把下一个字段合并上来 _TDEnd
        private readonly bool _tdEnd;
        /// <summary>
        /// 是否把下一个字段合并上来。false：不合并；true：合并
        /// </summary>
        public bool TdEnd
        {
            get { return _tdEnd; }
        }
        #endregion

        #region 一个控件占用几个TD _TDColspan
        private readonly Int32 _tdColspan = 1;
        /// <summary>
        /// 记录一个控件占用几个TD
        /// </summary>
        public Int32 TdColspan
        {
            get { return _tdColspan; }
        }
        #endregion

        #endregion

        #region 初始化
        /// <summary>
        /// 通过传递过来的DataTable来给属性赋值
        /// </summary>
        public FormColumnMeta(DataRow dr)
            : base(dr)
        {
            //布局
            _tdStart = (Int32)dr["clearTDStart"];
            _tdEnd = dr["clearTDEnd"].ToString() != "0";
            _tdColspan = (Int32)dr["TDColspan"];

            //控件信息
            _defaultValue = dr["DefaultValue"].ToString();
            _controlState = dr["ControlState"].ToString();

            _controlKind = (ControlType)(Int32)dr["ControlTypeID"];

            //设置控件的特殊属性
            ControlExt ctrlext = null;
            string strInfo = dr["ControlInfo"].ToString();

            Dictionary<string, string> dicControlExt = Json.JsonToDictionary(strInfo);

            switch (_controlKind)
            {
                case ControlType.SingleTextBox:   //201单行文本框
                    ctrlext = new BaseTextBoxExtend(dicControlExt);
                    break;
                case ControlType.MultipleTextBox:   //202多行文本框
                    ctrlext = new TextBoxMulExtend(dicControlExt);
                    break;
                case ControlType.PasswordTextBox:   //203密码框
                    ctrlext = new TextBoxPassExtend(dicControlExt);
                    break;

                case ControlType.DateTimeTextBox :   //204日期 
                    ctrlext = new TextBoxTimeExtend(dicControlExt);
                    break;

                case ControlType.UpdateFile:   //205上传文件
                case ControlType.UpdatePicture:   //206上传图片
                    ctrlext = new TextUploadExtend(dicControlExt);
                    break;
                case ControlType.SelectRecords:   //207选择记录
                    ctrlext = new TextChooseExpand(dicControlExt);
                    break;
                case ControlType.FckEditor:   //208FCK
                    ctrlext = new BaseTextBoxExtend(dicControlExt);
                    break;

                case ControlType.DropDownList:   //250下拉列表框
                    ctrlext = new DropDownListExpand(dicControlExt);
                    break;
                
                case ControlType.Logon:   //251登录人
                    ctrlext = new BaseListExpand(dicControlExt);
                    break;
                case ControlType.UniteList:   //252级联下拉列表框
                    ctrlext = new UniteListExtend(dicControlExt);
                    break;
                
                case ControlType.RadioButtonList:  //253单选组
                case ControlType.CheckBoxList:   //254复选组
                    ctrlext = new GroupListExpand(dicControlExt);
                    break;
                
                case ControlType.CheckBox:   //255复选框
                    break;

                case ControlType.ListBox:   //256 列表框
                    ctrlext = new BaseListExpand(dicControlExt );
                    break;
                
            }

            _controlExt = ctrlext;

        }
        #endregion
    }
}
