using System.Collections;
using System.Collections.Generic;

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
namespace DDMSense.Test.DDMS.SecurityElements.Ism
{



    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.SecurityElements;
    using DDMSense.DDMS.SecurityElements.Ism;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to the ISM attributes </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class SecurityAttributesTest : AbstractBaseTestCase
    {

        private const string TEST_CLASS = "U";
        private static readonly List<string> TEST_OWNERS = Util.GetXsListAsList("USA");

        private static readonly Dictionary<string, string> TEST_OTHERS_41 = new Dictionary<string, string>();
        static SecurityAttributesTest()
        {
            TEST_OTHERS_41[SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME] = "RD";
            TEST_OTHERS_41[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
            TEST_OTHERS_41[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
            TEST_OTHERS_41[SecurityAttributes.COMPILATION_REASON_NAME] = "NO";
            TEST_OTHERS_41[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
            TEST_OTHERS_41[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
            TEST_OTHERS_41[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
            TEST_OTHERS_41[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
            TEST_OTHERS_41[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
            TEST_OTHERS_41[SecurityAttributes.DISPLAY_ONLY_TO_NAME] = "AIA";
            TEST_OTHERS_41[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
            TEST_OTHERS_41[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
            TEST_OTHERS_41[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
            TEST_OTHERS_41[SecurityAttributes.NON_IC_MARKINGS_NAME] = "DS";
            TEST_OTHERS_41[SecurityAttributes.NON_US_CONTROLS_NAME] = "ATOMAL";
            TEST_OTHERS_41[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
            TEST_OTHERS_41[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
            TEST_OTHERS_41[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
            TEST_OTHERS_31[SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME] = "RD";
            TEST_OTHERS_31[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
            TEST_OTHERS_31[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
            TEST_OTHERS_31[SecurityAttributes.COMPILATION_REASON_NAME] = "NO";
            TEST_OTHERS_31[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
            TEST_OTHERS_31[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
            TEST_OTHERS_31[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
            TEST_OTHERS_31[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
            TEST_OTHERS_31[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
            TEST_OTHERS_31[SecurityAttributes.DISPLAY_ONLY_TO_NAME] = "AIA";
            TEST_OTHERS_31[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
            TEST_OTHERS_31[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
            TEST_OTHERS_31[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
            TEST_OTHERS_31[SecurityAttributes.NON_IC_MARKINGS_NAME] = "SINFO";
            TEST_OTHERS_31[SecurityAttributes.NON_US_CONTROLS_NAME] = "ATOMAL";
            TEST_OTHERS_31[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
            TEST_OTHERS_31[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
            TEST_OTHERS_31[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
            TEST_OTHERS_30[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
            TEST_OTHERS_30[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
            TEST_OTHERS_30[SecurityAttributes.COMPILATION_REASON_NAME] = "NO";
            TEST_OTHERS_30[SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME] = "2005-10-11";
            TEST_OTHERS_30[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
            TEST_OTHERS_30[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
            TEST_OTHERS_30[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
            TEST_OTHERS_30[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
            TEST_OTHERS_30[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
            TEST_OTHERS_30[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
            TEST_OTHERS_30[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
            TEST_OTHERS_30[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
            TEST_OTHERS_30[SecurityAttributes.NON_IC_MARKINGS_NAME] = "SINFO";
            TEST_OTHERS_30[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
            TEST_OTHERS_30[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
            TEST_OTHERS_30[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
            TEST_OTHERS_30[SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME] = "OADR";
            TEST_OTHERS_20[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
            TEST_OTHERS_20[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
            TEST_OTHERS_20[SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME] = "2005-10-11";
            TEST_OTHERS_20[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
            TEST_OTHERS_20[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
            TEST_OTHERS_20[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
            TEST_OTHERS_20[SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME] = "true";
            TEST_OTHERS_20[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
            TEST_OTHERS_20[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
            TEST_OTHERS_20[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
            TEST_OTHERS_20[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
            TEST_OTHERS_20[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
            TEST_OTHERS_20[SecurityAttributes.NON_IC_MARKINGS_NAME] = "SINFO";
            TEST_OTHERS_20[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
            TEST_OTHERS_20[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
            TEST_OTHERS_20[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
            TEST_OTHERS_20[SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME] = "OADR";
        }
        private static readonly IDictionary<string, string> TEST_OTHERS_31 = new Dictionary<string, string>();
        private static readonly IDictionary<string, string> TEST_OTHERS_30 = new Dictionary<string, string>();
        private static readonly IDictionary<string, string> TEST_OTHERS_20 = new Dictionary<string, string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public SecurityAttributesTest()
            : base(null)
        {
        }

        /// <summary>
        /// Resets the validation property.
        /// </summary>
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: protected void tearDown() throws Exception
        protected internal override void TearDown()
        {
            base.TearDown();
        }

        /// <summary>
        /// Returns a fixture object for testing. These attributes will only contain the basic required attributes
        /// (classification and ownerProducer).
        /// </summary>
        public static SecurityAttributes Fixture
        {
            get
            {
                try
                {

                    return (new SecurityAttributes(TEST_CLASS, TEST_OWNERS, null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing. These attributes will be a full set, including optional attributes.
        /// </summary>
        public static SecurityAttributes FullFixture
        {
            get
            {
                try
                {
                    return (new SecurityAttributes(TEST_CLASS, TEST_OWNERS, OtherAttributes));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a set of attributes for a specific version of DDMS.
        /// </summary>
        /// <returns> an attribute group </returns>
        private static Dictionary<string, string> OtherAttributes
        {
            get
            {
                string version = DDMSVersion.CurrentVersion.Version;
                if ("2.0".Equals(version))
                {
                    return (new Dictionary<string, string>(TEST_OTHERS_20));
                }
                if ("3.0".Equals(version))
                {
                    return (new Dictionary<string, string>(TEST_OTHERS_30));
                }
                if ("3.1".Equals(version))
                {
                    return (new Dictionary<string, string>(TEST_OTHERS_31));
                }
                return (new Dictionary<string, string>(TEST_OTHERS_41));
            }
        }

        /// <summary>
        /// Returns a set of attributes for a specific version of DDMS, with a single attribute replaced by a custom value.
        /// </summary>
        /// <param name="key"> the key of the attribute to replace </param>
        /// <param name="value"> the new value to set for that attribute </param>
        /// <returns> an attribute group </returns>
        private static Dictionary<string, string> GetOtherAttributes(string key, string value)
        {
            //TODO: Not sure what to do here
            Assert.Fail("TODO: Dictionary<string, string> baseAttributes = new Hashtable(OtherAttributes);");
            return new Dictionary<string, string>();
            /*
            Dictionary<string, string> baseAttributes = new Hashtable(OtherAttributes);
            baseAttributes[key] = value;
            return (baseAttributes);
            */
        }

        /// <summary>
        /// Helper method to confirm that changing a single attribute correctly affects equality of two instances
        /// </summary>
        /// <param name="expected"> the base set of attributes </param>
        /// <param name="key"> the key of the attribute that will change </param>
        /// <param name="value"> the value of the attribute that will change </param>
        private void AssertAttributeChangeAffectsEquality(SecurityAttributes expected, string key, string value)
        {
            Dictionary<string, string> others = GetOtherAttributes(key, value);
            Assert.IsFalse(expected.Equals(GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, others)));
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private SecurityAttributes GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SecurityAttributes attributes = null;
            try
            {
                attributes = new SecurityAttributes(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (attributes);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="classification"> the classification level, which must be a legal classification type (optional) </param>
        /// <param name="ownerProducers"> a list of ownerProducers (optional) </param>
        /// <param name="otherAttributes"> a name/value mapping of other ISM attributes. The value will be a String value, as it
        /// appears in XML. </param>
        /// <returns> a valid object </returns>
        private SecurityAttributes GetInstance(string message, string classification, List<string> ownerProducers, Dictionary<string, string> otherAttributes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SecurityAttributes attributes = null;
            try
            {
                attributes = new SecurityAttributes(classification, ownerProducers, otherAttributes);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (attributes);
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");
                string icNamespace = version.IsmNamespace;

                // All fields
                XElement element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                FullFixture.AddTo(element);
                GetInstance(SUCCESS, element);

                // No optional fields
                element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, icNamespace, TEST_CLASS);
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.OWNER_PRODUCER_NAME, icNamespace, Util.GetXsList(TEST_OWNERS));
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
                GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, OtherAttributes);

                // No optional fields
                GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, null);

                // Extra fields
                GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, GetOtherAttributes("notAnAttribute", "test"));
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");
                string icNamespace = version.IsmNamespace;

                // invalid declassDate
                XElement element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, icNamespace, TEST_CLASS);
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.OWNER_PRODUCER_NAME, icNamespace, Util.GetXsList(TEST_OWNERS));
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.DECLASS_DATE_NAME, icNamespace, "2001");
                GetInstance("The declassDate must be in the xs:date format", element);

                // invalid dateOfExemptedSource
                element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, icNamespace, TEST_CLASS);
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.OWNER_PRODUCER_NAME, icNamespace, Util.GetXsList(TEST_OWNERS));
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.DECLASS_DATE_NAME, icNamespace, "2001");
                Util.AddAttribute(element, ismPrefix, SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, icNamespace, "2001");
                string message = (version.IsAtLeast("3.1") ? "The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0." : "The dateOfExemptedSource attribute must be in the xs:date format");
                GetInstance(message, element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // invalid declassDate
                GetInstance("The declassDate must be in the xs:date format", TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DECLASS_DATE_NAME, "2004"));

                // nonsensical declassDate
                GetInstance("The ISM:declassDate attribute is not in a valid date format.", TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DECLASS_DATE_NAME, "notAnXmlDate"));

                // invalid dateOfExemptedSource
                string message = (version.IsAtLeast("3.1") ? "The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0." : "The dateOfExemptedSource attribute must be in the xs:date format");
                GetInstance(message, TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "2004"));

                // nonsensical dateOfExemptedSource
                GetInstance("The ISM:dateOfExemptedSource attribute is not in a valid date format.", TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "notAnXmlDate"));
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string icNamespace = version.IsmNamespace;

                // No warnings
                XElement element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                FullFixture.AddTo(element);
                SecurityAttributes attr = GetInstance(SUCCESS, element);
                Assert.Equals(0, attr.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string icNamespace = version.IsmNamespace;

                XElement element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                FullFixture.AddTo(element);
                SecurityAttributes elementAttributes = GetInstance(SUCCESS, element);
                SecurityAttributes dataAttributes = GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, OtherAttributes);

                Assert.Equals(elementAttributes, elementAttributes);
                Assert.Equals(elementAttributes, dataAttributes);
                Assert.Equals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string icNamespace = version.IsmNamespace;

                XElement element = Util.BuildDDMSElement(Security.GetName(version), null);
                Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                FullFixture.AddTo(element);
                SecurityAttributes expected = GetInstance(SUCCESS, element);

                if (version.IsAtLeast("3.1"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME, "FRD");
                }
                Assert.IsFalse(expected.Equals(GetInstance(SUCCESS, "C", TEST_OWNERS, OtherAttributes))); // Classification
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.CLASSIFIED_BY_NAME, DIFFERENT_VALUE);
                if (version.IsAtLeast("3.0"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.COMPILATION_REASON_NAME, DIFFERENT_VALUE);
                }
                if (!version.IsAtLeast("3.1"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "2001-10-10");
                }
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_DATE_NAME, "2001-10-10");
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_EVENT_NAME, DIFFERENT_VALUE);
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_EXCEPTION_NAME, "25X4");
                if (!version.IsAtLeast("3.0"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME, "false");
                }
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME, DIFFERENT_VALUE);
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DERIVED_FROM_NAME, DIFFERENT_VALUE);
                if (version.IsAtLeast("3.1"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DISPLAY_ONLY_TO_NAME, "USA");
                }
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DISSEMINATION_CONTROLS_NAME, "EYES");
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.FGI_SOURCE_OPEN_NAME, "BGR");
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.FGI_SOURCE_PROTECTED_NAME, "BGR");
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.NON_IC_MARKINGS_NAME, "SBU");
                if (version.IsAtLeast("3.1"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.NON_US_CONTROLS_NAME, "BALK");
                }
                Assert.IsFalse(expected.Equals(GetInstance(SUCCESS, TEST_CLASS, Util.GetXsListAsList("AUS"), OtherAttributes))); // OwnerProducer
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.RELEASABLE_TO_NAME, "BGR");
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.SAR_IDENTIFIER_NAME, "SAR-AIA");
                AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.SCI_CONTROLS_NAME, "TK");
                if (!version.IsAtLeast("3.1"))
                {
                    AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME, "X4");
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes elementAttributes = FullFixture;
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementAttributes.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void TestAddTo()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes component = Fixture;

                XElement element = Util.BuildDDMSElement("sample", null);
                component.AddTo(element);
                SecurityAttributes output = new SecurityAttributes(element);
                Assert.Equals(component, output);
            }
        }

        [TestMethod]
        public virtual void TestGetNonNull()
        {
            SecurityAttributes component = new SecurityAttributes(null, null, null);
            SecurityAttributes output = SecurityAttributes.GetNonNullInstance(null);
            Assert.Equals(component, output);

            output = SecurityAttributes.GetNonNullInstance(Fixture);
            Assert.Equals(Fixture, output);
        }

        [TestMethod]
        public virtual void TestIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SecurityAttributes dataAttributes = GetInstance(SUCCESS, null, null, null);
                Assert.IsTrue(dataAttributes.Empty);
                dataAttributes = GetInstance(SUCCESS, TEST_CLASS, null, null);
                Assert.IsFalse(dataAttributes.Empty);
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionAttributes()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            SecurityAttributes attr = GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, OtherAttributes);
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new Title("Wrong Version Title", attr);
                Assert.Fail("Allowed different versions.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "These attributes cannot decorate");
            }
        }

        [TestMethod]
        public virtual void Test30AttributesIn31()
        {
            DDMSVersion.SetCurrentVersion("3.1");
            IDictionary<string, string> others = GetOtherAttributes(SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME, "OADR");
            try
            {
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
                Assert.Fail("Allowed 3.0 attributes in 3.1.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The typeOfExemptedSource attribute can only be used");
            }

            others = GetOtherAttributes(SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "2010-01-01");
            try
            {
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
                Assert.Fail("Allowed 3.0 attributes in 3.1.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The dateOfExemptedSource attribute can only be used");
            }
        }

        [TestMethod]
        public virtual void Test31AttributesIn30()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            IDictionary<string, string> others = GetOtherAttributes(SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME, "RD");
            try
            {
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
                Assert.Fail("Allowed 3.1 attributes in 3.0.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The atomicEnergyMarkings attribute cannot be used");
            }

            others = GetOtherAttributes(SecurityAttributes.DISPLAY_ONLY_TO_NAME, "AIA");
            try
            {
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
                Assert.Fail("Allowed 3.1 attributes in 3.0.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The displayOnlyTo attribute cannot be used");
            }

            others = GetOtherAttributes(SecurityAttributes.NON_US_CONTROLS_NAME, "ATOMAL");
            try
            {
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
                Assert.Fail("Allowed 3.1 attributes in 3.0.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The nonUSControls attribute cannot be used");
            }
        }

        [TestMethod]
        public virtual void TestClassificationValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Dictionary<string, string> others = OtherAttributes;

                // Missing classification
                SecurityAttributes dataAttributes = GetInstance(SUCCESS, null, TEST_OWNERS, others);
                try
                {
                    dataAttributes.RequireClassification();
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }

                // Empty classification
                dataAttributes = GetInstance(SUCCESS, "", TEST_OWNERS, others);
                try
                {
                    dataAttributes.RequireClassification();
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }

                // Invalid classification
                GetInstance("ZOO is not a valid enumeration token", "ZOO", TEST_OWNERS, others);

                // No ownerProducers
                dataAttributes = GetInstance(SUCCESS, TEST_CLASS, null, others);
                try
                {
                    dataAttributes.RequireClassification();
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least 1 ownerProducer must be set.");
                }

                // No non-empty ownerProducers
                List<string> ownerProducers = new List<string>();
                ownerProducers.Add("");
                dataAttributes = GetInstance(" is not a valid enumeration token", TEST_CLASS, ownerProducers, others);
            }
        }

        [TestMethod]
        public virtual void TestDateOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                Dictionary<string, string> others = new Dictionary<string, string>();
                others[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
                SecurityAttributes dataAttributes = GetInstance(SUCCESS, null, null, others);
                Assert.Equals(BuildOutput(true, "declassDate", "2005-10-10"), dataAttributes.GetOutput(true, ""));
                Assert.Equals(BuildOutput(false, "declassDate", "2005-10-10"), dataAttributes.GetOutput(false, ""));

                if (!version.IsAtLeast("3.1"))
                {
                    others = new Dictionary<string, string>();
                    others[SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME] = "2005-10-10";
                    dataAttributes = GetInstance(SUCCESS, null, null, others);
                    Assert.Equals(BuildOutput(true, "dateOfExemptedSource", "2005-10-10"), dataAttributes.GetOutput(true, ""));
                    Assert.Equals(BuildOutput(false, "dateOfExemptedSource", "2005-10-10"), dataAttributes.GetOutput(false, ""));
                }
            }
        }

        [TestMethod]
        public virtual void TestOldClassifications()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            GetInstance(SUCCESS, "NS-S", TEST_OWNERS, null);
            GetInstance(SUCCESS, "NS-A", TEST_OWNERS, null);
            DDMSVersion.SetCurrentVersion("3.0");
            GetInstance("NS-S is not a valid enumeration token", "NS-S", TEST_OWNERS, null);
            GetInstance("NS-A is not a valid enumeration token for this attribute", "NS-A", TEST_OWNERS, null);
        }

        [TestMethod]
        public virtual void Test30AttributesIn20()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("3.0");
                IDictionary<string, string> others = OtherAttributes;
                DDMSVersion.SetCurrentVersion("2.0");
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
                Assert.Fail("Allowed DDMS 3.0 attributes to be used in DDMS 2.0.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The compilationReason attribute cannot be used");
            }
        }

        [TestMethod]
        public virtual void Test20AttributesIn30()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            IDictionary<string, string> map = GetOtherAttributes(SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME, "true");
            try
            {
                DDMSVersion.SetCurrentVersion("3.0");
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
                Assert.Fail("Allowed DDMS 2.0 attributes to be used in DDMS 3.0.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The declassManualReview attribute can only be used in DDMS 2.0.");
            }

            DDMSVersion.SetCurrentVersion("2.0");
            map.Remove(SecurityAttributes.COMPILATION_REASON_NAME);
            SecurityAttributes attr = new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
            Assert.IsTrue(attr.GetOutput(true, "").Contains(BuildOutput(true, "declassManualReview", "true")));
            Assert.IsTrue(attr.GetOutput(false, "").Contains(BuildOutput(false, "declassManualReview", "true")));
        }

        [TestMethod]
        public virtual void TestMultipleDeclassException()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            IDictionary<string, string> map = GetOtherAttributes(SecurityAttributes.DECLASS_EXCEPTION_NAME, "25X1 25X2");
            new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
        }

        [TestMethod]
        public virtual void TestMultipleTypeExempted()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            IDictionary<string, string> map = GetOtherAttributes(SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME, "X1 X2");
            new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
        }

        [TestMethod]
        public virtual void TestDeclassManualReviewHtmlOutput()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            IDictionary<string, string> map = new Dictionary<string, string>();
            map[SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME] = "true";
            SecurityAttributes attributes = new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
            Assert.Equals(BuildOutput(true, "classification", "U") + BuildOutput(true, "declassManualReview", "true") + BuildOutput(true, "ownerProducer", "USA"), attributes.GetOutput(true, ""));
        }

        [TestMethod]
        public virtual void TestCVEErrorsByDefault()
        {
            IDictionary<string, string> map = new Dictionary<string, string>();
            map[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "UnknownValue";
            try
            {
                new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
                Assert.Fail("Allowed invalid CVE value without throwing an Exception.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "UnknownValue is not a valid enumeration token");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SecurityAttributes component = FullFixture;
                SecurityAttributes.Builder builder = new SecurityAttributes.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SecurityAttributes.Builder builder = new SecurityAttributes.Builder();
                Assert.IsNotNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.AtomicEnergyMarkings = Util.GetXsListAsList("");
                Assert.IsTrue(builder.Empty);
                builder.AtomicEnergyMarkings = Util.GetXsListAsList("RD FRD");
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SecurityAttributes.Builder builder = new SecurityAttributes.Builder();
                builder.Classification = "SuperSecret";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "SuperSecret is not a valid enumeration token");
                }
                builder.Classification = "U";
                builder.Commit();
            }
        }

        [TestMethod]
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes.Builder builder = new SecurityAttributes.Builder();
                Assert.IsNotNull(builder.OwnerProducers[1]);
                Assert.IsNotNull(builder.SCIcontrols[1]);
                Assert.IsNotNull(builder.SARIdentifier[1]);
                Assert.IsNotNull(builder.DisseminationControls[1]);
                Assert.IsNotNull(builder.FGIsourceOpen[1]);
                Assert.IsNotNull(builder.FGIsourceProtected[1]);
                Assert.IsNotNull(builder.ReleasableTo[1]);
                Assert.IsNotNull(builder.NonICmarkings[1]);

                if (version.IsAtLeast("3.1"))
                {
                    Assert.IsNotNull(builder.AtomicEnergyMarkings[1]);
                    Assert.IsNotNull(builder.DisplayOnlyTo[1]);
                    Assert.IsNotNull(builder.NonUSControls[1]);
                }
            }
        }
    }

}