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
namespace DDMSSense.DDMS.Summary {


	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using DDMSSense.DDMS.Summary;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:geospatialCoverage.
	/// 
	/// <para>
	/// Before DDMS 4.0.1, a geospatialCoverage element contains a nested GeospatialExtent element. Because
	/// DDMS does not decorate this element with any special attributes, it is not implemented as a Java object.
	/// Starting in DDMS 4.0.1, the GeospatialExtent wrapper has been removed.
	/// </para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>No more than 1 each of geographicIdentifier, boundingBox, boundingGeometry, postalAddress, or verticalExtent can 
	/// be used. The schema seems to support this assertion with explicit restrictions on those elements, but the enclosing 
	/// xs:choice element allows multiples. From the specification, "The intent of Geospatial Coverage is to provide 
	/// logically and semantically consistent information.  Flexibility in the specification does not absolve end users 
	/// using Geospatial Coverage from expressing information in a meaningful manner.  Users should ensure that combinations 
	/// of elements are appropriately relatable, consistent, meaningful, and useful for enterprise discovery."</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:geographicIdentifier</u>: an identifier (0-1 optional) implemented as a <seealso cref="GeographicIdentifier"/><br />
	/// <u>ddms:boundingBox</u>: a bounding box (0-1 optional) implemented as a <seealso cref="BoundingBox"/><br />
	/// <u>ddms:boundingGeometry</u>: a set of bounding geometry (0-1 optional) implemented as a 
	/// <seealso cref="BoundingGeometry"/><br />
	/// <u>ddms:postalAddress</u>: an address (0-1 optional), implemented as a (@link PostalAddress)<br />
	/// <u>ddms:verticalExtent</u>: an extent (0-1 optional), implemented as a (@link VerticalExtent)<br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:precedence</u>: priority claimed or received as a result of preeminence. Used with country codes (optional, 
	/// starting in DDMS 4.0.1)
	/// <u>ddms:order</u>: specifies a user-defined order of an element within the given document (optional, starting in 
	/// DDMS 4.0.1)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are optional. (starting in DDMS 3.0)
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class GeospatialCoverage : AbstractBaseComponent {

		private GeographicIdentifier _geographicIdentifier = null;
		private BoundingBox _boundingBox = null;
		private BoundingGeometry _boundingGeometry = null;
		private PostalAddress _postalAddress = null;
		private VerticalExtent _verticalExtent = null;
		private SecurityAttributes _securityAttributes = null;

		private const string GEOSPATIAL_EXTENT_NAME = "GeospatialExtent";
		private const string PRECEDENCE_NAME = "precedence";
		private const string ORDER_NAME = "order";

		private static readonly List<string> VALID_PRECEDENCE_VALUES = new List<string>();
		static GeospatialCoverage() {
			VALID_PRECEDENCE_VALUES.Add("Primary");
			VALID_PRECEDENCE_VALUES.Add("Secondary");
		}

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public GeospatialCoverage(Element element) {
			try {
				Util.RequireDDMSValue("geographicIdentifier element", element);
				SetXOMElement(element, false);
				Element extElement = ExtentElement;
				if (extElement != null) {
					DDMSVersion version = DDMSVersion.GetVersionForNamespace(Namespace);
					Element geographicIdentifierElement = extElement.Element(XName.Get(GeographicIdentifier.GetName(version), Namespace));
					if (geographicIdentifierElement != null) {
						_geographicIdentifier = new GeographicIdentifier(geographicIdentifierElement);
					}
					Element boundingBoxElement = extElement.Element(XName.Get(BoundingBox.GetName(version), Namespace));
					if (boundingBoxElement != null) {
						_boundingBox = new BoundingBox(boundingBoxElement);
					}
					Element boundingGeometryElement = extElement.Element(XName.Get(BoundingGeometry.GetName(version), Namespace));
					if (boundingGeometryElement != null) {
						_boundingGeometry = new BoundingGeometry(boundingGeometryElement);
					}
					Element postalAddressElement = extElement.Element(XName.Get(PostalAddress.GetName(version), Namespace));
					if (postalAddressElement != null) {
						_postalAddress = new PostalAddress(postalAddressElement);
					}
					Element verticalExtentElement = extElement.Element(XName.Get(VerticalExtent.GetName(version), Namespace));
					if (verticalExtentElement != null) {
						_verticalExtent = new VerticalExtent(verticalExtentElement);
					}
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
		/// <param name="geographicIdentifier"> an identifier (0-1 optional) </param>
		/// <param name="boundingBox"> a bounding box (0-1 optional) </param>
		/// <param name="boundingGeometry"> a set of bounding geometry (0-1 optional) </param>
		/// <param name="postalAddress"> an address (0-1 optional) </param>
		/// <param name="verticalExtent"> an extent (0-1 optional) </param>
		/// <param name="precedence"> the precedence attribute (optional, starting in DDMS 4.0.1) </param>
		/// <param name="order"> the order attribute (optional, starting in DDMS 4.0.1) </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public GeospatialCoverage(GeographicIdentifier geographicIdentifier, BoundingBox boundingBox, BoundingGeometry boundingGeometry, PostalAddress postalAddress, VerticalExtent verticalExtent, string precedence, int? order, SecurityAttributes securityAttributes) {
			try {
				Element coverageElement = Util.BuildDDMSElement(GeospatialCoverage.GetName(DDMSVersion.GetCurrentVersion()), null);

				Element element = DDMSVersion.GetCurrentVersion().IsAtLeast("4.0.1") ? coverageElement : Util.BuildDDMSElement(GEOSPATIAL_EXTENT_NAME, null);
				if (geographicIdentifier != null) {
					element.Add(geographicIdentifier.XOMElementCopy);
				}
				if (boundingBox != null) {
					element.Add(boundingBox.XOMElementCopy);
				}
				if (boundingGeometry != null) {
					element.Add(boundingGeometry.XOMElementCopy);
				}
				if (postalAddress != null) {
					element.Add(postalAddress.XOMElementCopy);
				}
				if (verticalExtent != null) {
					element.Add(verticalExtent.XOMElementCopy);
				}
				Util.AddDDMSAttribute(coverageElement, PRECEDENCE_NAME, precedence);
				if (order != null) {
					Util.AddDDMSAttribute(coverageElement, ORDER_NAME, order.ToString());
				}

				if (!DDMSVersion.GetCurrentVersion().IsAtLeast("4.0.1")) {
					coverageElement.Add(element);
				}

				_geographicIdentifier = geographicIdentifier;
				_boundingBox = boundingBox;
				_boundingGeometry = boundingGeometry;
				_postalAddress = postalAddress;
				_verticalExtent = verticalExtent;
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(coverageElement);
				SetXOMElement(coverageElement, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>At least 1 of geographicIdentifier, boundingBox, boundingGeometry, postalAddress, or verticalExtent must 
		/// be used.</li>
		/// <li>No more than 1 geographicIdentifier, boundingBox, boundingGeometry, postalAddress, or verticalExtent can 
		/// be used.</li>
		/// <li>If a geographicIdentifer is used and contains a facilityIdentifier, no other subcomponents can be used.</li>
		/// <li>The order and precedence cannot be used until DDMS 4.0.1 or later.</li>
		/// <li>If set, the precedence must be "Primary" or "Secondary".</li>
		/// <li>If a precedence is set, this coverage must contain a geographicIdentifier with a countryCode.</li>
		/// <li>Does not validate the value of the order attribute (this is done at the Resource level).</li>
		/// <li>The SecurityAttributes do not exist until DDMS 3.0 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, GeospatialCoverage.GetName(DDMSVersion));
			Element extElement = ExtentElement;
			Util.RequireDDMSValue("GeospatialExtent element", extElement);

			Util.RequireBoundedChildCount(extElement, GeographicIdentifier.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(extElement, BoundingBox.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(extElement, BoundingGeometry.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(extElement, PostalAddress.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(extElement, VerticalExtent.GetName(DDMSVersion), 0, 1);

			int validComponents = 0;
			if (GeographicIdentifier != null) {
				validComponents++;
			}
			if (BoundingBox != null) {
				validComponents++;
			}
			if (BoundingGeometry != null) {
				validComponents++;
			}
			if (PostalAddress != null) {
				validComponents++;
			}
			if (VerticalExtent != null) {
				validComponents++;
			}
			if (validComponents == 0) {
				throw new InvalidDDMSException("At least 1 of geographicIdentifier, boundingBox, boundingGeometry, " + "postalAddress, or verticalExtent must be used.");
			}
			if (HasFacilityIdentifier() && validComponents > 1) {
				throw new InvalidDDMSException("A geographicIdentifier containing a facilityIdentifier cannot be used in " + "tandem with any other coverage elements.");
			}

			// Should be reviewed as additional versions of DDMS are supported.
			if (!DDMSVersion.IsAtLeast("4.0.1") && !String.IsNullOrEmpty(Precedence)) {
				throw new InvalidDDMSException("The ddms:precedence attribute cannot be used until DDMS 4.0.1 or later.");
			}
			if (!DDMSVersion.IsAtLeast("4.0.1") && Order != null) {
				throw new InvalidDDMSException("The ddms:order attribute cannot be used until DDMS 4.0.1 or later.");
			}
			if (!DDMSVersion.IsAtLeast("3.0") && !SecurityAttributes.Empty) {
				throw new InvalidDDMSException("Security attributes cannot be applied to this component until DDMS 3.0 or later.");
			}
			if (!String.IsNullOrEmpty(Precedence)) {
				if (!VALID_PRECEDENCE_VALUES.Contains(Precedence)) {
					throw new InvalidDDMSException("The ddms:precedence attribute must have a value from: " + VALID_PRECEDENCE_VALUES);
				}
				if (GeographicIdentifier == null || GeographicIdentifier.CountryCode == null) {
					throw new InvalidDDMSException("The ddms:precedence attribute should only be applied to a " + "geospatialCoverage containing a country code.");
				}
			}

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getLocatorSuffix() </seealso>
		protected internal override string LocatorSuffix {
			get {
                return (DDMSVersion.IsAtLeast("4.0.1") ? "" : ValidationMessage.ELEMENT_PREFIX + Element.Prefix + ":" + GEOSPATIAL_EXTENT_NAME);
			}
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			if (!DDMSVersion.IsAtLeast("4.0.1")) {
				localPrefix += GEOSPATIAL_EXTENT_NAME + ".";
			}
			StringBuilder text = new StringBuilder();
			if (GeographicIdentifier != null) {
				text.Append(GeographicIdentifier.GetOutput(isHTML, localPrefix, ""));
			}
			if (BoundingBox != null) {
				text.Append(BoundingBox.GetOutput(isHTML, localPrefix, ""));
			}
			if (BoundingGeometry != null) {
				text.Append(BoundingGeometry.GetOutput(isHTML, localPrefix, ""));
			}
			if (PostalAddress != null) {
				text.Append(PostalAddress.GetOutput(isHTML, localPrefix, ""));
			}
			if (VerticalExtent != null) {
				text.Append(VerticalExtent.GetOutput(isHTML, localPrefix, ""));
			}
			text.Append(BuildOutput(isHTML, localPrefix + PRECEDENCE_NAME, Precedence));
			if (Order != null) {
				text.Append(BuildOutput(isHTML, localPrefix + ORDER_NAME, Convert.ToString(Order)));
			}
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(BoundingBox);
				list.Add(BoundingGeometry);
				list.Add(GeographicIdentifier);
				list.Add(PostalAddress);
				list.Add(VerticalExtent);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is GeospatialCoverage)) {
				return (false);
			}
			GeospatialCoverage test = (GeospatialCoverage) obj;
			return (Precedence.Equals(test.Precedence) && Util.NullEquals(Order, test.Order));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + Precedence.GetHashCode();
			if (Order != null) {
				result = 7 * result + Order.GetHashCode();
			}
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("geospatialCoverage");
		}

		/// <summary>
		/// Accessor for the element which contains the child elementsnt. Before DDMS 4.0.1, this is a wrapper element called
		/// ddms:GeospatialExtent. Starting in DDMS 4.0.1, it is the ddms:geospatialCoverage element itself.
		/// </summary>
		private Element ExtentElement {
			get {
				return (DDMSVersion.IsAtLeast("4.0.1") ? Element : GetChild(GEOSPATIAL_EXTENT_NAME));
			}
		}

		/// <summary>
		/// Accessor for the precedence attribute.
		/// </summary>
		public string Precedence {
			get {
				return (GetAttributeValue(PRECEDENCE_NAME));
			}
		}

		/// <summary>
		/// Accessor for the order attribute.
		/// </summary>
		public int? Order {
			get {
				string order = GetAttributeValue(ORDER_NAME);
                if (String.IsNullOrEmpty(order))
                    return null;
                else
                    return Convert.ToInt32(order);
			}
			
		}

		/// <summary>
		/// Accessor for whether this geospatialCoverage is using a facility identifier.
		/// </summary>
		public bool HasFacilityIdentifier() {
			return (GeographicIdentifier != null && GeographicIdentifier.HasFacilityIdentifier());
		}

		/// <summary>
		/// Accessor for the geographicIdentifier. May return null if not used.
		/// </summary>
		public GeographicIdentifier GeographicIdentifier {
			get {
				return _geographicIdentifier;
			}
			set {
					_geographicIdentifier = value;
			}
		}

		/// <summary>
		/// Accessor for the boundingBox. May return null if not used.
		/// </summary>
		public BoundingBox BoundingBox {
			get {
				return _boundingBox;
			}
			set {
					_boundingBox = value;
			}
		}

		/// <summary>
		/// Accessor for the boundingGeometry. May return null if not used.
		/// </summary>
		public BoundingGeometry BoundingGeometry {
			get {
				return _boundingGeometry;
			}
			set {
					_boundingGeometry = value;
			}
		}

		/// <summary>
		/// Accessor for the postalAddress. May return null if not used.
		/// </summary>
		public PostalAddress PostalAddress {
			get {
				return (_postalAddress);
			}
			set {
					_postalAddress = value;
			}
		}

		/// <summary>
		/// Accessor for the verticalExtent. May return null if not used.
		/// </summary>
		public VerticalExtent VerticalExtent {
			get {
				return (_verticalExtent);
			}
			set {
					_verticalExtent = value;
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes.  Will always be non-null, even if it has no values set.
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
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 2895705456552847432L;
			internal BoundingBox.Builder _boundingBox;
			internal BoundingGeometry.Builder _boundingGeometry;
			internal GeographicIdentifier.Builder _geographicIdentifier;
			internal PostalAddress.Builder _postalAddress;
			internal VerticalExtent.Builder _verticalExtent;
			internal string _precedence;
			internal int? _order;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(GeospatialCoverage coverage) {
				if (coverage.BoundingBox != null) {
					BoundingBox = new BoundingBox.Builder(coverage.BoundingBox);
				}
				if (coverage.BoundingGeometry != null) {
					BoundingGeometry = new BoundingGeometry.Builder(coverage.BoundingGeometry);
				}
				if (coverage.GeographicIdentifier != null) {
					GeographicIdentifier = new GeographicIdentifier.Builder(coverage.GeographicIdentifier);
				}
				if (coverage.PostalAddress != null) {
					PostalAddress = new PostalAddress.Builder(coverage.PostalAddress);
				}
				if (coverage.VerticalExtent != null) {
					VerticalExtent = new VerticalExtent.Builder(coverage.VerticalExtent);
				}
				Precedence = coverage.Precedence;
				Order = coverage.Order;
				SecurityAttributes = new SecurityAttributes.Builder(coverage.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				return (Empty ? null : new GeospatialCoverage((GeographicIdentifier)GeographicIdentifier.Commit(), (BoundingBox)BoundingBox.Commit(), (BoundingGeometry)BoundingGeometry.Commit(), (PostalAddress)PostalAddress.Commit(), (VerticalExtent)VerticalExtent.Commit(), Precedence, Order, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (GeographicIdentifier.Empty && BoundingBox.Empty && BoundingGeometry.Empty && PostalAddress.Empty && VerticalExtent.Empty && String.IsNullOrEmpty(Precedence) && Order == null && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the boundingBox
			/// </summary>
			public virtual BoundingBox.Builder BoundingBox {
				get {
					if (_boundingBox == null) {
						_boundingBox = new BoundingBox.Builder();
					}
					return _boundingBox;
				}
                set { _boundingBox = value; }
			}


			/// <summary>
			/// Builder accessor for the boundingGeometry
			/// </summary>
			public virtual BoundingGeometry.Builder BoundingGeometry {
				get {
					if (_boundingGeometry == null) {
						_boundingGeometry = new BoundingGeometry.Builder();
					}
					return _boundingGeometry;
				}
                set { _boundingGeometry = value; }
			}


			/// <summary>
			/// Builder accessor for the geographicIdentifier
			/// </summary>
			public virtual GeographicIdentifier.Builder GeographicIdentifier {
				get {
					if (_geographicIdentifier == null) {
						_geographicIdentifier = new GeographicIdentifier.Builder();
					}
					return _geographicIdentifier;
				}
                set { _geographicIdentifier = value; }
			}


			/// <summary>
			/// Builder accessor for the postalAddress
			/// </summary>
			public virtual PostalAddress.Builder PostalAddress {
				get {
					if (_postalAddress == null) {
						_postalAddress = new PostalAddress.Builder();
					}
					return _postalAddress;
				}
                set { _postalAddress = value; }
			}


			/// <summary>
			/// Builder accessor for the verticalExtent
			/// </summary>
			public virtual VerticalExtent.Builder VerticalExtent {
				get {
					if (_verticalExtent == null) {
						_verticalExtent = new VerticalExtent.Builder();
					}
					return _verticalExtent;
				}
                set { _verticalExtent = value; }
			}


			/// <summary>
			/// Builder accessor for the precedence
			/// </summary>
			public virtual string Precedence {
				get {
					return _precedence;
				}
                set { _precedence = value; }
			}


			/// <summary>
			/// Builder accessor for the order
			/// </summary>
			public virtual int? Order {
				get {
					return _order;
				}
                set { _order = value; }
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
                set { _securityAttributes = value; }
			}

		}
	}
}