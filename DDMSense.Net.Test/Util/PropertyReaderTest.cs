using DDMSense.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
namespace DDMSense.Test.Util {


	/// <summary>
	/// A collection of PropertyReader tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
    [TestClass]
    public class PropertyReaderTest : AbstractBaseTestCase
    {

		public PropertyReaderTest() : base(null) {
		}

		/// <summary>
		/// Resets the in-use prefix for DDMS.
		/// </summary>
		protected internal override void SetUp() {
			PropertyReader.SetProperty("ddms.prefix", "ddms");
		}

		/// <summary>
		/// Resets the in-use prefix for DDMS.
		/// </summary>
		protected internal override void TearDown() {
			PropertyReader.SetProperty("ddms.prefix", "ddms");
		}

        [TestMethod]
		public virtual void Util_PropertyReader_GetPropertyInvalid() {
			try {
				PropertyReader.GetProperty("unknown.property");
				Assert.Fail("Did not prevent invalid property.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "Undefined Property");
			}
		}

        [TestMethod]
        public virtual void Util_PropertyReader_GetListPropertyValid()
        {
			List<string> properties = PropertyReader.GetListProperty("ddms.supportedVersions");
			Assert.AreEqual(4, properties.Count());
		}

        [TestMethod]
        public virtual void Util_PropertyReader_SetPropertyInvalidName()
        {
			// This also handles unconfigurable properties.
			try {
				PropertyReader.SetProperty("unknown.property", "value");
				Assert.Fail("Did not prevent invalid property name.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "unknown.property is not a configurable property.");
			}
		}

        [TestMethod]
        public virtual void Util_PropertyReader_SetPropertyValid()
        {
			PropertyReader.SetProperty("ddms.prefix", "DDMS");
			Assert.AreEqual("DDMS", PropertyReader.GetPrefix("ddms"));
		}
	}

}