#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSSense.Util;

#endregion

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

namespace DDMSSense.DDMS.Summary.Gml
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of gml:Polygon.
    ///     <para>
    ///         A Polygon element contains a nested gml:exterior element, which itself contains a nested gml:LinearRing
    ///         element.
    ///         The points which mark the boundaries of the polygon should be provided in counter-clockwise order.
    ///         Because DDMS does not decorate these elements with any special attributes, they are not implemented as Java
    ///         objects.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>The srsName must also be non-empty.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>gml:pos</u>: the positions which comprise the LinearRing in this Polygon (at least 4 required),
    ///                 implemented as
    ///                 a <see cref="Position" /><br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>gml:id</u>: unique ID (required)<br />
    ///                 <u>
    ///                     <see cref="SRSAttributes" />
    ///                 </u>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 0.9.b
    /// </summary>
    public sealed class Polygon : AbstractBaseComponent
    {
        private const string EXTERIOR_NAME = "exterior";
        private const string LINEAR_RING_NAME = "LinearRing";
        private const string ID_NAME = "id";
        private readonly List<Position> _positions;
        private readonly SRSAttributes _srsAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Polygon(Element element)
        {
            try
            {
                SetElement(element, false);
                _positions = new List<Position>();
                Element extElement = element.Element(XName.Get(EXTERIOR_NAME, Namespace));
                if (extElement != null)
                {
                    Element ringElement = extElement.Element(XName.Get(LINEAR_RING_NAME, Namespace));
                    if (ringElement != null)
                    {
                        IEnumerable<Element> positions =
                            ringElement.Elements(XName.Get(Position.GetName(DDMSVersion), Namespace));
                        foreach (var position in positions)
                            _positions.Add(new Position(position));
                    }
                }
                _srsAttributes = new SRSAttributes(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="positions"> the positions of the Polygon (required) </param>
        /// <param name="srsAttributes">
        ///     the attribute group containing srsName, srsDimension, axisLabels, and uomLabels (srsName
        ///     required)
        /// </param>
        /// <param name="id">
        ///     the id value (required)
        /// </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Polygon(List<Position> positions, SRSAttributes srsAttributes, string id)
        {
            try
            {
                if (positions == null)
                {
                    positions = new List<Position>();
                }
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;
                Element ringElement = Util.Util.BuildElement(gmlPrefix, LINEAR_RING_NAME, gmlNamespace, null);
                foreach (var pos in positions)
                {
                    ringElement.Add(pos.ElementCopy);
                }
                Element extElement = Util.Util.BuildElement(gmlPrefix, EXTERIOR_NAME, gmlNamespace, null);
                extElement.Add(ringElement);
                Element element = Util.Util.BuildElement(gmlPrefix, GetName(version), gmlNamespace, null);
                element.Add(extElement);
                Util.Util.AddAttribute(element, gmlPrefix, ID_NAME, gmlNamespace, id);

                _positions = positions;
                _srsAttributes = SRSAttributes.GetNonNullInstance(srsAttributes);
                _srsAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getLocatorSuffix()"></see>
        protected internal override string LocatorSuffix
        {
            get
            {
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                return (ValidationMessage.ELEMENT_PREFIX + gmlPrefix + ":" + EXTERIOR_NAME +
                        ValidationMessage.ELEMENT_PREFIX + gmlPrefix + ":" + LINEAR_RING_NAME);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.AddRange(Positions);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the SRS Attributes. Will always be non-null.
        /// </summary>
        public SRSAttributes SRSAttributes
        {
            get { return (_srsAttributes); }
        }

        /// <summary>
        ///     Accessor for the ID
        /// </summary>
        public string Id
        {
            get { return (GetAttributeValue(ID_NAME, Namespace)); }
        }

        /// <summary>
        ///     Accessor for the coordinates. May return null, but cannot happen after instantiation.
        /// </summary>
        public List<Position> Positions
        {
            get { return _positions; }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>The srsName is required.</li>
        ///                 <li>If the position has an srsName, it matches the srsName of this Polygon.</li>
        ///                 <li>The ID is required, and must be a valid NCName.</li>
        ///                 <li>The first and last position coordinates must be identical (a closed polygon).</li>
        ///                 <li>Does not perform any special validation on the third coordinate (height above ellipsoid).</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, Namespace, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("srsAttributes", SRSAttributes);
            Util.Util.RequireDDMSValue("srsName", SRSAttributes.SrsName);
            Util.Util.RequireDDMSValue(ID_NAME, Id);
            Util.Util.RequireValidNCName(Id);

            Element extElement = Element.Element(XName.Get(EXTERIOR_NAME, Namespace));
            Util.Util.RequireDDMSValue("exterior element", extElement);
            if (extElement != null)
            {
                Util.Util.RequireDDMSValue("LinearRing element",
                    extElement.Element(XName.Get(LINEAR_RING_NAME, Namespace)));
            }
            List<Position> positions = Positions;
            foreach (var pos in positions)
            {
                if (pos.SRSAttributes != null)
                {
                    string srsName = pos.SRSAttributes.SrsName;
                    if (!String.IsNullOrEmpty(srsName) && !srsName.Equals(SRSAttributes.SrsName))
                    {
                        throw new InvalidDDMSException(
                            "The srsName of each position must match the srsName of the Polygon.");
                    }
                }
            }
            if (positions.Count < 4)
            {
                throw new InvalidDDMSException("At least 4 positions are required for a valid Polygon.");
            }
            if (positions.Count > 0 && !positions[0].Equals(positions[positions.Count - 1]))
            {
                throw new InvalidDDMSException("The first and last position in the Polygon must be the same.");
            }

            base.Validate();
        }

        /// <summary>
        ///     Validates any conditions that might result in a warning.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>Include any validation warnings from the SRS attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            AddWarnings(SRSAttributes.ValidationWarnings, true);
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + ID_NAME, Id));
            text.Append(SRSAttributes.GetOutput(isHtml, localPrefix));
            text.Append(BuildOutput(isHtml, localPrefix, Positions));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Polygon))
            {
                return (false);
            }
            var test = (Polygon) obj;
            return (SRSAttributes.Equals(test.SRSAttributes) && Id.Equals(test.Id));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + SRSAttributes.GetHashCode();
            result = 7*result + Id.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("Polygon");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            internal const long SerialVersionUID = -4324741146353401634L;
            internal string _id;
            internal List<Position.Builder> _positions;
            internal SRSAttributes.Builder _srsAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Polygon polygon)
            {
                SrsAttributes = new SRSAttributes.Builder(polygon.SRSAttributes);
                foreach (var position in polygon.Positions)
                {
                    Positions.Add(new Position.Builder(position));
                }
                Id = polygon.Id;
            }

            /// <summary>
            ///     Builder accessor for the SRS Attributes
            /// </summary>
            public virtual SRSAttributes.Builder SrsAttributes
            {
                get
                {
                    if (_srsAttributes == null)
                    {
                        _srsAttributes = new SRSAttributes.Builder();
                    }
                    return _srsAttributes;
                }
                set { _srsAttributes = value; }
            }


            /// <summary>
            ///     Builder accessor for the coordinates
            /// </summary>
            public virtual List<Position.Builder> Positions
            {
                get
                {
                    if (_positions == null)
                    {
                        _positions = new List<Position.Builder>();
                    }
                    return _positions;
                }
            }

            /// <summary>
            ///     Accessor for the ID
            /// </summary>
            public virtual string Id
            {
                get { return _id; }
                set { _id = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var positions = new List<Position>();
                foreach (var builder in Positions)
                {
                    var position = (Position) builder.Commit();
                    if (position != null)
                    {
                        positions.Add(position);
                    }
                }
                return (new Polygon(positions, SrsAttributes.Commit(), Id));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in Positions)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (String.IsNullOrEmpty(Id) && !hasValueInList && SrsAttributes.Empty);
                }
            }
        }
    }
}