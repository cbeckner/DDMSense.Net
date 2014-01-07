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
namespace buri.ddmsence.util {


	/// <summary>
	/// A collection of PropertyReader tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class PropertyReaderTest : AbstractBaseTestCase {

		public PropertyReaderTest() : base(null) {
		}

		/// <summary>
		/// Resets the in-use prefix for DDMS.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void setUp() throws Exception
		protected internal override void SetUp() {
			PropertyReader.setProperty("ddms.prefix", "ddms");
		}

		/// <summary>
		/// Resets the in-use prefix for DDMS.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void tearDown() throws Exception
		protected internal override void TearDown() {
			PropertyReader.setProperty("ddms.prefix", "ddms");
		}

		public virtual void TestGetPropertyInvalid() {
			try {
				PropertyReader.getProperty("unknown.property");
				fail("Did not prevent invalid property.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "Undefined Property");
			}
		}

		public virtual void TestGetListPropertyValid() {
			IList<string> properties = PropertyReader.getListProperty("ddms.supportedVersions");
			assertEquals(4, properties.Count);
		}

		public virtual void TestSetPropertyInvalidName() {
			// This also handles unconfigurable properties.
			try {
				PropertyReader.setProperty("unknown.property", "value");
				fail("Did not prevent invalid property name.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "unknown.property is not a configurable property.");
			}
		}

		public virtual void TestSetPropertyValid() {
			PropertyReader.setProperty("ddms.prefix", "DDMS");
			assertEquals("DDMS", PropertyReader.getPrefix("ddms"));
		}
	}

}