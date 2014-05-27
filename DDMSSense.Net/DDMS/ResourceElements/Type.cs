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
    ///     An immutable implementation of ddms:type.
    ///     <para>
    ///         Beginning in DDMS 4.0.1, a ddms:type element can contain child text. The intent of this text is to provide
    ///         further
    ///         context when the ddms:type element references an IC activity.
    ///     </para>
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
    ///                     <li>A type can be set without a qualifier or value.</li>
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
    ///                 <u>ddms:qualifier</u>: a URI-based qualifier (required if value is set)<br />
    ///                 <u>ddms:value</u>: includes terms describing general categories, functions, genres, or aggregation
    ///                 levels
    ///                 (optional)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and ownerProducer attributes are optional.
    ///                 (starting in DDMS 4.0.1)
    ///             </td>
    ///         </tr>
    ///     </table>
    /// </summary>
    public sealed class Type : AbstractQualifierValue
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Type(XElement element)
        {
            try
            {
                SecurityAttributes = new SecurityAttributes(element);
                SetElement(element, true);
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
        /// <param name="description">
        ///     the child text describing an IC activity, if this component is used to reference an IC
        ///     activity
        /// </param>
        /// <param name="qualifier"> the value of the qualifier attribute </param>
        /// <param name="value"> the value of the value attribute </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Type(string description, string qualifier, string value, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.CurrentVersion), qualifier, value, false)
        {
            try
            {
                XElement element = Element;
                if (!String.IsNullOrEmpty(description))
                    element.Add(description);
                
                SecurityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                SecurityAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the description child text, which provides additional context to the qualifier/value pairing of this
        ///     component. The underlying XOM method which retrieves the child text returns an empty string if not found.
        /// </summary>
        public string Description
        {
            get { return (Element.Value); }
            set { Element.Value = value; }
        }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null, even if it has no values set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes { get; set; }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>The description child text cannot exist until DDMS 4.0.1 or later.</li>
        ///                 <li>If a value is set, a qualifier must exist and be non-empty.</li>
        ///                 <li>Does NOT validate that the value is valid against the qualifier's vocabulary.</li>
        ///                 <li>The SecurityAttributes do not exist until DDMS 4.0.1 or later.</li>
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

            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("4.0.1") && !String.IsNullOrEmpty(Description))
                throw new InvalidDDMSException(                    "This component cannot contain description child text until DDMS 4.0.1 or later.");
            
            if (!DDMSVersion.IsAtLeast("4.0.1") && !SecurityAttributes.Empty)
                throw new InvalidDDMSException(                    "Security attributes cannot be applied to this component until DDMS 4.0.1 or later.");

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
        ///                 <li>Neither a qualifier nor a value was set on this type.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (!String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value))
                AddWarning("A qualifier has been set without an accompanying value XAttribute.");
            
            if (String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value))
                AddWarning("Neither a qualifier nor a value was set on this type.");
            
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + "description", Description));
            text.Append(BuildOutput(isHtml, localPrefix + QUALIFIER_NAME, Qualifier));
            text.Append(BuildOutput(isHtml, localPrefix + VALUE_NAME, Value));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Type))
                return (false);
            
            var test = (Type) obj;
            return (Description.Equals(test.Description));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Description.GetHashCode();
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
            return ("type");
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
                SecurityAttributes = new SecurityAttributes.Builder();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Type type) : base(type)
            {
                Description = type.Description;
                SecurityAttributes = new SecurityAttributes.Builder(type.SecurityAttributes);
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get { return (base.Empty && String.IsNullOrEmpty(Description) && SecurityAttributes.Empty); }
            }

            /// <summary>
            ///     Builder accessor for the description
            /// </summary>
            public virtual string Description { get; set; }

            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {

                return (Empty ? null : new Type(Description, Qualifier, Value, SecurityAttributes.Commit()));
            }
        }
    }
}