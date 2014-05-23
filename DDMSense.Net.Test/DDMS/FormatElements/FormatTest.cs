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

namespace DDMSense.Test.DDMS.FormatElements
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.FormatElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:format elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class FormatTest : AbstractBaseTestCase
    {
        private const string TEST_MIME_TYPE = "text/xml";
        private const string TEST_MEDIUM = "digital";

        /// <summary>
        /// Constructor
        /// </summary>
        public FormatTest()
            : base("format.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Format Fixture
        {
            get
            {
                try
                {
                    return (new Format("text/xml", null, null));
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
        private Format GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Format component = null;
            try
            {
                component = new Format(element);
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
        /// <param name="mimeType"> the mimeType element (required) </param>
        /// <param name="extent"> the extent element (may be null) </param>
        /// <param name="medium"> the medium element (may be null) </param>
        /// <returns> a valid object </returns>
        private Format GetInstance(string message, string mimeType, Extent extent, string medium)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Format component = null;
            try
            {
                component = new Format(mimeType, extent, medium);
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
        /// Helper method to manage the deprecated Media wrapper element
        /// </summary>
        /// <param name="innerElement"> the element containing the guts of this component </param>
        /// <returns> the element itself in DDMS 4.0.1 or later, or the element wrapped in another element </returns>
        private XElement WrapInnerElement(XElement innerElement)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string name = Format.GetName(version);
            if (version.IsAtLeast("4.0.1"))
            {
                innerElement.Name = XName.Get(name, innerElement.Name.NamespaceName);
                return (innerElement);
            }
            XElement element = Util.BuildDDMSElement(name, null);
            element.Add(innerElement);
            return (element);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string prefix = version.IsAtLeast("4.0.1") ? "format." : "format.Media.";
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, prefix + "mimeType", TEST_MIME_TYPE));
            text.Append(ExtentTest.Fixture.GetOutput(isHTML, prefix, ""));
            text.Append(BuildOutput(isHTML, prefix + "medium", TEST_MEDIUM));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:format ").Append(XmlnsDDMS).Append(">");
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
            {
                xml.Append("<ddms:mimeType>text/xml</ddms:mimeType>");
                xml.Append("<ddms:extent ddms:qualifier=\"sizeBytes\" ddms:value=\"75000\" />");
                xml.Append("<ddms:medium>digital</ddms:medium>");
            }
            else
            {
                xml.Append("<ddms:Media>");
                xml.Append("<ddms:mimeType>text/xml</ddms:mimeType>");
                xml.Append("<ddms:extent ddms:qualifier=\"sizeBytes\" ddms:value=\"75000\" />");
                xml.Append("<ddms:medium>digital</ddms:medium>");
                xml.Append("</ddms:Media>");
            }
            xml.Append("</ddms:format>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void FormatElements_Format_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Format.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement mediaElement = Util.BuildDDMSElement("Media", null);
                Util.AddDDMSChildElement(mediaElement, "mimeType", "text/html");
                GetInstance(SUCCESS, WrapInnerElement(mediaElement));
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);

                // No optional fields
                GetInstance(SUCCESS, TEST_MIME_TYPE, null, null);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string extentName = Extent.GetName(version);

                // Missing mimeType
                XElement mediaElement = Util.BuildDDMSElement("Media", null);
                GetInstance("mimeType is required.", WrapInnerElement(mediaElement));

                // Empty mimeType
                mediaElement = Util.BuildDDMSElement("Media", null);
                mediaElement.Add(Util.BuildDDMSElement("mimeType", ""));
                GetInstance("mimeType is required.", WrapInnerElement(mediaElement));

                // Too many mimeTypes
                mediaElement = Util.BuildDDMSElement("Media", null);
                mediaElement.Add(Util.BuildDDMSElement("mimeType", TEST_MIME_TYPE));
                mediaElement.Add(Util.BuildDDMSElement("mimeType", TEST_MIME_TYPE));
                GetInstance("Exactly 1 mimeType element must exist.", WrapInnerElement(mediaElement));

                // Too many extents
                mediaElement = Util.BuildDDMSElement("Media", null);
                mediaElement.Add(Util.BuildDDMSElement("mimeType", TEST_MIME_TYPE));
                mediaElement.Add(Util.BuildDDMSElement(extentName, null));
                mediaElement.Add(Util.BuildDDMSElement(extentName, null));
                GetInstance("No more than 1 extent element can exist.", WrapInnerElement(mediaElement));

                // Too many mediums
                mediaElement = Util.BuildDDMSElement("Media", null);
                mediaElement.Add(Util.BuildDDMSElement("mimeType", TEST_MIME_TYPE));
                mediaElement.Add(Util.BuildDDMSElement("medium", TEST_MEDIUM));
                mediaElement.Add(Util.BuildDDMSElement("medium", TEST_MEDIUM));
                GetInstance("No more than 1 medium element can exist.", WrapInnerElement(mediaElement));

                // Invalid Extent
                XElement extentElement = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(extentElement, "value", "test");
                mediaElement = Util.BuildDDMSElement("Media", null);
                Util.AddDDMSChildElement(mediaElement, "mimeType", "text/html");
                mediaElement.Add(extentElement);
                GetInstance("qualifier attribute is required.", WrapInnerElement(mediaElement));
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing mimeType
                GetInstance("mimeType is required.", null, ExtentTest.Fixture, TEST_MEDIUM);

                // Empty mimeType
                GetInstance("mimeType is required.", "", ExtentTest.Fixture, TEST_MEDIUM);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Medium element with no value or empty value
                XElement mediaElement = Util.BuildDDMSElement("Media", null);
                mediaElement.Add(Util.BuildDDMSElement("mimeType", TEST_MIME_TYPE));
                mediaElement.Add(Util.BuildDDMSElement("medium", null));
                component = GetInstance(SUCCESS, WrapInnerElement(mediaElement));
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A ddms:medium element was found with no value.";
                string locator = version.IsAtLeast("4.0.1") ? "ddms:format" : "ddms:format/ddms:Media";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Nested warnings
                component = GetInstance(SUCCESS, TEST_MIME_TYPE, new Extent("sizeBytes", ""), TEST_MEDIUM);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                text = "A qualifier has been set without an accompanying value attribute.";
                locator = (version.IsAtLeast("4.0.1")) ? "ddms:format/ddms:extent" : "ddms:format/ddms:Media/ddms:extent";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Format dataComponent = GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Format elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Format dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, ExtentTest.Fixture, TEST_MEDIUM);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_MIME_TYPE, null, TEST_MEDIUM);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, "TEST_MIME_TYPE", ExtentTest.Fixture, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML(SaveOptions.DisableFormatting));

                component = GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML(SaveOptions.DisableFormatting));
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_ExtentReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Extent extent = ExtentTest.Fixture;
                GetInstance(SUCCESS, TEST_MIME_TYPE, extent, TEST_MEDIUM);
                GetInstance(SUCCESS, TEST_MIME_TYPE, extent, TEST_MEDIUM);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_GetExtentQualifier()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(component.ExtentQualifier, component.Extent.Qualifier);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_GetExtentQualifierNoExtent()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format component = GetInstance(SUCCESS, TEST_MIME_TYPE, null, null);
                Assert.AreEqual("", component.ExtentQualifier);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_GetExtentValue()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(component.ExtentValue, component.Extent.Value);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_GetExtentValueNoExtent()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Format component = GetInstance(SUCCESS, TEST_MIME_TYPE, null, null);
                Assert.AreEqual("", component.ExtentValue);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Format.Builder builder = new Format.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Format.Builder builder = new Format.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.MimeType = TEST_MIME_TYPE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void FormatElements_Format_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Format.Builder builder = new Format.Builder();
                builder.Medium = TEST_MEDIUM;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "mimeType is required.");
                }
                builder.MimeType = TEST_MIME_TYPE;
                builder.Commit();

                // No extent vs. empty extent
                builder = new Format.Builder();
                builder.MimeType = TEST_MIME_TYPE;
                builder.Medium = TEST_MEDIUM;
                Assert.IsNotNull(builder.Extent);
                Assert.IsNull(((Format)builder.Commit()).Extent);
                builder.Extent.Qualifier = "sizeBytes";
                builder.Extent.Value = "75000";
                builder.Commit();
                Assert.IsNotNull(builder.Extent);
            }
        }
    }
}