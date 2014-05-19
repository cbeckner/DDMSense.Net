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
using DDMSense.DDMS;
using DDMSense.DDMS.ResourceElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DDMSense.Test.DDMS
{

    /// <summary>
    /// <para> Tests related to ValidationMessages </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.c
    /// </summary>
    [TestClass]
    public class ValidationMessageTest
    {

        [TestMethod]
        public virtual void ValidationMessage_Factory()
        {
            ValidationMessage message = ValidationMessage.NewWarning("Test", "ddms:test");
            Assert.AreEqual(ValidationMessage.WarningType, message.Type);
            Assert.AreEqual("Test", message.Text);
            Assert.AreEqual("/ddms:test", message.Locator);

            message = ValidationMessage.NewError("Test", "ddms:test");
            Assert.AreEqual(ValidationMessage.ErrorType, message.Type);
            Assert.AreEqual("Test", message.Text);
            Assert.AreEqual("/ddms:test", message.Locator);
        }

        [TestMethod]
        public virtual void ValidationMessage_Equality()
        {
            ValidationMessage message1 = ValidationMessage.NewWarning("Test", "ddms:test");
            ValidationMessage message2 = ValidationMessage.NewWarning("Test", "ddms:test");
            Assert.AreEqual(message1, message1);
            Assert.AreEqual(message1, message2);
            Assert.AreEqual(message1.GetHashCode(), message2.GetHashCode());
            Assert.AreEqual(message1.ToString(), message2.ToString());
        }

        [TestMethod]
        public virtual void ValidationMessage_InequalityDifferentValues()
        {
            ValidationMessage message1 = ValidationMessage.NewWarning("Test", "ddms:test");
            ValidationMessage message2 = ValidationMessage.NewError("Test", "ddms:test");
            Assert.IsFalse(message1.Equals(message2));

            message2 = ValidationMessage.NewWarning("Test2", "ddms:test");
            Assert.IsFalse(message1.Equals(message2));

            message2 = ValidationMessage.NewWarning("Test", "ddms:test2");
            Assert.IsFalse(message1.Equals(message2));
        }

        [TestMethod]
        public virtual void ValidationMessage_InequalityWrongClass()
        {
            ValidationMessage message = ValidationMessage.NewWarning("Test", "ddms:test");
            Rights wrongComponent = new Rights(true, true, true);
            Assert.IsFalse(message.Equals(wrongComponent));
        }

        [TestMethod]
        public virtual void ValidationMessage_LocatorEquality()
        {
            InvalidDDMSException e = new InvalidDDMSException("test");
            e.Locator = "test";
            Assert.AreEqual("/test", e.Locator);
        }
    }

}