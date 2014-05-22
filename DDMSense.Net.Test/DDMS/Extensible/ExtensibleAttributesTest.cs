using System.Collections;
using System.Collections.Generic;
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
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to the extensible attributes themselves. How they interact with parent classes is tested in those
    /// classes. </para>
    ///
    /// @author Brian Uri!
    /// @since 1.1.0
    /// </summary>
    [TestClass]
    public class ExtensibleAttributesTest : AbstractBaseTestCase
    {
        private const string TEST_NAMESPACE = "http://ddmsence.urizone.net/";
        private const string TEST_NAMESPACE_PREFIX = "ddmsence";
        private const string TEST_NAME = "relevance";

        private static readonly XAttribute TEST_ATTRIBUTE = new XAttribute(XNamespace.Get(TEST_NAMESPACE) + TEST_NAME, "95");

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtensibleAttributesTest()
            : base(null)
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static ExtensibleAttributes Fixture
        {
            get
            {
                try
                {
                    List<XAttribute> attributes = new List<XAttribute>();
                    attributes.Add(new XAttribute(TEST_ATTRIBUTE));
                    return (new ExtensibleAttributes(attributes));
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
        private ExtensibleAttributes GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ExtensibleAttributes attributes = null;
            try
            {
                attributes = new ExtensibleAttributes(element);
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
        /// <param name="attributes"> a list of attributes (optional) </param>
        /// <returns> a valid object </returns>
        private ExtensibleAttributes GetInstance(string message, List<XAttribute> attributes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ExtensibleAttributes exAttributes = null;
            try
            {
                exAttributes = new ExtensibleAttributes(attributes);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (exAttributes);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, TEST_NAMESPACE_PREFIX + "." + TEST_NAME, "95"));
            return (text.ToString());
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                GetInstance(SUCCESS, element);

                // No optional fields
                element = (new Keyword("testValue", null)).ElementCopy;
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                List<XAttribute> attributes = new List<XAttribute>();
                attributes.Add(new XAttribute(TEST_ATTRIBUTE));
                GetInstance(SUCCESS, attributes);

                // No optional fields
                GetInstance(SUCCESS, (List<XAttribute>)null);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No invalid cases right now, since reserved names are silently skipped.
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No invalid cases right now. The validation occurs when the attributes are added to some component.
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                GetInstance(SUCCESS, element);
                ExtensibleAttributes component = GetInstance(SUCCESS, element);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);

                List<XAttribute> attributes = new List<XAttribute>();
                attributes.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes dataAttributes = GetInstance(SUCCESS, attributes);

                Assert.AreEqual(elementAttributes, dataAttributes);
                Assert.AreEqual(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(XNamespace.Xmlns + TEST_NAMESPACE_PREFIX, TEST_NAMESPACE));
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);

                //Build an element with our test attribute.
                //This lets us extract an XAttribute that carries its namespace properties.
                string essenceNs = "http://essence/";
                string essenceNsPrefix = "essence";
                string essenceAttrName = "confidence";
                XAttribute essence = new XAttribute(XNamespace.Get(essenceNs) + essenceAttrName, "test");
                element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(XNamespace.Xmlns + essenceNsPrefix, essenceNs));
                element.Add(essence);

                List<XAttribute> attributes = new List<XAttribute>();
                attributes.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(essenceNs) + essenceAttrName).FirstOrDefault());

                ExtensibleAttributes dataAttributes = GetInstance(SUCCESS, attributes);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, (List<XAttribute>)null);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementAttributes.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(XNamespace.Xmlns + TEST_NAMESPACE_PREFIX, TEST_NAMESPACE));
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
                Assert.AreEqual(GetExpectedOutput(true), elementAttributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false), elementAttributes.GetOutput(false, ""));

                List<XAttribute> attributes = new List<XAttribute>();
                attributes.Add(element.Attributes().Where(a => a.Name == XNamespace.Get(TEST_NAMESPACE) + TEST_NAME).FirstOrDefault());
                elementAttributes = GetInstance(SUCCESS, attributes);
                Assert.AreEqual(GetExpectedOutput(true), elementAttributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false), elementAttributes.GetOutput(false, ""));
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_AddTo()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                ExtensibleAttributes component = Fixture;

                XElement element = Util.BuildDDMSElement("sample", null, false);
                component.AddTo(element);

                ExtensibleAttributes output = new ExtensibleAttributes(element);
                Assert.AreEqual(component, output);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_GetNonNull()
        {
            ExtensibleAttributes component = new ExtensibleAttributes((List<XAttribute>)null);
            ExtensibleAttributes output = ExtensibleAttributes.GetNonNullInstance(null);
            Assert.AreEqual(component, output);

            output = ExtensibleAttributes.GetNonNullInstance(Fixture);
            Assert.AreEqual(Fixture, output);
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_IsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
                Assert.IsFalse(elementAttributes.Empty);

                ExtensibleAttributes dataAttributes = GetInstance(SUCCESS, (List<XAttribute>)null);
                Assert.IsTrue(dataAttributes.Empty);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XElement element = (new Keyword("testValue", null)).ElementCopy;
                element.Add(new XAttribute(TEST_ATTRIBUTE));
                ExtensibleAttributes component = GetInstance(SUCCESS, element);
                ExtensibleAttributes.Builder builder = new ExtensibleAttributes.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ExtensibleAttributes.Builder builder = new ExtensibleAttributes.Builder();
                Assert.IsNotNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Attributes.Add(new ExtensibleAttributes.AttributeBuilder(TEST_ATTRIBUTE));
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Extensible_ExtensibleAttributes_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No invalid cases right now, because validation cannot occur until these attributes are attached to something.
            }
        }
    }
}