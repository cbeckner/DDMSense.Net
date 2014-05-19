using System.Text;

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
namespace DDMSense.Test.Util {


	using InvalidDDMSException = DDMSense.DDMS.InvalidDDMSException;
    using DDMSense.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

	/// <summary>
	/// A collection of DDMSReader tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
    [TestClass]
    public class DDMSReaderTest : AbstractBaseTestCase
    {

		private DDMSReader _reader;

		public DDMSReaderTest() : base(null) {
			_reader = new DDMSReader();
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementNullString()
        {
			try {
				Reader.GetElement((string) null);
				Assert.Fail("Allowed invalid data.");
			} catch (IOException) {
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "XML string is required.");
			}
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementNullInputStream()
        {
			try {
				Reader.GetElement((Stream) null);
				Assert.Fail("Allowed invalid data.");
			} catch (IOException) {
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "input stream is required.");
			}
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementDoesNotExistString()
        {
			try {
				Reader.GetElement("<wrong></wrong>");
				Assert.Fail("Allowed invalid data.");
			} catch (IOException) {
				Assert.Fail("Should have thrown an InvalidDDMSException");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ValidityException");
			}
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementDoesNotExistInputStream()
        {
			try {
				Reader.GetElement(new FileStream("doesnotexist", FileMode.Open));
				Assert.Fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "doesnotexist (The system cannot find the file specified)");
			}
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementNotXML()
        {
			try {
                //Reader.GetElement(new File("conf/ddmsence.properties"));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ParsingException");
			}
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementFileSuccess()
        {
            //Reader.GetElement(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml"));
            Assert.Fail("Not Implemented");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementStringSuccess()
        {
			Reader.GetElement("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ddms:language " + " xmlns:ddms=\"http://metadata.dod.mil/mdr/ns/DDMS/3.0/\" " + " ddms:qualifier=\"http://purl.org/dc/elements/1.1/language\" ddms:value=\"en\" />");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementInputStreamSuccess()
        {
            //Reader.GetElement(new FileInputStream(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml")));
            Assert.Fail("Not Implemented");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementReaderSuccess()
        {
            //Reader.GetElement(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml")));
            Assert.Fail("Not Implemented");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceFailure()
        {
			try {
                //Reader.GetDDMSResource(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml"));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceSuccessFile()
        {
            //Reader.GetDDMSResource(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml"));
            Assert.Fail("Not Implemented");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceSuccessString()
        {
            //LineNumberReader reader = new LineNumberReader(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
            //StringBuilder xmlString = new StringBuilder();
            //string nextLine = reader.readLine();
            //while (nextLine != null) {
            //    xmlString.Append(nextLine);
            //    nextLine = reader.readLine();
            //}
            //Reader.GetDDMSResource(xmlString.ToString());
            Assert.Fail("Not Implemented");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceSuccessInputStream()
        {
            //Reader.GetDDMSResource(new FileInputStream(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
            Assert.Fail("Not Implemented");
		}

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceSuccessReader()
        {
            //Reader.GetDDMSResource(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
            Assert.Fail();
		}

        [TestMethod]
		public virtual void Util_DDMSReaderTest_GetExternalSchemaLocation() {
			string externalLocations = Reader.ExternalSchemaLocations;
			Assert.Equals(14, externalLocations.Split(" ", true).Length);
			Assert.IsTrue(externalLocations.Contains("http://metadata.dod.mil/mdr/ns/DDMS/2.0/"));
			Assert.IsTrue(externalLocations.Contains("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
			Assert.IsTrue(externalLocations.Contains("http://www.opengis.net/gml"));
			Assert.IsTrue(externalLocations.Contains("http://www.opengis.net/gml/3.2"));
		}

		/// <summary>
		/// Accessor for the reader
		/// </summary>
		private DDMSReader Reader {
			get {
				return _reader;
			}
		}
	}

}