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



    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to ddms:service elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class ServiceTest : AbstractBaseTestCase
    {

        private static readonly List<string> TEST_NAMES = new List<string>();
        private static readonly List<string> TEST_PHONES = new List<string>();
        private static readonly List<string> TEST_EMAILS = new List<string>();
        static ServiceTest()
        {
            TEST_NAMES.Add("https://metadata.dod.mil/ebxmlquery/soap");
            TEST_PHONES.Add("703-882-1000");
            TEST_EMAILS.Add("ddms@fgm.com");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceTest()
            : base("service.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Service Fixture
        {
            get
            {
                try
                {
                    return (new Service(Util.GetXsListAsList("https://metadata.dod.mil/ebxmlquery/soap"), null, null));
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
        private Service GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Service component = null;
            try
            {
                component = new Service(element);
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
        private Service GetInstance(string message, List<string> names, List<string> phones, List<string> emails)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Service component = null;
            try
            {
                component = new Service(names, phones, emails);
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
            text.Append(BuildOutput(isHTML, "entityType", Service.GetName(version)));
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
            xml.Append("<ddms:").Append(Service.GetName(version)).Append(" ").Append(XmlnsDDMS).Append(">\n");
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
            xml.Append("</ddms:").Append(Service.GetName(version)).Append(">");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void ResourceElements_Service_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Service.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Service.GetName(version), null);
                element.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);

                // No optional fields
                GetInstance(SUCCESS, TEST_NAMES, null, null);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing name
                XElement element = Util.BuildDDMSElement(Service.GetName(version), null);
                GetInstance("At least 1 name element must exist.", element);

                // Empty name
                element = Util.BuildDDMSElement(Service.GetName(version), null);
                element.Add(Util.BuildDDMSElement("name", ""));
                GetInstance("At least 1 name element must have a non-empty value.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_DataConstructorInvalid()
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

        [TestMethod]
        public virtual void ResourceElements_Service_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Service elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Service dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Service elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Service dataComponent = GetInstance(SUCCESS, TEST_NAMES, null, TEST_EMAILS);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_PHONES, TEST_EMAILS);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Service component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Service.Builder builder = new Service.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Service.Builder builder = new Service.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Names = TEST_NAMES;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Service_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Service.Builder builder = new Service.Builder();
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

       [TestMethod]
        public virtual void ResourceElements_Service_BuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Service.Builder builder = new Service.Builder();
                Assert.IsNotNull(builder.Names[1]);
                Assert.IsNotNull(builder.Phones[1]);
                Assert.IsNotNull(builder.Emails[1]);
            }
        }
    }

}