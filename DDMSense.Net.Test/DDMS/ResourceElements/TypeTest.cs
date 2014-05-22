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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:type elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class TypeTest : AbstractBaseTestCase
    {
        private const string TEST_DESCRIPTION = "Description";
        private const string TEST_QUALIFIER = "DCMITYPE";
        private const string TEST_VALUE = "http://purl.org/dc/dcmitype/Text";

        /// <summary>
        /// Constructor
        /// </summary>
        public TypeTest()
            : base("type.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static DDMSense.DDMS.ResourceElements.Type Fixture
        {
            get
            {
                try
                {
                    return (new DDMSense.DDMS.ResourceElements.Type(null, "DCMITYPE", "http://purl.org/dc/dcmitype/Text", null));
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
        private DDMSense.DDMS.ResourceElements.Type GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            DDMSense.DDMS.ResourceElements.Type component = null;
            try
            {
                component = new DDMSense.DDMS.ResourceElements.Type(element);
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
        /// <param name="description"> the description child text </param>
        /// <param name="qualifier"> the qualifier value </param>
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private DDMSense.DDMS.ResourceElements.Type GetInstance(string message, string description, string qualifier, string value)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            DDMSVersion version = DDMSVersion.CurrentVersion;
            DDMSense.DDMS.ResourceElements.Type component = null;
            try
            {
                component = new DDMSense.DDMS.ResourceElements.Type(description, qualifier, value, version.IsAtLeast("4.0.1") ? SecurityAttributesTest.Fixture : null);
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
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "type.description", TEST_DESCRIPTION));
            }
            text.Append(BuildOutput(isHTML, "type.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "type.value", TEST_VALUE));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "type.classification", "U"));
                text.Append(BuildOutput(isHTML, "type.ownerProducer", "USA"));
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
                DDMSVersion version = DDMSVersion.CurrentVersion;
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:type ").Append(XmlnsDDMS).Append(" ");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(XmlnsISM).Append(" ");
                }
                xml.Append("ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\"");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Description</ddms:type>");
                }
                else
                {
                    xml.Append(" />");
                }
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, DDMSense.DDMS.ResourceElements.Type.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(DDMSense.DDMS.ResourceElements.Type.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, version.IsAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);

                // No optional fields
                GetInstance(SUCCESS, "", "", "");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing qualifier
                XElement element = Util.BuildDDMSElement(DDMSense.DDMS.ResourceElements.Type.GetName(version), null);
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing qualifier
                GetInstance("qualifier attribute is required.", null, null, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                DDMSense.DDMS.ResourceElements.Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Qualifier without value
                XElement element = Util.BuildDDMSElement(DDMSense.DDMS.ResourceElements.Type.GetName(version), null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A qualifier has been set without an accompanying value attribute.";
                string locator = "ddms:type";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Neither attribute
                element = Util.BuildDDMSElement(DDMSense.DDMS.ResourceElements.Type.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                text = "Neither a qualifier nor a value was set on this type.";
                locator = "ddms:type";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                DDMSense.DDMS.ResourceElements.Type elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                DDMSense.DDMS.ResourceElements.Type dataComponent = GetInstance(SUCCESS, version.IsAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                DDMSense.DDMS.ResourceElements.Type elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                DDMSense.DDMS.ResourceElements.Type dataComponent = GetInstance(SUCCESS, version.IsAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
                if (version.IsAtLeast("4.0.1"))
                {
                    dataComponent = GetInstance(SUCCESS, "differentDescription", TEST_QUALIFIER, TEST_VALUE);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                DDMSense.DDMS.ResourceElements.Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, version.IsAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                DDMSense.DDMS.ResourceElements.Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, version.IsAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("3.1");
            try
            {
                new DDMSense.DDMS.ResourceElements.Type(null, TEST_QUALIFIER, TEST_VALUE, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_WrongVersionDescriptionAttributes()
        {
            DDMSVersion.SetCurrentVersion("3.1");
            try
            {
                new DDMSense.DDMS.ResourceElements.Type(TEST_DESCRIPTION, TEST_QUALIFIER, TEST_VALUE, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "This component cannot contain description");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                DDMSense.DDMS.ResourceElements.Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
                DDMSense.DDMS.ResourceElements.Type.Builder builder = new DDMSense.DDMS.ResourceElements.Type.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                DDMSense.DDMS.ResourceElements.Type.Builder builder = new DDMSense.DDMS.ResourceElements.Type.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Type_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                DDMSense.DDMS.ResourceElements.Type.Builder builder = new DDMSense.DDMS.ResourceElements.Type.Builder();
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