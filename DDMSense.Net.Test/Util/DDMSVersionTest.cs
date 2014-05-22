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

namespace DDMSense.Test.Util
{
    using DDMSense.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using UnsupportedVersionException = DDMSense.DDMS.UnsupportedVersionException;

    /// <summary>
    /// A collection of DDMSVersion tests.
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class DDMSVersionTest : AbstractBaseTestCase
    {
        public DDMSVersionTest()
            : base(null)
        {
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetVersionForInvalid()
        {
            try
            {
                DDMSVersion.GetVersionFor("1.4");
                Assert.Fail("Allowed unsupported version.");
            }
            catch (UnsupportedVersionException e)
            {
                ExpectMessage(e, "DDMS Version 1.4");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetVersionForDDMSNamespace()
        {
            Assert.AreEqual(DDMSVersion.GetVersionFor("2.0"), DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/2.0/"));
            Assert.AreEqual(DDMSVersion.GetVersionFor("3.0"), DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
            Assert.AreEqual(DDMSVersion.GetVersionFor("3.1"), DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.1/"));
            Assert.AreEqual(DDMSVersion.GetVersionFor("4.1"), DDMSVersion.GetVersionForNamespace("urn:us:mil:ces:metadata:ddms:4"));
            try
            {
                DDMSVersion.GetVersionForNamespace("http://metadata.dod.mil/mdr/ns/DDMS/1.4/");
                Assert.Fail("Allowed unsupported version.");
            }
            catch (UnsupportedVersionException e)
            {
                ExpectMessage(e, "DDMS Version for XML namespace");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetVersionForGMLNamespace()
        {
            Assert.AreEqual(DDMSVersion.GetVersionFor("2.0"), DDMSVersion.GetVersionForNamespace("http://www.opengis.net/gml"));
            Assert.AreEqual(DDMSVersion.GetVersionFor("4.1"), DDMSVersion.GetVersionForNamespace("http://www.opengis.net/gml/3.2"));
            try
            {
                DDMSVersion.GetVersionForNamespace("http://www.opengis.net/gml/3.2.1");
                Assert.Fail("Allowed unsupported version.");
            }
            catch (UnsupportedVersionException e)
            {
                ExpectMessage(e, "DDMS Version for XML namespace");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetVersionForNTKNamespace()
        {
            Assert.AreEqual(DDMSVersion.GetVersionFor("4.1"), DDMSVersion.GetVersionForNamespace("urn:us:gov:ic:ntk"));
            try
            {
                DDMSVersion.GetVersionForNamespace("urn:us:gov:ic:ntk:v2");
                Assert.Fail("Allowed unsupported version.");
            }
            catch (UnsupportedVersionException e)
            {
                ExpectMessage(e, "DDMS Version for XML namespace");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetSupportedVersions()
        {
            Assert.IsFalse(!DDMSVersion.SupportedVersions.Any());
            Assert.IsTrue(DDMSVersion.SupportedVersions.Contains("3.0"));
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_IsSupportedXmlNamespace()
        {
            Assert.IsTrue(DDMSVersion.IsSupportedDDMSNamespace("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
            Assert.IsFalse(DDMSVersion.IsSupportedDDMSNamespace("http://metadata.dod.mil/mdr/ns/DDMS/1.4/"));
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetCurrentSchema()
        {
            Assert.AreEqual("TestData\\schemas\\4.1\\DDMS\\ddms.xsd", DDMSVersion.CurrentVersion.Schema);
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetCurrentNamespace()
        {
            Assert.AreEqual("urn:us:mil:ces:metadata:ddms:4", DDMSVersion.CurrentVersion.Namespace);
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetNamespaceForValid()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            Assert.AreEqual("http://metadata.dod.mil/mdr/ns/DDMS/2.0/", DDMSVersion.CurrentVersion.Namespace);
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_SetCurrentVersionInvalid()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("1.4");
                Assert.Fail("Allowed unsupported version.");
            }
            catch (UnsupportedVersionException e)
            {
                ExpectMessage(e, "DDMS Version 1.4");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_GetSchemaForValid()
        {
            DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
            Assert.AreEqual("TestData\\schemas\\2.0\\DDMS\\ddms.xsd", DDMSVersion.CurrentVersion.Schema);
            Assert.AreEqual("2.0", version.Version);
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_ToString()
        {
            Assert.AreEqual(DDMSVersion.CurrentVersion.Version, DDMSVersion.CurrentVersion.ToString());
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_Accessors()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            DDMSVersion version = DDMSVersion.CurrentVersion;
            Assert.AreEqual("3.0", version.Version);
            Assert.AreEqual("http://metadata.dod.mil/mdr/ns/DDMS/3.0/", version.Namespace);
            Assert.AreEqual("TestData\\schemas\\3.0\\DDMS\\ddms.xsd", version.Schema);
            Assert.AreEqual("http://www.opengis.net/gml/3.2", version.GmlNamespace);
            Assert.AreEqual("TestData\\schemas\\3.0\\DDMS\\gml.xsd", version.GmlSchema);
            Assert.AreEqual("urn:us:gov:ic:ism", version.IsmNamespace);
            Assert.AreEqual("TestData\\schemas\\3.0\\ISM\\CVE\\", version.IsmCveLocation);

            version = DDMSVersion.SetCurrentVersion("4.1");
            Assert.AreEqual("urn:us:gov:ic:ntk", version.NtkNamespace);
            Assert.AreEqual("TestData\\schemas\\4.1\\NTK\\IC-NTK.xsd", version.NtkSchema);
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_AliasVersion()
        {
            DDMSVersion.SetCurrentVersion("3.0.1");
            Assert.AreEqual("3.0", DDMSVersion.CurrentVersion.Version);
            Assert.AreEqual("3.0", DDMSVersion.GetVersionFor("3.0.1").Version);
            Assert.IsTrue(DDMSVersion.CurrentVersion.Version.Equals("3.0"));
        }

        [TestMethod]
        public virtual void Util_DDMSVersion_IsNewerThan()
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

            try
            {
                DDMSVersion.CurrentVersion.IsAtLeast("dog");
                Assert.Fail("Allowed invalid data.");
            }
            catch (UnsupportedVersionException e)
            {
                ExpectMessage(e, "DDMS Version dog is not yet supported.");
            }
        }
    }
}