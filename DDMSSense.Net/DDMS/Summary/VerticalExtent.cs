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

namespace DDMSense.DDMS.Summary
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:verticalExtent.
    ///     <para>
    ///         DDMSence is stricter than the specification in the following ways:
    ///     </para>
    ///     <ul>
    ///         <li>
    ///             The optional unitOfMeasure and datum on the minVerticalExtent/maxVerticalExtent child elements MUST match
    ///             the
    ///             values on the required attributes of the same name on this element. It does not seem logical to specify
    ///             these
    ///             attributes on the parent element and then express the actual values with a different measure. Note that
    ///             because
    ///             DDMSence is giving precedence to the top-level unitOfMeasure and datum attributes, those attributes on the
    ///             children are not displayed in HTML/Text. However, they are still rendered in XML, if present in an existing
    ///             document.
    ///         </li>
    ///     </ul></p>
    ///     <para>
    ///         The above design decision dictates that VerticalDistance (the type behind minVerticalExtent and
    ///         maxVerticalExtent)
    ///         does not need to be implemented as a Java class.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:minVerticalExtent</u>: minimum extent (required)<br />
    ///                 <u>ddms:maxVerticalExtent</u>: maximum extent (required)<br />
    ///                 Please note that the case of the nested elements changed starting in DDMS 4.0.1. Previously, the first
    ///                 letter
    ///                 of each element was capitalized (i.e. MinVerticalExtent/MaxVerticalExtent).
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:unitOfMeasure</u>: unit of measure (Meter, Kilometer, Foot, StatuteMile, NauticalMile, Fathom,
    ///                 Inch)
    ///                 (required) <br />
    ///                 <u>ddms:datum</u>: vertical datum (MSL, AGL, HAE) (required)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class VerticalExtent : AbstractBaseComponent
    {
        private const string DATUM_NAME = "datum";
        private const string UOM_NAME = "unitOfMeasure";
        private static readonly List<string> VERTICAL_DATUM_TYPES = new List<string>();
        private static readonly List<string> LENGTH_MEASURE_TYPES = new List<string>();
        private double? _max;
        private double? _min;

        static VerticalExtent()
        {
            VERTICAL_DATUM_TYPES.Add("MSL");
            VERTICAL_DATUM_TYPES.Add("AGL");
            VERTICAL_DATUM_TYPES.Add("HAE");
            LENGTH_MEASURE_TYPES.Add("Meter");
            LENGTH_MEASURE_TYPES.Add("Kilometer");
            LENGTH_MEASURE_TYPES.Add("Foot");
            LENGTH_MEASURE_TYPES.Add("StatuteMile");
            LENGTH_MEASURE_TYPES.Add("NauticalMile");
            LENGTH_MEASURE_TYPES.Add("Fathom");
            LENGTH_MEASURE_TYPES.Add("Inch");
        }

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public VerticalExtent(Element element)
        {
            try
            {
                Util.Util.RequireDDMSValue("verticalExtent element", element);
                SetElement(element, false);
                _min = GetChildTextAsDouble(element, MinVerticalExtentName);
                _max = GetChildTextAsDouble(element, MaxVerticalExtentName);
                Validate();
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
        /// <param name="minVerticalExtent"> the minimum (required) </param>
        /// <param name="maxVerticalExtent"> the maximum (required) </param>
        /// <param name="unitOfMeasure"> the unit of measure (required) </param>
        /// <param name="datum"> the datum (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public VerticalExtent(double minVerticalExtent, double maxVerticalExtent, string unitOfMeasure, string datum)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.GetCurrentVersion()), null);
                SetElement(element, false);
                Util.Util.AddDDMSAttribute(element, UOM_NAME, unitOfMeasure);
                Util.Util.AddDDMSAttribute(element, DATUM_NAME, datum);
                element.Add(Util.Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(minVerticalExtent)));
                element.Add(Util.Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(maxVerticalExtent)));
                _min = Convert.ToDouble(minVerticalExtent);
                _max = Convert.ToDouble(maxVerticalExtent);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the name of the minimum vertical extent element, which changed in DDMS 4.0.1.
        /// </summary>
        private string MinVerticalExtentName
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? "minVerticalExtent" : "MinVerticalExtent"); }
        }

        /// <summary>
        ///     Accessor for the name of the maximum vertical extent element, which changed in DDMS 4.0.1.
        /// </summary>
        private string MaxVerticalExtentName
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? "maxVerticalExtent" : "MaxVerticalExtent"); }
        }

        /// <summary>
        ///     Accessor for the unitOfMeasure attribute
        /// </summary>
        public string UnitOfMeasure
        {
            get { return (GetAttributeValue(UOM_NAME)); }
        }

        /// <summary>
        ///     Accessor for the vertical datum attribute
        /// </summary>
        public string Datum
        {
            get { return (GetAttributeValue(DATUM_NAME)); }
        }

        /// <summary>
        ///     Accessor for the minimum extent
        /// </summary>
        public double? MinVerticalExtent
        {
            get { return (_min); }
            set { _min = value; }
        }

        /// <summary>
        ///     Accessor for the maximum extent
        /// </summary>
        public double? MaxVerticalExtent
        {
            get { return (_max); }
            set { _max = value; }
        }

        /// <summary>
        ///     Validates a vertical datum type against the allowed types.
        /// </summary>
        /// <param name="datumType"> the type to test </param>
        /// <exception cref="InvalidDDMSException"> if the value is null, empty or invalid. </exception>
        public static void ValidateVerticalDatumType(string datumType)
        {
            Util.Util.RequireDDMSValue("vertical datum type", datumType);
            if (!VERTICAL_DATUM_TYPES.Contains(datumType))
            {
                throw new InvalidDDMSException("The vertical datum type must be one of " + VERTICAL_DATUM_TYPES);
            }
        }

        /// <summary>
        ///     Validates a length measure type against the allowed types.
        /// </summary>
        /// <param name="lengthType"> the type to test </param>
        /// <exception cref="InvalidDDMSException"> if the value is null, empty or invalid. </exception>
        public static void ValidateLengthMeasureType(string lengthType)
        {
            Util.Util.RequireDDMSValue("length measure type", lengthType);
            if (!LENGTH_MEASURE_TYPES.Contains(lengthType))
            {
                throw new InvalidDDMSException("The length measure type must be one of " + LENGTH_MEASURE_TYPES);
            }
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
        ///                 <li>A minVerticalExtent exists.</li>
        ///                 <li>A maxVerticalExtent exists.</li>
        ///                 <li>A unitOfMeasure exists and has an appropriate value.</li>
        ///                 <li>A datum exists and has an appropriate value.</li>
        ///                 <li>
        ///                     If a minVerticalExtent has unitOfMeasure or datum set, its values match the parent attribute
        ///                     values.
        ///                 </li>
        ///                 <li>
        ///                     If a maxVerticalExtent has unitOfMeasure or datum set, its values match the parent attribute
        ///                     values.
        ///                 </li>
        ///                 <li>The minVerticalExtent is less than the MaxVerticalExtent.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue(MinVerticalExtentName, MinVerticalExtent);
            Util.Util.RequireDDMSValue(MaxVerticalExtentName, MaxVerticalExtent);
            Util.Util.RequireDDMSValue(UOM_NAME, UnitOfMeasure);
            Util.Util.RequireDDMSValue(DATUM_NAME, Datum);
            ValidateLengthMeasureType(UnitOfMeasure);
            ValidateVerticalDatumType(Datum);
            ValidateInheritedAttributes(GetChild(MinVerticalExtentName));
            ValidateInheritedAttributes(GetChild(MaxVerticalExtentName));
            if (MaxVerticalExtent.Value.CompareTo(MinVerticalExtent) < 0)
            {
                throw new InvalidDDMSException("Minimum vertical extent must be less than maximum vertical extent.");
            }
            base.Validate();
        }

        /// <summary>
        ///     Confirms that the unitOfMeasure and datum on minimum and maximum extent elements matches the parent attribute
        ///     values. This is an additional level of logic added by DDMSence.
        /// </summary>
        /// <param name="extentElement"> </param>
        /// <exception cref="InvalidDDMSException"> </exception>
        private void ValidateInheritedAttributes(Element extentElement)
        {
            string unitOfMeasure = extentElement.Attribute(XName.Get(UOM_NAME, extentElement.Name.NamespaceName)).Value;
            string datum = extentElement.Attribute(XName.Get(DATUM_NAME, extentElement.Name.NamespaceName)).Value;
            if (!String.IsNullOrEmpty(unitOfMeasure) && !unitOfMeasure.Equals(UnitOfMeasure))
            {
                throw new InvalidDDMSException("The unitOfMeasure on the " + extentElement.Name.LocalName +
                                               " element must match the unitOfMeasure on the enclosing verticalExtent element.");
            }
            if (!String.IsNullOrEmpty(datum) && !datum.Equals(Datum))
            {
                throw new InvalidDDMSException("The datum on the " + extentElement.Name.LocalName +
                                               " element must match the datum on the enclosing verticalExtent element.");
            }
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + UOM_NAME, UnitOfMeasure));
            text.Append(BuildOutput(isHtml, localPrefix + DATUM_NAME, Datum));
            text.Append(BuildOutput(isHtml, localPrefix + "minimum", Convert.ToString(MinVerticalExtent)));
            text.Append(BuildOutput(isHtml, localPrefix + "maximum", Convert.ToString(MaxVerticalExtent)));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is VerticalExtent))
            {
                return (false);
            }
            var test = (VerticalExtent) obj;
            return (UnitOfMeasure.Equals(test.UnitOfMeasure) && Datum.Equals(test.Datum) &&
                    MinVerticalExtent.Equals(test.MinVerticalExtent) && MaxVerticalExtent.Equals(test.MaxVerticalExtent));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + UnitOfMeasure.GetHashCode();
            result = 7*result + Datum.GetHashCode();
            result = 7*result + MinVerticalExtent.GetHashCode();
            result = 7*result + MaxVerticalExtent.GetHashCode();
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
            return ("verticalExtent");
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
            
            internal string _datum;
            internal double? _maxVerticalExtent;
            internal double? _minVerticalExtent;
            internal string _unitOfMeasure;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(VerticalExtent extent)
            {
                MinVerticalExtent = extent.MinVerticalExtent;
                MaxVerticalExtent = extent.MaxVerticalExtent;
                UnitOfMeasure = extent.UnitOfMeasure;
                Datum = extent.Datum;
            }

            /// <summary>
            ///     Builder accessor for the minimum extent
            /// </summary>
            public virtual double? MinVerticalExtent
            {
                get { return _minVerticalExtent; }
                set { _minVerticalExtent = value; }
            }


            /// <summary>
            ///     Builder accessor for the maximum extent
            /// </summary>
            public virtual double? MaxVerticalExtent
            {
                get { return _maxVerticalExtent; }
                set { _maxVerticalExtent = value; }
            }


            /// <summary>
            ///     Builder accessor for the unitOfMeasure attribute
            /// </summary>
            public virtual string UnitOfMeasure
            {
                get { return _unitOfMeasure; }
                set { _unitOfMeasure = value; }
            }


            /// <summary>
            ///     Builder accessor for the vertical datum attribute
            /// </summary>
            public virtual string Datum
            {
                get { return _datum; }
                set { _datum = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                // Check for existence of values before casting to primitives.
                if (MinVerticalExtent == null || MaxVerticalExtent == null)
                {
                    throw new InvalidDDMSException("A ddms:verticalExtent requires a minimum and maximum extent value.");
                }
                return
                    (new VerticalExtent((double) MinVerticalExtent, (double) MaxVerticalExtent, UnitOfMeasure, Datum));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    return (MinVerticalExtent == null && MaxVerticalExtent == null &&
                            String.IsNullOrEmpty(UnitOfMeasure) && String.IsNullOrEmpty(Datum));
                }
            }
        }
    }
}