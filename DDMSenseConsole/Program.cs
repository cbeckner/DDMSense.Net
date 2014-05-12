using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDMSense;
using DDMSense.Util;
using DDMSense.DDMS;
using System.Xml;
using System.IO;

namespace DDMSenseConsole
{
    class Program
    {
        private static Resource _resource;

        static void Main(string[] args)
        {
            string sampleXML = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "SampleXML");
            loadFile(new FileInfo(Path.Combine(sampleXML, "3.1-identifierPostalAddressExample.xml")));
        }

        private static void loadFile(FileInfo file)
        {
            DDMSVersion version = DDMSVersion.GetVersionFor("4.1"); // DDMSVersion.GetCurrentVersion();

            var reader = new DDMSReader();

            _resource = reader.GetDDMSResourceFromFile(file.FullName);
       
            string xmlFormat = getResource().ToXML();
            string htmlFormat = getResource().ToHTML();
            string textFormat = getResource().ToText();
        }

        /**
        * Accessor for the currently loaded ddms:resource
        */
        private static Resource getResource()
        {
            return (_resource);
        }

    }
}
