#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS.ResourceElements;
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

namespace DDMSSense.DDMS.Summary.Xlink
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     Attribute group for the XLINK attributes.
    ///     <para>
    ///         This class only models the subset of attributes and values that are employed by the DDMS specification.
    ///         Determinations about whether an attribute is optional or required depend on the decorated class
    ///         (<see cref="Link" />, <see cref="RevisionRecall" />, or <see cref="TaskID" />).
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Locator Attributes (for ddms:link)</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>xlink:type</u>: the type of link (optional, but fixed as "locator" if set)<br />
    ///                 <u>xlink:href</u>: A target Uri (optional)<br />
    ///                 <u>xlink:role</u>: The URI reference identifies some resource that describes the intended property.
    ///                 (optional)<br />
    ///                 <u>xlink:title</u>: Used to describe the meaning of a link or resource in a human-readable fashion,
    ///                 along the same
    ///                 lines as the role or arcrole attribute. (optional)<br />
    ///                 <u>xlink:label</u>: The label attribute provides a name for the link (optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Resource Attributes (for ddms:revisionRecall)</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>xlink:type</u>: (optional, but fixed as "resource" if set)<br />
    ///                 <u>xlink:role</u>: The URI reference identifies some resource that describes the intended property.
    ///                 When no value is
    ///                 supplied, no particular role value is to be inferred. (optional, but must be non-empty if set)<br />
    ///                 <u>xlink:title</u>: Used to describe the meaning of a link or resource in a human-readable fashion,
    ///                 along the same
    ///                 lines as the role or arcrole attribute. (optional)<br />
    ///                 <u>xlink:label</u>: The label attribute provides a name for the link (optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Simple Attributes (for ddms:taskID)</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>xlink:type</u>: (optional, but fixed as "simple" if set)<br />
    ///                 <u>xlink:href</u>: A Uri (optional, must be a URI)<br />
    ///                 <u>xlink:role</u>: The URI reference identifies some resource that describes the intended property.
    ///                 When no value is
    ///                 supplied, no particular role value is to be inferred. (optional, but must be non-empty if set)<br />
    ///                 <u>xlink:title</u>: Used to describe the meaning of a link or resource in a human-readable fashion,
    ///                 along the same
    ///                 lines as the role or arcrole attribute. (optional)<br />
    ///                 <u>xlink:arcrole</u>: A URI reference describing an arc role (optional)<br />
    ///                 <u>xlink:show</u>: A token which signifies the behavior intentions for traversal (optional)<br />
    ///                 <u>xlink:actuate</u>: A token which signifies the behavior intentions for traversal (optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public sealed class XLinkAttributes : AbstractAttributeGroup
    {
        private const string TYPE_NAME = "type";
        private const string HREF_NAME = "href";
        private const string ROLE_NAME = "role";
        private const string TITLE_NAME = "title";
        private const string LABEL_NAME = "label";
        private const string ARC_ROLE_NAME = "arcrole";
        private const string SHOW_NAME = "show";
        private const string ACTUATE_NAME = "actuate";

        private const string TYPE_LOCATOR = "locator";
        private const string TYPE_SIMPLE = "simple";
        private const string TYPE_RESOURCE = "resource";

        private static readonly List<string> TYPE_TYPES = new List<string>();

        private static readonly List<string> SHOW_TYPES = new List<string>();

        private static readonly List<string> ACTUATE_TYPES = new List<string>();
        private string _actuate;
        private string _arcrole;
        private string _href;
        private string _label;
        private string _role;
        private string _show;
        private string _title;
        private string _type;

        static XLinkAttributes()
        {
            TYPE_TYPES.Add(TYPE_LOCATOR);
            TYPE_TYPES.Add(TYPE_SIMPLE);
            TYPE_TYPES.Add(TYPE_RESOURCE);
            SHOW_TYPES.Add("new");
            SHOW_TYPES.Add("replace");
            SHOW_TYPES.Add("embed");
            SHOW_TYPES.Add("other");
            SHOW_TYPES.Add("none");
            ACTUATE_TYPES.Add("onLoad");
            ACTUATE_TYPES.Add("onRequest");
            ACTUATE_TYPES.Add("other");
            ACTUATE_TYPES.Add("none");
        }

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element which is decorated with these attributes. </param>
        public XLinkAttributes(Element element) : base(element.Name.NamespaceName)
        {
            string xlinkNamespace = DDMSVersion.XlinkNamespace;
            _type = element.Attribute(XName.Get(TYPE_NAME, xlinkNamespace)).Value;
            _href = element.Attribute(XName.Get(HREF_NAME, xlinkNamespace)).Value;
            _role = element.Attribute(XName.Get(ROLE_NAME, xlinkNamespace)).Value;
            _title = element.Attribute(XName.Get(TITLE_NAME, xlinkNamespace)).Value;
            _label = element.Attribute(XName.Get(LABEL_NAME, xlinkNamespace)).Value;
            _arcrole = element.Attribute(XName.Get(ARC_ROLE_NAME, xlinkNamespace)).Value;
            _show = element.Attribute(XName.Get(SHOW_NAME, xlinkNamespace)).Value;
            _actuate = element.Attribute(XName.Get(ACTUATE_NAME, xlinkNamespace)).Value;
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data for an unknown type.
        /// </summary>
        /// <exception cref="InvalidDDMSException"> </exception>
        public XLinkAttributes() : base(DDMSVersion.GetCurrentVersion().Namespace)
        {
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data for a resource link.
        /// </summary>
        /// <param name="role">	the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="label"> the name of the link (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public XLinkAttributes(string role, string title, string label)
            : base(DDMSVersion.GetCurrentVersion().Namespace)
        {
            _type = TYPE_RESOURCE;
            _role = role;
            _title = title;
            _label = label;
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data for a locator link.
        /// </summary>
        /// <param name="href"> the link href (optional) </param>
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="label"> the name of the link (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public XLinkAttributes(string href, string role, string title, string label)
            : base(DDMSVersion.GetCurrentVersion().Namespace)
        {
            _type = TYPE_LOCATOR;
            _href = href;
            _role = role;
            _title = title;
            _label = label;
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data for a simple link.
        /// </summary>
        /// <param name="href">	the link href (optional) </param>
        /// <param name="role">	the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="arcrole"> the arcrole (optional) </param>
        /// <param name="show"> the show token (optional) </param>
        /// <param name="actuate"> the actuate token (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public XLinkAttributes(string href, string role, string title, string arcrole, string show, string actuate)
            : base(DDMSVersion.GetCurrentVersion().Namespace)
        {
            _type = TYPE_SIMPLE;
            _href = href;
            _role = role;
            _title = title;
            _arcrole = arcrole;
            _show = show;
            _actuate = actuate;
            Validate();
        }

        /// <summary>
        ///     Accessor for the type
        /// </summary>
        public string Type
        {
            get { return (Util.Util.GetNonNullString(_type)); }
            set { _type = value; }
        }

        /// <summary>
        ///     Accessor for the href
        /// </summary>
        public string Href
        {
            get { return (Util.Util.GetNonNullString(_href)); }
            set { _href = value; }
        }

        /// <summary>
        ///     Accessor for the role
        /// </summary>
        public string Role
        {
            get { return (Util.Util.GetNonNullString(_role)); }
            set { _role = value; }
        }

        /// <summary>
        ///     Accessor for the title
        /// </summary>
        public string Title
        {
            get { return (Util.Util.GetNonNullString(_title)); }
            set { _title = value; }
        }

        /// <summary>
        ///     Accessor for the label
        /// </summary>
        public string Label
        {
            get { return (Util.Util.GetNonNullString(_label)); }
            set { _label = value; }
        }

        /// <summary>
        ///     Accessor for the arcrole
        /// </summary>
        public string Arcrole
        {
            get { return (Util.Util.GetNonNullString(_arcrole)); }
            set { _arcrole = value; }
        }

        /// <summary>
        ///     Accessor for the show
        /// </summary>
        public string Show
        {
            get { return (Util.Util.GetNonNullString(_show)); }
            set { _show = value; }
        }

        /// <summary>
        ///     Accessor for the actuate
        /// </summary>
        public string Actuate
        {
            get { return (Util.Util.GetNonNullString(_actuate)); }
            set { _actuate = value; }
        }

        /// <summary>
        ///     Returns a non-null instance of XLink attributes. If the instance passed in is not null, it will be returned.
        /// </summary>
        /// <param name="xlinkAttributes"> the attributes to return by default </param>
        /// <returns> a non-null attributes instance </returns>
        /// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>
        public static XLinkAttributes GetNonNullInstance(XLinkAttributes xlinkAttributes)
        {
            return (xlinkAttributes == null ? new XLinkAttributes() : xlinkAttributes);
        }

        /// <summary>
        ///     Convenience method to add these attributes onto an existing XOM Element
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        /// <exception cref="InvalidDDMSException"> if the DDMS version of the element is different </exception>
        public void AddTo(Element element)
        {
            DDMSVersion elementVersion = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
            ValidateSameVersion(elementVersion);
            string xlinkNamespace = elementVersion.XlinkNamespace;
            string xlinkPrefix = PropertyReader.GetPrefix("xlink");

            Util.Util.AddAttribute(element, xlinkPrefix, TYPE_NAME, xlinkNamespace, Type);
            Util.Util.AddAttribute(element, xlinkPrefix, HREF_NAME, xlinkNamespace, Href);
            Util.Util.AddAttribute(element, xlinkPrefix, ROLE_NAME, xlinkNamespace, Role);
            Util.Util.AddAttribute(element, xlinkPrefix, TITLE_NAME, xlinkNamespace, Title);
            Util.Util.AddAttribute(element, xlinkPrefix, LABEL_NAME, xlinkNamespace, Label);
            Util.Util.AddAttribute(element, xlinkPrefix, ARC_ROLE_NAME, xlinkNamespace, Arcrole);
            Util.Util.AddAttribute(element, xlinkPrefix, SHOW_NAME, xlinkNamespace, Show);
            Util.Util.AddAttribute(element, xlinkPrefix, ACTUATE_NAME, xlinkNamespace, Actuate);
        }

        /// <summary>
        ///     Validates the attribute group.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>If the href attribute is set, it is a valid URI.</li>
        ///                 <li>If the role attribute is set, it is a valid URI, starting in DDMS 4.0.1.</li>
        ///                 <li>If the label attribute is set, it is a valid NCName, starting in DDMS 4.0.1.</li>
        ///                 <li>If the arcrole attribute is set, it is a valid URI.</li>
        ///                 <li>If the show attribute is set, it is a valid token.</li>
        ///                 <li>If the actuate attribute is set, it is a valid token.</li>
        ///                 <li>
        ///                     Does not validate the required nature of any attribute. It is the parent class'
        ///                     responsibility to do that.
        ///                 </li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            if (!String.IsNullOrEmpty(Href))
            {
                Util.Util.RequireDDMSValidUri(Href);
            }

            // Should be reviewed as additional versions of DDMS are supported.
            if (DDMSVersion.IsAtLeast("4.0.1"))
            {
                if (!String.IsNullOrEmpty(Role))
                {
                    Util.Util.RequireDDMSValidUri(Role);
                }
                if (!String.IsNullOrEmpty(Label))
                {
                    Util.Util.RequireValidNCName(Label);
                }
            }
            if (!String.IsNullOrEmpty(Arcrole))
            {
                Util.Util.RequireDDMSValidUri(Arcrole);
            }
            if (!String.IsNullOrEmpty(Show) && !SHOW_TYPES.Contains(Show))
            {
                throw new InvalidDDMSException("The show attribute must be one of " + SHOW_TYPES);
            }
            if (!String.IsNullOrEmpty(Actuate) && !ACTUATE_TYPES.Contains(Actuate))
            {
                throw new InvalidDDMSException("The actuate attribute must be one of " + ACTUATE_TYPES);
            }
            base.Validate();
        }

        /// <see cref="AbstractAttributeGroup#getOutput(boolean, String)"></see>
        public override string GetOutput(bool isHtml, string prefix)
        {
            string localPrefix = Util.Util.GetNonNullString(prefix);
            var text = new StringBuilder();
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + TYPE_NAME, Type));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + HREF_NAME, Href));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + ROLE_NAME, Role));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + TITLE_NAME, Title));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + LABEL_NAME, Label));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + ARC_ROLE_NAME, Arcrole));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + SHOW_NAME, Show));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + ACTUATE_NAME, Actuate));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!(obj is XLinkAttributes))
            {
                return (false);
            }
            var test = (XLinkAttributes) obj;
            return (Type.Equals(test.Type) && Href.Equals(test.Href) && Role.Equals(test.Role) &&
                    Title.Equals(test.Title) && Label.Equals(test.Label) && Arcrole.Equals(test.Arcrole) &&
                    Show.Equals(test.Show) && Actuate.Equals(test.Actuate));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = 0;
            result = 7*result + Type.GetHashCode();
            result = 7*result + Href.GetHashCode();
            result = 7*result + Role.GetHashCode();
            result = 7*result + Title.GetHashCode();
            result = 7*result + Label.GetHashCode();
            result = 7*result + Arcrole.GetHashCode();
            result = 7*result + Show.GetHashCode();
            result = 7*result + Actuate.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Builder for these attributes.
        ///     <para>
        ///         This class does not implement the IBuilder interface, because the behavior of commit() is at odds with the
        ///         standard commit() method. As an attribute group, an empty attribute group will always be returned instead of
        ///         null.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        [Serializable]
        public class Builder
        {
            internal const long SerialVersionUID = 6071979027185230870L;
            internal string _actuate;
            internal string _arcrole;
            internal string _href;
            internal string _label;
            internal string _role;
            internal string _show;
            internal string _title;
            internal string _type;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(XLinkAttributes attributes)
            {
                Type = attributes.Type;
                Href = attributes.Href;
                Role = attributes.Role;
                Title = attributes.Title;
                Label = attributes.Label;
                Arcrole = attributes.Arcrole;
                Show = attributes.Show;
                Actuate = attributes.Actuate;
            }

            /// <summary>
            ///     Checks if any values have been provided for this Builder.
            /// </summary>
            /// <returns> true if every field is empty </returns>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(Type) && String.IsNullOrEmpty(Href) && String.IsNullOrEmpty(Role) &&
                            String.IsNullOrEmpty(Title) && String.IsNullOrEmpty(Label) && String.IsNullOrEmpty(Arcrole) &&
                            String.IsNullOrEmpty(Show) && String.IsNullOrEmpty(Actuate));
                }
            }

            /// <summary>
            ///     Builder accessor for the type
            /// </summary>
            public virtual string Type
            {
                get { return _type; }
                set { _type = value; }
            }


            /// <summary>
            ///     Builder accessor for the href
            /// </summary>
            public virtual string Href
            {
                get { return _href; }
                set { _href = value; }
            }


            /// <summary>
            ///     Builder accessor for the role
            /// </summary>
            public virtual string Role
            {
                get { return _role; }
                set { _role = value; }
            }


            /// <summary>
            ///     Builder accessor for the title
            /// </summary>
            public virtual string Title
            {
                get { return _title; }
                set { _title = value; }
            }


            /// <summary>
            ///     Builder accessor for the label
            /// </summary>
            public virtual string Label
            {
                get { return _label; }
                set { _label = value; }
            }


            /// <summary>
            ///     Builder accessor for the arcrole
            /// </summary>
            public virtual string Arcrole
            {
                get { return _arcrole; }
                set { _arcrole = value; }
            }


            /// <summary>
            ///     Builder accessor for the show
            /// </summary>
            public virtual string Show
            {
                get { return _show; }
                set { _show = value; }
            }


            /// <summary>
            ///     Builder accessor for the actuate
            /// </summary>
            public virtual string Actuate
            {
                get { return _actuate; }
                set { _actuate = value; }
            }

            /// <summary>
            ///     Finalizes the data gathered for this builder instance. Will always return an empty instance instead of a null
            ///     one.
            /// </summary>
            /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
            public virtual XLinkAttributes Commit()
            {
                if (TYPE_LOCATOR.Equals(Type))
                {
                    return (new XLinkAttributes(Href, Role, Title, Label));
                }
                if (TYPE_SIMPLE.Equals(Type))
                {
                    return (new XLinkAttributes(Href, Role, Title, Arcrole, Show, Actuate));
                }
                if (TYPE_RESOURCE.Equals(Type))
                {
                    return (new XLinkAttributes(Role, Title, Label));
                }
                return (new XLinkAttributes());
            }
        }
    }
}