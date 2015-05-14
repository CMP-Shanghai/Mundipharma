using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.Csl.IdeaTracker.Infrastructure
{
    class FieldModel
    {
        public FieldModel() { }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private Field currField;
        public Field CurrField
        {
            get { return currField; }
            set { currField = value; }
        }

        private List currList;
        public List CurrList
        {
            get { return currList; }
            set { currList = value; }
        }

        private string fieldXML;
        public string FieldXML
        {
            get { return fieldXML; }
            set { fieldXML = value; }
        }
        //private const string fieldRef = "<FieldRefs><FieldRef Name=\"{0}\"/></FieldRefs></Field>";

        public void CreatField()
        {
            switch(name)
            {
                case "Title": 
                    this.GetField("Title").Title = displayName;
                    this.GetField("Title").Update();
                    break;
               default:
                    this.CurrList.Fields.AddFieldAsXml(fieldXML, true, AddFieldOptions.AddFieldToDefaultView);
                    break;
            }
        }

        public Field GetField(string internalName)
        {
            return this.currList.Fields.GetByInternalNameOrTitle(internalName);
        }

        public void DeleteField()
        {

        }



    }
}
