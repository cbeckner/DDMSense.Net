//using System.Collections.Generic;

/* Copyright 2010 - 2013 by Brian Uri!
   
   This file is part of DDMSence.
   
   This library is free software; you can redistribute it and/or modify
   it under the terms of version 3.0 of the GNU Lesser General Public 
   License as published by the Free Software Foundation.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
   GNU Lesser General Public License for more details.
   
   You should have received a copy of the GNU Lesser General Public 
   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

   You can contact the author at ddmsence@urizone.net. The DDMSence
   home page is located at http://ddmsence.urizone.net/
 */
namespace DDMSense.Test.DDMS
{


    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using DDMSense.DDMS;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to Schematron validation of Resources </para>
    /// 
    /// @author Brian Uri!
    /// @since 1.3.1
    /// </summary>
    [TestClass]
    public class SchematronValidationTest : AbstractBaseTestCase
    {

        private Dictionary<string, Resource> VersionToResourceMap;

        /// <summary>
        /// Constructor
        /// </summary>
        public SchematronValidationTest()
            : base("resource.xml")
        {
            VersionToResourceMap = new Dictionary<string, Resource>();
            foreach (string sVersion in SupportedVersions)
            {
                VersionToResourceMap[sVersion] = new Resource(GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void SchematronValidation_SchematronValidationXslt1()
        {
            List<string> supportedXslt1Processors = new List<string>();
            //if (System.getProperty("java.version").IndexOf("1.5.0") == -1)
            //{
            //    supportedXslt1Processors.Add("com.sun.org.apache.xalan.internal.xsltc.trax.TransformerFactoryImpl");
            //}
            supportedXslt1Processors.Add("net.sf.saxon.TransformerFactoryImpl");
            foreach (string processor in supportedXslt1Processors)
            {
                PropertyReader.SetProperty("xml.transform.TransformerFactory", processor);
                foreach (string sVersion in SupportedVersions)
                {
                    DDMSVersion version = DDMSVersion.GetVersionFor(sVersion);
                    Resource resource = VersionToResourceMap.GetValueOrNull(sVersion);
                    string ddmsNamespace = resource.Namespace;
                    string resourceName = Resource.GetName(version);
                    List<ValidationMessage> messages = resource.ValidateWithSchematron(new FileInfo("data/test/" + sVersion + "/testSchematronXslt1.sch").FullName);
                    Assert.AreEqual(version.IsAtLeast("4.0.1") ? 3 : 2, messages.Count);

                    string text = "A DDMS Resource must have an unknownElement child.";
                    string locator = "/*[local-name()='" + resourceName + "' and namespace-uri()='" + ddmsNamespace + "']";
                    AssertErrorEquality(text, locator, messages[0]);

                    int originalWarningIndex = version.IsAtLeast("4.0.1") ? 2 : 1;

                    text = "Members of the Uri family cannot be publishers.";
                    locator = "/*[local-name()='" + resourceName + "' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='publisher' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='" + Person.GetName(version) + "' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='surname' and namespace-uri()='" + ddmsNamespace + "']";
                    AssertWarningEquality(text, locator, messages[originalWarningIndex]);

                    if (version.IsAtLeast("4.0.1"))
                    {
                        text = "Members of the Uri family cannot be publishers.";
                        locator = "/*[local-name()='" + resourceName + "' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='metacardInfo' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='publisher' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='" + Person.GetName(version) + "' and namespace-uri()='" + ddmsNamespace + "']" + "/*[local-name()='surname' and namespace-uri()='" + ddmsNamespace + "']";
                        AssertWarningEquality(text, locator, messages[1]);
                    }
                }
            }
        }

        [TestMethod]
        public virtual void SchematronValidation_SchematronValidationXslt2()
        {
            string[] supportedXslt1Processors = new string[] { "net.sf.saxon.TransformerFactoryImpl" };
            foreach (string processor in supportedXslt1Processors)
            {
                PropertyReader.SetProperty("xml.transform.TransformerFactory", processor);
                foreach (string sVersion in SupportedVersions)
                {
                    DDMSVersion version = DDMSVersion.GetVersionFor(sVersion);
                    Resource resource = VersionToResourceMap.GetValueOrNull(sVersion);
                    string ddmsNamespace = resource.Namespace;
                    string gmlNamespace = version.GmlNamespace;
                    List<ValidationMessage> messages = resource.ValidateWithSchematron(new FileInfo("data/test/" + sVersion + "/testSchematronXslt2.sch").FullName);
                    Assert.AreEqual(1, messages.Count);

                    string text = "The second coordinate in a gml:pos element must be 40.2 degrees.";
                    string extent = version.IsAtLeast("4.0.1") ? "" : "/*:GeospatialExtent[namespace-uri()='" + ddmsNamespace + "'][1]";
                    string resourceName = Resource.GetName(version);
                    string locator = "/*:" + resourceName + "[namespace-uri()='" + ddmsNamespace + "'][1]" + "/*:geospatialCoverage[namespace-uri()='" + ddmsNamespace + "'][1]" + extent + "/*:boundingGeometry[namespace-uri()='" + ddmsNamespace + "'][1]" + "/*:Point[namespace-uri()='" + gmlNamespace + "'][1]" + "/*:pos[namespace-uri()='" + gmlNamespace + "'][1]";
                    AssertErrorEquality(text, locator, messages[0]);
                }
            }
        }

        //	public void SchematronValidation_IsmXmlV5SchematronValidation() throws SAXException, InvalidDDMSException, IOException, XSLException {
        //		// For this test to work, the ISM.XML V5 distribution must be unpacked in the data directory.
        //		File schematronFile = new File("ISM_XML.sch");
        //		Resource resource = new DDMSReader().getDDMSResource(new File("data/sample/DDMSence_Example_v3_1.xml"));
        //		List<ValidationMessage> messages = resource.validateWithSchematron(schematronFile);
        //		for (ValidationMessage message : messages) {
        //			System.out.println("Location: " + message.getLocator());
        //			System.out.println("Message: " + message.getText());
        //		}
        //	}
    }

}