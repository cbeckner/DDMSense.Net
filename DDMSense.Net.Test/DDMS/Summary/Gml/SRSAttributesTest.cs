using System;
using System.Collections.Generic;
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
namespace DDMSense.Test.DDMS.Summary.Gml
{
    using DDMSense.DDMS.Summary.Gml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to the SRS attributes on gml: elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class SRSAttributesTest : AbstractBaseTestCase
    {

        protected internal const string TEST_SRS_NAME = "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D";
        protected internal const int TEST_SRS_DIMENSION = 10;
        protected internal static readonly List<string> TEST_AXIS_LABELS = new List<string>() {"A","B","C"};
        protected internal static readonly List<string> TEST_UOM_LABELS = new List<string>() {"Meter","Meter","Meter"};

        /// <summary>
        /// Constructor
        /// </summary>
        public SRSAttributesTest()
            : base(null)
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static SRSAttributes Fixture
        {
            get
            {
                try
                {
                    return (new SRSAttributes(TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS));
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
        private SRSAttributes GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SRSAttributes attributes = null;
            try
            {
                attributes = new SRSAttributes(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (attributes);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="srsName"> the srsName (optional) </param>
        /// <param name="srsDimension"> the srsDimension (optional) </param>
        /// <param name="axisLabels"> the axis labels (optional, but should be omitted if no srsName is set) </param>
        /// <param name="uomLabels"> the labels for UOM (required when axisLabels is set) </param>
        /// <returns> a valid object </returns>
        private SRSAttributes GetInstance(string message, string srsName, int? srsDimension, List<string> axisLabels, List<string> uomLabels)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SRSAttributes attributes = null;
            try
            {
                attributes = new SRSAttributes(srsName, srsDimension, axisLabels, uomLabels);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (attributes);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "srsName", TEST_SRS_NAME));
            text.Append(BuildOutput(isHTML, "srsDimension", Convert.ToString(TEST_SRS_DIMENSION)));
            text.Append(BuildOutput(isHTML, "axisLabels", Util.GetXsList(TEST_AXIS_LABELS)));
            text.Append(BuildOutput(isHTML, "uomLabels", Util.GetXsList(TEST_UOM_LABELS)));
            return (text.ToString());
        }

        /// <summary>
        /// Helper method to add srs attributes to a XOM element. The element is not validated.
        /// </summary>
        /// <param name="element"> element </param>
        /// <param name="srsName"> the srsName (optional) </param>
        /// <param name="srsDimension"> the srsDimension (optional) </param>
        /// <param name="axisLabels"> the axis labels (optional, but should be omitted if no srsName is set) </param>
        /// <param name="uomLabels"> the labels for UOM (required when axisLabels is set) </param>
        private void AddAttributes(XElement element, string srsName, int? srsDimension, string axisLabels, string uomLabels)
        {
            Util.AddAttribute(element, SRSAttributes.NO_PREFIX, "srsName", SRSAttributes.NO_NAMESPACE, srsName);
            if (srsDimension != null)
            {
                Util.AddAttribute(element, SRSAttributes.NO_PREFIX, "srsDimension", SRSAttributes.NO_NAMESPACE, Convert.ToString(srsDimension));
            }
            Util.AddAttribute(element, SRSAttributes.NO_PREFIX, "axisLabels", SRSAttributes.NO_NAMESPACE, axisLabels);
            Util.AddAttribute(element, SRSAttributes.NO_PREFIX, "uomLabels", SRSAttributes.NO_NAMESPACE, uomLabels);
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                GetInstance(SUCCESS, element);

                // No optional fields
                element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

                // No optional fields
                GetInstance(SUCCESS, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // srsName not a URI
                XElement element = Util.BuildDDMSElement(Position.GetName(version), null);
                AddAttributes(element, INVALID_URI, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                GetInstance("Invalid URI", element);

                // axisLabels without srsName
                element = Util.BuildDDMSElement(Position.GetName(version), null);
                AddAttributes(element, null, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                GetInstance("The axisLabels attribute can only be used", element);

                // uomLabels without axisLabels
                element = Util.BuildDDMSElement(Position.GetName(version), null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, null, Util.GetXsList(TEST_UOM_LABELS));
                GetInstance("The uomLabels attribute can only be used", element);

                // Non-NCNames in axisLabels
                List<string> newLabels = new List<string>(TEST_AXIS_LABELS);
                newLabels.Add("1TEST");
                element = Util.BuildDDMSElement(Position.GetName(version), null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(newLabels), Util.GetXsList(TEST_UOM_LABELS));
                GetInstance("\"1TEST\" is not a valid NCName.", element);

                // Non-NCNames in uomLabels
                newLabels = new List<string>(TEST_UOM_LABELS);
                newLabels.Add("TEST:TEST");
                element = Util.BuildDDMSElement(Position.GetName(version), null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(newLabels));
                GetInstance("\"TEST:TEST\" is not a valid NCName.", element);

                // Dimension is a positive integer
                element = Util.BuildDDMSElement(Position.GetName(version), null);
                AddAttributes(element, TEST_SRS_NAME, Convert.ToInt32(-1), null, Util.GetXsList(TEST_UOM_LABELS));
                GetInstance("The srsDimension must be a positive integer.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // srsName not a URI
                GetInstance("Invalid URI", INVALID_URI, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

                // axisLabels without srsName
                GetInstance("The axisLabels attribute can only be used", null, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

                // uomLabels without axisLabels
                GetInstance("The uomLabels attribute can only be used", TEST_SRS_NAME, TEST_SRS_DIMENSION, null, TEST_UOM_LABELS);

                // Non-NCNames in axisLabels
                List<string> newLabels = new List<string>(TEST_AXIS_LABELS);
                newLabels.Add("TEST:TEST");
                GetInstance("\"TEST:TEST\" is not a valid NCName.", TEST_SRS_NAME, TEST_SRS_DIMENSION, newLabels, TEST_UOM_LABELS);

                // Non-NCNames in uomLabels
                newLabels = new List<string>(TEST_UOM_LABELS);
                newLabels.Add("TEST:TEST");
                GetInstance("\"TEST:TEST\" is not a valid NCName.", TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, newLabels);

                // Dimension is a positive integer
                GetInstance("The srsDimension must be a positive integer.", TEST_SRS_NAME, Convert.ToInt32(-1), TEST_AXIS_LABELS, TEST_UOM_LABELS);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                SRSAttributes component = GetInstance(SUCCESS, element);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                SRSAttributes elementAttributes = GetInstance(SUCCESS, element);
                SRSAttributes dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

                Assert.AreEqual(elementAttributes, dataAttributes);
                Assert.AreEqual(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                SRSAttributes elementAttributes = GetInstance(SUCCESS, element);
                SRSAttributes dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, null, TEST_AXIS_LABELS, TEST_UOM_LABELS);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                List<string> newLabels = new List<string>(TEST_AXIS_LABELS);
                newLabels.Add("NewLabel");
                dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, newLabels, TEST_UOM_LABELS);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, null);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                SRSAttributes attributes = new SRSAttributes(element);
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(attributes.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Position.GetName(version), version.GmlNamespace, null);
                AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.GetXsList(TEST_AXIS_LABELS), Util.GetXsList(TEST_UOM_LABELS));
                SRSAttributes attributes = new SRSAttributes(element);
                Assert.AreEqual(GetExpectedOutput(true), attributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false), attributes.GetOutput(false, ""));

                SRSAttributes dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);
                Assert.AreEqual(GetExpectedOutput(true), dataAttributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false), dataAttributes.GetOutput(false, ""));
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_AddTo()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SRSAttributes component = Fixture;

                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), "sample", version.GmlNamespace, null);
                component.AddTo(element);
                SRSAttributes output = new SRSAttributes(element);
                Assert.AreEqual(component, output);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_GetNonNull()
        {
            SRSAttributes component = new SRSAttributes(null, null, null, null);
            SRSAttributes output = SRSAttributes.GetNonNullInstance(null);
            Assert.AreEqual(component, output);

            output = SRSAttributes.GetNonNullInstance(Fixture);
            Assert.AreEqual(Fixture, output);
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_WrongVersionAttributes()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            SRSAttributes attr = Fixture;
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new Position(PositionTest.TEST_COORDS, attr);
                Assert.Fail("Allowed different versions.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "These attributes cannot decorate");
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SRSAttributes component = Fixture;
                SRSAttributes.Builder builder = new SRSAttributes.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SRSAttributes.Builder builder = new SRSAttributes.Builder();
                Assert.IsNotNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.SrsName = TEST_SRS_NAME;
                Assert.IsFalse(builder.Empty);

                builder = new SRSAttributes.Builder();
                builder.SrsDimension = TEST_SRS_DIMENSION;
                Assert.IsFalse(builder.Empty);

                builder = new SRSAttributes.Builder();
                builder.UomLabels.Add(null);
                builder.UomLabels.Add("label");
                Assert.IsFalse(builder.Empty);

                builder = new SRSAttributes.Builder();
                builder.AxisLabels.Add(null);
                builder.AxisLabels.Add("label");
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_SRSAttributes_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SRSAttributes.Builder builder = new SRSAttributes.Builder();
                builder.SrsDimension = Convert.ToInt32(-1);
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The srsDimension must be a positive integer.");
                }
                builder.SrsDimension = Convert.ToInt32(1);
                builder.Commit();
            }
        }
    }

}