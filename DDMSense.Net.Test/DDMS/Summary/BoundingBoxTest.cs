using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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


    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to ddms:boundingBox elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class BoundingBoxTest : AbstractBaseTestCase
    {

        private const double TEST_WEST = 12.3;
        private const double TEST_EAST = 23.4;
        private const double TEST_SOUTH = 34.5;
        private const double TEST_NORTH = 45.6;

        /// <summary>
        /// Constructor
        /// </summary>
        public BoundingBoxTest()
            : base("boundingBox.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static BoundingBox Fixture
        {
            get
            {
                try
                {
                    return (new BoundingBox(1.1, 2.2, 3.3, 4.4));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
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
        private BoundingBox GetInstance(string message, XElement element)
        {
            bool expectedAssert = !String.IsNullOrEmpty(message);
            BoundingBox component = null;
            try
            {
                component = new BoundingBox(element);
                CheckConstructorSuccess(expectedAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectedAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="westBL"> the westbound longitude </param>
        /// <param name="eastBL"> the eastbound longitude </param>
        /// <param name="southBL"> the southbound latitude </param>
        /// <param name="northBL"> the northbound latitude </param>
        /// <returns> a valid object </returns>
        private BoundingBox GetInstance(string message, double westBL, double eastBL, double southBL, double northBL)
        {
            bool expectedAssert = !String.IsNullOrEmpty(message);
            BoundingBox component = null;
            try
            {
                component = new BoundingBox(westBL, eastBL, southBL, northBL);
                CheckConstructorSuccess(expectedAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectedAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to get the name of the westbound longitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string WestBLName
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "westBL" : "WestBL");
            }
        }

        /// <summary>
        /// Helper method to get the name of the eastbound longitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string EastBLName
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "eastBL" : "EastBL");
            }
        }

        /// <summary>
        /// Helper method to get the name of the southbound latitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string SouthBLName
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "southBL" : "SouthBL");
            }
        }

        /// <summary>
        /// Helper method to get the name of the northbound latitude element, which changed in DDMS 4.0.1.
        /// </summary>
        private string NorthBLName
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "northBL" : "NorthBL");
            }
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "boundingBox." + WestBLName, Convert.ToString(TEST_WEST)));
            text.Append(BuildOutput(isHTML, "boundingBox." + EastBLName, Convert.ToString(TEST_EAST)));
            text.Append(BuildOutput(isHTML, "boundingBox." + SouthBLName, Convert.ToString(TEST_SOUTH)));
            text.Append(BuildOutput(isHTML, "boundingBox." + NorthBLName, Convert.ToString(TEST_NORTH)));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:boundingBox ").Append(XmlnsDDMS).Append(">\n\t");
            xml.Append("<ddms:").Append(WestBLName).Append(">").Append(TEST_WEST).Append("</ddms:").Append(WestBLName).Append(">\n\t");
            xml.Append("<ddms:").Append(EastBLName).Append(">").Append(TEST_EAST).Append("</ddms:").Append(EastBLName).Append(">\n\t");
            xml.Append("<ddms:").Append(SouthBLName).Append(">").Append(TEST_SOUTH).Append("</ddms:").Append(SouthBLName).Append(">\n\t");
            xml.Append("<ddms:").Append(NorthBLName).Append(">").Append(TEST_NORTH).Append("</ddms:").Append(NorthBLName).Append(">\n");
            xml.Append("</ddms:boundingBox>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        /// <summary>
        /// Helper method to create a XOM element that can be used to test element constructors
        /// </summary>
        /// <param name="west"> westBL </param>
        /// <param name="east"> eastBL </param>
        /// <param name="south"> southBL </param>
        /// <param name="north"> northBL </param>
        /// <returns> XElement </returns>
        private XElement BuildComponentElement(string west, string east, string south, string north)
        {
            XElement element = Util.BuildDDMSElement(BoundingBox.GetName(DDMSVersion.CurrentVersion), null);
            element.Add(Util.BuildDDMSElement(WestBLName, Convert.ToString(west)));
            element.Add(Util.BuildDDMSElement(EastBLName, Convert.ToString(east)));
            element.Add(Util.BuildDDMSElement(SouthBLName, Convert.ToString(south)));
            element.Add(Util.BuildDDMSElement(NorthBLName, Convert.ToString(north)));
            return (element);
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, BoundingBox.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing values
                XElement element = Util.BuildDDMSElement(BoundingBox.GetName(version), null);
                GetInstance("westbound longitude is required.", element);

                // Not Double
                element = BuildComponentElement("west", Convert.ToString(TEST_EAST), Convert.ToString(TEST_SOUTH), Convert.ToString(TEST_NORTH));
                GetInstance("westbound longitude is required.", element);

                // Longitude too small
                element = BuildComponentElement("-181", Convert.ToString(TEST_EAST), Convert.ToString(TEST_SOUTH), Convert.ToString(TEST_NORTH));
                GetInstance("A longitude value must be between", element);

                // Longitude too big
                element = BuildComponentElement("181", Convert.ToString(TEST_EAST), Convert.ToString(TEST_SOUTH), Convert.ToString(TEST_NORTH));
                GetInstance("A longitude value must be between", element);

                // Latitude too small
                element = BuildComponentElement(Convert.ToString(TEST_WEST), Convert.ToString(TEST_EAST), "-91", Convert.ToString(TEST_NORTH));
                GetInstance("A latitude value must be between", element);

                // Latitude too big
                element = BuildComponentElement(Convert.ToString(TEST_WEST), Convert.ToString(TEST_EAST), "91", Convert.ToString(TEST_NORTH));
                GetInstance("A latitude value must be between", element);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_NorthboundLatitudeValiation()
        {
            // Issue #65
            GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, TEST_SOUTH, -91);
            GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, TEST_SOUTH, 91);
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Longitude too small
                GetInstance("A longitude value must be between", -181, TEST_EAST, TEST_SOUTH, TEST_NORTH);

                // Longitude too big
                GetInstance("A longitude value must be between", 181, TEST_EAST, TEST_SOUTH, TEST_NORTH);

                // Latitude too small
                GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, -91, TEST_NORTH);

                // Latitude too big
                GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, 91, TEST_NORTH);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                BoundingBox dataComponent = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                BoundingBox dataComponent = GetInstance(SUCCESS, 10, TEST_EAST, TEST_SOUTH, TEST_NORTH);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_WEST, 10, TEST_SOUTH, TEST_NORTH);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, 10, TEST_NORTH);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, 10);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_DoubleEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(component.WestBL, Convert.ToDouble(TEST_WEST));
                Assert.AreEqual(component.EastBL, Convert.ToDouble(TEST_EAST));
                Assert.AreEqual(component.SouthBL, Convert.ToDouble(TEST_SOUTH));
                Assert.AreEqual(component.NorthBL, Convert.ToDouble(TEST_NORTH));
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
                BoundingBox.Builder builder = new BoundingBox.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                BoundingBox.Builder builder = new BoundingBox.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                //TODO - Figure out how to test this
                //builder.WestBL = TEST_WEST;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void Summary_BoundingBox_BuilderValidation()
        {
            //TODO - Figure out how to test this
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    BoundingBox.Builder builder = new BoundingBox.Builder();
            //    builder.EastBL = Convert.ToDouble(TEST_EAST);
            //    try
            //    {
            //        builder.Commit();
            //        Assert.Fail("Builder allowed invalid data.");
            //    }
            //    catch (InvalidDDMSException e)
            //    {
            //        ExpectMessage(e, "A ddms:boundingBox requires");
            //    }
            //    builder.WestBL = Convert.ToDouble(TEST_WEST);
            //    builder.NorthBL = Convert.ToDouble(TEST_NORTH);
            //    builder.SouthBL = Convert.ToDouble(TEST_SOUTH);
            //    builder.Commit();
            //}
        }
    }

}