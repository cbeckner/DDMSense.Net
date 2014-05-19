using System;
using System.Text;
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to ddms:verticalExtent elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class VerticalExtentTest : AbstractBaseTestCase
    {

        private const string TEST_UOM = "Meter";
        private const string TEST_DATUM = "AGL";
        private const double TEST_MIN = 0.1;
        private const double TEST_MAX = 100.1;

        /// <summary>
        /// Constructor
        /// </summary>
        public VerticalExtentTest()
            : base("verticalExtent.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static VerticalExtent Fixture
        {
            get
            {
                try
                {
                    return (new VerticalExtent(1.1, 2.2, "Meter", "HAE"));
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
        private VerticalExtent GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            VerticalExtent component = null;
            try
            {
                component = new VerticalExtent(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="minVerticalExtent"> the minimum (required) </param>
        /// <param name="maxVerticalExtent"> the maximum (required) </param>
        /// <param name="unitOfMeasure"> the unit of measure (required) </param>
        /// <param name="datum"> the datum (required) </param>
        /// <returns> a valid object </returns>
        private VerticalExtent GetInstance(string message, double minVerticalExtent, double maxVerticalExtent, string unitOfMeasure, string datum)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            VerticalExtent component = null;
            try
            {
                component = new VerticalExtent(minVerticalExtent, maxVerticalExtent, unitOfMeasure, datum);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "verticalExtent.unitOfMeasure", Convert.ToString(TEST_UOM)));
            text.Append(BuildOutput(isHTML, "verticalExtent.datum", Convert.ToString(TEST_DATUM)));
            text.Append(BuildOutput(isHTML, "verticalExtent.minimum", Convert.ToString(TEST_MIN)));
            text.Append(BuildOutput(isHTML, "verticalExtent.maximum", Convert.ToString(TEST_MAX)));
            return (text.ToString());
        }

        /// <summary>
        /// Helper method to get the correct-case of the minVerticalExtent eleemnt.
        /// </summary>
        private string MinVerticalExtentName
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "minVerticalExtent" : "MinVerticalExtent");
            }
        }

        /// <summary>
        /// Helper method to get the correct-case of the maxVerticalExtent eleemnt.
        /// </summary>
        private string MaxVerticalExtentName
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "maxVerticalExtent" : "MaxVerticalExtent");
            }
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:verticalExtent ").Append(XmlnsDDMS).Append(" ");
            xml.Append("ddms:unitOfMeasure=\"").Append(TEST_UOM).Append("\" ");
            xml.Append("ddms:datum=\"").Append(TEST_DATUM).Append("\">\n\t");
            xml.Append("<ddms:").Append(MinVerticalExtentName).Append(">").Append(TEST_MIN).Append("</ddms:").Append(MinVerticalExtentName).Append(">\n\t");
            xml.Append("<ddms:").Append(MaxVerticalExtentName).Append(">").Append(TEST_MAX).Append("</ddms:").Append(MaxVerticalExtentName).Append(">\n");
            xml.Append("</ddms:verticalExtent>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, VerticalExtent.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string extentName = VerticalExtent.GetName(version);
                // Missing UOM
                XElement element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance("unitOfMeasure is required.", element);

                // Invalid UOM
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", "furlong");
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance("The length measure type must be one of", element);

                // Missing Datum
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance("datum is required.", element);

                // Invalid Datum
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", "PDQ");
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance("The vertical datum type must be one of", element);

                // Missing MinVerticalExtent
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance(MinVerticalExtentName + " is required.", element);

                // Missing MaxVerticalExtent
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                GetInstance(MaxVerticalExtentName + " is required.", element);

                // MinVerticalExtent UOM doesn't match parent
                XElement minElement = Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN));
                Util.AddDDMSAttribute(minElement, "unitOfMeasure", "Inch");
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(minElement);
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance("The unitOfMeasure on the", element);

                // MinVerticalExtent Datum doesn't match parent
                minElement = Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN));
                Util.AddDDMSAttribute(minElement, "datum", "PDQ");
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(minElement);
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
                GetInstance("The datum on the", element);

                // MaxVerticalExtent UOM doesn't match parent
                XElement maxElement = Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX));
                Util.AddDDMSAttribute(maxElement, "unitOfMeasure", "Inch");
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(maxElement);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                GetInstance("The unitOfMeasure on the", element);

                // MaxVerticalExtent Datum doesn't match parent
                maxElement = Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX));
                Util.AddDDMSAttribute(maxElement, "datum", "PDQ");
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(maxElement);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                GetInstance("The datum on the", element);

                // MinVerticalExtent is not less than MaxVerticalExtent
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MAX)));
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MIN)));
                GetInstance("Minimum vertical extent must be less", element);

                // Not Double
                element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
                Util.AddDDMSAttribute(element, "datum", TEST_DATUM);
                element.Add(Util.BuildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
                element.Add(Util.BuildDDMSElement(MaxVerticalExtentName, Convert.ToString("ground-level")));
                GetInstance(MaxVerticalExtentName + " is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing UOM
                GetInstance("unitOfMeasure is required.", TEST_MIN, TEST_MAX, null, TEST_DATUM);

                // Invalid UOM
                GetInstance("The length measure type must be one of", TEST_MIN, TEST_MAX, "furlong", TEST_DATUM);

                // Missing Datum
                GetInstance("datum is required.", TEST_MIN, TEST_MAX, TEST_UOM, null);

                // Invalid Datum
                GetInstance("The vertical datum type must be one of", TEST_MIN, TEST_MAX, TEST_UOM, "PDQ");

                // MinVerticalExtent is not less than MaxVerticalExtent
                GetInstance("Minimum vertical extent must be less", TEST_MAX, TEST_MIN, TEST_UOM, TEST_DATUM);
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                VerticalExtent dataComponent = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                VerticalExtent dataComponent = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, "Inch", TEST_DATUM);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, "HAE");
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, 1, TEST_MAX, TEST_UOM, TEST_DATUM);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_MIN, 101, TEST_UOM, TEST_DATUM);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_DoubleEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(component.MinVerticalExtent, Convert.ToDouble(TEST_MIN));
                Assert.AreEqual(component.MaxVerticalExtent, Convert.ToDouble(TEST_MAX));
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                VerticalExtent.Builder builder = new VerticalExtent.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                VerticalExtent.Builder builder = new VerticalExtent.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Datum = TEST_DATUM;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void Summary_VerticalExtent_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                VerticalExtent.Builder builder = new VerticalExtent.Builder();
                builder.UnitOfMeasure = TEST_UOM;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "A ddms:verticalExtent requires");
                }
                builder.Datum = TEST_DATUM;
                builder.MaxVerticalExtent = TEST_MAX;
                builder.MinVerticalExtent = TEST_MIN;
            }
        }
    }

}