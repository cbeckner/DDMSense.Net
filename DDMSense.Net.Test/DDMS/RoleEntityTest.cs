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
namespace DDMSense.Test.DDMS
{



    using ExtensibleAttributes = DDMSense.DDMS.Extensible.ExtensibleAttributes;
    using ExtensibleAttributesTest = DDMSense.Test.DDMS.Extensible.ExtensibleAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using System.Xml.Linq;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSense.DDMS;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to underlying methods in the base class for DDMS producer entities </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class RoleEntityTest : AbstractBaseTestCase
    {

        public RoleEntityTest()
            : base(null)
        {
        }

        private const string TEST_POC_TYPE = "DoD-Dist-B";

        /// <summary>
        /// Helper method to generate a pocType for producers
        /// </summary>
        public static List<string> PocTypes
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? Util.GetXsListAsList(TEST_POC_TYPE) : Util.GetXsListAsList(""));
            }
        }

        [TestMethod]
        public virtual void RoleEntity_SharedWarnings()
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;

            // Empty phone
            XElement entityElement = Util.BuildDDMSElement(Organization.GetName(version), null);
            entityElement.Add(Util.BuildDDMSElement("name", "name"));
            entityElement.Add(Util.BuildDDMSElement("phone", ""));
            Organization component = new Organization(entityElement);
            Assert.Equals(1, component.ValidationWarnings.Count());
            Assert.Equals(ValidationMessage.WarningType, component.ValidationWarnings[0].Type);
            Assert.Equals("A ddms:phone element was found with no value.", component.ValidationWarnings[0].Text);

            // Empty email
            entityElement = Util.BuildDDMSElement(Organization.GetName(version), null);
            entityElement.Add(Util.BuildDDMSElement("name", "name"));
            entityElement.Add(Util.BuildDDMSElement("email", ""));
            component = new Organization(entityElement);
            Assert.Equals(1, component.ValidationWarnings.Count());
            Assert.Equals(ValidationMessage.WarningType, component.ValidationWarnings[0].Type);
            Assert.Equals("A ddms:email element was found with no value.", component.ValidationWarnings[0].Text);
        }

        [TestMethod]
        public virtual void RoleEntity_IndexLevelsStringLists()
        {
            List<string> names = Util.GetXsListAsList("Brian BU");
            List<string> phones = Util.GetXsListAsList("703-885-1000");
            Person person = new Person(names, "Uri", phones, null, null, null);

            PropertyReader.SetProperty("output.indexLevel", "0");
            Assert.Equals("entityType: person\nname: Brian\nname: BU\nphone: 703-885-1000\nsurname: Uri\n", person.ToText());

            PropertyReader.SetProperty("output.indexLevel", "1");
            Assert.Equals("entityType: person\nname[1]: Brian\nname[2]: BU\nphone: 703-885-1000\nsurname: Uri\n", person.ToText());

            PropertyReader.SetProperty("output.indexLevel", "2");
            Assert.Equals("entityType: person\nname[1]: Brian\nname[2]: BU\nphone[1]: 703-885-1000\nsurname: Uri\n", person.ToText());
        }

        [TestMethod]
        public virtual void RoleEntity_ExtensibleSuccess()
        {
            foreach (string sVersion in DDMSVersion.SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
                List<string> names = new List<string>();
                names.Add("DISA");
                new Organization(names, null, null, null, null, attr);
            }
        }

        [TestMethod]
        public virtual void RoleEntity_ExtensibleFailure()
        {
            // No failure cases to test right now.
            // ISM attributes are at creator/contributor level, so they never clash with extensibles on the entity level.
        }
    }

}