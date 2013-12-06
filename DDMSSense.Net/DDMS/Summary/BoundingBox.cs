using System;
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
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:boundingBox.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:westBL</u>: westbound longitude (required)<br />
	/// <u>ddms:eastBL</u>: eastbound longitude (required)<br />
	/// <u>ddms:southBL</u>: northbound latitude (required)<br />
	/// <u>ddms:northBL</u>: southbound latitude (required)<br />
	/// Please note that the case of the nested elements changed starting in DDMS 4.0.1. Previously, the first letter of 
	/// each element was capitalized (i.e. WestBL/EastBL/SouthBL/NorthBL).
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class BoundingBox : AbstractBaseComponent {

		private double? _westBL = null;
		private double? _eastBL = null;
		private double? _southBL = null;
		private double? _northBL = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public BoundingBox(Element element) {
			try {
				Util.RequireDDMSValue("boundingBox element", element);
				SetXOMElement(element, false);
				_westBL = GetChildTextAsDouble(element, WestBLName);
				_eastBL = GetChildTextAsDouble(element, EastBLName);
				_southBL = GetChildTextAsDouble(element, SouthBLName);
				_northBL = GetChildTextAsDouble(element, NorthBLName);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="westBL"> the westbound longitude </param>
		/// <param name="eastBL"> the eastbound longitude </param>
		/// <param name="southBL"> the southbound latitude </param>
		/// <param name="northBL"> the northbound latitude </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public BoundingBox(double westBL, double eastBL, double southBL, double northBL) {
			try {
				Element element = Util.BuildDDMSElement(BoundingBox.GetName(DDMSVersion.GetCurrentVersion()), null);
				SetXOMElement(element, false);
				element.Add(Util.BuildDDMSElement(WestBLName, Convert.ToString(westBL)));
				element.Add(Util.BuildDDMSElement(EastBLName, Convert.ToString(eastBL)));
				element.Add(Util.BuildDDMSElement(SouthBLName, Convert.ToString(southBL)));
				element.Add(Util.BuildDDMSElement(NorthBLName, Convert.ToString(northBL)));
				_westBL = Convert.ToDouble(westBL);
				_eastBL = Convert.ToDouble(eastBL);
				_southBL = Convert.ToDouble(southBL);
				_northBL = Convert.ToDouble(northBL);
				Validate();
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
		/// <li>A westBL exists.</li>
		/// <li>An eastBL exists.</li>
		/// <li>A southBL exists.</li>
		/// <li>A northBL exists.</li>
		/// <li>westBL and eastBL must be between -180 and 180 degrees.</li>
		/// <li>southBL and northBL must be between -90 and 90 degrees.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, BoundingBox.GetName(DDMSVersion));
			Util.RequireDDMSValue("westbound longitude", WestBL);
			Util.RequireDDMSValue("eastbound longitude", EastBL);
			Util.RequireDDMSValue("southbound latitude", SouthBL);
			Util.RequireDDMSValue("northbound latitude", NorthBL);
			Util.RequireValidLongitude(WestBL);
			Util.RequireValidLongitude(EastBL);
			Util.RequireValidLatitude(SouthBL);
			Util.RequireValidLatitude(NorthBL);
			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + WestBLName, Convert.ToString(WestBL)));
			text.Append(BuildOutput(isHTML, localPrefix + EastBLName, Convert.ToString(EastBL)));
			text.Append(BuildOutput(isHTML, localPrefix + SouthBLName, Convert.ToString(SouthBL)));
			text.Append(BuildOutput(isHTML, localPrefix + NorthBLName, Convert.ToString(NorthBL)));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is BoundingBox)) {
				return (false);
			}
			BoundingBox test = (BoundingBox) obj;
			return (WestBL.Equals(test.WestBL) && EastBL.Equals(test.EastBL) && SouthBL.Equals(test.SouthBL) && NorthBL.Equals(test.NorthBL));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + WestBL.GetHashCode();
			result = 7 * result + EastBL.GetHashCode();
			result = 7 * result + SouthBL.GetHashCode();
			result = 7 * result + NorthBL.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("boundingBox");
		}

		/// <summary>
		/// Accessor for the name of the westbound longitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string WestBLName {
			get {
				return (DDMSVersion.IsAtLeast("4.0.1") ? "westBL" : "WestBL");
			}
		}

		/// <summary>
		/// Accessor for the name of the eastbound longitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string EastBLName {
			get {
				return (DDMSVersion.IsAtLeast("4.0.1") ? "eastBL" : "EastBL");
			}
		}

		/// <summary>
		/// Accessor for the name of the southbound latitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string SouthBLName {
			get {
				return (DDMSVersion.IsAtLeast("4.0.1") ? "southBL" : "SouthBL");
			}
		}

		/// <summary>
		/// Accessor for the name of the northbound latitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string NorthBLName {
			get {
				return (DDMSVersion.IsAtLeast("4.0.1") ? "northBL" : "NorthBL");
			}
		}

		/// <summary>
		/// Accessor for the westbound longitude.
		/// </summary>
		public double? WestBL {
			get {
				return (_westBL);
			}
			set {
					_westBL = value;
			}
		}

		/// <summary>
		/// Accessor for the eastbound longitude.
		/// </summary>
		public double? EastBL {
			get {
				return (_eastBL);
			}
			set {
					_eastBL = value;
			}
		}

		/// <summary>
		/// Accessor for the southbound latitude.
		/// </summary>
		public double? SouthBL {
			get {
				return (_southBL);
			}
			set {
					_southBL = value;
			}
		}

		/// <summary>
		/// Accessor for the northbound latitude.
		/// </summary>
		public double? NorthBL {
			get {
				return (_northBL);
			}
			set {
					_northBL = value;
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
			internal const long SerialVersionUID = -2364407215439097065L;
			internal double? _westBL;
			internal double? _eastBL;
			internal double? _southBL;
			internal double? _northBL;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(BoundingBox box) {
				_westBL = box.WestBL;
				_eastBL = box.EastBL;
				_southBL = box.SouthBL;
				_northBL = box.NorthBL;
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				// Check for existence of values before casting to primitives.
				if (WestBL == null || EastBL == null || SouthBL == null || NorthBL == null) {
					throw new InvalidDDMSException("A ddms:boundingBox requires two latitude and two longitude values.");
				}
				return (new BoundingBox((double)WestBL, (double)EastBL, (double)SouthBL, (double)NorthBL));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (WestBL == null && EastBL == null && SouthBL == null && NorthBL == null);
				}
			}

			/// <summary>
			/// Builder accessor for the westbound longitude
			/// </summary>
			public virtual double? WestBL {
				get {
					return _westBL;
				}
			}


			/// <summary>
			/// Builder accessor for the eastbound longitude
			/// </summary>
			public virtual double? EastBL {
				get {
					return _eastBL;
				}
			}


			/// <summary>
			/// Builder accessor for the southbound latitude
			/// </summary>
			public virtual double? SouthBL {
				get {
					return _southBL;
				}
			}


			/// <summary>
			/// Builder accessor for the northbound latitude
			/// </summary>
			public virtual double? NorthBL {
				get {
					return _northBL;
				}
			}

		}
	}
}