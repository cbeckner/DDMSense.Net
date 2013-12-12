#region usings

using System;
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

namespace DDMSSense.DDMS.Summary
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:boundingBox.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:westBL</u>: westbound longitude (required)<br />
    ///                 <u>ddms:eastBL</u>: eastbound longitude (required)<br />
    ///                 <u>ddms:southBL</u>: northbound latitude (required)<br />
    ///                 <u>ddms:northBL</u>: southbound latitude (required)<br />
    ///                 Please note that the case of the nested elements changed starting in DDMS 4.0.1. Previously, the first
    ///                 letter of
    ///                 each element was capitalized (i.e. WestBL/EastBL/SouthBL/NorthBL).
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class BoundingBox : AbstractBaseComponent
    {
        private double? _eastBL;
        private double? _northBL;
        private double? _southBL;
        private double? _westBL;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public BoundingBox(Element element)
        {
            try
            {
                Util.Util.RequireDDMSValue("boundingBox element", element);
                SetElement(element, false);
                _westBL = GetChildTextAsDouble(element, WestBLName);
                _eastBL = GetChildTextAsDouble(element, EastBLName);
                _southBL = GetChildTextAsDouble(element, SouthBLName);
                _northBL = GetChildTextAsDouble(element, NorthBLName);
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
        /// <param name="westBL"> the westbound longitude </param>
        /// <param name="eastBL"> the eastbound longitude </param>
        /// <param name="southBL"> the southbound latitude </param>
        /// <param name="northBL"> the northbound latitude </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public BoundingBox(double westBL, double eastBL, double southBL, double northBL)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.GetCurrentVersion()), null);
                SetElement(element, false);
                element.Add(Util.Util.BuildDDMSElement(WestBLName, Convert.ToString(westBL)));
                element.Add(Util.Util.BuildDDMSElement(EastBLName, Convert.ToString(eastBL)));
                element.Add(Util.Util.BuildDDMSElement(SouthBLName, Convert.ToString(southBL)));
                element.Add(Util.Util.BuildDDMSElement(NorthBLName, Convert.ToString(northBL)));
                _westBL = Convert.ToDouble(westBL);
                _eastBL = Convert.ToDouble(eastBL);
                _southBL = Convert.ToDouble(southBL);
                _northBL = Convert.ToDouble(northBL);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the name of the westbound longitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string WestBLName
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? "westBL" : "WestBL"); }
        }

        /// <summary>
        ///     Accessor for the name of the eastbound longitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string EastBLName
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? "eastBL" : "EastBL"); }
        }

        /// <summary>
        ///     Accessor for the name of the southbound latitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string SouthBLName
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? "southBL" : "SouthBL"); }
        }

        /// <summary>
        ///     Accessor for the name of the northbound latitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string NorthBLName
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? "northBL" : "NorthBL"); }
        }

        /// <summary>
        ///     Accessor for the westbound longitude.
        /// </summary>
        public double? WestBL
        {
            get { return (_westBL); }
            set { _westBL = value; }
        }

        /// <summary>
        ///     Accessor for the eastbound longitude.
        /// </summary>
        public double? EastBL
        {
            get { return (_eastBL); }
            set { _eastBL = value; }
        }

        /// <summary>
        ///     Accessor for the southbound latitude.
        /// </summary>
        public double? SouthBL
        {
            get { return (_southBL); }
            set { _southBL = value; }
        }

        /// <summary>
        ///     Accessor for the northbound latitude.
        /// </summary>
        public double? NorthBL
        {
            get { return (_northBL); }
            set { _northBL = value; }
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
        ///                 <li>A westBL exists.</li>
        ///                 <li>An eastBL exists.</li>
        ///                 <li>A southBL exists.</li>
        ///                 <li>A northBL exists.</li>
        ///                 <li>westBL and eastBL must be between -180 and 180 degrees.</li>
        ///                 <li>southBL and northBL must be between -90 and 90 degrees.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("westbound longitude", WestBL);
            Util.Util.RequireDDMSValue("eastbound longitude", EastBL);
            Util.Util.RequireDDMSValue("southbound latitude", SouthBL);
            Util.Util.RequireDDMSValue("northbound latitude", NorthBL);
            Util.Util.RequireValidLongitude(WestBL);
            Util.Util.RequireValidLongitude(EastBL);
            Util.Util.RequireValidLatitude(SouthBL);
            Util.Util.RequireValidLatitude(NorthBL);
            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + WestBLName, Convert.ToString(WestBL)));
            text.Append(BuildOutput(isHtml, localPrefix + EastBLName, Convert.ToString(EastBL)));
            text.Append(BuildOutput(isHtml, localPrefix + SouthBLName, Convert.ToString(SouthBL)));
            text.Append(BuildOutput(isHtml, localPrefix + NorthBLName, Convert.ToString(NorthBL)));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is BoundingBox))
            {
                return (false);
            }
            var test = (BoundingBox) obj;
            return (WestBL.Equals(test.WestBL) && EastBL.Equals(test.EastBL) && SouthBL.Equals(test.SouthBL) &&
                    NorthBL.Equals(test.NorthBL));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + WestBL.GetHashCode();
            result = 7*result + EastBL.GetHashCode();
            result = 7*result + SouthBL.GetHashCode();
            result = 7*result + NorthBL.GetHashCode();
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
            return ("boundingBox");
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
            internal const long SerialVersionUID = -2364407215439097065L;
            internal double? _eastBL;
            internal double? _northBL;
            internal double? _southBL;
            internal double? _westBL;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(BoundingBox box)
            {
                _westBL = box.WestBL;
                _eastBL = box.EastBL;
                _southBL = box.SouthBL;
                _northBL = box.NorthBL;
            }

            /// <summary>
            ///     Builder accessor for the westbound longitude
            /// </summary>
            public virtual double? WestBL
            {
                get { return _westBL; }
            }


            /// <summary>
            ///     Builder accessor for the eastbound longitude
            /// </summary>
            public virtual double? EastBL
            {
                get { return _eastBL; }
            }


            /// <summary>
            ///     Builder accessor for the southbound latitude
            /// </summary>
            public virtual double? SouthBL
            {
                get { return _southBL; }
            }


            /// <summary>
            ///     Builder accessor for the northbound latitude
            /// </summary>
            public virtual double? NorthBL
            {
                get { return _northBL; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                // Check for existence of values before casting to primitives.
                if (WestBL == null || EastBL == null || SouthBL == null || NorthBL == null)
                {
                    throw new InvalidDDMSException("A ddms:boundingBox requires two latitude and two longitude values.");
                }
                return (new BoundingBox((double) WestBL, (double) EastBL, (double) SouthBL, (double) NorthBL));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (WestBL == null && EastBL == null && SouthBL == null && NorthBL == null); }
            }
        }
    }
}