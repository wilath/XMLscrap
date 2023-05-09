using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XMLscrap
{
    public class Data
    {   
        public string CompanyName { get; set; }
        public decimal Revenue { get; set; }
        public decimal OperatingIncome { get; set; }
        public decimal NetIncome { get; set; }  
    }

    public class Grab
    {
        public void LoadXml(string url)
        {                
            XmlTextReader reader = new XmlTextReader(url);
          
            string NodePrefix = "";
            string InnerNodeLocalName = "KwotaA";
            string InnerNodePrefix = "";

            string RevenueNodeName = "A";
            string CElement = "C";
            string DElement = "D";
            string EElement = "E";
            string NetIncomeNodeName = "";

            bool isThousands = false;

            decimal CValue = 0;
            decimal DValue = 0;
            decimal EValue = 0;

            string CompanyName = "";
            decimal Revenue = 0;
            decimal OperatingIncome = 0;
            decimal NetIncome = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {

                    if(reader.LocalName == "KodSprawozdania" && reader.ReadInnerXml().Contains("Tys")) // CHECK IF TYS.
                    {
                        isThousands = true;
                        
                    }
                    if(reader.LocalName == "NazwaFirmy")   // OUTPUT COMPANY NAME
                    {
                        CompanyName = reader.ReadInnerXml();                      
                    }
                    if (reader.LocalName == "KwotaA")
                    {
                        InnerNodePrefix = reader.Prefix; //FIND KWOTAA NODE PREFIX                      
                    }

                    if (reader.LocalName == "RZiSPor")
                    {
                        XmlReader rTree = reader.ReadSubtree();
                        NodePrefix = reader.Prefix;  //  FIND RZSIPOR NODE PREFIX
                        NetIncomeNodeName = new LastNode().FindNode(url, NodePrefix); // ASSIGN NET INCOME NODE
                      
                        while (rTree.Read())
                        {                          
                            if (rTree.Name == NodePrefix + ":" + RevenueNodeName) // FIND REVENUE
                            {                                       
                                if (reader.ReadToDescendant(InnerNodePrefix + ":" + InnerNodeLocalName))
                                {                               
                                    Decimal.TryParse(reader.ReadInnerXml().Replace(".", ","), out Revenue);
                                }
                            }
                            if (rTree.Name == NodePrefix + ":" + CElement) // FIND C
                            {                         
                                if (reader.ReadToDescendant(InnerNodePrefix + ":" + InnerNodeLocalName))
                                {
                                    Decimal.TryParse(reader.ReadInnerXml().Replace(".", ","), out CValue);
                                }
                            }
                            if (rTree.Name == NodePrefix + ":" + DElement) // FIND D
                            {
                                if (reader.ReadToDescendant(InnerNodePrefix + ":" + InnerNodeLocalName))
                                {
                                    Decimal.TryParse(reader.ReadInnerXml().Replace(".", ","), out DValue);                                  
                                }
                            }
                            if (rTree.Name == NodePrefix + ":" + EElement) // FIND E
                            {
                                if (reader.ReadToDescendant(InnerNodePrefix + ":" + InnerNodeLocalName))
                                {
                                    Decimal.TryParse(reader.ReadInnerXml().Replace(".", ","), out EValue);
                                    EValue = Math.Abs(EValue);
                                }
                            }
                            if (rTree.Name == NetIncomeNodeName) // FIND NET INCOME
                            {
                                if (reader.ReadToDescendant(InnerNodePrefix + ":" + InnerNodeLocalName))
                                {
                                    Decimal.TryParse(reader.ReadInnerXml().Replace(".", ","), out NetIncome);
                                 
                                }
                            }
                        }                     
                    }                    
                }             
            }

            OperatingIncome = CValue + DValue - EValue;
            if (isThousands)
            {
                Revenue = Revenue * 1000;
                OperatingIncome = OperatingIncome * 1000;
                NetIncome = NetIncome * 1000; 

            }
            
            Console.WriteLine("----------------------------");
            Console.WriteLine("Company: " + CompanyName);
            Console.WriteLine("Revenue: " + Revenue);
            Console.WriteLine("Operating Income: " + OperatingIncome);
            Console.WriteLine("Net Income: " + NetIncome);
            Console.WriteLine("----------------------------");
            
        }
    }
}

