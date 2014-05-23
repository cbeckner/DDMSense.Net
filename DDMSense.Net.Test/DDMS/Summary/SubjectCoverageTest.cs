using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace DDMSense.Test.DDMS.Summary
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.Summary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:subjectCoverage elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class SubjectCoverageTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public SubjectCoverageTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public SubjectCoverageTest()
            : base("subjectCoverage.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="order"> an order value for a nonstate actor inside </param>
        public static SubjectCoverage GetFixture(int order)
        {
            try
            {
                List<NonStateActor> actors = new List<NonStateActor>();
                actors.Add(NonStateActorTest.GetFixture(order));
                return (new SubjectCoverage(KeywordTest.FixtureList, null, null, DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? actors : null, null));
            }
            catch (InvalidDDMSException e)
            {
                Assert.Fail("Could not create fixture: " + e.Message);
            }
            return (null);
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static SubjectCoverage Fixture
        {
            get
            {
                try
                {
                    List<Keyword> keywords = new List<Keyword>();
                    keywords.Add(new Keyword("DDMSence", null));
                    return (new SubjectCoverage(keywords, null, null, null, null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private SubjectCoverage GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SubjectCoverage component = null;
            try
            {
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    SecurityAttributesTest.Fixture.AddTo(element);
                }
                component = new SubjectCoverage(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="keywords"> list of keywords </param>
        /// <param name="categories"> list of categories </param>
        /// <param name="metrics"> list of production metrics </param>
        /// <param name="actors"> list of non-state actors </param>
        /// <returns> a valid object </returns>
        private SubjectCoverage GetInstance(string message, List<Keyword> keywords, List<Category> categories, List<ProductionMetric> metrics, List<NonStateActor> actors)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SubjectCoverage component = null;
            try
            {
                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.IsAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
                component = new SubjectCoverage(keywords, categories, metrics, actors, attr);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to manage the deprecated Subject wrapper element
        /// </summary>
        /// <param name="innerElement"> the element containing the guts of this component </param>
        /// <returns> the element itself in DDMS 4.0.1 or later, or the element wrapped in another element </returns>
        private XElement WrapInnerElement(XElement innerElement)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string name = SubjectCoverage.GetName(version);
            if (version.IsAtLeast("4.0.1"))
            {
                innerElement.Name = innerElement.Name.Namespace + name;
                return (innerElement);
            }
            XElement element = Util.BuildDDMSElement(name, null);
            element.Add(innerElement);
            return (element);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string prefix = version.IsAtLeast("4.0.1") ? "subjectCoverage." : "subjectCoverage.Subject.";
            StringBuilder text = new StringBuilder();
            foreach (Keyword keyword in KeywordTest.FixtureList)
            {
                text.Append(keyword.GetOutput(isHTML, prefix, ""));
            }
            foreach (Category category in CategoryTest.FixtureList)
            {
                text.Append(category.GetOutput(isHTML, prefix, ""));
            }

            if (version.IsAtLeast("4.0.1"))
            {
                foreach (ProductionMetric metric in ProductionMetricTest.FixtureList)
                {
                    text.Append(metric.GetOutput(isHTML, prefix, ""));
                }
                foreach (NonStateActor actor in NonStateActorTest.FixtureList)
                {
                    text.Append(actor.GetOutput(isHTML, prefix, ""));
                }
            }
            if (version.IsAtLeast("3.0"))
            {
                text.Append(SecurityAttributesTest.Fixture.GetOutput(isHTML, prefix));
            }
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:subjectCoverage ").Append(XmlnsDDMS);
            if (version.IsAtLeast("3.0"))
            {
                xml.Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
            }
            xml.Append(">\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
                xml.Append("\t<ddms:keyword ddms:value=\"Uri\" />\n");
                xml.Append("\t<ddms:category ddms:qualifier=\"urn:buri:ddmsence:categories\" ddms:code=\"DDMS\" ").Append("ddms:label=\"DDMS\" />\n");
                xml.Append("\t<ddms:productionMetric ddms:subject=\"FOOD\" ddms:coverage=\"AFG\" ISM:classification=\"U\" ISM:ownerProducer=\"USA\" />\n");
                xml.Append("\t<ddms:nonStateActor ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ddms:order=\"1\"");
                if (version.IsAtLeast("4.1"))
                {
                    xml.Append(" ddms:qualifier=\"urn:sample\"");
                }
                xml.Append(">Laotian Monks</ddms:nonStateActor>\n");
            }
            else
            {
                xml.Append("\t<ddms:Subject>\n");
                xml.Append("\t\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
                xml.Append("\t\t<ddms:keyword ddms:value=\"Uri\" />\n");
                xml.Append("\t\t<ddms:category ddms:qualifier=\"urn:buri:ddmsence:categories\" ddms:code=\"DDMS\" ").Append("ddms:label=\"DDMS\" />\n");
                xml.Append("\t</ddms:Subject>\n");
            }
            xml.Append("</ddms:subjectCoverage>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, SubjectCoverage.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement subjectElement = Util.BuildDDMSElement("Subject", null);
                subjectElement.Add(KeywordTest.FixtureList[0].ElementCopy);
                GetInstance(SUCCESS, WrapInnerElement(subjectElement));
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);

                // No optional fields
                GetInstance(SUCCESS, KeywordTest.FixtureList, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No keywords or categories
                XElement subjectElement = Util.BuildDDMSElement("Subject", null);
                GetInstance("At least 1 keyword or category must exist.", WrapInnerElement(subjectElement));
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No keywords or categories
                GetInstance("At least 1 keyword or category must exist.", null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                string text, locator;
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // 4.1 ddms:qualifier element used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.AreEqual(1, component.ValidationWarnings.Count());
                    text = "The ddms:qualifier attribute in this DDMS component";
                    locator = "ddms:subjectCoverage/ddms:nonStateActor";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                // No warnings
                else
                {
                    Assert.AreEqual(0, component.ValidationWarnings.Count());
                }

                // Identical keywords
                XElement subjectElement = Util.BuildDDMSElement("Subject", null);
                subjectElement.Add(KeywordTest.FixtureList[0].ElementCopy);
                subjectElement.Add(KeywordTest.FixtureList[0].ElementCopy);
                component = GetInstance(SUCCESS, WrapInnerElement(subjectElement));
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                text = "1 or more keywords have the same value.";
                locator = version.IsAtLeast("4.0.1") ? "ddms:subjectCoverage" : "ddms:subjectCoverage/ddms:Subject";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Identical categories
                subjectElement = Util.BuildDDMSElement("Subject", null);
                subjectElement.Add(CategoryTest.FixtureList[0].ElementCopy);
                subjectElement.Add(CategoryTest.FixtureList[0].ElementCopy);
                component = GetInstance(SUCCESS, WrapInnerElement(subjectElement));
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                text = "1 or more categories have the same value.";
                locator = version.IsAtLeast("4.0.1") ? "ddms:subjectCoverage" : "ddms:subjectCoverage/ddms:Subject";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Identical productionMetrics
                if (version.IsAtLeast("4.0.1"))
                {
                    subjectElement = Util.BuildDDMSElement("Subject", null);
                    subjectElement.Add(CategoryTest.FixtureList[0].ElementCopy);
                    subjectElement.Add(ProductionMetricTest.FixtureList[0].ElementCopy);
                    subjectElement.Add(ProductionMetricTest.FixtureList[0].ElementCopy);
                    component = GetInstance(SUCCESS, WrapInnerElement(subjectElement));
                    Assert.AreEqual(1, component.ValidationWarnings.Count());
                    text = "1 or more productionMetrics have the same value.";
                    locator = "ddms:subjectCoverage";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SubjectCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                SubjectCoverage dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SubjectCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                SubjectCoverage dataComponent = GetInstance(SUCCESS, null, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, null, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                if (version.IsAtLeast("4.0.1"))
                {
                    dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, null, NonStateActorTest.FixtureList);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));

                    dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, null);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_CategoryReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<Category> categories = CategoryTest.FixtureList;
                GetInstance(SUCCESS, null, categories, null, null);
                GetInstance(SUCCESS, null, categories, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_KeywordReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<Keyword> keywords = KeywordTest.FixtureList;
                GetInstance(SUCCESS, keywords, null, null, null);
                GetInstance(SUCCESS, keywords, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_MetricReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<ProductionMetric> metrics = ProductionMetricTest.FixtureList;
                GetInstance(SUCCESS, KeywordTest.FixtureList, null, metrics, null);
                GetInstance(SUCCESS, KeywordTest.FixtureList, null, metrics, null);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_ActorReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<NonStateActor> actors = NonStateActorTest.FixtureList;
                GetInstance(SUCCESS, KeywordTest.FixtureList, null, null, actors);
                GetInstance(SUCCESS, KeywordTest.FixtureList, null, null, actors);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_SecurityAttributes()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes attr = (!version.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                SubjectCoverage component = new SubjectCoverage(KeywordTest.FixtureList, CategoryTest.FixtureList, null, null, attr);
                if (!version.IsAtLeast("3.0"))
                {
                    Assert.IsTrue(component.SecurityAttributes.Empty);
                }
                else
                {
                    Assert.AreEqual(attr, component.SecurityAttributes);
                }
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new SubjectCoverage(KeywordTest.FixtureList, CategoryTest.FixtureList, null, null, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_WrongVersions()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            List<Keyword> keywords = KeywordTest.FixtureList;
            DDMSVersion.SetCurrentVersion("3.0");
            try
            {
                new SubjectCoverage(keywords, null, null, null, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed different versions.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "At least 1 keyword or category must exist.");
            }

            DDMSVersion.SetCurrentVersion("2.0");
            List<Category> categories = CategoryTest.FixtureList;
            DDMSVersion.SetCurrentVersion("3.0");
            try
            {
                new SubjectCoverage(null, categories, null, null, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed different versions.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "At least 1 keyword or category must exist.");
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                SubjectCoverage.Builder builder = new SubjectCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubjectCoverage.Builder builder = new SubjectCoverage.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_SubjectCoverage_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                SubjectCoverage.Builder builder = new SubjectCoverage.Builder();
                builder.Categories.Add(new Category.Builder());
                builder.Categories[0].Code = "TEST";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "label attribute is required.");
                }
                builder.Categories[0].Qualifier = "qualifier";
                builder.Categories[0].Label = "label";
                builder.Commit();

                // Skip empty Keywords
                builder = new SubjectCoverage.Builder();
                Keyword.Builder emptyBuilder = new Keyword.Builder();
                Keyword.Builder fullBuilder = new Keyword.Builder();
                fullBuilder.Value = "keyword";
                builder.Keywords.Add(emptyBuilder);
                builder.Keywords.Add(fullBuilder);
                Assert.AreEqual(1, ((SubjectCoverage)builder.Commit()).Keywords.Count());

                // Skip empty Categories
                builder = new SubjectCoverage.Builder();
                Category.Builder emptyCategoryBuilder = new Category.Builder();
                Category.Builder fullCategoryBuilder = new Category.Builder();
                fullCategoryBuilder.Label = "label";
                builder.Categories.Add(emptyCategoryBuilder);
                builder.Categories.Add(fullCategoryBuilder);
                Assert.AreEqual(1, ((SubjectCoverage)builder.Commit()).Categories.Count());

                if (version.IsAtLeast("4.0.1"))
                {
                    // Skip empty metrics
                    builder = new SubjectCoverage.Builder();
                    ProductionMetric.Builder emptyProductionMetricBuilder = new ProductionMetric.Builder();
                    ProductionMetric.Builder fullProductionMetricBuilder = new ProductionMetric.Builder();
                    fullProductionMetricBuilder.Subject = "FOOD";
                    fullProductionMetricBuilder.Coverage = "AFG";
                    builder.Keywords.Add(new Keyword.Builder());
                    builder.Keywords[0].Value = "test";
                    builder.ProductionMetrics.Add(emptyProductionMetricBuilder);
                    builder.ProductionMetrics.Add(fullProductionMetricBuilder);
                    Assert.AreEqual(1, ((SubjectCoverage)builder.Commit()).ProductionMetrics.Count());

                    // Skip empty actors
                    builder = new SubjectCoverage.Builder();
                    NonStateActor.Builder emptyNonStateActorBuilder = new NonStateActor.Builder();
                    NonStateActor.Builder fullNonStateActorBuilder = new NonStateActor.Builder();
                    fullNonStateActorBuilder.Value = "Laotian Monks";
                    builder.Keywords.Add(new Keyword.Builder());
                    builder.Keywords[0].Value = "test";
                    builder.NonStateActors.Add(emptyNonStateActorBuilder);
                    builder.NonStateActors.Add(fullNonStateActorBuilder);
                    Assert.AreEqual(1, ((SubjectCoverage)builder.Commit()).NonStateActors.Count());
                }
            }
        }
    }
}