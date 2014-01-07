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
namespace buri.ddmsence.util {


	using SAXException = org.xml.sax.SAXException;

	using InvalidDDMSException = buri.ddmsence.ddms.InvalidDDMSException;

	/// <summary>
	/// A collection of DDMSReader tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class DDMSReaderTest : AbstractBaseTestCase {

		private DDMSReader _reader;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public DDMSReaderTest() throws org.xml.sax.SAXException
		public DDMSReaderTest() : base(null) {
			_reader = new DDMSReader();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementNullFile() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementNullFile() {
			try {
				Reader.getElement((File) null);
				fail("Allowed invalid data.");
			} catch (IOException) {
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "file is required.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementNullString() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementNullString() {
			try {
				Reader.getElement((string) null);
				fail("Allowed invalid data.");
			} catch (IOException) {
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "XML string is required.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementNullInputStream() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementNullInputStream() {
			try {
				Reader.getElement((InputStream) null);
				fail("Allowed invalid data.");
			} catch (IOException) {
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "input stream is required.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementNullReader() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementNullReader() {
			try {
				Reader.getElement((Reader) null);
				fail("Allowed invalid data.");
			} catch (IOException) {
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "reader is required.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementDoesNotExistFile() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementDoesNotExistFile() {
			try {
				Reader.getElement(new File("doesnotexist"));
				fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "doesnotexist (The system cannot find the file specified)");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementDoesNotExistString() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementDoesNotExistString() {
			try {
				Reader.getElement("<wrong></wrong>");
				fail("Allowed invalid data.");
			} catch (IOException) {
				fail("Should have thrown an InvalidDDMSException");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ValidityException");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementDoesNotExistInputStream() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementDoesNotExistInputStream() {
			try {
				Reader.getElement(new FileInputStream(new File("doesnotexist")));
				fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "doesnotexist (The system cannot find the file specified)");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementDoesNotExistReader() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetElementDoesNotExistReader() {
			try {
				Reader.getElement(new FileReader(new File("doesnotexist")));
				fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "doesnotexist (The system cannot find the file specified)");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementNotXML() throws java.io.IOException
		public virtual void TestGetElementNotXML() {
			try {
				Reader.getElement(new File("conf/ddmsence.properties"));
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ParsingException");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementFileSuccess() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetElementFileSuccess() {
			Reader.getElement(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml"));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementStringSuccess() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetElementStringSuccess() {
			Reader.getElement("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ddms:language " + " xmlns:ddms=\"http://metadata.dod.mil/mdr/ns/DDMS/3.0/\" " + " ddms:qualifier=\"http://purl.org/dc/elements/1.1/language\" ddms:value=\"en\" />");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementInputStreamSuccess() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetElementInputStreamSuccess() {
			Reader.getElement(new FileInputStream(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml")));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetElementReaderSuccess() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetElementReaderSuccess() {
			Reader.getElement(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml")));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetResourceFailure() throws java.io.IOException
		public virtual void TestGetResourceFailure() {
			try {
				Reader.getDDMSResource(new File(PropertyReader.getProperty("test.unit.data"), "3.0/rights.xml"));
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetResourceSuccessFile() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetResourceSuccessFile() {
			Reader.getDDMSResource(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml"));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetResourceSuccessString() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetResourceSuccessString() {
			LineNumberReader reader = new LineNumberReader(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
			StringBuilder xmlString = new StringBuilder();
			string nextLine = reader.readLine();
			while (nextLine != null) {
				xmlString.Append(nextLine);
				nextLine = reader.readLine();
			}
			Reader.getDDMSResource(xmlString.ToString());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetResourceSuccessInputStream() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetResourceSuccessInputStream() {
			Reader.getDDMSResource(new FileInputStream(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetResourceSuccessReader() throws buri.ddmsence.ddms.InvalidDDMSException, java.io.IOException
		public virtual void TestGetResourceSuccessReader() {
			Reader.getDDMSResource(new FileReader(new File(PropertyReader.getProperty("test.unit.data"), "3.0/resource.xml")));
		}

		public virtual void TestGetExternalSchemaLocation() {
			string externalLocations = Reader.ExternalSchemaLocations;
			assertEquals(14, externalLocations.Split(" ", true).length);
			assertTrue(externalLocations.Contains("http://metadata.dod.mil/mdr/ns/DDMS/2.0/"));
			assertTrue(externalLocations.Contains("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
			assertTrue(externalLocations.Contains("http://www.opengis.net/gml"));
			assertTrue(externalLocations.Contains("http://www.opengis.net/gml/3.2"));
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