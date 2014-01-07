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
namespace buri.ddmsence.ddms.security.ntk {


	using Element = nu.xom.Element;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ntk:AccessIndividual elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class IndividualTest : AbstractBaseTestCase {

		/// <summary>
		/// Constructor
		/// </summary>
		public IndividualTest() : base("accessIndividual.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static Individual Fixture {
			get {
				try {
					return (new Individual(SystemNameTest.Fixture, IndividualValueTest.FixtureList, SecurityAttributesTest.Fixture));
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<Individual> FixtureList {
			get {
				IList<Individual> list = new List<Individual>();
				list.Add(IndividualTest.Fixture);
				return (list);
			}
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private Individual GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Individual component = null;
			try {
				component = new Individual(element);
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
		/// <param name="systemName"> the system (required) </param>
		/// <param name="values"> the values (1 required) </param>
		private Individual GetInstance(string message, SystemName systemName, IList<IndividualValue> values) {
			bool expectFailure = !Util.isEmpty(message);
			Individual component = null;
			try {
				component = new Individual(systemName, values, SecurityAttributesTest.Fixture);
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
			text.Append(SystemNameTest.Fixture.getOutput(isHTML, "individual.", ""));
			text.Append(IndividualValueTest.FixtureList[0].getOutput(isHTML, "individual.", ""));
			text.Append(BuildOutput(isHTML, "individual.classification", "U"));
			text.Append(BuildOutput(isHTML, "individual.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ntk:AccessIndividual ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
			xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
			xml.Append("\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
			xml.Append("\t<ntk:AccessIndividualValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">user_2321889:Doe_John_H</ntk:AccessIndividualValue>\n");
			xml.Append("</ntk:AccessIndividual>\n");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, Individual.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ntkPrefix = PropertyReader.getPrefix("ntk");

				// Missing systemName
				Element element = Util.buildElement(ntkPrefix, Individual.getName(version), version.NtkNamespace, null);
				foreach (IndividualValue value in IndividualValueTest.FixtureList) {
					element.appendChild(value.XOMElementCopy);
				}
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("systemName is required.", element);

				// Missing individualValue
				element = Util.buildElement(ntkPrefix, Individual.getName(version), version.NtkNamespace, null);
				element.appendChild(SystemNameTest.Fixture.XOMElementCopy);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("At least one individual value is required.", element);

				// Missing security attributes
				element = Util.buildElement(ntkPrefix, Individual.getName(version), version.NtkNamespace, null);
				element.appendChild(SystemNameTest.Fixture.XOMElementCopy);
				foreach (IndividualValue value in IndividualValueTest.FixtureList) {
					element.appendChild(value.XOMElementCopy);
				}
				GetInstance("classification is required.", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Missing systemName
				GetInstance("systemName is required.", null, IndividualValueTest.FixtureList);

				// Missing individualValue
				GetInstance("At least one individual value is required.", SystemNameTest.Fixture, null);

				// Missing security attributes
				try {
					new Individual(SystemNameTest.Fixture, IndividualValueTest.FixtureList, null);
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Individual dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Individual dataComponent = GetInstance(SUCCESS, new SystemName("MDR", null, null, null, SecurityAttributesTest.Fixture), IndividualValueTest.FixtureList);
				assertFalse(elementComponent.Equals(dataComponent));

				IList<IndividualValue> list = new List<IndividualValue>();
				list.Add(IndividualValueTest.Fixture);
				list.Add(IndividualValueTest.Fixture);
				dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, list);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(false), component.toXML());

				component = GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			// Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Individual.Builder builder = new Individual.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual.Builder builder = new Individual.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.IndividualValues.get(0);
				assertTrue(builder.Empty);
				builder.IndividualValues.get(1).Value = "TEST";
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Individual.Builder builder = new Individual.Builder();
				builder.SecurityAttributes.Classification = "U";
				builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				builder.SystemName.Value = "value";
				builder.SystemName.SecurityAttributes.Classification = "U";
				builder.SystemName.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");

				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "At least one individual value is required.");
				}
				builder.IndividualValues.get(0).Qualifier = "test";
				builder.IndividualValues.get(0).Value = "test";
				builder.IndividualValues.get(0).SecurityAttributes.Classification = "U";
				builder.IndividualValues.get(0).SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Individual.Builder builder = new Individual.Builder();
				assertNotNull(builder.IndividualValues.get(1));
			}
		}
	}

}