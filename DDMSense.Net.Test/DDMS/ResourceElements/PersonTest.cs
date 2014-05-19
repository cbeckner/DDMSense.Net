using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

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
namespace DDMSense.Test.DDMS.ResourceElements
{



    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:person elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class PersonTest : AbstractBaseTestCase
    {

        private const string TEST_SURNAME = "Uri";
        private const string TEST_USERID = "123";
        private const string TEST_AFFILIATION = "DISA";
        private static readonly List<string> TEST_NAMES = new List<string>();
        private static readonly List<string> TEST_PHONES = new List<string>();
        private static readonly List<string> TEST_EMAILS = new List<string>();
        static PersonTest()
        {
            TEST_NAMES.Add("Brian");
            TEST_NAMES.Add("BU");
            TEST_PHONES.Add("703-885-1000");
            TEST_EMAILS.Add("ddms@fgm.com");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PersonTest()
            : base("person.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Person Fixture
        {
            get
            {
                try
                {
                    return (new Person(Util.GetXsListAsList("Brian"), TEST_SURNAME, null, null, null, null, null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
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
        private Person GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Person component = null;
            try
            {
                component = new Person(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="surname"> the surname of the person </param>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="userID"> optional unique identifier within an organization </param>
        /// <param name="affiliation"> organizational affiliation of the person </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        private Person GetInstance(string message, string surname, List<string> names, string userID, string affiliation, List<string> phones, List<string> emails)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Person component = null;
            try
            {
                component = new Person(names, surname, phones, emails, userID, affiliation);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "entityType", Person.GetName(version)));
            foreach (string name in TEST_NAMES)
            {
                text.Append(BuildOutput(isHTML, "name", name));
            }
            foreach (string phone in TEST_PHONES)
            {
                text.Append(BuildOutput(isHTML, "phone", phone));
            }
            foreach (string email in TEST_EMAILS)
            {
                text.Append(BuildOutput(isHTML, "email", email));
            }
            text.Append(BuildOutput(isHTML, "surname", TEST_SURNAME));
            text.Append(BuildOutput(isHTML, "userID", TEST_USERID));
            text.Append(BuildOutput(isHTML, "affiliation", TEST_AFFILIATION));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:").Append(Person.GetName(version)).Append(" ").Append(XmlnsDDMS).Append(">\n");
            foreach (string name in TEST_NAMES)
            {
                xml.Append("\t<ddms:name>").Append(name).Append("</ddms:name>\n");
            }
            xml.Append("\t<ddms:surname>").Append(TEST_SURNAME).Append("</ddms:surname>\n");
            if (version.IsAtLeast("4.0.1"))
            {
                foreach (string phone in TEST_PHONES)
                {
                    xml.Append("\t<ddms:phone>").Append(phone).Append("</ddms:phone>\n");
                }
                foreach (string email in TEST_EMAILS)
                {
                    xml.Append("\t<ddms:email>").Append(email).Append("</ddms:email>\n");
                }
                xml.Append("\t<ddms:userID>").Append(TEST_USERID).Append("</ddms:userID>\n");
                xml.Append("\t<ddms:affiliation>").Append(TEST_AFFILIATION).Append("</ddms:affiliation>\n");
            }
            else
            {
                xml.Append("\t<ddms:userID>").Append(TEST_USERID).Append("</ddms:userID>\n");
                xml.Append("\t<ddms:affiliation>").Append(TEST_AFFILIATION).Append("</ddms:affiliation>\n");
                foreach (string phone in TEST_PHONES)
                {
                    xml.Append("\t<ddms:phone>").Append(phone).Append("</ddms:phone>\n");
                }
                foreach (string email in TEST_EMAILS)
                {
                    xml.Append("\t<ddms:email>").Append(email).Append("</ddms:email>\n");
                }
            }
            xml.Append("</ddms:").Append(Person.GetName(version)).Append(">");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Person.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Person.GetName(version), null);
                element.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                element.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

                // No optional fields
                GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string personName = Person.GetName(version);
                // Missing name
                XElement entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                GetInstance("At least 1 name element must exist.", entityElement);

                // Empty name
                entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("name", ""));
                entityElement.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                GetInstance("At least 1 name element must have a non-empty value.", entityElement);

                // Missing surname
                entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                GetInstance("surname is required.", entityElement);

                // Empty surname
                entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("surname", ""));
                GetInstance("surname is required.", entityElement);

                // Too many surnames
                entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                entityElement.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                entityElement.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                GetInstance("Exactly 1 surname element must exist.", entityElement);

                // Too many userIds
                entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                entityElement.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                entityElement.Add(Util.BuildDDMSElement("userID", TEST_USERID));
                entityElement.Add(Util.BuildDDMSElement("userID", TEST_USERID));
                GetInstance("No more than 1 userID", entityElement);

                // Too many affiliations
                entityElement = Util.BuildDDMSElement(personName, null);
                entityElement.Add(Util.BuildDDMSElement("surname", TEST_SURNAME));
                entityElement.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                entityElement.Add(Util.BuildDDMSElement("affiliation", TEST_AFFILIATION));
                entityElement.Add(Util.BuildDDMSElement("affiliation", TEST_AFFILIATION));
                GetInstance("No more than 1 affiliation", entityElement);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing name
                GetInstance("At least 1 name element must exist.", TEST_SURNAME, null, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

                // Empty name
                List<string> names = new List<string>();
                names.Add("");
                GetInstance("At least 1 name element must have a non-empty value.", TEST_SURNAME, names, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

                // Missing surname
                GetInstance("surname is required.", null, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);

                // Empty surname
                GetInstance("surname is required.", "", TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());

                // Empty userID
                XElement entityElement = Util.BuildDDMSElement(Person.GetName(version), null);
                entityElement.Add(Util.BuildDDMSElement("name", "name"));
                entityElement.Add(Util.BuildDDMSElement("surname", "name"));
                entityElement.Add(Util.BuildDDMSElement("userID", ""));
                component = new Person(entityElement);
                Assert.Equals(1, component.ValidationWarnings.Count());
                string text = "A ddms:userID element was found with no value.";
                string locator = "ddms:" + Person.GetName(version);
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Empty affiliation
                entityElement = Util.BuildDDMSElement(Person.GetName(version), null);
                entityElement.Add(Util.BuildDDMSElement("name", "name"));
                entityElement.Add(Util.BuildDDMSElement("surname", "name"));
                entityElement.Add(Util.BuildDDMSElement("affiliation", ""));
                component = new Person(entityElement);
                Assert.Equals(1, component.ValidationWarnings.Count());
                text = "A ddms:affiliation element was found with no value.";
                locator = "ddms:" + Person.GetName(version);
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Person elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Person dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Person elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Person dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<string> differentNames = new List<string>();
                differentNames.Add("Brian");
                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, differentNames, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, DIFFERENT_VALUE, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, DIFFERENT_VALUE, TEST_PHONES, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, null, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_SURNAME, TEST_NAMES, TEST_USERID, TEST_AFFILIATION, TEST_PHONES, TEST_EMAILS);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Person component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Person.Builder builder = new Person.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Person.Builder builder = new Person.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Names = TEST_NAMES;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Person.Builder builder = new Person.Builder();
                builder.Phones = TEST_PHONES;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "surname is required.");
                }
                builder.Surname = TEST_SURNAME;
                builder.Names = TEST_NAMES;
                builder.Commit();
            }
        }

        [TestMethod]
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Person.Builder builder = new Person.Builder();
                Assert.IsNotNull(builder.Names[1]);
                Assert.IsNotNull(builder.Phones[1]);
                Assert.IsNotNull(builder.Emails[1]);
            }
        }
    }

}