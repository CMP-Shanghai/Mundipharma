using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;
using MR.SP.DueDiligence.Framework.Const;
using MR.SP.DueDiligence.Framework;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MR.SP.DueDiligence.Pages.Layouts.MR.SP.DueDiligence.Pages.mail
{

    public partial class SendMail : LayoutsPageBase
    {
        #region Properties
        private string _internalMailTemplateFieldName = "MailTemplate";
        private string InternalMailTemplateFieldName
        {
            get { return _internalMailTemplateFieldName; }
            set { _internalMailTemplateFieldName = value; }
        }

        private string _internalActiveFieldName = "Active";
        private string InternalActiveFieldName
        {
            get { return _internalActiveFieldName; }
            set { _internalActiveFieldName = value; }
        }

        private string _internalContentFieldName = "Content";
        private string InternalContentFieldName
        {
            get { return _internalContentFieldName; }
            set { _internalContentFieldName = value; }
        }
        #endregion


        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                  {
                      using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                      {
                          using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                          {
                              EnsureIntenralFieldName(web);
                              InitData(web);
                          }
                      }
                  });

                if (!IsPostBack)
                {
                    DDLBind();
                }
            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(ex.ToString());
            }
        }


        protected void InitData(SPWeb web)
        {
            SPList list = web.Lists[ListName.Project];
            int id = Convert.ToInt32(Request[RequestParameter.ParameterID]);
            SPListItem item = list.GetItemById(id);


            SPFieldUserValueCollection fieldParticipants = item[Fields.Participants] as SPFieldUserValueCollection;


            txtTo.Text = GetAllUserEmails(web,fieldParticipants) + (string)item[Fields.ExternalParticipants];
            
            
            
        }

        protected string GetAllUserEmails(SPWeb web, SPFieldUserValueCollection listUsers)
        {
            string allMails = null;
            if (listUsers == null) return null;
            foreach (SPFieldUserValue u in listUsers)
                {
                    SPUser user = web.EnsureUser(u.User.LoginName);
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        allMails += user.Email + ";";
                    }
                    
                }
            return allMails;
        }



        protected void Send_Click(object sender, EventArgs e)
        {


            try
            {
                //throw (new Exception("test by aaron"));
                MailMessage msg = new MailMessage();

                string toMail = GetMailAddress();
                string[] ArrToMail = toMail.Split(';');
                foreach (string item in ArrToMail)
                {
                    msg.To.Add(item);
                }
                msg.Subject = txtSubject.Text;
                if (!string.IsNullOrEmpty(SPContext.Current.Site.WebApplication.OutboundMailSenderAddress))
                {
                    msg.From = new MailAddress(SPContext.Current.Site.WebApplication.OutboundMailSenderAddress);
                }
                else
                {
                    msg.From = new MailAddress("");
                }
                msg.IsBodyHtml = true;

                //string comments = Page.Request.Form.Get("t1").ToString();
                string htmlbodycontent = txtBody.Text;  
                AlternateView htmlbody = AlternateView.CreateAlternateViewFromString(htmlbodycontent, null, "text/html");

                msg.AlternateViews.Add(htmlbody);

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = true;
                smtp.Host = SPContext.Current.Site.WebApplication.OutboundMailServiceInstance.Server.Address;
                try
                {
                    smtp.Send(msg);
                    string url = "/";
                    if (Request["reurl"] != null)
                    {
                        url = Request["reurl"];
                    }
                    string jsMessage = string.Format("<script>alert('Sent successfully');window.location.href='{0}'</script>", url);
                    Response.Write(jsMessage);
                }
                catch (Exception exsend)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Failed to send mail!');", true);
                    //Response.Write("<script>alert('Failed to send mail!');</script>");
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<div>Error Message: " + ex.Message + "</div>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Failed to send mail!');", true);
            }


           

        }



        private string GetMailAddress()
        {
            string toMailAddress = string.Empty;
            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                if (txtTo.Text.IndexOf(";") != -1)
                {

                    string[] toEmail = txtTo.Text.Split(';');
                    foreach (string email in toEmail)
                    {
                        if (!string.IsNullOrEmpty(email))
                        {
                            toMailAddress+=email+";";
                        }

                    }

                }
                else { toMailAddress+=txtTo.Text+";"; }
            }

            foreach (PickerEntity p in peMilTo.ResolvedEntities)
            {
                if (p.EntityData["Email"] != null)
                {

                    toMailAddress += (string)p.EntityData["Email"] + ";";
                }
            }
            return toMailAddress;
        }


        private bool SendEmail(string pSubject,List<string> pTos, string pBody)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = SPContext.Current.Site.WebApplication.OutboundMailServiceInstance.Server.Address;
                //client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                //client.Credentials = new System.Net.NetworkCredential(pFrom, pPassword);

                MailMessage mail = new MailMessage();
                if (pTos!=null)
                {
                    foreach (string to in pTos)
                    {
                        mail.To.Add(to);
                    }
                }
                //mail.From = new MailAddress(pFrom);
                mail.Body = pBody;
                mail.Subject = pSubject;
                mail.IsBodyHtml = true;
                mail.ReplyTo = new MailAddress(SPContext.Current.Site.WebApplication.OutboundMailReplyToAddress);
                client.Send(mail);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }


        protected void ddlTempTyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLSelectValue();
        }

        protected void ddlTempTyle_Load(object sender, EventArgs e)
        {
            DDLSelectValue();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Refresh fields internal name
        /// </summary>
        /// <param name="web"></param>
        private void EnsureIntenralFieldName(SPWeb web)
        {
            //Refresh all internal field name for current environment
            SPList list = web.Lists.TryGetList(ListName.MailTemplate);
            if (list == null) return;
            //Refresh all internal name for fields
            InternalMailTemplateFieldName = list.Fields[InternalMailTemplateFieldName].InternalName;
            InternalActiveFieldName = list.Fields[InternalActiveFieldName].InternalName;
            InternalContentFieldName = list.Fields[InternalContentFieldName].InternalName;

        }
        private void DDLBind()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            string query = @"<Where>
				                <Eq>
					                <FieldRef Name='" + InternalActiveFieldName + @"'/>
					                <Value Type='Boolean'>1</Value>
				                </Eq>
			                </Where>";
                            SPListItemCollection items = GetListMailItems(web, query);
                            if (items != null && items.Count > 0)
                            {
                                foreach (SPListItem mail in items)
                                {
                                    ddlTempTyle.Items.Add((string)mail[InternalMailTemplateFieldName]);
                                }
                            }
                        }
                    }
                });
        }

        /// <summary>
        /// 
        /// </summary>
        private void DDLSelectValue()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {

                            if (ddlTempTyle.SelectedItem != null && !string.IsNullOrEmpty(ddlTempTyle.SelectedItem.Text))
                            {
                                string query = @"<Where>
				                    <Eq>
					                    <FieldRef Name='" + InternalMailTemplateFieldName + @"'/>
					                    <Value Type='Text'>" + ddlTempTyle.SelectedItem.Text + @"</Value>
				                    </Eq>
			                    </Where>";
                                SPListItemCollection mailConfigItems = GetListMailItems(web, query);
                                if (mailConfigItems != null && mailConfigItems.Count > 0)
                                {
                                    #region Get Control Value
                                    SPListItem configItem = mailConfigItems[0];
                                    string ID = Request[RequestParameter.ParameterID];
                                    SPList projectList = web.Lists[ListName.Project];
                                    SPListItem projectItem = projectList.GetItemById(int.Parse(ID));

                                    string notificationContent = string.Empty;
                                    string notificationSubject = string.Empty;
                                    if (projectItem != null)
                                    {
                                        notificationContent = null == configItem[InternalContentFieldName] ? string.Empty : (string)configItem[InternalContentFieldName];
                                        notificationSubject = null == configItem.Title ? string.Empty : configItem.Title;
                                        SPFieldCollection fileds = projectList.Fields;
                                        foreach (SPField field in fileds)
                                        {
                                            string fieldToken = string.Format("{0}{1}{2}", "_", field.Title, "_");
                                            notificationContent = GenerateContent(notificationContent, projectItem, fieldToken, field);
                                            notificationSubject = GenerateContent(notificationSubject, projectItem, fieldToken, field);
                                        }

                                    }
                                    #endregion
                                    txtSubject.Text = notificationSubject;
                                    txtBody.Text = notificationContent;
                                }
                            }


                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(ex.Message);

            }
        }


        /// <summary>
        /// Replace pre-definde token to real value
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="projectItem"></param>
        /// <param name="fieldToken"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private static string GenerateContent(string sourceString, SPListItem projectItem, string fieldToken, SPField field)
        {
            string result = sourceString;
            if (projectItem == null) return result;
            object fieldValueObject = projectItem[field.Id];
            switch (field.Type)
            {
                case SPFieldType.DateTime:
                    bool isDateTime = false;
                    DateTime dateValue = DateTime.MinValue;
                    string dateString = string.Empty;
                    if (fieldValueObject != null)
                        isDateTime = DateTime.TryParse(fieldValueObject.ToString(), out dateValue);

                    if (isDateTime)
                    {
                        dateString = Convert.ToDateTime(fieldValueObject).ToString("dd/MM/yyyy");
                    }
                    result = result.Replace(fieldToken, dateString);

                    break;
                default:

                    result = result.Replace(fieldToken, null == fieldValueObject ? string.Empty : Convert.ToString(projectItem[field.Id]));

                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="web"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        private SPListItemCollection GetListMailItems(SPWeb web, string queryStr)
        {
            SPListItemCollection items;
            SPList listMail = web.Lists.TryGetList(ListName.MailTemplate);
            if (listMail == null) return null;

            SPQuery query = new SPQuery();
            query.Query = queryStr;
            query.RowLimit = (uint)listMail.ItemCount;
            items = listMail.GetItems(query);
            return items;
        }
        #endregion
    }
}
