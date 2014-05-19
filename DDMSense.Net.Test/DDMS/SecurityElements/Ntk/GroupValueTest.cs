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



    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.SecurityElements.Ntk;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using DDMSense.DDMS.ResourceElements;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to ntk:AccessGroupValue elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class GroupValueTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "WISE/RODCA";
        private new const string TEST_ID = "ID";
        private const string TEST_ID_REFERENCE = "ID";
        private const string TEST_QUALIFIER = "qualifier";

        /// <summary>
        /// Constructor
        /// </summary>
        public GroupValueTest()
            : base("accessGroupValue.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static GroupValue Fixture
        {
            get
            {
                try
                {
                    return (new GroupValue(TEST_VALUE, null, null, null, SecurityAttributesTest.Fixture));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<GroupValue> FixtureList
        {
            get
            {
                List<GroupValue> list = new List<GroupValue>();
                list.Add(GroupValueTest.Fixture);
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
        private GroupValue GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            GroupValue component = null;
            try
            {
                component = new GroupValue(element);
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
        /// <param name="id"> the NTK ID (optional) </param>
        /// <param name="idReference"> a reference to an NTK ID (optional) </param>
        /// <param name="qualifier"> an NTK qualifier (optional) </param>
        /// <returns> a valid object </returns>
        private GroupValue GetInstance(string message, string value, string id, string idReference, string qualifier)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            GroupValue component = null;
            try
            {
                component = new GroupValue(value, id, idReference, qualifier, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "groupValue", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "groupValue.id", TEST_ID));
            text.Append(BuildOutput(isHTML, "groupValue.idReference", TEST_ID_REFERENCE));
            text.Append(BuildOutput(isHTML, "groupValue.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "groupValue.classification", "U"));
            text.Append(BuildOutput(isHTML, "groupValue.ownerProducer", "USA"));
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
                xml.Append("<ntk:AccessGroupValue ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM);
                xml.Append(" ntk:id=\"ID\" ntk:IDReference=\"ID\" ntk:qualifier=\"qualifier\"");
                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append(TEST_VALUE).Append("</ntk:AccessGroupValue>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, GroupValue.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildElement(ntkPrefix, GroupValue.GetName(version), version.NtkNamespace, TEST_VALUE);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);

                // No optional fields
                GetInstance(SUCCESS, TEST_VALUE, null, null, null);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // Missing security attributes
                XElement element = Util.BuildElement(ntkPrefix, GroupValue.GetName(version), version.NtkNamespace, TEST_VALUE);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing security attributes
                try
                {
                    new GroupValue(TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                GroupValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());

                // No value
                component = GetInstance(SUCCESS, null, null, null, null);
                Assert.Equals(1, component.ValidationWarnings.Count());
                string text = "A ntk:AccessGroupValue element was found with no value.";
                string locator = "ntk:AccessGroupValue";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GroupValue dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GroupValue dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, DIFFERENT_VALUE, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, DIFFERENT_VALUE, TEST_QUALIFIER);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_WrongVersion()
        {
            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
                GroupValue.Builder builder = new GroupValue.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue.Builder builder = new GroupValue.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.ID = TEST_ID;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_GroupValue_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GroupValue.Builder builder = new GroupValue.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }

}