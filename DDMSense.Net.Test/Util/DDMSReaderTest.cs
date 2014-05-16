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
	public class DDMSReaderTest : AbstractBaseTestCase {

		private DDMSReader _reader;

		public DDMSReaderTest() : base(null) {
			_reader = new DDMSReader();
		}

		public virtual void TestGetElementNullString() {
			try {
				Reader.GetElement((string) null);
				Assert.Fail("Allowed invalid data.");
			} catch (IOException) {
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "XML string is required.");
			}
		}

		public virtual void TestGetElementNullInputStream() {
			try {
				Reader.GetElement((Stream) null);
				Assert.Fail("Allowed invalid data.");
			} catch (IOException) {
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "input stream is required.");
			}
		}

		public virtual void TestGetElementDoesNotExistString() {
			try {
				Reader.GetElement("<wrong></wrong>");
				Assert.Fail("Allowed invalid data.");
			} catch (IOException) {
				Assert.Fail("Should have thrown an InvalidDDMSException");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ValidityException");
			}
		}

		public virtual void TestGetElementDoesNotExistInputStream() {
			try {
				Reader.GetElement(new FileInputStream(new File("doesnotexist")));
				Assert.Fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "doesnotexist (The system cannot find the file specified)");
			}
		}
		
		public virtual void TestGetElementNotXML() {
			try {
				Reader.GetElement(new File("conf/ddmsence.properties"));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ParsingException");
			}
		}

		public virtual void TestGetElementFileSuccess() {
			Reader.GetElement(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml"));
		}

		public virtual void TestGetElementStringSuccess() {
			Reader.GetElement("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ddms:language " + " xmlns:ddms=\"http://metadata.dod.mil/mdr/ns/DDMS/3.0/\" " + " ddms:qualifier=\"http://purl.org/dc/elements/1.1/language\" ddms:value=\"en\" />");
		}

		public virtual void TestGetElementInputStreamSuccess() {
			Reader.GetElement(new FileInputStream(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml")));
		}

		public virtual void TestGetElementReaderSuccess() {
			Reader.GetElement(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml")));
		}

		public virtual void TestGetResourceFailure() {
			try {
				Reader.GetDDMSResource(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml"));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}

		public virtual void TestGetResourceSuccessFile() {
			Reader.GetDDMSResource(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml"));
		}

		public virtual void TestGetResourceSuccessString() {
			LineNumberReader reader = new LineNumberReader(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
			StringBuilder xmlString = new StringBuilder();
			string nextLine = reader.readLine();
			while (nextLine != null) {
				xmlString.Append(nextLine);
				nextLine = reader.readLine();
			}
			Reader.GetDDMSResource(xmlString.ToString());
		}

		public virtual void TestGetResourceSuccessInputStream() {
			Reader.GetDDMSResource(new FileInputStream(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
		}

		public virtual void TestGetResourceSuccessReader() {
			Reader.GetDDMSResource(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
		}

		public virtual void TestGetExternalSchemaLocation() {
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