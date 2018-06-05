using Newtonsoft.Json;
using System;
using System.Web;

namespace LeeFuns.Utilities
{
    public class ManageProvider : IManageProvider
    {
        private string LoginProvider = ConfigHelper.AppSettings("LoginProvider");
        private string LoginUserKey = "LoginUserKey";

        public virtual void AddCurrent(IManageUser user)
        {
            try
            {
                if (this.LoginProvider == "Cookie")
                {
                    CookieHelper.WriteCookie(this.LoginUserKey, DESEncrypt.Encrypt(JsonConvert.SerializeObject(user)), 0x5a0);
                }
                else
                {
                    SessionHelper.Add(this.LoginUserKey, DESEncrypt.Encrypt(JsonConvert.SerializeObject(user)));
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual IManageUser Current()
        {
            IManageUser user2;
            try
            {
                IManageUser user = new IManageUser();
                if (this.LoginProvider == "Cookie")
                {
                    user = JsonConvert.DeserializeObject<IManageUser>(DESEncrypt.Decrypt(CookieHelper.GetCookie(this.LoginUserKey)));
                }
                else
                {
                    user = JsonConvert.DeserializeObject<IManageUser>(DESEncrypt.Decrypt(SessionHelper.Get(this.LoginUserKey).ToString()));
                }
                if (user == null)
                {
                    throw new Exception("登录信息超时，请重新登录。");
                }
                user2 = user;
            }
            catch
            {
                throw new Exception("登录信息超时，请重新登录。");
            }
            return user2;
        }

        public virtual void EmptyCurrent()
        {
            if (this.LoginProvider == "Cookie")
            {
                HttpCookie cookie = new HttpCookie(this.LoginUserKey.Trim()) {
                    Expires = DateTime.Now.AddYears(-5)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                SessionHelper.Remove(this.LoginUserKey.Trim());
            }
        }

        public virtual bool IsOverdue()
        {
            object cookie = "";
            if (this.LoginProvider == "Cookie")
            {
                cookie = CookieHelper.GetCookie(this.LoginUserKey);
            }
            else
            {
                cookie = SessionHelper.Get(this.LoginUserKey);
            }
            return ((cookie != null) && (cookie.ToString() != ""));
        }

        public static IManageProvider Provider
        {
            get
            {
                return new ManageProvider();
            }
        }
    }
}

