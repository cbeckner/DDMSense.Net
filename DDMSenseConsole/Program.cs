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
        private Resource _resource;
        private const string EARLIER_VERSION_EXAMPLE_2_0 = "2.0-earlierVersionExample.xml";//TODO - This example does not currently work (5/14/14)WFM
        private const string EXTENSIBLE_LAYER_EXAMPLE_3_0 = "3.0-extensibleLayerExample.xml";
        private const string INVALID_RESOURCE_EXAMPLE_3_0 = "3.0-invalidResourceExample.xml";//TODO - This example does not currently work?? (5/14/14)WFM
        private const string BOUNDING_GEOMETRY_EXAMPLE_3_1 = "3.1-boundingGeometryExample.xml"; //TODO - This example does not currently work (5/14/14)WFM
        private const string IDENTIFIER_POSTAL_ADDRESS_EXAMPLE_3_1 = "3.1-identifierPostalAddressExample.xml";
        private const string DDMSENCEEXAMPLE4_1 = "4.1-ddmsenceExample.xml";
        private const string IRM_EXAMPLE_4_1 = "4.1-irmExample.xml"; //TODO - This example does not currently work (5/14/14)WFM
        private const string DDMSENCE_EXAMPLE_5_0 = "5.0-ddmsenceExample.xml"; //TODO - This example does not currently work (5/14/14). Namespace not supported exception WFM

        public Program()
        {
            string sampleXML = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "SampleXML");
            loadFile(new FileInfo(Path.Combine(sampleXML, DDMSENCEEXAMPLE4_1)));
        }

        static void Main(string[] args)
        {
            new Program();
        }

        private void loadFile(FileInfo file)
        {
            //DDMSVersion version = DDMSVersion.GetVersionFor("4.1"); // DDMSVersion.GetCurrentVersion();

            //var reader = new DDMSReader();

            //_resource = reader.GetDDMSResourceFromFile(file.FullName);
       
            //string xmlFormat = getResource().ToXML();
            //string htmlFormat = getResource().ToHTML();
            //string textFormat = getResource().ToText();

            //_resource = new Resource()
            Resource.Builder builder = new Resource.Builder();
        }

        /**
        * Accessor for the currently loaded ddms:resource
        */
        private Resource getResource()
        {
            return (_resource);
        }

    }
}
