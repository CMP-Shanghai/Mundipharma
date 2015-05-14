using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*******/
namespace MR.Csl.IdeaTracker.Infrastructure.Common
{
    /// <summary>
    /// XmlHelper Class。
    /// </summary>
    public class XmlHelper
    {
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();

        public XmlHelper(string XmlFile)
        {
            try
            {
                objXmlDoc.Load(XmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            strXmlFile = XmlFile;
        }

        public DataTable GetData(string XmlPathNode)
        {
            DataSet ds = new DataSet();
            StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            return ds.Tables[0];
        }
        /// <summary>
        /// Replace Node content
        /// Sample：xmlTool.Replace("Book/Authors[ISBN=\"0002\"]/Content","ppppppp"); 
        /// </summary>
        /// <param name="XmlPathNode"></param>
        /// <param name="Content"></param>
        public void Replace(string XmlPathNode, string Content)
        {
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        /// <summary>
        /// Delete child node
        /// Sample： xmlTool.DeleteChild("Book/Authors[ISBN=\"0003\"]"); 
        /// </summary>
        /// <param name="Node"></param>
        public void DeleteChild(string Node)
        {
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }


        /// <summary>
        ///  Delete node attribute
        ///  Sample： XmlHelper.Delete( "/Node", "")
        ///  XmlHelper.Delete( "/Node", "Attribute")
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        public void Delete(string node, string attribute)
        {
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
            }
            catch { }
        }


        /// <summary>
        /// 插入一节点和此节点的一子节点。 
        /// 示例：xmlTool.InsertNode("Book","Author","ISBN","0004"); 
        /// </summary>
        /// <param name="MainNode">主节点</param>
        /// <param name="ChildNode">子节点</param>
        /// <param name="Element">元素</param>
        /// <param name="Content">内容</param>
        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);
        }

        /// <summary>
        /// Insert a node with an attribute。
        /// Sample： xmlTool.InsertElement("Book/Author[ISBN=\"0004\"]","Title","Sex","man","iiiiiiii"); 
        /// </summary>
        /// <param name="MainNode">Main node</param>
        /// <param name="Element">Element</param>
        /// <param name="Attrib">Attribute</param>
        /// <param name="AttribContent">Attribute Content</param>
        /// <param name="Content">Element Content</param>
        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        /// Insert a node without attributes。
        /// Sameple：xmlTool.InsertElement("Book/Author[ISBN=\"0004\"]","Content","aaaaaaaaa"); 
        /// </summary>
        /// <param name="MainNode">Main node</param>
        /// <param name="Element">Element</param>
        /// <param name="Content">Content</param>
        public void InsertElement(string MainNode, string Element, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        /// Save after operations.
        /// </summary>
        public void Save()
        {
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            objXmlDoc = null;
        }
    }

}
