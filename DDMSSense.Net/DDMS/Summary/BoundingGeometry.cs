using System;
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
	
	using Point = DDMSSense.DDMS.Summary.Gml.Point;
	using Polygon = DDMSSense.DDMS.Summary.Gml.Polygon;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using DDMSSense.DDMS;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:boundingGeometry.
	/// 
	/// <para>The DDMS documentation has no Text/HTML examples for the output of this component. However, the component has no 
	/// additional attributes or elements besides the nested Polygon/Point components, so no additional output is needed.</para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>gml:Polygon</u>: a polygon (0-many optional), implemented as a <seealso cref="Polygon"/><br />
	/// <u>gml:Point</u>: a point (0-many optional), implemented as a <seealso cref="Point"/><br />
	/// <para>At least 1 of Polygon or Point must be used.</para>
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class BoundingGeometry : AbstractBaseComponent {

		private List<Polygon> _polygons = null;
		private List<Point> _points = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public BoundingGeometry(Element element) {
			try {
				Util.RequireDDMSValue("boundingGeometry element", element);
				SetXOMElement(element, false);
				string gmlNamespace = DDMSVersion.GmlNamespace;
				_polygons = new List<Polygon>();
				_points = new List<Point>();
				IEnumerable<Element> polygons = element.Elements(XName.Get(Polygon.GetName(DDMSVersion), gmlNamespace));
				polygons.ToList().ForEach(p=>_polygons.Add(new Polygon(p)));
				IEnumerable<Element> points = element.Elements(XName.Get(Point.GetName(DDMSVersion), gmlNamespace));
				points.ToList().ForEach(p=> _points.Add(new Point(p)));
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="polygons"> an ordered list of the polygons used in this geometry </param>
		/// <param name="points"> an ordered list of the points used in this geometry </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public BoundingGeometry(List<Polygon> polygons, List<Point> points) {
			try {
				if (polygons == null) {
					polygons = new List<Polygon>();
				}
				if (points == null) {
					points = new List<Point>();
				}
				Element element = Util.BuildDDMSElement(BoundingGeometry.GetName(DDMSVersion.GetCurrentVersion()), null);
				foreach (Polygon polygon in polygons) {
					element.Add(polygon.XOMElementCopy);
				}
				foreach (Point point in points) {
					element.Add(point.XOMElementCopy);
				}
				_polygons = polygons;
				_points = points;
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
		/// <li>At least 1 polygon or point exists.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, BoundingGeometry.GetName(DDMSVersion));
			if (Polygons.Count + Points.Count == 0) {
				throw new InvalidDDMSException("At least 1 of Polygon or Point must be used.");
			}

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, Polygons));
			text.Append(BuildOutput(isHTML, localPrefix, Points));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(Points);
				list.AddRange(Polygons);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is BoundingGeometry)) {
				return (false);
			}
			return (true);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("boundingGeometry");
		}

		/// <summary>
		/// Accessor for the polygons in this geometry.
		/// </summary>
		public List<Polygon> Polygons {
			get {
				return _polygons;
			}
		}

		/// <summary>
		/// Accessor for the points in this geometry.
		/// </summary>
		public List<Point> Points {
			get {
				return _points;
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
			internal const long SerialVersionUID = -5734267242408462644L;
			internal List<Polygon.Builder> _polygons;
			internal List<Point.Builder> _points;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(BoundingGeometry geometry) {
				foreach (Polygon polygon in geometry.Polygons) {
					Polygons.Add(new Polygon.Builder(polygon));
				}
				foreach (Point point in geometry.Points) {
					Points.Add(new Point.Builder(point));
				}
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				List<Polygon> polygons = new List<Polygon>();
				foreach (Polygon.Builder builder in Polygons) {
					Polygon polygon = builder.Commit();
					if (polygon != null) {
						polygons.Add(polygon);
					}
				}
				List<Point> points = new List<Point>();
				foreach (Point.Builder builder in Points) {
					Point point = builder.Commit();
					if (point != null) {
						points.Add(point);
					}
				}
				return (new BoundingGeometry(polygons, points));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in ChildBuilders) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList);
				}
			}

			/// <summary>
			/// Convenience method to get every child Builder in this Builder.
			/// </summary>
			/// <returns> a list of IBuilders </returns>
			internal virtual List<IBuilder> ChildBuilders {
				get {
					List<IBuilder> list = new List<IBuilder>();
					list.AddRange(Polygons);
					list.AddRange(Points);
					return (list);
				}
			}

			/// <summary>
			/// Builder accessor for the polygons in this geometry.
			/// </summary>
			public virtual List<Polygon.Builder> Polygons {
				get {
					if (_polygons == null) {
                        _polygons = new List<Polygon.Builder>();
					}
					return _polygons;
				}
			}

			/// <summary>
			/// Builder accessor for the points in this geometry.
			/// </summary>
			public virtual List<Point.Builder> Points {
				get {
					if (_points == null) {
                        _points = new List<Point.Builder>();
					}
					return _points;
				}
			}
		}
	}
}