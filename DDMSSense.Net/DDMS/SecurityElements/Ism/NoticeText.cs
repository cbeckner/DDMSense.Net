#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.SecurityElements.Ism
{
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
    /// </summary>
    public sealed class NoticeText : AbstractSimpleString
    {
        private const string POC_TYPE_NAME = "pocType";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NoticeText(XElement element) : base(element, false)
        {
            try
            {
                string pocTypes = (string)element.Attribute(XName.Get(POC_TYPE_NAME, DDMSVersion.IsmNamespace));
                PocTypes = Util.Util.GetXsListAsList(pocTypes);
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
            : base(                PropertyReader.GetPrefix("ism"), DDMSVersion.GetCurrentVersion().IsmNamespace,                GetName(DDMSVersion.GetCurrentVersion()), value, securityAttributes, false)
        {
            try
            {
                if (pocTypes == null)
                    pocTypes = new List<string>();
                
                if (pocTypes.Count > 0)
                    Util.Util.AddAttribute(Element, PropertyReader.GetPrefix("ism"), POC_TYPE_NAME,                        DDMSVersion.GetCurrentVersion().IsmNamespace, Util.Util.GetXsList(pocTypes));
                
                PocTypes = pocTypes;
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
        public List<string> PocTypes { get; set; }

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
                    ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_POC_TYPE, pocType);
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
                AddWarning("An ISM:" + Name + " element was found with no value.");
            
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
                return (false);
            
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
        /// <see cref="IBuilder"></see>
        public class Builder : AbstractSimpleString.Builder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
                PocTypes = new List<string>();
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
            public virtual List<string> PocTypes { get; set; }

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