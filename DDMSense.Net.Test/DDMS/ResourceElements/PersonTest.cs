//using System.Collections.Generic;
//using System.Text;

///* Copyright 2010 - 2013 by Brian Uri!
   
//   This file is part of DDMSence.
   
//   This library is free software; you can redistribute it and/or modify
//   it under the terms of version 3.0 of the GNU Lesser General Public 
//   License as published by the Free Software Foundation.
   
//   This library is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
//   GNU Lesser General Public License for more details.
   
//   You should have received a copy of the GNU Lesser General Public 
//   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

//   You can contact the author at ddmsence@urizone.net. The DDMSence
//   home page is located at http://ddmsence.urizone.net/
// */
//namespace DDMSense.Test.DDMS.ResourceElements {



//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;

//    /// <summary>
//    /// <para> Tests related to ddms:person elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class PersonTest : AbstractBaseTestCase {

//        private const string TEST_SURNAME = "Uri";
//        private const string TEST_USERID = "123";
//        private const string TEST_AFFILIATION = "DISA";
//        private static readonly IList<string> TEST_NAMES = new List<string>();
//        private static readonly IList<string> TEST_PHONES = new List<string>();
//        private static readonly IList<string> TEST_EMAILS = new List<string>();
//        static PersonTest() {
//            TEST_NAMES.Add("Brian");
//            TEST_NAMES.Add("BU");
//            TEST_PHONES.Add("703-885-1000");
//            TEST_EMAILS.Add("ddms@fgm.com");
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public PersonTest() : base("person.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Person Fixture {
//            get {
//                try {
//                    return (new Person(Util.getXsListAsList("Brian"), TEST_SURNAME, null, null, null, null, null));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private Person GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Person component = null;
//            try {
//                component = new Person(element);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="surname"> the surname of the person </param>
//        /// <param name="names"> an ordered list of names </param>
//        /// <param name="userID"> optional unique identifier within an organization </param>
//        /// <param name="affiliation"> organizational affiliation of the person </param>
//        /// <param name="phones"> an ordered list of phone numbers </param>
//        /// <param name="emails"> an ordered list of email addresses </param>
//        private Person GetInstance(string message, string surname, IList<string> names, string userID, string affiliation, IList<string> phones, IList<string> emails) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Person component = null;
//            try {
//                component = new Person(names, surname, phones, emails, userID, affiliation);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, "entityType", Person.getName(version)));
//            foreach (string name in TEST_NAMES) {
//                text.Append(BuildOutput(isHTML, "name", name));
//            }
//            foreach (string phone in TEST_PHONES) {
//                text.Append(BuildOutput(isHTML, "phone", phone));
//            }
//            foreach (string email in TEST_EMAILS) {
//                text.Append(BuildOutput(isHTML, "email", email));
//            }
//            text.Append(BuildOutput(isHTML, "surname", TEST_SURNAME));
//            text.Append(BuildOutput(isHTML, "userID", TEST_USERID));
//            text.Append(BuildOutput(isHTML, "affiliation", TEST_AFFILIATION));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:").Append(Person.getName(version)).Append(" ").Append(XmlnsDDMS).Append(">\n");
//            foreach (string name in TEST_NAMES) {
//                xml.Append("\t<ddms:name>").Append(name).Append("</ddms:name>\n");
//            }
//            xml.Append("\t<ddms:surname>").Append(TEST_SURNAME).Append("</ddms:surname>\n");
//            if (version.isAtLeast("4.0.1")) {
//                foreach (string phone in TEST_PHONES) {
//                    xml.Append("\t<ddms:phone>").Append(phone).Append("</ddms:phone>\n");
//                }
//                foreach (string email in TEST_EMAILS) {
//                    xml.Append("\t<ddms:email>").Append(email).Append("</ddms:email>\n");
//                }
//                xml.Append("\t<ddms:userID>").Append(TEST_USERID).Append("</ddms:userID>\n");
//                xml.Append("\t<ddms:affiliation>").Append(TEST_AFFILIATION).Append("</ddms:affiliation>\n");
//            } else {
//                xml.Append("\t<ddms:userID>").Append(TEST_USERID).Append("</ddms:userID>\n");
//                xml.Append("\t<ddms:affiliation>").Append(TEST_AFFILIATION).Append("</ddms:affiliation>\n");
//                foreach (string phone in TEST_PHONES) {
//                    xml.Append("\t<ddms:phone>").Append(phone).Append("</ddms:phone>\n");
//                }
//                foreach (string email in TEST_EMAILS) {
//                    xml.Append("\t<ddms:email>").Append(email).Append("</ddms:email>\n");
//                }
//            }
//            xml.Append("</ddms:").Append(Person.getName(version)).Append(">");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Person.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Person.getName(version), null);
//                element.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                element.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

//                // No optional fields
//                GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, null, null, null, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string personName = Person.getName(version);
//                // Missing name
//                XElement entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                GetInstance("At least 1 name element must exist.", entityElement);

//                // Empty name
//                entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("name", ""));
//                entityElement.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                GetInstance("At least 1 name element must have a non-empty value.", entityElement);

//                // Missing surname
//                entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
//                GetInstance("surname is required.", entityElement);

//                // Empty surname
//                entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("surname", ""));
//                GetInstance("surname is required.", entityElement);

//                // Too many surnames
//                entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                entityElement.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                entityElement.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
//                GetInstance("Exactly 1 surname element must exist.", entityElement);

//                // Too many userIds
//                entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                entityElement.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
//                entityElement.appendChild(Util.buildDDMSElement("userID", TEST_USERID));
//                entityElement.appendChild(Util.buildDDMSElement("userID", TEST_USERID));
//                GetInstance("No more than 1 userID", entityElement);

//                // Too many affiliations
//                entityElement = Util.buildDDMSElement(personName, null);
//                entityElement.appendChild(Util.buildDDMSElement("surname", TEST_SURNAME));
//                entityElement.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
//                entityElement.appendChild(Util.buildDDMSElement("affiliation", TEST_AFFILIATION));
//                entityElement.appendChild(Util.buildDDMSElement("affiliation", TEST_AFFILIATION));
//                GetInstance("No more than 1 affiliation", entityElement);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Missing name
//                GetInstance("At least 1 name element must exist.", TEST_SURNAME, null, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

//                // Empty name
//                IList<string> names = new List<string>();
//                names.Add("");
//                GetInstance("At least 1 name element must have a non-empty value.", TEST_SURNAME, names, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

//                // Missing surname
//                GetInstance("surname is required.", null, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

//                // Empty surname
//                GetInstance("surname is required.", "", TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No warnings
//                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // Empty userID
//                XElement entityElement = Util.buildDDMSElement(Person.getName(version), null);
//                entityElement.appendChild(Util.buildDDMSElement("name", "name"));
//                entityElement.appendChild(Util.buildDDMSElement("surname", "name"));
//                entityElement.appendChild(Util.buildDDMSElement("userID", ""));
//                component = new Person(entityElement);
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A ddms:userID element was found with no value.";
//                string locator = "ddms:" + Person.getName(version);
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

//                // Empty affiliation
//                entityElement = Util.buildDDMSElement(Person.getName(version), null);
//                entityElement.appendChild(Util.buildDDMSElement("name", "name"));
//                entityElement.appendChild(Util.buildDDMSElement("surname", "name"));
//                entityElement.appendChild(Util.buildDDMSElement("affiliation", ""));
//                component = new Person(entityElement);
//                assertEquals(1, component.ValidationWarnings.size());
//                text = "A ddms:affiliation element was found with no value.";
//                locator = "ddms:" + Person.getName(version);
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Person elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Person dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Person elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Person dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//                assertFalse(elementComponent.Equals(dataComponent));

//                IList<string> differentNames = new List<string>();
//                differentNames.Add("Brian");
//                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, differentNames, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, DIFFERENT_VALUE, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, DIFFERENT_VALUE, TEST_PHONES, TEST_EMAILS);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, null, TEST_EMAILS);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, null);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Person.Builder builder = new Person.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Person.Builder builder = new Person.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Names = TEST_NAMES;
//                assertFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Person.Builder builder = new Person.Builder();
//                builder.Phones = TEST_PHONES;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "surname is required.");
//                }
//                builder.Surname = TEST_SURNAME;
//                builder.Names = TEST_NAMES;
//                builder.commit();
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Person.Builder builder = new Person.Builder();
//                assertNotNull(builder.Names.get(1));
//                assertNotNull(builder.Phones.get(1));
//                assertNotNull(builder.Emails.get(1));
//            }
//        }
//    }

//}