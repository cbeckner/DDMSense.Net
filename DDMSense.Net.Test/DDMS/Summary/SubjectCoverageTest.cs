using System.Collections.Generic;
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
namespace DDMSense.Test.DDMS.Summary {


	
	using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
	using DDMSVersion = DDMSense.Util.DDMSVersion;
	using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;

	/// <summary>
	/// <para> Tests related to ddms:subjectCoverage elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class SubjectCoverageTest : AbstractBaseTestCase {

		/// <summary>
		/// Constructor
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public SubjectCoverageTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public SubjectCoverageTest() : base("subjectCoverage.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		/// <param name="order"> an order value for a nonstate actor inside </param>
		public static SubjectCoverage GetFixture(int order) {
			try {
				IList<NonStateActor> actors = new List<NonStateActor>();
				actors.Add(NonStateActorTest.GetFixture(order));
				return (new SubjectCoverage(KeywordTest.FixtureList, null, null, DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? actors : null, null));
			} catch (InvalidDDMSException e) {
				fail("Could not create fixture: " + e.Message);
			}
			return (null);
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static SubjectCoverage Fixture {
			get {
				try {
					IList<Keyword> keywords = new List<Keyword>();
					keywords.Add(new Keyword("DDMSence", null));
					return (new SubjectCoverage(keywords, null, null, null, null));
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Helper method to create an object which is expected to be valid.
		/// </summary>
		/// <param name="element"> the element to build from </param>
		/// <returns> a valid object </returns>
		private SubjectCoverage GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			SubjectCoverage component = null;
			try {
				if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
					SecurityAttributesTest.Fixture.addTo(element);
				}
				component = new SubjectCoverage(element);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
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
		private SubjectCoverage GetInstance(string message, IList<Keyword> keywords, IList<Category> categories, IList<ProductionMetric> metrics, IList<NonStateActor> actors) {
			bool expectFailure = !Util.isEmpty(message);
			SubjectCoverage component = null;
			try {
				SecurityAttributes attr = (!DDMSVersion.CurrentVersion.isAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
				component = new SubjectCoverage(keywords, categories, metrics, actors, attr);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
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
		private XElement WrapInnerElement(XElement innerElement) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			string name = SubjectCoverage.getName(version);
			if (version.isAtLeast("4.0.1")) {
				innerElement.LocalName = name;
				return (innerElement);
			}
			XElement element = Util.buildDDMSElement(name, null);
			element.appendChild(innerElement);
			return (element);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			string prefix = version.isAtLeast("4.0.1") ? "subjectCoverage." : "subjectCoverage.Subject.";
			StringBuilder text = new StringBuilder();
			foreach (Keyword keyword in KeywordTest.FixtureList) {
				text.Append(keyword.getOutput(isHTML, prefix, ""));
			}
			foreach (Category category in CategoryTest.FixtureList) {
				text.Append(category.getOutput(isHTML, prefix, ""));
			}

			if (version.isAtLeast("4.0.1")) {
				foreach (ProductionMetric metric in ProductionMetricTest.FixtureList) {
					text.Append(metric.getOutput(isHTML, prefix, ""));
				}
				foreach (NonStateActor actor in NonStateActorTest.FixtureList) {
					text.Append(actor.getOutput(isHTML, prefix, ""));
				}
			}
			if (version.isAtLeast("3.0")) {
				text.Append(SecurityAttributesTest.Fixture.getOutput(isHTML, prefix));
			}
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:subjectCoverage ").Append(XmlnsDDMS);
			if (version.isAtLeast("3.0")) {
				xml.Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
			}
			xml.Append(">\n");
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
				xml.Append("\t<ddms:keyword ddms:value=\"Uri\" />\n");
				xml.Append("\t<ddms:category ddms:qualifier=\"urn:buri:ddmsence:categories\" ddms:code=\"DDMS\" ").Append("ddms:label=\"DDMS\" />\n");
				xml.Append("\t<ddms:productionMetric ddms:subject=\"FOOD\" ddms:coverage=\"AFG\" ISM:classification=\"U\" ISM:ownerProducer=\"USA\" />\n");
				xml.Append("\t<ddms:nonStateActor ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ddms:order=\"1\"");
				if (version.isAtLeast("4.1")) {
					xml.Append(" ddms:qualifier=\"urn:sample\"");
				}
				xml.Append(">Laotian Monks</ddms:nonStateActor>\n");
			} else {
				xml.Append("\t<ddms:Subject>\n");
				xml.Append("\t\t<ddms:keyword ddms:value=\"DDMSence\" />\n");
				xml.Append("\t\t<ddms:keyword ddms:value=\"Uri\" />\n");
				xml.Append("\t\t<ddms:category ddms:qualifier=\"urn:buri:ddmsence:categories\" ddms:code=\"DDMS\" ").Append("ddms:label=\"DDMS\" />\n");
				xml.Append("\t</ddms:Subject>\n");
			}
			xml.Append("</ddms:subjectCoverage>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, SubjectCoverage.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				XElement subjectElement = Util.buildDDMSElement("Subject", null);
				subjectElement.appendChild(KeywordTest.FixtureList[0].XOMElementCopy);
				GetInstance(SUCCESS, WrapInnerElement(subjectElement));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);

				// No optional fields
				GetInstance(SUCCESS, KeywordTest.FixtureList, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No keywords or categories
				XElement subjectElement = Util.buildDDMSElement("Subject", null);
				GetInstance("At least 1 keyword or category must exist.", WrapInnerElement(subjectElement));
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No keywords or categories
				GetInstance("At least 1 keyword or category must exist.", null, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));

				// 4.1 ddms:qualifier element used
				if (version.isAtLeast("4.1")) {
					assertEquals(1, component.ValidationWarnings.size());
					string text = "The ddms:qualifier attribute in this DDMS component";
					string locator = "ddms:subjectCoverage/ddms:nonStateActor";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
				}
				// No warnings 
				else {
					assertEquals(0, component.ValidationWarnings.size());
				}

				// Identical keywords
				XElement subjectElement = Util.buildDDMSElement("Subject", null);
				subjectElement.appendChild(KeywordTest.FixtureList[0].XOMElementCopy);
				subjectElement.appendChild(KeywordTest.FixtureList[0].XOMElementCopy);
				component = GetInstance(SUCCESS, WrapInnerElement(subjectElement));
				assertEquals(1, component.ValidationWarnings.size());
				string text = "1 or more keywords have the same value.";
				string locator = version.isAtLeast("4.0.1") ? "ddms:subjectCoverage" : "ddms:subjectCoverage/ddms:Subject";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

				// Identical categories
				subjectElement = Util.buildDDMSElement("Subject", null);
				subjectElement.appendChild(CategoryTest.FixtureList[0].XOMElementCopy);
				subjectElement.appendChild(CategoryTest.FixtureList[0].XOMElementCopy);
				component = GetInstance(SUCCESS, WrapInnerElement(subjectElement));
				assertEquals(1, component.ValidationWarnings.size());
				text = "1 or more categories have the same value.";
				locator = version.isAtLeast("4.0.1") ? "ddms:subjectCoverage" : "ddms:subjectCoverage/ddms:Subject";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

				// Identical productionMetrics
				if (version.isAtLeast("4.0.1")) {
					subjectElement = Util.buildDDMSElement("Subject", null);
					subjectElement.appendChild(CategoryTest.FixtureList[0].XOMElementCopy);
					subjectElement.appendChild(ProductionMetricTest.FixtureList[0].XOMElementCopy);
					subjectElement.appendChild(ProductionMetricTest.FixtureList[0].XOMElementCopy);
					component = GetInstance(SUCCESS, WrapInnerElement(subjectElement));
					assertEquals(1, component.ValidationWarnings.size());
					text = "1 or more productionMetrics have the same value.";
					locator = "ddms:subjectCoverage";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SubjectCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				SubjectCoverage dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SubjectCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				SubjectCoverage dataComponent = GetInstance(SUCCESS, null, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, null, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
				assertFalse(elementComponent.Equals(dataComponent));

				if (version.isAtLeast("4.0.1")) {
					dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, null, NonStateActorTest.FixtureList);
					assertFalse(elementComponent.Equals(dataComponent));

					dataComponent = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, null);
					assertFalse(elementComponent.Equals(dataComponent));
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, KeywordTest.FixtureList, CategoryTest.FixtureList, ProductionMetricTest.FixtureList, NonStateActorTest.FixtureList);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testCategoryReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestCategoryReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				IList<Category> categories = CategoryTest.FixtureList;
				GetInstance(SUCCESS, null, categories, null, null);
				GetInstance(SUCCESS, null, categories, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testKeywordReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestKeywordReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				IList<Keyword> keywords = KeywordTest.FixtureList;
				GetInstance(SUCCESS, keywords, null, null, null);
				GetInstance(SUCCESS, keywords, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testMetricReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestMetricReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				IList<ProductionMetric> metrics = ProductionMetricTest.FixtureList;
				GetInstance(SUCCESS, KeywordTest.FixtureList, null, metrics, null);
				GetInstance(SUCCESS, KeywordTest.FixtureList, null, metrics, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testActorReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestActorReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				IList<NonStateActor> actors = NonStateActorTest.FixtureList;
				GetInstance(SUCCESS, KeywordTest.FixtureList, null, null, actors);
				GetInstance(SUCCESS, KeywordTest.FixtureList, null, null, actors);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestSecurityAttributes() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SecurityAttributes attr = (!version.isAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
				SubjectCoverage component = new SubjectCoverage(KeywordTest.FixtureList, CategoryTest.FixtureList, null, null, attr);
				if (!version.isAtLeast("3.0")) {
					assertTrue(component.SecurityAttributes.Empty);
				} else {
					assertEquals(attr, component.SecurityAttributes);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWrongVersionSecurityAttributes() {
			DDMSVersion.CurrentVersion = "2.0";
			try {
				new SubjectCoverage(KeywordTest.FixtureList, CategoryTest.FixtureList, null, null, SecurityAttributesTest.Fixture);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Security attributes cannot be applied");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersions() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWrongVersions() {
			DDMSVersion.CurrentVersion = "2.0";
			IList<Keyword> keywords = KeywordTest.FixtureList;
			DDMSVersion.CurrentVersion = "3.0";
			try {
				new SubjectCoverage(keywords, null, null, null, SecurityAttributesTest.Fixture);
				fail("Allowed different versions.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "At least 1 keyword or category must exist.");
			}

			DDMSVersion.CurrentVersion = "2.0";
			IList<Category> categories = CategoryTest.FixtureList;
			DDMSVersion.CurrentVersion = "3.0";
			try {
				new SubjectCoverage(null, categories, null, null, SecurityAttributesTest.Fixture);
				fail("Allowed different versions.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "At least 1 keyword or category must exist.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SubjectCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
				SubjectCoverage.Builder builder = new SubjectCoverage.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SubjectCoverage.Builder builder = new SubjectCoverage.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.SecurityAttributes.Classification = "U";
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				SubjectCoverage.Builder builder = new SubjectCoverage.Builder();
				builder.Categories.get(0).Code = "TEST";
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "label attribute is required.");
				}
				builder.Categories.get(0).Qualifier = "qualifier";
				builder.Categories.get(0).Label = "label";
				builder.commit();

				// Skip empty Keywords
				builder = new SubjectCoverage.Builder();
				Keyword.Builder emptyBuilder = new Keyword.Builder();
				Keyword.Builder fullBuilder = new Keyword.Builder();
				fullBuilder.Value = "keyword";
				builder.Keywords.add(emptyBuilder);
				builder.Keywords.add(fullBuilder);
				assertEquals(1, builder.commit().Keywords.size());

				// Skip empty Categories
				builder = new SubjectCoverage.Builder();
				Category.Builder emptyCategoryBuilder = new Category.Builder();
				Category.Builder fullCategoryBuilder = new Category.Builder();
				fullCategoryBuilder.Label = "label";
				builder.Categories.add(emptyCategoryBuilder);
				builder.Categories.add(fullCategoryBuilder);
				assertEquals(1, builder.commit().Categories.size());

				if (version.isAtLeast("4.0.1")) {
					// Skip empty metrics
					builder = new SubjectCoverage.Builder();
					ProductionMetric.Builder emptyProductionMetricBuilder = new ProductionMetric.Builder();
					ProductionMetric.Builder fullProductionMetricBuilder = new ProductionMetric.Builder();
					fullProductionMetricBuilder.Subject = "FOOD";
					fullProductionMetricBuilder.Coverage = "AFG";
					builder.Keywords.get(0).Value = "test";
					builder.ProductionMetrics.add(emptyProductionMetricBuilder);
					builder.ProductionMetrics.add(fullProductionMetricBuilder);
					assertEquals(1, builder.commit().ProductionMetrics.size());

					// Skip empty actors
					builder = new SubjectCoverage.Builder();
					NonStateActor.Builder emptyNonStateActorBuilder = new NonStateActor.Builder();
					NonStateActor.Builder fullNonStateActorBuilder = new NonStateActor.Builder();
					fullNonStateActorBuilder.Value = "Laotian Monks";
					builder.Keywords.get(0).Value = "test";
					builder.NonStateActors.add(emptyNonStateActorBuilder);
					builder.NonStateActors.add(fullNonStateActorBuilder);
					assertEquals(1, builder.commit().NonStateActors.size());
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SubjectCoverage.Builder builder = new SubjectCoverage.Builder();
				assertNotNull(builder.Keywords.get(1));
				assertNotNull(builder.Categories.get(1));
				assertNotNull(builder.ProductionMetrics.get(1));
			}
		}
	}

}