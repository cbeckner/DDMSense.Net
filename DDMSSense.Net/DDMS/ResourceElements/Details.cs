#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.ResourceElements
{
    /// <summary>
    ///     An immutable implementation of ddms:details.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A details element can be used without any child text.</li>
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
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Details : AbstractSimpleString
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Details(XElement element) : base(element, true)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="value"> the value of the child text </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Details(string value, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.CurrentVersion), value, securityAttributes, true)
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
        ///                 <li>A classification is required.</li>
        ///                 <li>At least 1 ownerProducer exists and is non-empty.</li>
        ///                 <li>This component cannot be used until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

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
        ///                 <li>A ddms:details element was found with no child text.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Value))
                AddWarning("A ddms:details element was found with no value.");
            
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Value));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Details))
                return (false);
            
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
            return ("details");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        public class Builder : AbstractSimpleString.Builder
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
            public Builder(Details details) : base(details)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Details(Value, SecurityAttributes.Commit()));
            }
        }
    }
}