using System;
using System.Collections.Generic;

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
namespace buri.ddmsence {


	using Element = nu.xom.Element;
	using IDDMSComponent = buri.ddmsence.ddms.IDDMSComponent;
	using InvalidDDMSException = buri.ddmsence.ddms.InvalidDDMSException;
	using ValidationMessage = buri.ddmsence.ddms.ValidationMessage;
	using Creator = buri.ddmsence.ddms.resource.Creator;
	using Language = buri.ddmsence.ddms.resource.Language;
	using Organization = buri.ddmsence.ddms.resource.Organization;
	using Rights = buri.ddmsence.ddms.resource.Rights;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to underlying methods in the base class for DDMS components </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class BaseComponentTest : AbstractBaseTestCase {

		public BaseComponentTest() : base(null) {
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildIndexInvalidInputBounds() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuildIndexInvalidInputBounds() {
			Rights rights = new Rights(true, true, true);

			// Bad total
			try {
				rights.buildIndex(0, 0);
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "The total must be at least 1");
			}

			// Low index
			try {
				rights.buildIndex(-1, 1);
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "The index is not properly bounded");
			}

			// High index
			try {
				rights.buildIndex(2, 2);
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "The index is not properly bounded");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildIndexValidInputBounds() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuildIndexValidInputBounds() {
			Rights rights = new Rights(true, true, true);

			// Good total
			rights.buildIndex(0, 1);

			// Good index
			rights.buildIndex(1, 2);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildIndexLevel0() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuildIndexLevel0() {
			Rights rights = new Rights(true, true, true);

			PropertyReader.setProperty("output.indexLevel", "0");
			string index = rights.buildIndex(0, 1);
			assertEquals("", index);
			index = rights.buildIndex(2, 4);
			assertEquals("", index);

			PropertyReader.setProperty("output.indexLevel", "unknown");
			index = rights.buildIndex(0, 1);
			assertEquals("", index);
			index = rights.buildIndex(2, 4);
			assertEquals("", index);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildIndexLevel1() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuildIndexLevel1() {
			Rights rights = new Rights(true, true, true);

			PropertyReader.setProperty("output.indexLevel", "1");
			string index = rights.buildIndex(0, 1);
			assertEquals("", index);
			index = rights.buildIndex(2, 4);
			assertEquals("[3]", index);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildIndexLevel2() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuildIndexLevel2() {
			Rights rights = new Rights(true, true, true);

			PropertyReader.setProperty("output.indexLevel", "2");
			string index = rights.buildIndex(0, 1);
			assertEquals("[1]", index);
			index = rights.buildIndex(2, 4);
			assertEquals("[3]", index);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuildOutput() {
			Rights rights = new Rights(true, true, true);
			IList<IDDMSComponent> objectList = new List<IDDMSComponent>();
			objectList.Add(rights);
			assertEquals("rights.privacyAct: true\nrights.intellectualProperty: true\nrights.copyright: true\n", rights.buildOutput(false, "", objectList));

			IList<string> stringList = new List<string>();
			stringList.Add("Text");
			assertEquals("name: Text\n", rights.buildOutput(false, "name", stringList));

			IList<double?> otherList = new List<double?>();
			otherList.Add(Convert.ToDouble(2.0));
			assertEquals("name: 2.0\n", rights.buildOutput(false, "name", otherList));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSelfEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestSelfEquality() {
			Rights rights = new Rights(true, true, true);
			assertEquals(rights, rights);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testToString() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestToString() {
			Rights rights = new Rights(true, true, true);
			assertEquals(rights.ToString(), rights.toXML());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testVersion() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestVersion() {
			Rights rights = new Rights(true, true, true);
			assertEquals(DDMSVersion.CurrentVersion.Namespace, rights.Namespace);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testCustomPrefix() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestCustomPrefix() {
			string @namespace = DDMSVersion.CurrentVersion.Namespace;
			Element element = Util.buildElement("customPrefix", Language.getName(DDMSVersion.CurrentVersion), @namespace, null);
			Util.addAttribute(element, "customPrefix", "qualifier", @namespace, "testQualifier");
			Util.addAttribute(element, "customPrefix", "value", @namespace, "en");
			new Language(element);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNullChecks() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNullChecks() {
			AbstractBaseComponent component = new AbstractBaseComponentAnonymousInnerClassHelper(this);
			assertEquals("", component.Name);
			assertEquals("", component.Namespace);
			assertEquals("", component.Prefix);
			assertEquals("", component.toXML());
		}

		private class AbstractBaseComponentAnonymousInnerClassHelper : AbstractBaseComponent {
			private readonly BaseComponentTest OuterInstance;

			public AbstractBaseComponentAnonymousInnerClassHelper(BaseComponentTest outerInstance) {
				this.OuterInstance = outerInstance;
			}

			public virtual string GetOutput(bool isHTML, string prefix, string suffix) {
				return null;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testAttributeWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestAttributeWarnings() {
			AbstractBaseComponent component = new AbstractBaseComponentAnonymousInnerClassHelper2(this);
			IList<ValidationMessage> warnings = new List<ValidationMessage>();
			warnings.Add(ValidationMessage.newWarning("test", "locator"));
			component.addWarnings(warnings, true);
			assertEquals("//locator", component.ValidationWarnings.get(0).Locator);
		}

		private class AbstractBaseComponentAnonymousInnerClassHelper2 : AbstractBaseComponent {
			private readonly BaseComponentTest OuterInstance;

			public AbstractBaseComponentAnonymousInnerClassHelper2(BaseComponentTest outerInstance) {
				this.OuterInstance = outerInstance;
			}

			public virtual string GetOutput(bool isHTML, string prefix, string suffix) {
				return null;
			}

			protected internal virtual string LocatorSuffix {
				get {
					return ("locatorSuffix");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSameVersion() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestSameVersion() {
			DDMSVersion.CurrentVersion = "3.0";
			Organization org = new Organization(Util.getXsListAsList("DISA"), null, null, null, null, null);
			DDMSVersion.CurrentVersion = "2.0";
			try {
				new Creator(org, null, null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A child component, ddms:Organization");
			}
		}
	}

}