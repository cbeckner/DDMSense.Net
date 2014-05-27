using System;
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

namespace DDMSense.Test
{
    using DDMSense;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using IDDMSComponent = DDMSense.DDMS.IDDMSComponent;
    using InvalidDDMSException = DDMSense.DDMS.InvalidDDMSException;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using ValidationMessage = DDMSense.DDMS.ValidationMessage;

    /// <summary>
    /// <para> Tests related to underlying methods in the base class for DDMS components </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class BaseComponentTest : AbstractBaseTestCase
    {
        public BaseComponentTest()
            : base(null)
        {
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The total must be at least 1")]
        public virtual void TestBuildIndexInvalidInputBounds()
        {
            Rights rights = new Rights(true, true, true);

            // Bad total
            rights.BuildIndex(0, 0);

            // Low index
            rights.BuildIndex(-1, 1);

            // High index
            rights.BuildIndex(2, 2);
        }

        [TestMethod()]
        public virtual void TestBuildIndexValidInputBounds()
        {
            Rights rights = new Rights(true, true, true);
            // Good total
            rights.BuildIndex(0, 1);

            // Good index
            rights.BuildIndex(1, 2);
        }

        [TestMethod()]
        public virtual void TestBuildIndexLevel0()
        {
            string index = string.Empty;
            Rights rights = new Rights(true, true, true);

            PropertyReader.SetProperty("output.indexLevel", "0");

            index = rights.BuildIndex(0, 1);
            Assert.AreEqual("", index);

            index = rights.BuildIndex(2, 4);
            Assert.AreEqual("", index);

            PropertyReader.SetProperty("output.indexLevel", "unknown");
            index = rights.BuildIndex(0, 1);
            Assert.AreEqual("", index);

            index = rights.BuildIndex(2, 4);
            Assert.AreEqual("", index);
        }

        [TestMethod()]
        public virtual void TestBuildIndexLevel1()
        {
            string index = string.Empty;
            Rights rights = new Rights(true, true, true);

            PropertyReader.SetProperty("output.indexLevel", "1");
            index = rights.BuildIndex(0, 1);
            Assert.AreEqual("", index);

            index = rights.BuildIndex(2, 4);
            Assert.AreEqual("[3]", index);
        }

        [TestMethod()]
        public virtual void TestBuildIndexLevel2()
        {
            string index = string.Empty;
            Rights rights = new Rights(true, true, true);

            PropertyReader.SetProperty("output.indexLevel", "2");
            index = rights.BuildIndex(0, 1);
            Assert.AreEqual("[1]", index);

            index = rights.BuildIndex(2, 4);
            Assert.AreEqual("[3]", index);
        }

        [TestMethod()]
        public virtual void TestBuildOutput()
        {
            Rights rights = new Rights(true, true, true);
            List<IDDMSComponent> objectList = new List<IDDMSComponent>();
            objectList.Add(rights);

            Assert.AreEqual("rights.privacyAct: true\nrights.intellectualProperty: true\nrights.copyright: true\n",  rights.BuildOutput(false, "", objectList));

            List<string> stringList = new List<string>();
            stringList.Add("Text");
            Assert.AreEqual("name: Text\n", rights.BuildOutput(false, "name", stringList));

            List<double?> otherList = new List<double?>();
            otherList.Add(Convert.ToDouble(2.0));
            Assert.AreEqual("name: 2\n", rights.BuildOutput(false, "name", otherList));
        }

        [TestMethod()]
        public virtual void TestSelfEquality()
        {
            Rights rights = new Rights(true, true, true);
            Assert.AreEqual(rights, rights);
        }

        [TestMethod()]
        public virtual void TestToString()
        {
            Rights rights = new Rights(true, true, true);
            Assert.AreEqual(rights.ToString(), rights.ToXML());
        }

        [TestMethod()]
        public virtual void TestVersion()
        {
            Rights rights = new Rights(true, true, true);
            Assert.AreEqual(DDMSVersion.CurrentVersion.Namespace, rights.Namespace);
        }

        [TestMethod()]
        public virtual void TestCustomPrefix()
        {
            string @namespace = DDMSVersion.CurrentVersion.Namespace;
            XElement element = DDMSense.Util.Util.BuildElement("customPrefix", Language.GetName(DDMSVersion.CurrentVersion), @namespace, null);
            DDMSense.Util.Util.AddAttribute(element, "customPrefix", "qualifier", @namespace, "testQualifier");
            DDMSense.Util.Util.AddAttribute(element, "customPrefix", "value", @namespace, "en");
            new Language(element);
        }

        [TestMethod()]
        public virtual void TestNullChecks()
        {
            AbstractBaseComponent component = new AbstractBaseComponentAnonymousInnerClassHelper(this);
            Assert.AreEqual("", component.Name);
            Assert.AreEqual("", component.Namespace);
            Assert.AreEqual("", component.Prefix);
            Assert.AreEqual("", component.ToXML());
        }

        private class AbstractBaseComponentAnonymousInnerClassHelper : AbstractBaseComponent
        {
            private readonly BaseComponentTest OuterInstance;

            public AbstractBaseComponentAnonymousInnerClassHelper(BaseComponentTest outerInstance)
            {
                this.OuterInstance = outerInstance;
            }

            public override string GetOutput(bool isHTML, string prefix, string suffix)
            {
                return null;
            }
        }

        [TestMethod()]
        public virtual void TestAttributeWarnings()
        {
            AbstractBaseComponent component = new AbstractBaseComponentAnonymousInnerClassHelper2(this);
            PrivateObject po = new PrivateObject(component, new PrivateType(typeof(AbstractBaseComponent)));
            List<ValidationMessage> warnings = new List<ValidationMessage>();
            warnings.Add(ValidationMessage.NewWarning("test", "locator"));
            po.Invoke("AddWarnings", new object[] { warnings, true });
            //component.AddWarnings(warnings, true);
            Assert.AreEqual("//locator", component.ValidationWarnings[0].Locator);
        }

        private class AbstractBaseComponentAnonymousInnerClassHelper2 : AbstractBaseComponent
        {
            private readonly BaseComponentTest OuterInstance;

            public AbstractBaseComponentAnonymousInnerClassHelper2(BaseComponentTest outerInstance)
            {
                this.OuterInstance = outerInstance;
            }

            public override string GetOutput(bool isHTML, string prefix, string suffix)
            {
                return null;
            }

            protected override string LocatorSuffix
            {
                get
                {
                    return ("locatorSuffix");
                }
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDDMSException), "A child component, ddms:Organization")]
        public virtual void TestSameVersion()
        {
            DDMSVersion.SetCurrentVersion("3.0");
            Organization org = new Organization(DDMSense.Util.Util.GetXsListAsList("DISA"), null, null, null, null, null);
            DDMSVersion.SetCurrentVersion("2.0");
            new Creator(org, null, null);
        }
    }
}