using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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


    using IBuilder = DDMSense.DDMS.IBuilder;
    using IDDMSComponent = DDMSense.DDMS.IDDMSComponent;

    /// <summary>
    /// A collection of tests related to the LazyList
    /// 
    /// @author Brian Uri!
    /// @since 1.9.9
    /// </summary>
    public class LazyListTest : AbstractBaseTestCase
    {

        public LazyListTest()
            : base(null)
        {
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void testSerializableInterface() throws Exception
        public virtual void TestSerializableInterface()
        {
            LazyList list = new LazyList(typeof(Dates.Builder));
            list.add(new Dates.Builder());
            ((Dates.Builder)(list.get(0))).Created = "05-24-2011";

            ByteArrayOutputStream @out = new ByteArrayOutputStream();
            ObjectOutputStream oos = new ObjectOutputStream(@out);
            oos.writeObject(list);
            oos.close();
            sbyte[] serialized = @out.toByteArray();
            assertTrue(serialized.Length > 0);

            ObjectInputStream ois = new ObjectInputStream(new ByteArrayInputStream(serialized));
            LazyList unserializedList = (LazyList)ois.readObject();
            assertEquals(list.size(), unserializedList.size());
            assertEquals(((Dates.Builder)(list.get(0))).Created, ((Dates.Builder)(unserializedList.get(0))).Created);
        }

        public virtual void TestUnmodifiableList()
        {
            IList<string> list = new List<string>();
            list.Add("Test");
            LazyList lazyList = new LazyList(Collections.unmodifiableList(list), typeof(string));
            try
            {
                lazyList.set(0, "Test2");
            }
            catch (System.NotSupportedException)
            {
                fail("Should have unwrapped the unmodifiable list.");
            }
        }

        public virtual void TestObjectMethods()
        {
            IList<string> list = new List<string>();
            list.Add("Test");
            LazyList lazyList = new LazyList(list, typeof(string));
            assertEquals(lazyList, lazyList);
            assertEquals(lazyList, list);
            assertEquals(lazyList.GetHashCode(), list.GetHashCode());
            assertEquals(lazyList.ToString(), list.ToString());
        }

        public virtual void TestListInterface()
        {
            // Because a Java ArrayList underlies this Object, I'm not too worried that these tests are necessary.

            LazyList list = new LazyList(typeof(Dates.Builder));
            assertTrue(list.Empty);
            Dates.Builder datesBuilder = new Dates.Builder();
            list.add(datesBuilder);
            assertEquals(1, list.size());
            assertFalse(list.Empty);
            list.add(datesBuilder);
            assertEquals(2, list.size());
            list.remove(1);
            assertEquals(1, list.size());
            Rights.Builder rightsBuilder = new Rights.Builder();
            list.add(rightsBuilder);
            assertTrue(list.contains(rightsBuilder));
            assertEquals(2, list.size());
            list.remove(rightsBuilder);
            assertFalse(list.contains(rightsBuilder));
            assertEquals(1, list.size());
            for (IEnumerator iterator = list.GetEnumerator(); iterator.hasNext(); )
            {
                IBuilder builder = (IBuilder)iterator.next();
                assertNotNull(builder);
            }
            assertEquals(0, list.IndexOf(datesBuilder));
            list.add(datesBuilder);
            assertEquals(1, list.LastIndexOf(datesBuilder));

            IBuilder[] array = new IBuilder[0];
            object[] returnedArray = list.toArray();
            assertEquals(2, returnedArray.Length);
            IBuilder[] returnedBuilderArray = (IBuilder[])list.toArray(array);
            assertEquals(2, returnedBuilderArray.Length);

            //JAVA TO C# CONVERTER WARNING: Unlike Java's ListIterator, enumerators in .NET do not allow altering the collection:
            assertNotNull(list.GetEnumerator());
            assertNotNull(list.listIterator(1));

            list.set(1, rightsBuilder);
            assertEquals(rightsBuilder, list.get(1));
            list.add(1, datesBuilder);
            assertEquals(datesBuilder, list.get(1));
            assertEquals(rightsBuilder, list.get(2));

            IList newList = new List<object>();
            newList.Add(rightsBuilder);
            list.removeAll(newList);
            assertFalse(list.containsAll(newList));
            assertEquals(2, list.size());
            list.add(rightsBuilder);
            list.retainAll(newList);
            assertEquals(1, list.size());
            assertTrue(list.containsAll(newList));

            list.addAll(newList);
            assertEquals(2, list.size());
            list.add(datesBuilder);
            list.addAll(2, newList);
            assertEquals(rightsBuilder, list.get(2));
            assertEquals(datesBuilder, list.get(3));

            list.add(datesBuilder);
            list.clear();
            assertTrue(list.Empty);
        }

        public virtual void TestSublist()
        {
            LazyList list = new LazyList(typeof(Dates.Builder));
            Rights.Builder rightsBuilder = new Rights.Builder();
            Dates.Builder datesBuilder = new Dates.Builder();

            list.add(rightsBuilder);
            list.add(rightsBuilder);
            list.add(rightsBuilder);
            list.add(datesBuilder);

            IList subList = list.subList(0, 1);
            assertFalse(subList.Contains(datesBuilder));
            assertEquals(1, subList.Count);

            assertNotNull(subList[10]);
        }

        public virtual void TestGet()
        {
            LazyList list = new LazyList(typeof(Dates.Builder));
            assertTrue(list.Empty);
            Dates.Builder datesBuilder = new Dates.Builder();
            list.add(datesBuilder);
            assertEquals(datesBuilder, list.get(0));
            Dates.Builder builder = (Dates.Builder)list.get(3);
            assertNotNull(builder);
            assertEquals(4, list.size());
        }

        public virtual void TestNullList()
        {
            try
            {
                new LazyList(null, typeof(string));
            }
            catch (System.NullReferenceException)
            {
                fail("Should not have thrown NPE.");
            }
        }

        public virtual void TestListGetExceptions()
        {
            LazyList list = new LazyList(null, typeof(AbstractBaseComponent));
            try
            {
                list.get(0);
                fail("Allowed invalid class.");
            }
            catch (System.ArgumentException e)
            {
                ExpectMessage(e, "Cannot instantiate list item");
            }

            list = new LazyList(null, typeof(IDDMSComponent));
            try
            {
                list.get(0);
                fail("Allowed invalid class.");
            }
            catch (System.ArgumentException e)
            {
                ExpectMessage(e, "Cannot instantiate list item");
            }
        }
    }

}