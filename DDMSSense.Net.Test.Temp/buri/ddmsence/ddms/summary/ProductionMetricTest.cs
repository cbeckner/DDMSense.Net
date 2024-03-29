using System.Collections.Generic;
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
namespace buri.ddmsence.ddms.summary {


	using Element = nu.xom.Element;
	using Rights = buri.ddmsence.ddms.resource.Rights;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:productionMetric elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class ProductionMetricTest : AbstractBaseTestCase {

		private const string TEST_SUBJECT = "FOOD";
		private const string TEST_COVERAGE = "AFG";

		/// <summary>
		/// Constructor
		/// </summary>
		public ProductionMetricTest() : base("productionMetric.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<ProductionMetric> FixtureList {
			get {
				try {
					DDMSVersion version = DDMSVersion.CurrentVersion;
					IList<ProductionMetric> metrics = new List<ProductionMetric>();
					if (version.isAtLeast("4.0.1")) {
						metrics.Add(new ProductionMetric("FOOD", "AFG", SecurityAttributesTest.Fixture));
					}
					return (metrics);
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private ProductionMetric GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			ProductionMetric component = null;
			try {
				component = new ProductionMetric(element);
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
		/// <param name="subject"> a method of categorizing the subject of a document in a fashion understandable by DDNI-A
		/// (required) </param>
		/// <param name="coverage"> a method of categorizing the coverage of a document in a fashion understandable by DDNI-A
		/// (required) </param>
		/// <param name="label"> the label (required) </param>
		/// <returns> a valid object </returns>
		private ProductionMetric GetInstance(string message, string subject, string coverage) {
			bool expectFailure = !Util.isEmpty(message);
			ProductionMetric component = null;
			try {
				component = new ProductionMetric(subject, coverage, SecurityAttributesTest.Fixture);
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
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws buri.ddmsence.ddms.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "productionMetric.subject", TEST_SUBJECT));
			text.Append(BuildOutput(isHTML, "productionMetric.coverage", TEST_COVERAGE));
			text.Append(BuildOutput(isHTML, "productionMetric.classification", "U"));
			text.Append(BuildOutput(isHTML, "productionMetric.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:productionMetric ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
				xml.Append("ddms:subject=\"").Append(TEST_SUBJECT).Append("\" ").Append("ddms:coverage=\"").Append(TEST_COVERAGE).Append("\" ");
				xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
				xml.Append(" />");
				return (xml.ToString());
			}
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, ProductionMetric.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Missing subject
				Element element = Util.buildDDMSElement(ProductionMetric.getName(version), null);
				element.addAttribute(Util.buildDDMSAttribute("coverage", TEST_COVERAGE));
				GetInstance("subject attribute is required.", element);

				// Missing coverage
				element = Util.buildDDMSElement(ProductionMetric.getName(version), null);
				element.addAttribute(Util.buildDDMSAttribute("subject", TEST_SUBJECT));
				GetInstance("coverage attribute is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Missing subject
				GetInstance("subject attribute is required.", null, TEST_COVERAGE);

				// Missing coverage
				GetInstance("coverage attribute is required.", TEST_SUBJECT, null);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				ProductionMetric dataComponent = GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				ProductionMetric dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_COVERAGE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_SUBJECT, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new ProductionMetric(TEST_SUBJECT, TEST_COVERAGE, null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The productionMetric element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
				ProductionMetric.Builder builder = new ProductionMetric.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric.Builder builder = new ProductionMetric.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Coverage = TEST_COVERAGE;
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProductionMetric.Builder builder = new ProductionMetric.Builder();
				assertNull(builder.commit());
				builder.Coverage = TEST_COVERAGE;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "subject attribute is required.");
				}
				builder.Subject = TEST_SUBJECT;
				builder.commit();
			}
		}
	}

}