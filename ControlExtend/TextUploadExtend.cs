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
* function: 文件上传控件的信息
* history:  created by Administrator 2010-05-18
* ***********************************************/


using System.Collections.Generic;
using Nature.Common;
using Nature.MetaData.Enum;

namespace Nature.MetaData.ControlExtend
{
    /// <summary>
    /// 上传文件用的一些信息
    /// </summary>
    public class TextUploadExtend : BaseTextBoxExtend
    {
        #region 属性

        #region 上传文件的方式 UploadKind
        private readonly FileUploadKind _uploadType = FileUploadKind.SiampleImage;
        /// <summary>
        /// 上传文件的方式
        /// </summary>
        public FileUploadKind UploadType
        {
            get { return _uploadType; }
        }
        #endregion

        #region 文件命名方式 _FileNameKind
        private readonly FileNameKind _fileNameKind = FileNameKind.UserIDTime;
        /// <summary>
        /// 文件命名方式
        /// </summary>
        public FileNameKind FileNameKind
        {
            get { return _fileNameKind; }
        }
        #endregion

        #region 文件存放路径 _FilePath
        private readonly string _filePath = "";
        /// <summary>
        /// 文件存放路径，从网站根目录算起
        /// 比如/img/Big
        /// 或者/upload/file
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }
        #endregion


        #endregion

        #region 初始化
        /// <summary>
        /// 选择框的特有属性的初始化
        /// </summary>
        /// <param name="dicControlExt">配置信息里的ControlInfo的内容</param>
        public TextUploadExtend(Dictionary<string, string> dicControlExt)
            : base(dicControlExt)
        {
            string key = "uploadType";
            if (dicControlExt.ContainsKey(key))
                _uploadType = (FileUploadKind)(int.Parse(dicControlExt[key]));
            else
                Functions.MsgBox("没有找到上传配置信息的" + key + "！<BR>", true);

            key = "fileNameKind";
            if (dicControlExt.ContainsKey(key))
                _fileNameKind = (FileNameKind)(int.Parse(dicControlExt[key]));
            else
                Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);
            key = "filePath";
            if (dicControlExt.ContainsKey(key))
                _filePath = dicControlExt[key];
            else
                Functions.MsgBox("没有找到选择框配置信息的" + key + "！<BR>", true);


        }
        #endregion

    }
}

