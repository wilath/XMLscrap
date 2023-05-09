using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLscrap
{
    public class GetSchema
    {
        public string DownloadSchema(string url)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(url);

            XmlElement root = doc.DocumentElement;

            XmlAttributeCollection attributes = root.Attributes;

            string schemaLink = "";

            foreach (XmlAttribute attribute in attributes)
            {
                if (attribute.Name.Contains("schemaLocation"))
                {
                    string input = attribute.Value;
                    string[] links = input.Split(' ');
                    schemaLink = links[1];
                }

            }

            string fileName = Path.GetFileName(schemaLink); // nazwa schemy
            if (!Path.HasExtension(fileName) || Path.GetExtension(fileName) != ".xsd") // sprwadz czy path ma rozszrzenie xsd, jesli nie, dodaj
            {
                fileName += ".xsd";
            }
            string filePath = Path.Combine("C:\\Users\\Sebastian\\source\\repos\\XMLscrap\\XMLscrap\\Schemas\\", fileName); // utwórz pełną ścieżkę pliku w folderze "Schemas"

            if (File.Exists(filePath)) // sprawdź, czy plik już istnieje, jeśli tak - zwróc adres istniejacego pliku
            {
                Console.WriteLine($"Plik {fileName} już istnieje w folderze Schemas.");
                return filePath;
            }

            using (var client = new WebClient()) // jeśli plik nie istnieje, pobierz
            {
                client.DownloadFile(schemaLink, filePath);
          
            }

            return filePath;
            
        }

        public void DeleteSchema()
        {

        }
        
    }
}
