using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Web;
using Microsoft.SharePoint;
using MR.SP.DueDiligence.Framework;
using MR.SP.DueDiligence.Framework.Const;
using MR.SP.DueDiligence.Framework.Entity;

namespace MR.SP.DueDiligence.Framework
{
    public sealed class DDContext
    {
        //Global level context which include current login user and related information
        private const string ContextPropertyName = "DD_Context";
        private HttpContext _context;

        private DDContext(HttpContext context)
        {
            _context = context;

        }
        /// <summary>
        /// AD DOMAIN
        /// </summary>
        public static class DOMAIN
        {
            public const string SHAREPOINT = "SHAREPOINT\\";
            public const string MUNDIPHARMA = "mundipharma";
        }
        public static string SYSTEMISID = "system";
        /// <summary>
        /// 
        /// </summary>
        public static DDContext Current
        {
            get
            {
                DDContext context = null;
                if (HttpContext.Current != null)
                {
                    context = GetDDContext(HttpContext.Current);
                }
                return context;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static DDContext GetDDContext(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            var gContext = (DDContext)context.Items[ContextPropertyName];
            if (gContext == null)
            {
                gContext = new DDContext(context);
                context.Items[ContextPropertyName] = gContext;
            }

            return gContext;
        }
        /// <summary>
        /// 
        /// </summary>
        public SPContext SPContext
        {
            get
            {
                try
                {
                    SPContext context = SPContext.Current;
                    if (context == null)
                    {
                        context = SPContext.GetContext(HttpContext.Current);
                    }
                    return context;
                }
                catch
                {
                    return null;
                }
            }
        }

        private UserInfo _loginUser;

        /// <summary>
        /// Login User
        /// </summary>
        public UserInfo LoginUser
        {
            get
            {
                if (null == _loginUser)
                {
                    _loginUser = new UserInfo();
                    _loginUser.LoginId = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                    _loginUser.Name = SPContext.Current.Web.CurrentUser.Name;
                    _loginUser.Email = SPContext.Current.Web.CurrentUser.Email;
                    //var result = DDContext.LoadPropertiesFromUserProfile(_loginUser);
                    //load user roles
                    DDContext.LoadUserRoles(_loginUser);
                }

                return _loginUser;
            }
            set
            {
                _loginUser = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginAccount"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByLoginAccount(string loginAccount)
        {
            UserInfo userinfo = new UserInfo();
            try
            {
                if (loginAccount.ToLower() == SYSTEMISID.ToLower())
                {
                    SPUser user = SPContext.Current.Web.EnsureUser(DOMAIN.SHAREPOINT + loginAccount);
                    userinfo.LoginId = loginAccount;
                    userinfo.Name = user.Name;
                    userinfo.Email = user.Email;
                }
                else
                {
                    string domainPrefix = DOMAIN.MUNDIPHARMA;
                    if (domainPrefix.EndsWith("\\")) domainPrefix.Substring(0, domainPrefix.Length - 1); //remove last slash if existed
                    string userIdWithDomain = string.Format(@"{0}\{1}", domainPrefix, loginAccount);
                    SPUser user = SPContext.Current.Web.EnsureUser(userIdWithDomain);
                    userinfo.LoginId = loginAccount;
                    userinfo.Name = user.Name;
                    userinfo.Email = user.Email;
                }
            }
            catch (Exception ex)
            {

            }
            return userinfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool LoadUserRoles(UserInfo user)
        {
            bool result = false;
            try
            {
                EnumRole role = EnumRole.None;
                //Check Normal permission
                role = role | EnumRole.Normal;
                //Check Planner permission
                //Check Business admin permission
                bool isParticipant = false;
                bool isAdmin = false;
                CheckUserPermission(user, out isParticipant, out isAdmin);
                if (isParticipant) role = role | EnumRole.Participant;
                if (isAdmin) role = role | EnumRole.Admin;
                user.Role = role;
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isParticipant"></param>
        /// <param name="isAdmin"></param>
        private static void CheckUserPermission(UserInfo user, out bool isParticipant, out bool isAdmin)
        {
            isParticipant = false;
            isAdmin = false;
            int adminGroupId = -1;
            List<int> participantGroupsId = new System.Collections.Generic.List<int>();

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {

                    using (SPWeb curWeb = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPGroup adminGroup = curWeb.SiteGroups.GetByName(GroupName.AdminGroup);
                        if (adminGroup != null) adminGroupId = adminGroup.ID;
                        foreach (string gpName in GroupName.GroupsHasRightsOnProject)
                        {

                            try
                            {
                                SPGroup partGroup = curWeb.SiteGroups.GetByName(gpName);
                                if (partGroup != null) participantGroupsId.Add(partGroup.ID);
                            }
                            catch (Exception inEx)
                            {
                                //Group is not existed
                                //throw;
                            }
                        }
                    }

                }
            });

            if (participantGroupsId.Count >= 0)
            {
                foreach (int partGroupId in participantGroupsId)
                {
                    if (SPContext.Current.Web.IsCurrentUserMemberOfGroup(partGroupId))
                    {
                        isParticipant = true;
                        break;
                    }
                }
            }

            if (SPContext.Current.Web.IsCurrentUserMemberOfGroup(adminGroupId))
            {
                isAdmin = true;
            }
            else if (SPContext.Current.Web.CurrentUser.IsSiteAdmin || SPContext.Current.Web.UserIsSiteAdmin || SPContext.Current.Web.UserIsWebAdmin)
            {
                isAdmin = true;
            }
        }

        private string _dateFormat;
        /// <summary>
        /// Get Current site date format
        /// </summary>
        /// <returns></returns>
        public string RegionalDateFormat
        {
            get
            {
                if (string.IsNullOrEmpty(_dateFormat))
                {
                    //SPUser user = SPContext.Current.Web.CurrentUser;
                    //SPWeb web = SPContext.Current.Web;
                    ////get date format
                    //string timeZoneInformation = string.Empty; //like:(UTC-05:00) Eastern Time (US and Canada)
                    //if (user != null && user.RegionalSettings != null && user.RegionalSettings.TimeZone != null)
                    //{
                    //    //_dateFormat = user.RegionalSettings.Locales[0].
                    //    //timeZoneInformation = user.RegionalSettings.TimeZone.Description;
                    //    //user.RegionalSettings.DateFormat
                    //    //DateTime.Now.ToString()
                    //}
                    //else if (web != null && web.RegionalSettings != null && web.RegionalSettings.TimeZone != null)
                    //{
                    //    timeZoneInformation = web.RegionalSettings.TimeZone.Description;
                    //}
                    _dateFormat = "dd/MM/yyyy";
                }

                return _dateFormat;
            }
            set
            {
                _dateFormat = value;
            }
        }

        #region Regional Methods
        /// <summary>
        /// Get Date time string with regional name
        /// </summary>
        /// <param name="web"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetTimeStringBasedOnRegionalSetting(SPWeb web, SPUser user)
        {

            DateTime result = GetCurrentRegionalTime(web, user);
            string timeZoneInformation = string.Empty; //like:(UTC-05:00) Eastern Time (US and Canada)
            if (user != null && user.RegionalSettings != null && user.RegionalSettings.TimeZone != null)
            {
                timeZoneInformation = user.RegionalSettings.TimeZone.Description;
            }
            else if (web != null && web.RegionalSettings != null && web.RegionalSettings.TimeZone != null)
            {
                timeZoneInformation = web.RegionalSettings.TimeZone.Description;
            }
            timeZoneInformation = timeZoneInformation.IndexOf(" ") > 0
                                          ? timeZoneInformation.Substring(timeZoneInformation.IndexOf(" "))
                                          : timeZoneInformation;
            
            if (string.Compare(timeZoneInformation, " Eastern Time (US and Canada)", true) == 0)
                timeZoneInformation = " EST";
            return result.ToString("ddd MMM dd hh:mm:ss yyyy") + timeZoneInformation;
        }

        protected DateTime GetCurrentRegionalTime(SPWeb web, SPUser user)
        {
            DateTime result = DateTime.UtcNow;
            result = ParseToRegionalTime(web, user, result);

            return result;
        }

        protected DateTime ParseToRegionalTime(SPWeb web, SPUser user, DateTime date)
        {
            DateTime result = date;

            if (user != null && user.RegionalSettings != null && user.RegionalSettings.TimeZone != null)
            {
                result = user.RegionalSettings.TimeZone.UTCToLocalTime(result);
            }
            else if (web != null && web.RegionalSettings != null && web.RegionalSettings.TimeZone != null)
            {
                result = web.RegionalSettings.TimeZone.UTCToLocalTime(result);
            }

            return result;
        }
        #endregion
    }
}

