#region usings

using System.Collections.Generic;
using System.Linq;
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
    ///     An immutable implementation of ntk:AccessProfile.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ntk:AccessSystemName</u>: The system described by this access record (exactly 1 required),
    ///                 implemented as a
    ///                 <see cref="SystemName" /><br />
    ///                 <u>ntk:AccessProfileValue</u>: The value used to describe the profile (1-to-many required), implemented
    ///                 as a
    ///                 <see cref="ProfileValue" /><br />
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
    public sealed class Profile : AbstractAccessEntity
    {
        private readonly List<ProfileValue> _profileValues;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Profile(Element element) : base(element)
        {
            try
            {
                IEnumerable<Element> values = element.Elements(XName.Get(ProfileValue.GetName(DDMSVersion), Namespace));
                _profileValues = new List<ProfileValue>();
                values.ToList().ForEach(p => _profileValues.Add(new ProfileValue(p)));
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
        /// <param name="systemName"> the system name (required) </param>
        /// <param name="profileValues"> the list of values (at least 1 required) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Profile(SystemName systemName, List<ProfileValue> profileValues, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.GetCurrentVersion()), systemName, securityAttributes)
        {
            try
            {
                if (profileValues == null)
                {
                    profileValues = new List<ProfileValue>();
                }
                foreach (var value in profileValues)
                {
                    Element.Add(value.ElementCopy);
                }
                _profileValues = profileValues;
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                List<IDDMSComponent> list = base.NestedComponents;
                list.AddRange(ProfileValues);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the list of profile values (1-many)
        /// </summary>
        public List<ProfileValue> ProfileValues
        {
            get { return _profileValues; }
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
        ///                 <li>At least 1 profile value is required.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, Namespace, GetName(DDMSVersion));
            if (ProfileValues.Count == 0)
            {
                throw new InvalidDDMSException("At least one profile value is required.");
            }

            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "profile", suffix) + ".";
            var text = new StringBuilder();
            if (SystemName != null)
            {
                text.Append(SystemName.GetOutput(isHtml, localPrefix, ""));
            }
            text.Append(BuildOutput(isHtml, localPrefix, ProfileValues));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Profile))
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
            return ("AccessProfile");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractAccessEntity.Builder
        {
            
            internal List<ProfileValue.Builder> _profileValues;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Profile profile) : base(profile)
            {
                foreach (var value in profile.ProfileValues)
                {
                    ProfileValues.Add(new ProfileValue.Builder(value));
                }
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in ProfileValues)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && base.Empty);
                }
            }

            /// <summary>
            ///     Builder accessor for the values
            /// </summary>
            public virtual List<ProfileValue.Builder> ProfileValues
            {
                get
                {
                    if (_profileValues == null)
                    {
                        _profileValues = new List<ProfileValue.Builder>();
                    }
                    return _profileValues;
                }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var values = new List<ProfileValue>();
                foreach (IBuilder builder in ProfileValues)
                {
                    var component = (ProfileValue) builder.Commit();
                    if (component != null)
                    {
                        values.Add(component);
                    }
                }
                return (new Profile((SystemName) SystemName.Commit(), values, SecurityAttributes.Commit()));
            }
        }
    }
}