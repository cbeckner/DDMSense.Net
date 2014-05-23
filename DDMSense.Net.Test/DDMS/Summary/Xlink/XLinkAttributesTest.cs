using System;
using System.Linq;
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

namespace DDMSense.Test.DDMS.Summary.Xlink
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary.Xlink;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to the XLINK attributes on ddms:link and ddms:taskID elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class XLinkAttributesTest : AbstractBaseTestCase
    {
        private const string TEST_HREF = "http://en.wikipedia.org/wiki/Tank";
        private const string TEST_ROLE = "tank";
        private const string TEST_TITLE = "Tank Page";
        private const string TEST_LABEL = "tank";
        private const string TEST_ARCROLE = "arcrole";
        private const string TEST_SHOW = "new";
        private const string TEST_ACTUATE = "onLoad";

        /// <summary>
        /// Constructor
        /// </summary>
        public XLinkAttributesTest()
            : base(null)
        {
        }

        /// <summary>
        /// Returns a fixture object for testing. The type will be "locator".
        /// </summary>
        public static XLinkAttributes LocatorFixture
        {
            get
            {
                try
                {
                    return (new XLinkAttributes(TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing. The type will be "simple".
        /// </summary>
        public static XLinkAttributes SimpleFixture
        {
            get
            {
                try
                {
                    return (new XLinkAttributes(TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing. The type will be "resource".
        /// </summary>
        public static XLinkAttributes ResourceFixture
        {
            get
            {
                try
                {
                    return (new XLinkAttributes(TEST_ROLE, TEST_TITLE, TEST_LABEL));
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
        private XLinkAttributes GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            XLinkAttributes attributes = null;
            try
            {
                attributes = new XLinkAttributes(element);
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
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="label"> the name of the link (optional) </param>
        /// <returns> a valid object </returns>
        private XLinkAttributes GetInstance(string message, string role, string title, string label)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            XLinkAttributes attributes = null;
            try
            {
                attributes = new XLinkAttributes(role, title, label);
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
        /// <param name="href"> the link href (optional) </param>
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="label"> the name of the link (optional) </param>
        /// <returns> a valid object </returns>
        private XLinkAttributes GetInstance(string message, string href, string role, string title, string label)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            XLinkAttributes attributes = null;
            try
            {
                attributes = new XLinkAttributes(href, role, title, label);
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
        /// <param name="href"> the link href (optional) </param>
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="arcrole"> the arcrole attribute (optional) </param>
        /// <param name="show"> the show token (optional) </param>
        /// <param name="actuate"> the actuate token (optional) </param>
        /// <returns> a valid object </returns>
        private XLinkAttributes GetInstance(string message, string href, string role, string title, string arcrole, string show, string actuate)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            XLinkAttributes attributes = null;
            try
            {
                attributes = new XLinkAttributes(href, role, title, arcrole, show, actuate);
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
        private string GetExpectedOutput(bool isHTML, string type)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "type", type));
            if (!"resource".Equals(type))
            {
                text.Append(BuildOutput(isHTML, "href", TEST_HREF));
            }
            text.Append(BuildOutput(isHTML, "role", TEST_ROLE));
            text.Append(BuildOutput(isHTML, "title", TEST_TITLE));
            if (!"simple".Equals(type))
            {
                text.Append(BuildOutput(isHTML, "label", TEST_LABEL));
            }
            if ("simple".Equals(type))
            {
                text.Append(BuildOutput(isHTML, "arcrole", TEST_ARCROLE));
                text.Append(BuildOutput(isHTML, "show", TEST_SHOW));
                text.Append(BuildOutput(isHTML, "actuate", TEST_ACTUATE));
            }
            return (text.ToString());
        }

        /// <summary>
        /// Helper method to add attributes to a XOM element. The element is not validated.
        /// </summary>
        /// <param name="element"> element </param>
        /// <param name="href"> the link href (optional) </param>
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="arcrole"> the arcrole attribute (optional) </param>
        /// <param name="show"> the show token (optional) </param>
        /// <param name="actuate"> the actuate token (optional) </param>
        private void AddAttributes(XElement element, string href, string role, string title, string arcrole, string show, string actuate)
        {
            string xlinkPrefix = PropertyReader.GetPrefix("xlink");
            string xlinkNamespace = DDMSVersion.CurrentVersion.XlinkNamespace;
            Util.AddAttribute(element, xlinkPrefix, "type", xlinkNamespace, "simple");
            Util.AddAttribute(element, xlinkPrefix, "href", xlinkNamespace, href);
            Util.AddAttribute(element, xlinkPrefix, "role", xlinkNamespace, role);
            Util.AddAttribute(element, xlinkPrefix, "title", xlinkNamespace, title);
            Util.AddAttribute(element, xlinkPrefix, "arcrole", xlinkNamespace, arcrole);
            Util.AddAttribute(element, xlinkPrefix, "show", xlinkNamespace, show);
            Util.AddAttribute(element, xlinkPrefix, "actuate", xlinkNamespace, actuate);
        }

        /// <summary>
        /// Helper method to add attributes to a XOM element. The element is not validated.
        /// </summary>
        /// <param name="element"> element </param>
        /// <param name="href"> the link href (optional) </param>
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="label"> the name of the link (optional) </param>
        private void AddAttributes(XElement element, string href, string role, string title, string label)
        {
            string xlinkPrefix = PropertyReader.GetPrefix("xlink");
            string xlinkNamespace = DDMSVersion.CurrentVersion.XlinkNamespace;
            Util.AddAttribute(element, xlinkPrefix, "type", xlinkNamespace, "locator");
            Util.AddAttribute(element, xlinkPrefix, "href", xlinkNamespace, href);
            Util.AddAttribute(element, xlinkPrefix, "role", xlinkNamespace, role);
            Util.AddAttribute(element, xlinkPrefix, "title", xlinkNamespace, title);
            Util.AddAttribute(element, xlinkPrefix, "label", xlinkNamespace, label);
        }

        /// <summary>
        /// Helper method to add attributes to a XOM element. The element is not validated.
        /// </summary>
        /// <param name="element"> element </param>
        /// <param name="href"> the link href (optional) </param>
        /// <param name="role"> the role attribute (optional) </param>
        /// <param name="title"> the link title (optional) </param>
        /// <param name="label"> the name of the link (optional) </param>
        private void AddAttributes(XElement element, string role, string title, string label)
        {
            string xlinkPrefix = PropertyReader.GetPrefix("xlink");
            string xlinkNamespace = DDMSVersion.CurrentVersion.XlinkNamespace;
            Util.AddAttribute(element, xlinkPrefix, "type", xlinkNamespace, "resource");
            Util.AddAttribute(element, xlinkPrefix, "role", xlinkNamespace, role);
            Util.AddAttribute(element, xlinkPrefix, "title", xlinkNamespace, title);
            Util.AddAttribute(element, xlinkPrefix, "label", xlinkNamespace, label);
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields (locator)
                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                GetInstance(SUCCESS, element);

                // All fields (simple)
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                GetInstance(SUCCESS, element);

                // All fields (resource)
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                GetInstance(SUCCESS, element);

                // No optional fields (all)
                element = Util.BuildDDMSElement("link", null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields (locator)
                GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);

                // All fields (simple)
                GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);

                // All fields (resource)
                GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, TEST_LABEL);

                // No optional fields (all)
                GetInstance(SUCCESS, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // href is not valid URI
                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, INVALID_URI, null, null, null);
                GetInstance("Invalid URI", element);

                // role is not valid URI
                if (version.IsAtLeast("4.0.1"))
                {
                    element = Util.BuildDDMSElement("link", null);
                    AddAttributes(element, null, INVALID_URI, null, null);
                    GetInstance("Invalid URI", element);
                }

                // label is not valid NCName
                if (version.IsAtLeast("4.0.1"))
                {
                    element = Util.BuildDDMSElement("link", null);
                    AddAttributes(element, null, null, null, "ddms:prefix& GML");
                    GetInstance("The ':' character, hexadecimal value 0x3A, cannot be included in a name.", element);
                }

                // invalid arcrole
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, null, null, null, INVALID_URI, null, null);

                // invalid show
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, null, null, null, null, "notInTheTokenList", null);

                // invalid actuate
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, null, null, null, null, null, "notInTheTokenList");
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // href is not valid URI
                GetInstance("Invalid URI", INVALID_URI, null, null, null);

                // role is not valid URI
                if (version.IsAtLeast("4.0.1"))
                {
                    GetInstance("Invalid URI", null, INVALID_URI, null, null);
                }

                // label is not valid NCName
                if (version.IsAtLeast("4.0.1"))
                {
                    GetInstance("The ':' character, hexadecimal value 0x3A, cannot be included in a name.", null, null, null, "ddms:prefix& GML");
                }

                // invalid arcrole
                GetInstance("Invalid URI", null, null, null, INVALID_URI, null, null);

                // invalid show
                GetInstance("The show attribute must be one of", null, null, null, null, "notInTheTokenList", null);

                // invalid actuate
                GetInstance("The actuate attribute must be one of", null, null, null, null, null, "notInTheTokenList");
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                XLinkAttributes component = GetInstance(SUCCESS, element);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // locator version
                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                XLinkAttributes elementAttributes = GetInstance(SUCCESS, element);
                XLinkAttributes dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                Assert.AreEqual(elementAttributes, dataAttributes);
                Assert.AreEqual(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());

                // simple version
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                elementAttributes = GetInstance(SUCCESS, element);
                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                Assert.AreEqual(elementAttributes, dataAttributes);
                Assert.AreEqual(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());

                // resource version
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                elementAttributes = GetInstance(SUCCESS, element);
                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                Assert.AreEqual(elementAttributes, dataAttributes);
                Assert.AreEqual(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // locator version
                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                XLinkAttributes elementAttributes = GetInstance(SUCCESS, element);
                XLinkAttributes dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, DIFFERENT_VALUE, TEST_TITLE, TEST_LABEL);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, DIFFERENT_VALUE, TEST_LABEL);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, DIFFERENT_VALUE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                // simple version
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);

                dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, DIFFERENT_VALUE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, DIFFERENT_VALUE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, DIFFERENT_VALUE, TEST_SHOW, TEST_ACTUATE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, "replace", TEST_ACTUATE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, "onRequest");
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                // resource version
                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                elementAttributes = GetInstance(SUCCESS, element);
                dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_TITLE, TEST_LABEL);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, DIFFERENT_VALUE, TEST_LABEL);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));

                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, DIFFERENT_VALUE);
                Assert.IsFalse(elementAttributes.Equals(dataAttributes));
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                XLinkAttributes attributes = new XLinkAttributes(element);
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(attributes.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XElement element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                XLinkAttributes attributes = new XLinkAttributes(element);
                Assert.AreEqual(GetExpectedOutput(true, "locator"), attributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false, "locator"), attributes.GetOutput(false, ""));

                XLinkAttributes dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                Assert.AreEqual(GetExpectedOutput(true, "locator"), dataAttributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false, "locator"), dataAttributes.GetOutput(false, ""));

                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                attributes = new XLinkAttributes(element);
                Assert.AreEqual(GetExpectedOutput(true, "simple"), attributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false, "simple"), attributes.GetOutput(false, ""));

                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
                Assert.AreEqual(GetExpectedOutput(true, "simple"), dataAttributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false, "simple"), dataAttributes.GetOutput(false, ""));

                element = Util.BuildDDMSElement("link", null);
                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                attributes = new XLinkAttributes(element);
                Assert.AreEqual(GetExpectedOutput(true, "resource"), attributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false, "resource"), attributes.GetOutput(false, ""));

                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, TEST_LABEL);
                Assert.AreEqual(GetExpectedOutput(true, "resource"), dataAttributes.GetOutput(true, ""));
                Assert.AreEqual(GetExpectedOutput(false, "resource"), dataAttributes.GetOutput(false, ""));
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_AddTo()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                XLinkAttributes component = LocatorFixture;

                XElement element = Util.BuildDDMSElement("sample", null);
                component.AddTo(element);
                XLinkAttributes output = new XLinkAttributes(element);
                Assert.AreEqual(component, output);
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_GetNonNull()
        {
            XLinkAttributes component = new XLinkAttributes();
            XLinkAttributes output = XLinkAttributes.GetNonNullInstance(null);
            Assert.AreEqual(component, output);

            output = XLinkAttributes.GetNonNullInstance(LocatorFixture);
            Assert.AreEqual(LocatorFixture, output);
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XLinkAttributes component = LocatorFixture;
                XLinkAttributes.Builder builder = new XLinkAttributes.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XLinkAttributes.Builder builder = new XLinkAttributes.Builder();
                Assert.IsNotNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Label = TEST_LABEL;
                Assert.IsFalse(builder.Empty);

                // An untyped instance
                XLinkAttributes output = builder.Commit();
                Assert.IsTrue(String.IsNullOrEmpty(output.Type));
            }
        }

        [TestMethod]
        public virtual void Summary_Xlink_XLinkAttributes_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XLinkAttributes.Builder builder = new XLinkAttributes.Builder();
                builder.Type = "locator";
                builder.Href = INVALID_URI;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "Invalid URI");
                }
                builder.Type = "locator";
                builder.Href = TEST_HREF;
                builder.Commit();
                builder.Type = "simple";
                builder.Commit();
                builder.Type = "resource";
                builder.Commit();
            }
        }
    }
}