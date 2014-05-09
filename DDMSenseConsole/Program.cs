using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDMSSense;
using DDMSSense.Util;
using DDMSSense.DDMS;
using System.Xml;
using System.IO;

namespace DDMSenseConsole
{
    class Program
    {
        private static Resource _resource;

        static void Main(string[] args)
        {

            loadFile(new FileInfo(@"4.1-ddmsenceExample.xml"));
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
