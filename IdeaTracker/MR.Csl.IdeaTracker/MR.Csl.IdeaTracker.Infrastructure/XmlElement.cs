using MR.Csl.IdeaTracker.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace MR.Csl.IdeaTracker.Infrastructure
{
    /// <summary>
    /// XML Helper Class
    /// </summary>
    public class XmlElement : IXmlElement
    {
        private static XElement _xe;
        private static readonly object _syncObj = new object();

        private static string _xmlFilePath = System.Configuration.ConfigurationManager.AppSettings["XmlFilePath"];

        public XElement xe
        {
            get
            {
                if (_xe == null)
                {
                    lock (_syncObj)
                    {
                        if (_xe == null)
                        {
                            _xe = XElement.Load(_xmlFilePath);
                        }
                    }
                }
                return _xe;
            }
        }


        public IEnumerable<XElement> GetLists()
        {
            var lists = from ele in xe.Elements("Lists").Elements("List")
                        select ele;
            return lists;
        }

        public IEnumerable<XElement> GetFieldsByList(XElement listNode)
        {
            var fields = from field in listNode.Elements("Fields").Elements("Field")
                         select field;
            return fields;
        }

        public IEnumerable<XElement> GetViewFieldsByList(XElement listNode)
        {
            var viewFields = from viewfield in listNode.Element("View").Elements("Field")
                             select viewfield;
            return viewFields;
        }

        public IEnumerable<XElement> GetGroups()
        {
            throw new NotImplementedException();
        }

        public string GetRootNodeAttr(string attrName)
        {
            return xe.Attribute(attrName).Value;
        }

        public string GetAttributeByNode(XElement node, string attrName)
        {
            var attr = node.Attribute(attrName).Value;
            return attr;
        }

        public string GetAttributeByNode(XElement node)
        {
            throw new NotImplementedException();
        }
    }
}
