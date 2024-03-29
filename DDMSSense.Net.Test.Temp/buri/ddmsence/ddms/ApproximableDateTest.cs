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
namespace buri.ddmsence.ddms {

	using Element = nu.xom.Element;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to elements of type ddms:ApproximableDateType (includes ddms:acquiredOn, and the ddms:start / ddms:end values in a ddms:temporalCoverage element.</para>
	/// 
	/// <para> Because these are local components, we cannot load a valid document from a unit test data file. We
	/// have to build the well-formed Element ourselves. </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.1.0
	/// </summary>
	public class ApproximableDateTest : AbstractBaseTestCase {

		private const string TEST_NAME = "acquiredOn";
		private const string TEST_DESCRIPTION = "description";
		private const string TEST_APPROXIMABLE_DATE = "2012";
		private const string TEST_APPROXIMATION = "1st qtr";
		private const string TEST_START_DATE = "2012-01";
		private const string TEST_END_DATE = "2012-03-31";

		/// <summary>
		/// Constructor
		/// </summary>
		public ApproximableDateTest() : base(null) {
			RemoveSupportedVersions("2.0 3.0 3.1 4.0.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		/// <param name="name"> the element name </param>
		/// <param name="includeAllFields"> true to include optional fields </param>
		public static Element GetFixtureElement(string name, bool includeAllFields) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			Element element = Util.buildDDMSElement(name, null);
			element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
			if (includeAllFields) {
				Util.addDDMSChildElement(element, "description", TEST_DESCRIPTION);

				Element approximableElment = Util.buildDDMSElement("approximableDate", TEST_APPROXIMABLE_DATE);
				Util.addDDMSAttribute(approximableElment, "approximation", TEST_APPROXIMATION);
				element.appendChild(approximableElment);

				Element searchableElement = Util.buildDDMSElement("searchableDate", null);
				Util.addDDMSChildElement(searchableElement, "start", TEST_START_DATE);
				Util.addDDMSChildElement(searchableElement, "end", TEST_END_DATE);
				element.appendChild(searchableElement);
			}
			return (element);
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private ApproximableDate GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			ApproximableDate component = null;
			try {
				component = new ApproximableDate(element);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Helper method to create an object which is expected to be valid.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="name"> the name of the element </param>
		/// <param name="description"> the description of this approximable date (optional) </param>
		/// <param name="approximableDate"> the value of the approximable date (optional) </param>
		/// <param name="approximation"> an attribute that decorates the date (optional) </param>
		/// <param name="searchableStartDate"> the lower bound for this approximable date (optional) </param>
		/// <param name="searchableEndDate"> the upper bound for this approximable date (optional) </param>
		/// <param name="entity"> the person or organization in this role </param>
		/// <param name="org"> the organization </param>
		private ApproximableDate GetInstance(string message, string name, string description, string approximableDate, string approximation, string searchableStartDate, string searchableEndDate) {
			bool expectFailure = !Util.isEmpty(message);
			ApproximableDate component = null;
			try {
				component = new ApproximableDate(name, description, approximableDate, approximation, searchableStartDate, searchableEndDate);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "acquiredOn.description", TEST_DESCRIPTION));
			text.Append(BuildOutput(isHTML, "acquiredOn.approximableDate", TEST_APPROXIMABLE_DATE));
			text.Append(BuildOutput(isHTML, "acquiredOn.approximableDate.approximation", TEST_APPROXIMATION));
			text.Append(BuildOutput(isHTML, "acquiredOn.searchableDate.start", TEST_START_DATE));
			text.Append(BuildOutput(isHTML, "acquiredOn.searchableDate.end", TEST_END_DATE));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:acquiredOn ").Append(XmlnsDDMS).Append(">");
				xml.Append("<ddms:description>").Append(TEST_DESCRIPTION).Append("</ddms:description>");
				xml.Append("<ddms:approximableDate ddms:approximation=\"").Append(TEST_APPROXIMATION).Append("\">");
				xml.Append(TEST_APPROXIMABLE_DATE).Append("</ddms:approximableDate>");
				xml.Append("<ddms:searchableDate><ddms:start>").Append(TEST_START_DATE).Append("</ddms:start>");
				xml.Append("<ddms:end>").Append(TEST_END_DATE).Append("</ddms:end></ddms:searchableDate>");
				xml.Append("</ddms:acquiredOn>");
				return (xml.ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				AssertNameAndNamespace(GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true)), DEFAULT_DDMS_PREFIX, TEST_NAME);
				GetInstance("The element name must be one of", WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));

				// No fields
				GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, false));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);

				// No fields
				GetInstance(SUCCESS, TEST_NAME, null, null, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				 // Wrong date format: approximableDate
				Element element = Util.buildDDMSElement(TEST_NAME, null);
				Element approximableElment = Util.buildDDMSElement("approximableDate", "---31");
				element.appendChild(approximableElment);
				GetInstance("The date datatype", element);

				 // Invalid approximation
				element = Util.buildDDMSElement(TEST_NAME, null);
				approximableElment = Util.buildDDMSElement("approximableDate", TEST_APPROXIMABLE_DATE);
				Util.addDDMSAttribute(approximableElment, "approximation", "almost-nearly");
				element.appendChild(approximableElment);
				GetInstance("The approximation must be one of", element);

				 // Wrong date format: start
				element = Util.buildDDMSElement(TEST_NAME, null);
				Element searchableElement = Util.buildDDMSElement("searchableDate", null);
				Util.addDDMSChildElement(searchableElement, "start", "---31");
				Util.addDDMSChildElement(searchableElement, "end", TEST_END_DATE);
				element.appendChild(searchableElement);
				GetInstance("The date datatype", element);

				 // Wrong date format: end
				element = Util.buildDDMSElement(TEST_NAME, null);
				searchableElement = Util.buildDDMSElement("searchableDate", null);
				Util.addDDMSChildElement(searchableElement, "start", TEST_START_DATE);
				Util.addDDMSChildElement(searchableElement, "end", "---31");
				element.appendChild(searchableElement);
				GetInstance("The date datatype", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				 // Wrong date format: approximableDate
				GetInstance("The date datatype", TEST_NAME, TEST_DESCRIPTION, "---31", TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);

				 // Invalid approximation
				GetInstance("The approximation", TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, "almost-nearly", TEST_START_DATE, TEST_END_DATE);

				 // Wrong date format: start
				GetInstance("The date datatype", TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, "---31", TEST_END_DATE);

				 // Wrong date format: end
				GetInstance("The date datatype", TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, "---31");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
				assertEquals(0, component.ValidationWarnings.size());

				// Empty element
				component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, false));
				assertEquals(1, component.ValidationWarnings.size());
				string text = "A completely empty ddms:acquiredOn";
				string locator = "ddms:acquiredOn";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

				// Description element with no child text
				Element element = Util.buildDDMSElement(TEST_NAME, null);
				element.appendChild(Util.buildDDMSElement("description", null));
				Util.addDDMSChildElement(element, "description", null);
				Util.addDDMSChildElement(element, "approximableDate", TEST_APPROXIMABLE_DATE);
				component = GetInstance(SUCCESS, element);
				assertEquals(1, component.ValidationWarnings.size());
				text = "A completely empty ddms:description";
				locator = "ddms:acquiredOn";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate elementComponent = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
				ApproximableDate dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate elementComponent = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
				ApproximableDate dataComponent = GetInstance(SUCCESS, "approximableStart", TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAME, DIFFERENT_VALUE, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, "2000", TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, "2nd qtr", TEST_START_DATE, TEST_END_DATE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, "2000", TEST_END_DATE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, "2500");
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new ApproximableDate(TEST_NAME, null, null, null, null, null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The acquiredOn element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
				ApproximableDate.Builder builder = new ApproximableDate.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate.Builder builder = new ApproximableDate.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Description = TEST_DESCRIPTION;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApproximableDate.Builder builder = new ApproximableDate.Builder();
				builder.Name = TEST_NAME;
				builder.ApproximableDate = TEST_APPROXIMABLE_DATE;
				builder.Approximation = "almost-nearly";
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The approximation");
				}
				builder.Approximation = TEST_APPROXIMATION;
				builder.commit();
			}
		}
	}

}