using System;
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
namespace buri.ddmsence.ddms.resource {


	using Element = nu.xom.Element;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using Link = buri.ddmsence.ddms.summary.Link;
	using LinkTest = buri.ddmsence.ddms.summary.LinkTest;
	using XLinkAttributes = buri.ddmsence.ddms.summary.xlink.XLinkAttributes;
	using XLinkAttributesTest = buri.ddmsence.ddms.summary.xlink.XLinkAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:revisionRecall elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class RevisionRecallTest : AbstractBaseTestCase {

		private static readonly int? TEST_REVISION_ID = Convert.ToInt32(1);
		private const string TEST_REVISION_TYPE = "ADMINISTRATIVE RECALL";
		private const string TEST_VALUE = "Description of Recall";
		private const string TEST_NETWORK = "NIPRNet";
		private const string TEST_OTHER_NETWORK = "PBS";

		/// <summary>
		/// Constructor
		/// </summary>
		public RevisionRecallTest() : base("revisionRecall.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static Element TextFixtureElement {
			get {
				DDMSVersion version = DDMSVersion.CurrentVersion;
				Element element = new Element((new RevisionRecallTest()).GetValidElement(version.Version));
				element.removeChildren();
				element.appendChild(TEST_VALUE);
				return (element);
			}
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static RevisionRecall TextFixture {
			get {
				try {
					return (new RevisionRecall(TextFixtureElement));
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private RevisionRecall GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			RevisionRecall component = null;
			try {
				component = new RevisionRecall(element);
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
		/// <param name="links"> associated links (optional) </param>
		/// <param name="details"> associated details (optional) </param>
		/// <param name="revisionID"> integer ID for this recall (required) </param>
		/// <param name="revisionType"> type of revision (required) </param>
		/// <param name="network"> the network (optional) </param>
		/// <param name="otherNetwork"> another network (optional) </param>
		/// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
		private RevisionRecall GetInstance(string message, IList<Link> links, IList<Details> details, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes) {
			bool expectFailure = !Util.isEmpty(message);
			RevisionRecall component = null;
			try {
				component = new RevisionRecall(links, details, revisionID, revisionType, network, otherNetwork, xlinkAttributes, SecurityAttributesTest.Fixture);
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
		/// <param name="value"> child text (optional) </param>
		/// <param name="revisionID"> integer ID for this recall (required) </param>
		/// <param name="revisionType"> type of revision (required) </param>
		/// <param name="network"> the network (optional) </param>
		/// <param name="otherNetwork"> another network (optional) </param>
		/// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
		private RevisionRecall GetInstance(string message, string value, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes) {
			bool expectFailure = !Util.isEmpty(message);
			RevisionRecall component = null;
			try {
				component = new RevisionRecall(value, revisionID, revisionType, network, otherNetwork, xlinkAttributes, SecurityAttributesTest.Fixture);
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
//ORIGINAL LINE: private String getExpectedOutput(boolean hasLinks, boolean isHTML) throws buri.ddmsence.ddms.InvalidDDMSException
		private string GetExpectedOutput(bool hasLinks, bool isHTML) {
			StringBuilder text = new StringBuilder();
			if (!hasLinks) {
				text.Append(BuildOutput(isHTML, "revisionRecall", TEST_VALUE));
			}
			text.Append(BuildOutput(isHTML, "revisionRecall.revisionID", "1"));
			text.Append(BuildOutput(isHTML, "revisionRecall.revisionType", "ADMINISTRATIVE RECALL"));
			text.Append(BuildOutput(isHTML, "revisionRecall.network", "NIPRNet"));
			text.Append(BuildOutput(isHTML, "revisionRecall.otherNetwork", "PBS"));
			if (hasLinks) {
				text.Append(BuildOutput(isHTML, "revisionRecall.link.type", "locator"));
				text.Append(BuildOutput(isHTML, "revisionRecall.link.href", "http://en.wikipedia.org/wiki/Tank"));
				text.Append(BuildOutput(isHTML, "revisionRecall.link.role", "tank"));
				text.Append(BuildOutput(isHTML, "revisionRecall.link.title", "Tank Page"));
				text.Append(BuildOutput(isHTML, "revisionRecall.link.label", "tank"));
				text.Append(BuildOutput(isHTML, "revisionRecall.link.classification", "U"));
				text.Append(BuildOutput(isHTML, "revisionRecall.link.ownerProducer", "USA"));
				text.Append(BuildOutput(isHTML, "revisionRecall.details", "Details"));
				text.Append(BuildOutput(isHTML, "revisionRecall.details.classification", "U"));
				text.Append(BuildOutput(isHTML, "revisionRecall.details.ownerProducer", "USA"));
			}
			text.Append(XLinkAttributesTest.ResourceFixture.getOutput(isHTML, "revisionRecall."));
			text.Append(SecurityAttributesTest.Fixture.getOutput(isHTML, "revisionRecall."));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string GetExpectedXMLOutput(bool hasLinks) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:revisionRecall ").Append(XmlnsDDMS).Append(" xmlns:xlink=\"http://www.w3.org/1999/xlink\" ");
			xml.Append(XmlnsISM).Append(" ");
			xml.Append("ddms:revisionID=\"1\" ddms:revisionType=\"ADMINISTRATIVE RECALL\" ");
			xml.Append("network=\"NIPRNet\" otherNetwork=\"PBS\" ");
			xml.Append("xlink:type=\"resource\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
			xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");

			if (hasLinks) {
				xml.Append("<ddms:link xlink:type=\"locator\" xlink:href=\"http://en.wikipedia.org/wiki/Tank\" ");
				xml.Append("xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
				xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" />");
				xml.Append("<ddms:details ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Details</ddms:details>");
			} else {
				xml.Append(TEST_VALUE);
			}
			xml.Append("</ddms:revisionRecall>");

			return (xml.ToString());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, RevisionRecall.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// All fields (links)
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// All fields (text)
				GetInstance(SUCCESS, TextFixtureElement);

				// No optional fields (links)
				Element element = Util.buildDDMSElement(RevisionRecall.getName(version), null);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				element.appendChild(LinkTest.GetLocatorFixture(true).XOMElementCopy);
				GetInstance(SUCCESS, element);

				// No optional fields (text)
				element = Util.buildDDMSElement(RevisionRecall.getName(version), null);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields (links)
				GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

				// All fields (text)
				GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

				// No optional fields (links)
				GetInstance(SUCCESS, null, null, TEST_REVISION_ID, TEST_REVISION_TYPE, null, null, null);

				// No optional fields (text)
				GetInstance(SUCCESS, null, TEST_REVISION_ID, TEST_REVISION_TYPE, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Wrong type of XLinkAttributes (locator)
				Element element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				XLinkAttributesTest.LocatorFixture.addTo(element);
				GetInstance("revision ID is required.", element);

				// Both text AND links/details, text first
				element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				element.appendChild(LinkTest.GetLocatorFixture(true).XOMElementCopy);
				GetInstance("A ddms:revisionRecall element cannot have both child text and nested elements.", element);

				// Both text AND links/details, text last
				element = Util.buildDDMSElement(RevisionRecall.getName(version), null);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				element.appendChild(LinkTest.GetLocatorFixture(true).XOMElementCopy);
				element.appendChild(TEST_VALUE);
				GetInstance("A ddms:revisionRecall element cannot have both child text and nested elements.", element);

				// Links without security attributes
				element = Util.buildDDMSElement(RevisionRecall.getName(version), null);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				Link link = new Link(XLinkAttributesTest.LocatorFixture, null);
				element.appendChild(link.XOMElementCopy);
				GetInstance("classification is required.", element);

				// Bad revisionID
				element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				Util.addDDMSAttribute(element, "revisionID", "one");
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("revision ID is required.", element);

				// Missing revisionID
				element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("revision ID is required.", element);

				// Missing revisionType
				element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("The revisionType attribute must be one of", element);

				// Bad revisionType
				element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", "MISTAKE");
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("The revisionType attribute must be one of", element);

				// Bad network
				element = Util.buildDDMSElement(RevisionRecall.getName(version), TEST_VALUE);
				Util.addAttribute(element, "", "network", "", "PBS");
				Util.addDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
				Util.addDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("The network attribute must be one of", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Wrong type of XLinkAttributes (locator)
				GetInstance("The type attribute must have a fixed value", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.LocatorFixture);

				// Links without security attributes
				Link link = new Link(XLinkAttributesTest.LocatorFixture, null);
				IList<Link> linkList = new List<Link>();
				linkList.Add(link);
				GetInstance("classification is required.", linkList, DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.LocatorFixture);

				// Missing revisionID
				GetInstance("revision ID is required.", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, null, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

				// Missing revisionType
				GetInstance("The revisionType attribute must be one of", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, null, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

				// Bad revisionType
				GetInstance("The revisionType attribute must be one of", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, "MISTAKE", TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

				// Bad network
				GetInstance("The network attribute must be one of", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, "PBS", TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// links
				RevisionRecall elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				RevisionRecall dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

				// text
				elementComponent = GetInstance(SUCCESS, TextFixtureElement);
				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// links
				RevisionRecall elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				RevisionRecall dataComponent = GetInstance(SUCCESS, null, DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), null, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, Convert.ToInt32(2), TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, "ADMINISTRATIVE REVISION", TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, "SIPRNet", TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, "DoD-Dist-B", XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, null);
				assertFalse(elementComponent.Equals(dataComponent));

				// text
				elementComponent = GetInstance(SUCCESS, TextFixtureElement);
				dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, Convert.ToInt32(2), TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, "ADMINISTRATIVE REVISION", TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, "SIPRNet", TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, "DoD-Dist-B", XLinkAttributesTest.ResourceFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, null);
				assertFalse(elementComponent.Equals(dataComponent));

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RevisionRecall elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// links
				RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true, true), component.toHTML());
				assertEquals(GetExpectedOutput(true, false), component.toText());

				component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertEquals(GetExpectedOutput(true, true), component.toHTML());
				assertEquals(GetExpectedOutput(true, false), component.toText());

				// text
				component = GetInstance(SUCCESS, TextFixtureElement);
				assertEquals(GetExpectedOutput(false, true), component.toHTML());
				assertEquals(GetExpectedOutput(false, false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertEquals(GetExpectedOutput(false, true), component.toHTML());
				assertEquals(GetExpectedOutput(false, false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// links
				RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				// text
				component = GetInstance(SUCCESS, TextFixtureElement);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "4.0.1";
				XLinkAttributes attr = XLinkAttributesTest.SimpleFixture;
				DDMSVersion.CurrentVersion = "2.0";
				new RevisionRecall(TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, attr, SecurityAttributesTest.Fixture);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "These attributes cannot decorate");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Equality after Building (links)
				RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
				RevisionRecall.Builder builder = new RevisionRecall.Builder(component);
				assertEquals(component, builder.commit());

				// Equality after Building (text)
				component = GetInstance(SUCCESS, TextFixtureElement);
				builder = new RevisionRecall.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RevisionRecall.Builder builder = new RevisionRecall.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Links.get(2).SecurityAttributes.Classification = "U";
				assertFalse(builder.Empty);

				builder = new RevisionRecall.Builder();
				assertTrue(builder.Empty);
				builder.Details.get(2).SecurityAttributes.Classification = "U";
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RevisionRecall.Builder builder = new RevisionRecall.Builder();
				builder.RevisionID = TEST_REVISION_ID;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The revisionType attribute must be one of");
				}
				builder.RevisionType = TEST_REVISION_TYPE;
				builder.commit();
			}
		}
	}

}