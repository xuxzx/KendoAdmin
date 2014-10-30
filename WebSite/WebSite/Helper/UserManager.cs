using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Helper
{
    [Serializable()]
    public class UserSession
    {
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string TrueName { get; set; }
    }

    public static class UserManager
    {
        /// <summary>
        /// 创建保存到Session中的对象
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserSession CreateUserSession(int iUserID, string LoginName, string TrueName)
        {
            UserSession usersession = new UserSession();
            usersession.UserID = iUserID;
            usersession.LoginName = LoginName;
            usersession.TrueName = TrueName;
            return usersession;
        }
    }
}