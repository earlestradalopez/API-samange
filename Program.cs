using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace API_samange
{
    class Program
    {
        static void Main(string[] args)
        {
            Program FUNCTION = new Program();            
            while (true)
            {
                
                FUNCTION.EXECUTE("CURL\\curl.exe", "--digest -u \"test@uplifteducation.org:xxxxx\" https://app.samanage.com/incidents.xml?per_page=1000 -k -o XML\\TOTAL.xml");

                //string STR_TOTAL = File.ReadAllText("XML\\TOTAL.xml");
                //STR_TOTAL = STR_TOTAL.Substring(STR_TOTAL.IndexOf("<total_entries>") + 15);
                //STR_TOTAL = STR_TOTAL.Substring(0, STR_TOTAL.IndexOf("</total_entries>"));
                //int INT_TOTAL = int.Parse(STR_TOTAL) / 100;

                ////for (int x = 2; x <= (INT_TOTAL + 1); x++)            
                //for (int x = 2; x <= 5; x++)
                //    FUNCTION.EXECUTE("CURL\\curl.exe", "--digest -u \"test@uplifteducation.org:xxxxx\" https://app.samanage.com/incidents.xml?page=" + x.ToString() + " -k -o XML\\output-" + x.ToString() + ".xml");

                XML SERIAL = new XML();
                SERIAL.SRL("XML\\TOTAL.xml");
                Thread.Sleep(86400000);
            }

        }

        public void EXECUTE(string FILENAME, string PARAMETER)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = FILENAME;
            proc.StartInfo.Arguments = PARAMETER;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.WaitForExit();
        }

        public T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch //(Exception ex)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
            return returnObject;
        }
    }
}
