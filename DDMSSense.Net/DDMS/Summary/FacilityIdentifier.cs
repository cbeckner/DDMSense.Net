#region usings

using System;
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
    ///     An immutable implementation of ddms:facilityIdentifier.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 DDMSence is stricter than the specification in the following ways:</p>
    ///                 <ul>
    ///                     <li>The beNumber value must be non-empty.</li>
    ///                     <li>The osuffix value must be non-empty.</li>
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
    ///                 <u>ddms:beNumber</u>: uniquely identifies the installation of the facility (required).<br />
    ///                 <u>ddms:osuffix</u>: identifies a facility in conjunction with a beNumber (required if beNumber is
    ///                 set).<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class FacilityIdentifier : AbstractBaseComponent
    {
        private const string BE_NUMBER_NAME = "beNumber";
        private const string OSUFFIX_NAME = "osuffix";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public FacilityIdentifier(Element element) : base(element)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="beNumber"> the beNumber (required) </param>
        /// <param name="osuffix"> the Osuffix (required, because beNumber is required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public FacilityIdentifier(string beNumber, string osuffix)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);
                Util.Util.AddDDMSAttribute(element, BE_NUMBER_NAME, beNumber);
                Util.Util.AddDDMSAttribute(element, OSUFFIX_NAME, osuffix);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the beNumber attribute.
        /// </summary>
        public string BeNumber
        {
            get { return (GetAttributeValue(BE_NUMBER_NAME)); }
        }

        /// <summary>
        ///     Accessor for the osuffix attribute.
        /// </summary>
        public string Osuffix
        {
            get { return (GetAttributeValue(OSUFFIX_NAME)); }
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
        ///                 <li>A beNumber exists and is non-empty.</li>
        ///                 <li>An osuffix exists and is non-empty.</li>
        ///                 <li>Does not validate whether the attributes have logical values.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue(BE_NUMBER_NAME, BeNumber);
            Util.Util.RequireDDMSValue(OSUFFIX_NAME, Osuffix);
            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + BE_NUMBER_NAME, BeNumber));
            text.Append(BuildOutput(isHtml, localPrefix + OSUFFIX_NAME, Osuffix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is FacilityIdentifier))
            {
                return (false);
            }
            var test = (FacilityIdentifier) obj;
            return (BeNumber.Equals(test.BeNumber) && Osuffix.Equals(test.Osuffix));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + BeNumber.GetHashCode();
            result = 7*result + Osuffix.GetHashCode();
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
            return ("facilityIdentifier");
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
            
            internal string _beNumber;
            internal string _osuffix;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(FacilityIdentifier facilityIdentifier)
            {
                _beNumber = facilityIdentifier.BeNumber;
                _osuffix = facilityIdentifier.Osuffix;
            }

            /// <summary>
            ///     Builder accessor for the beNumber attribute.
            /// </summary>
            public virtual string BeNumber
            {
                get { return _beNumber; }
            }


            /// <summary>
            ///     Builder accessor for the osuffix attribute.
            /// </summary>
            public virtual string Osuffix
            {
                get { return _osuffix; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                return (Empty ? null : new FacilityIdentifier(BeNumber, Osuffix));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (String.IsNullOrEmpty(BeNumber) && String.IsNullOrEmpty(Osuffix)); }
            }
        }
    }
}