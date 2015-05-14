using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace MR.Csl.IdeaTracker.Infrastructure.Interface
{
    interface IXmlElement
    {
        XElement xe { get; }
        IEnumerable<XElement> GetLists();

        IEnumerable<XElement> GetFieldsByList(XElement listNode);

        IEnumerable<XElement> GetViewFieldsByList(XElement listNode);

        IEnumerable<XElement> GetGroups();


        string GetAttributeByNode(XElement node);

    }
}
