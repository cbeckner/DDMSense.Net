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
namespace DDMSense.Test.DDMS.ResourceElements
{


    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:organization elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class OrganizationTest : AbstractBaseTestCase
    {

        private static readonly List<string> TEST_NAMES = new List<string>();
        private static readonly List<string> TEST_PHONES = new List<string>();
        private static readonly List<string> TEST_EMAILS = new List<string>();
        static OrganizationTest()
        {
            TEST_NAMES.Add("DISA");
            TEST_NAMES.Add("PEO-GES");
            TEST_PHONES.Add("703-882-1000");
            TEST_PHONES.Add("703-885-1000");
            TEST_EMAILS.Add("ddms@fgm.com");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OrganizationTest()
            : base("organization.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Organization Fixture
        {
            get
            {
                try
                {
                    return (new Organization(Util.GetXsListAsList("DISA"), null, null, null, null, null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Generates an acronym for testing.
        /// </summary>
        private string Acronym
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "DISA" : "");
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private Organization GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Organization component = null;
            try
            {
                component = new Organization(element);
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
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="subOrganizations"> an ordered list of suborganizations </param>
        /// <param name="acronym"> the organization acronym </param>
        private Organization GetInstance(string message, List<string> names, List<string> phones, List<string> emails, List<SubOrganization> subOrganizations, string acronym)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Organization component = null;
            try
            {
                component = new Organization(names, phones, emails, subOrganizations, acronym, null);
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
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        private string GetExpectedOutput(bool isHTML)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "entityType", Organization.GetName(version)));
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
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "subOrganization", "sub1"));
                text.Append(BuildOutput(isHTML, "subOrganization.classification", "U"));
                text.Append(BuildOutput(isHTML, "subOrganization.ownerProducer", "USA"));
                text.Append(BuildOutput(isHTML, "subOrganization", "sub2"));
                text.Append(BuildOutput(isHTML, "subOrganization.classification", "U"));
                text.Append(BuildOutput(isHTML, "subOrganization.ownerProducer", "USA"));
                text.Append(BuildOutput(isHTML, "acronym", "DISA"));
            }
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
            xml.Append("<ddms:").Append(Organization.GetName(version)).Append(" ").Append(XmlnsDDMS);
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append(" ddms:acronym=\"DISA\"");
            }
            xml.Append(">\n");
            foreach (string name in TEST_NAMES)
            {
                xml.Append("\t<ddms:name>").Append(name).Append("</ddms:name>\n");
            }
            foreach (string phone in TEST_PHONES)
            {
                xml.Append("\t<ddms:phone>").Append(phone).Append("</ddms:phone>\n");
            }
            foreach (string email in TEST_EMAILS)
            {
                xml.Append("\t<ddms:email>").Append(email).Append("</ddms:email>\n");
            }
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t<ddms:subOrganization ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">sub1</ddms:subOrganization>\n");
                xml.Append("\t<ddms:subOrganization ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">sub2</ddms:subOrganization>\n");
            }
            xml.Append("</ddms:").Append(Organization.GetName(version)).Append(">");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Organization.GetName(version));
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
                XElement element = Util.BuildDDMSElement(Organization.GetName(version), null);
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
                GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);

                // No optional fields
                GetInstance(SUCCESS, TEST_NAMES, null, null, SubOrganizationTest.FixtureList, Acronym);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing name
                XElement entityElement = Util.BuildDDMSElement(Organization.GetName(version), null);
                GetInstance("At least 1 name element must exist.", entityElement);

                // Empty name
                entityElement = Util.BuildDDMSElement(Organization.GetName(version), null);
                entityElement.Add(Util.BuildDDMSElement("name", ""));
                GetInstance("At least 1 name element must have a non-empty value.", entityElement);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing name
                GetInstance("At least 1 name element must exist.", null, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);

                // Empty name
                List<string> names = new List<string>();
                names.Add("");
                GetInstance("At least 1 name element must have a non-empty value.", names, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Organization component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count);

                // Empty acronym in DDMS 4.0.1
                if (version.IsAtLeast("4.0.1"))
                {
                    XElement element = Util.BuildDDMSElement(Organization.GetName(version), null);
                    element.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                    element.Add(new XAttribute(XName.Get("ddms:acronym", version.Namespace), ""));
                    component = GetInstance(SUCCESS, element);
                    Assert.Equals(1, component.ValidationWarnings.Count);
                    string text = "A ddms:acronym attribute was found with no value.";
                    string locator = "ddms:organization";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Organization elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Organization dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                Organization elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Organization dataComponent = GetInstance(SUCCESS, TEST_NAMES, null, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, null, SubOrganizationTest.FixtureList, Acronym);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                if (version.IsAtLeast("4.0.1"))
                {
                    dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, null, Acronym);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));

                    dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, "NewACRONYM");
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Organization component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);
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
                Organization component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, SubOrganizationTest.FixtureList, Acronym);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestSubOrganizationReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<SubOrganization> subOrgs = SubOrganizationTest.FixtureList;
                GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, subOrgs, Acronym);
                GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS, subOrgs, Acronym);
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionAcronym()
        {
            DDMSVersion.SetCurrentVersion("4.1");
            Organization component = GetInstance(SUCCESS, GetValidElement("4.1"));
            Organization.Builder builder = new Organization.Builder(component);
            builder.SubOrganizations.Clear();
            try
            {
                DDMSVersion.SetCurrentVersion("3.1");
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "An organization cannot have an acronym");
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            // This test is implicit -- SubOrganization cannot even be instantiated except in DDMS 4.0.1 or later.
        }

        [TestMethod]
        public virtual void TestIndexLevelsObjectLists()
        {
            List<string> names = Util.GetXsListAsList("DISA");
            Organization org = new Organization(names, null, null, SubOrganizationTest.FixtureList, null, null);

            PropertyReader.SetProperty("output.indexLevel", "0");
            Assert.Equals("entityType: organization\nname: DISA\n" + "subOrganization: sub1\nsubOrganization.classification: U\nsubOrganization.ownerProducer: USA\n" + "subOrganization: sub2\nsubOrganization.classification: U\nsubOrganization.ownerProducer: USA\n", org.ToText());

            PropertyReader.SetProperty("output.indexLevel", "1");
            Assert.Equals("entityType: organization\nname: DISA\n" + "subOrganization[1]: sub1\nsubOrganization[1].classification: U\nsubOrganization[1].ownerProducer: USA\n" + "subOrganization[2]: sub2\nsubOrganization[2].classification: U\nsubOrganization[2].ownerProducer: USA\n", org.ToText());

            PropertyReader.SetProperty("output.indexLevel", "2");
            Assert.Equals("entityType: organization\nname[1]: DISA\n" + "subOrganization[1]: sub1\nsubOrganization[1].classification: U\nsubOrganization[1].ownerProducer: USA\n" + "subOrganization[2]: sub2\nsubOrganization[2].classification: U\nsubOrganization[2].ownerProducer: USA\n", org.ToText());
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Organization component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Organization.Builder builder = new Organization.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                
                Organization.Builder builder = new Organization.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);

                // List Emptiness
                if (version.IsAtLeast("4.0.1"))
                {
                    Assert.IsTrue(builder.Empty);
                    //TODO: Not sure what to do here (MAM).
                    //Original Java: builder.getSubOrganizations().get(0);
                    builder.SubOrganizations[0];
                    Assert.IsTrue(builder.Empty);
                    builder.SubOrganizations[0].Value = "TEST";
                    Assert.IsFalse(builder.Empty);
                }

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

                Organization.Builder builder = new Organization.Builder();
                builder.Phones = TEST_PHONES;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least 1 name element must exist.");
                }
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
                Organization.Builder builder = new Organization.Builder();
                Assert.IsNotNull(builder.Names[1]);
                Assert.IsNotNull(builder.Phones[1]);
                Assert.IsNotNull(builder.Emails[1]);
            }
        }
    }

}