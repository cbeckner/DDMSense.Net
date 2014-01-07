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
namespace buri.ddmsence.ddms.resource {


	using Element = nu.xom.Element;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:addressee elements </para>
	/// 
	/// <para> Because a ddms:addressee is a local component, we cannot load a valid document from a unit test data file. We
	/// have to build the well-formed Element ourselves. </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class AddresseeTest : AbstractBaseTestCase {

		/// <summary>
		/// Constructor
		/// </summary>
		public AddresseeTest() : base(null) {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		/// <param name="useOrg"> true to put an organization in, false for a person </param>
		public static Element GetFixtureElement(bool useOrg) {
			try {
				DDMSVersion version = DDMSVersion.CurrentVersion;
				Element element = Util.buildDDMSElement(Addressee.getName(version), null);
				element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
				element.appendChild(useOrg ? OrganizationTest.Fixture.XOMElementCopy : PersonTest.Fixture.XOMElementCopy);
				SecurityAttributesTest.Fixture.addTo(element);
				return (element);
			} catch (InvalidDDMSException e) {
				fail("Could not create fixture: " + e.Message);
			}
			return (null);
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<Addressee> FixtureList {
			get {
				try {
					IList<Addressee> list = new List<Addressee>();
					list.Add(new Addressee(GetFixtureElement(true)));
					return (list);
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
		private Addressee GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Addressee component = null;
			try {
				component = new Addressee(element);
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
		/// <param name="entity"> the person or organization in this role </param>
		/// <param name="org"> the organization </param>
		private Addressee GetInstance(string message, IRoleEntity entity) {
			bool expectFailure = !Util.isEmpty(message);
			Addressee component = null;
			try {
				component = new Addressee(entity, SecurityAttributesTest.Fixture);
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
			text.Append(BuildOutput(isHTML, "addressee.entityType", "organization"));
			text.Append(BuildOutput(isHTML, "addressee.name", "DISA"));
			text.Append(BuildOutput(isHTML, "addressee.classification", "U"));
			text.Append(BuildOutput(isHTML, "addressee.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:addressee ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
				xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
				xml.Append("</ddms:addressee>");
				return (xml.ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetFixtureElement(true)), DEFAULT_DDMS_PREFIX, Addressee.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields, organization
				GetInstance(SUCCESS, GetFixtureElement(true));

				// All fields, person
				GetInstance(SUCCESS, GetFixtureElement(false));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields, organization
				GetInstance(SUCCESS, OrganizationTest.Fixture);

				// All fields, person
				GetInstance(SUCCESS, PersonTest.Fixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Missing entity
				Element element = Util.buildDDMSElement(Addressee.getName(version), null);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("entity is required.", element);

				// Missing security attributes
				element = Util.buildDDMSElement(Addressee.getName(version), null);
				element.appendChild(OrganizationTest.Fixture.XOMElementCopy);
				GetInstance("classification is required.", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Missing entity
				GetInstance("entity is required.", (IRoleEntity) null);

				// Wrong entity
				GetInstance("The entity must be a person or an organization.", new Service(Util.getXsListAsList("Name"), null, null));

				// Missing security attributes
				try {
					new Addressee(OrganizationTest.Fixture, null);
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
				Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Addressee elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
				Addressee dataComponent = GetInstance(SUCCESS, OrganizationTest.Fixture);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Addressee elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
				Addressee dataComponent = GetInstance(SUCCESS, PersonTest.Fixture);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, OrganizationTest.Fixture);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, OrganizationTest.Fixture);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new Addressee(OrganizationTest.Fixture, SecurityAttributesTest.Fixture);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The addressee element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Equality after Building, organization
				Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
				Addressee.Builder builder = new Addressee.Builder(component);
				assertEquals(component, builder.commit());

				// Equality after Building, person
				component = GetInstance(SUCCESS, GetFixtureElement(false));
				builder = new Addressee.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Addressee.Builder builder = new Addressee.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Person.Names = Util.getXsListAsList("Name");
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Addressee.Builder builder = new Addressee.Builder();
				builder.Person.Names = Util.getXsListAsList("Name");
				builder.Person.Surname = "Surname";
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
				builder.SecurityAttributes.Classification = "U";
				builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				builder.commit();
			}
		}
	}

}