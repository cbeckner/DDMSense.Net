#region usings

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DDMSense.DDMS;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.DDMS.SecurityElements.Ntk;
using DDMSense.Util;

#endregion

namespace DDMSense
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     Base class for NTK elements which describe system access rules for an individual, group, or profile.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before the component is used.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ntk:AccessSystemName</u>: The system described by this access record (exactly 1 required),
    ///                 implemented as a
    ///                 <see cref="SystemName" /><br />
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
    public abstract class AbstractAccessEntity : AbstractBaseComponent
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element. Does not validate.
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public AbstractAccessEntity(Element element)
        {
            SetElement(element, false);
            Element systemElement = element.Element(XName.Get(SystemName.GetName(DDMSVersion), Namespace));
            if (systemElement != null)
                SystemName = new SystemName(systemElement);
            
            SecurityAttributes = new SecurityAttributes(element);
        }

        /// <summary>
        ///     Constructor for creating a component from raw data. Does not validate.
        /// </summary>
        /// <param name="systemName"> the system name (required) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public AbstractAccessEntity(string name, SystemName systemName, SecurityAttributes securityAttributes)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            Element element = Util.Util.BuildElement(PropertyReader.GetPrefix("ntk"), name, version.NtkNamespace, null);
            SetElement(element, false);
            if (systemName != null)
                element.Add(systemName.ElementCopy);
            
            SystemName = systemName;
            SecurityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
            SecurityAttributes.AddTo(element);
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(SystemName);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the system name
        /// </summary>
        public virtual SystemName SystemName {get;set;}
        
        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
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
        ///                 <li>A systemName is required.</li>
        ///                 <li>Exactly 1 systemName exists.</li>
        ///                 <li>A classification is required.</li>
        ///                 <li>At least 1 ownerProducer exists and is non-empty.</li>
        ///                 <li>This component cannot exist until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSValue("systemName", SystemName);
            Util.Util.RequireBoundedChildCount(Element, SystemName.GetName(DDMSVersion), 1, 1);
            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractAccessEntity))
                return (false);
            
            return (true);
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
                SecurityAttributes = new SecurityAttributes.Builder();
                SystemName = new SystemName.Builder();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(AbstractAccessEntity group)
            {
                if (group.SystemName != null)
                    SystemName = new SystemName.Builder(group.SystemName);
                
                SecurityAttributes = new SecurityAttributes.Builder(group.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the systemName
            /// </summary>
            public virtual SystemName.Builder SystemName {get;set;}

            /// <summary>
            ///     Builder accessor for the securityAttributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes {get;set;}

            public abstract IDDMSComponent Commit();

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (SystemName.Empty && SecurityAttributes.Empty); }
            }
        }
    }
}