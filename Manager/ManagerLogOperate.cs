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
 * function: 记录操作情况。访问列表、添加记录、修改记录、删除记录，等等
 * history:  created by 金洋 2013-09-26 09:08:34 
 *           
 * **********************************************
 */


using System.Collections.Generic;
using System.Web;
using Nature.Data;
using Nature.DebugWatch;

namespace Nature.MetaData.Manager
{
    /// <summary>
    /// 管理日志，日志的添加
    /// 本来想单独写个类的，但是不想在弄一套属性了，就借用 ManagerData 了
    /// </summary>
    public class ManagerLogOperate
    {
        #region 要操作的数据库的访问实例
        /// <summary>
        /// 要操作的数据库的访问实例
        /// </summary>
        /// user:jyk
        /// time:2012/9/5 17:06
        public DataAccessLibrary Dal { get; set; }
        #endregion


        #region 模块ID
        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID { get; set; }
        #endregion

        #region 视图ID
        /// <summary>
        /// 视图ID
        /// </summary>
        public int PageViewID { get; set; }
        #endregion

        #region 按钮ID
        /// <summary>
        /// 按钮ID
        /// </summary>
        public int ButtonID { get; set; }
        #endregion

        #region 操作类型
        /// <summary>
        /// 操作类型
        /// <para>11：替换列表里的标识；12：翻页；13：获取指定项目的元数据；14：角色记录；15：全部记录，不分页；16：一条记录；</para>
        /// <para>17：获取用户信息；18：下一个分类的序号；19：获取服务器的年月日；20：获取服务器的年月日 时分；21：获取服务器的小时和分钟；</para>
        /// <para>22：获取上传图片的图片ID集合；23：删除指定的图片；24：没有这个action；25：获取服务器的年月</para>
        /// <para>31：打开表单添加；32：打开表单修改；</para>
        /// <para>51：添加数据；52：修改数据；</para>
        /// <para>53：物理删除；54：逻辑删除；</para>
        /// </summary>
        public int OperateKind { get; set; }
        #endregion

        #region 操作类型
        /// <summary>
        /// 操作类型
        /// 1：成功；2：失败；3：数据验证失败；4：保存时出现异常；5：修改数据时出现异常；6：删除时没有权限
        /// </summary>
        public int State { get; set; }
        #endregion

        #region 添加人ID
        /// <summary>
        /// 添加人ID
        /// </summary>
        public int AddUserID { get; set; }
        #endregion

        #region 操作日志ID
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public string OperateLogID { get; set; }
        #endregion


        #region 添加操作日志ID
        /// <summary>
        /// OperateKind:
        /// <para>11：替换列表里的标识；12：翻页；13：获取指定项目的元数据；14：角色记录；15：全部记录，不分页；16：一条记录；</para>
        /// <para>17：获取用户信息；18：下一个分类的序号；19：获取服务器的年月日；20：获取服务器的年月日 时分；21：获取服务器的小时和分钟；</para>
        /// <para>22：获取上传图片的图片ID集合；23：删除指定的图片；24：没有这个action；</para>
        /// <para>31：打开表单添加；32：打开表单修改；</para>
        /// <para>51：添加数据；52：修改数据；</para>
        /// <para>53：物理删除；54：逻辑删除；</para>
        /// <para>State:</para>
        /// <para>1：成功；2：失败；3：数据验证失败；4：保存时出现异常；5：修改数据时出现异常；6：删除时没有权限</para>
        /// </summary>
        /// <returns></returns>

        public string WriteOperateLog(IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "添加操作日志" };
            debugInfoList.Add(debugInfo);

            const string sql = "insert into Manage_Log_Operate (ModuleID,PageViewID,ButtonID,OperateKind,State,Url,IP,AddUserid) values (@ModuleID,@PageViewID,@ButtonID,@OperateKind,@State,@Url,@IP,@AddUserid) select scope_identity() as newID";

            DataAccessLibrary dal = DalFactory.CreateDal(Dal.ConnectionString, Dal.ProviderName);

            //
            dal.ManagerParameter.AddNewInParameter("ModuleID", ModuleID);
            dal.ManagerParameter.AddNewInParameter("PageViewID", PageViewID);
            dal.ManagerParameter.AddNewInParameter("ButtonID", ButtonID);
            dal.ManagerParameter.AddNewInParameter("OperateKind", OperateKind);
            dal.ManagerParameter.AddNewInParameter("State", State);
            dal.ManagerParameter.AddNewInParameter("Url", HttpContext.Current.Request.Url.ToString(),300);
            dal.ManagerParameter.AddNewInParameter("IP", HttpContext.Current.Request.UserHostAddress,15);
            dal.ManagerParameter.AddNewInParameter("AddUserid", AddUserID);

            OperateLogID = dal.ExecuteString(sql);

            debugInfo.Stop();

            return "";

        }
        #endregion

        #region 修改操作日志的状态

        /// <summary>
        /// 修改操作日志的状态
        /// </summary>
        /// <param name="state">1：成功；2：失败；3：数据验证失败；4：保存时出现异常；5：修改数据时出现异常；6：删除时没有权限</param>
        /// <param name="debugInfoList"></param>
        /// <returns></returns>
        public string UpdateOperateLogState(int state,IList<NatureDebugInfo> debugInfoList)
        {
            var debugInfo = new NatureDebugInfo { Title = "修改操作日志的状态" };
            debugInfoList.Add(debugInfo);

            const string sql = "update Manage_Log_Operate set State = @State where OperateID = @OperateID";

            DataAccessLibrary dal = DalFactory.CreateDal(Dal.ConnectionString, Dal.ProviderName);

            //
            dal.ManagerParameter.AddNewInParameter("OperateID", OperateLogID);
            dal.ManagerParameter.AddNewInParameter("State", State);

            dal.ExecuteString(sql);

            debugInfo.Remark = sql + "_state:" + state + "_OperateID:" + OperateLogID;

            debugInfo.Stop();
            return "";
        }

        #endregion
    }
}
