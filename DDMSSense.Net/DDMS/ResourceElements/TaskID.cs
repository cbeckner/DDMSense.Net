#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.DDMS.Summary.Xlink;
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

namespace DDMSSense.DDMS.ResourceElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:taskID.
    ///     <para>This element is not a global component, but is being implemented because it has attributes.</para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>The child text must not be empty.</li>
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
    ///                 <u>ddms:taskingSystem</u>: the tasking system (optional)<br />
    ///                 <u>network</u>: the name of the network, taken from a token list (optional)<br />
    ///                 <u>otherNetwork</u>: an alternate network name (optional)<br />
    ///                 <u>
    ///                     <see cref="XLinkAttributes" />
    ///                 </u>
    ///                 : If set, the xlink:type attribute must have a fixed value of "simple".<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public sealed class TaskID : AbstractBaseComponent
    {
        private const string FIXED_TYPE = "simple";

        /// <summary>
        ///     The prefix of the network attributes
        /// </summary>
        public const string NO_PREFIX = "";

        /// <summary>
        ///     The namespace of the network attributes
        /// </summary>
        public const string NO_NAMESPACE = "";

        private const string NETWORK_NAME = "network";
        private const string OTHER_NETWORK_NAME = "otherNetwork";
        private const string TASKING_SYSTEM_NAME = "taskingSystem";
        private XLinkAttributes _xlinkAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TaskID(Element element)
        {
            try
            {
                _xlinkAttributes = new XLinkAttributes(element);
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
        /// <param name="value">	the child text (optional) </param>
        /// <param name="taskingSystem"> the tasking system (optional) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="xlinkAttributes"> simple xlink attributes </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TaskID(string value, string taskingSystem, string network, string otherNetwork,
            XLinkAttributes xlinkAttributes)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.GetCurrentVersion()), value);
                Util.Util.AddDDMSAttribute(element, TASKING_SYSTEM_NAME, taskingSystem);
                Util.Util.AddAttribute(element, NO_PREFIX, NETWORK_NAME, NO_NAMESPACE, network);
                Util.Util.AddAttribute(element, NO_PREFIX, OTHER_NETWORK_NAME, NO_NAMESPACE, otherNetwork);

                _xlinkAttributes = XLinkAttributes.GetNonNullInstance(xlinkAttributes);
                _xlinkAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the value of the child text.
        /// </summary>
        public string Value
        {
            get { return (Element.Value); }
            set { Element.Value = value; }
        }

        /// <summary>
        ///     Accessor for the taskingSystem attribute.
        /// </summary>
        public string TaskingSystem
        {
            get { return (GetAttributeValue(TASKING_SYSTEM_NAME)); }
        }

        /// <summary>
        ///     Accessor for the network attribute.
        /// </summary>
        public string Network
        {
            get { return (GetAttributeValue(NETWORK_NAME, NO_NAMESPACE)); }
        }

        /// <summary>
        ///     Accessor for the otherNetwork attribute.
        /// </summary>
        public string OtherNetwork
        {
            get { return (GetAttributeValue(OTHER_NETWORK_NAME, NO_NAMESPACE)); }
        }

        /// <summary>
        ///     Accessor for the XLink Attributes. Will always be non-null, even if it has no values set.
        /// </summary>
        public XLinkAttributes XLinkAttributes
        {
            get { return (_xlinkAttributes); }
            set { _xlinkAttributes = value; }
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
        ///                 <li>A child text value is required.</li>
        ///                 <li>If set, the xlink:type attribute has a value of "simple".</li>
        ///                 <li>If set, the network attribute must be a valid network token.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("value", Value);
            if (!String.IsNullOrEmpty(XLinkAttributes.Type) && !XLinkAttributes.Type.Equals(FIXED_TYPE))
            {
                throw new InvalidDDMSException("The type attribute must have a fixed value of \"" + FIXED_TYPE + "\".");
            }
            if (!String.IsNullOrEmpty(Network))
            {
                ISMVocabulary.RequireValidNetwork(Network);
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
        ///                 <li>Include any warnings from the XLink attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (XLinkAttributes != null)
            {
                AddWarnings(XLinkAttributes.ValidationWarnings, true);
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Value));
            text.Append(BuildOutput(isHtml, localPrefix + "." + TASKING_SYSTEM_NAME, TaskingSystem));
            text.Append(BuildOutput(isHtml, localPrefix + "." + NETWORK_NAME, Network));
            text.Append(BuildOutput(isHtml, localPrefix + "." + OTHER_NETWORK_NAME, OtherNetwork));
            text.Append(XLinkAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is TaskID))
            {
                return (false);
            }
            var test = (TaskID) obj;
            return (Value.Equals(test.Value) && TaskingSystem.Equals(test.TaskingSystem) && Network.Equals(test.Network) &&
                    OtherNetwork.Equals(test.OtherNetwork) && XLinkAttributes.Equals(test.XLinkAttributes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Value.GetHashCode();
            result = 7*result + TaskingSystem.GetHashCode();
            result = 7*result + Network.GetHashCode();
            result = 7*result + OtherNetwork.GetHashCode();
            result = 7*result + XLinkAttributes.GetHashCode();
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
            return ("taskID");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            internal const long SerialVersionUID = 4325950371570699184L;
            internal string _network;
            internal string _otherNetwork;
            internal string _taskingSystem;
            internal string _value;
            internal XLinkAttributes.Builder _xlinkAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(TaskID taskID)
            {
                Value = taskID.Value;
                TaskingSystem = taskID.TaskingSystem;
                Network = taskID.Network;
                OtherNetwork = taskID.OtherNetwork;
                XLinkAttributes = new XLinkAttributes.Builder(taskID.XLinkAttributes);
            }

            /// <summary>
            ///     Builder accessor for the value
            /// </summary>
            public virtual string Value
            {
                get { return _value; }
                set { _value = value; }
            }


            /// <summary>
            ///     Builder accessor for the taskingSystem
            /// </summary>
            public virtual string TaskingSystem
            {
                get { return _taskingSystem; }
                set { _taskingSystem = value; }
            }


            /// <summary>
            ///     Builder accessor for the network
            /// </summary>
            public virtual string Network
            {
                get { return _network; }
                set { _network = value; }
            }


            /// <summary>
            ///     Builder accessor for the otherNetwork
            /// </summary>
            public virtual string OtherNetwork
            {
                get { return _otherNetwork; }
                set { _otherNetwork = value; }
            }


            /// <summary>
            ///     Builder accessor for the XLink Attributes
            /// </summary>
            public virtual XLinkAttributes.Builder XLinkAttributes
            {
                get
                {
                    if (_xlinkAttributes == null)
                    {
                        _xlinkAttributes = new XLinkAttributes.Builder();
                    }
                    return _xlinkAttributes;
                }
                set { _xlinkAttributes = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                return (Empty ? null : new TaskID(Value, TaskingSystem, Network, OtherNetwork, XLinkAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(Value) && String.IsNullOrEmpty(TaskingSystem) &&
                            String.IsNullOrEmpty(Network) && String.IsNullOrEmpty(OtherNetwork) && XLinkAttributes.Empty);
                }
            }
        }
    }
}