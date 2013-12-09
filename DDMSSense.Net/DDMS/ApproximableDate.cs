#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

#endregion

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

namespace DDMSSense.DDMS
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     Base class for DDMS elements which are an approximable date, such as ddms:dates/ddms:acquiredOn.
    ///     <para>
    ///         The structure of this class diverges from the usual DDMSence approach of selecting which DDMS components are
    ///         implemented as Java classes. The ApproximableDateType, introduced in DDMS 4.1, is directly reused in three
    ///         locations
    ///         in the DDMS schema, so it is implemented as a final class rather than an Abstract class. It contains one
    ///         wrapper
    ///         element, ddms:searchableDate, which is not implemented as a Java class.
    ///     </para>
    ///     <para>
    ///         This type also contains one element, ddms:approximableDate, which should be implemented as a Java class, since
    ///         it
    ///         contains an attribute. To simplify the class structure, this element and its attribute are collapsed into this
    ///         Java
    ///         class.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>This component can be used with no description, approximableDate, or searchableDate values.</li>
    ///                     <li>A ddms:description element can be used without child text.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:description</u>: A description of this date (0-1, optional)<br />
    ///                 <u>ddms:approximableDate</u>: The value of this date, associated with an optional approximation
    ///                 decorator (0-1,
    ///                 optional)<br />
    ///                 <u>ddms:searchableDate/ddms:start</u>: The exact date which is the lower bound for this approximable
    ///                 date in searches
    ///                 (0-1, optional)<br />
    ///                 <u>ddms:searchableDate/ddms:end</u>: The exact date which is the upper bound for this approximable date
    ///                 in searches
    ///                 (0-1, optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:approximableDate/ddms:approximation</u>: An attribute that decorates the approximableDate with
    ///                 terms such as
    ///                 "early" or "late" (0-1, optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 2.1.0
    /// </summary>
    public sealed class ApproximableDate : AbstractBaseComponent
    {
        private const string DESCRIPTION_NAME = "description";
        private const string APPROXIMABLE_DATE_NAME = "approximableDate";
        private const string APPROXIMATION_NAME = "approximation";
        private const string SEARCHABLE_DATE_NAME = "searchableDate";
        private const string START_NAME = "start";
        private const string END_NAME = "end";

        private static readonly List<string> APPROXIMATION_TYPES = new List<string>();

        private static readonly List<string> NAME_TYPES = new List<string>();

        static ApproximableDate()
        {
            APPROXIMATION_TYPES.Add("1st qtr");
            APPROXIMATION_TYPES.Add("2nd qtr");
            APPROXIMATION_TYPES.Add("3rd qtr");
            APPROXIMATION_TYPES.Add("4th qtr");
            APPROXIMATION_TYPES.Add("circa");
            APPROXIMATION_TYPES.Add("early");
            APPROXIMATION_TYPES.Add("mid");
            APPROXIMATION_TYPES.Add("late");
            NAME_TYPES.Add("acquiredOn");
            NAME_TYPES.Add("approximableStart");
            NAME_TYPES.Add("approximableEnd");
        }

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ApproximableDate(Element element)
        {
            base.SetXOMElement(element, true);
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="name"> the name of the element </param>
        /// <param name="description"> the description of this approximable date (optional) </param>
        /// <param name="approximableDate"> the value of the approximable date (optional) </param>
        /// <param name="approximation"> an attribute that decorates the date (optional) </param>
        /// <param name="searchableStartDate"> the lower bound for this approximable date (optional) </param>
        /// <param name="searchableEndDate"> the upper bound for this approximable date (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ApproximableDate(string name, string description, string approximableDate, string approximation,
            string searchableStartDate, string searchableEndDate)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(name, null);
                SetXOMElement(element, false);

                if (!String.IsNullOrEmpty(description))
                {
                    Util.Util.AddDDMSChildElement(Element, DESCRIPTION_NAME, description);
                }
                if (!String.IsNullOrEmpty(approximableDate) || String.IsNullOrEmpty(approximation))
                {
                    Element approximableElment = Util.Util.BuildDDMSElement(APPROXIMABLE_DATE_NAME, approximableDate);
                    Util.Util.AddDDMSAttribute(approximableElment, APPROXIMATION_NAME, approximation);
                    Element.Add(approximableElment);
                }
                if (!String.IsNullOrEmpty(searchableStartDate) || String.IsNullOrEmpty(searchableEndDate))
                {
                    Element searchableElement = Util.Util.BuildDDMSElement(SEARCHABLE_DATE_NAME, null);
                    Util.Util.AddDDMSChildElement(searchableElement, START_NAME, searchableStartDate);
                    Util.Util.AddDDMSChildElement(searchableElement, END_NAME, searchableEndDate);
                    Element.Add(searchableElement);
                }
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the description.
        /// </summary>
        public string Description
        {
            get
            {
                Element descriptionElement = GetChild(DESCRIPTION_NAME);
                return (descriptionElement == null ? "" : Util.Util.GetNonNullString(descriptionElement.Value));
            }
        }

        /// <summary>
        ///     Accessor for the approximableDate (optional).
        /// </summary>
        public string ApproximableDateString
        {
            get
            {
                Element dateElement = GetChild(APPROXIMABLE_DATE_NAME);
                return (dateElement == null ? "" : Util.Util.GetNonNullString(dateElement.Value));
            }
        }

        /// <summary>
        ///     Accessor for the value of the approximation attribute
        /// </summary>
        public string Approximation
        {
            get
            {
                string approximation = null;
                Element approximableDateElement = GetChild(APPROXIMABLE_DATE_NAME);
                if (approximableDateElement != null)
                {
                    approximation = approximableDateElement.Attribute(XName.Get(APPROXIMATION_NAME, Namespace)).Value;
                }
                return (Util.Util.GetNonNullString(approximation));
            }
        }

        /// <summary>
        ///     Accessor for the searchableStart date (optional)
        /// </summary>
        public string SearchableStartString
        {
            get
            {
                string date = "";
                Element dateElement = GetChild(SEARCHABLE_DATE_NAME);
                if (dateElement != null)
                {
                    Element startElement = dateElement.Element(XName.Get(START_NAME, Namespace));
                    if (startElement != null)
                    {
                        date = Util.Util.GetNonNullString(startElement.Value);
                    }
                }
                return (date);
            }
        }

        /// <summary>
        ///     Accessor for the searchableEnd date (optional)
        /// </summary>
        public string SearchableEndString
        {
            get
            {
                string date = "";
                Element dateElement = GetChild(SEARCHABLE_DATE_NAME);
                if (dateElement != null)
                {
                    Element endElement = dateElement.Element(XName.Get(END_NAME, Namespace));
                    if (endElement != null)
                    {
                        date = Util.Util.GetNonNullString(endElement.Value);
                    }
                }
                return (date);
            }
        }

        /// <summary>
        ///     Validates an approximation against the allowed values.
        /// </summary>
        /// <param name="approximation"> the value to test </param>
        /// <exception cref="InvalidDDMSException"> if the value is null, empty or invalid. </exception>
        public static void ValidateApproximation(string approximation)
        {
            Util.Util.RequireDDMSValue("approximation", approximation);
            if (!APPROXIMATION_TYPES.Contains(approximation))
            {
                throw new InvalidDDMSException("The approximation must be one of " + APPROXIMATION_TYPES);
            }
        }

        /// <summary>
        ///     Validates an element name against the allowed values.
        /// </summary>
        /// <param name="name"> the value to test </param>
        /// <exception cref="InvalidDDMSException"> if the value is null, empty or invalid. </exception>
        public static void ValidateElementName(string name)
        {
            Util.Util.RequireDDMSValue("name", name);
            if (!NAME_TYPES.Contains(name))
            {
                throw new InvalidDDMSException("The element name must be one of " + NAME_TYPES);
            }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The name of the element has an appropriate value.</li>
        ///                 <li>If the approximableDate exists, it is an acceptable date format.</li>
        ///                 <li>If an approximation exists, it has an appropriate value.</li>
        ///                 <li>If start exists, it is a valid date format.</li>
        ///                 <li>If end exists, it is a valid date format.</li>
        ///                 <li>This component cannot be used until DDMS 4.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            ValidateElementName(Name);
            if (!String.IsNullOrEmpty(ApproximableDateString))
            {
                Util.Util.RequireDDMSDateFormat(ApproximableDateString, Namespace);
            }
            if (!String.IsNullOrEmpty(Approximation))
            {
                ValidateApproximation(Approximation);
            }
            if (!String.IsNullOrEmpty(SearchableStartString))
            {
                Util.Util.RequireDDMSDateFormat(SearchableStartString, Namespace);
            }
            if (!String.IsNullOrEmpty(SearchableEndString))
            {
                Util.Util.RequireDDMSDateFormat(SearchableEndString, Namespace);
            }

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.1");

            base.Validate();
        }

        /// <summary>
        ///     Validates any conditions that might result in a warning.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>A completely empty element was found.</li>
        ///                 <li>A description element can be used without any child text.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Description) && String.IsNullOrEmpty(ApproximableDateString) &&
                String.IsNullOrEmpty(Approximation) && String.IsNullOrEmpty(SearchableStartString) &&
                String.IsNullOrEmpty(SearchableEndString))
            {
                AddWarning("A completely empty " + QualifiedName + " element was found.");
            }
            if (GetChild(DESCRIPTION_NAME) != null && String.IsNullOrEmpty(Description))
            {
                AddWarning("A completely empty ddms:description element was found.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + "." + DESCRIPTION_NAME, Description));
            text.Append(BuildOutput(isHtml, localPrefix + "." + APPROXIMABLE_DATE_NAME, ApproximableDateString));
            text.Append(BuildOutput(isHtml, localPrefix + "." + APPROXIMABLE_DATE_NAME + "." + APPROXIMATION_NAME,
                Approximation));
            text.Append(BuildOutput(isHtml, localPrefix + "." + SEARCHABLE_DATE_NAME + "." + START_NAME,
                SearchableStartString));
            text.Append(BuildOutput(isHtml, localPrefix + "." + SEARCHABLE_DATE_NAME + "." + END_NAME,
                SearchableEndString));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is ApproximableDate))
            {
                return (false);
            }
            var test = (ApproximableDate) obj;
            return (Description.Equals(test.Description) && ApproximableDateString.Equals(test.ApproximableDateString) &&
                    Approximation.Equals(test.Approximation) && SearchableStartString.Equals(test.SearchableStartString) &&
                    SearchableEndString.Equals(test.SearchableEndString));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Description.GetHashCode();
            result = 7*result + ApproximableDateString.GetHashCode();
            result = 7*result + Approximation.GetHashCode();
            result = 7*result + SearchableStartString.GetHashCode();
            result = 7*result + SearchableEndString.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.1.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            internal const long SerialVersionUID = -7348511606867959470L;
            internal string _approximableDate;
            internal string _approximation;
            internal string _description;
            internal string _name;
            internal string _searchableEnd;
            internal string _searchableStart;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(ApproximableDate approximableDate)
            {
                Name = approximableDate.Name;
                Description = approximableDate.Description;
                ApproximableDate = approximableDate.ApproximableDateString;
                Approximation = approximableDate.Approximation;
                SearchableStart = approximableDate.SearchableStartString;
                SearchableEnd = approximableDate.SearchableEndString;
            }

            /// <summary>
            ///     Builder accessor for the name of the element
            /// </summary>
            public virtual string Name
            {
                get { return _name; }
                set { _name = value; }
            }


            /// <summary>
            ///     Builder accessor for the description
            /// </summary>
            public virtual string Description
            {
                get { return _description; }
                set { _description = value; }
            }


            /// <summary>
            ///     Builder accessor for the approximableDate
            /// </summary>
            public virtual string ApproximableDate
            {
                get { return _approximableDate; }
                set { _approximableDate = value; }
            }


            /// <summary>
            ///     Builder accessor for the approximation
            /// </summary>
            public virtual string Approximation
            {
                get { return _approximation; }
                set { _approximation = value; }
            }


            /// <summary>
            ///     Builder accessor for the searchableStart
            /// </summary>
            public virtual string SearchableStart
            {
                get { return _searchableStart; }
                set { _searchableStart = value; }
            }


            /// <summary>
            ///     Builder accessor for the searchableEnd
            /// </summary>
            public virtual string SearchableEnd
            {
                get { return _searchableEnd; }
                set { _searchableEnd = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                return (Empty
                    ? null
                    : new ApproximableDate(Name, Description, ApproximableDate, Approximation, SearchableStart,
                        SearchableEnd));
            }

            /// <summary>
            ///     Does not include the element name.
            /// </summary>
            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(Description) && String.IsNullOrEmpty(ApproximableDate) &&
                            String.IsNullOrEmpty(Approximation) && String.IsNullOrEmpty(SearchableStart) &&
                            String.IsNullOrEmpty(SearchableEnd));
                }
            }
        }
    }
}