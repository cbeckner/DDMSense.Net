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
namespace DDMSSense.DDMS.Summary.Gml {


	using Element = System.Xml.Linq.XElement;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using DDMSSense.DDMS;

	/// <summary>
	/// An immutable implementation of gml:pos.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>A position must either have 2 coordinates (to comply with WGS84E_2D) or 3 coordinates (to comply with WGS84E_3D).
	/// </li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SRSAttributes"/></u>
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Position : AbstractBaseComponent {

		private SRSAttributes _srsAttributes = null;
		private List<double?> _coordinates = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Position(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public Position(Element element) {
			try {
				SetXOMElement(element, false);
				List<string> tuple = Util.GetXsListAsList(CoordinatesAsXsList);
				_coordinates = new List<double?>();
				foreach (string coordinate in tuple) {
					_coordinates.Add(GetStringAsDouble(coordinate));
				}
				_srsAttributes = new SRSAttributes(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data </summary>
		/// <param name="coordinates"> a list of either 2 or 3 coordinate Double values </param>
		/// <param name="srsAttributes"> the attribute group containing srsName, srsDimension, axisLabels, and uomLabels
		/// </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Position(java.util.List<Double> coordinates, SRSAttributes srsAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public Position(List<double?> coordinates, SRSAttributes srsAttributes) {
			try {
				if (coordinates == null) {
					coordinates = new List<double?>();
				}
				DDMSVersion version = DDMSVersion.GetCurrentVersion();
				Element element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, Util.GetXsList(coordinates));

				_coordinates = coordinates;
				_srsAttributes = SRSAttributes.GetNonNullInstance(srsAttributes);
				_srsAttributes.AddTo(element);
				SetXOMElement(element, true);
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
		/// <li>Each coordinate is a valid Double value.</li>
		/// <li>The position is represented by 2 or 3 coordinates.</li>
		/// <li>The first coordinate is a valid latitude.</li>
		/// <li>The second coordinate is a valid longitude.</li>
		/// <li>Does not perform any special validation on the third coordinate (height above ellipsoid).</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireQualifiedName(Element, Namespace, Position.GetName(DDMSVersion));
			foreach (double? coordinate in Coordinates) {
				Util.RequireDDMSValue("coordinate", coordinate);
			}
			if (!Util.IsBounded(Coordinates.Count, 2, 3)) {
				throw new InvalidDDMSException("A position must be represented by either 2 or 3 coordinates.");
			}
			Util.RequireValidLatitude(Coordinates[0]);
			Util.RequireValidLongitude(Coordinates[1]);

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>Include any validation warnings from the SRS attributes.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			AddWarnings(SRSAttributes.ValidationWarnings, true);
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix);
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, CoordinatesAsXsList));
			text.Append(SRSAttributes.GetOutput(isHTML, localPrefix + "."));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Position)) {
				return (false);
			}
			Position test = (Position) obj;
			return (SRSAttributes.Equals(test.SRSAttributes) && Coordinates.Count == test.Coordinates.Count && Util.ListEquals(Coordinates, test.Coordinates));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + SRSAttributes.HashCode();
			result = 7 * result + CoordinatesAsXsList.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("pos");
		}

		/// <summary>
		/// Accessor for the SRS Attributes. Will always be non-null, even if the attributes inside are not set.
		/// </summary>
		public SRSAttributes SRSAttributes {
			get {
				return (_srsAttributes);
			}
		}

		/// <summary>
		/// Accessor for the coordinates of the position. May return null, but cannot happen after instantiation.
		/// </summary>
		public List<double?> Coordinates {
			get {
				return _coordinates;
			}
		}

		/// <summary>
		/// Accessor for the String representation of the coordinates
		/// </summary>
		public string CoordinatesAsXsList {
			get {
                return (Element.Value);
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
			internal const long SerialVersionUID = 33638279863455987L;
			internal SRSAttributes.Builder _srsAttributes;
			internal List<Position.DoubleBuilder> _coordinates;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Position position) {
				SrsAttributes = new SRSAttributes.Builder(position.SRSAttributes);
				foreach (double? coord in position.Coordinates) {
					Coordinates.Add(new DoubleBuilder(coord));
				}
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Position commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual Position Commit() {
				if (Empty) {
					return (null);
				}
				List<double?> coordinates = new List<double?>();
				foreach (Position.DoubleBuilder builder in Coordinates) {
					double? coord = builder.Commit();
					if (coord != null) {
						coordinates.Add(coord);
					}
				}
				return (new Position(coordinates, SrsAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (Position.DoubleBuilder builder in Coordinates) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && SrsAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the SRS Attributes
			/// </summary>
			public virtual SRSAttributes.Builder SrsAttributes {
				get {
					if (_srsAttributes == null) {
						_srsAttributes = new SRSAttributes.Builder();
					}
					return _srsAttributes;
				}
				set {
					_srsAttributes = value;
				}
			}


			/// <summary>
			/// Builder accessor for the coordinates of the position
			/// </summary>
			public virtual List<Position.DoubleBuilder> Coordinates {
				get {
					if (_coordinates == null) {
                        _coordinates = new List<Position.DoubleBuilder>();
					}
					return _coordinates;
				}
			}
		}

		/// <summary>
		/// Builder for a Double
		/// 
		/// <para>This builder is implemented because the Java Double class does not have a no-arg constructor which can be
		/// hooked into a LazyList. Because the Builder returns a Double instead of an IDDMSComponent, it does not officially
		/// implement the IBuilder interface.</para>
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.9.0 </seealso>
		[Serializable]
		public class DoubleBuilder {
			internal const long SerialVersionUID = -5102193614065692204L;
			internal double? _value;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public DoubleBuilder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public DoubleBuilder(double? value) {
				Value = value;
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Double commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual double? Commit() {
				return (Empty ? null : Value);
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (Value == null);
				}
			}

			/// <summary>
			/// Builder accessor for the value
			/// </summary>
			public virtual double? Value {
				get {
					return _value;
				}
				set {
					_value = value;
				}
			}

		}
	}
}