using System;
using System.Data;

namespace Nature.MetaData.Entity.WebPage
{
    /// <summary>
    /// 模块的实体类，即菜单的实体类
    /// </summary>
    public class ModuleEntity:IColumn 
    {
        #region 属性

        /// <summary>
        /// 加入的集合里的key——模块ID
        /// </summary>
        public int Key { get; set; }


        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 父级ID集合.逗号分割所有父级ID，格式【-9,****,-9】
        /// </summary>
        public string ParentIDAll { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块层级
        /// </summary>
        public int ModuleLevel { get; set; }

        /// <summary>
        /// 菜单里的图标ID
        /// </summary>
        public int IconID { get; set; }

        /// <summary>
        /// 打开网页的地址
        /// </summary>
        public string  URL { get; set; }

        /// <summary>
        /// 模块打开目标
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 是否叶节点,0：不是；1：是
        /// </summary>
        public bool IsLeaf { get; set; }
        
        /// <summary>
        /// 是否隐藏，0=正常显示；1=菜单隐藏；2=不可更新；3=提示不可用(更新程序时使用)
        /// </summary>
        public int GridPageViewID { get; set; }

        /// <summary>
        /// 是否隐藏，0=正常显示；1=菜单隐藏；2=不可更新；3=提示不可用(更新程序时使用)
        /// </summary>
        public int FindPageViewID { get; set; }

        /// <summary>
        /// 是否隐藏，0=正常显示；1=菜单隐藏；2=不可更新；3=提示不可用(更新程序时使用)
        /// </summary>
        public int IsHidden { get; set; }

        /// <summary>
        /// 是否锁定,配置信息是否可以修改，非专业人别动。0：可以；1：不可以
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 排序，所有节点全排序
        /// </summary>
        public int DisOrder { get; set; }

        #endregion

        #region 初始化

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="dr">记录集</param>
        public ModuleEntity(DataRow dr)
        {
            ModuleID = (Int32) dr["ModuleID"];
            ParentID = (Int32) dr["ParentID"];
            ParentIDAll = dr["ParentIDAll"].ToString();
            ModuleName = dr["ModuleName"].ToString();
            ModuleLevel = int.Parse(dr["ModuleLevel"].ToString());
            IconID = (Int32) dr["IconID"];
            URL = dr["URL"].ToString();
            Target = dr["Target"].ToString();
            IsLeaf = (bool) dr["IsLeaf"];

            GridPageViewID = (Int32)dr["GridPageViewID"];
            FindPageViewID = (Int32)dr["FindPageViewID"];
          
            IsHidden = int.Parse(dr["IsHidden"].ToString());
            IsLock = (bool) dr["IsLock"];
            DisOrder = (Int32) dr["DisOrder"];

            Key = ModuleID;
        }

        #endregion


    }
}
