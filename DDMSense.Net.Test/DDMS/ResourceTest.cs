using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DDMSVersion = DDMSense.Util.DDMSVersion;

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

namespace DDMSense.Test.DDMS
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.Test.DDMS.ResourceElements;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using System.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using DescriptionTest = DDMSense.Test.DDMS.Summary.DescriptionTest;
    using ExtensibleAttributes = DDMSense.DDMS.Extensible.ExtensibleAttributes;
    using ExtensibleAttributesTest = DDMSense.Test.DDMS.Extensible.ExtensibleAttributesTest;
    using ExtensibleElement = DDMSense.DDMS.Extensible.ExtensibleElement;
    using ExtensibleElementTest = DDMSense.Test.DDMS.Extensible.ExtensibleElementTest;
    using Extent = DDMSense.DDMS.FormatElements.Extent;
    using Format = DDMSense.DDMS.FormatElements.Format;
    using FormatTest = DDMSense.Test.DDMS.FormatElements.FormatTest;
    using GeospatialCoverage = DDMSense.DDMS.Summary.GeospatialCoverage;
    using GeospatialCoverageTest = DDMSense.Test.DDMS.Summary.GeospatialCoverageTest;
    using Keyword = DDMSense.DDMS.Summary.Keyword;
    using Link = DDMSense.DDMS.Summary.Link;
    using MetacardInfoTest = DDMSense.Test.DDMS.Metacard.MetacardInfoTest;
    using NoticeAttributes = DDMSense.DDMS.SecurityElements.Ism.NoticeAttributes;
    using NoticeAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.NoticeAttributesTest;
    using PostalAddress = DDMSense.DDMS.Summary.PostalAddress;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using RelatedResource = DDMSense.DDMS.Summary.RelatedResource;
    using RelatedResourceTest = DDMSense.Test.DDMS.Summary.RelatedResourceTest;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using SecurityTest = DDMSense.Test.DDMS.SecurityElements.SecurityTest;
    using SubjectCoverageTest = DDMSense.Test.DDMS.Summary.SubjectCoverageTest;
    using TemporalCoverage = DDMSense.DDMS.Summary.TemporalCoverage;
    using TemporalCoverageTest = DDMSense.Test.DDMS.Summary.TemporalCoverageTest;
    using Util = DDMSense.Util.Util;
    using VirtualCoverage = DDMSense.DDMS.Summary.VirtualCoverage;
    using VirtualCoverageTest = DDMSense.Test.DDMS.Summary.VirtualCoverageTest;
    using XLinkAttributes = DDMSense.DDMS.Summary.Xlink.XLinkAttributes;

    /// <summary>
    /// <para> Tests related to ddms:resource elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class ResourceTest : AbstractBaseTestCase
    {
        private List<IDDMSComponent> TEST_TOP_LEVEL_COMPONENTS;
        private List<IDDMSComponent> TEST_NO_OPTIONAL_COMPONENTS;

        private static readonly bool? TEST_RESOURCE_ELEMENT = true;
        private const string TEST_CREATE_DATE = "2010-01-21";
        private static readonly List<string> TEST_COMPLIES_WITH = Util.GetXsListAsList("DoD5230.24");

        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceTest()
            : base("resource.xml")
        {
        }

        /// <summary>
        /// Regenerates all the components needed in a Resource
        /// </summary>
        private void CreateComponents()
        {
            TEST_TOP_LEVEL_COMPONENTS = new List<IDDMSComponent>();
            TEST_TOP_LEVEL_COMPONENTS.Add(SecurityTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(RelatedResourceTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(ResourceManagementTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(GeospatialCoverageTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(TemporalCoverageTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(VirtualCoverageTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(SubjectCoverageTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(FormatTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(PointOfContactTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(ContributorTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(PublisherTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(CreatorTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(TypeTest.Fixture);
            //TEST_TOP_LEVEL_COMPONENTS.Add(TypeTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(SourceTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(RightsTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(DatesTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(LanguageTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(DescriptionTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(SubtitleTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(TitleTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(IdentifierTest.Fixture);
            TEST_TOP_LEVEL_COMPONENTS.Add(MetacardInfoTest.Fixture);

            TEST_NO_OPTIONAL_COMPONENTS = new List<IDDMSComponent>();
            TEST_NO_OPTIONAL_COMPONENTS.Add(MetacardInfoTest.Fixture);
            TEST_NO_OPTIONAL_COMPONENTS.Add(IdentifierTest.Fixture);
            TEST_NO_OPTIONAL_COMPONENTS.Add(TitleTest.Fixture);
            TEST_NO_OPTIONAL_COMPONENTS.Add(CreatorTest.Fixture);
            TEST_NO_OPTIONAL_COMPONENTS.Add(SubjectCoverageTest.Fixture);
            TEST_NO_OPTIONAL_COMPONENTS.Add(SecurityTest.Fixture);
        }

        /// <summary>
        /// Creates a stub resource element that is otherwise correct, but leaves resource header attributes off.
        /// </summary>
        /// <returns> the element </returns>
        /// <exception cref="InvalidDDMSException"> </exception>
        private XElement ResourceWithoutHeaderElement
        {
            get
            {
                XElement element = Util.BuildDDMSElement(Resource.GetName(DDMSVersion.CurrentVersion), null);
                if (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
                {
                    element.Add(MetacardInfoTest.Fixture.ElementCopy);
                }
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                return (element);
            }
        }

        /// <summary>
        /// Creates a stub resource element that is otherwise correct, but leaves resource components out.
        /// </summary>
        /// <returns> the element </returns>
        /// <exception cref="InvalidDDMSException"> </exception>
        private XElement ResourceWithoutBodyElement
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                string ismPrefix = PropertyReader.GetPrefix("ism");
                string ismNamespace = version.IsmNamespace;
                string ntkPrefix = PropertyReader.GetPrefix("ntk");
                string ntkNamespace = version.NtkNamespace;

                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);

                Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, XmlConvert.ToString(TEST_RESOURCE_ELEMENT.Value));
                Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                if (version.IsAtLeast("4.0.1"))
                {
                    Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                }
                SecurityAttributesTest.Fixture.AddTo(element);
                return (element);
            }
        }

        /// <summary>
        /// Creates a stub resource element that contains a bunch of pre-DDMS 4.0.1 relatedResources in different
        /// configurations.
        /// </summary>
        /// <returns> the element </returns>
        /// <exception cref="InvalidDDMSException"> </exception>
        private XElement ResourceWithMultipleRelated
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                if (version.IsAtLeast("4.0.1"))
                {
                    return null;
                }
                string ismPrefix = PropertyReader.GetPrefix("ism");
                string ismNamespace = version.IsmNamespace;

                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, XmlConvert.ToString(TEST_RESOURCE_ELEMENT.Value));
                Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                SecurityAttributesTest.Fixture.AddTo(element);
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);

                Link link = new Link(new XLinkAttributes("http://en.wikipedia.org/wiki/Tank", "role", null, null));

                // #1: a ddms:relatedResources containing 1 ddms:RelatedResource
                XElement rel1 = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(rel1, "relationship", "http://purl.org/dc/terms/references");
                XElement innerElement = Util.BuildDDMSElement("RelatedResource", null);
                Util.AddDDMSAttribute(innerElement, "qualifier", "http://purl.org/dc/terms/URI");
                Util.AddDDMSAttribute(innerElement, "value", "http://en.wikipedia.org/wiki/Tank1");
                innerElement.Add(link.ElementCopy);
                rel1.Add(innerElement);
                element.Add(rel1);

                // #2: a ddms:relatedResources containing 3 ddms:RelatedResources
                XElement rel2 = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(rel2, "relationship", "http://purl.org/dc/terms/references");
                XElement innerElement1 = Util.BuildDDMSElement("RelatedResource", null);
                Util.AddDDMSAttribute(innerElement1, "qualifier", "http://purl.org/dc/terms/URI");
                Util.AddDDMSAttribute(innerElement1, "value", "http://en.wikipedia.org/wiki/Tank2");
                innerElement1.Add(link.ElementCopy);
                XElement innerElement2 = Util.BuildDDMSElement("RelatedResource", null);
                Util.AddDDMSAttribute(innerElement2, "qualifier", "http://purl.org/dc/terms/URI");
                Util.AddDDMSAttribute(innerElement2, "value", "http://en.wikipedia.org/wiki/Tank3");
                innerElement2.Add(link.ElementCopy);
                XElement innerElement3 = Util.BuildDDMSElement("RelatedResource", null);
                Util.AddDDMSAttribute(innerElement3, "qualifier", "http://purl.org/dc/terms/URI");
                Util.AddDDMSAttribute(innerElement3, "value", "http://en.wikipedia.org/wiki/Tank4");
                innerElement3.Add(link.ElementCopy);
                rel2.Add(innerElement1);
                rel2.Add(innerElement2);
                rel2.Add(innerElement3);
                element.Add(rel2);

                element.Add(SecurityTest.Fixture.ElementCopy);
                return (element);
            }
        }

        /// <summary>
        /// Returns an acceptable DESVersion for the version of DDMS
        /// </summary>
        /// <returns> a DESVersion </returns>
        private int? IsmDESVersion
        {
            get
            {
                if (!DDMSVersion.CurrentVersion.IsAtLeast("3.1"))
                {
                    return (Convert.ToInt32(2));
                }
                if (!DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
                {
                    return (Convert.ToInt32(5));
                }
                return (Convert.ToInt32(9));
            }
        }

        /// <summary>
        /// Returns an acceptable DESVersion for the version of DDMS
        /// </summary>
        /// <returns> a DESVersion </returns>
        private int? NtkDESVersion
        {
            get
            {
                if (!DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
                {
                    return (null);
                }
                return (Convert.ToInt32(7));
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private Resource GetInstance(string message, XElement element)
        {
            bool expectFailure = !string.IsNullOrEmpty(message);
            Resource component = null;
            try
            {
                component = new Resource(element);
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
        /// Helper method to create a DDMS 3.0 object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="topLevelComponents"> a list of top level components </param>
        /// <param name="resourceElement"> value of the resourceElement XAttribute (required) </param>
        /// <param name="createDate"> the create date as an xs:date (YYYY-MM-DD) (required) </param>
        /// <param name="compliesWith"> the compliesWith XAttribute </param>
        /// <param name="ismDESVersion"> the ISM DES Version as an Integer (required) </param>
        /// <param name="ntkDESVersion"> the NTK DES Version as an Integer (required, starting in DDMS 4.0.1) </param>
        /// <returns> a valid object </returns>
        private Resource GetInstance(string message, List<IDDMSComponent> topLevelComponents, bool? resourceElement, string createDate, List<string> compliesWiths, int? ismDESVersion, int? ntkDESVersion)
        {
            bool expectFailure = !string.IsNullOrEmpty(message);
            DDMSVersion version = DDMSVersion.CurrentVersion;
            Resource component = null;
            try
            {
                NoticeAttributes notice = (!version.IsAtLeast("4.0.1") ? null : NoticeAttributesTest.Fixture);
                SecurityAttributes attr = (!version.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                component = new Resource(topLevelComponents, resourceElement, createDate, compliesWiths, ismDESVersion, ntkDESVersion, attr, notice, null);
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
            string resourcePrefix = Resource.GetName(version);
            if (version.IsAtLeast("3.0"))
            {
                text.Append(BuildOutput(isHTML, resourcePrefix + ".resourceElement", "true"));
                text.Append(BuildOutput(isHTML, resourcePrefix + ".createDate", "2010-01-21"));
            }
            text.Append(BuildOutput(isHTML, resourcePrefix + ".ism.DESVersion", Convert.ToString(IsmDESVersion)));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, resourcePrefix + ".ntk.DESVersion", Convert.ToString(NtkDESVersion)));
            }
            if (version.IsAtLeast("3.0"))
            {
                text.Append(BuildOutput(isHTML, resourcePrefix + ".classification", "U"));
                text.Append(BuildOutput(isHTML, resourcePrefix + ".ownerProducer", "USA"));
            }
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, resourcePrefix + ".noticeType", "DoD-Dist-B"));
                text.Append(BuildOutput(isHTML, resourcePrefix + ".noticeReason", "noticeReason"));
                text.Append(BuildOutput(isHTML, resourcePrefix + ".noticeDate", "2011-09-15"));
                text.Append(BuildOutput(isHTML, resourcePrefix + ".unregisteredNoticeType", "unregisteredNoticeType"));
                if (version.IsAtLeast("4.1"))
                {
                    text.Append(BuildOutput(isHTML, resourcePrefix + ".externalNotice", "false"));
                }
                text.Append(BuildOutput(isHTML, "metacardInfo.identifier.qualifier", "URI"));
                text.Append(BuildOutput(isHTML, "metacardInfo.identifier.value", "urn:buri:ddmsence:testIdentifier"));
                text.Append(BuildOutput(isHTML, "metacardInfo.dates.created", "2003"));
                text.Append(BuildOutput(isHTML, "metacardInfo.publisher.entityType", "person"));
                text.Append(BuildOutput(isHTML, "metacardInfo.publisher.name", "Brian"));
                text.Append(BuildOutput(isHTML, "metacardInfo.publisher.surname", "Uri"));
                text.Append(BuildOutput(isHTML, "metacardInfo.classification", "U"));
                text.Append(BuildOutput(isHTML, "metacardInfo.ownerProducer", "USA"));
            }
            text.Append(BuildOutput(isHTML, "identifier.qualifier", "URI"));
            text.Append(BuildOutput(isHTML, "identifier.value", "urn:buri:ddmsence:testIdentifier"));
            text.Append(BuildOutput(isHTML, "title", "DDMSence"));
            text.Append(BuildOutput(isHTML, "title.classification", "U"));
            text.Append(BuildOutput(isHTML, "title.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "subtitle", "Version 0.1"));
            text.Append(BuildOutput(isHTML, "subtitle.classification", "U"));
            text.Append(BuildOutput(isHTML, "subtitle.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "description", "A transformation service."));
            text.Append(BuildOutput(isHTML, "description.classification", "U"));
            text.Append(BuildOutput(isHTML, "description.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "language.qualifier", "http://purl.org/dc/elements/1.1/language"));
            text.Append(BuildOutput(isHTML, "language.value", "en"));
            text.Append(BuildOutput(isHTML, "dates.created", "2003"));
            text.Append(BuildOutput(isHTML, "rights.privacyAct", "true"));
            text.Append(BuildOutput(isHTML, "rights.intellectualProperty", "true"));
            text.Append(BuildOutput(isHTML, "rights.copyright", "true"));
            text.Append(BuildOutput(isHTML, "source.value", "http://www.xmethods.com"));
            text.Append(BuildOutput(isHTML, "type.qualifier", "DCMITYPE"));
            text.Append(BuildOutput(isHTML, "type.value", "http://purl.org/dc/dcmitype/Text"));
            text.Append(BuildOutput(isHTML, "creator.entityType", Organization.GetName(version)));
            text.Append(BuildOutput(isHTML, "creator.name", "DISA"));
            text.Append(BuildOutput(isHTML, "publisher.entityType", Person.GetName(version)));
            text.Append(BuildOutput(isHTML, "publisher.name", "Brian"));
            text.Append(BuildOutput(isHTML, "publisher.surname", "Uri"));
            text.Append(BuildOutput(isHTML, "contributor.entityType", Service.GetName(version)));
            text.Append(BuildOutput(isHTML, "contributor.name", "https://metadata.dod.mil/ebxmlquery/soap"));
            if (version.IsAtLeast("3.0"))
            {
                text.Append(BuildOutput(isHTML, "pointOfContact.entityType", Unknown.GetName(version)));
                text.Append(BuildOutput(isHTML, "pointOfContact.name", "UnknownEntity"));
            }
            else
            {
                text.Append(BuildOutput(isHTML, "pointOfContact.entityType", Person.GetName(version)));
                text.Append(BuildOutput(isHTML, "pointOfContact.name", "Brian"));
                text.Append(BuildOutput(isHTML, "pointOfContact.surname", "Uri"));
            }

            string formatPrefix = (version.IsAtLeast("4.0.1") ? "format." : "format.Media.");
            string subjectPrefix = (version.IsAtLeast("4.0.1") ? "subjectCoverage." : "subjectCoverage.Subject.");
            string temporalPrefix = (version.IsAtLeast("4.0.1") ? "temporalCoverage." : "temporalCoverage.TimePeriod.");
            string geospatialPrefix = version.IsAtLeast("4.0.1") ? "geospatialCoverage." : "geospatialCoverage.GeospatialExtent.";
            string relatedPrefix = (version.IsAtLeast("4.0.1") ? "relatedResource." : "relatedResources.RelatedResource.");

            text.Append(BuildOutput(isHTML, formatPrefix + "mimeType", "text/xml"));
            text.Append(BuildOutput(isHTML, subjectPrefix + "keyword", "DDMSence"));
            text.Append(BuildOutput(isHTML, "virtualCoverage.address", "123.456.789.0"));
            text.Append(BuildOutput(isHTML, "virtualCoverage.protocol", "IP"));
            text.Append(BuildOutput(isHTML, temporalPrefix + "name", "Unknown"));
            text.Append(BuildOutput(isHTML, temporalPrefix + "start", "1979-09-15"));
            text.Append(BuildOutput(isHTML, temporalPrefix + "end", "Not Applicable"));
            text.Append(BuildOutput(isHTML, geospatialPrefix + "boundingGeometry.Point.id", "IDValue"));
            text.Append(BuildOutput(isHTML, geospatialPrefix + "boundingGeometry.Point.srsName", "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D"));
            text.Append(BuildOutput(isHTML, geospatialPrefix + "boundingGeometry.Point.srsDimension", "10"));
            text.Append(BuildOutput(isHTML, geospatialPrefix + "boundingGeometry.Point.axisLabels", "A B C"));
            text.Append(BuildOutput(isHTML, geospatialPrefix + "boundingGeometry.Point.uomLabels", "Meter Meter Meter"));
            text.Append(BuildOutput(isHTML, geospatialPrefix + "boundingGeometry.Point.pos", "32.1 40.1"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "relationship", "http://purl.org/dc/terms/references"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "direction", "outbound"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "qualifier", "http://purl.org/dc/terms/URI"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "value", "http://en.wikipedia.org/wiki/Tank"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "link.type", "locator"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "link.href", "http://en.wikipedia.org/wiki/Tank"));
            text.Append(BuildOutput(isHTML, relatedPrefix + "link.role", "role"));

            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo", "XSLT Transformation to convert DDMS 2.0 to DDMS 3.1."));
                text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo.dateProcessed", "2011-08-19"));
                text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo.classification", "U"));
                text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo.ownerProducer", "USA"));
                text.Append(BuildOutput(isHTML, "resourceManagement.classification", "U"));
                text.Append(BuildOutput(isHTML, "resourceManagement.ownerProducer", "USA"));
            }
            if (version.IsAtLeast("3.0"))
            {
                text.Append(BuildOutput(isHTML, "security.excludeFromRollup", "true"));
            }
            text.Append(BuildOutput(isHTML, "security.classification", "U"));
            text.Append(BuildOutput(isHTML, "security.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "extensible.layer", "false"));
            text.Append(BuildOutput(isHTML, "ddms.generator", "DDMSence " + PropertyReader.GetProperty("version")));
            // Output for version will be based upon XML namespace of created resource, not the currently set version.
            text.Append(BuildOutput(isHTML, "ddms.version", DDMSVersion.GetVersionForNamespace(version.Namespace).Version));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:").Append(Resource.GetName(version)).Append(" ").Append(XmlnsDDMS);
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append(" ").Append(XmlnsNTK);
            }
            xml.Append(" ").Append(XmlnsISM);
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append(" ntk:DESVersion=\"").Append(NtkDESVersion).Append("\"");
            }
            if (version.IsAtLeast("3.0"))
            {
                xml.Append(" ISM:resourceElement=\"true\"");
            }
            // Adding DESVersion in DDMS 2.0 allows the namespace declaration to definitely be in the Resource element.
            xml.Append(" ISM:DESVersion=\"").Append(IsmDESVersion).Append("\"");
            if (version.IsAtLeast("3.0"))
            {
                xml.Append(" ISM:createDate=\"2010-01-21\"");
            }
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append(" ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
                xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
                if (version.IsAtLeast("4.1"))
                {
                    xml.Append(" ISM:externalNotice=\"false\"");
                }
            }
            if (version.IsAtLeast("3.0"))
            {
                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
            }
            xml.Append(">\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t<ddms:metacardInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:identifier ddms:qualifier=\"URI\" ddms:value=\"urn:buri:ddmsence:testIdentifier\" />");
                xml.Append("<ddms:dates ddms:created=\"2003\" /><ddms:publisher><ddms:person><ddms:name>Brian</ddms:name>");
                xml.Append("<ddms:surname>Uri</ddms:surname></ddms:person></ddms:publisher></ddms:metacardInfo>\n");
            }
            xml.Append("\t<ddms:identifier ddms:qualifier=\"URI\" ddms:value=\"urn:buri:ddmsence:testIdentifier\" />\n");
            xml.Append("\t<ddms:title ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DDMSence</ddms:title>\n");
            xml.Append("\t<ddms:subtitle ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Version 0.1</ddms:subtitle>\n");
            xml.Append("\t<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
            xml.Append("A transformation service.</ddms:description>\n");
            xml.Append("\t<ddms:language ddms:qualifier=\"http://purl.org/dc/elements/1.1/language\" ");
            xml.Append("ddms:value=\"en\" />\n");
            xml.Append("\t<ddms:dates ddms:created=\"2003\" />\n");
            xml.Append("\t<ddms:rights ddms:privacyAct=\"true\" ddms:intellectualProperty=\"true\" ");
            xml.Append("ddms:copyright=\"true\" />\n");
            xml.Append("\t<ddms:source ddms:value=\"http://www.xmethods.com\" />\n");
            xml.Append("\t<ddms:type ddms:qualifier=\"DCMITYPE\" ddms:value=\"http://purl.org/dc/dcmitype/Text\" />\n");
            xml.Append("\t<ddms:creator>\n");
            xml.Append("\t\t<ddms:").Append(Organization.GetName(version)).Append(">\n");
            xml.Append("\t\t\t<ddms:name>DISA</ddms:name>\n");
            xml.Append("\t\t</ddms:").Append(Organization.GetName(version)).Append(">\t\n");
            xml.Append("\t</ddms:creator>\n");
            xml.Append("\t<ddms:publisher>\n");
            xml.Append("\t\t<ddms:").Append(Person.GetName(version)).Append(">\n");
            xml.Append("\t\t\t<ddms:name>Brian</ddms:name>\n");
            xml.Append("\t\t\t<ddms:surname>Uri</ddms:surname>\n");
            xml.Append("\t\t</ddms:").Append(Person.GetName(version)).Append(">\t\n");
            xml.Append("\t</ddms:publisher>\n");
            xml.Append("\t<ddms:contributor>\n");
            xml.Append("\t\t<ddms:").Append(Service.GetName(version)).Append(">\n");
            xml.Append("\t\t\t<ddms:name>https://metadata.dod.mil/ebxmlquery/soap</ddms:name>\n");
            xml.Append("\t\t</ddms:").Append(Service.GetName(version)).Append(">\t\n");
            xml.Append("\t</ddms:contributor>\n");
            xml.Append("\t<ddms:pointOfContact>\n");
            if (version.IsAtLeast("3.0"))
            {
                xml.Append("\t\t<ddms:").Append(Unknown.GetName(version)).Append(">\n");
                xml.Append("\t\t\t<ddms:name>UnknownEntity</ddms:name>\n");
                xml.Append("\t\t</ddms:").Append(Unknown.GetName(version)).Append(">\t\n");
            }
            else
            {
                xml.Append("\t\t<ddms:").Append(Person.GetName(version)).Append(">\n");
                xml.Append("\t\t\t<ddms:name>Brian</ddms:name>\n");
                xml.Append("\t\t\t<ddms:surname>Uri</ddms:surname>\n");
                xml.Append("\t\t</ddms:").Append(Person.GetName(version)).Append(">\n");
            }
            xml.Append("\t</ddms:pointOfContact>\n");
            xml.Append("\t<ddms:format>\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t\t<ddms:mimeType>text/xml</ddms:mimeType>\n");
            }
            else
            {
                xml.Append("\t\t<ddms:Media>\n");
                xml.Append("\t\t\t<ddms:mimeType>text/xml</ddms:mimeType>\n");
                xml.Append("\t\t</ddms:Media>\n");
            }
            xml.Append("\t</ddms:format>\n");
            xml.Append("\t<ddms:subjectCoverage>\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
            }
            else
            {
                xml.Append("\t\t<ddms:Subject>\n");
                xml.Append("\t\t\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
                xml.Append("\t\t</ddms:Subject>\n");
            }
            xml.Append("\t</ddms:subjectCoverage>\n");
            xml.Append("\t<ddms:virtualCoverage ddms:address=\"123.456.789.0\" ddms:protocol=\"IP\" />\n");
            xml.Append("\t<ddms:temporalCoverage>\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t\t<ddms:start>1979-09-15</ddms:start>\n");
                xml.Append("\t\t<ddms:end>Not Applicable</ddms:end>\n");
            }
            else
            {
                xml.Append("\t\t<ddms:TimePeriod>\n");
                xml.Append("\t\t\t<ddms:start>1979-09-15</ddms:start>\n");
                xml.Append("\t\t\t<ddms:end>Not Applicable</ddms:end>\n");
                xml.Append("\t\t</ddms:TimePeriod>\n");
            }
            xml.Append("\t</ddms:temporalCoverage>\n");
            xml.Append("\t<ddms:geospatialCoverage>\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t\t<ddms:boundingGeometry>\n");
                xml.Append("\t\t\t<gml:Point xmlns:gml=\"").Append(version.GmlNamespace).Append("\" ");
                xml.Append("gml:id=\"IDValue\" srsName=\"http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D\" ");
                xml.Append("srsDimension=\"10\" axisLabels=\"A B C\" uomLabels=\"Meter Meter Meter\">\n");
                xml.Append("\t\t\t\t<gml:pos>32.1 40.1</gml:pos>\n");
                xml.Append("\t\t\t</gml:Point>\n");
                xml.Append("\t\t</ddms:boundingGeometry>\n");
            }
            else
            {
                xml.Append("\t\t<ddms:GeospatialExtent>\n");
                xml.Append("\t\t\t<ddms:boundingGeometry>\n");
                xml.Append("\t\t\t\t<gml:Point xmlns:gml=\"").Append(version.GmlNamespace).Append("\" ");
                xml.Append("gml:id=\"IDValue\" srsName=\"http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D\" ");
                xml.Append("srsDimension=\"10\" axisLabels=\"A B C\" uomLabels=\"Meter Meter Meter\">\n");
                xml.Append("\t\t\t\t\t<gml:pos>32.1 40.1</gml:pos>\n");
                xml.Append("\t\t\t\t</gml:Point>\n");
                xml.Append("\t\t\t</ddms:boundingGeometry>\n");
                xml.Append("\t\t</ddms:GeospatialExtent>\n");
            }
            xml.Append("\t</ddms:geospatialCoverage>\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t<ddms:relatedResource ddms:relationship=\"http://purl.org/dc/terms/references\" ").Append("ddms:direction=\"outbound\" ddms:qualifier=\"http://purl.org/dc/terms/URI\" ").Append("ddms:value=\"http://en.wikipedia.org/wiki/Tank\">\n");
                xml.Append("\t\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ").Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"role\" />\n");
                xml.Append("\t</ddms:relatedResource>\n");
            }
            else
            {
                xml.Append("\t<ddms:relatedResources ddms:relationship=\"http://purl.org/dc/terms/references\" ").Append("ddms:direction=\"outbound\">\n");
                xml.Append("\t\t<ddms:RelatedResource ddms:qualifier=\"http://purl.org/dc/terms/URI\" ").Append("ddms:value=\"http://en.wikipedia.org/wiki/Tank\">\n");
                xml.Append("\t\t\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ").Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"role\" />\n");
                xml.Append("\t\t</ddms:RelatedResource>\n");
                xml.Append("\t</ddms:relatedResources>\n");
            }
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t<ddms:resourceManagement ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:processingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
                xml.Append("ddms:dateProcessed=\"2011-08-19\">");
                xml.Append("XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.</ddms:processingInfo>");
                xml.Append("</ddms:resourceManagement>\n");
            }
            xml.Append("\t<ddms:security ");
            if (version.IsAtLeast("3.0"))
            {
                xml.Append("ISM:excludeFromRollup=\"true\" ");
            }
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" />\n");
            xml.Append("</ddms:").Append(Resource.GetName(version)).Append(">");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Resource.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = ResourceWithoutBodyElement;
                if (version.IsAtLeast("4.0.1"))
                {
                    element.Add(MetacardInfoTest.Fixture.ElementCopy);
                }
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance(SUCCESS, element);

                // More than 1 subjectCoverage
                if (version.IsAtLeast("4.0.1"))
                {
                    element = ResourceWithoutBodyElement;
                    element.Add(MetacardInfoTest.Fixture.ElementCopy);
                    element.Add(IdentifierTest.Fixture.ElementCopy);
                    element.Add(TitleTest.Fixture.ElementCopy);
                    element.Add(CreatorTest.Fixture.ElementCopy);
                    element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                    element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                    element.Add(SecurityTest.Fixture.ElementCopy);
                    GetInstance(SUCCESS, element);
                }
            }
        }

        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                // All fields
                GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // No optional fields
                GetInstance(SUCCESS, TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
            }
        }

        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();
                string ismPrefix = PropertyReader.GetPrefix("ism");
                string ismNamespace = version.IsmNamespace;
                string ntkPrefix = PropertyReader.GetPrefix("ntk");
                string ntkNamespace = version.NtkNamespace;
                XElement element;
                if (version.IsAtLeast("3.0"))
                {
                    // Missing resourceElement
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("resourceElement is required.", element);

                    // Empty resourceElement
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, "");
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("resourceElement is required.", element);

                    // Invalid resourceElement
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, "aardvark");
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("resourceElement is required.", element);

                    // Missing createDate
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, XmlConvert.ToString(TEST_RESOURCE_ELEMENT.Value));
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("createDate is required.", element);

                    // Invalid createDate
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, XmlConvert.ToString(TEST_RESOURCE_ELEMENT.Value));
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, "2004");
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("The createDate must be in the xs:date format", element);

                    // Missing desVersion
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("ISM:DESVersion is required.", element);

                    // desVersion not an integer
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, XmlConvert.ToString(TEST_RESOURCE_ELEMENT.Value));
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, "one");
                    if (version.IsAtLeast("4.0.1"))
                    {
                        Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
                    }
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("ISM:DESVersion is required", element);
                }
                if (version.IsAtLeast("4.0.1"))
                {
                    // NTK desVersion not an integer
                    element = ResourceWithoutHeaderElement;
                    Util.AddAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, XmlConvert.ToString(TEST_RESOURCE_ELEMENT.Value));
                    Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
                    Util.AddAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
                    Util.AddAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, "one");
                    SecurityAttributesTest.Fixture.AddTo(element);
                    GetInstance("ntk:DESVersion is required.", element);
                }

                // At least 1 producer
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                GetInstance("Exactly 1 security element must exist.", element);

                // At least 1 identifier
                element = ResourceWithoutBodyElement;
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("At least 1 identifier is required.", element);

                // At least 1 title
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("At least 1 title is required.", element);

                // No more than 1 description
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(DescriptionTest.Fixture.ElementCopy);
                element.Add(DescriptionTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("No more than 1 description element can exist.", element);

                // No more than 1 dates
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(DatesTest.Fixture.ElementCopy);
                element.Add(DatesTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("No more than 1 dates element can exist.", element);

                // No more than 1 rights
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(RightsTest.Fixture.ElementCopy);
                element.Add(RightsTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("No more than 1 rights element can exist.", element);

                // No more than 1 formats
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(FormatTest.Fixture.ElementCopy);
                element.Add(FormatTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("No more than 1 format element can exist.", element);

                // No more than 1 resourceManagement
                if (version.IsAtLeast("4.0.1"))
                {
                    element = ResourceWithoutBodyElement;
                    element.Add(IdentifierTest.Fixture.ElementCopy);
                    element.Add(TitleTest.Fixture.ElementCopy);
                    element.Add(CreatorTest.Fixture.ElementCopy);
                    element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                    element.Add(ResourceManagementTest.Fixture.ElementCopy);
                    element.Add(ResourceManagementTest.Fixture.ElementCopy);
                    element.Add(SecurityTest.Fixture.ElementCopy);
                    GetInstance("No more than 1 resourceManagement", element);
                }

                // At least 1 subjectCoverage
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                string message = version.IsAtLeast("4.0.1") ? "At least 1 subjectCoverage is required." : "Exactly 1 subjectCoverage element must exist.";
                GetInstance(message, element);

                // No more than 1 subjectCoverage
                if (!version.IsAtLeast("4.0.1"))
                {
                    element = ResourceWithoutBodyElement;
                    element.Add(IdentifierTest.Fixture.ElementCopy);
                    element.Add(TitleTest.Fixture.ElementCopy);
                    element.Add(CreatorTest.Fixture.ElementCopy);
                    element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                    element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                    element.Add(SecurityTest.Fixture.ElementCopy);
                    GetInstance("Exactly 1 subjectCoverage element must exist.", element);
                }

                // At least 1 security
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                GetInstance("Exactly 1 security element must exist.", element);

                // No more than 1 security
                element = ResourceWithoutBodyElement;
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                GetInstance("Extensible elements cannot be defined", element);

                // No top level components
                element = ResourceWithoutBodyElement;
                GetInstance("At least 1 identifier is required.", element);
            }
        }

        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                if (version.IsAtLeast("3.0"))
                {
                    // Missing createDate
                    GetInstance("createDate is required.", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, null, null, IsmDESVersion, NtkDESVersion);

                    // Invalid createDate
                    GetInstance("The createDate must be in the xs:date format", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, "2001", null, IsmDESVersion, NtkDESVersion);

                    // Nonsensical createDate
                    GetInstance("The ISM:createDate XAttribute is not in a valid date format.", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, "notAnXmlDate", null, IsmDESVersion, NtkDESVersion);

                    // Missing desVersion
                    GetInstance("ISM:DESVersion is required.", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, null, NtkDESVersion);
                }
                if (version.IsAtLeast("4.0.1"))
                {
                    // Missing desVersion
                    GetInstance("ntk:DESVersion is required", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, null);
                }

                // At least 1 producer
                List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Remove(CreatorTest.Fixture);
                GetInstance("At least 1 producer", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // At least 1 identifier
                components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Remove(IdentifierTest.Fixture);
                GetInstance("At least 1 identifier is required.", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // At least 1 title
                components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Remove(TitleTest.Fixture);
                GetInstance("At least 1 title is required.", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // At least 1 subjectCoverage
                string message = version.IsAtLeast("4.0.1") ? "At least 1 subjectCoverage is required." : "Exactly 1 subjectCoverage element must exist.";
                components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Remove(SubjectCoverageTest.Fixture);
                GetInstance(message, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // At least 1 security
                components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Remove(SecurityTest.Fixture);
                GetInstance("Exactly 1 security element must exist.", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // No top level components
                GetInstance("At least 1 identifier is required.", null, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // Non-top-level component
                components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Add(new Keyword("test", null));
                GetInstance("keyword is not a valid top-level component in a resource.", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();
                string text = string.Empty;
                string locator = string.Empty;

                Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // 4.1 ism:Notice used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.AreEqual(1, component.ValidationWarnings.Count);
                    text = "The ISM:externalNotice XAttribute in this DDMS component";
                    locator = "ddms:resource";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                // No warnings
                else
                {
                    Assert.AreEqual(0, component.ValidationWarnings.Count);
                }

                int countIndex = version.IsAtLeast("4.1") ? 1 : 0;

                // Nested warnings
                List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Add(new Format("test", new Extent("test", ""), "test"));
                component = GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
                Assert.AreEqual(countIndex + 1, component.ValidationWarnings.Count);

                if (version.IsAtLeast("4.1"))
                {
                    text = "The ISM:externalNotice XAttribute";
                    locator = "ddms:resource";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                string resourceName = Resource.GetName(version);
                text = "A qualifier has been set without an accompanying value XAttribute.";
                locator = (version.IsAtLeast("4.0.1")) ? "ddms:" + resourceName + "/ddms:format/ddms:extent" : "ddms:" + resourceName + "/ddms:format/ddms:Media/ddms:extent";
                AssertWarningEquality(text, locator, component.ValidationWarnings[countIndex]);

                // More nested warnings
                XElement element = Util.BuildDDMSElement(PostalAddress.GetName(version), null);
                PostalAddress address = new PostalAddress(element);
                components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
                components.Add(new GeospatialCoverage(null, null, null, address, null, null, null, null));
                component = GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
                Assert.AreEqual(countIndex + 1, component.ValidationWarnings.Count);

                if (version.IsAtLeast("4.1"))
                {
                    text = "The ISM:externalNotice XAttribute";
                    locator = "ddms:resource";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                text = "A completely empty ddms:postalAddress element was found.";
                locator = (version.IsAtLeast("4.0.1")) ? "ddms:" + resourceName + "/ddms:geospatialCoverage/ddms:postalAddress" : "ddms:" + resourceName + "/ddms:geospatialCoverage/ddms:GeospatialExtent/ddms:postalAddress";
                AssertWarningEquality(text, locator, component.ValidationWarnings[countIndex]);
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                Resource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Resource dataComponent = (!version.IsAtLeast("3.0") ? GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion) : GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion));
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                Resource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Resource dataComponent;
                // resourceElement is fixed starting in 3.1.
                if (!version.IsAtLeast("3.1"))
                {
                    dataComponent = GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, false, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }

                dataComponent = GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, "1999-10-10", null, IsmDESVersion, NtkDESVersion);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                // Can only use alternate DESVersions in early DDMS versions
                if (!version.IsAtLeast("3.1"))
                {
                    dataComponent = GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, Convert.ToInt32(1), NtkDESVersion);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }

                dataComponent = GetInstance(SUCCESS, TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();
                Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = (!version.IsAtLeast("3.0") ? GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion) : GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));

                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = (!version.IsAtLeast("3.0") ? GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion) : GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionCompliesWith()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            CreateComponents();

            GetInstance("The compliesWith XAttribute cannot be used", TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, TEST_COMPLIES_WITH, IsmDESVersion, NtkDESVersion);
        }

        [TestMethod]
        public virtual void TestWrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            CreateComponents();
            // Security attributes do not exist in 2.0
            new Resource(TEST_TOP_LEVEL_COMPONENTS, ExtensibleAttributesTest.Fixture);

            // But attributes can still be used.
            new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, null, SecurityAttributesTest.Fixture, null, null);
        }

        [TestMethod]
        public virtual void TestExtensibleSuccess()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                // Extensible XAttribute added
                ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
                if (!version.IsAtLeast("3.0"))
                {
                    new Resource(TEST_TOP_LEVEL_COMPONENTS, attr);
                }
                else
                {
                    new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, attr);
                }
            }
        }

        [TestMethod]
        public virtual void Test20ExtensibleElementSize()
        {
            DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
            CreateComponents();
            string ismPrefix = PropertyReader.GetPrefix("ism");

            // ISM:DESVersion in element
            XElement element = ResourceWithoutHeaderElement;
            Util.AddAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, version.IsmNamespace, Convert.ToString(IsmDESVersion));
            Resource component = new Resource(element);
            Assert.AreEqual(IsmDESVersion, component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(0, component.ExtensibleAttributes.Attributes.Count);

            // ISM:classification in element
            element = ResourceWithoutHeaderElement;
            Util.AddAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, version.IsmNamespace, "U");
            component = new Resource(element);
            Assert.IsFalse(component.SecurityAttributes.Empty);
            Assert.AreEqual(0, component.ExtensibleAttributes.Attributes.Count);

            // ddmsence:confidence in element
            element = ResourceWithoutHeaderElement;
            Util.AddAttribute(element, "ddmsence", "confidence", "http://ddmsence.urizone.net/", "95");
            component = new Resource(element);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(1, component.ExtensibleAttributes.Attributes.Count);
        }

        [TestMethod]
        public virtual void Test20ExtensibleDataSizes()
        {
            DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
            CreateComponents();

            string ddmsenceNs = "http://ddmsence.urizone.net/";

            //Build a wrapper element with the required namespaces so the attributes will inherit the proper prefix
            XElement element = Util.BuildDDMSElement("TestElement", null);
            element.Add(new XAttribute(XNamespace.Xmlns + PropertyReader.GetPrefix("ism"), version.IsmNamespace));
            element.Add(new XAttribute(XNamespace.Xmlns + "ddmsence", ddmsenceNs));

            // This can be a parameter or an extensible.
            XAttribute icAttribute = new XAttribute(XNamespace.Get(version.IsmNamespace) + "DESVersion", "2");
            element.Add(icAttribute);
            // This can be a securityAttribute or an extensible.
            XAttribute secAttribute = new XAttribute(XNamespace.Get(version.IsmNamespace) + "classification", "U");
            element.Add(secAttribute);
            // This can be an extensible.
            XAttribute uniqueAttribute = new XAttribute(XNamespace.Get(ddmsenceNs) + "classification", "95");
            element.Add(uniqueAttribute);
            List<XAttribute> exAttr = new List<XAttribute>();

            //Extract the attributes from the element (they will now carry their prefix)
            icAttribute = element.Attributes().Where(a => a.Name == XNamespace.Get(version.IsmNamespace) + "DESVersion").FirstOrDefault();
            secAttribute = element.Attributes().Where(a => a.Name == XNamespace.Get(version.IsmNamespace) + "classification").FirstOrDefault();
            uniqueAttribute = element.Attributes().Where(a => a.Name == XNamespace.Get(ddmsenceNs) + "classification").FirstOrDefault();

            // Base Case
            Resource component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null);
            Assert.IsNull(component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(0, component.ExtensibleAttributes.Attributes.Count);

            // icAttribute as parameter, uniqueAttribute as extensibleAttribute
            exAttr.Clear();
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, null, null, new ExtensibleAttributes(exAttr));
            Assert.AreEqual(IsmDESVersion, component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(1, component.ExtensibleAttributes.Attributes.Count);

            // icAttribute and uniqueAttribute as extensibleAttributes
            exAttr.Clear();
            exAttr.Add(new XAttribute(icAttribute));
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, new ExtensibleAttributes(exAttr));
            Assert.IsNull(component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(2, component.ExtensibleAttributes.Attributes.Count);

            // secAttribute as securityAttribute, uniqueAttribute as extensibleAttribute
            exAttr.Clear();
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
            Assert.IsNull(component.IsmDESVersion);
            Assert.IsFalse(component.SecurityAttributes.Empty);
            Assert.AreEqual(1, component.ExtensibleAttributes.Attributes.Count);

            // secAttribute and uniqueAttribute as extensibleAttribute
            exAttr.Clear();
            exAttr.Add(new XAttribute(secAttribute));
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, new ExtensibleAttributes(exAttr));
            Assert.IsNull(component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(2, component.ExtensibleAttributes.Attributes.Count);

            // icAttribute as parameter, secAttribute as securityAttribute, uniqueAttribute as extensibleAttribute
            exAttr.Clear();
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
            Assert.AreEqual(IsmDESVersion, component.IsmDESVersion);
            Assert.IsFalse(component.SecurityAttributes.Empty);
            Assert.AreEqual(1, component.ExtensibleAttributes.Attributes.Count);

            // icAttribute as parameter, secAttribute and uniqueAttribute as extensibleAttributes
            exAttr.Clear();
            exAttr.Add(new XAttribute(secAttribute));
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, null, null, new ExtensibleAttributes(exAttr));
            Assert.AreEqual(IsmDESVersion, component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(2, component.ExtensibleAttributes.Attributes.Count);

            // secAttribute as securityAttribute, icAttribute and uniqueAttribute as extensibleAttributes
            exAttr.Clear();
            exAttr.Add(new XAttribute(icAttribute));
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
            Assert.IsNull(component.IsmDESVersion);
            Assert.IsFalse(component.SecurityAttributes.Empty);
            Assert.AreEqual(2, component.ExtensibleAttributes.Attributes.Count);

            // all three as extensibleAttributes
            exAttr.Clear();
            exAttr.Add(new XAttribute(icAttribute));
            exAttr.Add(new XAttribute(secAttribute));
            exAttr.Add(new XAttribute(uniqueAttribute));
            component = new Resource(TEST_TOP_LEVEL_COMPONENTS, new ExtensibleAttributes(exAttr));
            Assert.IsNull(component.IsmDESVersion);
            Assert.IsTrue(component.SecurityAttributes.Empty);
            Assert.AreEqual(3, component.ExtensibleAttributes.Attributes.Count);
        }

        [TestMethod]
        public virtual void TestExtensibleDataDuplicates()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                //Wrapper element
                XElement element = Util.BuildDDMSElement("TestElement", null);
                element.Add(new XAttribute(XNamespace.Xmlns + PropertyReader.GetPrefix("ism"), version.IsmNamespace));

                // IsmDESVersion in parameter AND extensible.
                try
                {
                    element.Add(new XAttribute(XNamespace.Get(version.IsmNamespace) + "DESVersion", "2"));

                    List<XAttribute> exAttr = new List<XAttribute>();
                    exAttr.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(version.IsmNamespace) + "DESVersion").FirstOrDefault());
                    new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The extensible XAttribute with the name, ISM:DESVersion");
                }

                // NtkDESVersion in parameter AND extensible.
                if (version.IsAtLeast("4.0.1"))
                {
                    try
                    {
                        element.Add(new XAttribute(XNamespace.Xmlns + PropertyReader.GetPrefix("ntk"), version.NtkNamespace));
                    element.Add(new XAttribute(XNamespace.Get(version.NtkNamespace) + "DESVersion", "2"));
                        
                        List<XAttribute> exAttr = new List<XAttribute>();
                        exAttr.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(version.NtkNamespace) + "DESVersion").FirstOrDefault());
                        new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
                        Assert.Fail("Allowed invalid data.");
                    }
                    catch (InvalidDDMSException e)
                    {
                        ExpectMessage(e, "The extensible XAttribute with the name, ntk:DESVersion");
                    }
                }

                // classification in securityAttributes AND extensible.
                try
                {
                    element.Add(new XAttribute(XNamespace.Get(version.IsmNamespace) + "classification", version.IsmNamespace), "U");

                    List<XAttribute> exAttr = new List<XAttribute>();
                    exAttr.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(version.IsmNamespace) + "classification").FirstOrDefault());
                    new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The extensible XAttribute with the name, ISM:classification");
                }
            }
        }

        [TestMethod]
        public virtual void TestExtensibleElementElementConstructor()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();

                ExtensibleElement component = new ExtensibleElement(ExtensibleElementTest.FixtureElement);
                XElement element = ResourceWithoutBodyElement;
                if (version.IsAtLeast("4.0.1"))
                {
                    element.Add(MetacardInfoTest.Fixture.ElementCopy);
                }
                element.Add(IdentifierTest.Fixture.ElementCopy);
                element.Add(TitleTest.Fixture.ElementCopy);
                element.Add(CreatorTest.Fixture.ElementCopy);
                element.Add(SubjectCoverageTest.Fixture.ElementCopy);
                element.Add(SecurityTest.Fixture.ElementCopy);
                element.Add(component.ElementCopy);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void TestExtensibleElementOutput()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            CreateComponents();
            ExtensibleElement component = new ExtensibleElement(ExtensibleElementTest.FixtureElement);

            List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
            components.Add(component);
            Resource resource = GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
            Assert.IsTrue(resource.ToHTML().IndexOf(BuildOutput(true, "extensible.layer", "true")) != -1);
            Assert.IsTrue(resource.ToText().IndexOf(BuildOutput(false, "extensible.layer", "true")) != -1);
        }

        [TestMethod]
        public virtual void TestWrongVersionExtensibleElementAllowed()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            ExtensibleElement component = new ExtensibleElement(ExtensibleElementTest.FixtureElement);
            DDMSVersion.SetCurrentVersion("3.0");
            CreateComponents();

            List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
            components.Add(component);
            GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
        }

        [TestMethod]
        public virtual void Test20TooManyExtensibleElements()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            CreateComponents();

            List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
            components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
            components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
            GetInstance("Only 1 extensible element is allowed in DDMS 2.0.", components, null, null, null, null, null);
        }

        [TestMethod]
        public virtual void TestAfter20TooManyExtensibleElements()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            CreateComponents();

            List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
            components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
            components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
            GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
        }

        [TestMethod]
        public virtual void Test20DeclassManualReviewAttribute()
        {
            DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
            CreateComponents();
            string ismNamespace = version.IsmNamespace;

            XElement element = ResourceWithoutHeaderElement;
            Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME, ismNamespace, "true");
            SecurityAttributesTest.Fixture.AddTo(element);
            Resource resource = GetInstance(SUCCESS, element);

            // ISM:declassManualReview should not get picked up as an extensible XAttribute
            Assert.AreEqual(0, resource.ExtensibleAttributes.Attributes.Count);
        }

        [TestMethod]
        public virtual void TestRelatedResourcesMediation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();
                if (version.IsAtLeast("4.0.1"))
                {
                    continue;
                }

                XElement element = ResourceWithMultipleRelated;
                Resource resource = GetInstance(SUCCESS, element);
                Assert.AreEqual(4, resource.RelatedResources.Count);
                Assert.AreEqual("http://en.wikipedia.org/wiki/Tank1", resource.RelatedResources[0].Value);
                Assert.AreEqual("http://en.wikipedia.org/wiki/Tank2", resource.RelatedResources[1].Value);
                Assert.AreEqual("http://en.wikipedia.org/wiki/Tank3", resource.RelatedResources[2].Value);
                Assert.AreEqual("http://en.wikipedia.org/wiki/Tank4", resource.RelatedResources[3].Value);
            }
        }

        [TestMethod]
        public virtual void TestOrderConstraints()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                CreateComponents();
                if (!version.IsAtLeast("4.0.1"))
                {
                    continue;
                }

                // Valid orders
                List<IDDMSComponent> components = new List<IDDMSComponent>(TEST_TOP_LEVEL_COMPONENTS);
                components.Add(SubjectCoverageTest.GetFixture(1));
                components.Add(GeospatialCoverageTest.GetFixture(2));
                components.Add(SubjectCoverageTest.GetFixture(3));
                GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // Duplicate orders
                components = new List<IDDMSComponent>(TEST_TOP_LEVEL_COMPONENTS);
                components.Add(SubjectCoverageTest.GetFixture(1));
                components.Add(GeospatialCoverageTest.GetFixture(1));
                components.Add(SubjectCoverageTest.GetFixture(3));
                GetInstance("The ddms:order attributes throughout this resource", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

                // Skipped orders
                components = new List<IDDMSComponent>(TEST_TOP_LEVEL_COMPONENTS);
                components.Add(SubjectCoverageTest.GetFixture(1));
                components.Add(GeospatialCoverageTest.GetFixture(3));
                components.Add(SubjectCoverageTest.GetFixture(4));
                GetInstance("The ddms:order attributes throughout this resource", components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
            }
        }

        [TestMethod]
        public virtual void TestConstructorChaining()
        {
            // DDMS 2.0
            DDMSVersion.SetCurrentVersion("2.0");
            CreateComponents();
            Resource resource = new Resource(TEST_TOP_LEVEL_COMPONENTS, null);
            Resource fullResource = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, null, null, null);
            Assert.AreEqual(resource, fullResource);

            // DDMS 3.0
            DDMSVersion.SetCurrentVersion("3.0");
            CreateComponents();
            resource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, IsmDESVersion, SecurityAttributesTest.Fixture, null);
            fullResource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, null, SecurityAttributesTest.Fixture, null, null);
            Assert.AreEqual(resource, fullResource);

            // DDMS 3.1
            DDMSVersion.SetCurrentVersion("3.1");
            CreateComponents();
            resource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, TEST_COMPLIES_WITH, IsmDESVersion, SecurityAttributesTest.Fixture, null);
            fullResource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, TEST_COMPLIES_WITH, IsmDESVersion, null, SecurityAttributesTest.Fixture, null, null);
            Assert.AreEqual(resource, fullResource);
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Resource.Builder builder = new Resource.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                // Equality with ExtensibleElement
                builder.ExtensibleElements.Add(new ExtensibleElement.Builder());
                builder.ExtensibleElements[0].Xml = "<ddmsence:extension xmlns:ddmsence=\"http://ddmsence.urizone.net/\">" + "This is an extensible element.</ddmsence:extension>";
                component = (Resource)builder.Commit();
                builder = new Resource.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                
                Resource.Builder builder = new Resource.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Identifiers.Add(new Identifier.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Titles.Add(new Title.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Subtitles.Add(new Subtitle.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Languages.Add(new Language.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Sources.Add(new Source.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Types.Add(new Type.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Creators.Add(new Creator.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Contributors.Add(new Contributor.Builder());
                Assert.IsTrue(builder.Empty);
                builder.Publishers.Add(new Publisher.Builder());
                Assert.IsTrue(builder.Empty);
                builder.PointOfContacts.Add(new PointOfContact.Builder());
                Assert.IsTrue(builder.Empty);
                Assert.AreEqual(4, builder.Producers.Count);
                builder.VirtualCoverages.Add(new VirtualCoverage.Builder());
                Assert.IsTrue(builder.Empty);
                builder.TemporalCoverages.Add(new TemporalCoverage.Builder());
                Assert.IsTrue(builder.Empty);
                builder.GeospatialCoverages.Add(new GeospatialCoverage.Builder());
                Assert.IsTrue(builder.Empty);
                builder.RelatedResources.Add(new RelatedResource.Builder());
                Assert.IsTrue(builder.Empty);
                builder.ExtensibleElements.Add(new ExtensibleElement.Builder());
                Assert.IsTrue(builder.Empty);
                builder.ExtensibleElements.Add(new ExtensibleElement.Builder());
                builder.ExtensibleElements[0].Xml = "InvalidXml";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Resource.Builder builder = new Resource.Builder();
                builder.CreateDate = TEST_CREATE_DATE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least 1 identifier is required.");
                }
                // Successful cases covered in-depth below.
            }
        }

        [TestMethod]
        public virtual void TestBuild20Commit30()
        {
            // Version during building should be 100% irrelevant
            DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
            Resource.Builder builder = new Resource.Builder();
            builder.ResourceElement = TEST_RESOURCE_ELEMENT;
            builder.CreateDate = TEST_CREATE_DATE;
            builder.IsmDESVersion = IsmDESVersion;
            builder.SecurityAttributes.Classification = "U";
            builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");

            builder.Identifiers.Add(new Identifier.Builder());
            builder.Identifiers[0].Qualifier = "testQualifier";
            builder.Identifiers[0].Value = "testValue";
            builder.Titles.Add(new Title.Builder());
            builder.Titles[0].Value = "testTitle";
            builder.Titles[0].SecurityAttributes.Classification = "U";
            builder.Titles[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
            builder.Creators.Add(new Creator.Builder());
            builder.Creators[0].EntityType = Organization.GetName(version);
            builder.Creators[0].Organization.Names = Util.GetXsListAsList("testName");
            builder.SubjectCoverages.Add(new DDMSense.DDMS.Summary.SubjectCoverage.Builder());
            builder.SubjectCoverages[0].Keywords.Add(new Keyword.Builder());
            builder.SubjectCoverages[0].Keywords[0].Value = "keyword";
            builder.Security.SecurityAttributes.Classification = "U";
            builder.Security.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
            DDMSVersion.SetCurrentVersion("3.0");
            builder.Commit();

            // Using a 2.0-specific value
            builder.Security.SecurityAttributes.Classification = "NS-S";
            try
            {
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "NS-S is not a valid enumeration token");
            }
        }

        [TestMethod]
        public virtual void TestBuild30Commit20()
        {
            // Version during building should be 100% irrelevant
            DDMSVersion version = DDMSVersion.SetCurrentVersion("3.0");
            Resource.Builder builder = new Resource.Builder();
            builder.ResourceElement = TEST_RESOURCE_ELEMENT;
            builder.IsmDESVersion = IsmDESVersion;
            builder.SecurityAttributes.Classification = "U";
            builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");

            builder.Identifiers.Add(new Identifier.Builder());
            builder.Identifiers[0].Qualifier = "testQualifier";
            builder.Identifiers[0].Value = "testValue";
            builder.Titles.Add(new Title.Builder());
            builder.Titles[0].Value = "testTitle";
            builder.Titles[0].SecurityAttributes.Classification = "U";
            builder.Titles[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
            builder.Creators.Add(new Creator.Builder());
            builder.Creators[0].EntityType = Organization.GetName(version);
            builder.Creators[0].Organization.Names = Util.GetXsListAsList("testName");
            builder.SubjectCoverages.Add(new DDMSense.DDMS.Summary.SubjectCoverage.Builder());
            builder.SubjectCoverages[0].Keywords.Add(new Keyword.Builder());
            builder.SubjectCoverages[0].Keywords[0].Value = "keyword";
            builder.Security.SecurityAttributes.Classification = "U";
            builder.Security.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");

            DDMSVersion.SetCurrentVersion("2.0");
            builder.Commit();

            // Using a 3.0-specific value
            builder.SubjectCoverages[0].SecurityAttributes.Classification = "U";
            builder.SubjectCoverages[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
            try
            {
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void TestLoad30Commit20()
        {
            Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("3.0")));

            // Direct mapping works
            DDMSVersion.SetCurrentVersion("3.0");
            builder.Commit();

            // Transform back to 2.0 fails on 3.0-specific fields
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The Unknown element cannot be used");
            }

            // Wiping of 3.0-specific fields works
            builder.PointOfContacts.Clear();
            builder.Commit();
        }

        [TestMethod]
        public virtual void TestLoad20Commit30()
        {
            Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("2.0")));

            // Direct mapping works
            DDMSVersion.SetCurrentVersion("2.0");
            builder.Commit();

            // Transform up to 3.0 fails on 3.0-specific fields
            DDMSVersion.SetCurrentVersion("3.0");
            try
            {
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "resourceElement is required.");
            }

            // Adding 3.0-specific fields works
            builder.ResourceElement = TEST_RESOURCE_ELEMENT;
            builder.CreateDate = TEST_CREATE_DATE;
            builder.IsmDESVersion = IsmDESVersion;
            builder.SecurityAttributes.Classification = "U";
            builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
            builder.Commit();
        }

        [TestMethod]
        public virtual void TestLoad30Commit31()
        {
            Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("3.0")));

            // Direct mapping works
            DDMSVersion.SetCurrentVersion("3.0");
            builder.Commit();

            // Transform up to 3.1 fails on 3.0-specific fields
            DDMSVersion.SetCurrentVersion("3.1");
            try
            {
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "nu.xom.ValidityException: cvc-XAttribute.4: The value '2' of XAttribute 'ISM:DESVersion'");
            }

            // Adding 3.1-specific fields works
            builder.IsmDESVersion = Convert.ToInt32(5);
            builder.Commit();
        }

        [TestMethod]
        public virtual void TestLoad31Commit40()
        {
            Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("3.1")));

            // Direct mapping works
            DDMSVersion.SetCurrentVersion("3.1");
            builder.Commit();

            // Transform up to 4.0.1 fails on 3.1-specific fields
            DDMSVersion.SetCurrentVersion("4.0.1");
            try
            {
                builder.Commit();
                Assert.Fail("Builder allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "ntk:DESVersion is required.");
            }

            // Adding 4.0.1-specific fields works
            builder.NtkDESVersion = Convert.ToInt32(7);
            builder.IsmDESVersion = Convert.ToInt32(9);
            builder.MetacardInfo.Identifiers.Add(new Identifier.Builder());
            builder.MetacardInfo.Identifiers[0].Qualifier = "qualifier";
            builder.MetacardInfo.Identifiers[0].Value = "value";
            builder.MetacardInfo.Dates.Created = "2011-09-25";
            builder.MetacardInfo.Publishers.Add(new Publisher.Builder());
            builder.MetacardInfo.Publishers[0].EntityType = "organization";
            builder.MetacardInfo.Publishers[0].Organization.Names = Util.GetXsListAsList("DISA");
            builder.Commit();
        }

        [TestMethod]
        public virtual void TestBuilderSerialization()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));

                Resource.Builder builder = new Resource.Builder(component);
                // TODO - Find .NET version of this code
                //ByteArrayOutputStream @out = new ByteArrayOutputStream();
                //ObjectOutputStream oos = new ObjectOutputStream(@out);
                //oos.writeObject(builder);
                //oos.close();
                //sbyte[] serialized = @out.toByteArray();
                //Assert.IsTrue(serialized.Length > 0);

                //ObjectInputStream ois = new ObjectInputStream(new ByteArrayInputStream(serialized));
                //Resource.Builder unserializedBuilder = (Resource.Builder) ois.readObject();
                //Assert.AreEqual(component, unserializedBuilder.Commit());
            }
        }
    }
}