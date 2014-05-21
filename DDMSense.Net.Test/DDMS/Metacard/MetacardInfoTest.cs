using System.Collections;
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
namespace DDMSense.Test.DDMS.Metacard
{



    using NoticeList = DDMSense.DDMS.SecurityElements.NoticeList;
    using NoticeListTest = DDMSense.Test.DDMS.SecurityElements.NoticeListTest;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using AccessTest = DDMSense.Test.DDMS.SecurityElements.Ntk.AccessTest;
    using Description = DDMSense.DDMS.Summary.Description;
    using DescriptionTest = DDMSense.Test.DDMS.Summary.DescriptionTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using DDMSense.DDMS.Metacard;
    using System.Xml.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.Test.DDMS.ResourceElements;
    using System;

    /// <summary>
    /// <para> Tests related to ddms:metacardInfo elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class MetacardInfoTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public MetacardInfoTest()
            : base("metacardInfo.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static MetacardInfo Fixture
        {
            get
            {
                try
                {
                    return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? new MetacardInfo(RequiredChildComponents, SecurityAttributesTest.Fixture) : null);
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a list of child components for testing.
        /// </summary>
        private static List<IDDMSComponent> RequiredChildComponents
        {
            get
            {
                List<IDDMSComponent> childComponents = new List<IDDMSComponent>();
                childComponents.Add(IdentifierTest.Fixture);
                childComponents.Add(DatesTest.Fixture);
                childComponents.Add(PublisherTest.Fixture);
                return (childComponents);
            }
        }

        /// <summary>
        /// Returns a list of child components for testing.
        /// </summary>
        private static List<IDDMSComponent> ChildComponents
        {
            get
            {
                List<IDDMSComponent> childComponents = new List<IDDMSComponent>();
                childComponents.Add(IdentifierTest.Fixture);
                childComponents.Add(DatesTest.Fixture);
                childComponents.Add(PublisherTest.Fixture);
                childComponents.Add(ContributorTest.Fixture);
                childComponents.Add(CreatorTest.Fixture);
                childComponents.Add(PointOfContactTest.Fixture);
                childComponents.Add(DescriptionTest.Fixture);
                childComponents.Add(ProcessingInfoTest.Fixture);
                childComponents.Add(RevisionRecallTest.TextFixture);
                childComponents.Add(RecordsManagementInfoTest.Fixture);
                childComponents.Add(NoticeListTest.Fixture);
                if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
                {
                    childComponents.Add(AccessTest.Fixture);
                }
                return (childComponents);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private MetacardInfo GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            MetacardInfo component = null;
            try
            {
                component = new MetacardInfo(element);
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
        /// <param name="childComponents"> a list of child components to be put into this instance (required) </param>
        private MetacardInfo GetInstance(string message, List<IDDMSComponent> childComponents)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            MetacardInfo component = null;
            try
            {
                component = new MetacardInfo(childComponents, SecurityAttributesTest.Fixture);
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
            foreach (IDDMSComponent component in ChildComponents)
            {
                text.Append(((AbstractBaseComponent)component).GetOutput(isHTML, "metacardInfo.", ""));
            }
            text.Append(BuildOutput(isHTML, "metacardInfo.classification", "U"));
            text.Append(BuildOutput(isHTML, "metacardInfo.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:metacardInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
            xml.Append("<ddms:identifier ddms:qualifier=\"URI\" ddms:value=\"urn:buri:ddmsence:testIdentifier\" />");
            xml.Append("<ddms:dates ddms:created=\"2003\" />");
            xml.Append("<ddms:publisher><ddms:person><ddms:name>Brian</ddms:name>");
            xml.Append("<ddms:surname>Uri</ddms:surname></ddms:person></ddms:publisher>");
            xml.Append("<ddms:contributor><ddms:service><ddms:name>https://metadata.dod.mil/ebxmlquery/soap</ddms:name>");
            xml.Append("</ddms:service></ddms:contributor>");
            xml.Append("<ddms:creator><ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:creator>");
            xml.Append("<ddms:pointOfContact><ddms:unknown><ddms:name>UnknownEntity</ddms:name>");
            xml.Append("</ddms:unknown></ddms:pointOfContact>");
            xml.Append("<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
            xml.Append("A transformation service.</ddms:description>");
            xml.Append("<ddms:processingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
            xml.Append("ddms:dateProcessed=\"2011-08-19\">");
            xml.Append("XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.</ddms:processingInfo>");
            xml.Append("<ddms:revisionRecall xmlns:xlink=\"http://www.w3.org/1999/xlink\" ddms:revisionID=\"1\" ");
            xml.Append("ddms:revisionType=\"ADMINISTRATIVE RECALL\" network=\"NIPRNet\" otherNetwork=\"PBS\" ");
            xml.Append("xlink:type=\"resource\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Description of Recall</ddms:revisionRecall>");
            xml.Append("<ddms:recordsManagementInfo ddms:vitalRecordIndicator=\"true\">");
            xml.Append("<ddms:recordKeeper><ddms:recordKeeperID>#289-99202.9</ddms:recordKeeperID>");
            xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:recordKeeper>");
            xml.Append("<ddms:applicationSoftware ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
            xml.Append("IRM Generator 2L-9</ddms:applicationSoftware>");
            xml.Append("</ddms:recordsManagementInfo>");
            xml.Append("<ddms:noticeList ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
            xml.Append("<ISM:Notice ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
            xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
            {
                xml.Append(" ISM:externalNotice=\"false\"");
            }
            xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
            xml.Append("<ISM:NoticeText ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
            xml.Append(" ISM:pocType=\"DoD-Dist-B\">noticeText</ISM:NoticeText>");
            xml.Append("</ISM:Notice></ddms:noticeList>");
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
            {
                xml.Append("<ntk:Access xmlns:ntk=\"urn:us:gov:ic:ntk\" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ntk:AccessIndividualList>");
                xml.Append("<ntk:AccessIndividual ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>");
                xml.Append("<ntk:AccessIndividualValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("user_2321889:Doe_John_H</ntk:AccessIndividualValue>");
                xml.Append("</ntk:AccessIndividual>");
                xml.Append("</ntk:AccessIndividualList>");
                xml.Append("</ntk:Access>");
            }
            xml.Append("</ddms:metacardInfo>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, MetacardInfo.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in RequiredChildComponents)
                {
                    element.Add(component.ElementCopy);
                }
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, ChildComponents);

                // Null components
                List<IDDMSComponent> components = ChildComponents;
                components.Add(null);
                GetInstance(SUCCESS, components);

                // No optional fields
                GetInstance(SUCCESS, RequiredChildComponents);
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing identifier
                XElement element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in RequiredChildComponents)
                {
                    if (component is Identifier)
                    {
                        continue;
                    }
                    element.Add(component.ElementCopy);
                }
                GetInstance("At least one ddms:identifier", element);

                // Missing publisher
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in RequiredChildComponents)
                {
                    if (component is Publisher)
                    {
                        continue;
                    }
                    element.Add(component.ElementCopy);
                }
                GetInstance("At least one ddms:publisher", element);

                // Missing dates
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in RequiredChildComponents)
                {
                    if (component is Dates)
                    {
                        continue;
                    }
                    element.Add(component.ElementCopy);
                }
                GetInstance("Exactly 1 dates element must exist.", element);

                // Too many dates
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in RequiredChildComponents)
                {
                    element.Add(component.ElementCopy);
                    if (component is Dates)
                    {
                        element.Add(component.ElementCopy);
                    }
                }
                GetInstance("Exactly 1 dates element must exist.", element);

                // Too many descriptions
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in ChildComponents)
                {
                    element.Add(component.ElementCopy);
                    if (component is Description)
                    {
                        element.Add(component.ElementCopy);
                    }
                }
                GetInstance("No more than 1 description element can exist.", element);

                // Too many revisionRecalls
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in ChildComponents)
                {
                    element.Add(component.ElementCopy);
                    if (component is RevisionRecall)
                    {
                        element.Add(component.ElementCopy);
                    }
                }
                GetInstance("No more than 1 revisionRecall element can exist.", element);

                // Too many recordsManagementInfos
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in ChildComponents)
                {
                    element.Add(component.ElementCopy);
                    if (component is RecordsManagementInfo)
                    {
                        element.Add(component.ElementCopy);
                    }
                }
                GetInstance("No more than 1 recordsManagementInfo", element);

                // Too many noticeLists
                element = Util.BuildDDMSElement(MetacardInfo.GetName(version), null);
                foreach (IDDMSComponent component in ChildComponents)
                {
                    element.Add(component.ElementCopy);
                    if (component is NoticeList)
                    {
                        element.Add(component.ElementCopy);
                    }
                }
                GetInstance("No more than 1 noticeList element can exist.", element);

            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No components
                GetInstance("At least one ddms:identifier", (XElement)null);

                // Weird component
                List<IDDMSComponent> components = ChildComponents;
                components.Add(LanguageTest.Fixture);
                GetInstance("language is not a valid child", components);

                // Missing identifier
                components = ChildComponents;
                components.Remove(IdentifierTest.Fixture);
                GetInstance("At least one ddms:identifier must exist", components);

                // Missing publisher
                components = ChildComponents;
                components.Remove(PublisherTest.Fixture);
                GetInstance("At least one ddms:publisher must exist", components);

                // Missing dates
                components = ChildComponents;
                components.Remove(DatesTest.Fixture);
                GetInstance("Exactly 1 dates element must exist.", components);

                // Incorrect version of security attributes
                DDMSVersion.SetCurrentVersion("2.0");
                SecurityAttributes attributes = SecurityAttributesTest.Fixture;
                DDMSVersion.SetCurrentVersion(sVersion);
                try
                {
                    new MetacardInfo(ChildComponents, attributes);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "These attributes cannot decorate");
                }
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // 4.1 ntk:Access element used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.AreEqual(2, component.ValidationWarnings.Count());
                    string text = "The ntk:Access element in this DDMS component";
                    string locator = "ddms:metacardInfo";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                    text = "The ISM:externalNotice attribute in this DDMS component";
                    locator = "ddms:metacardInfo/ddms:noticeList/ISM:Notice";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[1]);

                }
                // No warnings 
                else
                {
                    Assert.AreEqual(0, component.ValidationWarnings.Count());
                }
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                MetacardInfo dataComponent = GetInstance(SUCCESS, ChildComponents);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));

                List<IDDMSComponent> components = ChildComponents;
                components.Add(IdentifierTest.Fixture);
                MetacardInfo dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(DatesTest.Fixture);
                components.Add(new Dates(null, null, null, null, null, null, null));
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(CreatorTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(ContributorTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(PointOfContactTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Add(PublisherTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(DescriptionTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(ProcessingInfoTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(RevisionRecallTest.TextFixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(RecordsManagementInfoTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                components = ChildComponents;
                components.Remove(NoticeListTest.Fixture);
                dataComponent = GetInstance(SUCCESS, components);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, ChildComponents);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                var expected = GetExpectedXMLOutput(true);
                var actual = component.ToXML(SaveOptions.DisableFormatting);
                Assert.AreEqual(expected,  actual);

                component = GetInstance(SUCCESS, ChildComponents);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML(SaveOptions.DisableFormatting));
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                MetacardInfo.Builder builder = new MetacardInfo.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo.Builder builder = new MetacardInfo.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Dates.ApprovedOn = "2001";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Metacard_MetacardInfo_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                MetacardInfo.Builder builder = new MetacardInfo.Builder(component);
                builder.Identifiers.Clear();
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least one ddms:identifier");
                }
                builder.Identifiers[0].Qualifier = "test";
                builder.Identifiers[0].Value = "test";
                builder.Commit();
            }
        }
    }

}