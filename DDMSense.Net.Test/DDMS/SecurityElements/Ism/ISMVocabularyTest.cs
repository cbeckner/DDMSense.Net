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
namespace DDMSense.Test.DDMS.SecurityElements.Ism
{

    using DDMSense.DDMS.SecurityElements.Ism;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSVersion = DDMSense.Util.DDMSVersion;

    /// <summary>
    /// <para> Tests related to the ISM Controlled Vocabularies </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.d
    /// </summary>
    public class ISMVocabularyTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ISMVocabularyTest()
            : base(null)
        {
        }

        [TestMethod]
        public virtual void TestBadKey()
        {
            try
            {
                ISMVocabulary.GetEnumerationTokens("unknownKey");
                Assert.Fail("Allowed invalid key.");
            }
            catch (System.ArgumentException e)
            {
                ExpectMessage(e, "No controlled vocabulary could be found");
            }
        }

        [TestMethod]
        public virtual void TestEnumerationTokens()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                ISMVocabulary.DDMSVersion = version;
                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_ALL_CLASSIFICATIONS, "C"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_ALL_CLASSIFICATIONS, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_OWNER_PRODUCERS, "AUS"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_OWNER_PRODUCERS, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SCI_CONTROLS, "HCS"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SCI_CONTROLS, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "FOUO"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_FGI_SOURCE_OPEN, "ABW"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_FGI_SOURCE_OPEN, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_FGI_SOURCE_PROTECTED, "ABW"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_FGI_SOURCE_PROTECTED, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_RELEASABLE_TO, "ABW"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_RELEASABLE_TO, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_NON_IC_MARKINGS, "DS"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_NON_IC_MARKINGS, "unknown"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DECLASS_EXCEPTION, "25X1"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DECLASS_EXCEPTION, "unknown"));

                if (!version.IsAtLeast("3.1"))
                {
                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_TYPE_EXEMPTED_SOURCE, "X1"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_TYPE_EXEMPTED_SOURCE, "unknown"));
                }

                if (version.IsAtLeast("3.1"))
                {
                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_ATOMIC_ENERGY_MARKINGS, "RD"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_ATOMIC_ENERGY_MARKINGS, "unknown"));

                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_COMPLIES_WITH, "DoD5230.24"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_COMPLIES_WITH, "unknown"));

                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISPLAY_ONLY_TO, "ABW"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISPLAY_ONLY_TO, "unknown"));

                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_NON_US_CONTROLS, "ATOMAL"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_NON_US_CONTROLS, "unknown"));
                }

                if (version.IsAtLeast("4.0.1"))
                {
                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_NOTICE_TYPE, "DoD-Dist-B"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_NOTICE_TYPE, "unknown"));

                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_POC_TYPE, "DoD-Dist-B"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_POC_TYPE, "unknown"));
                }
            }
        }

        [TestMethod]
        public virtual void TestEnumerationPatterns()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                ISMVocabulary.DDMSVersion = version;
                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SCI_CONTROLS, "SI-G-ABCD"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SCI_CONTROLS, "SI-G-ABCDE"));

                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SAR_IDENTIFIER, "SAR-ABC"));
                Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SAR_IDENTIFIER, "SAR-AB"));
                Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_SAR_IDENTIFIER, "SAR-ABCD"));

                if (!version.IsAtLeast("3.1"))
                {
                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "RD-SG-1"));
                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "RD-SG-12"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "RD-SG-100"));

                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "FRD-SG-1"));
                    Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "FRD-SG-12"));
                    Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, "FRD-SG-100"));
                }
            }
        }

        [TestMethod]
        public virtual void TestIsUSMarking()
        {
            ISMVocabulary.DDMSVersion = DDMSVersion.GetVersionFor("2.0");
            Assert.IsTrue(ISMVocabulary.EnumContains(ISMVocabulary.CVE_US_CLASSIFICATIONS, "TS"));
            Assert.IsFalse(ISMVocabulary.EnumContains(ISMVocabulary.CVE_US_CLASSIFICATIONS, "CTS"));
        }

        [TestMethod]
        public virtual void TestInvalidMessage()
        {
            Assert.Equals("Dog is not a valid enumeration token for this attribute, as specified in Cat.", ISMVocabulary.GetInvalidMessage("Cat", "Dog"));
        }
    }

}