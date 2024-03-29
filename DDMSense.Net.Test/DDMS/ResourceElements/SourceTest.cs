using System;
using System.Linq;
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
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:source elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class SourceTest : AbstractBaseTestCase
    {
        private const string TEST_QUALIFIER = "URL";
        private const string TEST_VALUE = "http://www.xmethods.com";
        private const string TEST_SCHEMA_QUALIFIER = "WSDL";
        private const string TEST_SCHEMA_HREF = "http://www.xmethods.com/sd/2001/TemperatureService?wsdl";

        /// <summary>
        /// Constructor
        /// </summary>
        public SourceTest()
            : base("source.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Source Fixture
        {
            get
            {
                try
                {
                    return (new Source(null, "http://www.xmethods.com", null, null, null));
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
        private Source GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Source component = null;
            try
            {
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    SecurityAttributesTest.Fixture.AddTo(element);
                }
                component = new Source(element);
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
        /// <param name="schemaQualifier"> the value of the schemaQualifier attribute </param>
        /// <param name="schemaHref"> the value of the schemaHref attribute </param>
        /// <returns> a valid object </returns>
        private Source GetInstance(string message, string qualifier, string value, string schemaQualifier, string schemaHref)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Source component = null;
            try
            {
                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                component = new Source(qualifier, value, schemaQualifier, schemaHref, attr);
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
            text.Append(BuildOutput(isHTML, "source.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "source.value", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "source.schemaQualifier", TEST_SCHEMA_QUALIFIER));
            text.Append(BuildOutput(isHTML, "source.schemaHref", TEST_SCHEMA_HREF));
            if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
            {
                text.Append(BuildOutput(isHTML, "source.classification", "U"));
                text.Append(BuildOutput(isHTML, "source.ownerProducer", "USA"));
            }
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
                xml.Append("<ddms:source ").Append(XmlnsDDMS).Append(" ");
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    xml.Append(XmlnsISM).Append(" ");
                }
                xml.Append("ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" ");
                xml.Append("ddms:schemaQualifier=\"").Append(TEST_SCHEMA_QUALIFIER).Append("\" ");
                xml.Append("ddms:schemaHref=\"").Append(TEST_SCHEMA_HREF).Append("\" ");
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
                }
                xml.Append("/>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Source.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Source.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);

                // No optional fields
                GetInstance(SUCCESS, "", "", "", "");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Href not URI
                XElement element = Util.BuildDDMSElement(Source.GetName(version), null);
                Util.AddDDMSAttribute(element, "schemaHref", INVALID_URI);
                GetInstance("Invalid URI", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_DataConstructorInvalidHrefNotURI()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Href not URI
                GetInstance("Invalid URI", TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, INVALID_URI);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                XElement element = Util.BuildDDMSElement(Source.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A completely empty ddms:source element was found.";
                string locator = "ddms:source";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Source elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Source dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Source elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Source dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, DIFFERENT_VALUE, TEST_SCHEMA_HREF);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_SecurityAttributes()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes attr = (!version.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                Source component = new Source(TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF, attr);
                if (!version.IsAtLeast("3.0"))
                {
                    Assert.IsTrue(component.SecurityAttributes.Empty);
                }
                else
                {
                    Assert.AreEqual(attr, component.SecurityAttributes);
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new Source(TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Source.Builder builder = new Source.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Source.Builder builder = new Source.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Source_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Source.Builder builder = new Source.Builder();
                builder.SecurityAttributes.Classification = "SuperSecret";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "SuperSecret is not a valid enumeration token");
                }
                builder.SecurityAttributes.Classification = "";
                builder.Qualifier = TEST_QUALIFIER;
                builder.Qualifier = TEST_VALUE;
                builder.Commit();
            }
        }
    }
}