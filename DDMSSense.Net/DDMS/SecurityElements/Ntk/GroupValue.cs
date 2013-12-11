#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Util;

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

namespace DDMSSense.DDMS.SecurityElements.Ntk
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ntk:AccessGroupValue.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A group value element can be used without any child text.</li>
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
    ///                 <u>ntk:id</u>: A unique XML identifier (optional)<br />
    ///                 <u>ntk:IDReference</u>: A cross-reference to a unique identifier (optional)<br />
    ///                 <u>ntk:qualifier</u>: A user-defined property within an element for general purpose processing used
    ///                 with block
    ///                 objects to provide supplemental information over and above that conveyed by the element name (optional)
    ///                 <br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public sealed class GroupValue : AbstractNtkString
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public GroupValue(Element element) : base(false, element)
        {
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="value"> the value of the element's child text </param>
        /// <param name="id"> the NTK ID (optional) </param>
        /// <param name="idReference"> a reference to an NTK ID (optional) </param>
        /// <param name="qualifier"> an NTK qualifier (optional) </param>
        /// <param name="securityAttributes"> the security attributes </param>
        public GroupValue(string value, string id, string idReference, string qualifier,
            SecurityAttributes securityAttributes)
            : base(
                false, GetName(DDMSVersion.GetCurrentVersion()), value, id, idReference, qualifier, securityAttributes,
                true)
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
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, Namespace, GetName(DDMSVersion));
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
        ///                 <li>An element was found with no child text.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Value))
            {
                AddWarning("A ntk:" + Name + " element was found with no value.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "groupValue", suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Value));
            text.Append(BuildOutput(isHtml, localPrefix + ".id", ID));
            text.Append(BuildOutput(isHtml, localPrefix + ".idReference", IDReference));
            text.Append(BuildOutput(isHtml, localPrefix + ".qualifier", Qualifier));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("AccessGroupValue");
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is GroupValue))
            {
                return (false);
            }
            return (true);
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractNtkString.Builder
        {
            internal const long SerialVersionUID = 7750664735441105296L;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(GroupValue value) : base(value)
            {
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new GroupValue(Value, ID, IDReference, Qualifier, SecurityAttributes.Commit()));
            }
        }
    }
}