#region usings

using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.ResourceElements
{

    /// <summary>
    ///     An immutable implementation of ddms:identifier.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>A non-empty qualifier value is required.</li>
    ///                     <li>A non-empty value attribute is required.</li>
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
    ///                 <u>ddms:qualifier</u>: a URI-based qualifier (required)<br />
    ///                 <u>ddms:value</u>: an unambiguous reference to the resource (required)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Identifier : AbstractQualifierValue
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Identifier(XElement element) : base(element)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="qualifier">	the value of the qualifier attribute </param>
        /// <param name="value">	the value of the value attribute </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Identifier(string qualifier, string value)
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
        ///                 <li>The qualifier exists and is not empty.</li>
        ///                 <li>The value exists and is not empty.</li>
        ///                 <li>The qualifier is a valid URI.</li>
        ///                 <li>Does NOT validate that the value is valid against the qualifier's vocabulary.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("qualifier attribute", Value);
            Util.Util.RequireDDMSValidUri(Value);
            Util.Util.RequireDDMSValue("value attribute", Value);
            base.Validate();
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

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            return (base.Equals(obj) && (obj is Identifier));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("identifier");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
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
            public Builder(Identifier identifier) : base(identifier)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new Identifier(Qualifier, Value));
            }
        }
    }
}