using System.Collections.Generic;
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
namespace DDMSense.Test.DDMS.ResourceElements
{

    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using System.Xml.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// <para> Tests related to ddms:source elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class DatesTest : AbstractBaseTestCase
    {

        private const string TEST_CREATED = "2003";
        private const string TEST_POSTED = "2003-02";
        private const string TEST_VALID = "2003-02-15";
        private const string TEST_CUTOFF = "2001-10-31T17:00:00Z";
        private const string TEST_APPROVED = "2003-02-16";
        private const string TEST_RECEIVED = "2003-02-17";

        /// <summary>
        /// Constructor
        /// </summary>
        public DatesTest()
            : base("dates.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Dates Fixture
        {
            get
            {
                try
                {
                    return (new Dates(null, "2003", null, null, null, null, null));
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
        private Dates GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Dates component = null;
            try
            {
                component = new Dates(element);
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
        /// <param name="acquiredOns"> a list of acquisition dates (optional, starting in 4.1) </param>
        /// <param name="created"> the creation date (optional) </param>
        /// <param name="posted"> the posting date (optional) </param>
        /// <param name="validTil"> the expiration date (optional) </param>
        /// <param name="infoCutOff"> the info cutoff date (optional) </param>
        /// <param name="approvedOn"> the approved on date (optional, starting in 3.1) </param>
        /// <param name="receivedOn"> the received on date (optional, starting in 4.0.1) </param>
        /// <returns> a valid object </returns>
        private Dates GetInstance(string message, List<ApproximableDate> acquiredOns, string created, string posted, string validTil, string infoCutOff, string approvedOn, string receivedOn)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Dates component = null;
            try
            {
                component = new Dates(acquiredOns, created, posted, validTil, infoCutOff, approvedOn, receivedOn);
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
        /// Generates an getApprovedOn() Date for testing
        /// </summary>
        private string ApprovedOn
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("3.1") ? TEST_APPROVED : "");
            }
        }

        /// <summary>
        /// Generates a receivedOn Date for testing
        /// </summary>
        private string ReceivedOn
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? TEST_RECEIVED : "");
            }
        }

        /// <summary>
        /// Generates an acquiredOn Date for testing
        /// </summary>
        private List<ApproximableDate> AcquiredOns
        {
            get
            {
                List<ApproximableDate> list = new List<ApproximableDate>();
                if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
                {
                    list.Add(new ApproximableDate(ApproximableDateTest.GetFixtureElement("acquiredOn", true)));
                }
                return (list);
            }
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder text = new StringBuilder();
            if (version.IsAtLeast("4.1"))
            {
                foreach (ApproximableDate acquiredOn in AcquiredOns)
                {
                    text.Append(acquiredOn.GetOutput(isHTML, "dates.", ""));
                }
            }
            text.Append(BuildOutput(isHTML, "dates.created", TEST_CREATED));
            text.Append(BuildOutput(isHTML, "dates.posted", TEST_POSTED));
            text.Append(BuildOutput(isHTML, "dates.validTil", TEST_VALID));
            text.Append(BuildOutput(isHTML, "dates.infoCutOff", TEST_CUTOFF));
            if (version.IsAtLeast("3.1"))
            {
                text.Append(BuildOutput(isHTML, "dates.approvedOn", TEST_APPROVED));
            }
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "dates.receivedOn", TEST_RECEIVED));
            }
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:dates ").Append(XmlnsDDMS).Append(" ");
                xml.Append("ddms:created=\"").Append(TEST_CREATED).Append("\" ");
                xml.Append("ddms:posted=\"").Append(TEST_POSTED).Append("\" ");
                xml.Append("ddms:validTil=\"").Append(TEST_VALID).Append("\" ");
                xml.Append("ddms:infoCutOff=\"").Append(TEST_CUTOFF).Append("\"");
                if (version.IsAtLeast("3.1"))
                {
                    xml.Append(" ddms:approvedOn=\"").Append(TEST_APPROVED).Append("\"");
                }
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append(" ddms:receivedOn=\"").Append(TEST_RECEIVED).Append("\"");
                }
                if (version.IsAtLeast("4.1"))
                {
                    xml.Append("><ddms:acquiredOn>");
                    xml.Append("<ddms:description>description</ddms:description>");
                    xml.Append("<ddms:approximableDate ddms:approximation=\"1st qtr\">2012</ddms:approximableDate>");
                    xml.Append("<ddms:searchableDate><ddms:start>2012-01</ddms:start>");
                    xml.Append("<ddms:end>2012-03-31</ddms:end></ddms:searchableDate>");
                    xml.Append("</ddms:acquiredOn></ddms:dates>");
                }
                else
                {
                    xml.Append(" />");
                }
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Dates.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Dates.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);

                // No optional fields
                GetInstance(SUCCESS, null, "", "", "", "", "", "");
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Wrong date format (using xs:gDay here)
                XElement element = Util.BuildDDMSElement(Dates.GetName(version), null);
                Util.AddDDMSAttribute(element, "created", "---31");
                GetInstance("The date datatype must be one of", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong date format (using xs:gDay here)
                GetInstance("The date datatype must be one of", AcquiredOns, "---31", TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, "---31", TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, "---31", TEST_CUTOFF, ApprovedOn, ReceivedOn);
                GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, "---31", ApprovedOn, ReceivedOn);
                if (version.IsAtLeast("3.1"))
                {
                    GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, "---31", ReceivedOn);
                }
                if (version.IsAtLeast("4.0.1"))
                {
                    GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, "---31");
                }
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                string text, locator;
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // 4.1 ddms:acquiredOn element used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.Equals(1, component.ValidationWarnings.Count());
                    text = "The ddms:acquiredOn element in this DDMS component";
                    locator = "ddms:dates";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                // No warnings 
                else
                {
                    Assert.Equals(0, component.ValidationWarnings.Count());
                }

                // Empty element
                XElement element = Util.BuildDDMSElement(Dates.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count());
                text = "A completely empty ddms:dates element was found.";
                locator = "ddms:dates";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void TestDeprecatedConstructor()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates component = new Dates(TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.IsTrue(!component.AcquiredOns.Any());
            }
        }

        [TestMethod]
        public virtual void TestDeprecatedAccessors()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                Dates component = new Dates(TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.Equals(TEST_CREATED, component.Created.GetValueOrDefault().ToString("o"));
                Assert.Equals(TEST_POSTED, component.Posted.GetValueOrDefault().ToString("o"));
                Assert.Equals(TEST_VALID, component.ValidTil.GetValueOrDefault().ToString("o"));
                Assert.Equals(TEST_CUTOFF, component.InfoCutOff.GetValueOrDefault().ToString("o"));
                if (version.IsAtLeast("3.1"))
                {
                    Assert.Equals(TEST_APPROVED, component.ApprovedOn.GetValueOrDefault().ToString("o"));
                }
                if (version.IsAtLeast("4.0.1"))
                {
                    Assert.Equals(TEST_RECEIVED, component.ReceivedOn.GetValueOrDefault().ToString("o"));
                }

                // Not compatible with XMLGregorianCalendar
                if (version.IsAtLeast("4.1"))
                {
                    component = new Dates("2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z");
                    Assert.IsNull(component.Created);
                    Assert.IsNull(component.Posted);
                    Assert.IsNull(component.ValidTil);
                    Assert.IsNull(component.InfoCutOff);
                    Assert.IsNull(component.ApprovedOn);
                    Assert.IsNull(component.ReceivedOn);
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Dates dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                Dates elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Dates dataComponent = GetInstance(SUCCESS, AcquiredOns, "", TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, "", TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, "", TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, "", ApprovedOn, ReceivedOn);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                if (version.IsAtLeast("3.1"))
                {
                    dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, "", ReceivedOn);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }

                if (version.IsAtLeast("4.0.1"))
                {
                    dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, "");
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }

                if (version.IsAtLeast("4.1"))
                {
                    dataComponent = GetInstance(SUCCESS, null, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, "");
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionApprovedOn()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            try
            {
                new Dates(null, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, TEST_APPROVED, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "This component cannot have an approvedOn date ");
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionReceivedOn()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            try
            {
                new Dates(null, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, null, TEST_RECEIVED);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "This component cannot have a receivedOn date ");
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionAcquiredOn()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("4.1");
                List<ApproximableDate> acquiredOns = AcquiredOns;
                DDMSVersion.SetCurrentVersion("3.0");
                new Dates(acquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, null, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "This component cannot have an acquiredOn date");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Dates.Builder builder = new Dates.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates.Builder builder = new Dates.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.AcquiredOns[0].Description = "";
                Assert.IsTrue(builder.Empty);
                builder.AcquiredOns[0].Description = "test";
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Dates.Builder builder = new Dates.Builder();
                builder.Created = "notAnXmlDate";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The date datatype must be one of");
                }
                builder.Created = TEST_CREATED;
                builder.Commit();
            }
        }

        [TestMethod]
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Dates.Builder builder = new Dates.Builder();
                Assert.IsNotNull(builder.AcquiredOns[1]);
            }
        }
    }
}