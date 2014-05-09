#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

#endregion

namespace DDMSSense.DDMS.ResourceElements
{

    /// <summary>
    ///     An immutable implementation of ddms:language.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>A non-empty qualifier value is required when the value attribute is set.</li>
    ///                 </ul>
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A qualifier can be set with no value.</li>
    ///                     <li>A language can be set without a qualifier or value.</li>
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
    ///                 <u>ddms:qualifier</u>: a URI-based vocabulary (required if value is set)<br />
    ///                 <u>ddms:value</u>: the identification of the content language (optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Language : AbstractQualifierValue
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Language(XElement element) : base(element)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="qualifier">	the value of the qualifier attribute </param>
        /// <param name="value">	the value of the value attribute </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Language(string qualifier, string value)
            : base(GetName(DDMSVersion.GetCurrentVersion()), qualifier, value, true)
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
        ///                 <li>If a value is set, a qualifier must exist and be non-empty.</li>
        ///                 <li>Does not validate that the value is valid against the qualifier's vocabulary.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            if (!String.IsNullOrEmpty(Value))
                Util.Util.RequireDDMSValue("qualifier attribute", Qualifier);

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
        ///                 <li>A qualifier has been set without an accompanying value attribute.</li>
        ///                 <li>Neither a qualifier nor a value was set on this language.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (!String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value))
                AddWarning("A qualifier has been set without an accompanying value attribute.");
            
            if (String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value))
                AddWarning("Neither a qualifier nor a value was set on this language.");
            
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + QUALIFIER_NAME, Qualifier));
            text.Append(BuildOutput(isHtml, localPrefix + VALUE_NAME, Value));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            return (base.Equals(obj) && (obj is Language));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("language");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        public class Builder : AbstractQualifierValue.Builder
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
            public Builder(Language language) : base(language)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Language(Qualifier, Value));
            }
        }
    }
}