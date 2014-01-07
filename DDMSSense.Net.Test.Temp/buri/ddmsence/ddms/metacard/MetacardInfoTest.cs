using System.Collections;
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
namespace buri.ddmsence.ddms.metacard {


	using Element = nu.xom.Element;
	using ContributorTest = buri.ddmsence.ddms.resource.ContributorTest;
	using CreatorTest = buri.ddmsence.ddms.resource.CreatorTest;
	using Dates = buri.ddmsence.ddms.resource.Dates;
	using DatesTest = buri.ddmsence.ddms.resource.DatesTest;
	using Identifier = buri.ddmsence.ddms.resource.Identifier;
	using IdentifierTest = buri.ddmsence.ddms.resource.IdentifierTest;
	using LanguageTest = buri.ddmsence.ddms.resource.LanguageTest;
	using PointOfContactTest = buri.ddmsence.ddms.resource.PointOfContactTest;
	using ProcessingInfoTest = buri.ddmsence.ddms.resource.ProcessingInfoTest;
	using Publisher = buri.ddmsence.ddms.resource.Publisher;
	using PublisherTest = buri.ddmsence.ddms.resource.PublisherTest;
	using RecordsManagementInfo = buri.ddmsence.ddms.resource.RecordsManagementInfo;
	using RecordsManagementInfoTest = buri.ddmsence.ddms.resource.RecordsManagementInfoTest;
	using RevisionRecall = buri.ddmsence.ddms.resource.RevisionRecall;
	using RevisionRecallTest = buri.ddmsence.ddms.resource.RevisionRecallTest;
	using NoticeList = buri.ddmsence.ddms.security.NoticeList;
	using NoticeListTest = buri.ddmsence.ddms.security.NoticeListTest;
	using SecurityAttributes = buri.ddmsence.ddms.security.ism.SecurityAttributes;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using AccessTest = buri.ddmsence.ddms.security.ntk.AccessTest;
	using Description = buri.ddmsence.ddms.summary.Description;
	using DescriptionTest = buri.ddmsence.ddms.summary.DescriptionTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:metacardInfo elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class MetacardInfoTest : AbstractBaseTestCase {

		/// <summary>
		/// Constructor
		/// </summary>
		public MetacardInfoTest() : base("metacardInfo.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static MetacardInfo Fixture {
			get {
				try {
					return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? new MetacardInfo(RequiredChildComponents, SecurityAttributesTest.Fixture) : null);
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Returns a list of child components for testing.
		/// </summary>
		private static IList<IDDMSComponent> RequiredChildComponents {
			get {
				IList<IDDMSComponent> childComponents = new List<IDDMSComponent>();
				childComponents.Add(IdentifierTest.Fixture);
				childComponents.Add(DatesTest.Fixture);
				childComponents.Add(PublisherTest.Fixture);
				return (childComponents);
			}
		}

		/// <summary>
		/// Returns a list of child components for testing.
		/// </summary>
		private static IList<IDDMSComponent> ChildComponents {
			get {
				IList<IDDMSComponent> childComponents = new List<IDDMSComponent>();
				childComponents.Add(IdentifierTest.Fixture);
				childComponents.Add(DatesTest.Fixture);
				childComponents.Add(PublisherTest.Fixture);
				childComponents.Add(ContributorTest.Fixture);
				childComponents.Add(CreatorTest.Fixture);
				childComponents.Add(PointOfContactTest.Fixture);
				childComponents.Add(DescriptionTest.Fixture);
				childComponents.Add(ProcessingInfoTest.Fixture);
				childComponents.Add(RevisionRecallTest.TextFixture);
				childComponents.Add(RecordsManagementInfoTest.Fixture);
				childComponents.Add(NoticeListTest.Fixture);
				if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
					childComponents.Add(AccessTest.Fixture);
				}
				return (childComponents);
			}
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private MetacardInfo GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			MetacardInfo component = null;
			try {
				component = new MetacardInfo(element);
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
		/// <param name="childComponents"> a list of child components to be put into this instance (required) </param>
		private MetacardInfo GetInstance(string message, IList<IDDMSComponent> childComponents) {
			bool expectFailure = !Util.isEmpty(message);
			MetacardInfo component = null;
			try {
				component = new MetacardInfo(childComponents, SecurityAttributesTest.Fixture);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws buri.ddmsence.ddms.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			foreach (IDDMSComponent component in ChildComponents) {
				text.Append(((AbstractBaseComponent) component).getOutput(isHTML, "metacardInfo.", ""));
			}
			text.Append(BuildOutput(isHTML, "metacardInfo.classification", "U"));
			text.Append(BuildOutput(isHTML, "metacardInfo.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:metacardInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
			xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
			xml.Append("<ddms:identifier ddms:qualifier=\"URI\" ddms:value=\"urn:buri:ddmsence:testIdentifier\" />");
			xml.Append("<ddms:dates ddms:created=\"2003\" />");
			xml.Append("<ddms:publisher><ddms:person><ddms:name>Brian</ddms:name>");
			xml.Append("<ddms:surname>Uri</ddms:surname></ddms:person></ddms:publisher>");
			xml.Append("<ddms:contributor><ddms:service><ddms:name>https://metadata.dod.mil/ebxmlquery/soap</ddms:name>");
			xml.Append("</ddms:service></ddms:contributor>");
			xml.Append("<ddms:creator><ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:creator>");
			xml.Append("<ddms:pointOfContact><ddms:unknown><ddms:name>UnknownEntity</ddms:name>");
			xml.Append("</ddms:unknown></ddms:pointOfContact>");
			xml.Append("<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
			xml.Append("A transformation service.</ddms:description>");
			xml.Append("<ddms:processingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
			xml.Append("ddms:dateProcessed=\"2011-08-19\">");
			xml.Append("XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.</ddms:processingInfo>");
			xml.Append("<ddms:revisionRecall xmlns:xlink=\"http://www.w3.org/1999/xlink\" ddms:revisionID=\"1\" ");
			xml.Append("ddms:revisionType=\"ADMINISTRATIVE RECALL\" network=\"NIPRNet\" otherNetwork=\"PBS\" ");
			xml.Append("xlink:type=\"resource\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
			xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Description of Recall</ddms:revisionRecall>");
			xml.Append("<ddms:recordsManagementInfo ddms:vitalRecordIndicator=\"true\">");
			xml.Append("<ddms:recordKeeper><ddms:recordKeeperID>#289-99202.9</ddms:recordKeeperID>");
			xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:recordKeeper>");
			xml.Append("<ddms:applicationSoftware ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
			xml.Append("IRM Generator 2L-9</ddms:applicationSoftware>");
			xml.Append("</ddms:recordsManagementInfo>");
			xml.Append("<ddms:noticeList ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
			xml.Append("<ISM:Notice ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
			xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
			if (DDMSVersion.CurrentVersion.isAtLeast("4.0.1")) {
				xml.Append(" ISM:externalNotice=\"false\"");
			}
			xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
			xml.Append("<ISM:NoticeText ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
			xml.Append(" ISM:pocType=\"DoD-Dist-B\">noticeText</ISM:NoticeText>");
			xml.Append("</ISM:Notice></ddms:noticeList>");
			if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
				xml.Append("<ntk:Access xmlns:ntk=\"urn:us:gov:ic:ntk\" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append("<ntk:AccessIndividualList>");
				xml.Append("<ntk:AccessIndividual ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append("<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>");
				xml.Append("<ntk:AccessIndividualValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append("user_2321889:Doe_John_H</ntk:AccessIndividualValue>");
				xml.Append("</ntk:AccessIndividual>");
				xml.Append("</ntk:AccessIndividualList>");
				xml.Append("</ntk:Access>");
			}
			xml.Append("</ddms:metacardInfo>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, MetacardInfo.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in RequiredChildComponents) {
					element.appendChild(component.XOMElementCopy);
				}
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, ChildComponents);

				// Null components
				IList<IDDMSComponent> components = ChildComponents;
				components.Add(null);
				GetInstance(SUCCESS, components);

				// No optional fields
				GetInstance(SUCCESS, RequiredChildComponents);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Missing identifier
				Element element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in RequiredChildComponents) {
					if (component is Identifier) {
						continue;
					}
					element.appendChild(component.XOMElementCopy);
				}
				GetInstance("At least one ddms:identifier", element);

				// Missing publisher
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in RequiredChildComponents) {
					if (component is Publisher) {
						continue;
					}
					element.appendChild(component.XOMElementCopy);
				}
				GetInstance("At least one ddms:publisher", element);

				// Missing dates
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in RequiredChildComponents) {
					if (component is Dates) {
						continue;
					}
					element.appendChild(component.XOMElementCopy);
				}
				GetInstance("Exactly 1 dates element must exist.", element);

				// Too many dates
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in RequiredChildComponents) {
					element.appendChild(component.XOMElementCopy);
					if (component is Dates) {
						element.appendChild(component.XOMElementCopy);
					}
				}
				GetInstance("Exactly 1 dates element must exist.", element);

				// Too many descriptions
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in ChildComponents) {
					element.appendChild(component.XOMElementCopy);
					if (component is Description) {
						element.appendChild(component.XOMElementCopy);
					}
				}
				GetInstance("No more than 1 description element can exist.", element);

				// Too many revisionRecalls
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in ChildComponents) {
					element.appendChild(component.XOMElementCopy);
					if (component is RevisionRecall) {
						element.appendChild(component.XOMElementCopy);
					}
				}
				GetInstance("No more than 1 revisionRecall element can exist.", element);

				// Too many recordsManagementInfos
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in ChildComponents) {
					element.appendChild(component.XOMElementCopy);
					if (component is RecordsManagementInfo) {
						element.appendChild(component.XOMElementCopy);
					}
				}
				GetInstance("No more than 1 recordsManagementInfo", element);

				// Too many noticeLists
				element = Util.buildDDMSElement(MetacardInfo.getName(version), null);
				foreach (IDDMSComponent component in ChildComponents) {
					element.appendChild(component.XOMElementCopy);
					if (component is NoticeList) {
						element.appendChild(component.XOMElementCopy);
					}
				}
				GetInstance("No more than 1 noticeList element can exist.", element);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No components
				GetInstance("At least one ddms:identifier", (IList) null);

				// Weird component
				IList<IDDMSComponent> components = ChildComponents;
				components.Add(LanguageTest.Fixture);
				GetInstance("language is not a valid child", components);

				// Missing identifier
				components = ChildComponents;
				components.Remove(IdentifierTest.Fixture);
				GetInstance("At least one ddms:identifier must exist", components);

				// Missing publisher
				components = ChildComponents;
				components.Remove(PublisherTest.Fixture);
				GetInstance("At least one ddms:publisher must exist", components);

				// Missing dates
				components = ChildComponents;
				components.Remove(DatesTest.Fixture);
				GetInstance("Exactly 1 dates element must exist.", components);

				// Incorrect version of security attributes
				DDMSVersion.CurrentVersion = "2.0";
				SecurityAttributes attributes = SecurityAttributesTest.Fixture;
				DDMSVersion.CurrentVersion = sVersion;
				try {
					new MetacardInfo(ChildComponents, attributes);
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "These attributes cannot decorate");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));

				// 4.1 ntk:Access element used
				if (version.isAtLeast("4.1")) {
					assertEquals(2, component.ValidationWarnings.size());
					string text = "The ntk:Access element in this DDMS component";
					string locator = "ddms:metacardInfo";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

					text = "The ISM:externalNotice attribute in this DDMS component";
					locator = "ddms:metacardInfo/ddms:noticeList/ISM:Notice";
					AssertWarningEquality(text, locator, component.ValidationWarnings.get(1));

				}
				// No warnings 
				else {
					assertEquals(0, component.ValidationWarnings.size());
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				MetacardInfo dataComponent = GetInstance(SUCCESS, ChildComponents);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));

				IList<IDDMSComponent> components = ChildComponents;
				components.Add(IdentifierTest.Fixture);
				MetacardInfo dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(DatesTest.Fixture);
				components.Add(new Dates(null, null, null, null, null, null, null));
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(CreatorTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(ContributorTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(PointOfContactTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Add(PublisherTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(DescriptionTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(ProcessingInfoTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(RevisionRecallTest.TextFixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(RecordsManagementInfoTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));

				components = ChildComponents;
				components.Remove(NoticeListTest.Fixture);
				dataComponent = GetInstance(SUCCESS, components);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, ChildComponents);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, ChildComponents);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				MetacardInfo.Builder builder = new MetacardInfo.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo.Builder builder = new MetacardInfo.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Dates.ApprovedOn = "2001";
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				MetacardInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				MetacardInfo.Builder builder = new MetacardInfo.Builder(component);
				builder.Identifiers.clear();
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "At least one ddms:identifier");
				}
				builder.Identifiers.get(0).Qualifier = "test";
				builder.Identifiers.get(0).Value = "test";
				builder.commit();
			}
		}
	}

}