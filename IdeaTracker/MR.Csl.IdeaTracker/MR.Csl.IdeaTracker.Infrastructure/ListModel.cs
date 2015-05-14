using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint.Client;

namespace MR.Csl.IdeaTracker.Infrastructure
{
    public class ListModel
    {
        public ListModel() { }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        private Web currWeb;
        public Web CurrWeb
        {
            get { return currWeb; }
            set { currWeb = value; }
        }

        private ListTemplateType listTemplate;
        public ListTemplateType ListTemplate
        {
            get { return listTemplate; }
            set { ListTemplate = value; }
        }

        public string ListType
        {
            set
            {
                switch (value)
                {
                    case "List":
                        listTemplate = ListTemplateType.GenericList;
                        break;
                    case "Document Library":
                        listTemplate = ListTemplateType.DocumentLibrary;
                        break;
                }
            }
        }

        private List currList;
        public List CurrList
        {
            get { return currList; }
            set { currList = value; }
        }

        public void CreateList()
        {
            ListCreationInformation listInfo = new ListCreationInformation();
            listInfo.Title = name;
            listInfo.Description = description;
            listInfo.TemplateType = (int)listTemplate;
            listInfo.QuickLaunchOption = QuickLaunchOptions.On;

            Tools.ConsoleWrite(this.Name + " Creating.");
            var listID = this.CurrWeb.Lists.Add(listInfo);

            this.CurrList = this.CurrWeb.Lists.GetByTitle(name);
            Tools.ConsoleWrite(this.Name + " Created.");
        }

        public void DeleteList()
        {
            if (this.CurrList != null)
            {
                Tools.ConsoleWrite(this.Name + " exists, deleting.");
                CurrList.DeleteObject();
                Tools.ConsoleWrite(this.Name + " Deleted.");
            }
        }

        public void UpdateList()
        {
            this.CurrList.Update();
        }

    }
}

