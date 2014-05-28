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
    /// <para> Tests related to ddms:language elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class LanguageTest : AbstractBaseTestCase
    {

        private const string TEST_QUALIFIER = "http://purl.org/dc/elements/1.1/language";
        private const string TEST_VALUE = "en";

        /// <summary>
        /// Constructor
        /// </summary>
        public LanguageTest()
            : base("language.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Language Fixture
        {
            get
            {
                try
                {
                    return (new Language("http://purl.org/dc/elements/1.1/language", "en"));
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
        private Language GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Language component = null;
            try
            {
                component = new Language(element);
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
        /// <param name="qualifier"> the qualifier value </param>
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private Language GetInstance(string message, string qualifier, string value)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Language component = null;
            try
            {
                component = new Language(qualifier, value);
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
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "language.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "language.value", TEST_VALUE));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:language ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Language.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Language.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);

                // No optional fields
                GetInstance(SUCCESS, "", "");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing qualifier
                XElement element = Util.BuildDDMSElement(Language.GetName(version), null);
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing qualifier
                GetInstance("qualifier attribute is required.", null, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Qualifier without value
                XElement element = Util.BuildDDMSElement(Language.GetName(version), null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A qualifier has been set without an accompanying value XAttribute.";
                string locator = "ddms:language";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Neither attribute
                element = Util.BuildDDMSElement(Language.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                text = "Neither a qualifier nor a value was set on this language.";
                locator = "ddms:language";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Language elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Language dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Language elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Language dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Language.Builder builder = new Language.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Language.Builder builder = new Language.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void ResourceElements_Language_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Language.Builder builder = new Language.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "qualifier attribute is required.");
                }
                builder.Qualifier = TEST_QUALIFIER;
                builder.Commit();
            }
        }
    }

}