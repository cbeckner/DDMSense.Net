using System;
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
namespace buri.ddmsence.ddms {


	using Attribute = nu.xom.Attribute;
	using Element = nu.xom.Element;
	using ExtensibleAttributes = buri.ddmsence.ddms.extensible.ExtensibleAttributes;
	using ExtensibleAttributesTest = buri.ddmsence.ddms.extensible.ExtensibleAttributesTest;
	using ExtensibleElement = buri.ddmsence.ddms.extensible.ExtensibleElement;
	using ExtensibleElementTest = buri.ddmsence.ddms.extensible.ExtensibleElementTest;
	using Extent = buri.ddmsence.ddms.format.Extent;
	using Format = buri.ddmsence.ddms.format.Format;
	using FormatTest = buri.ddmsence.ddms.format.FormatTest;
	using MetacardInfoTest = buri.ddmsence.ddms.metacard.MetacardInfoTest;
	using Contributor = buri.ddmsence.ddms.resource.Contributor;
	using ContributorTest = buri.ddmsence.ddms.resource.ContributorTest;
	using Creator = buri.ddmsence.ddms.resource.Creator;
	using CreatorTest = buri.ddmsence.ddms.resource.CreatorTest;
	using DatesTest = buri.ddmsence.ddms.resource.DatesTest;
	using Identifier = buri.ddmsence.ddms.resource.Identifier;
	using IdentifierTest = buri.ddmsence.ddms.resource.IdentifierTest;
	using Language = buri.ddmsence.ddms.resource.Language;
	using LanguageTest = buri.ddmsence.ddms.resource.LanguageTest;
	using Organization = buri.ddmsence.ddms.resource.Organization;
	using Person = buri.ddmsence.ddms.resource.Person;
	using PointOfContact = buri.ddmsence.ddms.resource.PointOfContact;
	using PointOfContactTest = buri.ddmsence.ddms.resource.PointOfContactTest;
	using Publisher = buri.ddmsence.ddms.resource.Publisher;
	using PublisherTest = buri.ddmsence.ddms.resource.PublisherTest;
	using ResourceManagementTest = buri.ddmsence.ddms.resource.ResourceManagementTest;
	using RightsTest = buri.ddmsence.ddms.resource.RightsTest;
	using Service = buri.ddmsence.ddms.resource.Service;
	using Source = buri.ddmsence.ddms.resource.Source;
	using SourceTest = buri.ddmsence.ddms.resource.SourceTest;
	using Subtitle = buri.ddmsence.ddms.resource.Subtitle;
	using SubtitleTest = buri.ddmsence.ddms.resource.SubtitleTest;
	using Title = buri.ddmsence.ddms.resource.Title;
	using TitleTest = buri.ddmsence.ddms.resource.TitleTest;
	using Type = buri.ddmsence.ddms.resource.Type;
	using TypeTest = buri.ddmsence.ddms.resource.TypeTest;
	using Unknown = buri.ddmsence.ddms.resource.Unknown;
	using SecurityTest = buri.ddmsence.ddms.security.SecurityTest;
	using NoticeAttributes = buri.ddmsence.ddms.security.ism.NoticeAttributes;
	using NoticeAttributesTest = buri.ddmsence.ddms.security.ism.NoticeAttributesTest;
	using SecurityAttributes = buri.ddmsence.ddms.security.ism.SecurityAttributes;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DescriptionTest = buri.ddmsence.ddms.summary.DescriptionTest;
	using GeospatialCoverage = buri.ddmsence.ddms.summary.GeospatialCoverage;
	using GeospatialCoverageTest = buri.ddmsence.ddms.summary.GeospatialCoverageTest;
	using Keyword = buri.ddmsence.ddms.summary.Keyword;
	using Link = buri.ddmsence.ddms.summary.Link;
	using PostalAddress = buri.ddmsence.ddms.summary.PostalAddress;
	using RelatedResource = buri.ddmsence.ddms.summary.RelatedResource;
	using RelatedResourceTest = buri.ddmsence.ddms.summary.RelatedResourceTest;
	using SubjectCoverageTest = buri.ddmsence.ddms.summary.SubjectCoverageTest;
	using TemporalCoverage = buri.ddmsence.ddms.summary.TemporalCoverage;
	using TemporalCoverageTest = buri.ddmsence.ddms.summary.TemporalCoverageTest;
	using VirtualCoverage = buri.ddmsence.ddms.summary.VirtualCoverage;
	using VirtualCoverageTest = buri.ddmsence.ddms.summary.VirtualCoverageTest;
	using XLinkAttributes = buri.ddmsence.ddms.summary.xlink.XLinkAttributes;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:resource elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class ResourceTest : AbstractBaseTestCase {
		private IList<IDDMSComponent> TEST_TOP_LEVEL_COMPONENTS;
		private IList<IDDMSComponent> TEST_NO_OPTIONAL_COMPONENTS;

		private static readonly bool? TEST_RESOURCE_ELEMENT = true;
		private const string TEST_CREATE_DATE = "2010-01-21";
		private static readonly IList<string> TEST_COMPLIES_WITH = Util.getXsListAsList("DoD5230.24");

		/// <summary>
		/// Constructor
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ResourceTest() throws InvalidDDMSException
		public ResourceTest() : base("resource.xml") {
		}

		/// <summary>
		/// Regenerates all the components needed in a Resource
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void createComponents() throws InvalidDDMSException
		private void CreateComponents() {
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
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private nu.xom.Element getResourceWithoutHeaderElement() throws InvalidDDMSException
		private Element ResourceWithoutHeaderElement {
			get {
				Element element = Util.buildDDMSElement(Resource.getName(DDMSVersion.CurrentVersion), null);
				if (DDMSVersion.CurrentVersion.isAtLeast("4.0.1")) {
					element.appendChild(MetacardInfoTest.Fixture.XOMElementCopy);
				}
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				return (element);
			}
		}

		/// <summary>
		/// Creates a stub resource element that is otherwise correct, but leaves resource components out.
		/// </summary>
		/// <returns> the element </returns>
		/// <exception cref="InvalidDDMSException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private nu.xom.Element getResourceWithoutBodyElement() throws InvalidDDMSException
		private Element ResourceWithoutBodyElement {
			get {
				DDMSVersion version = DDMSVersion.CurrentVersion;
				string ismPrefix = PropertyReader.getPrefix("ism");
				string ismNamespace = version.IsmNamespace;
				string ntkPrefix = PropertyReader.getPrefix("ntk");
				string ntkNamespace = version.NtkNamespace;
    
				Element element = Util.buildDDMSElement(Resource.getName(version), null);
				Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
				Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
				Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
				if (version.isAtLeast("4.0.1")) {
					Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
				}
				SecurityAttributesTest.Fixture.addTo(element);
				return (element);
			}
		}

		/// <summary>
		/// Creates a stub resource element that contains a bunch of pre-DDMS 4.0.1 relatedResources in different
		/// configurations.
		/// </summary>
		/// <returns> the element </returns>
		/// <exception cref="InvalidDDMSException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private nu.xom.Element getResourceWithMultipleRelated() throws InvalidDDMSException
		private Element ResourceWithMultipleRelated {
			get {
				DDMSVersion version = DDMSVersion.CurrentVersion;
				if (version.isAtLeast("4.0.1")) {
					return null;
				}
				string ismPrefix = PropertyReader.getPrefix("ism");
				string ismNamespace = version.IsmNamespace;
    
				Element element = Util.buildDDMSElement(Resource.getName(version), null);
				Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
				Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
				Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
				SecurityAttributesTest.Fixture.addTo(element);
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
    
				Link link = new Link(new XLinkAttributes("http://en.wikipedia.org/wiki/Tank", "role", null, null));
    
				// #1: a ddms:relatedResources containing 1 ddms:RelatedResource
				Element rel1 = Util.buildDDMSElement(RelatedResource.getName(version), null);
				Util.addDDMSAttribute(rel1, "relationship", "http://purl.org/dc/terms/references");
				Element innerElement = Util.buildDDMSElement("RelatedResource", null);
				Util.addDDMSAttribute(innerElement, "qualifier", "http://purl.org/dc/terms/URI");
				Util.addDDMSAttribute(innerElement, "value", "http://en.wikipedia.org/wiki/Tank1");
				innerElement.appendChild(link.XOMElementCopy);
				rel1.appendChild(innerElement);
				element.appendChild(rel1);
    
				// #2: a ddms:relatedResources containing 3 ddms:RelatedResources
				Element rel2 = Util.buildDDMSElement(RelatedResource.getName(version), null);
				Util.addDDMSAttribute(rel2, "relationship", "http://purl.org/dc/terms/references");
				Element innerElement1 = Util.buildDDMSElement("RelatedResource", null);
				Util.addDDMSAttribute(innerElement1, "qualifier", "http://purl.org/dc/terms/URI");
				Util.addDDMSAttribute(innerElement1, "value", "http://en.wikipedia.org/wiki/Tank2");
				innerElement1.appendChild(link.XOMElementCopy);
				Element innerElement2 = Util.buildDDMSElement("RelatedResource", null);
				Util.addDDMSAttribute(innerElement2, "qualifier", "http://purl.org/dc/terms/URI");
				Util.addDDMSAttribute(innerElement2, "value", "http://en.wikipedia.org/wiki/Tank3");
				innerElement2.appendChild(link.XOMElementCopy);
				Element innerElement3 = Util.buildDDMSElement("RelatedResource", null);
				Util.addDDMSAttribute(innerElement3, "qualifier", "http://purl.org/dc/terms/URI");
				Util.addDDMSAttribute(innerElement3, "value", "http://en.wikipedia.org/wiki/Tank4");
				innerElement3.appendChild(link.XOMElementCopy);
				rel2.appendChild(innerElement1);
				rel2.appendChild(innerElement2);
				rel2.appendChild(innerElement3);
				element.appendChild(rel2);
    
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				return (element);
			}
		}

		/// <summary>
		/// Returns an acceptable DESVersion for the version of DDMS
		/// </summary>
		/// <returns> a DESVersion </returns>
		private int? IsmDESVersion {
			get {
				if (!DDMSVersion.CurrentVersion.isAtLeast("3.1")) {
					return (Convert.ToInt32(2));
				}
				if (!DDMSVersion.CurrentVersion.isAtLeast("4.0.1")) {
					return (Convert.ToInt32(5));
				}
				return (Convert.ToInt32(9));
			}
		}

		/// <summary>
		/// Returns an acceptable DESVersion for the version of DDMS
		/// </summary>
		/// <returns> a DESVersion </returns>
		private int? NtkDESVersion {
			get {
				if (!DDMSVersion.CurrentVersion.isAtLeast("4.0.1")) {
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
		private Resource GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Resource component = null;
			try {
				component = new Resource(element);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
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
		/// <param name="resourceElement"> value of the resourceElement attribute (required) </param>
		/// <param name="createDate"> the create date as an xs:date (YYYY-MM-DD) (required) </param>
		/// <param name="compliesWith"> the compliesWith attribute </param>
		/// <param name="ismDESVersion"> the ISM DES Version as an Integer (required) </param>
		/// <param name="ntkDESVersion"> the NTK DES Version as an Integer (required, starting in DDMS 4.0.1) </param>
		/// <returns> a valid object </returns>
		private Resource GetInstance(string message, IList<IDDMSComponent> topLevelComponents, bool? resourceElement, string createDate, IList<string> compliesWiths, int? ismDESVersion, int? ntkDESVersion) {
			bool expectFailure = !Util.isEmpty(message);
			DDMSVersion version = DDMSVersion.CurrentVersion;
			Resource component = null;
			try {
				NoticeAttributes notice = (!version.isAtLeast("4.0.1") ? null : NoticeAttributesTest.Fixture);
				SecurityAttributes attr = (!version.isAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
				component = new Resource(topLevelComponents, resourceElement, createDate, compliesWiths, ismDESVersion, ntkDESVersion, attr, notice, null);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder text = new StringBuilder();
			string resourcePrefix = Resource.getName(version);
			if (version.isAtLeast("3.0")) {
				text.Append(BuildOutput(isHTML, resourcePrefix + ".resourceElement", "true"));
				text.Append(BuildOutput(isHTML, resourcePrefix + ".createDate", "2010-01-21"));
			}
			text.Append(BuildOutput(isHTML, resourcePrefix + ".ism.DESVersion", Convert.ToString(IsmDESVersion)));
			if (version.isAtLeast("4.0.1")) {
				text.Append(BuildOutput(isHTML, resourcePrefix + ".ntk.DESVersion", Convert.ToString(NtkDESVersion)));
			}
			if (version.isAtLeast("3.0")) {
				text.Append(BuildOutput(isHTML, resourcePrefix + ".classification", "U"));
				text.Append(BuildOutput(isHTML, resourcePrefix + ".ownerProducer", "USA"));
			}
			if (version.isAtLeast("4.0.1")) {
				text.Append(BuildOutput(isHTML, resourcePrefix + ".noticeType", "DoD-Dist-B"));
				text.Append(BuildOutput(isHTML, resourcePrefix + ".noticeReason", "noticeReason"));
				text.Append(BuildOutput(isHTML, resourcePrefix + ".noticeDate", "2011-09-15"));
				text.Append(BuildOutput(isHTML, resourcePrefix + ".unregisteredNoticeType", "unregisteredNoticeType"));
				if (version.isAtLeast("4.1")) {
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
			text.Append(BuildOutput(isHTML, "creator.entityType", Organization.getName(version)));
			text.Append(BuildOutput(isHTML, "creator.name", "DISA"));
			text.Append(BuildOutput(isHTML, "publisher.entityType", Person.getName(version)));
			text.Append(BuildOutput(isHTML, "publisher.name", "Brian"));
			text.Append(BuildOutput(isHTML, "publisher.surname", "Uri"));
			text.Append(BuildOutput(isHTML, "contributor.entityType", Service.getName(version)));
			text.Append(BuildOutput(isHTML, "contributor.name", "https://metadata.dod.mil/ebxmlquery/soap"));
			if (version.isAtLeast("3.0")) {
				text.Append(BuildOutput(isHTML, "pointOfContact.entityType", Unknown.getName(version)));
				text.Append(BuildOutput(isHTML, "pointOfContact.name", "UnknownEntity"));
			} else {
				text.Append(BuildOutput(isHTML, "pointOfContact.entityType", Person.getName(version)));
				text.Append(BuildOutput(isHTML, "pointOfContact.name", "Brian"));
				text.Append(BuildOutput(isHTML, "pointOfContact.surname", "Uri"));
			}

			string formatPrefix = (version.isAtLeast("4.0.1") ? "format." : "format.Media.");
			string subjectPrefix = (version.isAtLeast("4.0.1") ? "subjectCoverage." : "subjectCoverage.Subject.");
			string temporalPrefix = (version.isAtLeast("4.0.1") ? "temporalCoverage." : "temporalCoverage.TimePeriod.");
			string geospatialPrefix = version.isAtLeast("4.0.1") ? "geospatialCoverage." : "geospatialCoverage.GeospatialExtent.";
			string relatedPrefix = (version.isAtLeast("4.0.1") ? "relatedResource." : "relatedResources.RelatedResource.");

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

			if (version.isAtLeast("4.0.1")) {
				text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo", "XSLT Transformation to convert DDMS 2.0 to DDMS 3.1."));
				text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo.dateProcessed", "2011-08-19"));
				text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo.classification", "U"));
				text.Append(BuildOutput(isHTML, "resourceManagement.processingInfo.ownerProducer", "USA"));
				text.Append(BuildOutput(isHTML, "resourceManagement.classification", "U"));
				text.Append(BuildOutput(isHTML, "resourceManagement.ownerProducer", "USA"));
			}
			if (version.isAtLeast("3.0")) {
				text.Append(BuildOutput(isHTML, "security.excludeFromRollup", "true"));
			}
			text.Append(BuildOutput(isHTML, "security.classification", "U"));
			text.Append(BuildOutput(isHTML, "security.ownerProducer", "USA"));
			text.Append(BuildOutput(isHTML, "extensible.layer", "false"));
			text.Append(BuildOutput(isHTML, "ddms.generator", "DDMSence " + PropertyReader.getProperty("version")));
			// Output for version will be based upon XML namespace of created resource, not the currently set version.
			text.Append(BuildOutput(isHTML, "ddms.version", DDMSVersion.getVersionForNamespace(version.Namespace).Version));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:").Append(Resource.getName(version)).Append(" ").Append(XmlnsDDMS);
			if (version.isAtLeast("4.0.1")) {
				xml.Append(" ").Append(XmlnsNTK);
			}
			xml.Append(" ").Append(XmlnsISM);
			if (version.isAtLeast("4.0.1")) {
				xml.Append(" ntk:DESVersion=\"").Append(NtkDESVersion).Append("\"");
			}
			if (version.isAtLeast("3.0")) {
				xml.Append(" ISM:resourceElement=\"true\"");
			}
			// Adding DESVersion in DDMS 2.0 allows the namespace declaration to definitely be in the Resource element.
			xml.Append(" ISM:DESVersion=\"").Append(IsmDESVersion).Append("\"");
			if (version.isAtLeast("3.0")) {
				xml.Append(" ISM:createDate=\"2010-01-21\"");
			}
			if (version.isAtLeast("4.0.1")) {
				xml.Append(" ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
				xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
				if (version.isAtLeast("4.1")) {
					xml.Append(" ISM:externalNotice=\"false\"");
				}
			}
			if (version.isAtLeast("3.0")) {
				xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
			}
			xml.Append(">\n");
			if (version.isAtLeast("4.0.1")) {
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
			xml.Append("\t\t<ddms:").Append(Organization.getName(version)).Append(">\n");
			xml.Append("\t\t\t<ddms:name>DISA</ddms:name>\n");
			xml.Append("\t\t</ddms:").Append(Organization.getName(version)).Append(">\t\n");
			xml.Append("\t</ddms:creator>\n");
			xml.Append("\t<ddms:publisher>\n");
			xml.Append("\t\t<ddms:").Append(Person.getName(version)).Append(">\n");
			xml.Append("\t\t\t<ddms:name>Brian</ddms:name>\n");
			xml.Append("\t\t\t<ddms:surname>Uri</ddms:surname>\n");
			xml.Append("\t\t</ddms:").Append(Person.getName(version)).Append(">\t\n");
			xml.Append("\t</ddms:publisher>\n");
			xml.Append("\t<ddms:contributor>\n");
			xml.Append("\t\t<ddms:").Append(Service.getName(version)).Append(">\n");
			xml.Append("\t\t\t<ddms:name>https://metadata.dod.mil/ebxmlquery/soap</ddms:name>\n");
			xml.Append("\t\t</ddms:").Append(Service.getName(version)).Append(">\t\n");
			xml.Append("\t</ddms:contributor>\n");
			xml.Append("\t<ddms:pointOfContact>\n");
			if (version.isAtLeast("3.0")) {
				xml.Append("\t\t<ddms:").Append(Unknown.getName(version)).Append(">\n");
				xml.Append("\t\t\t<ddms:name>UnknownEntity</ddms:name>\n");
				xml.Append("\t\t</ddms:").Append(Unknown.getName(version)).Append(">\t\n");
			} else {
				xml.Append("\t\t<ddms:").Append(Person.getName(version)).Append(">\n");
				xml.Append("\t\t\t<ddms:name>Brian</ddms:name>\n");
				xml.Append("\t\t\t<ddms:surname>Uri</ddms:surname>\n");
				xml.Append("\t\t</ddms:").Append(Person.getName(version)).Append(">\n");
			}
			xml.Append("\t</ddms:pointOfContact>\n");
			xml.Append("\t<ddms:format>\n");
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t\t<ddms:mimeType>text/xml</ddms:mimeType>\n");
			} else {
				xml.Append("\t\t<ddms:Media>\n");
				xml.Append("\t\t\t<ddms:mimeType>text/xml</ddms:mimeType>\n");
				xml.Append("\t\t</ddms:Media>\n");
			}
			xml.Append("\t</ddms:format>\n");
			xml.Append("\t<ddms:subjectCoverage>\n");
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
			} else {
				xml.Append("\t\t<ddms:Subject>\n");
				xml.Append("\t\t\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
				xml.Append("\t\t</ddms:Subject>\n");
			}
			xml.Append("\t</ddms:subjectCoverage>\n");
			xml.Append("\t<ddms:virtualCoverage ddms:address=\"123.456.789.0\" ddms:protocol=\"IP\" />\n");
			xml.Append("\t<ddms:temporalCoverage>\n");
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t\t<ddms:start>1979-09-15</ddms:start>\n");
				xml.Append("\t\t<ddms:end>Not Applicable</ddms:end>\n");
			} else {
				xml.Append("\t\t<ddms:TimePeriod>\n");
				xml.Append("\t\t\t<ddms:start>1979-09-15</ddms:start>\n");
				xml.Append("\t\t\t<ddms:end>Not Applicable</ddms:end>\n");
				xml.Append("\t\t</ddms:TimePeriod>\n");
			}
			xml.Append("\t</ddms:temporalCoverage>\n");
			xml.Append("\t<ddms:geospatialCoverage>\n");
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t\t<ddms:boundingGeometry>\n");
				xml.Append("\t\t\t<gml:Point xmlns:gml=\"").Append(version.GmlNamespace).Append("\" ");
				xml.Append("gml:id=\"IDValue\" srsName=\"http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D\" ");
				xml.Append("srsDimension=\"10\" axisLabels=\"A B C\" uomLabels=\"Meter Meter Meter\">\n");
				xml.Append("\t\t\t\t<gml:pos>32.1 40.1</gml:pos>\n");
				xml.Append("\t\t\t</gml:Point>\n");
				xml.Append("\t\t</ddms:boundingGeometry>\n");
			} else {
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
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t<ddms:relatedResource ddms:relationship=\"http://purl.org/dc/terms/references\" ").Append("ddms:direction=\"outbound\" ddms:qualifier=\"http://purl.org/dc/terms/URI\" ").Append("ddms:value=\"http://en.wikipedia.org/wiki/Tank\">\n");
				xml.Append("\t\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ").Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"role\" />\n");
				xml.Append("\t</ddms:relatedResource>\n");
			} else {
				xml.Append("\t<ddms:relatedResources ddms:relationship=\"http://purl.org/dc/terms/references\" ").Append("ddms:direction=\"outbound\">\n");
				xml.Append("\t\t<ddms:RelatedResource ddms:qualifier=\"http://purl.org/dc/terms/URI\" ").Append("ddms:value=\"http://en.wikipedia.org/wiki/Tank\">\n");
				xml.Append("\t\t\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ").Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"role\" />\n");
				xml.Append("\t\t</ddms:RelatedResource>\n");
				xml.Append("\t</ddms:relatedResources>\n");
			}
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t<ddms:resourceManagement ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append("<ddms:processingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
				xml.Append("ddms:dateProcessed=\"2011-08-19\">");
				xml.Append("XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.</ddms:processingInfo>");
				xml.Append("</ddms:resourceManagement>\n");
			}
			xml.Append("\t<ddms:security ");
			if (version.isAtLeast("3.0")) {
				xml.Append("ISM:excludeFromRollup=\"true\" ");
			}
			xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" />\n");
			xml.Append("</ddms:").Append(Resource.getName(version)).Append(">");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Resource.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = ResourceWithoutBodyElement;
				if (version.isAtLeast("4.0.1")) {
					element.appendChild(MetacardInfoTest.Fixture.XOMElementCopy);
				}
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance(SUCCESS, element);

				// More than 1 subjectCoverage
				if (version.isAtLeast("4.0.1")) {
					element = ResourceWithoutBodyElement;
					element.appendChild(MetacardInfoTest.Fixture.XOMElementCopy);
					element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
					element.appendChild(TitleTest.Fixture.XOMElementCopy);
					element.appendChild(CreatorTest.Fixture.XOMElementCopy);
					element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
					element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
					element.appendChild(SecurityTest.Fixture.XOMElementCopy);
					GetInstance(SUCCESS, element);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CreateComponents();

				// All fields
				GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);

				// No optional fields
				GetInstance(SUCCESS, TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();
				string ismPrefix = PropertyReader.getPrefix("ism");
				string ismNamespace = version.IsmNamespace;
				string ntkPrefix = PropertyReader.getPrefix("ntk");
				string ntkNamespace = version.NtkNamespace;

				if (version.isAtLeast("3.0")) {
					// Missing resourceElement
					Element element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("resourceElement is required.", element);

					// Empty resourceElement
					element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, "");
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("resourceElement is required.", element);

					// Invalid resourceElement
					element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, "aardvark");
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("resourceElement is required.", element);

					// Missing createDate
					element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("createDate is required.", element);

					// Invalid createDate
					element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, "2004");
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("The createDate must be in the xs:date format", element);

					// Missing desVersion
					element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("ISM:DESVersion is required.", element);

					// desVersion not an integer
					element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, "one");
					if (version.isAtLeast("4.0.1")) {
						Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, Convert.ToString(NtkDESVersion));
					}
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("ISM:DESVersion is required", element);
				}
				if (version.isAtLeast("4.0.1")) {
					// NTK desVersion not an integer
					Element element = ResourceWithoutHeaderElement;
					Util.addAttribute(element, ismPrefix, Resource.RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(TEST_RESOURCE_ELEMENT));
					Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, ismNamespace, Convert.ToString(IsmDESVersion));
					Util.addAttribute(element, ismPrefix, Resource.CREATE_DATE_NAME, ismNamespace, TEST_CREATE_DATE);
					Util.addAttribute(element, ntkPrefix, Resource.DES_VERSION_NAME, ntkNamespace, "one");
					SecurityAttributesTest.Fixture.addTo(element);
					GetInstance("ntk:DESVersion is required.", element);
				}

				// At least 1 producer
				Element element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				GetInstance("Exactly 1 security element must exist.", element);

				// At least 1 identifier
				element = ResourceWithoutBodyElement;
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("At least 1 identifier is required.", element);

				// At least 1 title
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("At least 1 title is required.", element);

				// No more than 1 description
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(DescriptionTest.Fixture.XOMElementCopy);
				element.appendChild(DescriptionTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("No more than 1 description element can exist.", element);

				// No more than 1 dates
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(DatesTest.Fixture.XOMElementCopy);
				element.appendChild(DatesTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("No more than 1 dates element can exist.", element);

				// No more than 1 rights
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(RightsTest.Fixture.XOMElementCopy);
				element.appendChild(RightsTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("No more than 1 rights element can exist.", element);

				// No more than 1 formats
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(FormatTest.Fixture.XOMElementCopy);
				element.appendChild(FormatTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("No more than 1 format element can exist.", element);

				// No more than 1 resourceManagement
				if (version.isAtLeast("4.0.1")) {
					element = ResourceWithoutBodyElement;
					element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
					element.appendChild(TitleTest.Fixture.XOMElementCopy);
					element.appendChild(CreatorTest.Fixture.XOMElementCopy);
					element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
					element.appendChild(ResourceManagementTest.Fixture.XOMElementCopy);
					element.appendChild(ResourceManagementTest.Fixture.XOMElementCopy);
					element.appendChild(SecurityTest.Fixture.XOMElementCopy);
					GetInstance("No more than 1 resourceManagement", element);
				}

				// At least 1 subjectCoverage
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				string message = version.isAtLeast("4.0.1") ? "At least 1 subjectCoverage is required." : "Exactly 1 subjectCoverage element must exist.";
				GetInstance(message, element);

				// No more than 1 subjectCoverage
				if (!version.isAtLeast("4.0.1")) {
					element = ResourceWithoutBodyElement;
					element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
					element.appendChild(TitleTest.Fixture.XOMElementCopy);
					element.appendChild(CreatorTest.Fixture.XOMElementCopy);
					element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
					element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
					element.appendChild(SecurityTest.Fixture.XOMElementCopy);
					GetInstance("Exactly 1 subjectCoverage element must exist.", element);
				}

				// At least 1 security
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				GetInstance("Exactly 1 security element must exist.", element);

				// No more than 1 security
				element = ResourceWithoutBodyElement;
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				GetInstance("Extensible elements cannot be defined", element);

				// No top level components
				element = ResourceWithoutBodyElement;
				GetInstance("At least 1 identifier is required.", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				if (version.isAtLeast("3.0")) {
					// Missing createDate
					GetInstance("createDate is required.", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, null, null, IsmDESVersion, NtkDESVersion);

					// Invalid createDate
					GetInstance("The createDate must be in the xs:date format", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, "2001", null, IsmDESVersion, NtkDESVersion);

					// Nonsensical createDate
					GetInstance("The ISM:createDate attribute is not in a valid date format.", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, "notAnXmlDate", null, IsmDESVersion, NtkDESVersion);

					// Missing desVersion
					GetInstance("ISM:DESVersion is required.", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, null, NtkDESVersion);
				}
				if (version.isAtLeast("4.0.1")) {
					// Missing desVersion
					GetInstance("ntk:DESVersion is required", TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, null);
				}

				// At least 1 producer
				IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
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
				string message = version.isAtLeast("4.0.1") ? "At least 1 subjectCoverage is required." : "Exactly 1 subjectCoverage element must exist.";
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));

				// 4.1 ism:Notice used
				if (version.isAtLeast("4.1")) {
					assertEquals(1, component.ValidationWarnings.size());
					string text = "The ISM:externalNotice attribute in this DDMS component";
					string locator = "ddms:resource";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
				}
				// No warnings 
				else {
					assertEquals(0, component.ValidationWarnings.size());
				}

				int countIndex = version.isAtLeast("4.1") ? 1 : 0;

				// Nested warnings
				IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
				components.Add(new Format("test", new Extent("test", ""), "test"));
				component = GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
				assertEquals(countIndex + 1, component.ValidationWarnings.size());

				if (version.isAtLeast("4.1")) {
					string text = "The ISM:externalNotice attribute";
					string locator = "ddms:resource";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
				}
				string resourceName = Resource.getName(version);
				string text = "A qualifier has been set without an accompanying value attribute.";
				string locator = (version.isAtLeast("4.0.1")) ? "ddms:" + resourceName + "/ddms:format/ddms:extent" : "ddms:" + resourceName + "/ddms:format/ddms:Media/ddms:extent";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(countIndex));

				// More nested warnings
				Element element = Util.buildDDMSElement(PostalAddress.getName(version), null);
				PostalAddress address = new PostalAddress(element);
				components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
				components.Add(new GeospatialCoverage(null, null, null, address, null, null, null, null));
				component = GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
				assertEquals(countIndex + 1, component.ValidationWarnings.size());

				if (version.isAtLeast("4.1")) {
					text = "The ISM:externalNotice attribute";
					locator = "ddms:resource";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
				}
				text = "A completely empty ddms:postalAddress element was found.";
				locator = (version.isAtLeast("4.0.1")) ? "ddms:" + resourceName + "/ddms:geospatialCoverage/ddms:postalAddress" : "ddms:" + resourceName + "/ddms:geospatialCoverage/ddms:GeospatialExtent/ddms:postalAddress";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(countIndex));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				Resource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Resource dataComponent = (!version.isAtLeast("3.0") ? GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion) : GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion));
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				Resource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Resource dataComponent;
				// resourceElement is fixed starting in 3.1.
				if (!version.isAtLeast("3.1")) {
					dataComponent = GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, false, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
					assertFalse(elementComponent.Equals(dataComponent));
				}

				dataComponent = GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, "1999-10-10", null, IsmDESVersion, NtkDESVersion);
				assertFalse(elementComponent.Equals(dataComponent));

				// Can only use alternate DESVersions in early DDMS versions
				if (!version.isAtLeast("3.1")) {
					dataComponent = GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, Convert.ToInt32(1), NtkDESVersion);
					assertFalse(elementComponent.Equals(dataComponent));
				}

				dataComponent = GetInstance(SUCCESS, TEST_NO_OPTIONAL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();
				Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = (!version.isAtLeast("3.0") ? GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion) : GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));

				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = (!version.isAtLeast("3.0") ? GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion) : GetInstance(SUCCESS, TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion));
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionCompliesWith() throws InvalidDDMSException
		public virtual void TestWrongVersionCompliesWith() {
			DDMSVersion.CurrentVersion = "3.0";
			CreateComponents();

			GetInstance("The compliesWith attribute cannot be used", TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, TEST_COMPLIES_WITH, IsmDESVersion, NtkDESVersion);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws InvalidDDMSException
		public virtual void TestWrongVersionSecurityAttributes() {
			DDMSVersion.CurrentVersion = "2.0";
			CreateComponents();
			// Security attributes do not exist in 2.0
			new Resource(TEST_TOP_LEVEL_COMPONENTS, null);

			// But attributes can still be used.
			new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, null, SecurityAttributesTest.Fixture, null, null);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleSuccess() throws InvalidDDMSException
		public virtual void TestExtensibleSuccess() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				// Extensible attribute added
				ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
				if (!version.isAtLeast("3.0")) {
					new Resource(TEST_TOP_LEVEL_COMPONENTS, attr);
				} else {
					new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, attr);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test20ExtensibleElementSize() throws InvalidDDMSException
		public virtual void Test20ExtensibleElementSize() {
			DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
			CreateComponents();
			string ismPrefix = PropertyReader.getPrefix("ism");

			// ISM:DESVersion in element
			Element element = ResourceWithoutHeaderElement;
			Util.addAttribute(element, ismPrefix, Resource.DES_VERSION_NAME, version.IsmNamespace, Convert.ToString(IsmDESVersion));
			Resource component = new Resource(element);
			assertEquals(IsmDESVersion, component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(0, component.ExtensibleAttributes.Attributes.size());

			// ISM:classification in element
			element = ResourceWithoutHeaderElement;
			Util.addAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, version.IsmNamespace, "U");
			component = new Resource(element);
			assertFalse(component.SecurityAttributes.Empty);
			assertEquals(0, component.ExtensibleAttributes.Attributes.size());

			// ddmsence:confidence in element
			element = ResourceWithoutHeaderElement;
			Util.addAttribute(element, "ddmsence", "confidence", "http://ddmsence.urizone.net/", "95");
			component = new Resource(element);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(1, component.ExtensibleAttributes.Attributes.size());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test20ExtensibleDataSizes() throws InvalidDDMSException
		public virtual void Test20ExtensibleDataSizes() {
			DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
			CreateComponents();

			// This can be a parameter or an extensible.
			Attribute icAttribute = new Attribute("ISM:DESVersion", version.IsmNamespace, "2");
			// This can be a securityAttribute or an extensible.
			Attribute secAttribute = new Attribute("ISM:classification", version.IsmNamespace, "U");
			// This can be an extensible.
			Attribute uniqueAttribute = new Attribute("ddmsence:confidence", "http://ddmsence.urizone.net/", "95");
			IList<Attribute> exAttr = new List<Attribute>();

			// Base Case
			Resource component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null);
			assertNull(component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(0, component.ExtensibleAttributes.Attributes.size());

			// icAttribute as parameter, uniqueAttribute as extensibleAttribute
			exAttr.Clear();
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, null, null, new ExtensibleAttributes(exAttr));
			assertEquals(IsmDESVersion, component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(1, component.ExtensibleAttributes.Attributes.size());

			// icAttribute and uniqueAttribute as extensibleAttributes
			exAttr.Clear();
			exAttr.Add(new Attribute(icAttribute));
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, new ExtensibleAttributes(exAttr));
			assertNull(component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(2, component.ExtensibleAttributes.Attributes.size());

			// secAttribute as securityAttribute, uniqueAttribute as extensibleAttribute
			exAttr.Clear();
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
			assertNull(component.IsmDESVersion);
			assertFalse(component.SecurityAttributes.Empty);
			assertEquals(1, component.ExtensibleAttributes.Attributes.size());

			// secAttribute and uniqueAttribute as extensibleAttribute
			exAttr.Clear();
			exAttr.Add(new Attribute(secAttribute));
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, new ExtensibleAttributes(exAttr));
			assertNull(component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(2, component.ExtensibleAttributes.Attributes.size());

			// icAttribute as parameter, secAttribute as securityAttribute, uniqueAttribute as extensibleAttribute
			exAttr.Clear();
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
			assertEquals(IsmDESVersion, component.IsmDESVersion);
			assertFalse(component.SecurityAttributes.Empty);
			assertEquals(1, component.ExtensibleAttributes.Attributes.size());

			// icAttribute as parameter, secAttribute and uniqueAttribute as extensibleAttributes
			exAttr.Clear();
			exAttr.Add(new Attribute(secAttribute));
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, null, null, new ExtensibleAttributes(exAttr));
			assertEquals(IsmDESVersion, component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(2, component.ExtensibleAttributes.Attributes.size());

			// secAttribute as securityAttribute, icAttribute and uniqueAttribute as extensibleAttributes
			exAttr.Clear();
			exAttr.Add(new Attribute(icAttribute));
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
			assertNull(component.IsmDESVersion);
			assertFalse(component.SecurityAttributes.Empty);
			assertEquals(2, component.ExtensibleAttributes.Attributes.size());

			// all three as extensibleAttributes
			exAttr.Clear();
			exAttr.Add(new Attribute(icAttribute));
			exAttr.Add(new Attribute(secAttribute));
			exAttr.Add(new Attribute(uniqueAttribute));
			component = new Resource(TEST_TOP_LEVEL_COMPONENTS, new ExtensibleAttributes(exAttr));
			assertNull(component.IsmDESVersion);
			assertTrue(component.SecurityAttributes.Empty);
			assertEquals(3, component.ExtensibleAttributes.Attributes.size());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleDataDuplicates() throws InvalidDDMSException
		public virtual void TestExtensibleDataDuplicates() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				// IsmDESVersion in parameter AND extensible.
				try {
					IList<Attribute> exAttr = new List<Attribute>();
					exAttr.Add(new Attribute("ISM:DESVersion", version.IsmNamespace, "2"));
					new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The extensible attribute with the name, ISM:DESVersion");
				}

				// NtkDESVersion in parameter AND extensible.
				if (version.isAtLeast("4.0.1")) {
					try {
						IList<Attribute> exAttr = new List<Attribute>();
						exAttr.Add(new Attribute("ntk:DESVersion", version.NtkNamespace, "2"));
						new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, IsmDESVersion, NtkDESVersion, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
						fail("Allowed invalid data.");
					} catch (InvalidDDMSException e) {
						ExpectMessage(e, "The extensible attribute with the name, ntk:DESVersion");
					}
				}

				// classification in securityAttributes AND extensible.
				try {
					IList<Attribute> exAttr = new List<Attribute>();
					exAttr.Add(new Attribute("ISM:classification", version.IsmNamespace, "U"));
					new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, SecurityAttributesTest.Fixture, null, new ExtensibleAttributes(exAttr));
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The extensible attribute with the name, ISM:classification");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleElementElementConstructor() throws InvalidDDMSException
		public virtual void TestExtensibleElementElementConstructor() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();

				ExtensibleElement component = new ExtensibleElement(ExtensibleElementTest.FixtureElement);
				Element element = ResourceWithoutBodyElement;
				if (version.isAtLeast("4.0.1")) {
					element.appendChild(MetacardInfoTest.Fixture.XOMElementCopy);
				}
				element.appendChild(IdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(TitleTest.Fixture.XOMElementCopy);
				element.appendChild(CreatorTest.Fixture.XOMElementCopy);
				element.appendChild(SubjectCoverageTest.Fixture.XOMElementCopy);
				element.appendChild(SecurityTest.Fixture.XOMElementCopy);
				element.appendChild(component.XOMElementCopy);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleElementOutput() throws InvalidDDMSException
		public virtual void TestExtensibleElementOutput() {
			DDMSVersion.CurrentVersion = "3.0";
			CreateComponents();
			ExtensibleElement component = new ExtensibleElement(ExtensibleElementTest.FixtureElement);

			IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
			components.Add(component);
			Resource resource = GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
			assertTrue(resource.toHTML().IndexOf(BuildOutput(true, "extensible.layer", "true")) != -1);
			assertTrue(resource.toText().IndexOf(BuildOutput(false, "extensible.layer", "true")) != -1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionExtensibleElementAllowed() throws InvalidDDMSException
		public virtual void TestWrongVersionExtensibleElementAllowed() {
			DDMSVersion.CurrentVersion = "2.0";
			ExtensibleElement component = new ExtensibleElement(ExtensibleElementTest.FixtureElement);
			DDMSVersion.CurrentVersion = "3.0";
			CreateComponents();

			IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
			components.Add(component);
			GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test20TooManyExtensibleElements() throws InvalidDDMSException
		public virtual void Test20TooManyExtensibleElements() {
			DDMSVersion.CurrentVersion = "2.0";
			CreateComponents();

			IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
			components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
			components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
			GetInstance("Only 1 extensible element is allowed in DDMS 2.0.", components, null, null, null, null, null);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testAfter20TooManyExtensibleElements() throws InvalidDDMSException
		public virtual void TestAfter20TooManyExtensibleElements() {
			DDMSVersion.CurrentVersion = "3.0";
			CreateComponents();

			IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_NO_OPTIONAL_COMPONENTS);
			components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
			components.Add(new ExtensibleElement(ExtensibleElementTest.FixtureElement));
			GetInstance(SUCCESS, components, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, NtkDESVersion);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test20DeclassManualReviewAttribute() throws InvalidDDMSException
		public virtual void Test20DeclassManualReviewAttribute() {
			DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
			CreateComponents();
			string ismNamespace = version.IsmNamespace;

			Element element = ResourceWithoutHeaderElement;
			Util.addAttribute(element, PropertyReader.getPrefix("ism"), SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME, ismNamespace, "true");
			SecurityAttributesTest.Fixture.addTo(element);
			Resource resource = GetInstance(SUCCESS, element);

			// ISM:declassManualReview should not get picked up as an extensible attribute
			assertEquals(0, resource.ExtensibleAttributes.Attributes.size());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRelatedResourcesMediation() throws InvalidDDMSException
		public virtual void TestRelatedResourcesMediation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();
				if (version.isAtLeast("4.0.1")) {
					continue;
				}

				Element element = ResourceWithMultipleRelated;
				Resource resource = GetInstance(SUCCESS, element);
				assertEquals(4, resource.RelatedResources.size());
				assertEquals("http://en.wikipedia.org/wiki/Tank1", resource.RelatedResources.get(0).Value);
				assertEquals("http://en.wikipedia.org/wiki/Tank2", resource.RelatedResources.get(1).Value);
				assertEquals("http://en.wikipedia.org/wiki/Tank3", resource.RelatedResources.get(2).Value);
				assertEquals("http://en.wikipedia.org/wiki/Tank4", resource.RelatedResources.get(3).Value);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testOrderConstraints() throws InvalidDDMSException
		public virtual void TestOrderConstraints() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				CreateComponents();
				if (!version.isAtLeast("4.0.1")) {
					continue;
				}

				// Valid orders
				IList<IDDMSComponent> components = new List<IDDMSComponent>(TEST_TOP_LEVEL_COMPONENTS);
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorChaining() throws InvalidDDMSException
		public virtual void TestConstructorChaining() {
			// DDMS 2.0
			DDMSVersion.CurrentVersion = "2.0";
			CreateComponents();
			Resource resource = new Resource(TEST_TOP_LEVEL_COMPONENTS, null);
			Resource fullResource = new Resource(TEST_TOP_LEVEL_COMPONENTS, null, null, null, null, null, null, null, null);
			assertEquals(resource, fullResource);

			// DDMS 3.0
			DDMSVersion.CurrentVersion = "3.0";
			CreateComponents();
			resource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, IsmDESVersion, SecurityAttributesTest.Fixture, null);
			fullResource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, null, IsmDESVersion, null, SecurityAttributesTest.Fixture, null, null);
			assertEquals(resource, fullResource);

			// DDMS 3.1
			DDMSVersion.CurrentVersion = "3.1";
			CreateComponents();
			resource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, TEST_COMPLIES_WITH, IsmDESVersion, SecurityAttributesTest.Fixture, null);
			fullResource = new Resource(TEST_TOP_LEVEL_COMPONENTS, TEST_RESOURCE_ELEMENT, TEST_CREATE_DATE, TEST_COMPLIES_WITH, IsmDESVersion, null, SecurityAttributesTest.Fixture, null, null);
			assertEquals(resource, fullResource);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Resource.Builder builder = new Resource.Builder(component);
				assertEquals(component, builder.commit());

				// Equality with ExtensibleElement
				builder.ExtensibleElements.add(new ExtensibleElement.Builder());
				builder.ExtensibleElements.get(0).Xml = "<ddmsence:extension xmlns:ddmsence=\"http://ddmsence.urizone.net/\">" + "This is an extensible element.</ddmsence:extension>";
				component = builder.commit();
				builder = new Resource.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Resource.Builder builder = new Resource.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Identifiers.add(new Identifier.Builder());
				assertTrue(builder.Empty);
				builder.Titles.add(new Title.Builder());
				assertTrue(builder.Empty);
				builder.Subtitles.add(new Subtitle.Builder());
				assertTrue(builder.Empty);
				builder.Languages.add(new Language.Builder());
				assertTrue(builder.Empty);
				builder.Sources.add(new Source.Builder());
				assertTrue(builder.Empty);
				builder.Types.add(new Type.Builder());
				assertTrue(builder.Empty);
				builder.Creators.add(new Creator.Builder());
				assertTrue(builder.Empty);
				builder.Contributors.add(new Contributor.Builder());
				assertTrue(builder.Empty);
				builder.Publishers.add(new Publisher.Builder());
				assertTrue(builder.Empty);
				builder.PointOfContacts.add(new PointOfContact.Builder());
				assertTrue(builder.Empty);
				assertEquals(4, builder.Producers.size());
				builder.VirtualCoverages.add(new VirtualCoverage.Builder());
				assertTrue(builder.Empty);
				builder.TemporalCoverages.add(new TemporalCoverage.Builder());
				assertTrue(builder.Empty);
				builder.GeospatialCoverages.add(new GeospatialCoverage.Builder());
				assertTrue(builder.Empty);
				builder.RelatedResources.add(new RelatedResource.Builder());
				assertTrue(builder.Empty);
				builder.ExtensibleElements.add(new ExtensibleElement.Builder());
				assertTrue(builder.Empty);
				builder.ExtensibleElements.get(0).Xml = "InvalidXml";
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Resource.Builder builder = new Resource.Builder();
				builder.CreateDate = TEST_CREATE_DATE;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "At least 1 identifier is required.");
				}
				// Successful cases covered in-depth below.
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Resource.Builder builder = new Resource.Builder();
				assertNotNull(builder.Identifiers.get(1));
				assertNotNull(builder.Titles.get(1));
				assertNotNull(builder.Subtitles.get(1));
				assertNotNull(builder.Languages.get(1));
				assertNotNull(builder.Sources.get(1));
				assertNotNull(builder.Types.get(1));
				assertNotNull(builder.Creators.get(1));
				assertNotNull(builder.Contributors.get(1));
				assertNotNull(builder.Publishers.get(1));
				assertNotNull(builder.PointOfContacts.get(1));
				assertNotNull(builder.VirtualCoverages.get(1));
				assertNotNull(builder.TemporalCoverages.get(1));
				assertNotNull(builder.GeospatialCoverages.get(1));
				assertNotNull(builder.RelatedResources.get(1));
				assertNotNull(builder.ExtensibleElements.get(1));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuild20Commit30() throws InvalidDDMSException
		public virtual void TestBuild20Commit30() {
			// Version during building should be 100% irrelevant
			DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
			Resource.Builder builder = new Resource.Builder();
			builder.ResourceElement = TEST_RESOURCE_ELEMENT;
			builder.CreateDate = TEST_CREATE_DATE;
			builder.IsmDESVersion = IsmDESVersion;
			builder.SecurityAttributes.Classification = "U";
			builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");

			builder.Identifiers.get(0).Qualifier = "testQualifier";
			builder.Identifiers.get(0).Value = "testValue";
			builder.Titles.get(0).Value = "testTitle";
			builder.Titles.get(0).SecurityAttributes.Classification = "U";
			builder.Titles.get(0).SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
			builder.Creators.get(0).EntityType = Organization.getName(version);
			builder.Creators.get(0).Organization.Names = Util.getXsListAsList("testName");
			builder.SubjectCoverages.get(0).Keywords.get(0).Value = "keyword";
			builder.Security.SecurityAttributes.Classification = "U";
			builder.Security.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
			DDMSVersion.CurrentVersion = "3.0";
			builder.commit();

			// Using a 2.0-specific value
			builder.Security.SecurityAttributes.Classification = "NS-S";
			try {
				builder.commit();
				fail("Builder allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "NS-S is not a valid enumeration token");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuild30Commit20() throws InvalidDDMSException
		public virtual void TestBuild30Commit20() {
			// Version during building should be 100% irrelevant
			DDMSVersion version = DDMSVersion.setCurrentVersion("3.0");
			Resource.Builder builder = new Resource.Builder();
			builder.ResourceElement = TEST_RESOURCE_ELEMENT;
			builder.IsmDESVersion = IsmDESVersion;
			builder.SecurityAttributes.Classification = "U";
			builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");

			builder.Identifiers.get(0).Qualifier = "testQualifier";
			builder.Identifiers.get(0).Value = "testValue";
			builder.Titles.get(0).Value = "testTitle";
			builder.Titles.get(0).SecurityAttributes.Classification = "U";
			builder.Titles.get(0).SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
			builder.Creators.get(0).EntityType = Organization.getName(version);
			builder.Creators.get(0).Organization.Names = Util.getXsListAsList("testName");
			builder.SubjectCoverages.get(0).Keywords.get(0).Value = "keyword";
			builder.Security.SecurityAttributes.Classification = "U";
			builder.Security.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");

			DDMSVersion.CurrentVersion = "2.0";
			builder.commit();

			// Using a 3.0-specific value
			builder.SubjectCoverages.get(0).SecurityAttributes.Classification = "U";
			builder.SubjectCoverages.get(0).SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
			try {
				builder.commit();
				fail("Builder allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Security attributes cannot be applied");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testLoad30Commit20() throws InvalidDDMSException
		public virtual void TestLoad30Commit20() {
			Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("3.0")));

			// Direct mapping works
			DDMSVersion.CurrentVersion = "3.0";
			builder.commit();

			// Transform back to 2.0 fails on 3.0-specific fields
			DDMSVersion.CurrentVersion = "2.0";
			try {
				builder.commit();
				fail("Builder allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The Unknown element cannot be used");
			}

			// Wiping of 3.0-specific fields works
			builder.PointOfContacts.clear();
			builder.commit();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testLoad20Commit30() throws InvalidDDMSException
		public virtual void TestLoad20Commit30() {
			Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("2.0")));

			// Direct mapping works
			DDMSVersion.CurrentVersion = "2.0";
			builder.commit();

			// Transform up to 3.0 fails on 3.0-specific fields
			DDMSVersion.CurrentVersion = "3.0";
			try {
				builder.commit();
				fail("Builder allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "resourceElement is required.");
			}

			// Adding 3.0-specific fields works
			builder.ResourceElement = TEST_RESOURCE_ELEMENT;
			builder.CreateDate = TEST_CREATE_DATE;
			builder.IsmDESVersion = IsmDESVersion;
			builder.SecurityAttributes.Classification = "U";
			builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
			builder.commit();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testLoad30Commit31() throws InvalidDDMSException
		public virtual void TestLoad30Commit31() {
			Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("3.0")));

			// Direct mapping works
			DDMSVersion.CurrentVersion = "3.0";
			builder.commit();

			// Transform up to 3.1 fails on 3.0-specific fields
			DDMSVersion.CurrentVersion = "3.1";
			try {
				builder.commit();
				fail("Builder allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "nu.xom.ValidityException: cvc-attribute.4: The value '2' of attribute 'ISM:DESVersion'");
			}

			// Adding 3.1-specific fields works
			builder.IsmDESVersion = Convert.ToInt32(5);
			builder.commit();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testLoad31Commit40() throws InvalidDDMSException
		public virtual void TestLoad31Commit40() {
			Resource.Builder builder = new Resource.Builder(new Resource(GetValidElement("3.1")));

			// Direct mapping works
			DDMSVersion.CurrentVersion = "3.1";
			builder.commit();

			// Transform up to 4.0.1 fails on 3.1-specific fields
			DDMSVersion.CurrentVersion = "4.0.1";
			try {
				builder.commit();
				fail("Builder allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "ntk:DESVersion is required.");
			}

			// Adding 4.0.1-specific fields works
			builder.NtkDESVersion = Convert.ToInt32(7);
			builder.IsmDESVersion = Convert.ToInt32(9);
			builder.MetacardInfo.Identifiers.get(0).Qualifier = "qualifier";
			builder.MetacardInfo.Identifiers.get(0).Value = "value";
			builder.MetacardInfo.Dates.Created = "2011-09-25";
			builder.MetacardInfo.Publishers.get(0).EntityType = "organization";
			builder.MetacardInfo.Publishers.get(0).Organization.Names = Util.getXsListAsList("DISA");
			builder.commit();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderSerialization() throws Exception
		public virtual void TestBuilderSerialization() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Resource component = GetInstance(SUCCESS, GetValidElement(sVersion));

				Resource.Builder builder = new Resource.Builder(component);
				ByteArrayOutputStream @out = new ByteArrayOutputStream();
				ObjectOutputStream oos = new ObjectOutputStream(@out);
				oos.writeObject(builder);
				oos.close();
				sbyte[] serialized = @out.toByteArray();
				assertTrue(serialized.Length > 0);

				ObjectInputStream ois = new ObjectInputStream(new ByteArrayInputStream(serialized));
				Resource.Builder unserializedBuilder = (Resource.Builder) ois.readObject();
				assertEquals(component, unserializedBuilder.commit());
			}
		}

	}

}