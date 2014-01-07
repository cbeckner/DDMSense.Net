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
namespace buri.ddmsence.ddms {


	using Element = nu.xom.Element;
	using ExtensibleAttributes = buri.ddmsence.ddms.extensible.ExtensibleAttributes;
	using ExtensibleAttributesTest = buri.ddmsence.ddms.extensible.ExtensibleAttributesTest;
	using Organization = buri.ddmsence.ddms.resource.Organization;
	using Person = buri.ddmsence.ddms.resource.Person;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to underlying methods in the base class for DDMS producer entities </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class RoleEntityTest : AbstractBaseTestCase {

		public RoleEntityTest() : base(null) {
		}

		private const string TEST_POC_TYPE = "DoD-Dist-B";

		/// <summary>
		/// Helper method to generate a pocType for producers
		/// </summary>
		public static IList<string> PocTypes {
			get {
				return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? Util.getXsListAsList(TEST_POC_TYPE) : Util.getXsListAsList(""));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSharedWarnings() throws InvalidDDMSException
		public virtual void TestSharedWarnings() {
			DDMSVersion version = DDMSVersion.CurrentVersion;

			// Empty phone
			Element entityElement = Util.buildDDMSElement(Organization.getName(version), null);
			entityElement.appendChild(Util.buildDDMSElement("name", "name"));
			entityElement.appendChild(Util.buildDDMSElement("phone", ""));
			Organization component = new Organization(entityElement);
			assertEquals(1, component.ValidationWarnings.size());
			assertEquals(ValidationMessage.WARNING_TYPE, component.ValidationWarnings.get(0).Type);
			assertEquals("A ddms:phone element was found with no value.", component.ValidationWarnings.get(0).Text);

			// Empty email
			entityElement = Util.buildDDMSElement(Organization.getName(version), null);
			entityElement.appendChild(Util.buildDDMSElement("name", "name"));
			entityElement.appendChild(Util.buildDDMSElement("email", ""));
			component = new Organization(entityElement);
			assertEquals(1, component.ValidationWarnings.size());
			assertEquals(ValidationMessage.WARNING_TYPE, component.ValidationWarnings.get(0).Type);
			assertEquals("A ddms:email element was found with no value.", component.ValidationWarnings.get(0).Text);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testIndexLevelsStringLists() throws InvalidDDMSException
		public virtual void TestIndexLevelsStringLists() {
			IList<string> names = Util.getXsListAsList("Brian BU");
			IList<string> phones = Util.getXsListAsList("703-885-1000");
			Person person = new Person(names, "Uri", phones, null, null, null);

			PropertyReader.setProperty("output.indexLevel", "0");
			assertEquals("entityType: person\nname: Brian\nname: BU\nphone: 703-885-1000\nsurname: Uri\n", person.toText());

			PropertyReader.setProperty("output.indexLevel", "1");
			assertEquals("entityType: person\nname[1]: Brian\nname[2]: BU\nphone: 703-885-1000\nsurname: Uri\n", person.toText());

			PropertyReader.setProperty("output.indexLevel", "2");
			assertEquals("entityType: person\nname[1]: Brian\nname[2]: BU\nphone[1]: 703-885-1000\nsurname: Uri\n", person.toText());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleSuccess() throws InvalidDDMSException
		public virtual void TestExtensibleSuccess() {
			foreach (string sVersion in DDMSVersion.SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
				IList<string> names = new List<string>();
				names.Add("DISA");
				new Organization(names, null, null, null, null, attr);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleFailure() throws InvalidDDMSException
		public virtual void TestExtensibleFailure() {
			// No failure cases to test right now.
			// ISM attributes are at creator/contributor level, so they never clash with extensibles on the entity level.
		}
	}

}