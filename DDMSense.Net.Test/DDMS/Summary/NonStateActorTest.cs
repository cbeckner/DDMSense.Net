using System;
using System.Collections.Generic;
using System.Text;
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
namespace DDMSense.Test.DDMS.Summary
{



    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:nonStateActor elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class NonStateActorTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "Laotian Monks";
        private static readonly int? TEST_ORDER = Convert.ToInt32(1);
        private const string TEST_QUALIFIER = "urn:sample";

        /// <summary>
        /// Constructor
        /// </summary>
        public NonStateActorTest()
            : base("nonStateActor.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="a"> fixed order value </param>
        public static NonStateActor GetFixture(int order)
        {
            try
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                return (version.IsAtLeast("4.0.1") ? new NonStateActor(TEST_VALUE, Convert.ToInt32(order), Qualifier, SecurityAttributesTest.Fixture) : null);
            }
            catch (InvalidDDMSException e)
            {
                Assert.Fail("Could not create fixture: " + e.Message);
            }
            return (null);
        }

        /// <summary>
        /// Returns a dummy value for the qualifier, based upon the current DDMS version.
        /// </summary>
        private static string Qualifier
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.1") ? TEST_QUALIFIER : null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<NonStateActor> FixtureList
        {
            get
            {
                try
                {
                    DDMSVersion version = DDMSVersion.CurrentVersion;
                    List<NonStateActor> actors = new List<NonStateActor>();
                    if (version.IsAtLeast("4.0.1"))
                    {
                        actors.Add(new NonStateActor(TEST_VALUE, TEST_ORDER, Qualifier, SecurityAttributesTest.Fixture));
                    }
                    return (actors);
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
        private NonStateActor GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NonStateActor component = null;
            try
            {
                component = new NonStateActor(element);
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
        /// <param name="value"> the value of the actor (optional) </param>
        /// <param name="order"> the order of the actor (optional) </param>
        /// <param name="qualifier"> the qualifier of the actor (optional) </param>
        /// <returns> a valid object </returns>
        private NonStateActor GetInstance(string message, string value, int? order, string qualifier)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NonStateActor component = null;
            try
            {
                component = new NonStateActor(value, order, qualifier, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "nonStateActor.value", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "nonStateActor.order", Convert.ToString(TEST_ORDER)));
            if (version.IsAtLeast("4.1"))
            {
                text.Append(BuildOutput(isHTML, "nonStateActor.qualifier", TEST_QUALIFIER));
            }
            text.Append(BuildOutput(isHTML, "nonStateActor.classification", "U"));
            text.Append(BuildOutput(isHTML, "nonStateActor.ownerProducer", "USA"));
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
                xml.Append("<ddms:nonStateActor ").Append(XmlnsDDMS).Append(" ");
                xml.Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
                xml.Append("ddms:order=\"").Append(TEST_ORDER).Append("\"");
                if (version.IsAtLeast("4.1"))
                {
                    xml.Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\"");
                }
                xml.Append(">").Append(TEST_VALUE).Append("</ddms:nonStateActor>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, NonStateActor.GetName(version));
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
                XElement element = Util.BuildDDMSElement(NonStateActor.GetName(version), null);
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
                GetInstance(SUCCESS, TEST_VALUE, TEST_ORDER, Qualifier);

                // No optional fields
                GetInstance(SUCCESS, null, null, null);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Bad qualifier
                if (version.IsAtLeast("4.1"))
                {
                    XElement element = Util.BuildDDMSElement(NonStateActor.GetName(version), null);
                    Util.AddDDMSAttribute(element, "qualifier", INVALID_URI);
                    GetInstance("Invalid URI", element);
                }
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Bad qualifier
                if (version.IsAtLeast("4.1"))
                {
                    GetInstance("Invalid URI", TEST_VALUE, TEST_ORDER, INVALID_URI);
                }
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                string text, locator;
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                NonStateActor component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // 4.1 ddms:qualifier element used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.Equals(1, component.ValidationWarnings.Count());
                    text = "The ddms:qualifier attribute in this DDMS component";
                    locator = "ddms:nonStateActor";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                // No warnings 
                else
                {
                    Assert.Equals(0, component.ValidationWarnings.Count());
                }

                // Empty value
                XElement element = Util.BuildDDMSElement(NonStateActor.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count());
                text = "A ddms:nonStateActor element was found with no value.";
                locator = "ddms:nonStateActor";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void TestDeprecatedConstructor()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NonStateActor dataComponent = new NonStateActor(TEST_VALUE, TEST_ORDER, SecurityAttributesTest.Fixture);
                Assert.IsTrue(String.IsNullOrEmpty(dataComponent.Qualifier));
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NonStateActor elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                NonStateActor dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ORDER, Qualifier);

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

                NonStateActor elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                NonStateActor dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ORDER, Qualifier);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, null, Qualifier);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                if (version.IsAtLeast("4.1"))
                {
                    dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ORDER, DIFFERENT_VALUE);
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

                NonStateActor component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_ORDER, Qualifier);
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

                NonStateActor component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_ORDER, Qualifier);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new NonStateActor(TEST_VALUE, TEST_ORDER, null, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The nonStateActor element cannot be used");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NonStateActor component = GetInstance(SUCCESS, GetValidElement(sVersion));
                NonStateActor.Builder builder = new NonStateActor.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NonStateActor.Builder builder = new NonStateActor.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Order = TEST_ORDER;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // There are no invalid cases right now -- every field is optional.
            }
        }
    }

}