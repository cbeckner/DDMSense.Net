#region usings

using System.Collections.Generic;
using System.Xml.Linq;
using DDMSense.DDMS.Extensible;
using DDMSense.Util;
using System;

#endregion

namespace DDMSense.DDMS.ResourceElements
{
    /// <summary>
    ///     An immutable implementation of a ddms:service element.
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
    ///     <para>The name of this component was changed from "Service" to "service" in DDMS 4.0.1.</para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:name</u>: names of the producer entity (1-many, at least 1 required)<br />
    ///                 <u>ddms:phone</u>: phone numbers of the producer entity (0-many optional)<br />
    ///                 <u>ddms:email</u>: email addresses of the producer entity (0-many optional)<br />
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
    public sealed class Service : AbstractRoleEntity
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Service(XElement element) : base(element, true)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        public Service(List<string> names, List<string> phones, List<string> emails) : this(names, phones, emails, null)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="extensions"> extensible attributes (optional) </param>
        public Service(List<string> names, List<string> phones, List<string> emails, ExtensibleAttributes extensions)
            : base(GetName(DDMSVersion.CurrentVersion), names, phones, emails, extensions, true)
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
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractRoleEntity#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            base.Validate();
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            return (base.Equals(obj) && (obj is Service));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return (version.IsAtLeast("4.0.1") ? "service" : "Service");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public class Builder : AbstractRoleEntity.Builder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Service service) : base(service)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Service(Names, Phones, Emails, ExtensibleAttributes.Commit()));
            }
        }
    }
}