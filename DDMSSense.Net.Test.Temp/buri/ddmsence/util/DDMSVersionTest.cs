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
namespace buri.ddmsence.util {

	using UnsupportedVersionException = buri.ddmsence.ddms.UnsupportedVersionException;

	/// <summary>
	/// A collection of DDMSVersion tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class DDMSVersionTest : AbstractBaseTestCase {

		public DDMSVersionTest() : base(null) {
		}

		public virtual void TestGetVersionForInvalid() {
			try {
				DDMSVersion.getVersionFor("1.4");
				fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version 1.4");
			}
		}

		public virtual void TestGetVersionForDDMSNamespace() {
			assertEquals(DDMSVersion.getVersionFor("2.0"), DDMSVersion.getVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/2.0/"));
			assertEquals(DDMSVersion.getVersionFor("3.0"), DDMSVersion.getVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
			assertEquals(DDMSVersion.getVersionFor("3.1"), DDMSVersion.getVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.1/"));
			assertEquals(DDMSVersion.getVersionFor("4.1"), DDMSVersion.getVersionForNamespace("urn:us:mil:ces:metadata:ddms:4"));
			try {
				DDMSVersion.getVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/1.4/");
				fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version for XML namespace");
			}
		}

		public virtual void TestGetVersionForGMLNamespace() {
			assertEquals(DDMSVersion.getVersionFor("2.0"), DDMSVersion.getVersionForNamespace("http://www.opengis.net/gml"));
			assertEquals(DDMSVersion.getVersionFor("4.1"), DDMSVersion.getVersionForNamespace("http://www.opengis.net/gml/3.2"));
			try {
				DDMSVersion.getVersionForNamespace("http://www.opengis.net/gml/3.2.1");
				fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version for XML namespace");
			}
		}

		public virtual void TestGetVersionForNTKNamespace() {
			assertEquals(DDMSVersion.getVersionFor("4.1"), DDMSVersion.getVersionForNamespace("urn:us:gov:ic:ntk"));
			try {
				DDMSVersion.getVersionForNamespace("urn:us:gov:ic:ntk:v2");
				fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version for XML namespace");
			}
		}

		public virtual void TestGetSupportedVersions() {
			assertFalse(DDMSVersion.SupportedVersions.Empty);
			assertTrue(DDMSVersion.SupportedVersions.contains("3.0"));
		}

		public virtual void TestIsSupportedXmlNamespace() {
			assertTrue(DDMSVersion.isSupportedDDMSNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
			assertFalse(DDMSVersion.isSupportedDDMSNamespace("http://metadata.dod.mil/mdr/ns/DDMS/1.4/"));
		}

		public virtual void TestGetCurrentSchema() {
			assertEquals("/schemas/4.1/DDMS/ddms.xsd", DDMSVersion.CurrentVersion.Schema);
		}

		public virtual void TestGetCurrentNamespace() {
			assertEquals("urn:us:mil:ces:metadata:ddms:4", DDMSVersion.CurrentVersion.Namespace);
		}

		public virtual void TestGetNamespaceForValid() {
			DDMSVersion.CurrentVersion = "2.0";
			assertEquals("http://metadata.dod.mil/mdr/ns/DDMS/2.0/", DDMSVersion.CurrentVersion.Namespace);
		}

		public virtual void TestSetCurrentVersionInvalid() {
			try {
				DDMSVersion.CurrentVersion = "1.4";
				fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version 1.4");
			}
		}

		public virtual void TestGetSchemaForValid() {
			DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
			assertEquals("/schemas/2.0/DDMS/ddms.xsd", DDMSVersion.CurrentVersion.Schema);
			assertEquals("2.0", version.Version);
		}

		public virtual void TestToString() {
			assertEquals(DDMSVersion.CurrentVersion.Version, DDMSVersion.CurrentVersion.ToString());
		}

		public virtual void TestAccessors() {
			DDMSVersion.CurrentVersion = "3.0";
			DDMSVersion version = DDMSVersion.CurrentVersion;
			assertEquals("3.0", version.Version);
			assertEquals("http://metadata.dod.mil/mdr/ns/DDMS/3.0/", version.Namespace);
			assertEquals("/schemas/3.0/DDMS/ddms.xsd", version.Schema);
			assertEquals("http://www.opengis.net/gml/3.2", version.GmlNamespace);
			assertEquals("/schemas/3.0/DDMS/gml.xsd", version.GmlSchema);
			assertEquals("urn:us:gov:ic:ism", version.IsmNamespace);
			assertEquals("/schemas/3.0/ISM/CVE/", version.IsmCveLocation);

			version = DDMSVersion.setCurrentVersion("4.1");
			assertEquals("urn:us:gov:ic:ntk", version.NtkNamespace);
			assertEquals("/schemas/4.1/NTK/IC-NTK.xsd", version.NtkSchema);
		}

		public virtual void TestAliasVersion() {
			DDMSVersion.CurrentVersion = "3.0.1";
			assertEquals("3.0", DDMSVersion.CurrentVersion.Version);
			assertEquals("3.0", DDMSVersion.getVersionFor("3.0.1").Version);
			assertTrue(DDMSVersion.CurrentVersion.Version.Equals("3.0"));
		}

		public virtual void TestIsNewerThan() {
			assertTrue(DDMSVersion.getVersionFor("2.0").isAtLeast("2.0"));
			assertFalse(DDMSVersion.getVersionFor("2.0").isAtLeast("3.0"));
			assertFalse(DDMSVersion.getVersionFor("2.0").isAtLeast("3.0.1"));
			assertFalse(DDMSVersion.getVersionFor("2.0").isAtLeast("3.1"));
			assertFalse(DDMSVersion.getVersionFor("2.0").isAtLeast("4.0.1"));
			assertFalse(DDMSVersion.getVersionFor("2.0").isAtLeast("4.1"));

			assertTrue(DDMSVersion.getVersionFor("3.0").isAtLeast("2.0"));
			assertTrue(DDMSVersion.getVersionFor("3.0").isAtLeast("3.0"));
			assertTrue(DDMSVersion.getVersionFor("3.0").isAtLeast("3.0.1"));
			assertFalse(DDMSVersion.getVersionFor("3.0").isAtLeast("3.1"));
			assertFalse(DDMSVersion.getVersionFor("3.0").isAtLeast("4.0.1"));
			assertFalse(DDMSVersion.getVersionFor("3.0").isAtLeast("4.1"));

			assertTrue(DDMSVersion.getVersionFor("3.1").isAtLeast("2.0"));
			assertTrue(DDMSVersion.getVersionFor("3.1").isAtLeast("3.0"));
			assertTrue(DDMSVersion.getVersionFor("3.1").isAtLeast("3.0.1"));
			assertTrue(DDMSVersion.getVersionFor("3.1").isAtLeast("3.1"));
			assertFalse(DDMSVersion.getVersionFor("3.1").isAtLeast("4.0.1"));
			assertFalse(DDMSVersion.getVersionFor("3.1").isAtLeast("4.1"));

			assertTrue(DDMSVersion.getVersionFor("4.1").isAtLeast("2.0"));
			assertTrue(DDMSVersion.getVersionFor("4.1").isAtLeast("3.0"));
			assertTrue(DDMSVersion.getVersionFor("4.1").isAtLeast("3.0.1"));
			assertTrue(DDMSVersion.getVersionFor("4.1").isAtLeast("3.1"));
			assertTrue(DDMSVersion.getVersionFor("4.1").isAtLeast("4.0.1"));
			assertTrue(DDMSVersion.getVersionFor("4.1").isAtLeast("4.1"));

			try {
				DDMSVersion.CurrentVersion.isAtLeast("dog");
				fail("Allowed invalid data.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version dog is not yet supported.");
			}
		}
	}

}