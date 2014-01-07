#region usings

using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.ResourceElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:addressee.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:organization</u>: The organization who is the addressee (0-1, optional), implemented as an
    ///                 <see cref="Organization" /><br />
    ///                 <u>ddms:person</u>: the person who is the addressee (0-1, optional), implemented as a
    ///                 <see cref="Person" /><br />
    ///                 Only one of the nested entities can appear in an addressee element.
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
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public class Addressee : AbstractTaskingRole
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Addressee(Element element) : base(element)
        {
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="entity"> the actual entity who is the addressee (required) </param>
        /// <param name="securityAttributes"> any security attributes (required) </param>
        public Addressee(IRoleEntity entity, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.GetCurrentVersion()), entity, securityAttributes)
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
        ///                 <li>Only 0-1 persons or organizations exist.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractTaskingRole#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Person.GetName(DDMSVersion), 0, 1);
            base.Validate();
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Addressee))
            {
                return (false);
            }
            return (true);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("addressee");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractTaskingRole.Builder
        {
            internal const long SerialVersionUID = 4565840434345629470L;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Addressee info) : base(info)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Addressee(CommitSelectedEntity(), SecurityAttributes.Commit()));
            }
        }
    }
}