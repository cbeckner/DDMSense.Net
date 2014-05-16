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
namespace DDMSense.Test.DDMS.FormatElements
{


    using DDMSense.DDMS;
    using DDMSense.DDMS.FormatElements;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:extent elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class ExtentTest : AbstractBaseTestCase
    {

        private const string TEST_QUALIFIER = "sizeBytes";
        private const string TEST_VALUE = "75000";

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtentTest()
            : base("extent.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Extent Fixture
        {
            get
            {
                try
                {
                    return (new Extent(TEST_QUALIFIER, TEST_VALUE));
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
        private Extent GetInstance(string message, XElement element)
        {
            bool expectFailure = !string.IsNullOrEmpty(message);
            Extent component = null;
            try
            {
                component = new Extent(element);
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
        /// <param name="qualifier"> the qualifier value </param>
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private Extent GetInstance(string message, string qualifier, string value)
        {
            bool expectFailure = !string.IsNullOrEmpty(message);
            Extent component = null;
            try
            {
                component = new Extent(qualifier, value);
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
            text.Append(BuildOutput(isHTML, "extent.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "extent.value", TEST_VALUE));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:extent ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
                return (xml.ToString());
            }
        }

        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Extent.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Extent.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);

                // No optional fields
                GetInstance(SUCCESS, "", "");
            }
        }

        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing qualifier
                XElement element = Util.BuildDDMSElement(Extent.GetName(version), null);
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);

                // Qualifier not URI
                element = Util.BuildDDMSElement(Extent.GetName(version), null);
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                Util.AddDDMSAttribute(element, "qualifier", INVALID_URI);
                GetInstance("Invalid URI", element);
            }
        }

        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing qualifier
                GetInstance("qualifier attribute is required.", null, TEST_VALUE);

                // Qualifier not URI
                GetInstance("Invalid URI", INVALID_URI, TEST_VALUE);
            }
        }

        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string extentName = Extent.GetName(version);

                // No warnings
                Extent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count);

                // Qualifier without value
                XElement element = Util.BuildDDMSElement(extentName, null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count);

                string text = "A qualifier has been set without an accompanying value attribute.";
                string locator = "ddms:extent";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Neither attribute
                element = Util.BuildDDMSElement(extentName, null);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count);
                text = "A completely empty ddms:extent element was found.";
                locator = "ddms:extent";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Extent dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Extent dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Extent.Builder builder = new Extent.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent.Builder builder = new Extent.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);

            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Extent.Builder builder = new Extent.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "qualifier attribute is required.");
                }
                builder.Qualifier = TEST_QUALIFIER;
                builder.Commit();
            }
        }
    }

}