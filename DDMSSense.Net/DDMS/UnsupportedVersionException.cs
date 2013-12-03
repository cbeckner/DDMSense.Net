using System;

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
namespace DDMSSense.DDMS {

	/// <summary>
	/// Exception class for attempts to use a version of DDMS which is not supported by this library.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class UnsupportedVersionException : Exception {

		private const long SerialVersionUID = -183915550465140589L;

		/// <seealso cref= Exception#Exception(String) </seealso>
		public UnsupportedVersionException(string version) : base("DDMS Version " + version + " is not yet supported.") {
		}
	}

}