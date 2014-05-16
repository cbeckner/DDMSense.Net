#region usings

using System.Collections.Generic;
using System.Linq;
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

namespace DDMSense.DDMS.SecurityElements.Ntk
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ntk:AccessGroup.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ntk:AccessSystemName</u>: The system described by this access record (exactly 1 required),
    ///                 implemented as a
    ///                 <see cref="SystemName" /><br />
    ///                 <u>ntk:AccessGroupValue</u>: The value used to describe the group (1-to-many required), implemented as
    ///                 a
    ///                 <see cref="GroupValue" /><br />
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
    public sealed class Group : AbstractAccessEntity
    {
        private readonly List<GroupValue> _groupValues;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Group(Element element) : base(element)
        {
            try
            {
                IEnumerable<Element> values = element.Elements(XName.Get(GroupValue.GetName(DDMSVersion), Namespace));
                _groupValues = new List<GroupValue>();
                values.ToList().ForEach(p => _groupValues.Add(new GroupValue(p)));
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
        /// <param name="groupValues"> the list of values (at least 1 required) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Group(SystemName systemName, List<GroupValue> groupValues, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.CurrentVersion), systemName, securityAttributes)
        {
            try
            {
                if (groupValues == null)
                {
                    groupValues = new List<GroupValue>();
                }
                foreach (var value in groupValues)
                {
                    Element.Add(value.ElementCopy);
                }
                _groupValues = groupValues;
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
                list.AddRange(GroupValues);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the list of group values (1-many)
        /// </summary>
        public List<GroupValue> GroupValues
        {
            get { return _groupValues; }
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
        ///                 <li>At least 1 group value is required.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, Namespace, GetName(DDMSVersion));
            if (GroupValues.Count == 0)
            {
                throw new InvalidDDMSException("At least one group value is required.");
            }

            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "group", suffix) + ".";
            var text = new StringBuilder();
            if (SystemName != null)
            {
                text.Append(SystemName.GetOutput(isHtml, localPrefix, ""));
            }
            text.Append(BuildOutput(isHtml, localPrefix, GroupValues));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Group))
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
            return ("AccessGroup");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractAccessEntity.Builder
        {
            
            internal List<GroupValue.Builder> _groupValues;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Group group) : base(group)
            {
                foreach (var value in group.GroupValues)
                {
                    GroupValues.Add(new GroupValue.Builder(value));
                }
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in GroupValues)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && base.Empty);
                }
            }

            /// <summary>
            ///     Builder accessor for the values
            /// </summary>
            public virtual List<GroupValue.Builder> GroupValues
            {
                get
                {
                    if (_groupValues == null)
                    {
                        _groupValues = new List<GroupValue.Builder>();
                    }
                    return _groupValues;
                }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var values = new List<GroupValue>();
                foreach (IBuilder builder in GroupValues)
                {
                    var component = (GroupValue) builder.Commit();
                    if (component != null)
                    {
                        values.Add(component);
                    }
                }
                return (new Group((SystemName) SystemName.Commit(), values, SecurityAttributes.Commit()));
            }
        }
    }
}