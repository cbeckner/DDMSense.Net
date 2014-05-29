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

namespace DDMSense.Test.Util
{
    using DDMSense.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using InvalidDDMSException = DDMSense.DDMS.InvalidDDMSException;

    /// <summary>
    /// A collection of DDMSReader tests.
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class DDMSReaderTest : AbstractBaseTestCase
    {
        private DDMSReader _reader;

        public DDMSReaderTest()
            : base(null)
        {
            _reader = new DDMSReader();
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementNullString()
        {
            try
            {
                Reader.GetElement((string)null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (IOException)
            {
                Assert.Fail("Allowed invalid data.");
            }
            catch (System.ArgumentException e)
            {
                ExpectMessage(e, "XML string is required.");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementNullInputStream()
        {
            try
            {
                Reader.GetElement((Stream)null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (IOException)
            {
                Assert.Fail("Allowed invalid data.");
            }
            catch (System.ArgumentException e)
            {
                ExpectMessage(e, "input stream is required.");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementDoesNotExistString()
        {
            try
            {
                Reader.GetElement("<wrong></wrong>");
                Assert.Fail("Allowed invalid data.");
            }
            catch (IOException)
            {
                Assert.Fail("Should have thrown an InvalidDDMSException");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Valid Namespace is required");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementDoesNotExistInputStream()
        {
            try
            {
                Reader.GetElement(File.OpenRead("doesnotexist"));
                Assert.Fail("Allowed invalid data.");
            }
            catch (IOException e)
            {
                ExpectMessage(e, "Could not find file");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetElementStringSuccess()
        {
            Reader.GetElement("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ddms:language " + " xmlns:ddms=\"http://metadata.dod.mil/mdr/ns/DDMS/3.0/\" " + " ddms:qualifier=\"http://purl.org/dc/elements/1.1/language\" ddms:value=\"en\" />");
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceFailure()
        {
            try
            {
                Reader.GetDDMSResource(File.OpenRead(PropertyReader.GetProperty("test.unit.data") + "3.0/rights.xml"));
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Unexpected namespace URI and local name encountered");
            }
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetResourceSuccessInputStream()
        {
            Reader.GetDDMSResource(File.OpenRead(PropertyReader.GetProperty("test.unit.data") + "3.0/resource.xml"));
        }

        [TestMethod]
        public virtual void Util_DDMSReaderTest_GetExternalSchemaLocation()
        {
            string externalLocations = Reader.ExternalSchemaLocations;
            Assert.AreEqual(14, externalLocations.Split(" ", true).Length);
            Assert.IsTrue(externalLocations.Contains("http://metadata.dod.mil/mdr/ns/DDMS/2.0/"));
            Assert.IsTrue(externalLocations.Contains("http://metadata.dod.mil/mdr/ns/DDMS/3.0/"));
            Assert.IsTrue(externalLocations.Contains("http://www.opengis.net/gml"));
            Assert.IsTrue(externalLocations.Contains("http://www.opengis.net/gml/3.2"));
        }

        /// <summary>
        /// Accessor for the reader
        /// </summary>
        private DDMSReader Reader
        {
            get
            {
                return _reader;
            }
        }
    }
}