#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.DDMS.Summary;
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

namespace DDMSSense.DDMS.Extensible
{
    #region usings

    using Attribute = XAttribute;
    using Element = XElement;

    #endregion

    /// <summary>
    ///     Attribute group representing the xs:anyAttribute tag which appears on various DDMS components.
    ///     <para>
    ///         Starting in DDMS 3.0, this attribute group can decorate <see cref="DDMSSense.DDMS.Resource.Organization" />,
    ///         <see cref="DDMSSense.DDMS.Resource.Person" />, <see cref="DDMSSense.DDMS.Resource.Service" />,
    ///         <see cref="DDMSSense.DDMS.Resource.Unknown" />, <see cref="Keyword" />, <see cref="Category" />, or the
    ///         <see cref="Resource" /> itself.
    ///         In DDMS 2.0, this attribute group can only decorate <see cref="DDMSSense.DDMS.Resource.Organization" />,
    ///         <see cref="DDMSSense.DDMS.Resource.Person" />, <see cref="DDMSSense.DDMS.Resource.Service" />, or the
    ///         <see cref="Resource" />.
    ///     </para>
    ///     <para>
    ///         No validation or processing of any kind is performed by DDMSence on extensible attributes, other than the base
    ///         validation used when loading attributes from an XML file, and a check to confirm that extensible attributes do
    ///         not
    ///         collide with existing attributes. This class merely exposes a <code>getAttributes()</code> method which returns
    ///         a
    ///         read-only List of XOM Attributes that can be manipulated in business-specific ways.
    ///     </para>
    ///     <para>
    ///         For example, this ddms:Keyword would have an ExtensibleAttributes instance containing 2 attributes (assuming
    ///         that
    ///         the "opensearch" namespace was defined earlier in the file):
    ///     </para>
    ///     <ul>
    ///         <code> &lt;ddms:Keyword ddms:value="xml" opensearch:relevance="95" opensearch:confidence="82" /&gt; </code>
    ///     </ul>
    ///     <para>XOM attributes can be created as follows:</para>
    ///     <ul>
    ///         <code> Attribute attribute = new Attribute("opensearch:relevance", "http://opensearch.namespace/", "95");<br />
    /// Attribute attribute = new Attribute("opensearch:confidence", "http://opensearch.namespace/", "82"); </code>
    ///     </ul>
    ///     <para>
    ///         The DDMS documentation does not provide sample HTML/Text output for extensible attributes, so the following
    ///         approach is used. In general, the HTML/Text output of extensible attributes will be prefixed with the name of
    ///         the
    ///         element being marked. For example:
    ///     </para>
    ///     <ul>
    ///         <code> keyword opensearch:relevance: 95<br /> keyword opensearch:confidence: 82<br />
    /// &lt;meta name="subjectCoverage.Subject.keyword.opensearch.relevance" content="95" /&gt;<br />
    /// &lt;meta name="subjectCoverage.Subject.keyword.opensearch.confidence" content="82" /&gt;<br />
    /// </code>
    ///     </ul>
    ///     <para>
    ///         Details about the XOM Attribute class can be found at:
    ///         <i>http://www.xom.nu/apidocs/index.html?nu/xom/Attribute.html</i>
    ///     </para>
    
    ///     @since 1.1.0
    /// </summary>
    public sealed class ExtensibleAttributes : AbstractAttributeGroup
    {
        private readonly List<XmlQualifiedName> RESERVED_RESOURCE_NAMES = new List<XmlQualifiedName>();
        private readonly List<Attribute> _attributes;

        /// <summary>
        ///     Base constructor
        ///     <para>
        ///         Will only load attributes from a different namespace than DDMS (##other)
        ///         and will also skip any Resource attributes that are reserved.
        ///     </para>
        /// </summary>
        /// <param name="element"> the XOM element which is decorated with these attributes. </param>
        public ExtensibleAttributes(Element element) : base(element.Name.NamespaceName)
        {
            BuildReservedNames(element.Name.NamespaceName);

            _attributes = new List<Attribute>();
            foreach (var attribute in element.Attributes())
            {
                // Skip ddms: attributes.
                if (element.Name.NamespaceName.Equals(attribute.Name.NamespaceName))
                {
                    continue;
                }
                // Skip reserved ISM attributes on Resource and Category
                DDMSVersion version = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
                if (Resource.GetName(version).Equals(element.Name.LocalName) ||
                    Category.GetName(version).Equals(element.Name.LocalName) ||
                    Keyword.GetName(version).Equals(element.Name.LocalName))
                {
                    var testName = new XmlQualifiedName(attribute.Name.NamespaceName, attribute.Name.LocalName);
                    if (RESERVED_RESOURCE_NAMES.Contains(testName))
                    {
                        continue;
                    }
                }
                _attributes.Add(attribute);
            }
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data. Because the parent is not known at this time, will accept
        ///     all attributes. The method, addTo() will confirm that the names do not clash with existing or reserved
        ///     names on the element.
        /// </summary>
        /// <param name="attributes"> a list of extensible attributes </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ExtensibleAttributes(List<Attribute> attributes) : base(DDMSVersion.GetCurrentVersion().Namespace)
        {
            if (attributes == null)
            {
                attributes = new List<Attribute>();
            }
            _attributes = new List<Attribute>(attributes);
            Validate();
        }

        /// <summary>
        ///     Checks if any attributes have been set.
        /// </summary>
        /// <returns> true if no attributes have values, false otherwise </returns>
        public bool Empty
        {
            get { return (Attributes.Count == 0); }
        }

        /// <summary>
        ///     Accessor for the attributes. Returns a copy.
        /// </summary>
        public List<Attribute> Attributes
        {
            get
            {
                var attributes = new List<Attribute>();
                foreach (var attribute in _attributes)
                {
                    attributes.Add(attribute);
                }
                return attributes;
            }
        }

        /// <summary>
        ///     Returns a non-null instance of extensible attributes. If the instance passed in is not null, it will be returned.
        /// </summary>
        /// <param name="extensibleAttributes"> the attributes to return by default </param>
        /// <returns> a non-null attributes instance </returns>
        /// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>
        public static ExtensibleAttributes GetNonNullInstance(ExtensibleAttributes extensibleAttributes)
        {
            return (extensibleAttributes == null
                ? new ExtensibleAttributes((List<Attribute>) null)
                : extensibleAttributes);
        }

        /// <summary>
        ///     Compiles lists of attribute names which should be ignored when creating extensible attributes. In most cases,
        ///     this is easy to determine, because namespace="##other" forces all extensible attributes to be in a non-DDMS
        ///     namespace, so the Resource is the only element that might encounter collisions (it has ISM attributes that
        ///     should be ignored).
        /// </summary>
        /// <param name="parentNamespace"> the namespace of the element which owns these attributes </param>
        private void BuildReservedNames(string parentNamespace)
        {
            DDMSVersion version = DDMSVersion.GetVersionForNamespace(parentNamespace);
            RESERVED_RESOURCE_NAMES.Clear();
            string ismPrefix = PropertyReader.GetPrefix("ism");
            string ntkPrefix = PropertyReader.GetPrefix("ntk");
            foreach (var reservedName in Resource.NON_EXTENSIBLE_NAMES)
            {
                RESERVED_RESOURCE_NAMES.Add(new XmlQualifiedName(version.IsmNamespace, reservedName));
            }
            foreach (var reservedName in SecurityAttributes.NON_EXTENSIBLE_NAMES)
            {
                RESERVED_RESOURCE_NAMES.Add(new XmlQualifiedName(version.IsmNamespace, reservedName));
            }
            if (version.IsAtLeast("4.0.1"))
            {
                foreach (var reservedName in NoticeAttributes.NON_EXTENSIBLE_NAMES)
                {
                    RESERVED_RESOURCE_NAMES.Add(new XmlQualifiedName(version.IsmNamespace, reservedName));
                }
                RESERVED_RESOURCE_NAMES.Add(new XmlQualifiedName(version.NtkNamespace, Resource.DES_VERSION_NAME));
            }
        }

        /// <summary>
        ///     Convenience method to add these attributes onto an existing XOM Element
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        /// <exception cref="InvalidDDMSException"> if the attribute already exists </exception>
        public void AddTo(Element element)
        {
            foreach (var attribute in Attributes)
            {
                if (element.Attribute(XName.Get(attribute.Name.LocalName, attribute.Name.NamespaceName)).Value != null)
                {
                    throw new InvalidDDMSException("The extensible attribute with the name, " + attribute.Name +
                                                   " conflicts with a pre-existing attribute on the element.");
                }
                element.Add(attribute);
            }
        }

        /// <summary>
        ///     Currently, no further validation is performed.
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            base.Validate();
        }

        /// <see cref="AbstractAttributeGroup#getOutput(boolean, String)"></see>
        public override string GetOutput(bool isHTML, string prefix)
        {
            string localPrefix = Util.Util.GetNonNullString(prefix);
            var text = new StringBuilder();
            foreach (var attribute in Attributes)
            {
                text.Append(AbstractBaseComponent.BuildOutput(isHTML, localPrefix + "." + attribute.Name.LocalName,
                    attribute.Value));
            }
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!(obj is ExtensibleAttributes))
            {
                return (false);
            }
            var test = (ExtensibleAttributes) obj;
            // XOM Attribute has no logical equality. Must compare by hand.
            if (Attributes.Count != test.Attributes.Count)
            {
                return (false);
            }
            for (int i = 0; i < Attributes.Count; i++)
            {
                Attribute attr1 = Attributes[i];
                Attribute attr2 = test.Attributes[i];
                if (!attr1.Name.LocalName.Equals(attr2.Name.LocalName) ||
                    !attr1.Name.NamespaceName.Equals(attr1.Name.NamespaceName))
                {
                    return (false);
                }
            }
            return (true);
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = 0;
            // XOM Attribute has no logical equality. Must calculate by hand.		
            foreach (var attribute in Attributes)
            {
                result = 7*result + attribute.Name.LocalName.GetHashCode();
                result = 7*result + attribute.Name.NamespaceName.GetHashCode();
            }
            return (result);
        }

        /// <summary>
        ///     Builder for a XOM attribute.
        ///     <para>
        ///         This builder is implemented because the XOM attribute does not have a no-arg constructor which can be hooked
        ///         into a LazyList. Because the Builder returns a XOM attribute instead of an IDDMSComponent, it does not
        ///         officially
        ///         implement the IBuilder interface.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.9.0"></see>
        [Serializable]
        public class AttributeBuilder
        {
            internal const long SerialVersionUID = -5102193614065692204L;
            internal string _name;
            internal XmlNodeType _type;
            internal string _uri;
            internal string _value;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public AttributeBuilder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public AttributeBuilder(Attribute attribute)
            {
                Name = attribute.Name.LocalName;
                Uri = attribute.Name.NamespaceName;
                Value = attribute.Value;
                Type = attribute.NodeType;
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Uri) && String.IsNullOrEmpty(Value) &&
                            Type == null);
                }
            }

            /// <summary>
            ///     Builder accessor for the name
            /// </summary>
            public virtual string Name
            {
                get { return _name; }
                set { _name = value; }
            }


            /// <summary>
            ///     Builder accessor for the uri
            /// </summary>
            public virtual string Uri
            {
                get { return _uri; }
                set { _uri = value; }
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
            ///     Builder accessor for the type
            /// </summary>
            public virtual XmlNodeType Type
            {
                get { return _type; }
                set { _type = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual Attribute Commit()
            {
                return (Empty ? null : new Attribute(XName.Get(Name, Uri), Value));
            }
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
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder
        {
            internal const long SerialVersionUID = 1257270526054778197L;
            internal List<AttributeBuilder> _attributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(ExtensibleAttributes attributes)
            {
                foreach (var attribute in attributes.Attributes)
                {
                    Attributes.Add(new AttributeBuilder(attribute));
                }
            }

            /// <summary>
            ///     Checks if any values have been provided for this Builder.
            /// </summary>
            /// <returns> true if every field is empty </returns>
            public virtual bool Empty
            {
                get { return (Attributes.Count == 0); }
            }

            /// <summary>
            ///     Builder accessor for the attributes
            /// </summary>
            public virtual List<AttributeBuilder> Attributes
            {
                get
                {
                    if (_attributes == null)
                    {
                        _attributes = new List<AttributeBuilder>();
                    }
                    return _attributes;
                }
            }

            /// <summary>
            ///     Finalizes the data gathered for this builder instance. Will always return an empty instance instead of
            ///     a null one.
            /// </summary>
            /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
            public virtual ExtensibleAttributes Commit()
            {
                var attributes = new List<Attribute>();
                foreach (var builder in Attributes)
                {
                    Attribute attr = builder.Commit();
                    if (attr != null)
                    {
                        attributes.Add(attr);
                    }
                }
                return (new ExtensibleAttributes(attributes));
            }
        }
    }
}