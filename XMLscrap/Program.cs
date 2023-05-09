using System;

namespace XMLscrap
{
    class Program
    {
        static void Main()
        {
            const string url1 = "file:///C:/Users/Sebastian/Desktop/XMLs/justjoinit.xml";
            const string url2 = "file:///C:/Users/Sebastian/Desktop/XMLs/amrest.xml";
            const string url3 = "file:///C:/Users/Sebastian/Desktop/XMLs/olx.xml";
            const string url4 = "file:///C:/Users/Sebastian/Desktop/XMLs/wizual.xml";

            var doc = new Grab();
            doc.LoadXml(url1);
            doc.LoadXml(url2);
            doc.LoadXml(url3);
            doc.LoadXml(url4);

  



        }
    }
}