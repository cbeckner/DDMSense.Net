using System;
using System.Collections.Generic;
using System.Configuration;
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
namespace DDMSSense.Util {


	/// <summary>
	/// Utility class for dealing with the property file.
	/// 
	/// <para> Properties in DDMSence are found in the <code>ddmsence.properties</code> file. All properties are prefixed with
	/// "DDMSSense.", so <code>getProperty</code> calls should be performed with just the property suffix. </para>
	/// 
	/// <para> The Property Reader supports several custom properties, which can be specified at runtime. The complete list of
	/// configurable properties can be found on the DDMSence website at:
	/// http://ddmsence.urizone.net/documentation.jsp#tips-configuration. </para>
	/// 
	/// <para> Changing a namespace prefix will affect both components created from scratch and components loaded from XML
	/// files. </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class PropertyReader {

		private const string PROPERTIES_FILE = "ddmsence.properties";
		private const string UNDEFINED_PROPERTY = "Undefined Property: ";

		private static readonly List<string> CUSTOM_PROPERTIES = new List<string>();
		static PropertyReader() {
			CUSTOM_PROPERTIES.Add("ddms.prefix");
			CUSTOM_PROPERTIES.Add("gml.prefix");
			CUSTOM_PROPERTIES.Add("ism.prefix");
			CUSTOM_PROPERTIES.Add("ntk.prefix");
			CUSTOM_PROPERTIES.Add("output.indexLevel");
			CUSTOM_PROPERTIES.Add("sample.data");
			CUSTOM_PROPERTIES.Add("xlink.prefix");
			CUSTOM_PROPERTIES.Add("xml.transform.TransformerFactory");
		}

		private static readonly PropertyReader INSTANCE = new PropertyReader();

		/// <summary>
		/// Private to prevent instantiation
		/// </summary>
		private PropertyReader() {			
                if (ConfigurationManager.AppSettings["ddms.supportedversions"] == null)
                    throw new Exception("Could not load the properties file");
		}

		/// <summary>
		/// Convenience method to look up an XML prefix </summary>
		/// <param name="key"> the schema key, such as ddms, ism, or ntk. </param>
		public static string GetPrefix(string key) {
			return (GetProperty(key + ".prefix"));
		}

		/// <summary>
		/// Locates a property and returns it. Assumes that the property is required.
		/// </summary>
		/// <param name="name">		the simple name of the property, without "DDMSSense." </param>
		/// <returns> the property specified </returns>
		/// <exception cref="IllegalArgumentException"> if the property does not exist. </exception>
		public static string GetProperty(string name) {
			string value = ConfigurationManager.AppSettings[name];
			if (value == null) {
				throw new System.ArgumentException(UNDEFINED_PROPERTY + name);
			}
			return (value);
		}

		/// <summary>
		/// Attempts to set one of the properties defined as a configurable property.
		/// </summary>
		/// <param name="name"> the key of the property, without the "DDMSSense." prefix </param>
		/// <param name="value"> the new value of the property </param>
		/// <exception cref="IllegalArgumentException"> if the property is not a valid configurable property. </exception>
		public static void SetProperty(string name, string value) {
			if (!CUSTOM_PROPERTIES.Contains(name)) {
				throw new System.ArgumentException(name + " is not a configurable property.");
			}
            ConfigurationManager.AppSettings.Set(name,Util.GetNonNullString(value).Trim());
		}

		/// <summary>
		/// Locates a list property and returns it as a List
		/// </summary>
		/// <param name="name">		the simple name of the property, without "DDMSSense." </param>
		/// <returns> the property specified </returns>
		/// <exception cref="IllegalArgumentException"> if the property does not exist </exception>
		public static List<string> GetListProperty(string name) {
			string value = GetProperty(name);
			return value.Split(',').ToList();
		}
	}

}