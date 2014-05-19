///* Copyright 2010 - 2013 by Brian Uri!
   
//   This file is part of DDMSence.
   
//   This library is free software; you can redistribute it and/or modify
//   it under the terms of version 3.0 of the GNU Lesser General Public 
//   License as published by the Free Software Foundation.
   
//   This library is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
//   GNU Lesser General Public License for more details.
   
//   You should have received a copy of the GNU Lesser General Public 
//   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

//   You can contact the author at ddmsence@urizone.net. The DDMSence
//   home page is located at http://ddmsence.urizone.net/
// */
//namespace DDMSense.Test.DDMS {

//    /// <summary>
//    /// <para> Tests related to ValidationMessages </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.c
//    /// </summary>
//    public class ValidationMessageTest : TestCase {

//        public virtual void TestFactory() {
//            ValidationMessage message = ValidationMessage.newWarning("Test", "ddms:test");
//            Assert.Equals(ValidationMessage.WARNING_TYPE, message.Type);
//            Assert.Equals("Test", message.Text);
//            Assert.Equals("/ddms:test", message.Locator);

//            message = ValidationMessage.newError("Test", "ddms:test");
//            Assert.Equals(ValidationMessage.ERROR_TYPE, message.Type);
//            Assert.Equals("Test", message.Text);
//            Assert.Equals("/ddms:test", message.Locator);
//        }

//        public virtual void TestEquality() {
//            ValidationMessage message1 = ValidationMessage.newWarning("Test", "ddms:test");
//            ValidationMessage message2 = ValidationMessage.newWarning("Test", "ddms:test");
//            Assert.Equals(message1, message1);
//            Assert.Equals(message1, message2);
//            Assert.Equals(message1.GetHashCode(), message2.GetHashCode());
//            Assert.Equals(message1.ToString(), message2.ToString());
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testInequalityDifferentValues() throws InvalidDDMSException
//        public virtual void TestInequalityDifferentValues() {
//            ValidationMessage message1 = ValidationMessage.newWarning("Test", "ddms:test");
//            ValidationMessage message2 = ValidationMessage.newError("Test", "ddms:test");
//            Assert.IsFalse(message1.Equals(message2));

//            message2 = ValidationMessage.newWarning("Test2", "ddms:test");
//            Assert.IsFalse(message1.Equals(message2));

//            message2 = ValidationMessage.newWarning("Test", "ddms:test2");
//            Assert.IsFalse(message1.Equals(message2));
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testInequalityWrongClass() throws InvalidDDMSException
//        public virtual void TestInequalityWrongClass() {
//            ValidationMessage message = ValidationMessage.newWarning("Test", "ddms:test");
//            Rights wrongComponent = new Rights(true, true, true);
//            Assert.IsFalse(message.Equals(wrongComponent));
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testLocatorEquality() throws InvalidDDMSException
//        public virtual void TestLocatorEquality() {
//            InvalidDDMSException e = new InvalidDDMSException("test");
//            e.Locator = "test";
//            Assert.Equals("/test", e.Locator);
//        }
//    }

//}