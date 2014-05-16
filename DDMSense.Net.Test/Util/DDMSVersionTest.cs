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
namespace DDMSense.Test.Util {
    using System.Linq;
    using DDMSense.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UnsupportedVersionException = DDMSense.DDMS.UnsupportedVersionException;

	/// <summary>
	/// A collection of DDMSVersion tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class DDMSVersionTest : AbstractBaseTestCase {

		public DDMSVersionTest() : base(null) {
		}

        [TestMethod]
        public virtual void TestGetVersionForInvalid()
        {
			try {
				DDMSVersion.GetVersionFor("1.4");
				Assert.Fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version 1.4");
			}
		}

        [TestMethod]
        public virtual void TestGetVersionForDDMSNamespace()
        {
			Assert.Equals(DDMSVersion.GetVersionFor("2.0"), DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/2.0/"));
			Assert.Equals(DDMSVersion.GetVersionFor("3.0"), DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
			Assert.Equals(DDMSVersion.GetVersionFor("3.1"), DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.1/"));
			Assert.Equals(DDMSVersion.GetVersionFor("4.1"), DDMSVersion.GetVersionForNamespace("urn:us:mil:ces:metadata:ddms:4"));
			try {
				DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/1.4/");
				Assert.Fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version for XML namespace");
			}
		}

        [TestMethod]
        public virtual void TestGetVersionForGMLNamespace()
        {
			Assert.Equals(DDMSVersion.GetVersionFor("2.0"), DDMSVersion.GetVersionForNamespace("http://www.opengis.net/gml"));
			Assert.Equals(DDMSVersion.GetVersionFor("4.1"), DDMSVersion.GetVersionForNamespace("http://www.opengis.net/gml/3.2"));
			try {
				DDMSVersion.GetVersionForNamespace("http://www.opengis.net/gml/3.2.1");
				Assert.Fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version for XML namespace");
			}
		}

        [TestMethod]
        public virtual void TestGetVersionForNTKNamespace()
        {
			Assert.Equals(DDMSVersion.GetVersionFor("4.1"), DDMSVersion.GetVersionForNamespace("urn:us:gov:ic:ntk"));
			try {
				DDMSVersion.GetVersionForNamespace("urn:us:gov:ic:ntk:v2");
				Assert.Fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version for XML namespace");
			}
		}

        [TestMethod]
        public virtual void TestGetSupportedVersions()
        {
			Assert.IsFalse(!DDMSVersion.SupportedVersions.Any());
			Assert.IsTrue(DDMSVersion.SupportedVersions.Contains("3.0"));
		}

        [TestMethod]
        public virtual void TestIsSupportedXmlNamespace()
        {
			Assert.IsTrue(DDMSVersion.IsSupportedDDMSNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
			Assert.IsFalse(DDMSVersion.IsSupportedDDMSNamespace("http://metadata.dod.mil/mdr/ns/DDMS/1.4/"));
		}

        [TestMethod]
        public virtual void TestGetCurrentSchema()
        {
			Assert.Equals("/schemas/4.1/DDMS/ddms.xsd", DDMSVersion.CurrentVersion.Schema);
		}

        [TestMethod]
        public virtual void TestGetCurrentNamespace()
        {
			Assert.Equals("urn:us:mil:ces:metadata:ddms:4", DDMSVersion.CurrentVersion.Namespace);
		}

        [TestMethod]
        public virtual void TestGetNamespaceForValid()
        {
            DDMSVersion.SetCurrentVersion("2.0");
			Assert.Equals("http://metadata.dod.mil/mdr/ns/DDMS/2.0/", DDMSVersion.CurrentVersion.Namespace);
		}

        [TestMethod]
        public virtual void TestSetCurrentVersionInvalid()
        {
			try {
				DDMSVersion.SetCurrentVersion( "1.4");
				Assert.Fail("Allowed unsupported version.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version 1.4");
			}
		}

        [TestMethod]
        public virtual void TestGetSchemaForValid()
        {
			DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
			Assert.Equals("/schemas/2.0/DDMS/ddms.xsd", DDMSVersion.CurrentVersion.Schema);
			Assert.Equals("2.0", version.Version);
		}

        [TestMethod]
        public virtual void TestToString()
        {
			Assert.Equals(DDMSVersion.CurrentVersion.Version, DDMSVersion.CurrentVersion.ToString());
		}

        [TestMethod]
        public virtual void TestAccessors()
        {
			DDMSVersion.SetCurrentVersion("3.0");
			DDMSVersion version = DDMSVersion.CurrentVersion;
			Assert.Equals("3.0", version.Version);
			Assert.Equals("http://metadata.dod.mil/mdr/ns/DDMS/3.0/", version.Namespace);
			Assert.Equals("/schemas/3.0/DDMS/ddms.xsd", version.Schema);
			Assert.Equals("http://www.opengis.net/gml/3.2", version.GmlNamespace);
			Assert.Equals("/schemas/3.0/DDMS/gml.xsd", version.GmlSchema);
			Assert.Equals("urn:us:gov:ic:ism", version.IsmNamespace);
			Assert.Equals("/schemas/3.0/ISM/CVE/", version.IsmCveLocation);

			version = DDMSVersion.SetCurrentVersion("4.1");
			Assert.Equals("urn:us:gov:ic:ntk", version.NtkNamespace);
			Assert.Equals("/schemas/4.1/NTK/IC-NTK.xsd", version.NtkSchema);
		}

        [TestMethod]
        public virtual void TestAliasVersion()
        {
			DDMSVersion.SetCurrentVersion("3.0.1");
			Assert.Equals("3.0", DDMSVersion.CurrentVersion.Version);
			Assert.Equals("3.0", DDMSVersion.GetVersionFor("3.0.1").Version);
			Assert.IsTrue(DDMSVersion.CurrentVersion.Version.Equals("3.0"));
		}

        [TestMethod]
        public virtual void TestIsNewerThan()
        {
			Assert.IsTrue(DDMSVersion.GetVersionFor("2.0").IsAtLeast("2.0"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("2.0").IsAtLeast("3.0"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("2.0").IsAtLeast("3.0.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("2.0").IsAtLeast("3.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("2.0").IsAtLeast("4.0.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("2.0").IsAtLeast("4.1"));

			Assert.IsTrue(DDMSVersion.GetVersionFor("3.0").IsAtLeast("2.0"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("3.0").IsAtLeast("3.0"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("3.0").IsAtLeast("3.0.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("3.0").IsAtLeast("3.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("3.0").IsAtLeast("4.0.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("3.0").IsAtLeast("4.1"));

			Assert.IsTrue(DDMSVersion.GetVersionFor("3.1").IsAtLeast("2.0"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("3.1").IsAtLeast("3.0"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("3.1").IsAtLeast("3.0.1"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("3.1").IsAtLeast("3.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("3.1").IsAtLeast("4.0.1"));
			Assert.IsFalse(DDMSVersion.GetVersionFor("3.1").IsAtLeast("4.1"));

			Assert.IsTrue(DDMSVersion.GetVersionFor("4.1").IsAtLeast("2.0"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("4.1").IsAtLeast("3.0"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("4.1").IsAtLeast("3.0.1"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("4.1").IsAtLeast("3.1"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("4.1").IsAtLeast("4.0.1"));
			Assert.IsTrue(DDMSVersion.GetVersionFor("4.1").IsAtLeast("4.1"));

			try {
				DDMSVersion.CurrentVersion.IsAtLeast("dog");
				Assert.Fail("Allowed invalid data.");
			} catch (UnsupportedVersionException e) {
				ExpectMessage(e, "DDMS Version dog is not yet supported.");
			}
		}
	}

}