using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDMSSense.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DDMSSense.Util.Tests
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
