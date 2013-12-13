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
    ///     An immutable implementation of gml:pos.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>
    ///                         A position must either have 2 coordinates (to comply with WGS84E_2D) or 3 coordinates (to
    ///                         comply with WGS84E_3D).
    ///                     </li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>
    ///                     <see cref="SRSAttributes" />
    ///                 </u>
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Position : AbstractBaseComponent
    {
        private readonly List<double?> _coordinates;
        private readonly SRSAttributes _srsAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Position(Element element)
        {
            try
            {
                SetElement(element, false);
                List<string> tuple = Util.Util.GetXsListAsList(CoordinatesAsXsList);
                _coordinates = new List<double?>();
                foreach (var coordinate in tuple)
                {
                    _coordinates.Add(GetStringAsDouble(coordinate));
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
        /// <param name="coordinates"> a list of either 2 or 3 coordinate Double values </param>
        /// <param name="srsAttributes">
        ///     the attribute group containing srsName, srsDimension, axisLabels, and uomLabels
        /// </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Position(List<double?> coordinates, SRSAttributes srsAttributes)
        {
            try
            {
                if (coordinates == null)
                {
                    coordinates = new List<double?>();
                }
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                Element element = Util.Util.BuildElement(PropertyReader.GetPrefix("gml"), GetName(version),
                    version.GmlNamespace, Util.Util.GetXsList(coordinates));

                _coordinates = coordinates;
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

        /// <summary>
        ///     Accessor for the SRS Attributes. Will always be non-null, even if the attributes inside are not set.
        /// </summary>
        public SRSAttributes SRSAttributes
        {
            get { return (_srsAttributes); }
        }

        /// <summary>
        ///     Accessor for the coordinates of the position. May return null, but cannot happen after instantiation.
        /// </summary>
        public List<double?> Coordinates
        {
            get { return _coordinates; }
        }

        /// <summary>
        ///     Accessor for the String representation of the coordinates
        /// </summary>
        public string CoordinatesAsXsList
        {
            get { return (Element.Value); }
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
        ///                 <li>Each coordinate is a valid Double value.</li>
        ///                 <li>The position is represented by 2 or 3 coordinates.</li>
        ///                 <li>The first coordinate is a valid latitude.</li>
        ///                 <li>The second coordinate is a valid longitude.</li>
        ///                 <li>Does not perform any special validation on the third coordinate (height above ellipsoid).</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, Namespace, GetName(DDMSVersion));
            foreach (var coordinate in Coordinates)
            {
                Util.Util.RequireDDMSValue("coordinate", coordinate);
            }
            if (!Util.Util.IsBounded(Coordinates.Count, 2, 3))
            {
                throw new InvalidDDMSException("A position must be represented by either 2 or 3 coordinates.");
            }
            Util.Util.RequireValidLatitude(Coordinates[0]);
            Util.Util.RequireValidLongitude(Coordinates[1]);

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
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, CoordinatesAsXsList));
            text.Append(SRSAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Position))
            {
                return (false);
            }
            var test = (Position) obj;
            return (SRSAttributes.Equals(test.SRSAttributes) && Coordinates.Count == test.Coordinates.Count &&
                    Util.Util.ListEquals(Coordinates, test.Coordinates));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + SRSAttributes.GetHashCode();
            result = 7*result + CoordinatesAsXsList.GetHashCode();
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
            return ("pos");
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
            
            internal List<DoubleBuilder> _coordinates;
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
            public Builder(Position position)
            {
                SrsAttributes = new SRSAttributes.Builder(position.SRSAttributes);
                foreach (var coord in position.Coordinates)
                {
                    Coordinates.Add(new DoubleBuilder(coord));
                }
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
            ///     Builder accessor for the coordinates of the position
            /// </summary>
            public virtual List<DoubleBuilder> Coordinates
            {
                get
                {
                    if (_coordinates == null)
                    {
                        _coordinates = new List<DoubleBuilder>();
                    }
                    return _coordinates;
                }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var coordinates = new List<double?>();
                foreach (var builder in Coordinates)
                {
                    double? coord = builder.Commit();
                    if (coord != null)
                    {
                        coordinates.Add(coord);
                    }
                }
                return (new Position(coordinates, SrsAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (var builder in Coordinates)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && SrsAttributes.Empty);
                }
            }
        }

        /// <summary>
        ///     Builder for a Double
        ///     <para>
        ///         This builder is implemented because the Java Double class does not have a no-arg constructor which can be
        ///         hooked into a LazyList. Because the Builder returns a Double instead of an IDDMSComponent, it does not
        ///         officially
        ///         implement the IBuilder interface.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.9.0"></see>
        [Serializable]
        public class DoubleBuilder
        {
            
            internal double? _value;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public DoubleBuilder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public DoubleBuilder(double? value)
            {
                Value = value;
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (Value == null); }
            }

            /// <summary>
            ///     Builder accessor for the value
            /// </summary>
            public virtual double? Value
            {
                get { return _value; }
                set { _value = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual double? Commit()
            {
                return (Empty ? null : Value);
            }
        }
    }
}