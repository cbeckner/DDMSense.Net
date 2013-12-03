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
namespace DDMSSense.DDMS.Summary {


	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;
    using System;

	/// <summary>
	/// An immutable implementation of the ddms:relatedResource component.
	/// 
	/// <para>Before DDMS 4.0.1, ddms:RelatedResources was the top-level component (0-many in a resource) and contained
	/// 1 to many ddms:relatedResource components. Starting in DDMS 4.0.1, the ddms:RelatedResources component was
	/// removed, and the ddms:relatedResource now contains all of the parent information (relationship and direction).</para>
	/// 
	/// <para>The element-based constructor for this class can automatically handle these cases, and will automatically
	/// mediate the Text/HTML/XML output:</para>
	/// <ul>
	/// 	<li>A pre-DDMS 4.0.1 ddms:RelatedResources element containing 1 ddms:relatedResource.</li>
	///  <li>A post-DDMS 4.0.1 ddms:relatedResource element.</li>
	/// </ul>
	/// <para>If you have a case where a pre-DDMS 4.0.1 ddms:RelatedResources element contained 5 ddms:relatedResource
	/// elements, the Resource class will automatically mediate it to create 5 RelatedResource instances. If an
	/// old-fashioned parent element containing multiple children is loaded in the element-based constructor,
	/// only the first child will be processed, and a warning will be provided.</para>
	///  
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>A non-empty relationship attribute is required.</li>
	/// <li>A non-empty qualifier value is required.</li>
	/// <li>A non-empty value attribute is required.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:link</u>: a link for the resource (1-many required), implemented as a <seealso cref="Link"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:relationship</u>: A URI representing a relationship of some relationship type between the resource being
	/// described and other resources. (required)<br />
	/// <u>ddms:direction</u>: Used to indicate the direction of the relationship between the resource being described and
	/// the target related resource. Valid values are "inbound," "outbound," and "bidirectional". (optional)
	/// <u>ddms:qualifier</u>: A URI specifying the formal identification system or encoding scheme by which the identifier
	/// value is to be interpreted. (required)<br />
	/// <u>ddms:value</u>: an unambiguous reference to the resource within a given context. An internal, external, and/or
	/// universal identification number for a data asset or resource. (required)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are optional.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class RelatedResource : AbstractQualifierValue {

		private List<Link> _links = null;
		private SecurityAttributes _securityAttributes = null;

		/// <summary>
		/// The value for an inbound direction. </summary>
		public const string INBOUND_DIRECTION = "inbound";

		/// <summary>
		/// The value for an outbound direction. </summary>
		public const string OUTBOUND_DIRECTION = "outbound";

		/// <summary>
		/// The value for an bidirectional direction. </summary>
		public const string BIDRECTIONAL_DIRECTION = "bidirectional";

		/// <summary>
		/// The pre-DDMS 4.0.1 name of the nested resource elements </summary>
		public const string OLD_INNER_NAME = "RelatedResource";

		private static List<string> RELATIONSHIP_DIRECTIONS = new List<string>();
		static RelatedResource() {
			RELATIONSHIP_DIRECTIONS.Add(INBOUND_DIRECTION);
			RELATIONSHIP_DIRECTIONS.Add(OUTBOUND_DIRECTION);
			RELATIONSHIP_DIRECTIONS.Add(BIDRECTIONAL_DIRECTION);
		}

		private const string RELATIONSHIP_NAME = "relationship";
		private const string DIRECTION_NAME = "direction";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public RelatedResource(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public RelatedResource(Element element) {
			try {
				Util.RequireDDMSValue("element", element);
				SetXOMElement(element, false);
				Element innerElement = InnerElement;
				if (innerElement != null) {
					_links = new List<Link>();
					IEnumerable<Element> links = innerElement.Elements(XName.Get(Link.GetName(DDMSVersion), Namespace));
                    foreach (var link in links)
                        _links.Add(new Link(link));
				}
				_securityAttributes = new SecurityAttributes(element);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="links"> the xlinks </param>
		/// <param name="relationship"> the relationship attribute (required) </param>
		/// <param name="direction"> the relationship direction (optional) </param>
		/// <param name="qualifier"> the value of the qualifier attribute </param>
		/// <param name="value"> the value of the value attribute </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public RelatedResource(java.util.List<Link> links, String relationship, String direction, String qualifier, String value, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public RelatedResource(List<Link> links, string relationship, string direction, string qualifier, string value, SecurityAttributes securityAttributes) {
			try {
				if (links == null) {
					links = new List<Link>();
				}
				DDMSVersion version = DDMSVersion.GetCurrentVersion();
				Element element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
				Util.AddDDMSAttribute(element, RELATIONSHIP_NAME, relationship);
				Util.AddDDMSAttribute(element, DIRECTION_NAME, direction);
				Element innerElement = (version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement(OLD_INNER_NAME, null));
				Util.AddDDMSAttribute(innerElement, QUALIFIER_NAME, qualifier);
				Util.AddDDMSAttribute(innerElement, VALUE_NAME, value);
				foreach (Link link in links) {
					innerElement.Add(link.XOMElementCopy);
				}

				if (!version.IsAtLeast("4.0.1")) {
					element.Add(innerElement);
				}
				_links = links;
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Asserts that a direction is valid.
		/// </summary>
		/// <param name="direction">	the string to check </param>
		/// <exception cref="InvalidDDMSException"> if the value is null, empty or invalid. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void validateRelationshipDirection(String direction) throws DDMSSense.DDMS.InvalidDDMSException
		public static void ValidateRelationshipDirection(string direction) {
			Util.RequireDDMSValue("relationship direction", direction);
			if (!RELATIONSHIP_DIRECTIONS.Contains(direction)) {
				throw new InvalidDDMSException("The direction attribute must be one of " + RELATIONSHIP_DIRECTIONS);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>A relationship exists and is not empty.</li>
		/// <li>The relationship is a valid URI.</li>
		/// <li>If set, the direction has a valid value.</li>
		/// <li>A qualifier exists and is not empty.</li>
		/// <li>A value exists and is not empty.</li>
		/// <li>The qualifier is a valid URI.</li>
		/// <li>At least 1 link exists.</li>
		/// <li>No link contains security attributes.</li>
		/// <li>Does NOT validate that the value is valid against the qualifier's vocabulary.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException">  if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, RelatedResource.GetName(DDMSVersion));
			Util.RequireDDMSValue("relationship attribute", Relationship);
			Util.RequireDDMSValidURI(Relationship);
			if (!String.IsNullOrEmpty(Direction)) {
				ValidateRelationshipDirection(Direction);
			}
			Util.RequireDDMSValue("qualifier attribute", Qualifier);
			Util.RequireDDMSValue("value attribute", Value);
			Util.RequireDDMSValidURI(Qualifier);
			if (Links.Count == 0) {
				throw new InvalidDDMSException("At least 1 link must exist.");
			}
			foreach (Link link in Links) {
				if (!link.SecurityAttributes.Empty) {
					throw new InvalidDDMSException("Security attributes cannot be applied to links in a related resource.");
				}
			}

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>Before DDMS 4.0.1, warn if the parent component contains more than 1 ddms:relatedResource.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (!DDMSVersion.IsAtLeast("4.0.1")) {
                IEnumerable<Element> elements = Element.Elements(XName.Get(OLD_INNER_NAME, Namespace));
				if (elements.Count() > 1) {
					AddWarning("A ddms:RelatedResources element contains more than 1 ddms:relatedResource. " + "To ensure consistency between versions of DDMS, each ddms:RelatedResources element " + "should contain only 1 ddms:RelatedResource. DDMSence will only process the first child.");
				}
			}

			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			if (!DDMSVersion.GetCurrentVersion().IsAtLeast("4.0.1")) {
				localPrefix += "RelatedResource.";
			}
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + RELATIONSHIP_NAME, Relationship));
			text.Append(BuildOutput(isHTML, localPrefix + DIRECTION_NAME, Direction));
			text.Append(BuildOutput(isHTML, localPrefix + QUALIFIER_NAME, Qualifier));
			text.Append(BuildOutput(isHTML, localPrefix + VALUE_NAME, Value));
			text.Append(BuildOutput(isHTML, localPrefix, Links));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(Links);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is RelatedResource)) {
				return (false);
			}
			RelatedResource test = (RelatedResource) obj;
			return (Relationship.Equals(test.Relationship) && Direction.Equals(test.Direction));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Relationship.GetHashCode();
			result = 7 * result + Direction.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return (version.IsAtLeast("4.0.1") ? "relatedResource" : "relatedResources");
		}

		/// <summary>
		/// Accessor for the element which contains the links, qualifier and value. Before DDMS 4.0.1,
		/// this is a wrapper element called ddms:RelatedResource. Starting in DDMS 4.0.1, it is the ddms:relatedResource
		/// element itself.
		/// </summary>
		private Element InnerElement {
			get {
				return (DDMSVersion.IsAtLeast("4.0.1") ? Element : GetChild(OLD_INNER_NAME));
			}
		}

		/// <summary>
		/// Accessor for the links (1 to many).
		/// </summary>
		/// <returns> unmodifiable List </returns>
		public List<Link> Links {
			get {
				return _links;
			}
		}

		/// <summary>
		/// Accessor for the relationship attribute
		/// </summary>
		public string Relationship {
			get {
				return (GetAttributeValue(RELATIONSHIP_NAME));
			}
		}

		/// <summary>
		/// Accessor for the direction attribute (may be empty)
		/// </summary>
		public string Direction {
			get {
				return (GetAttributeValue(DIRECTION_NAME));
			}
		}

		/// <summary>
		/// Accessor for the value of the qualifier attribute
		/// </summary>
		public override string Qualifier {
			get {
				Element innerElement = InnerElement;
                string attrValue = innerElement.Attribute(XName.Get(QUALIFIER_NAME, Namespace)).Value;
				return (Util.GetNonNullString(attrValue));
			}
		}

		/// <summary>
		/// Accessor for the value of the value attribute
		/// </summary>
		public override string Value {
			get {
				Element innerElement = InnerElement;
                string attrValue = innerElement.Attribute(XName.Get(VALUE_NAME, Namespace)).Value;
				return (Util.GetNonNullString(attrValue));
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
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		public class Builder : AbstractQualifierValue.Builder {
			internal const long SerialVersionUID = 5430464017408842022L;
			internal string _relationship;
			internal string _direction;
			internal List<Link.Builder> _links;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(RelatedResource resource) : base(resource) {
				Relationship = resource.Relationship;
				Direction = resource.Direction;
				foreach (Link link in resource.Links) {
					Links.Add(new Link.Builder(link));
				}
				SecurityAttributes = new SecurityAttributes.Builder(resource.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public RelatedResource commit() throws DDMSSense.DDMS.InvalidDDMSException
			public override RelatedResource Commit() {
				if (Empty) {
					return (null);
				}
				List<Link> links = new List<Link>();
				foreach (Link.Builder builder in Links) {
					Link link = builder.Commit();
					if (link != null) {
						links.Add(link);
					}
				}
				return (new RelatedResource(links, Relationship, Direction, Qualifier, Value, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public override bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in Links) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (base.Empty && !hasValueInList && String.IsNullOrEmpty(Relationship) && String.IsNullOrEmpty(Direction) && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the links
			/// </summary>
			public virtual List<Link.Builder> Links {
				get {
					if (_links == null) {
                        _links = new List<Link.Builder>();
					}
					return _links;
				}
			}

			/// <summary>
			/// Builder accessor for the relationship attribute
			/// </summary>
			public virtual string Relationship {
				get {
					return _relationship;
				}
                set { _relationship = value; }
			}


			/// <summary>
			/// Builder accessor for the direction attribute
			/// </summary>
			public virtual string Direction {
				get {
					return _direction;
				}
                set { _direction = value; }
			}


			/// <summary>
			/// Builder accessor for the Security Attributes
			/// </summary>
			public virtual SecurityAttributes.Builder SecurityAttributes {
				get {
					if (_securityAttributes == null) {
						_securityAttributes = new SecurityAttributes.Builder();
					}
					return _securityAttributes;
				}
                set
                {
                    _securityAttributes = value;
                }
			}

		}
	}
}