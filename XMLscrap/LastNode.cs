using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;


namespace XMLscrap
{
    public class LastNode
    {
        public string FindNode(string url, string NodePrefix)
        {
            XmlReader reader = new XmlTextReader(url);


            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == NodePrefix + ":RZiSPor")
                {
                    // Parent node found, iterate through child nodes
                    string lastChildNodeName = "";
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Prefix == NodePrefix)
                        {
                            // Keep track of the last child node encountered
                            lastChildNodeName = reader.Name;
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == NodePrefix + ":RZiSPor")
                        {
                            // End of parent node reached, return the last child node name
                            
                            return lastChildNodeName;
                        }
                    }
                }
            }
            return "";
        }
    }
}
