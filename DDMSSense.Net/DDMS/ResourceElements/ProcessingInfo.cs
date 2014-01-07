#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
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
    ///     An immutable implementation of ddms:processingInfo.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A processingInfo element can be used without any child text.</li>
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
    ///                 <u>ddms:dateProcessed</u>: date when this processing occurred (required)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public sealed class ProcessingInfo : AbstractSimpleString
    {
        private const string DATE_PROCESSED_NAME = "dateProcessed";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ProcessingInfo(Element element) : base(element, true)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="value"> the value of the child text </param>
        /// <param name="dateProcessed"> the processing date (required) </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ProcessingInfo(string value, string dateProcessed, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.GetCurrentVersion()), value, securityAttributes, false)
        {
            try
            {
                Util.Util.AddDDMSAttribute(Element, DATE_PROCESSED_NAME, dateProcessed);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the processing date (required). Returns a copy.
        /// </summary>
        /// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
        /// DateTime is no longer a sufficient representation. This accessor will return
        /// null for dates in the new format. Use
        /// <code>getDateProcessedString()</code>
        /// to
        /// access the raw XML format of the date instead.
        public DateTime? DateProcessed
        {
            get
            {
                try
                {
                    return (DateTime.Parse(DateProcessedString));
                }
                catch (ArgumentException)
                {
                    return (null);
                }
            }
        }

        /// <summary>
        ///     Accessor for the processing date (required).
        /// </summary>
        public string DateProcessedString
        {
            get { return (GetAttributeValue(DATE_PROCESSED_NAME)); }
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
        ///                 <li>The dateProcessed exists, and is an acceptable date format.</li>
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
            Util.Util.RequireDDMSValue(DATE_PROCESSED_NAME, DateProcessedString);
            Util.Util.RequireDDMSDateFormat(DateProcessedString, Namespace);

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
        ///                 <li>A ddms:processingInfo element was found with no child text.</li>
        ///                 <li>Include any warnings from the security attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Value))
            {
                AddWarning("A ddms:processingInfo element was found with no value.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Value));
            text.Append(BuildOutput(isHtml, localPrefix + "." + DATE_PROCESSED_NAME, DateProcessedString));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is ProcessingInfo))
            {
                return (false);
            }
            var test = (ProcessingInfo) obj;
            return (Util.Util.NullEquals(DateProcessedString, test.DateProcessedString));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("processingInfo");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractSimpleString.Builder
        {
            internal const long SerialVersionUID = -7348511606867959470L;
            internal string _dateProcessed;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(ProcessingInfo info) : base(info)
            {
                DateProcessed = info.DateProcessedString;
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get { return (base.Empty && String.IsNullOrEmpty(DateProcessed)); }
            }

            /// <summary>
            ///     Builder accessor for the dateProcessed
            /// </summary>
            public virtual string DateProcessed
            {
                get { return _dateProcessed; }
                set { _dateProcessed = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new ProcessingInfo(Value, DateProcessed, SecurityAttributes.Commit()));
            }
        }
    }
}