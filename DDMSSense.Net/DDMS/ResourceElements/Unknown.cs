#region usings

using System.Collections.Generic;
using System.Xml.Linq;
using DDMSense.DDMS.Extensible;
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
    ///     An immutable implementation of a ddms:unknown element.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>At least 1 name value must be non-empty.</li>
    ///                 </ul>
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A phone number can be set with no value.</li>
    ///                     <li>An email can be set with no value.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <para>
    ///         The ddms:unknown element is new in DDMS 3.0. Attempts to use it with DDMS 2.0 will result in an
    ///         UnsupportedVersionException. Its name was changed from "Unknown" to "unknown" in DDMS 4.0.1.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:name</u>: names of the producer (1-many, at least 1 required)<br />
    ///                 <u>ddms:phone</u>: phone numbers of the producer (0-many optional)<br />
    ///                 <u>ddms:email</u>: email addresses of the producer (0-many optional)<br />
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
    ///                     <see cref="ExtensibleAttributes" />
    ///                 </u>
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Unknown : AbstractRoleEntity
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Unknown(Element element) : base(element, true)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        public Unknown(List<string> names, List<string> phones, List<string> emails) : this(names, phones, emails, null)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="extensions"> extensible attributes (optional) </param>
        public Unknown(List<string> names, List<string> phones, List<string> emails, ExtensibleAttributes extensions)
            : base(GetName(DDMSVersion.GetCurrentVersion()), names, phones, emails, extensions, true)
        {
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
        ///                 <li>This component cannot be used until DDMS 3.0 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractRoleEntity#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("3.0");

            base.Validate();
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            return (base.Equals(obj) && (obj is Unknown));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return (version.IsAtLeast("4.0.1") ? "unknown" : "Unknown");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        public class Builder : AbstractRoleEntity.Builder
        {
            internal const long SerialVersionUID = -2278534009019179572L;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Unknown unknown) : base(unknown)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Unknown(Names, Phones, Emails, ExtensibleAttributes.Commit()));
            }
        }
    }
}