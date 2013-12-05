using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
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
namespace DDMSSense.DDMS {



    using Document = System.Xml.Linq.XDocument;
    using Element = System.Xml.Linq.XElement;
    
    using Node = System.Xml.Linq.XNode;
    using XPathContext = System.Xml.XPath.XPathDocument;
    using XSLException = System.Xml.Xsl.XsltException;
    using XSLTransform = System.Xml.Xsl.XslTransform;
	using ExtensibleAttributes = DDMSSense.DDMS.Extensible.ExtensibleAttributes;
	using ExtensibleElement = DDMSSense.DDMS.Extensible.ExtensibleElement;
	using Format = DDMSSense.DDMS.FormatElements.Format;
	using MetacardInfo = DDMSSense.DDMS.Metacard.MetacardInfo;
    using Contributor = DDMSSense.DDMS.ResourceElements.Contributor;
    using Creator = DDMSSense.DDMS.ResourceElements.Creator;
    using Dates = DDMSSense.DDMS.ResourceElements.Dates;
    using Identifier = DDMSSense.DDMS.ResourceElements.Identifier;
    using Language = DDMSSense.DDMS.ResourceElements.Language;
    using PointOfContact = DDMSSense.DDMS.ResourceElements.PointOfContact;
    using Publisher = DDMSSense.DDMS.ResourceElements.Publisher;
	using ResourceManagement = DDMSSense.DDMS.ResourceElements.ResourceManagement;
    using Rights = DDMSSense.DDMS.ResourceElements.Rights;
    using Source = DDMSSense.DDMS.ResourceElements.Source;
    using Subtitle = DDMSSense.DDMS.ResourceElements.Subtitle;
    using Title = DDMSSense.DDMS.ResourceElements.Title;
    using Type = DDMSSense.DDMS.ResourceElements.Type;
	using Security = DDMSSense.DDMS.SecurityElements.Security;
	using ISMVocabulary = DDMSSense.DDMS.SecurityElements.Ism.ISMVocabulary;
	using NoticeAttributes = DDMSSense.DDMS.SecurityElements.Ism.NoticeAttributes;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using Description = DDMSSense.DDMS.Summary.Description;
	using GeospatialCoverage = DDMSSense.DDMS.Summary.GeospatialCoverage;
	using NonStateActor = DDMSSense.DDMS.Summary.NonStateActor;
	using RelatedResource = DDMSSense.DDMS.Summary.RelatedResource;
	using SubjectCoverage = DDMSSense.DDMS.Summary.SubjectCoverage;
	using TemporalCoverage = DDMSSense.DDMS.Summary.TemporalCoverage;
	using VirtualCoverage = DDMSSense.DDMS.Summary.VirtualCoverage;
	using DDMSReader = DDMSSense.Util.DDMSReader;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:resource (the top-level element of a DDMS record).
	/// 
	/// <para>
	/// Starting in DDMS 3.0, resources have additional ISM attributes which did not exist in 2.0. However, the 2.0 schema 
	/// still allows "any" attributes on the Resource, so the 3.0 attribute values will be loaded if present.
	/// </para>
	///  
	/// <para>When generating HTML/Text output for a Resource, additional tags are generated listing the version of DDMSence 
	/// used to create the record (to help identify formatting bugs), and the version of DDMS. These lines are not required 
	/// (and may be removed). For example:</para>
	/// 
	/// <ul><code>
	/// ddms.generator: DDMSence 1.0.0<br />
	/// ddms.version: 3.0<br /><br />
	/// &lt;meta name="ddms.generator" content="DDMSence 1.0.0" /&gt;<br />
	/// &lt;meta name="ddms.version" content="3.0" /&gt;<br />
	/// </code></ul></p>
	/// 
	/// <para>The name of this component was changed from "Resource" to "resource" in DDMS 4.0.1.</para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:metacardInfo</u>: (exactly 1 required, starting in DDMS 4.0.1), implemented as a <seealso cref="MetacardInfo"/><br />
	/// <u>ddms:identifier</u>: (1-many required), implemented as an <seealso cref="Identifier"/><br />
	/// <u>ddms:title</u>: (1-many required), implemented as a <seealso cref="Title"/><br />
	/// <u>ddms:subtitle</u>: (0-many optional), implemented as a <seealso cref="Subtitle"/><br />
	/// <u>ddms:description</u>: (0-1 optional), implemented as a <seealso cref="Description"/><br />
	/// <u>ddms:language</u>: (0-many optional), implemented as a <seealso cref="Language"/><br />
	/// <u>ddms:dates</u>: (0-1 optional), implemented as a <seealso cref="Dates"/><br />
	/// <u>ddms:rights</u>: (0-1 optional), implemented as a <seealso cref="Rights"/><br />
	/// <u>ddms:source</u>: (0-many optional), implemented as a <seealso cref="Source"/><br />
	/// <u>ddms:type</u>: (0-many optional), implemented as a <seealso cref="Type"/><br />
	/// <u>ddms:contributor</u>: (0-many optional), implemented as a <seealso cref="Contributor"/><br />
	/// <u>ddms:creator</u>: (0-many optional), implemented as a <seealso cref="Creator"/><br />
	/// <u>ddms:pointOfContact</u>: (0-many optional), implemented as a <seealso cref="PointOfContact"/><br />
	/// <u>ddms:publisher</u>: (0-many optional), implemented as a <seealso cref="Publisher"/><br />
	/// <u>ddms:format</u>: (0-1 optional), implemented as a <seealso cref="Format"/><br />
	/// <u>ddms:subjectCoverage</u>: (1-many required, starting in DDMS 4.0.1), implemented as a <seealso cref="SubjectCoverage"/><br />
	/// <u>ddms:virtualCoverage</u>: (0-many optional), implemented as a <seealso cref="VirtualCoverage"/><br />
	/// <u>ddms:temporalCoverage</u>: (0-many optional), implemented as a <seealso cref="TemporalCoverage"/><br />
	/// <u>ddms:geospatialCoverage</u>: (0-many optional), implemented as a <seealso cref="GeospatialCoverage"/><br />
	/// <u>ddms:relatedResource</u>: (0-many optional), implemented as a <seealso cref="RelatedResource"/><br />
	/// <u>ddms:resourceManagement</u>: (0-1 optional, starting in DDMS 4.0.1), implemented as a <seealso cref="ResourceManagement"/><br />
	/// <u>ddms:security</u>: (exactly 1 required), implemented as a <seealso cref="Security"/><br />
	/// <u>Extensible Layer</u>: (0-many optional), implemented as a <seealso cref="ExtensibleElement"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ISM:resourceElement</u>: Identifies whether this tag sets the classification for the xml file as a whole
	/// (required, starting in DDMS 3.0)<br />
	/// <u>ISM:createDate</u>: Specifies the creation or latest modification date (YYYY-MM-DD) (required, starting in 
	/// DDMS 3.0)<br />
	/// <u>ISM:DESVersion</u>: Specifies the version of the Data Encoding Specification used for the security
	/// markings on this record. (required, starting in DDMS 3.0)<br />
	/// <u>ntk:DESVersion</u>: Specifies the version of the Data Encoding Specification used for Need-To-Know markings
	/// on this record. (required, starting in DDMS 4.0.1 with a fixed value)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are required. (starting in DDMS 
	/// 3.0)<br />
	/// <u><seealso cref="NoticeAttributes"/></u>: (optional, starting in DDMS 4.0.1)<br />
	/// <u><seealso cref="ExtensibleAttributes"/></u>: (optional)<br />
	/// <br />
	/// Starting in DDMS 3.0, the ISM attributes explicitly defined in the schema should appear in the SecurityAttributes, 
	/// not the ExtensibleAttributes. Attempts to load them as ExtensibleAttributes will throw an InvalidDDMSException.
	/// In DDMS 2.0, there are no ISM attributes explicitly defined in the schema, so you can load them in any way you 
	/// want. It is recommended that you load them as SecurityAttributes anyhow, for consistency with newer DDMS resources. 
	/// Please see the "Power Tips" on the Extensible Layer (on the DDMSence home page) for details. 
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Resource : AbstractBaseComponent {

		private MetacardInfo _metacardInfo = null;
		private List<Identifier> _identifiers = new List<Identifier>();
		private List<Title> _titles = new List<Title>();
		private List<Subtitle> _subtitles = new List<Subtitle>();
		private Description _description = null;
		private List<Language> _languages = new List<Language>();
		private Dates _dates = null;
		private Rights _rights = null;
		private List<Source> _sources = new List<Source>();
		private List<Type> _types = new List<Type>();
		private List<Creator> _creators = new List<Creator>();
		private List<Publisher> _publishers = new List<Publisher>();
		private List<Contributor> _contributors = new List<Contributor>();
		private List<PointOfContact> _pointOfContacts = new List<PointOfContact>();
		private Format _format = null;
		private List<SubjectCoverage> _subjectCoverages = new List<SubjectCoverage>();
		private List<VirtualCoverage> _virtualCoverages = new List<VirtualCoverage>();
		private List<TemporalCoverage> _temporalCoverages = new List<TemporalCoverage>();
		private List<GeospatialCoverage> GeospatialCoverages_Renamed = new List<GeospatialCoverage>();
		private List<RelatedResource> _relatedResources = new List<RelatedResource>();
		private ResourceManagement _resourceManagement = null;
		private Security _security = null;
		private List<ExtensibleElement> _extensibleElements = new List<ExtensibleElement>();
		private List<IDDMSComponent> _orderedList = new List<IDDMSComponent>();

		private DateTime? _createDate = null;
		internal List<string> _compliesWiths = null;
		private int? _ismDESVersion = null;
		private int? _ntkDESVersion = null;
		private NoticeAttributes _noticeAttributes = null;
		private SecurityAttributes _securityAttributes = null;
		private ExtensibleAttributes _extensibleAttributes = null;

		/// <summary>
		/// The attribute name for resource element flag </summary>
		protected internal const string RESOURCE_ELEMENT_NAME = "resourceElement";

		/// <summary>
		/// The attribute name for create date </summary>
		protected internal const string CREATE_DATE_NAME = "createDate";

		/// <summary>
		/// The attribute name for the compliesWith attribute </summary>
		public const string COMPLIES_WITH_NAME = "compliesWith";

		/// <summary>
		/// The attribute name for DES version </summary>
		public const string DES_VERSION_NAME = "DESVersion";

		private static readonly List<string> ALL_IC_ATTRIBUTES = new List<string>();
		static Resource() {
			ALL_IC_ATTRIBUTES.Add(RESOURCE_ELEMENT_NAME);
			ALL_IC_ATTRIBUTES.Add(CREATE_DATE_NAME);
			ALL_IC_ATTRIBUTES.Add(DES_VERSION_NAME);
		}

		/// <summary>
		/// A set of all Resource attribute names which should not be converted into ExtensibleAttributes </summary>
		public static readonly List<string> NON_EXTENSIBLE_NAMES = ALL_IC_ATTRIBUTES;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// 
		/// <para>Starting in DDMS 3.0, resources have additional ISM attributes which did not exist in 2.0. However, the 2.0
		/// schema still allows "any" attributes on the Resource, so the 3.0 attribute values will be loaded if present.</para>
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Resource(Element element) {
			try {
				SetXOMElement(element, false);
				string @namespace = element.Name.NamespaceName;
				string ismNamespace = DDMSVersion.IsmNamespace;

				string createDate = GetAttributeValue(CREATE_DATE_NAME, ismNamespace);
				if (!String.IsNullOrEmpty(createDate)) {
					_createDate = DateTime.Parse(createDate);
				}
				_compliesWiths = Util.GetXsListAsList(GetAttributeValue(COMPLIES_WITH_NAME, ismNamespace));
				string ismDESVersion = element.Attribute(XName.Get(DES_VERSION_NAME, ismNamespace)).Value;
				if (!String.IsNullOrEmpty(ismDESVersion)) {
					try {
						_ismDESVersion = Convert.ToInt32(ismDESVersion);
					} catch (FormatException) {
						// 	This will be thrown as an InvalidDDMSException during validation
					}
				}
				if (DDMSVersion.IsAtLeast("4.0.1")) {
					string ntkDESVersion = element.Attribute(XName.Get(DES_VERSION_NAME, DDMSVersion.NtkNamespace)).Value;
					if (!String.IsNullOrEmpty(ntkDESVersion)) {
						try {
							_ntkDESVersion = Convert.ToInt32(ntkDESVersion);
						} catch (FormatException) {
							// This will be thrown as an InvalidDDMSException during validation
						}
					}
				}
				_noticeAttributes = new NoticeAttributes(element);
				_securityAttributes = new SecurityAttributes(element);
				_extensibleAttributes = new ExtensibleAttributes(element);

				DDMSVersion version = DDMSVersion;

				// Metacard Set
				Element component = GetChild(MetacardInfo.GetName(version));
				if (component != null) {
					_metacardInfo = new MetacardInfo(component);
				}
				// Resource Set
				IEnumerable<Element> components = element.Elements(XName.Get(Identifier.GetName(version), @namespace));
				foreach(var comp in components)
                    _identifiers.Add(new Identifier(component));

                components = element.Elements(XName.Get(Title.GetName(version), @namespace));
				foreach(var comp in components)
                    _titles.Add(new Title(component));

                components = element.Elements(XName.Get(Subtitle.GetName(version), @namespace));
				foreach(var comp in components)
                    _subtitles.Add(new Subtitle(component));
				
                component = GetChild(Description.GetName(version));
				if (component != null) {
					_description = new Description(component);
				}
				components = element.Elements(XName.Get(Language.GetName(version), @namespace));
				foreach(var comp in components)
                    _languages.Add(new Language(component));
				
                component = GetChild(Dates.GetName(version));
				if (component != null) {
					_dates = new Dates(component);
				}
				component = GetChild(Rights.GetName(version));
				if (component != null) {
					_rights = new Rights(component);
				}
				components = element.Elements(XName.Get(Source.GetName(version), @namespace));
				foreach(var comp in components)
                    _sources.Add(new Source(component));
				
                components = element.Elements(XName.Get(Type.GetName(version), @namespace));
				foreach(var comp in components)
                    _types.Add(new Type(component));
				
                components = element.Elements(XName.Get(Creator.GetName(version), @namespace));
				foreach(var comp in components)
                    _creators.Add(new Creator(component));
				
                components = element.Elements(XName.Get(Publisher.GetName(version), @namespace));
				foreach(var comp in components)
                    _publishers.Add(new Publisher(component));
				
                components = element.Elements(XName.Get(Contributor.GetName(version), @namespace));
				foreach(var comp in components)
                    _contributors.Add(new Contributor(component));
				
                components = element.Elements(XName.Get(PointOfContact.GetName(version), @namespace));
				foreach(var comp in components)
                    _pointOfContacts.Add(new PointOfContact(component));
				
				// Format Set
				component = GetChild(Format.GetName(version));
				if (component != null) {
					_format = new Format(component);
				}

				// Summary Set
				components = element.Elements(XName.Get(SubjectCoverage.GetName(version), @namespace));
				foreach(var comp in components)
                    _subjectCoverages.Add(new SubjectCoverage(component));
				
                components = element.Elements(XName.Get(VirtualCoverage.GetName(version), @namespace));
				foreach(var comp in components)
                    _virtualCoverages.Add(new VirtualCoverage(component));
				
                components = element.Elements(XName.Get(TemporalCoverage.GetName(version), @namespace));
				foreach(var comp in components)
                    _temporalCoverages.Add(new TemporalCoverage(component));
				
                components = element.Elements(XName.Get(GeospatialCoverage.GetName(version), @namespace));
				foreach(var comp in components)
                    GeospatialCoverages_Renamed.Add(new GeospatialCoverage(component));
				
                components = element.Elements(XName.Get(RelatedResource.GetName(version), @namespace));
				foreach(var comp in components)
                    LoadRelatedResource(component);
				
				// Resource Set again
				component = GetChild(ResourceManagement.GetName(version));
				if (component != null) {
					_resourceManagement = new ResourceManagement(component);
				}

				// Security Set
				component = GetChild(Security.GetName(version));
				if (component != null) {
					_security = new Security(component);

					// Extensible Layer

					// We use the security component to locate the extensible layer. If it is null, this resource is going
					// to fail validation anyhow, so we skip the extensible layer.
					int index = 0;
					IEnumerable<Element> allElements = element.Elements();                    
					foreach(var el in allElements)
                        _extensibleElements.Add(new ExtensibleElement(el));
					
				}
				PopulatedOrderedList();
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Helper method to convert element-based related resources into components. In DDMS 4.0.1, there is a
		/// one-to-one correlation between the two. In DDMS 2.0, 3.0, or 3.1, the top-level ddms:RelatedResources
		/// element might contain more than 1 ddms:relatedResource. In the latter case, each ddms:relatedResource
		/// must be mediated into a separate RelatedResource instance.
		/// </summary>
		/// <param name="resource"> the top-level element </param>


		private void LoadRelatedResource(Element resource) {
			IEnumerable<Element> children = resource.Elements(XName.Get(RelatedResource.OLD_INNER_NAME, Namespace));
			if (children.Count() <= 1) {
				_relatedResources.Add(new RelatedResource(resource));
			} else {
				foreach(var child in children) {
					Element copy = new Element(resource);
                    copy.RemoveAll();
                    copy.Add(new Element(child));
					_relatedResources.Add(new RelatedResource(copy));
				}
			}
		}

		/// <summary>
		/// Constructor for creating a DDMS 2.0 Resource from raw data.
		/// 
		/// <para>This helper constructor merely calls the fully-parameterized version. Attempts to use it with DDMS 3.0
		/// (or higher) components will fail, because some required attributes are missing.</para>
		/// </summary>
		/// <param name="topLevelComponents"> a list of top level components </param>
		/// <param name="extensibleAttributes"> any extensible attributes (optional) </param>


		public Resource(List<IDDMSComponent> topLevelComponents, ExtensibleAttributes extensibleAttributes) : this(topLevelComponents, null, null, null, null, null, null, null, extensibleAttributes) {
		}

		/// <summary>
		/// Constructor for creating a DDMS 3.0 Resource from raw data.
		/// 
		/// <para>This helper constructor merely calls the fully-parameterized version. Attempts to use it with DDMS 3.1
		/// (or higher) components will fail, because some required attributes are missing.</para>
		/// </summary>
		/// <param name="topLevelComponents"> a list of top level components </param>
		/// <param name="resourceElement"> value of the resourceElement attribute (required, starting in DDMS 3.0) </param>
		/// <param name="createDate"> the create date as an xs:date (YYYY-MM-DD) (required, starting in DDMS 3.0) </param>
		/// <param name="ismDESVersion"> the DES Version as an Integer (required, starting in DDMS 3.0) </param>
		/// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required, starting in
		/// DDMS 3.0) </param>
		/// <param name="extensibleAttributes"> any extensible attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed, or if one of the components
		/// does not belong at the top-level of the Resource. </exception>


		public Resource(List<IDDMSComponent> topLevelComponents, bool? resourceElement, string createDate, int? ismDESVersion, SecurityAttributes securityAttributes, ExtensibleAttributes extensibleAttributes) : this(topLevelComponents, resourceElement, createDate, null, ismDESVersion, null, securityAttributes, null, extensibleAttributes) {
		}

		/// <summary>
		/// Constructor for creating a DDMS 3.1 Resource from raw data.
		/// 
		/// <para>This helper constructor merely calls the fully-parameterized version. Attempts to use it with DDMS 4.0.1
		/// (or higher) components will fail, because some required attributes are missing.</para>
		/// </summary>
		/// <param name="topLevelComponents"> a list of top level components </param>
		/// <param name="resourceElement"> value of the resourceElement attribute (required, starting in DDMS 3.0) </param>
		/// <param name="createDate"> the create date as an xs:date (YYYY-MM-DD) (required, starting in DDMS 3.0) </param>
		/// <param name="compliesWiths"> shows what ISM rulesets this resource complies with (optional, starting in DDMS 3.1) </param>
		/// <param name="ismDESVersion"> the DES Version as an Integer (required, starting in DDMS 3.0) </param>
		/// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required, starting in
		/// DDMS 3.0) </param>
		/// <param name="extensibleAttributes"> any extensible attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed, or if one of the components
		/// does not belong at the top-level of the Resource. </exception>


		public Resource(List<IDDMSComponent> topLevelComponents, bool? resourceElement, string createDate, List<string> compliesWiths, int? ismDESVersion, SecurityAttributes securityAttributes, ExtensibleAttributes extensibleAttributes) : this(topLevelComponents, resourceElement, createDate, compliesWiths, ismDESVersion, null, securityAttributes, null, extensibleAttributes) {
		}

		/// <summary>
		/// Constructor for creating a DDMS resource of any version from raw data.
		/// 
		/// <para>The other data-driven constructors call this one.</para>
		/// 
		/// <para> Passing the top-level components in as a list is a compromise between a constructor with over twenty
		/// parameters, and the added complexity of a step-by-step factory/builder approach. If any component is not a
		/// top-level component, an InvalidDDMSException will be thrown. </para>
		/// 
		/// <para> The order of different types of components does not matter here (a security component could be the first
		/// component in the list). However, if multiple instances of the same component type exist in the list (such as
		/// multiple identifier components), those components will be stored and output in the order of the list. If only 1
		/// instance can be supported, the last one in the list will be the one used. </para>
		/// 
		/// <para>Starting in DDMS 3.0, resources have additional ISM attributes which did not exist in 2.0. However, the 2.0
		/// schema still allows "any" attributes on the Resource, so the attribute values will be loaded if present. </para>
		/// </summary>
		/// <param name="topLevelComponents"> a list of top level components </param>
		/// <param name="resourceElement"> value of the resourceElement attribute (required, starting in DDMS 3.0) </param>
		/// <param name="createDate"> the create date as an xs:date (YYYY-MM-DD) (required, starting in DDMS 3.0) </param>
		/// <param name="compliesWiths"> shows what ISM rule sets this resource complies with (optional, starting in DDMS 3.1) </param>
		/// <param name="ismDESVersion"> the DES Version as an Integer (required, starting in DDMS 3.0) </param>
		/// <param name="ntkDESVersion"> the DES Version as an Integer (required, starting in DDMS 4.0.1) </param>
		/// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required, starting in
		/// DDMS 3.0) </param>
		/// <param name="noticeAttributes"> any notice attributes (optional, starting in DDMS 4.0.1) </param>
		/// <param name="extensibleAttributes"> any extensible attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed, or if one of the components
		/// does not belong at the top-level of the Resource. </exception>


		public Resource(List<IDDMSComponent> topLevelComponents, bool? resourceElement, string createDate, List<string> compliesWiths, int? ismDESVersion, int? ntkDESVersion, SecurityAttributes securityAttributes, NoticeAttributes noticeAttributes, ExtensibleAttributes extensibleAttributes) {
			try {
				if (topLevelComponents == null) {
                    topLevelComponents = new List<IDDMSComponent>();
				}
				if (compliesWiths == null) {
                    compliesWiths = new List<string>();
				}

				DDMSVersion version = DDMSVersion.GetCurrentVersion();
				string ismPrefix = PropertyReader.GetPrefix("ism");
				string ismNamespace = version.IsmNamespace;
				string ntkPrefix = PropertyReader.GetPrefix("ntk");
				string ntkNamespace = version.NtkNamespace;
				Element element = Util.BuildDDMSElement(Resource.GetName(version), null);

				// Attributes
				_compliesWiths = compliesWiths;
				if (compliesWiths.Count > 0) {
					Util.AddAttribute(element, ismPrefix, COMPLIES_WITH_NAME, ismNamespace, Util.GetXsList(compliesWiths));
				}
				if (ntkDESVersion != null) {
					_ntkDESVersion = ntkDESVersion;
					Util.AddAttribute(element, ntkPrefix, DES_VERSION_NAME, ntkNamespace, ntkDESVersion.ToString());
				}
				if (resourceElement != null) {
					Util.AddAttribute(element, ismPrefix, RESOURCE_ELEMENT_NAME, ismNamespace, Convert.ToString(resourceElement));
				}
				if (ismDESVersion != null) {
					_ismDESVersion = ismDESVersion;
					Util.AddAttribute(element, ismPrefix, DES_VERSION_NAME, ismNamespace, ismDESVersion.ToString());
				}
				if (!String.IsNullOrEmpty(createDate)) {
					try {
						_createDate = DateTime.Parse(createDate);
					} catch (System.ArgumentException) {
						throw new InvalidDDMSException("The ISM:createDate attribute is not in a valid date format.");
					}
					Util.AddAttribute(element, ismPrefix, CREATE_DATE_NAME, version.IsmNamespace, CreateDate.ToString("o"));
				}
				_noticeAttributes = NoticeAttributes.GetNonNullInstance(noticeAttributes);
				_noticeAttributes.AddTo(element);
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				_extensibleAttributes = ExtensibleAttributes.GetNonNullInstance(extensibleAttributes);
				_extensibleAttributes.AddTo(element);

				foreach (IDDMSComponent component in topLevelComponents) {
					if (component == null) {
						continue;
					}

					// Metacard Set
					if (component is MetacardInfo) {
						_metacardInfo = (MetacardInfo) component;
					}
					// Resource Set
					else if (component is Identifier) {
						_identifiers.Add((Identifier) component);
					} else if (component is Title) {
						_titles.Add((Title) component);
					} else if (component is Subtitle) {
						_subtitles.Add((Subtitle) component);
					} else if (component is Description) {
						_description = (Description) component;
					} else if (component is Language) {
						_languages.Add((Language) component);
					} else if (component is Dates) {
						_dates = (Dates) component;
					} else if (component is Rights) {
						_rights = (Rights) component;
					} else if (component is Source) {
						_sources.Add((Source) component);
					} else if (component is Type) {
						_types.Add((Type) component);
					} else if (component is Creator) {
						_creators.Add((Creator) component);
					} else if (component is Publisher) {
						_publishers.Add((Publisher) component);
					} else if (component is Contributor) {
						_contributors.Add((Contributor) component);
					} else if (component is PointOfContact) {
						_pointOfContacts.Add((PointOfContact) component);
					}
					// Format Set
					else if (component is Format) {
						_format = (Format) component;
					}
					// Summary Set
					else if (component is SubjectCoverage) {
						_subjectCoverages.Add((SubjectCoverage) component);
					} else if (component is VirtualCoverage) {
						_virtualCoverages.Add((VirtualCoverage) component);
					} else if (component is TemporalCoverage) {
						_temporalCoverages.Add((TemporalCoverage) component);
					} else if (component is GeospatialCoverage) {
						GeospatialCoverages_Renamed.Add((GeospatialCoverage) component);
					} else if (component is RelatedResource) {
						_relatedResources.Add((RelatedResource) component);
					}
					// Resource Set again
					else if (component is ResourceManagement) {
						_resourceManagement = (ResourceManagement) component;
					}
					// Security Set
					else if (component is Security) {
						_security = (Security) component;
					}
					// Extensible Layer
					else if (component is ExtensibleElement) {
						_extensibleElements.Add((ExtensibleElement) component);
					} else {
						throw new InvalidDDMSException(component.Name + " is not a valid top-level component in a resource.");
					}
				}
				PopulatedOrderedList();
				foreach (IDDMSComponent component in TopLevelComponents) {
					element.Add(component.XOMElementCopy);
				}
				SetXOMElement(element, true);
				DDMSReader.ValidateWithSchema(ToXML());
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Creates an ordered list of all the top-level components in this Resource, for ease of traversal.
		/// </summary>
		private void PopulatedOrderedList() {
			if (MetacardInfo != null) {
				_orderedList.Add(MetacardInfo);
			}
			_orderedList.AddRange(Identifiers);
			_orderedList.AddRange(Titles);
			_orderedList.AddRange(Subtitles);
			if (Description != null) {
				_orderedList.Add(Description);
			}
			_orderedList.AddRange(Languages);
			if (Dates != null) {
				_orderedList.Add(Dates);
			}
			if (Rights != null) {
				_orderedList.Add(Rights);
			}
			_orderedList.AddRange(Sources);
			_orderedList.AddRange(Types);
			_orderedList.AddRange(Creators);
			_orderedList.AddRange(Publishers);
			_orderedList.AddRange(Contributors);
			_orderedList.AddRange(PointOfContacts);
			if (Format != null) {
				_orderedList.Add(Format);
			}
			_orderedList.AddRange(SubjectCoverages);
			_orderedList.AddRange(VirtualCoverages);
			_orderedList.AddRange(TemporalCoverages);
			_orderedList.AddRange(GeospatialCoverages);
			_orderedList.AddRange(RelatedResources);
			if (ResourceManagement != null) {
				_orderedList.Add(ResourceManagement);
			}
			if (Security != null) {
				_orderedList.Add(Security);
			}
			_orderedList.AddRange(ExtensibleElements);
		}

		/// <summary>
		/// Performs a Schematron validation of the DDMS Resource, via the ISO Schematron skeleton stylesheets for XSLT1
		/// or XSLT2 processors. This action can only be performed on a DDMS Resource which is already valid according 
		/// to the DDMS specification.
		/// 
		/// <para>The informational results of this validation are returned to the caller in a list of ValidationMessages of
		/// type "Warning" for reports and "Error" for failed asserts. These messages do NOT affect the validity of the
		/// underlying object model. The locator on the ValidationMessage will be the location attribute from the
		/// successful-report or failed-assert element.</para>
		/// 
		/// <para>Details about ISO Schematron can be found at: http://www.schematron.com/ </para>
		/// </summary>
		/// <param name="schematronFile"> the file containing the ISO Schematron constraints. This file is transformed with the ISO
		/// Schematron skeleton files. </param>
		/// <returns> a list of ValidationMessages </returns>
		/// <exception cref="XSLException"> if there are XSL problems transforming with stylesheets </exception>
		/// <exception cref="IOException"> if there are problems reading or parsing the Schematron file </exception>


		public List<ValidationMessage> ValidateWithSchematron(FileStream schematronFile) {
			List<ValidationMessage> messages = new List<ValidationMessage>();
			XSLTransform schematronTransform = Util.BuildSchematronTransform(schematronFile);
			Node nodes = schematronTransform.Transform(new Document(XOMElementCopy));
			Document doc = XSLTransform.toDocument(nodes);

            var reader = doc.CreateReader();
            var tempDoc = XDocument.Load(reader);
            XmlNamespaceManager ns = new XmlNamespaceManager(reader.NameTable);
			string svrlNamespace = context.lookup("svrl");
			IEnumerable<Element> outputNodes = doc.XPathSelectElements("//svrl:failed-assert | //svrl:successful-report", ns);
            foreach (var outputElement in outputNodes) { 
					bool isAssert = "failed-assert".Equals(outputElement.Name.LocalName);
                    string text = outputElement.Element(XName.Get("text", svrlNamespace)).Value;
					string locator = outputElement.Attribute("location").Value;
					messages.Add(isAssert ? ValidationMessage.NewError(text, locator) : ValidationMessage.NewWarning(text, locator));
			}
			return (messages);
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>Exactly 1 metacardInfo, 1-many identifiers, 1-many titles, 0-1 descriptions, 0-1 dates, 0-1 rights, 
		/// 0-1 formats, exactly 1 subjectCoverage, 0-1 resourceManagement, and exactly 1 security element must exist.</li>
		/// <li>Starting in DDMS 4.0.1, 1-many subjectCoverage elements can exist.</li>
		/// <li>At least 1 of creator, publisher, contributor, or pointOfContact must exist.</li>
		/// <li>All ddms:order attributes make a complete, consecutive set, starting at 1.</li>
		/// <li>resourceElement attribute must exist, starting in DDMS 3.0.</li>
		/// <li>createDate attribute must exist and conform to the xs:date date type (YYYY-MM-DD), starting in DDMS 3.0.</li>
		/// <li>The compliesWith attribute cannot be used until DDMS 3.1 or later.</li>
		/// <li>If set, the compliesWith attribute must be valid tokens.</li>
		/// <li>ISM DESVersion must exist and be a valid Integer, starting in DDMS 3.0.</li>
		/// <li>The value of ISM DESVersion must be fixed, starting in DDMS 3.1. This is checked during schema validation.</li>
		/// <li>NTK DESVersion must exist and be a valid Integer, starting in DDMS 4.0.1.</li>
		/// <li>The value of NTK DESVersion must be fixed,s tarting in DDMS 4.0.1. This is checked during schema validation.</li>
		/// <li>A classification is required, starting in DDMS 3.0.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty, starting in DDMS 3.0.</li>
		/// <li>Only 1 extensible element can exist in DDMS 2.0.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Resource.GetName(DDMSVersion));

			if (Identifiers.Count < 1) {
				throw new InvalidDDMSException("At least 1 identifier is required.");
			}
			if (Titles.Count < 1) {
				throw new InvalidDDMSException("At least 1 title is required.");
			}
			if (Creators.Count + Contributors.Count + Publishers.Count + PointOfContacts.Count == 0) {
				throw new InvalidDDMSException("At least 1 producer (creator, contributor, publisher, or pointOfContact) is required.");
			}
			Util.RequireBoundedChildCount(Element, Description.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, Dates.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, Rights.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, Format.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, ResourceManagement.GetName(DDMSVersion), 0, 1);
			if (DDMSVersion.IsAtLeast("4.0.1")) {
				if (SubjectCoverages.Count < 1) {
					throw new InvalidDDMSException("At least 1 subjectCoverage is required.");
				}
			} else {
				Util.RequireBoundedChildCount(Element, SubjectCoverage.GetName(DDMSVersion), 1, 1);
			}
			Util.RequireBoundedChildCount(Element, Security.GetName(DDMSVersion), 1, 1);

			// Should be reviewed as additional versions of DDMS are supported.
			if (DDMSVersion.IsAtLeast("4.0.1")) {
				ValidateOrderAttributes();
				Util.RequireDDMSValue("ntk:" + DES_VERSION_NAME, NtkDESVersion);
			}
			if (!DDMSVersion.IsAtLeast("3.1") && CompliesWiths.Count > 0) {
				throw new InvalidDDMSException("The compliesWith attribute cannot be used until DDMS 3.1 or later.");
			}
			foreach (string with in CompliesWiths) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_COMPLIES_WITH, with);
			}
			if (!DDMSVersion.IsAtLeast("3.0") && ExtensibleElements.Count > 1) {
				throw new InvalidDDMSException("Only 1 extensible element is allowed in DDMS 2.0.");
			}
			if (DDMSVersion.IsAtLeast("4.0.1")) {
				Util.RequireBoundedChildCount(Element, MetacardInfo.GetName(DDMSVersion), 1, 1);
			}
			if (DDMSVersion.IsAtLeast("3.0")) {
				Util.RequireDDMSValue(RESOURCE_ELEMENT_NAME, ResourceElement);
				Util.RequireDDMSValue(CREATE_DATE_NAME, CreateDate);
				Util.RequireDDMSValue("ISM:" + DES_VERSION_NAME, IsmDESVersion);
				Util.RequireDDMSValue("security attributes", SecurityAttributes);
				SecurityAttributes.RequireClassification();
			}

			base.Validate();
		}

		/// <summary>
		/// Validates the ddms:order attributes on ddms:nonStateActor and ddms:geospatialCoverage elements.  All elements 
		/// in the document which specify the order attribute should be interpreted as entries in a single, ordered list.
		/// Values must be sequential, starting at 1, and may not contain duplicates. </summary>
		/// <exception cref="InvalidDDMSException"> if the orders do not make a unique consecutive list starting at 1. </exception>


		private void ValidateOrderAttributes() {
			List<int?> orders = new List<int?>();
			foreach (GeospatialCoverage coverage in GeospatialCoverages) {
				if (coverage.Order != null) {
					orders.Add(coverage.Order);
				}
			}
			foreach (SubjectCoverage coverage in SubjectCoverages) {
				foreach (NonStateActor actor in coverage.NonStateActors) {
					if (actor.Order != null) {
						orders.Add(actor.Order);
					}
				}
			}
			orders.Sort();
			for (int i = 0; i < orders.Count; i++) {
				int? expectedValue = Convert.ToInt32(i + 1);
				if (!expectedValue.Equals(orders[i])) {
					throw new InvalidDDMSException("The ddms:order attributes throughout this resource must form " + "a single, ordered list starting from 1.");
				}
			}
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>An externalNotice attribute may cause issues for DDMS 4.0 records.</li>
		/// <li>Add any warnings from the notice attributes.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (!NoticeAttributes.Empty) {
				AddWarnings(NoticeAttributes.ValidationWarnings, true);
				if (NoticeAttributes.ExternalReference != null) {
					AddDdms40Warning("ISM:externalNotice attribute");
				}
			}
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			if (ResourceElement != null) {
				text.Append(BuildOutput(isHTML, localPrefix + RESOURCE_ELEMENT_NAME, Convert.ToString(ResourceElement)));
			}
			if (CreateDate != null) {
				text.Append(BuildOutput(isHTML, localPrefix + CREATE_DATE_NAME, CreateDate.ToString("o")));
			}
			text.Append(BuildOutput(isHTML, localPrefix + COMPLIES_WITH_NAME, Util.GetXsList(CompliesWiths)));
			if (IsmDESVersion != null) {
				text.Append(BuildOutput(isHTML, localPrefix + "ism." + DES_VERSION_NAME, Convert.ToString(IsmDESVersion)));
			}
			if (NtkDESVersion != null) {
				text.Append(BuildOutput(isHTML, localPrefix + "ntk." + DES_VERSION_NAME, Convert.ToString(NtkDESVersion)));
			}
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			text.Append(NoticeAttributes.GetOutput(isHTML, localPrefix));
			text.Append(ExtensibleAttributes.GetOutput(isHTML, localPrefix));

			// Traverse top-level components, suppressing the resource prefix
			if (MetacardInfo != null) {
				text.Append(MetacardInfo.GetOutput(isHTML, "", ""));
			}
			text.Append(BuildOutput(isHTML, "", Identifiers));
			text.Append(BuildOutput(isHTML, "", Titles));
			text.Append(BuildOutput(isHTML, "", Subtitles));
			if (Description != null) {
				text.Append(Description.GetOutput(isHTML, "", ""));
			}
			text.Append(BuildOutput(isHTML, "", Languages));
			if (Dates != null) {
				text.Append(Dates.GetOutput(isHTML, "", ""));
			}
			if (Rights != null) {
				text.Append(Rights.GetOutput(isHTML, "", ""));
			}
			text.Append(BuildOutput(isHTML, "", Sources));
			text.Append(BuildOutput(isHTML, "", Types));
			text.Append(BuildOutput(isHTML, "", Creators));
			text.Append(BuildOutput(isHTML, "", Publishers));
			text.Append(BuildOutput(isHTML, "", Contributors));
			text.Append(BuildOutput(isHTML, "", PointOfContacts));
			if (Format != null) {
				text.Append(Format.GetOutput(isHTML, "", ""));
			}
			text.Append(BuildOutput(isHTML, "", SubjectCoverages));
			text.Append(BuildOutput(isHTML, "", VirtualCoverages));
			text.Append(BuildOutput(isHTML, "", TemporalCoverages));
			text.Append(BuildOutput(isHTML, "", GeospatialCoverages));
			text.Append(BuildOutput(isHTML, "", RelatedResources));
			if (ResourceManagement != null) {
				text.Append(ResourceManagement.GetOutput(isHTML, "", ""));
			}
			if (Security != null) {
				text.Append(Security.GetOutput(isHTML, "", ""));
			}
			text.Append(BuildOutput(isHTML, "", ExtensibleElements));

			text.Append(BuildOutput(isHTML, "extensible.layer", Convert.ToString(ExtensibleElements.Count > 0)));
			text.Append(BuildOutput(isHTML, "ddms.generator", "DDMSence " + PropertyReader.GetProperty("version")));
			text.Append(BuildOutput(isHTML, "ddms.version", DDMSVersion.Version));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Resource)) {
				return (false);
			}
			Resource test = (Resource) obj;
			return (Util.NullEquals(ResourceElement, test.ResourceElement) && Util.NullEquals(CreateDate, test.CreateDate) && Util.ListEquals(CompliesWiths, test.CompliesWiths) && Util.NullEquals(IsmDESVersion, test.IsmDESVersion) && Util.NullEquals(NtkDESVersion, test.NtkDESVersion) && NoticeAttributes.Equals(test.NoticeAttributes) && ExtensibleAttributes.Equals(test.ExtensibleAttributes));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			if (ResourceElement != null) {
				result = 7 * result + ResourceElement.GetHashCode();
			}
			if (CreateDate != null) {
				result = 7 * result + CreateDate.GetHashCode();
			}
			result = 7 * result + CompliesWiths.GetHashCode();
			if (IsmDESVersion != null) {
				result = 7 * result + IsmDESVersion.GetHashCode();
			}
			if (NtkDESVersion != null) {
				result = 7 * result + NtkDESVersion.GetHashCode();
			}
			result = 7 * result + NoticeAttributes.GetHashCode();
			result = 7 * result + ExtensibleAttributes.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return (version.IsAtLeast("4.0.1") ? "resource" : "Resource");
		}

		/// <summary>
		/// Accessor for the MetacardInfo component (exactly 1)
		/// </summary>
		public MetacardInfo MetacardInfo {
			get {
				return (_metacardInfo);
			}
			set {
					_metacardInfo = value;
			}
		}

		/// <summary>
		/// Accessor for the identifier components. There will always be at least one.
		/// </summary>
		public List<Identifier> Identifiers {
			get {
				return _identifiers;
			}
		}

		/// <summary>
		/// Accessor for the title components. There will always be at least one.
		/// </summary>
		public List<Title> Titles {
			get {
				return _titles;
			}
		}

		/// <summary>
		/// Accessor for the subtitle components (0-many)
		/// </summary>
		public List<Subtitle> Subtitles {
			get {
				return _subtitles;
			}
		}

		/// <summary>
		/// Accessor for the description component (0-1)
		/// </summary>
		public Description Description {
			get {
				return (_description);
			}
			set {
					_description = value;
			}
		}

		/// <summary>
		/// Accessor for the language components (0-many)
		/// </summary>
		public List<Language> Languages {
			get {
				return _languages;
			}
		}

		/// <summary>
		/// Accessor for the dates component (0-1). May return null.
		/// </summary>
		public Dates Dates {
			get {
				return _dates;
			}
			set {
					_dates = value;
			}
		}

		/// <summary>
		/// Accessor for the rights component (0-1). May return null.
		/// </summary>
		public Rights Rights {
			get {
				return _rights;
			}
			set {
					_rights = value;
			}
		}

		/// <summary>
		/// Accessor for the source components (0-many)
		/// </summary>
		public List<Source> Sources {
			get {
				return _sources;
			}
		}

		/// <summary>
		/// Accessor for the type components (0-many)
		/// </summary>
		public List<Type> Types {
			get {
				return _types;
			}
		}

		/// <summary>
		/// Accessor for a list of all Creator entities (0-many)
		/// </summary>
		public List<Creator> Creators {
			get {
				return _creators;
			}
		}

		/// <summary>
		/// Accessor for a list of all Publisher entities (0-many)
		/// </summary>
		public List<Publisher> Publishers {
			get {
				return _publishers;
			}
		}

		/// <summary>
		/// Accessor for a list of all Contributor entities (0-many)
		/// </summary>
		public List<Contributor> Contributors {
			get {
				return _contributors;
			}
		}

		/// <summary>
		/// Accessor for a list of all PointOfContact entities (0-many)
		/// </summary>
		public List<PointOfContact> PointOfContacts {
			get {
				return _pointOfContacts;
			}
		}

		/// <summary>
		/// Accessor for the Format component (0-1). May return null.
		/// </summary>
        public Format Format
        {
			get {
				return (_format);
			}
			set {
					_format = value;
			}
		}

		/// <summary>
		/// Accessor for the subjectCoverage component (1-many)
		/// </summary>
		public List<SubjectCoverage> SubjectCoverages {
			get {
				return _subjectCoverages;
			}
		}

		/// <summary>
		/// Accessor for the virtualCoverage components (0-many)
		/// </summary>
		public List<VirtualCoverage> VirtualCoverages {
			get {
				return _virtualCoverages;
			}
		}

		/// <summary>
		/// Accessor for the temporalCoverage components (0-many)
		/// </summary>
		public List<TemporalCoverage> TemporalCoverages {
			get {
				return _temporalCoverages;
			}
		}

		/// <summary>
		/// Accessor for the geospatialCoverage components (0-many)
		/// </summary>
		public List<GeospatialCoverage> GeospatialCoverages {
			get {
				return GeospatialCoverages_Renamed;
			}
		}

		/// <summary>
		/// Accessor for the RelatedResource components (0-many)
		/// </summary>
		public List<RelatedResource> RelatedResources {
			get {
				return _relatedResources;
			}
		}

		/// <summary>
		/// Accessor for the ResourceManagement component (0-1). May return null.
		/// </summary>
		public ResourceManagement ResourceManagement {
			get {
				return (_resourceManagement);
			}
			set {
					_resourceManagement = value;
			}
		}

		/// <summary>
		/// Accessor for the security component (exactly 1). May return null but this cannot happen after instantiation.
		/// </summary>
		public Security Security {
			get {
				return (_security);
			}
			set {
					_security = value;
			}
		}

		/// <summary>
		/// Accessor for the extensible layer elements (0-many in 3.0, 0-1 in 2.0).
		/// </summary>
		public List<ExtensibleElement> ExtensibleElements {
			get {
				return _extensibleElements;
			}
		}

		/// <summary>
		/// Accessor for the resourceElement attribute. This may be null for v2.0 Resource components.
		/// </summary>
		public bool? ResourceElement {
			get {
				string value = GetAttributeValue(RESOURCE_ELEMENT_NAME, DDMSVersion.IsmNamespace);
				if ("true".Equals(value)) {
					return (true);
				}
				if ("false".Equals(value)) {
					return (false);
				}
				return (null);
			}
		}

		/// <summary>
		/// Accessor for the createDate date (optional). Returns a copy. This may be null for v2.0 Resource components.
		/// </summary>
		public DateTime CreateDate {
			get {
				return DateTime.Parse(_createDate.Value.ToString("o"));
			}
			set {
					_createDate = value;
			}
		}

		/// <summary>
		/// Accessor for the ISM compliesWith attribute.
		/// </summary>
		public List<string> CompliesWiths {
			get {
				return _compliesWiths;
			}
			set {
                _compliesWiths = value; ;
			}
		}

		/// <summary>
		/// Accessor for the ISM DESVersion attribute. Because this attribute does not exist before DDMS 3.0, the accessor
		/// will return null for v2.0 Resource elements.
		/// </summary>
		public int? IsmDESVersion {
			get {
				return (_ismDESVersion);
			}
			set {
					_ismDESVersion = value;
			}
		}

		/// <summary>
		/// Accessor for the NTK DESVersion attribute.
		/// </summary>
		public int? NtkDESVersion {
			get {
				return (_ntkDESVersion);
			}
			set {
					_ntkDESVersion = value;
			}
		}

		/// <summary>
		/// Accessor for an ordered list of the components in this Resource. Components which are missing are not represented
		/// in this list (no null entries).
		/// </summary>
		public List<IDDMSComponent> TopLevelComponents {
			get {
				return _orderedList;
			}
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				return (TopLevelComponents);
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
		/// </summary>
		public override SecurityAttributes SecurityAttributes {
			get {
				return (_securityAttributes);
			}
			set {
					_securityAttributes = value;
			}
		}

		/// <summary>
		/// Accessor for the Notice Attributes. Will always be non-null even if the attributes are not set.
		/// </summary>
		public NoticeAttributes NoticeAttributes {
			get {
				return (_noticeAttributes);
			}
			set {
					_noticeAttributes = value;
			}
		}

		/// <summary>
		/// Accessor for the extensible attributes. Will always be non-null, even if not set.
		/// </summary>
		public ExtensibleAttributes ExtensibleAttributes {
			get {
				return (_extensibleAttributes);
			}
			set {
					_extensibleAttributes = value;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = -8581492714895157280L;
			internal MetacardInfo.Builder _metacardInfo;
			internal List<Identifier.Builder> _identifiers;
			internal List<Title.Builder> _titles;
			internal List<Subtitle.Builder> _subtitles;
			internal Description.Builder _description;
			internal List<Language.Builder> _languages;
			internal Dates.Builder _dates;
			internal Rights.Builder _rights;
			internal List<Source.Builder> _sources;
			internal List<Type.Builder> _types;
			internal List<Creator.Builder> _creators;
			internal List<Contributor.Builder> _contributors;
			internal List<Publisher.Builder> _publishers;
			internal List<PointOfContact.Builder> _pointOfContacts;
			internal Format.Builder _format;
			internal List<SubjectCoverage.Builder> _subjectCoverages;
			internal List<VirtualCoverage.Builder> _virtualCoverages;
			internal List<TemporalCoverage.Builder> _temporalCoverages;
			internal List<GeospatialCoverage.Builder> _geospatialCoverages;
			internal List<RelatedResource.Builder> _relatedResources;
			internal ResourceManagement.Builder _resourceManagement;
			internal Security.Builder _security;
			internal List<ExtensibleElement.Builder> _extensibleElements;

			internal bool? _resourceElement;
			internal string _createDate;
			internal List<string> _compliesWiths;
			internal int? _ismDESVersion;
			internal int? _ntkDESVersion;
			internal NoticeAttributes.Builder _noticeAttributes;
			internal SecurityAttributes.Builder _securityAttributes;
			internal ExtensibleAttributes.Builder _extensibleAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Resource resource) {
				foreach (IDDMSComponent component in resource.TopLevelComponents) {
					// Metacard Set
					if (component is MetacardInfo) {
						MetacardInfo = new MetacardInfo.Builder((MetacardInfo) component);
					}
					// Resource Set
					else if (component is Identifier) {
						Identifiers.Add(new Identifier.Builder((Identifier) component));
					} else if (component is Title) {
						Titles.Add(new Title.Builder((Title) component));
					} else if (component is Subtitle) {
						Subtitles.Add(new Subtitle.Builder((Subtitle) component));
					} else if (component is Description) {
						Description = new Description.Builder((Description) component);
					} else if (component is Language) {
						Languages.Add(new Language.Builder((Language) component));
					} else if (component is Dates) {
						Dates = new Dates.Builder((Dates) component);
					} else if (component is Rights) {
						Rights = new Rights.Builder((Rights) component);
					} else if (component is Source) {
						Sources.Add(new Source.Builder((Source) component));
					} else if (component is Type) {
						Types.Add(new Type.Builder((Type) component));
					} else if (component is Creator) {
						Creators.Add(new Creator.Builder((Creator) component));
					} else if (component is Contributor) {
						Contributors.Add(new Contributor.Builder((Contributor) component));
					} else if (component is Publisher) {
						Publishers.Add(new Publisher.Builder((Publisher) component));
					} else if (component is PointOfContact) {
						PointOfContacts.Add(new PointOfContact.Builder((PointOfContact) component));
					}

					// Format Set
					else if (component is Format) {
						Format = new Format.Builder((Format) component);
					}
					// Summary Set
					else if (component is SubjectCoverage) {
						SubjectCoverages.Add(new SubjectCoverage.Builder((SubjectCoverage) component));
					} else if (component is VirtualCoverage) {
						VirtualCoverages.Add(new VirtualCoverage.Builder((VirtualCoverage) component));
					} else if (component is TemporalCoverage) {
						TemporalCoverages.Add(new TemporalCoverage.Builder((TemporalCoverage) component));
					} else if (component is GeospatialCoverage) {
						GeospatialCoverages.Add(new GeospatialCoverage.Builder((GeospatialCoverage) component));
					} else if (component is RelatedResource) {
						RelatedResources.Add(new RelatedResource.Builder((RelatedResource) component));
					}
					// Resource Set again
					else if (component is ResourceManagement) {
						ResourceManagement = new ResourceManagement.Builder((ResourceManagement) component);
					}

					// Security Set
					else if (component is Security) {
						Security = new Security.Builder((Security) component);
					}
					// Extensible Layer
					else if (component is ExtensibleElement) {
						ExtensibleElements.Add(new ExtensibleElement.Builder((ExtensibleElement) component));
					}
				}
				if (resource.CreateDate != null) {
					CreateDate = resource.CreateDate.ToString("o");
				}
				ResourceElement = resource.ResourceElement;
				CompliesWiths = resource.CompliesWiths;
				IsmDESVersion = resource.IsmDESVersion;
				NtkDESVersion = resource.NtkDESVersion;
				SecurityAttributes = new SecurityAttributes.Builder(resource.SecurityAttributes);
				NoticeAttributes = new NoticeAttributes.Builder(resource.NoticeAttributes);
				ExtensibleAttributes = new ExtensibleAttributes.Builder(resource.ExtensibleAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				List<IDDMSComponent> topLevelComponents = new List<IDDMSComponent>();
				foreach (IBuilder builder in ChildBuilders) {
					IDDMSComponent component = builder.Commit();
					if (component != null) {
						topLevelComponents.Add(component);
					}
				}
				return (new Resource(topLevelComponents, ResourceElement, CreateDate, CompliesWiths, IsmDESVersion, NtkDESVersion, SecurityAttributes.Commit(), NoticeAttributes.Commit(), ExtensibleAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in ChildBuilders) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && String.IsNullOrEmpty(CreateDate) && ResourceElement == null && CompliesWiths.Count == 0 && IsmDESVersion == null && NtkDESVersion == null && SecurityAttributes.Empty && NoticeAttributes.Empty && ExtensibleAttributes.Empty);
				}
			}

			/// <summary>
			/// Convenience method to get every child Builder in this Builder.
			/// </summary>
			/// <returns> a list of IBuilders </returns>
			internal virtual List<IBuilder> ChildBuilders {
				get {
					List<IBuilder> list = new List<IBuilder>();
					list.AddRange(Identifiers);
					list.AddRange(Titles);
					list.AddRange(Subtitles);
					list.AddRange(Languages);
					list.AddRange(Sources);
					list.AddRange(Types);
					list.AddRange(Creators);
					list.AddRange(Contributors);
					list.AddRange(Publishers);
					list.AddRange(PointOfContacts);
					list.AddRange(SubjectCoverages);
					list.AddRange(VirtualCoverages);
					list.AddRange(TemporalCoverages);
					list.AddRange(GeospatialCoverages);
					list.AddRange(RelatedResources);
					list.AddRange(ExtensibleElements);
					list.Add(MetacardInfo);
					list.Add(Description);
					list.Add(Dates);
					list.Add(Rights);
					list.Add(Format);
					list.Add(ResourceManagement);
					list.Add(Security);
					return (list);
				}
			}

			/// <summary>
			/// Builder accessor for the metacardInfo
			/// </summary>
			public virtual MetacardInfo.Builder MetacardInfo {
				get {
					if (_metacardInfo == null) {
						_metacardInfo = new MetacardInfo.Builder();
					}
					return _metacardInfo;
				}
                set { _metacardInfo = value; }
			}


			/// <summary>
			/// Builder accessor for the identifiers
			/// </summary>
			public virtual List<Identifier.Builder> Identifiers {
				get {
					if (_identifiers == null) {
                        _identifiers = new List<Identifier.Builder>();
					}
					return _identifiers;
				}
			}

			/// <summary>
			/// Builder accessor for the titles
			/// </summary>
			public virtual List<Title.Builder> Titles {
				get {
					if (_titles == null) {
                        _titles = new List<Title.Builder>();
					}
					return _titles;
				}
			}

			/// <summary>
			/// Builder accessor for the subtitles
			/// </summary>
			public virtual List<Subtitle.Builder> Subtitles {
				get {
					if (_subtitles == null) {
                        _subtitles = new List<Subtitle.Builder>();
					}
					return _subtitles;
				}
			}

			/// <summary>
			/// Builder accessor for the description
			/// </summary>
			public virtual Description.Builder Description {
				get {
					if (_description == null) {
						_description = new Description.Builder();
					}
					return _description;
				}
                set { _description = value; }
			}


			/// <summary>
			/// Builder accessor for the languages
			/// </summary>
			public virtual List<Language.Builder> Languages {
				get {
					if (_languages == null) {
                        _languages = new List<Language.Builder>();
					}
					return _languages;
				}
			}

			/// <summary>
			/// Builder accessor for the dates
			/// </summary>
			public virtual Dates.Builder Dates {
				get {
					if (_dates == null) {
						_dates = new Dates.Builder();
					}
					return _dates;
				}
                set { _dates = value; }
			}


			/// <summary>
			/// Builder accessor for the rights
			/// </summary>
			public virtual Rights.Builder Rights {
				get {
					if (_rights == null) {
						_rights = new Rights.Builder();
					}
					return _rights;
				}
                set { _rights = value; }
			}


			/// <summary>
			/// Builder accessor for the sources
			/// </summary>
			public virtual List<Source.Builder> Sources {
				get {
					if (_sources == null) {
                        _sources = new List<Source.Builder>();
					}
					return _sources;
				}
			}

			/// <summary>
			/// Builder accessor for the types
			/// </summary>
			public virtual List<Type.Builder> Types {
				get {
					if (_types == null) {
                        _types = new List<Type.Builder>();
					}
					return _types;
				}
			}

			/// <summary>
			/// Convenience accessor for all of the producers. This list does not grow dynamically.
			/// </summary>
			public virtual List<IBuilder> Producers {
				get {
					List<IBuilder> producers = new List<IBuilder>();
					producers.AddRange(Creators);
					producers.AddRange(Contributors);
					producers.AddRange(Publishers);
					producers.AddRange(PointOfContacts);
					return (producers);
				}
			}

			/// <summary>
			/// Builder accessor for creators
			/// </summary>
			public virtual List<Creator.Builder> Creators {
				get {
					if (_creators == null) {
                        _creators = new List<Creator.Builder>();
					}
					return _creators;
				}
			}

			/// <summary>
			/// Builder accessor for contributors
			/// </summary>
			public virtual List<Contributor.Builder> Contributors {
				get {
					if (_contributors == null) {
                        _contributors = new List<Contributor.Builder>();
					}
					return _contributors;
				}
			}

			/// <summary>
			/// Builder accessor for publishers
			/// </summary>
			public virtual List<Publisher.Builder> Publishers {
				get {
					if (_publishers == null) {
                        _publishers = new List<Publisher.Builder>();
					}
					return _publishers;
				}
			}

			/// <summary>
			/// Builder accessor for points of contact
			/// </summary>
			public virtual List<PointOfContact.Builder> PointOfContacts {
				get {
					if (_pointOfContacts == null) {
                        _pointOfContacts = new List<PointOfContact.Builder>();
					}
					return _pointOfContacts;
				}
			}

			/// <summary>
			/// Builder accessor for the format
			/// </summary>
			public virtual Format.Builder Format {
				get {
					if (_format == null) {
						_format = new Format.Builder();
					}
					return _format;
				}
                set { _format = value; }
			}


			/// <summary>
			/// Builder accessor for the subjectCoverages
			/// </summary>
			public virtual List<SubjectCoverage.Builder> SubjectCoverages {
				get {
					if (_subjectCoverages == null) {
                        _subjectCoverages = new List<SubjectCoverage.Builder>();
					}
					return _subjectCoverages;
				}
			}

			/// <summary>
			/// Builder accessor for the virtualCoverages
			/// </summary>
			public virtual List<VirtualCoverage.Builder> VirtualCoverages {
				get {
					if (_virtualCoverages == null) {
                        _virtualCoverages = new List<VirtualCoverage.Builder>();
					}
					return _virtualCoverages;
				}
			}

			/// <summary>
			/// Builder accessor for the temporalCoverages
			/// </summary>
			public virtual List<TemporalCoverage.Builder> TemporalCoverages {
				get {
					if (_temporalCoverages == null) {
                        _temporalCoverages = new List<TemporalCoverage.Builder>();
					}
					return _temporalCoverages;
				}
			}

			/// <summary>
			/// Builder accessor for the geospatialCoverages
			/// </summary>
			public virtual List<GeospatialCoverage.Builder> GeospatialCoverages {
				get {
					if (_geospatialCoverages == null) {
                        _geospatialCoverages = new List<GeospatialCoverage.Builder>();
					}
					return _geospatialCoverages;
				}
			}

			/// <summary>
			/// Builder accessor for the relatedResources
			/// </summary>
			public virtual List<RelatedResource.Builder> RelatedResources {
				get {
					if (_relatedResources == null) {
                        _relatedResources = new List<RelatedResource.Builder>();
					}
					return _relatedResources;
				}
			}

			/// <summary>
			/// Builder accessor for the resourceManagement
			/// </summary>
			public virtual ResourceManagement.Builder ResourceManagement {
				get {
					if (_resourceManagement == null) {
						_resourceManagement = new ResourceManagement.Builder();
					}
					return _resourceManagement;
				}
                set { _resourceManagement = value; }
			}


			/// <summary>
			/// Builder accessor for the security
			/// </summary>
			public virtual Security.Builder Security {
				get {
					if (_security == null) {
						_security = new Security.Builder();
					}
					return _security;
				}
                set
                {
                    _security = value;
                }
			}


			/// <summary>
			/// Builder accessor for the extensibleElements
			/// </summary>
			public virtual List<ExtensibleElement.Builder> ExtensibleElements {
				get {
					if (_extensibleElements == null) {
                        _extensibleElements = new List<ExtensibleElement.Builder>();
					}
					return _extensibleElements;
				}
			}

			/// <summary>
			/// Builder accessor for the createDate attribute
			/// </summary>
			public virtual string CreateDate {
				get {
					return _createDate;
				}
                set { _createDate = value; }
			}


			/// <summary>
			/// Accessor for the resourceElement attribute
			/// </summary>
			public virtual bool? ResourceElement {
				get {
					return _resourceElement;
				}
                set { _resourceElement = value; }
			}


			/// <summary>
			/// Builder accessor for the compliesWith attribute
			/// </summary>
			public virtual List<string> CompliesWiths {
				get {
					if (_compliesWiths == null) {
                        _compliesWiths = new List<string>();
					}
					return _compliesWiths;
				}
                set { _compliesWiths = value; }
			}



			/// <summary>
			/// Builder accessor for the NTK DESVersion
			/// </summary>
			public virtual int? NtkDESVersion {
				get {
					return _ntkDESVersion;
				}
                set { _ntkDESVersion = value; }
			}


			/// <summary>
			/// Builder accessor for the ISM DESVersion
			/// </summary>
			public virtual int? IsmDESVersion {
				get {
					return _ismDESVersion;
				}
                set { _ismDESVersion = value; }
			}


			/// <summary>
			/// Builder accessor for the securityAttributes
			/// </summary>
			public virtual SecurityAttributes.Builder SecurityAttributes {
				get {
					if (_securityAttributes == null) {
						_securityAttributes = new SecurityAttributes.Builder();
					}
					return _securityAttributes;
				}
                set { _securityAttributes = value; }
			}


			/// <summary>
			/// Builder accessor for the noticeAttributes
			/// </summary>
			public virtual NoticeAttributes.Builder NoticeAttributes {
				get {
					if (_noticeAttributes == null) {
						_noticeAttributes = new NoticeAttributes.Builder();
					}
					return _noticeAttributes;
				}
                set
                {
                    _noticeAttributes = value;
                }
			}


			/// <summary>
			/// Builder accessor for the extensibleAttributes
			/// </summary>
			public virtual ExtensibleAttributes.Builder ExtensibleAttributes {
				get {
					if (_extensibleAttributes == null) {
						_extensibleAttributes = new ExtensibleAttributes.Builder();
					}
					return _extensibleAttributes;
				}
                set
                {
                    _extensibleAttributes = value;
                }
			}

		}
	}
}