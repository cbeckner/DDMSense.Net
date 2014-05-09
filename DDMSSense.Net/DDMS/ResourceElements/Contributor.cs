#region usings

using System.Collections.Generic;
using System.Xml.Linq;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Util;

#endregion

namespace DDMSSense.DDMS.ResourceElements
{

    /// <summary>
    ///     An immutable implementation of ddms:contributor.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:organization</u>: The organization who is in this role (0-1, optional), implemented as an
    ///                 <see cref="Organization" /><br />
    ///                 <u>ddms:person</u>: the person who is in this role (0-1, optional), implemented as a
    ///                 <see cref="Person" /><br />
    ///                 <u>ddms:service</u>: The web service who is in this role (0-1, optional), implemented as a
    ///                 <see cref="Service" /><br />
    ///                 <u>ddms:unknown</u>: The unknown entity who is in this role (0-1, optional), implemented as an
    ///                 <see cref="Unknown" /><br />
    ///                 Only one of the nested entities can appear in this element.
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
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are optional.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public class Contributor : AbstractProducerRole
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Contributor(XElement element) : base(element)
        {
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="entity"> the actual entity fulfilling this role </param>
        /// <param name="pocTypes"> the ISM pocType for this producer (optional, starting in DDMS 4.0.1) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        public Contributor(IRoleEntity entity, List<string> pocTypes, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.GetCurrentVersion()), entity, pocTypes, securityAttributes)
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
        /// <see cref="AbstractProducerRole#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            base.Validate();
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            return (base.Equals(obj) && (obj is Contributor));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("contributor");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        public class Builder : AbstractProducerRole.Builder
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
            public Builder(Contributor producer) : base(producer)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Contributor(CommitSelectedEntity(), PocTypes, SecurityAttributes.Commit()));
            }
        }
    }
}
