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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSense.DDMS;
    using System;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to ntk:AccessIndividual elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class IndividualTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public IndividualTest()
            : base("accessIndividual.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Individual Fixture
        {
            get
            {
                try
                {
                    return (new Individual(SystemNameTest.Fixture, IndividualValueTest.FixtureList, SecurityAttributesTest.Fixture));
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
        public static List<Individual> FixtureList
        {
            get
            {
                List<Individual> list = new List<Individual>();
                list.Add(IndividualTest.Fixture);
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
        private Individual GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Individual component = null;
            try
            {
                component = new Individual(element);
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
        /// <param name="systemName"> the system (required) </param>
        /// <param name="values"> the values (1 required) </param>
        private Individual GetInstance(string message, SystemName systemName, List<IndividualValue> values)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Individual component = null;
            try
            {
                component = new Individual(systemName, values, SecurityAttributesTest.Fixture);
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
            text.Append(SystemNameTest.Fixture.GetOutput(isHTML, "individual.", ""));
            text.Append(IndividualValueTest.FixtureList[0].GetOutput(isHTML, "individual.", ""));
            text.Append(BuildOutput(isHTML, "individual.classification", "U"));
            text.Append(BuildOutput(isHTML, "individual.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ntk:AccessIndividual ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
            xml.Append("\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
            xml.Append("\t<ntk:AccessIndividualValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">user_2321889:Doe_John_H</ntk:AccessIndividualValue>\n");
            xml.Append("</ntk:AccessIndividual>\n");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, Individual.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // Missing systemName
                XElement element = Util.BuildElement(ntkPrefix, Individual.GetName(version), version.NtkNamespace, null);
                foreach (IndividualValue value in IndividualValueTest.FixtureList)
                {
                    element.Add(value.ElementCopy);
                }
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("systemName is required.", element);

                // Missing individualValue
                element = Util.BuildElement(ntkPrefix, Individual.GetName(version), version.NtkNamespace, null);
                element.Add(SystemNameTest.Fixture.ElementCopy);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("At least one individual value is required.", element);

                // Missing security attributes
                element = Util.BuildElement(ntkPrefix, Individual.GetName(version), version.NtkNamespace, null);
                element.Add(SystemNameTest.Fixture.ElementCopy);
                foreach (IndividualValue value in IndividualValueTest.FixtureList)
                {
                    element.Add(value.ElementCopy);
                }
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing systemName
                GetInstance("systemName is required.", null, IndividualValueTest.FixtureList);

                // Missing individualValue
                GetInstance("At least one individual value is required.", SystemNameTest.Fixture, null);

                // Missing security attributes
                try
                {
                    new Individual(SystemNameTest.Fixture, IndividualValueTest.FixtureList, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Individual dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Individual dataComponent = GetInstance(SUCCESS, new SystemName("MDR", null, null, null, SecurityAttributesTest.Fixture), IndividualValueTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<IndividualValue> list = new List<IndividualValue>();
                list.Add(IndividualValueTest.Fixture);
                list.Add(IndividualValueTest.Fixture);
                dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, list);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());

                component = GetInstance(SUCCESS, SystemNameTest.Fixture, IndividualValueTest.FixtureList);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_WrongVersion()
        {
            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Individual.Builder builder = new Individual.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual.Builder builder = new Individual.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                //TODO: Not sure what to do here.
                //builder.IndividualValues[0];
                Assert.Fail("TODO: builder.IndividualValues[0]");
                Assert.IsTrue(builder.Empty);
                builder.IndividualValues[1].Value = "TEST";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Individual.Builder builder = new Individual.Builder();
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.SystemName.Value = "value";
                builder.SystemName.SecurityAttributes.Classification = "U";
                builder.SystemName.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");

                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least one individual value is required.");
                }
                builder.IndividualValues[0].Qualifier = "test";
                builder.IndividualValues[0].Value = "test";
                builder.IndividualValues[0].SecurityAttributes.Classification = "U";
                builder.IndividualValues[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Individual_BuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Individual.Builder builder = new Individual.Builder();
                Assert.IsNotNull(builder.IndividualValues[1]);
            }
        }
    }

}