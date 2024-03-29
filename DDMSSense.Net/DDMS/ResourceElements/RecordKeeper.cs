#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.ResourceElements
{
    /// <summary>
    ///     An immutable implementation of ddms:recordKeeper.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>The recordKeeperID must not be empty.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:recordKeeperID</u>: A unique identifier for the Record Keeper (exactly 1 required)<br />
    ///                 <u>ddms:organization</u>: The organization which acts as the record keeper (exactly 1 required),
    ///                 implemented as an
    ///                 <see cref="Organization" /><br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public class RecordKeeper : AbstractBaseComponent
    {
        private const string RECORD_KEEPER_ID_NAME = "recordKeeperID";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public RecordKeeper(XElement element)
        {
            try
            {
                Util.Util.RequireDDMSValue("element", element);
                if (element.Nodes().Count() > 1)
                {
                    var organizationElement = (XElement) element.FirstNode.NextNode;
                    if (organizationElement != null)
                        Organization = new Organization(organizationElement);
                }
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="recordKeeperID"> a unique ID for the organization (required) </param>
        /// <param name="organization"> the organization acting as record keeper (required) </param>
        public RecordKeeper(string recordKeeperID, Organization organization)
        {
            try
            {
                XElement element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);
                if (!String.IsNullOrEmpty(recordKeeperID))
                    element.Add(Util.Util.BuildDDMSElement(RECORD_KEEPER_ID_NAME, recordKeeperID));
                
                if (organization != null)
                    element.Add(organization.ElementCopy);
                
                Organization = organization;
                SetElement(element, true);
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
                var list = new List<IDDMSComponent>();
                list.Add(Organization);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the recordKeeperID
        /// </summary>
        public virtual string RecordKeeperID
        {
            get { return (Util.Util.GetFirstDDMSChildValue(Element, RECORD_KEEPER_ID_NAME)); }
        }

        /// <summary>
        ///     Accessor for the organization
        /// </summary>
        public virtual Organization Organization{get;set;}

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>The recordKeeperID exists.</li>
        ///                 <li>The organization exists.</li>
        ///                 <li>Exactly 1 organization exists.</li>
        ///                 <li>This component cannot exist until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractProducerRole#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("record keeper ID", RecordKeeperID);
            Util.Util.RequireDDMSValue("organization", Organization);
            Util.Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 1, 1);

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is RecordKeeper))
            {
                return (false);
            }
            var test = (RecordKeeper) obj;
            return (RecordKeeperID.Equals(test.RecordKeeperID));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + RecordKeeperID.GetHashCode();
            return (result);
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + RECORD_KEEPER_ID_NAME, RecordKeeperID));
            text.Append(Organization.GetOutput(isHtml, localPrefix, ""));
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
            return ("recordKeeper");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
                Organization = new Organization.Builder();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(RecordKeeper keeper) : this()
            {
                RecordKeeperID = keeper.RecordKeeperID;
                Organization = new Organization.Builder(keeper.Organization);
            }

            /// <summary>
            ///     Builder accessor for the recordKeeperID
            /// </summary>
            public virtual string RecordKeeperID { get; set; }


            /// <summary>
            ///     Builder accessor for the organization builder
            /// </summary>
            public virtual Organization.Builder Organization { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                return (Empty ? null : new RecordKeeper(RecordKeeperID, (Organization) Organization.Commit()));
            }

            /// <summary>
            ///     Helper method to determine if any values have been entered.
            /// </summary>
            /// <returns> true if all values are empty </returns>
            public virtual bool Empty
            {
                get { return (Organization.Empty && String.IsNullOrEmpty(RecordKeeperID)); }
            }
        }
    }
}