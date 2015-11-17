using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using TEST4.ServiceReference1;
using System.IO;
using System.Xml;

namespace TEST4
{
    class Program
    {
        static void Main(string[] args)
        {
            //StockQuoteSoapClient client = new StockQuoteSoapClient("StockQuoteSoap12");
            //string output = client.GetQuote("MSFT");
            //Console.WriteLine("hey {0} \n", output);
            //Console.WriteLine(PrintXML(output));
            //Console.ReadKey();

            using(var client = SOAPHelp.GetServerApiClient("http://www.webservicex.com/stockquote.asmx?WSDL"))
            {
                Console.WriteLine(PrintXML(client.GetQuote("AAPL")));
                Console.ReadKey();

            }
        }
        public static String PrintXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException)
            {
            }

            mStream.Close();
            writer.Close();

            return Result;
        }
    }

}
