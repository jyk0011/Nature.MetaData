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
 * function: 表单控件、查询控件 的类别
 * history:  created by 金洋 2012-9-30
 *            
 * **********************************************
 */

namespace Nature.MetaData.Enum
{
    #region 上传文件的方式

    /// <summary>
    /// 上传文件的方式
    /// </summary>
    public enum FileUploadKind
    {
        /// <summary>
        /// 上传一个图片
        /// </summary>
        SiampleImage = 1,

        /// <summary>
        /// 上传大图、小图
        /// </summary>
        BigImag = 2,

        /// <summary>
        /// 一次上传多个图片
        /// </summary>
        MuliImage = 3,

        /// <summary>
        /// 上传一个文件
        /// </summary>
        SiampleFile = 4,

        /// <summary>
        /// 上传多个文件
        /// </summary>
        MuliFile = 5


    }

    #endregion

}
