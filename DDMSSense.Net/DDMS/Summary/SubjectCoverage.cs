#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Extensions;
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

namespace DDMSense.DDMS.Summary
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:subjectCoverage.
    ///     <para>
    ///         Before DDMS 4.0.1, a subjectCoverage element contains a locally defined Subject construct. This construct is a
    ///         container for the keywords and categories of a resource. It exists only inside of a ddms:subjectCoverage
    ///         parent,
    ///         so it is not implemented as a Java object. Starting in DDMS 4.0.1, the Subject wrapper has been removed.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>Duplicate keywords or categories can be used.</li>
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
    ///                 <u>ddms:category</u>: a category (0-many optional), implemented as a <see cref="Category" /><br />
    ///                 <u>ddms:keyword</u>: a keyword (0-many optional), implemented as a <see cref="Keyword" /><br />
    ///                 <u>ddms:productionMetric</u>: a categorization scheme whose values and use are defined by DDNI-A.
    ///                 (0-many optional,
    ///                 starting in DDMS 4.0.1), implemented as a <see cref="ProductionMetric" /><br />
    ///                 <u>ddms:nonStateActor</u>: a non-state actor within the scope of this coverage (0-many optional,
    ///                 starting in DDMS
    ///                 4.0.1), implemented as a <see cref="NonStateActor" /><br />
    ///                 <para>At least 1 of category or keyword must be used.</para>
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
    ///                 : The classification and ownerProducer attributes are optional. (starting in DDMS 3.0)
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class SubjectCoverage : AbstractBaseComponent
    {
        private const string SUBJECT_NAME = "Subject";
        private readonly List<Category> _categories;
        private readonly List<Keyword> _keywords;
        private readonly List<NonStateActor> _nonStateActors;
        private readonly List<ProductionMetric> _productionMetrics;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public SubjectCoverage(Element element)
        {
            try
            {
                Util.Util.RequireDDMSValue("subjectCoverage element", element);
                SetElement(element, false);
                Element subjectElement = SubjectElement;
                _keywords = new List<Keyword>();
                _categories = new List<Category>();
                _productionMetrics = new List<ProductionMetric>();
                _nonStateActors = new List<NonStateActor>();
                if (subjectElement != null)
                {
                    IEnumerable<Element> keywords =
                        subjectElement.Elements(XName.Get(Keyword.GetName(DDMSVersion), Namespace));
                    keywords.ToList().ForEach(k => _keywords.Add(new Keyword(k)));

                    IEnumerable<Element> categories =
                        subjectElement.Elements(XName.Get(Category.GetName(DDMSVersion), Namespace));
                    categories.ToList().ForEach(c => _categories.Add(new Category(c)));

                    IEnumerable<Element> metrics =
                        subjectElement.Elements(XName.Get(ProductionMetric.GetName(DDMSVersion), Namespace));
                    metrics.ToList().ForEach(m => _productionMetrics.Add(new ProductionMetric(m)));

                    IEnumerable<Element> actors =
                        subjectElement.Elements(XName.Get(NonStateActor.GetName(DDMSVersion), Namespace));
                    actors.ToList().ForEach(a => _nonStateActors.Add(new NonStateActor(a)));
                }
                _securityAttributes = new SecurityAttributes(element);
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
        /// <param name="keywords"> list of keywords </param>
        /// <param name="categories"> list of categories </param>
        /// <param name="productionMetrics"> list of metrics </param>
        /// <param name="nonStateActors"> list of actors </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public SubjectCoverage(List<Keyword> keywords, List<Category> categories,
            List<ProductionMetric> productionMetrics, List<NonStateActor> nonStateActors,
            SecurityAttributes securityAttributes)
        {
            try
            {
                if (keywords == null)
                {
                    keywords = new List<Keyword>();
                }
                if (categories == null)
                {
                    categories = new List<Category>();
                }
                if (productionMetrics == null)
                {
                    productionMetrics = new List<ProductionMetric>();
                }
                if (nonStateActors == null)
                {
                    nonStateActors = new List<NonStateActor>();
                }
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);

                Element subjectElement = DDMSVersion.CurrentVersion.IsAtLeast("4.0.1")
                    ? element
                    : Util.Util.BuildDDMSElement(SUBJECT_NAME, null);
                foreach (var keyword in keywords)
                {
                    subjectElement.Add(keyword.ElementCopy);
                }
                foreach (var category in categories)
                {
                    subjectElement.Add(category.ElementCopy);
                }
                foreach (var metric in productionMetrics)
                {
                    subjectElement.Add(metric.ElementCopy);
                }
                foreach (var actor in nonStateActors)
                {
                    subjectElement.Add(actor.ElementCopy);
                }

                if (!DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
                {
                    element.Add(subjectElement);
                }

                _keywords = keywords;
                _categories = categories;
                _productionMetrics = productionMetrics;
                _nonStateActors = nonStateActors;
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getLocatorSuffix()"></see>
        protected internal override string LocatorSuffix
        {
            get
            {
                return (DDMSVersion.IsAtLeast("4.0.1")
                    ? ""
                    : ValidationMessage.ElementPrefix + Element.GetPrefix() + ":" + SUBJECT_NAME);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.AddRange(Keywords);
                list.AddRange(Categories);
                list.AddRange(ProductionMetrics);
                list.AddRange(NonStateActors);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the element which contains the keywords and categories. Before DDMS 4.0.1, this is a wrapper element
        ///     called ddms:Subject. Starting in DDMS 4.0.1, it is the ddms:subjectCoverage element itself.
        /// </summary>
        private Element SubjectElement
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? Element : GetChild(SUBJECT_NAME)); }
        }

        /// <summary>
        ///     Accessor for the keywords (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public List<Keyword> Keywords
        {
            get { return _keywords; }
        }

        /// <summary>
        ///     Accessor for the categories (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public List<Category> Categories
        {
            get { return _categories; }
        }

        /// <summary>
        ///     Accessor for the production metrics (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public List<ProductionMetric> ProductionMetrics
        {
            get { return _productionMetrics; }
        }

        /// <summary>
        ///     Accessor for the non-state actors (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public List<NonStateActor> NonStateActors
        {
            get { return _nonStateActors; }
        }

        /// <summary>
        ///     Accessor for the Security Attributes.  Will always be non-null, even if it has no values set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
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
        ///                 <li>At least 1 of "Keyword" or "Category" must exist.</li>
        ///                 <li>The SecurityAttributes do not exist until DDMS 3.0 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Element subjectElement = SubjectElement;
            Util.Util.RequireDDMSValue("Subject element", subjectElement);
            string @namespace = subjectElement.Name.NamespaceName;
            int count = subjectElement.Elements(XName.Get(Keyword.GetName(DDMSVersion), @namespace)).Count() +
                        subjectElement.Elements(XName.Get(Category.GetName(DDMSVersion), @namespace)).Count();
            if (count < 1)
            {
                throw new InvalidDDMSException("At least 1 keyword or category must exist.");
            }
            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("3.0") && !SecurityAttributes.Empty)
            {
                throw new InvalidDDMSException(
                    "Security attributes cannot be applied to this component until DDMS 3.0 or later.");
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
        ///                 <li>1 or more keywords have the same value.</li>
        ///                 <li>1 or more categories have the same value.</li>
        ///                 <li>1 or more productionMetrics have the same value.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            var uniqueKeywords = new List<Keyword>(Keywords);
            if (uniqueKeywords.Count != Keywords.Count)
            {
                AddWarning("1 or more keywords have the same value.");
            }
            var uniqueCategories = new List<Category>(Categories);
            if (uniqueCategories.Count != Categories.Count)
            {
                AddWarning("1 or more categories have the same value.");
            }
            var uniqueMetrics = new List<ProductionMetric>(ProductionMetrics);
            if (uniqueMetrics.Count != ProductionMetrics.Count)
            {
                AddWarning("1 or more productionMetrics have the same value.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            if (!DDMSVersion.IsAtLeast("4.0.1"))
            {
                localPrefix += SUBJECT_NAME + ".";
            }
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Keywords));
            text.Append(BuildOutput(isHtml, localPrefix, Categories));
            text.Append(BuildOutput(isHtml, localPrefix, ProductionMetrics));
            text.Append(BuildOutput(isHtml, localPrefix, NonStateActors));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is SubjectCoverage))
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
            return ("subjectCoverage");
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
            
            internal List<Category.Builder> _categories;
            internal List<Keyword.Builder> _keywords;
            internal List<NonStateActor.Builder> _nonStateActors;
            internal List<ProductionMetric.Builder> _productionMetrics;
            internal SecurityAttributes.Builder _securityAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(SubjectCoverage coverage)
            {
                foreach (var keyword in coverage.Keywords)
                {
                    Keywords.Add(new Keyword.Builder(keyword));
                }
                foreach (var category in coverage.Categories)
                {
                    Categories.Add(new Category.Builder(category));
                }
                foreach (var metric in coverage.ProductionMetrics)
                {
                    ProductionMetrics.Add(new ProductionMetric.Builder(metric));
                }
                foreach (var actor in coverage.NonStateActors)
                {
                    NonStateActors.Add(new NonStateActor.Builder(actor));
                }
                SecurityAttributes = new SecurityAttributes.Builder(coverage.SecurityAttributes);
            }

            /// <summary>
            ///     Convenience method to get every child Builder in this Builder.
            /// </summary>
            /// <returns> a list of IBuilders </returns>
            internal virtual List<IBuilder> ChildBuilders
            {
                get
                {
                    var list = new List<IBuilder>();
                    list.AddRange(Keywords);
                    list.AddRange(Categories);
                    list.AddRange(ProductionMetrics);
                    list.AddRange(NonStateActors);
                    return (list);
                }
            }

            /// <summary>
            ///     Builder accessor for the keywords in this coverage.
            /// </summary>
            public virtual List<Keyword.Builder> Keywords
            {
                get
                {
                    if (_keywords == null)
                    {
                        _keywords = new List<Keyword.Builder>();
                    }
                    return _keywords;
                }
                set
                {
                    _keywords = value;
                }
            }

            /// <summary>
            ///     Builder accessor for the categories in this coverage.
            /// </summary>
            public virtual List<Category.Builder> Categories
            {
                get
                {
                    if (_categories == null)
                    {
                        _categories = new List<Category.Builder>();
                    }
                    return _categories;
                }
                set { _categories = value; }
            }

            /// <summary>
            ///     Builder accessor for the production metrics in this coverage.
            /// </summary>
            public virtual List<ProductionMetric.Builder> ProductionMetrics
            {
                get
                {
                    if (_productionMetrics == null)
                    {
                        _productionMetrics = new List<ProductionMetric.Builder>();
                    }
                    return _productionMetrics;
                }
                set
                {
                    _productionMetrics = value;
                }
            }

            /// <summary>
            ///     Builder accessor for the non-state actors in this coverage.
            /// </summary>
            public virtual List<NonStateActor.Builder> NonStateActors
            {
                get
                {
                    if (_nonStateActors == null)
                    {
                        _nonStateActors = new List<NonStateActor.Builder>();
                    }
                    return _nonStateActors;
                }
            }

            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes
            {
                get
                {
                    if (_securityAttributes == null)
                    {
                        _securityAttributes = new SecurityAttributes.Builder();
                    }
                    return _securityAttributes;
                }
                set { _securityAttributes = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var categories = new List<Category>();
                foreach (var builder in Categories)
                {
                    var category = (Category) builder.Commit();
                    if (category != null)
                    {
                        categories.Add(category);
                    }
                }
                var keywords = new List<Keyword>();
                foreach (var builder in Keywords)
                {
                    var keyword = (Keyword) builder.Commit();
                    if (keyword != null)
                    {
                        keywords.Add(keyword);
                    }
                }
                var metrics = new List<ProductionMetric>();
                foreach (var builder in ProductionMetrics)
                {
                    var metric = (ProductionMetric) builder.Commit();
                    if (metric != null)
                    {
                        metrics.Add(metric);
                    }
                }
                var actors = new List<NonStateActor>();
                foreach (var builder in NonStateActors)
                {
                    var actor = (NonStateActor) builder.Commit();
                    if (actor != null)
                    {
                        actors.Add(actor);
                    }
                }
                return (new SubjectCoverage(keywords, categories, metrics, actors, SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (var builder in ChildBuilders)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && SecurityAttributes.Empty);
                }
            }
        }
    }
}