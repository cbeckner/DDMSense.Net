#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

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

namespace DDMSense.DDMS.Summary.Gml
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of gml:Point.
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
    ///                 <u>gml:pos</u>: the position (exactly 1 required), implemented as a <see cref="Position" /><br />
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
    
    
    /// </summary>
    public sealed class Point : AbstractBaseComponent
    {
        private const string ID_NAME = "id";
        private readonly SRSAttributes _srsAttributes;
        private Position _position;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Point(Element element)
        {
            try
            {
                SetElement(element, false);
                Element posElement = element.Element(XName.Get(Position.GetName(DDMSVersion), Namespace));
                if (posElement != null)
                {
                    _position = new Position(posElement);
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
        /// <param name="position"> the position of the Point (required) </param>
        /// <param name="srsAttributes">
        ///     the attribute group containing srsName, srsDimension, axisLabels, and uomLabels (srsName
        ///     required)
        /// </param>
        /// <param name="id"> the id value (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Point(Position position, SRSAttributes srsAttributes, string id)
        {
            try
            {
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                Element element = Util.Util.BuildElement(PropertyReader.GetPrefix("gml"), GetName(version),
                    version.GmlNamespace, null);
                if (position != null)
                {
                    element.Add(position.ElementCopy);
                }
                Util.Util.AddAttribute(element, PropertyReader.GetPrefix("gml"), ID_NAME,
                    DDMSVersion.GetCurrentVersion().GmlNamespace, id);

                _position = position;
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

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(Position);
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
        ///     Accessor for the coordinates of the position. May return null, but cannot happen after instantiation.
        /// </summary>
        public Position Position
        {
            get { return (_position); }
            set { _position = value; }
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
        ///                 <li>The ID is required, and must be a valid NCName.</li>
        ///                 <li>If the position has an srsName, it matches the srsName of this Point.</li>
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
            Util.Util.RequireDDMSValue("position", Position);
            string srsName = Position.SRSAttributes.SrsName;
            if (!String.IsNullOrEmpty(srsName) && !srsName.Equals(SRSAttributes.SrsName))
            {
                throw new InvalidDDMSException("The srsName of the position must match the srsName of the Point.");
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
            text.Append(Position.GetOutput(isHtml, localPrefix, ""));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Point))
            {
                return (false);
            }
            var test = (Point) obj;
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
            return ("Point");
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
            internal const long SerialVersionUID = 4003805386998809149L;
            internal string _id;
            internal Position.Builder _position;
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
            public Builder(Point point)
            {
                SrsAttributes = new SRSAttributes.Builder(point.SRSAttributes);
                Position = new Position.Builder(point.Position);
                Id = point.Id;
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
            ///     Builder accessor for the position
            /// </summary>
            public virtual Position.Builder Position
            {
                get
                {
                    if (_position == null)
                    {
                        _position = new Position.Builder();
                    }
                    return _position;
                }
                set { _position = value; }
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
                return (Empty ? null : new Point((Position) Position.Commit(), SrsAttributes.Commit(), Id));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (String.IsNullOrEmpty(Id) && Position.Empty && SrsAttributes.Empty); }
            }
        }
    }
}