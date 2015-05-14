using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.SharePoint.Client;
using System.Net;
using System.Configuration;
using System.Threading;

namespace MR.Csl.IdeaTracker.Infrastructure
{
    /// <summary>
    /// Use to Create lists & fields Console.
    /// </summary>
    class Program
    {
        private static string USERNAME = ConfigurationManager.AppSettings["UserName"];
        private static string PASSWORD = ConfigurationManager.AppSettings["PassWord"];
        private static string DOMAIN = ConfigurationManager.AppSettings["Domain"];
        private static Dictionary<string, string> dicListID = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            XmlElement xle = new XmlElement();
            string siteUrl = xle.GetRootNodeAttr("SiteUrl");
            if (string.IsNullOrEmpty(siteUrl))
            {
                Tools.ConsoleWrite("Site Url can not be empty.");
                return;
            }

            ListModel listModel = new ListModel();
            FieldModel fieldModel = new FieldModel();
            IEnumerable<XElement> listFields;
            List spList;

            ClientContext cc = new ClientContext(siteUrl);
            NetworkCredential credential = new NetworkCredential(USERNAME, PASSWORD, DOMAIN);
            cc.Credentials = credential;

            var xlists = xle.GetLists();
            foreach (var listNode in xlists)
            {
                BuildList(listNode, cc.Web, listModel);

                //Delete Lists
                try
                {
                    spList = cc.Web.Lists.GetByTitle(listModel.Name);
                    cc.ExecuteQuery();
                    listModel.CurrList = spList;
                    listModel.DeleteList();
                    cc.ExecuteQuery();
                }
                catch (Exception ex)
                {
                    Tools.ConsoleWrite(listModel.Name + " is not existed!");
                }

                //Create Lists
                listModel.CreateList();
                cc.ExecuteQuery();
                Thread.Sleep(2 * 1000);
            }

            //Create Fields
            cc = new ClientContext(siteUrl);
            credential = new NetworkCredential(USERNAME, PASSWORD, DOMAIN);
            cc.Credentials = credential;
            foreach (var listNode in xlists)
            {
                BuildList(listNode, cc.Web, listModel);

                spList = cc.Web.Lists.GetByTitle(listModel.Name);
                cc.Load(spList);
                cc.ExecuteQuery();
                dicListID.Add(listModel.Name, spList.Id.ToString());
                listModel.CurrList = spList;
                listFields = xle.GetFieldsByList(listNode);
                CreateFieldsForList(listFields, listModel, fieldModel,cc);

            }
            cc.ExecuteQuery();

            Tools.ConsoleWrite("Finished! Press any key to close this console.");
            Console.ReadKey();
        }

        //Create List
        private static void BuildList(XElement listnode, Web web, ListModel listmodel)
        {
            listmodel.Name = listnode.Attribute("Name").Value;
            listmodel.Description = listnode.Attribute("Description").Value;
            listmodel.ListType = listnode.Attribute("Type").Value;
            listmodel.CurrWeb = web;
        }

        //Add Custom Columns to List
        private static void CreateFieldsForList(IEnumerable<XElement> eleColumns, ListModel listModel, FieldModel fieldModel, ClientContext cc)
        {
            Tools.ConsoleWrite(listModel.Name + " starting add columns.");
            foreach (var colmnNode in eleColumns)
            {
                fieldModel.CurrList = listModel.CurrList;
                fieldModel.Name = colmnNode.Attribute("Name").Value;
                fieldModel.DisplayName = colmnNode.Attribute("DisplayName").Value;
                if (colmnNode.ToString().Contains("Type=\"Lookup\""))
                {
                    string parListId = dicListID[colmnNode.Attribute("List").Value];
                    colmnNode.Attribute("List").SetValue(parListId);
                }
                fieldModel.FieldXML = colmnNode.ToString();
                fieldModel.CreatField();
            }
            listModel.UpdateList();
            Tools.ConsoleWrite(listModel.Name + " columns added.");
        }
    }
}