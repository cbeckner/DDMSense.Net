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

namespace DDMSSense.DDMS.SecurityElements.Ism
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ISM:NoticeText.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A NoticeText element can be used without any child text.</li>
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
    ///                 <u>ISM:pocType</u>: indicates that the element specifies a POC for particular notice type. (optional)
    ///                 <br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public sealed class NoticeText : AbstractSimpleString
    {
        private const string POC_TYPE_NAME = "pocType";
        private List<string> _pocTypes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NoticeText(Element element) : base(element, false)
        {
            try
            {
                string pocTypes = element.Attribute(XName.Get(POC_TYPE_NAME, DDMSVersion.IsmNamespace)).Value;
                _pocTypes = Util.Util.GetXsListAsList(pocTypes);
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
        /// <param name="value"> the value of the description child text </param>
        /// <param name="pocTypes"> the value of the pocType attribute (optional) </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NoticeText(string value, List<string> pocTypes, SecurityAttributes securityAttributes)
            : base(
                PropertyReader.GetPrefix("ism"), DDMSVersion.GetCurrentVersion().IsmNamespace,
                GetName(DDMSVersion.GetCurrentVersion()), value, securityAttributes, false)
        {
            try
            {
                if (pocTypes == null)
                {
                    pocTypes = new List<string>();
                }
                if (pocTypes.Count > 0)
                {
                    Util.Util.AddAttribute(Element, PropertyReader.GetPrefix("ism"), POC_TYPE_NAME,
                        DDMSVersion.GetCurrentVersion().IsmNamespace, Util.Util.GetXsList(pocTypes));
                }
                _pocTypes = pocTypes;
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the pocType attribute.
        /// </summary>
        public List<string> PocTypes
        {
            get { return (_pocTypes); }
            set { _pocTypes = value; }
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
        ///                 <li>This component cannot be used until DDMS 4.0.1 or later.</li>
        ///                 <li>If set, the pocTypes must each be a valid token.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, DDMSVersion.IsmNamespace, GetName(DDMSVersion));

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");
            if (DDMSVersion.IsAtLeast("4.0.1"))
            {
                foreach (var pocType in PocTypes)
                {
                    ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_POC_TYPE, pocType);
                }
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
        ///                 <li>An ISM:NoticeText element was found with no value.</li>
        ///                 <li>Include any validation warnings from the security attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Value))
            {
                AddWarning("An ISM:" + Name + " element was found with no value.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "noticeText", suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Value));
            text.Append(BuildOutput(isHtml, localPrefix + "." + POC_TYPE_NAME, Util.Util.GetXsList(PocTypes)));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is NoticeText))
            {
                return (false);
            }
            var test = (NoticeText) obj;
            return (Util.Util.ListEquals(PocTypes, test.PocTypes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + PocTypes.GetHashCode();
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
            return ("NoticeText");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractSimpleString.Builder
        {
            internal const long SerialVersionUID = 7750664735441105296L;
            internal List<string> _pocTypes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(NoticeText text) : base(text)
            {
                PocTypes = text.PocTypes;
            }

            /// <summary>
            ///     Builder accessor for the pocTypes
            /// </summary>
            public virtual List<string> PocTypes
            {
                get
                {
                    if (_pocTypes == null)
                    {
                        _pocTypes = new List<string>();
                    }
                    return _pocTypes;
                }
                set { _pocTypes = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty && PocTypes.Count == 0
                    ? null
                    : new NoticeText(Value, PocTypes, SecurityAttributes.Commit()));
            }
        }
    }
}