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

namespace DDMSense.Test.DDMS.Extensible
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.Extensible;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to extensible layer elements </para>
    ///
    /// @author Brian Uri!
    /// @since 1.1.0
    /// </summary>
    [TestClass]
    public class ExtensibleElementTest : AbstractBaseTestCase
    {
        private const string TEST_NAME = "extension";
        private const string TEST_PREFIX = "ddmsence";
        private const string TEST_NAMESPACE = "http://ddmsence.urizone.net/";

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtensibleElementTest()
            : base(null)
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static XElement FixtureElement
        {
            get
            {
                return (Util.BuildElement(TEST_PREFIX, TEST_NAME, TEST_NAMESPACE, "This is an extensible element."));
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private ExtensibleElement GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ExtensibleElement component = null;
            try
            {
                component = new ExtensibleElement(element);
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
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddmsence:extension xmlns:ddmsence=\"http://ddmsence.urizone.net/\">").Append("This is an extensible element.</ddmsence:extension>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), TEST_PREFIX, TEST_NAME);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, FixtureElement);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Using the DDMS namespace
                XElement element = Util.BuildDDMSElement("name", null);
                GetInstance("Extensible elements cannot be defined in the DDMS namespace.", element);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                ExtensibleElement elementComponent = GetInstance(SUCCESS, FixtureElement);

                XElement element = Util.BuildElement(TEST_PREFIX, TEST_NAME, TEST_NAMESPACE, "This is an extensible element.");
                ExtensibleElement dataComponent = GetInstance(SUCCESS, element);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                ExtensibleElement elementComponent = GetInstance(SUCCESS, FixtureElement);
                XElement element = Util.BuildElement(TEST_PREFIX, "newName", TEST_NAMESPACE, "This is an extensible element.");
                ExtensibleElement dataComponent = GetInstance(SUCCESS, element);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual("", component.ToHTML());
                Assert.AreEqual("", component.ToText());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
                ExtensibleElement.Builder builder = new ExtensibleElement.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                builder = new ExtensibleElement.Builder();
                builder.Xml = FixtureElement.ToString();
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleElement_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ExtensibleElement.Builder builder = new ExtensibleElement.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Xml = "<test/>";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDDMSException), "Could not create a valid element")]
        public virtual void Extensible_ExtensibleElement_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ExtensibleElement.Builder builder = new ExtensibleElement.Builder();
                builder.Xml = "InvalidXml";
                builder.Commit();

                builder.Xml = ExpectedXMLOutput;
                builder.Commit();
            }
        }
    }
}