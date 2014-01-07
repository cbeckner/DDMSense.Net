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

namespace DDMSense.DDMS.ResourceElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:rights.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:privacyAct</u>: protected by the Privacy Act (optional, default=false).<br />
    ///                 <u>ddms:intellectualProperty</u>: has an intellectual property rights owner (optional, default=false)
    ///                 <br />
    ///                 <u>ddms:copyright</u>: has a copyright owner (optional, default=false)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Rights : AbstractBaseComponent
    {
        private const string PRIVACY_ACT_NAME = "privacyAct";
        private const string INTELLECTUAL_PROPERY_NAME = "intellectualProperty";
        private const string COPYRIGHT_NAME = "copyright";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Rights(Element element) : base(element)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="privacyAct"> the value for the privacyAct attribute </param>
        /// <param name="intellectualProperty"> the value for the intellectualProperty attribute </param>
        /// <param name="copyright"> the value for the copyright attribute </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Rights(bool privacyAct, bool intellectualProperty, bool copyright)
        {
            Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.GetCurrentVersion()), null);
            Util.Util.AddDDMSAttribute(element, PRIVACY_ACT_NAME, Convert.ToString(privacyAct));
            Util.Util.AddDDMSAttribute(element, INTELLECTUAL_PROPERY_NAME, Convert.ToString(intellectualProperty));
            Util.Util.AddDDMSAttribute(element, COPYRIGHT_NAME, Convert.ToString(copyright));
            SetElement(element, true);
            // This cannot actually throw an exception, so locator information is not inserted in a catch statement.
        }

        /// <summary>
        ///     Accessor for the privacyAct attribute. The default value is false.
        /// </summary>
        public bool PrivacyAct
        {
            get { return (Convert.ToBoolean(GetAttributeValue(PRIVACY_ACT_NAME))); }
        }

        /// <summary>
        ///     Accessor for the intellectualProperty attribute. The default value is false.
        /// </summary>
        public bool IntellectualProperty
        {
            get { return (Convert.ToBoolean(GetAttributeValue(INTELLECTUAL_PROPERY_NAME))); }
        }

        /// <summary>
        ///     Accessor for the copyright attribute. The default value is false.
        /// </summary>
        public bool Copyright
        {
            get { return (Convert.ToBoolean(GetAttributeValue(COPYRIGHT_NAME))); }
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
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + PRIVACY_ACT_NAME, Convert.ToString(PrivacyAct)));
            text.Append(BuildOutput(isHtml, localPrefix + INTELLECTUAL_PROPERY_NAME,
                Convert.ToString(IntellectualProperty)));
            text.Append(BuildOutput(isHtml, localPrefix + COPYRIGHT_NAME, Convert.ToString(Copyright)));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Rights))
            {
                return (false);
            }
            var test = (Rights) obj;
            return (PrivacyAct == test.PrivacyAct && IntellectualProperty == test.IntellectualProperty &&
                    Copyright == test.Copyright);
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Util.Util.BooleanHashCode(PrivacyAct);
            result = 7*result + Util.Util.BooleanHashCode(IntellectualProperty);
            result = 7*result + Util.Util.BooleanHashCode(Copyright);
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
            return ("rights");
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
            internal const long SerialVersionUID = -2290965863004046496L;
            internal bool? _copyright = null;
            internal bool? _intellectualProperty = null;
            internal bool? _privacyAct = null;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Rights rights)
            {
                _privacyAct = Convert.ToBoolean(rights.PrivacyAct);
                _intellectualProperty = Convert.ToBoolean(rights.IntellectualProperty);
                _copyright = Convert.ToBoolean(rights.Copyright);
            }

            /// <summary>
            ///     Builder accessor for the privacyAct attribute.
            /// </summary>
            public virtual bool? PrivacyAct
            {
                get { return _privacyAct; }
            }


            /// <summary>
            ///     Builder accessor for the intellectualProperty attribute.
            /// </summary>
            public virtual bool? IntellectualProperty
            {
                get { return _intellectualProperty; }
            }


            /// <summary>
            ///     Builder accessor for the copyright attribute.
            /// </summary>
            public virtual bool? Copyright
            {
                get { return _copyright; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }

                // Handle default values.
                bool privacyAct = (PrivacyAct == null) ? false : (bool) PrivacyAct;
                bool intellectualProperty = (IntellectualProperty == null) ? false : (bool) IntellectualProperty;
                bool copyright = (Copyright == null) ? false : (bool) Copyright;
                return (new Rights(privacyAct, intellectualProperty, copyright));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (PrivacyAct == null && IntellectualProperty == null && Copyright == null); }
            }
        }
    }
}