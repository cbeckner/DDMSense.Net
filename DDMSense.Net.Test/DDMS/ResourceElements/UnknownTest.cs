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
    /// <para> Tests related to ddms:unknown elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class UnknownTest : AbstractBaseTestCase
    {

        private static readonly List<string> TEST_NAMES = new List<string>();
        private static readonly List<string> TEST_PHONES = new List<string>();
        private static readonly List<string> TEST_EMAILS = new List<string>();
        static UnknownTest()
        {
            TEST_NAMES.Add("Unknown Entity");
            TEST_PHONES.Add("703-882-1000");
            TEST_EMAILS.Add("ddms@fgm.com");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UnknownTest()
            : base("unknown.xml")
        {
            RemoveSupportedVersions("2.0");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Unknown Fixture
        {
            get
            {
                try
                {
                    return (new Unknown(Util.GetXsListAsList("UnknownEntity"), null, null));
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
        private Unknown GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Unknown component = null;
            try
            {
                component = new Unknown(element);
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
        private Unknown GetInstance(string message, List<string> names, List<string> phones, List<string> emails)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Unknown component = null;
            try
            {
                component = new Unknown(names, phones, emails);
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
            text.Append(BuildOutput(isHTML, "entityType", Unknown.GetName(version)));
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
            xml.Append("<ddms:").Append(Unknown.GetName(version)).Append(" ").Append(XmlnsDDMS).Append(">\n");
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
            xml.Append("</ddms:").Append(Unknown.GetName(version)).Append(">");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Unknown.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Unknown.GetName(version), null);
                element.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                GetInstance(SUCCESS, element);
            }
        }

        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing name
                XElement element = Util.BuildDDMSElement(Unknown.GetName(version), null);
                GetInstance("At least 1 name element must exist.", element);

                // Empty name
                element = Util.BuildDDMSElement(Unknown.GetName(version), null);
                element.Add(Util.BuildDDMSElement("name", ""));
                GetInstance("At least 1 name element must have a non-empty value.", element);
            }
        }

        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing name
                GetInstance("At least 1 name element must exist.", null, TEST_PHONES, TEST_EMAILS);

                // Empty name
                List<string> names = new List<string>();
                names.Add("");
                GetInstance("At least 1 name element must have a non-empty value.", names, TEST_PHONES, TEST_EMAILS);
            }
        }

        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                Unknown component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Unknown dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Unknown dataComponent = GetInstance(SUCCESS, TEST_NAMES, null, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new Unknown(TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The Unknown element cannot be used");
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Unknown.Builder builder = new Unknown.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown.Builder builder = new Unknown.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Names = TEST_NAMES;
                Assert.IsFalse(builder.Empty);

            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Unknown.Builder builder = new Unknown.Builder();
                builder.Phones = Util.GetXsListAsList("703-885-1000");
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

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Unknown.Builder builder = new Unknown.Builder();
                Assert.IsNotNull(builder.Names[0]);
                Assert.IsNotNull(builder.Phones[0]);
                Assert.IsNotNull(builder.Emails[0]);
            }
        }
    }

}