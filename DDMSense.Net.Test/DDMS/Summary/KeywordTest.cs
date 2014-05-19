using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    using ExtensibleAttributes = DDMSense.DDMS.Extensible.ExtensibleAttributes;
    using ExtensibleAttributesTest = DDMSense.Test.DDMS.Extensible.ExtensibleAttributesTest;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using System;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to ddms:keyword elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class KeywordTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "Tornado";

        /// <summary>
        /// Constructor
        /// </summary>
        public KeywordTest()
            : base("keyword.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<Keyword> FixtureList
        {
            get
            {
                try
                {
                    List<Keyword> keywords = new List<Keyword>();
                    keywords.Add(new Keyword("DDMSence", null));
                    keywords.Add(new Keyword("Uri", null));
                    return (keywords);
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
        private Keyword GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Keyword component = null;
            try
            {
                component = new Keyword(element);
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
        /// <param name="value"> the value child text </param>
        /// <returns> a valid object </returns>
        private Keyword GetInstance(string message, string value)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            DDMSVersion version = DDMSVersion.CurrentVersion;
            Keyword component = null;
            try
            {
                component = new Keyword(value, version.IsAtLeast("4.0.1") ? SecurityAttributesTest.Fixture : null);
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
            text.Append(BuildOutput(isHTML, "keyword", TEST_VALUE));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "keyword.classification", "U"));
                text.Append(BuildOutput(isHTML, "keyword.ownerProducer", "USA"));
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
                xml.Append("<ddms:keyword ").Append(XmlnsDDMS).Append(" ");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(XmlnsISM).Append(" ");
                }
                xml.Append("ddms:value=\"").Append(TEST_VALUE).Append("\"");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
                }
                xml.Append(" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Keyword.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing value
                XElement element = Util.BuildDDMSElement(Keyword.GetName(version), null);
                GetInstance("value attribute is required.", element);

                // Empty value
                element = Util.BuildDDMSElement(Keyword.GetName(version), "");
                GetInstance("value attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing value
                GetInstance("value attribute is required.", (string)null);

                // Empty value
                GetInstance("value attribute is required.", "");
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Keyword elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Keyword dataComponent = GetInstance(SUCCESS, TEST_VALUE);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Keyword elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Keyword dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Keyword elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());
            }
        }


        public virtual void Summary_Keyword_ExtensibleSuccess()
        {
            // Extensible attribute added
            DDMSVersion.SetCurrentVersion("3.0");
            ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
            new Keyword("xml", null, attr);
        }

        [TestMethod]
        public virtual void Summary_Keyword_ExtensibleAssert_Failure()
        {
            // Wrong DDMS Version
            DDMSVersion.SetCurrentVersion("2.0");
            ExtensibleAttributes attributes = ExtensibleAttributesTest.Fixture;
            try
            {
                new Keyword(TEST_VALUE, null, attributes);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "xs:anyAttribute cannot be applied");
            }

            DDMSVersion version = DDMSVersion.SetCurrentVersion("3.0");
            XAttribute attr = new XAttribute(XName.Get("ddms:value", version.Namespace), "dog");

            // Using ddms:value as the extension (data)
            List<XAttribute> extAttributes = new List<XAttribute>();
            extAttributes.Add(attr);
            attributes = new ExtensibleAttributes(extAttributes);
            try
            {
                new Keyword(TEST_VALUE, null, attributes);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The extensible attribute with the name, ddms:value");
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("3.1");
            try
            {
                new Keyword(TEST_VALUE, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_Keyword_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Keyword.Builder builder = new Keyword.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        //TODO - Figure out how to run this test
        [TestMethod]
        public virtual void Summary_Keyword_BuilderIsEmpty()
        {
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    Keyword.Builder builder = new Keyword.Builder();
            //    Assert.IsNull(builder.Commit());
            //    Assert.IsTrue(builder.Empty);
            //    builder.Value = TEST_VALUE;
            //    Assert.IsFalse(builder.Empty);
            //}
        }


        public virtual void Summary_Keyword_BuilderValidation()
        {
            Assert.Inconclusive("No invalid cases that span all versions of DDMS. The only field is \"value\" and if that is missing, the builder is empty");
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
            }
        }
    }

}