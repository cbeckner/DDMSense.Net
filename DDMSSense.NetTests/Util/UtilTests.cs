using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDMSense.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DDMSense.Util.Tests
{
    [TestClass()]
    public class UtilTests
    {
        [TestMethod()]
        public void RequireDDMSDateFormatTest()
        {
            Util.RequireDDMSDateFormat(DateTime.Now.ToString(), "urn:us:mil:ces:metadata:ddms:4");
        }
    }
}
