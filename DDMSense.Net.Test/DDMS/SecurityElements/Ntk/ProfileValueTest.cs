using System.Collections.Generic;
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

namespace DDMSense.Test.DDMS.SecurityElements.Ntk
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.SecurityElements.Ntk;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ntk:AccessProfileValue elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class ProfileValueTest : AbstractBaseTestCase
    {
        private const string TEST_VALUE = "profile";
        private const string TEST_VOCABULARY = "vocabulary";
        private new const string TEST_ID = "ID";
        private const string TEST_ID_REFERENCE = "ID";
        private const string TEST_QUALIFIER = "qualifier";

        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileValueTest()
            : base("accessProfileValue.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="value"> the value for this component </param>
        public static ProfileValue GetFixture(string value)
        {
            try
            {
                return (new ProfileValue(value, TEST_VOCABULARY, null, null, null, SecurityAttributesTest.Fixture));
            }
            catch (InvalidDDMSException e)
            {
                Assert.Fail("Could not create fixture: " + e.Message);
            }
            return (null);
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<ProfileValue> FixtureList
        {
            get
            {
                List<ProfileValue> list = new List<ProfileValue>();
                list.Add(ProfileValueTest.GetFixture("profile"));
                return (list);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private ProfileValue GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProfileValue component = null;
            try
            {
                component = new ProfileValue(element);
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
        /// <param name="value"> the value of the element's child text </param>
        /// <param name="vocabulary"> the vocabulary (required) </param>
        /// <param name="id"> the NTK ID (optional) </param>
        /// <param name="idReference"> a reference to an NTK ID (optional) </param>
        /// <param name="qualifier"> an NTK qualifier (optional) </param>
        /// <returns> a valid object </returns>
        private ProfileValue GetInstance(string message, string value, string vocabulary, string id, string idReference, string qualifier)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProfileValue component = null;
            try
            {
                component = new ProfileValue(value, vocabulary, id, idReference, qualifier, SecurityAttributesTest.Fixture);
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
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "profileValue", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "profileValue.vocabulary", TEST_VOCABULARY));
            text.Append(BuildOutput(isHTML, "profileValue.id", TEST_ID));
            text.Append(BuildOutput(isHTML, "profileValue.idReference", TEST_ID_REFERENCE));
            text.Append(BuildOutput(isHTML, "profileValue.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "profileValue.classification", "U"));
            text.Append(BuildOutput(isHTML, "profileValue.ownerProducer", "USA"));
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
                xml.Append("<ntk:AccessProfileValue ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM);
                xml.Append(" ntk:id=\"ID\" ntk:IDReference=\"ID\" ntk:qualifier=\"qualifier\"");
                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ntk:vocabulary=\"vocabulary\">");
                xml.Append(TEST_VALUE).Append("</ntk:AccessProfileValue>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, ProfileValue.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildElement(ntkPrefix, ProfileValue.GetName(version), version.NtkNamespace, TEST_VALUE);
                Util.AddAttribute(element, ntkPrefix, "vocabulary", version.NtkNamespace, TEST_VOCABULARY);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);

                // No optional fields
                GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, null, null, null);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // Missing vocabulary
                XElement element = Util.BuildElement(ntkPrefix, ProfileValue.GetName(version), version.NtkNamespace, TEST_VALUE);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("Invalid NmToken value ''.", element);

                // Missing security attributes
                element = Util.BuildElement(ntkPrefix, ProfileValue.GetName(version), version.NtkNamespace, TEST_VALUE);
                Util.AddAttribute(element, ntkPrefix, "vocabulary", version.NtkNamespace, TEST_VOCABULARY);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing vocabulary
                GetInstance("Invalid NmToken value ''.", TEST_VALUE, null, null, null, null);
                // Missing security attributes
                try
                {
                    new ProfileValue(TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // No value
                component = GetInstance(SUCCESS, null, TEST_VOCABULARY, null, null, null);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A ntk:AccessProfileValue element was found with no value.";
                string locator = "ntk:AccessProfileValue";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProfileValue dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProfileValue dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, DIFFERENT_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, DIFFERENT_VALUE, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, DIFFERENT_VALUE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        public virtual void SecurityElements_Ntk_ProfileValue_WrongVersion()
        {
            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProfileValue.Builder builder = new ProfileValue.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue.Builder builder = new ProfileValue.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileValue_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileValue.Builder builder = new ProfileValue.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "Invalid NmToken value ''.");
                }
                catch (Exception ex)
                {
                    ExpectMessage(ex, "Invalid NmToken value ''..");
                }
                builder.Vocabulary = "test";
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }
}