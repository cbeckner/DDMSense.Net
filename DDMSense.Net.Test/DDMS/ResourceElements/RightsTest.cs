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
namespace DDMSense.Test.DDMS.ResourceElements
{


    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to ddms:rights elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class RightsTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public RightsTest()
            : base("rights.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Rights Fixture
        {
            get
            {
                try
                {
                    return (new Rights(true, true, true));
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
        private Rights GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Rights component = null;
            try
            {
                component = new Rights(element);
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
        /// <param name="privacyAct"> the value for the privacyAct attribute </param>
        /// <param name="intellectualProperty"> the value for the intellectualProperty attribute </param>
        /// <param name="copyright"> the value for the copyright attribute </param>
        /// <returns> a valid object </returns>
        private Rights GetInstance(string message, bool privacyAct, bool intellectualProperty, bool copyright)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Rights component = null;
            try
            {
                component = new Rights(privacyAct, intellectualProperty, copyright);
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
            text.Append(BuildOutput(isHTML, "rights.privacyAct", "true"));
            text.Append(BuildOutput(isHTML, "rights.intellectualProperty", "true"));
            text.Append(BuildOutput(isHTML, "rights.copyright", "false"));
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
                xml.Append("<ddms:rights ").Append(XmlnsDDMS).Append(" ddms:privacyAct=\"true\" ddms:intellectualProperty=\"true\" ddms:copyright=\"false\" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                Rights component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(Rights.GetName(version), component.Name);
                Assert.AreEqual(PropertyReader.GetPrefix("ddms"), component.Prefix);
                Assert.AreEqual(PropertyReader.GetPrefix("ddms") + ":" + Rights.GetName(version), component.QualifiedName);

                // Wrong name/namespace
                XElement element = Util.BuildDDMSElement("wrongName", null);
                GetInstance(WRONG_NAME_MESSAGE, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Rights.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, true, true, true);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Rights component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Rights elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights dataComponent = GetInstance(SUCCESS, true, true, false);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Rights elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights dataComponent = GetInstance(SUCCESS, false, true, false);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, true, false, false);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, true, true, true);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Rights elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Language wrongComponent = new Language("qualifier", "value");
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Rights component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, true, true, false);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Rights component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, true, true, false);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_DefaultValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = Util.BuildDDMSElement(Rights.GetName(version), null);
                Rights component = GetInstance(SUCCESS, element);
                Assert.IsFalse(component.PrivacyAct);
                Assert.IsFalse(component.IntellectualProperty);
                Assert.IsFalse(component.Copyright);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Rights component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights.Builder builder = new Rights.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Rights.Builder builder = new Rights.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Copyright = true;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Rights_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Default values (at least 1 value must be explicit to prevent a null commit)
                Rights.Builder builder = new Rights.Builder();
                builder.PrivacyAct = true;
                Assert.IsFalse(((Rights)builder.Commit()).IntellectualProperty);
                Assert.IsFalse(((Rights)builder.Commit()).Copyright);
                builder = new Rights.Builder();
                builder.IntellectualProperty = true;
                Assert.IsFalse(((Rights)builder.Commit()).PrivacyAct);
            }
        }
    }

}