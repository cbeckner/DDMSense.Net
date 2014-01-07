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
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:service elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class ServiceTest : AbstractBaseTestCase {

		private static readonly IList<string> TEST_NAMES = new List<string>();
		private static readonly IList<string> TEST_PHONES = new List<string>();
		private static readonly IList<string> TEST_EMAILS = new List<string>();
		static ServiceTest() {
			TEST_NAMES.Add("https://metadata.dod.mil/ebxmlquery/soap");
			TEST_PHONES.Add("703-882-1000");
			TEST_EMAILS.Add("ddms@fgm.com");
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public ServiceTest() : base("service.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static Service Fixture {
			get {
				try {
					return (new Service(Util.getXsListAsList("https://metadata.dod.mil/ebxmlquery/soap"), null, null));
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
		private Service GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Service component = null;
			try {
				component = new Service(element);
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
		/// <param name="names"> an ordered list of names </param>
		/// <param name="phones"> an ordered list of phone numbers </param>
		/// <param name="emails"> an ordered list of email addresses </param>
		private Service GetInstance(string message, IList<string> names, IList<string> phones, IList<string> emails) {
			bool expectFailure = !Util.isEmpty(message);
			Service component = null;
			try {
				component = new Service(names, phones, emails);
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
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "entityType", Service.getName(version)));
			foreach (string name in TEST_NAMES) {
				text.Append(BuildOutput(isHTML, "name", name));
			}
			foreach (string phone in TEST_PHONES) {
				text.Append(BuildOutput(isHTML, "phone", phone));
			}
			foreach (string email in TEST_EMAILS) {
				text.Append(BuildOutput(isHTML, "email", email));
			}
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:").Append(Service.getName(version)).Append(" ").Append(XmlnsDDMS).Append(">\n");
			foreach (string name in TEST_NAMES) {
				xml.Append("\t<ddms:name>").Append(name).Append("</ddms:name>\n");
			}
			foreach (string phone in TEST_PHONES) {
				xml.Append("\t<ddms:phone>").Append(phone).Append("</ddms:phone>\n");
			}
			foreach (string email in TEST_EMAILS) {
				xml.Append("\t<ddms:email>").Append(email).Append("</ddms:email>\n");
			}
			xml.Append("</ddms:").Append(Service.getName(version)).Append(">");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Service.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = Util.buildDDMSElement(Service.getName(version), null);
				element.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);

				// No optional fields
				GetInstance(SUCCESS, TEST_NAMES, null, null);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// Missing name
				Element element = Util.buildDDMSElement(Service.getName(version), null);
				GetInstance("At least 1 name element must exist.", element);

				// Empty name
				element = Util.buildDDMSElement(Service.getName(version), null);
				element.appendChild(Util.buildDDMSElement("name", ""));
				GetInstance("At least 1 name element must have a non-empty value.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Missing name
				GetInstance("At least 1 name element must exist.", null, TEST_PHONES, TEST_EMAILS);

				// Empty name
				IList<string> names = new List<string>();
				names.Add("");
				GetInstance("At least 1 name element must have a non-empty value.", names, TEST_PHONES, TEST_EMAILS);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Service elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Service dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Service elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Service dataComponent = GetInstance(SUCCESS, TEST_NAMES, null, TEST_EMAILS);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, null);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Service.Builder builder = new Service.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Service.Builder builder = new Service.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Names = TEST_NAMES;
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Service.Builder builder = new Service.Builder();
				builder.Phones = Util.getXsListAsList("703-885-1000");
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "At least 1 name element must exist.");
				}
				builder.Names = TEST_NAMES;
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Service.Builder builder = new Service.Builder();
				assertNotNull(builder.Names.get(1));
				assertNotNull(builder.Phones.get(1));
				assertNotNull(builder.Emails.get(1));
			}
		}
	}

}