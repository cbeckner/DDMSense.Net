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

namespace DDMSense.Test.DDMS.Summary
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:virtualCoverage elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class VirtualCoverageTest : AbstractBaseTestCase
    {
        private const string TEST_ADDRESS = "123.456.789.0";
        private const string TEST_PROTOCOL = "IP";

        /// <summary>
        /// Constructor
        /// </summary>
        public VirtualCoverageTest()
            : base("virtualCoverage.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static VirtualCoverage Fixture
        {
            get
            {
                try
                {
                    return (new VirtualCoverage("123.456.789.0", "IP", null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private VirtualCoverage GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            VirtualCoverage component = null;
            try
            {
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    SecurityAttributesTest.Fixture.AddTo(element);
                }
                component = new VirtualCoverage(element);
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
        /// <param name="address"> the virtual address (optional) </param>
        /// <param name="protocol"> the network protocol (optional, should be used if address is provided) </param>
        /// <returns> a valid object </returns>
        private VirtualCoverage GetInstance(string message, string address, string protocol)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            VirtualCoverage component = null;
            try
            {
                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.IsAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
                component = new VirtualCoverage(address, protocol, attr);
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
            text.Append(BuildOutput(isHTML, "virtualCoverage.address", TEST_ADDRESS));
            text.Append(BuildOutput(isHTML, "virtualCoverage.protocol", TEST_PROTOCOL));
            if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
            {
                text.Append(BuildOutput(isHTML, "virtualCoverage.classification", "U"));
                text.Append(BuildOutput(isHTML, "virtualCoverage.ownerProducer", "USA"));
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
                xml.Append("<ddms:virtualCoverage ").Append(XmlnsDDMS);
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    xml.Append(" ").Append(XmlnsISM);
                }
                xml.Append(" ddms:address=\"").Append(TEST_ADDRESS).Append("\" ddms:protocol=\"").Append(TEST_PROTOCOL).Append("\"");
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
                }
                xml.Append(" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, VirtualCoverage.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(VirtualCoverage.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);

                // No optional fields
                GetInstance(SUCCESS, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // address without protocol
                XElement element = Util.BuildDDMSElement(VirtualCoverage.GetName(version), null);
                Util.AddDDMSAttribute(element, "address", TEST_ADDRESS);
                GetInstance("protocol is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // address without protocol
                GetInstance("protocol is required.", TEST_ADDRESS, null);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Empty element
                XElement element = Util.BuildDDMSElement(VirtualCoverage.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A completely empty ddms:virtualCoverage element was found.";
                string locator = "ddms:virtualCoverage";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VirtualCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                VirtualCoverage dataComponent = GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VirtualCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                VirtualCoverage dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_PROTOCOL);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_ADDRESS, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VirtualCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_SecurityAttributes()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes attr = (!version.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                VirtualCoverage component = new VirtualCoverage(TEST_ADDRESS, TEST_PROTOCOL, attr);
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
        public virtual void Summary_VirtualCoverage_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new VirtualCoverage(TEST_ADDRESS, TEST_PROTOCOL, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                VirtualCoverage.Builder builder = new VirtualCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                VirtualCoverage.Builder builder = new VirtualCoverage.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Address = TEST_ADDRESS;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_VirtualCoverage_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                VirtualCoverage.Builder builder = new VirtualCoverage.Builder();
                builder.Address = TEST_ADDRESS;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "protocol is required.");
                }
                builder.Protocol = TEST_PROTOCOL;
                builder.Commit();
            }
        }
    }
}