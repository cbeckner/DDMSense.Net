using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDMSense.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDMSense.DDMS;

namespace DDMSense.Util.Tests
{
	[TestClass()]
	public class UtilTests
	{
		[TestMethod()]
		public void RequireDDMSDateFormatTest()
		{
			Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy"), "urn:us:mil:ces:metadata:ddms:4");
			Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM"), "urn:us:mil:ces:metadata:ddms:4");
			Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-dd"), "urn:us:mil:ces:metadata:ddms:4");
            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHHK"), "urn:us:mil:ces:metadata:ddms:4");
            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidDDMSException))]
		public void Invalid_RequireDDMSDateFormat()
		{
			//Util.RequireDDMSDateFormat(DateTime.Now.ToString(), "urn:us:mil:ces:metadata:ddms:4");
            //Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy/MM/ddTHHK"), "urn:us:mil:ces:metadata:ddms:4");
            //Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.f"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
            //Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
		}
	}
}
	/*  YYYY
		YYYY-MM
		YYYY-MM-DD
		YYYY-MM-DDThhTZD
		YYYY-MM-DDThh:mmTZD
		YYYY-MM-DDThh:mm.ssTZD
		YYYY-MM-DDThh:mm:ss.sTZD
		Where:
		YYYY	0000 through current year
		MM	01 through 12  (month)
		DD	01 through 31  (day)
		hh	00 through 24  (hour)
		mm	00 through 59  (minute)
		ss	00 through 60  (second)
		.s	.0 through 999 (fractional second)
		TZD  = time zone designator (Z or +hh:mm or -hh:mm)
	*/