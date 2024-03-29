using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

namespace DDMSense.Test.DDMS.Summary
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.Extensible;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary;
    using Microsoft.XmlDiffPatch;
    using System;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using ExtensibleAttributes = DDMSense.DDMS.Extensible.ExtensibleAttributes;
    using ExtensibleAttributesTest = DDMSense.Test.DDMS.Extensible.ExtensibleAttributesTest;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:category elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class CategoryTest : AbstractBaseTestCase
    {
        private const string TEST_QUALIFIER = "http://metadata.dod.mil/mdr/artifiact/MET/severeWeatherCode_enum/xml";
        private const string TEST_CODE = "T";
        private const string TEST_LABEL = "TORNADO";

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryTest()
            : base("category.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<Category> FixtureList
        {
            get
            {
                try
                {
                    List<Category> categories = new List<Category>();
                    categories.Add(new Category("urn:buri:ddmsence:categories", "DDMS", "DDMS", null));
                    return (categories);
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
        private Category GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Category component = null;
            try
            {
                component = new Category(element);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="qualifier"> the qualifier (optional) </param>
        /// <param name="code"> the code (optional) </param>
        /// <param name="label"> the label (required) </param>
        /// <returns> a valid object </returns>
        private Category GetInstance(string message, string qualifier, string code, string label)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            DDMSVersion version = DDMSVersion.CurrentVersion;
            Category component = null;
            try
            {
                component = new Category(qualifier, code, label, version.IsAtLeast("4.0.1") ? SecurityAttributesTest.Fixture : null);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectAssert, e);
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
            text.Append(BuildOutput(isHTML, "category.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "category.code", TEST_CODE));
            text.Append(BuildOutput(isHTML, "category.label", TEST_LABEL));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "category.classification", "U"));
                text.Append(BuildOutput(isHTML, "category.ownerProducer", "USA"));
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
                xml.Append("<ddms:category ").Append(XmlnsDDMS).Append(" ");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(XmlnsISM).Append(" ");
                }
                xml.Append("ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ");
                xml.Append("ddms:code=\"").Append(TEST_CODE).Append("\" ");
                xml.Append("ddms:label=\"").Append(TEST_LABEL).Append("\"");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
                }
                xml.Append(" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_Category_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Category.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_Category_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Category.GetName(version), null);
                Util.AddDDMSAttribute(element, "label", TEST_LABEL);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_Category_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);

                // No optional fields
                GetInstance(SUCCESS, "", "", TEST_LABEL);
            }
        }

        [TestMethod]
        public virtual void Summary_Category_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing label
                XElement element = Util.BuildDDMSElement(Category.GetName(version), null);
                GetInstance("label attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_Category_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing label
                GetInstance("label attribute is required.", TEST_QUALIFIER, TEST_CODE, null);

                // Qualifier not URI
                GetInstance("Invalid URI", INVALID_URI, TEST_CODE, TEST_LABEL);
            }
        }

        [TestMethod]
        public virtual void Summary_Category_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_Category_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Category elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Category dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_Category_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Category elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Category dataComponent = GetInstance(SUCCESS, "", TEST_CODE, TEST_LABEL);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, "", TEST_LABEL);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Category_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Category elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Category_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_Category_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void Summary_Category_ExtensibleSuccess()
        {
            // Extensible attribute added
            DDMSVersion.SetCurrentVersion("3.0");
            ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
            new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attr);
        }

        [TestMethod]
        public virtual void Summary_Category_ExtensibleAssert_Failure()
        {
            // Wrong DDMS Version
            DDMSVersion.SetCurrentVersion("2.0");
            ExtensibleAttributes attributes = ExtensibleAttributesTest.Fixture;
            try
            {
                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "xs:anyAttribute cannot be applied");
            }

            DDMSVersion version = DDMSVersion.SetCurrentVersion("3.0");

            // Using ddms:qualifier as the extension (data)
            var element = new XElement("testElement", string.Empty);
            element.Add(new XAttribute(XNamespace.Xmlns + "ddms", version.Namespace));
            element.Add(new XAttribute(XName.Get("qualifier", version.Namespace), "dog"));

            List<XAttribute> extAttributes = new List<XAttribute>();
            extAttributes.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(version.Namespace) + "qualifier").FirstOrDefault());

            //extAttributes.Add(new XAttribute(XName.Get("qualifier", version.Namespace), "dog"));
            attributes = new ExtensibleAttributes(extAttributes);
            try
            {
                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The extensible XAttribute with the name, ddms:qualifier");
            }

            // Using ddms:code as the extension (data)
            element.Add(new XAttribute(XName.Get("code", version.Namespace), "dog"));
            extAttributes = new List<XAttribute>(); 
            extAttributes.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(version.Namespace) + "code").FirstOrDefault());
            attributes = new ExtensibleAttributes(extAttributes);
            try
            {
                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The extensible XAttribute with the name, ddms:code");
            }

            // Using ddms:label as the extension (data)
            element.Add(new XAttribute(XName.Get("label", version.Namespace), "dog"));
            extAttributes = new List<XAttribute>();
            extAttributes.Add(new XAttribute(XName.Get("label", version.Namespace), "dog"));
            attributes = new ExtensibleAttributes(extAttributes);
            try
            {
                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The extensible XAttribute with the name, ddms:label");
            }

            // Using icism:classification as the extension (data)
            element.Add(new XAttribute(XNamespace.Xmlns + "icism", version.IsmNamespace));

            extAttributes = new List<XAttribute>();
            extAttributes.Add(new XAttribute(XName.Get("classification", version.IsmNamespace), "dog"));
            attributes = new ExtensibleAttributes(extAttributes);
            try
            {
                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
            }
            catch (InvalidDDMSException)
            {
                Assert.Fail("Prevented valid data.");
            }
        }

        [TestMethod]
        public virtual void Summary_Category_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("3.1");
            try
            {
                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_Category_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Category.Builder builder = new Category.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        //TODO - Figure out how to implement this test
        [TestMethod]
        public virtual void Summary_Category_BuilderIsEmpty()
        {
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    Category.Builder builder = new Category.Builder();
            //    Assert.IsNull(builder.Commit());
            //    Assert.IsTrue(builder.Empty);
            //    builder.Label = TEST_LABEL;
            //    Assert.IsFalse(builder.Empty);

            //}
        }

        //TODO - Figure out how to implement this test
        [TestMethod]
        public virtual void Summary_Category_BuilderValidation()
        {
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    Category.Builder builder = new Category.Builder();
            //    builder.Qualifier = TEST_QUALIFIER;
            //    try
            //    {
            //        builder.Commit();
            //        Assert.Fail("Builder allowed invalid data.");
            //    }
            //    catch (InvalidDDMSException e)
            //    {
            //        ExpectMessage(e, "label attribute is required.");
            //    }
            //    builder.Label = TEST_LABEL;
            //    builder.Commit();
            //}
        }
    }
}