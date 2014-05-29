#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Extensions;
using DDMSense.Util;

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

namespace DDMSense.DDMS.Summary
{
    #region usings

    using Element = XElement;
    using System.Globalization;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:temporalCoverage.
    ///     <para>
    ///         Before DDMS 4.0.1, a temporalCoverage element contains a locally defined TimePeriod construct.
    ///         This TimePeriod construct is a container for the name, start, and end values of a time period.
    ///         It exists only inside of a ddms:temporalCoverage parent, so it is not implemented as a Java object.
    ///         Starting in DDMS 4.0.1, the TimePeriod wrapper has been removed.
    ///     </para>
    ///     <para>
    ///         Starting in DDMS 4.1, the start and end dates may optionally be replaced by an approximableStart
    ///         or approximableEnd date.
    ///     </para>
    ///     <para>
    ///         To avoid confusion between the name of the temporalCoverage element and the name of the specified time period,
    ///         the latter is referred to as the "time period name".
    ///     </para>
    ///     <para>
    ///         If not "Not Applicable" or "Unknown", date formats must adhere to one of the DDMS-allowed date formats.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A time period name element can be used with no child text.</li>
    ///                     <li>A completely empty approximableStart or approximableEnd date can be used.</li>
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
    ///                 <u>ddms:name</u>: An interval of time, which can be expressed as a named era (0-1 optional,
    ///                 default=Unknown). Zero or
    ///                 1 of these elements may appear.<br />
    ///                 <u>ddms:start</u>: The start date of a period of time (exactly 1 optional, default=Unknown).<br />
    ///                 <u>ddms:end</u>: The end date of a period of time (exactly 1 optional, default=Unknown).<br />
    ///                 <u>ddms:approximableStart</u>: The approximable start date (exactly 1 optional)<br />
    ///                 <u>ddms:approximableEnd</u>: The approximable end date (exactly 1 optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and ownerProducer attributes are optional. (starting in DDMS 3.0)
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class TemporalCoverage : AbstractBaseComponent
    {
        private const string DEFAULT_VALUE = "Unknown";

        // The name of the TimePeriod element itself
        private const string TIME_PERIOD_NAME = "TimePeriod";

        // The name of the "name" element nested inside the temporalCoverage element 
        private const string TIME_PERIOD_NAME_NAME = "name";

        private const string START_NAME = "start";
        private const string END_NAME = "end";
        private const string APPROXIMABLE_START_NAME = "approximableStart";
        private const string APPROXIMABLE_END_NAME = "approximableEnd";
        private ApproximableDate _approximableEnd;
        private ApproximableDate _approximableStart;
        private string _name = DEFAULT_VALUE;
        private SecurityAttributes _securityAttributes;

        static TemporalCoverage()
        {
        }

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TemporalCoverage(Element element)
        {
            try
            {
                SetElement(element, false);
                Element periodElement = TimePeriodElement;
                if (periodElement != null)
                {
                    Element nameElement = periodElement.Element(XName.Get(TIME_PERIOD_NAME_NAME, Namespace));
                    if (nameElement != null && !String.IsNullOrEmpty(nameElement.Value))
                    {
                        _name = nameElement.Value;
                    }

                    Element approximableStart = element.Element(XName.Get(APPROXIMABLE_START_NAME, Namespace));
                    if (approximableStart != null)
                    {
                        _approximableStart = new ApproximableDate(approximableStart);
                    }
                    Element approximableEnd = element.Element(XName.Get(APPROXIMABLE_END_NAME, Namespace));
                    if (approximableEnd != null)
                    {
                        _approximableEnd = new ApproximableDate(approximableEnd);
                    }
                }
                _securityAttributes = new SecurityAttributes(element);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, using two exact date values.
        /// </summary>
        /// <param name="timePeriodName"> the time period name (optional) (if empty, defaults to Unknown) </param>
        /// <param name="startString"> a string representation of the date (required) (if empty, defaults to Unknown) </param>
        /// <param name="endString"> a string representation of the end date (required) (if empty, defaults to Unknown) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TemporalCoverage(string timePeriodName, string startString, string endString,
            SecurityAttributes securityAttributes)
            : this(timePeriodName, startString, null, endString, null, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, using an exact start date
        ///     and an approximable end date.
        /// </summary>
        /// <param name="timePeriodName"> the time period name (optional) (if empty, defaults to Unknown) </param>
        /// <param name="startString"> a string representation of the date (required) (if empty, defaults to Unknown) </param>
        /// <param name="approximableEnd"> the end date, as an approximable date (required) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TemporalCoverage(string timePeriodName, string startString, ApproximableDate approximableEnd,
            SecurityAttributes securityAttributes)
            : this(timePeriodName, startString, null, null, approximableEnd, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, using an approximable start date
        ///     and an exact end date.
        /// </summary>
        /// <param name="timePeriodName"> the time period name (optional) (if empty, defaults to Unknown) </param>
        /// <param name="approximableStart"> the start date, as an approximable date (required) </param>
        /// <param name="endString"> a string representation of the end date (required) (if empty, defaults to Unknown) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TemporalCoverage(string timePeriodName, ApproximableDate approximableStart, string endString,
            SecurityAttributes securityAttributes)
            : this(timePeriodName, null, approximableStart, endString, null, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, using two approximable dates.
        /// </summary>
        /// <param name="timePeriodName"> the time period name (optional) (if empty, defaults to Unknown) </param>
        /// <param name="approximableStart"> the start date, as an approximable date (required) </param>
        /// <param name="approximableEnd"> the end date, as an approximable date (required) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public TemporalCoverage(string timePeriodName, ApproximableDate approximableStart,
            ApproximableDate approximableEnd, SecurityAttributes securityAttributes)
            : this(timePeriodName, null, approximableStart, null, approximableEnd, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, which handles all permutations of exact and
        ///     approximable date formats.
        /// </summary>
        /// <param name="timePeriodName"> the time period name (optional) (if empty, defaults to Unknown) </param>
        /// <param name="startString"> a string representation of the date (optional) (if empty, defaults to Unknown) </param>
        /// <param name="approximableStart"> the start date, as an approximable date (optional) </param>
        /// <param name="endString"> a string representation of the end date (optional) (if empty, defaults to Unknown) </param>
        /// <param name="approximableEnd"> the end date, as an approximable date (optional) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        private TemporalCoverage(string timePeriodName, string startString, ApproximableDate approximableStart,
            string endString, ApproximableDate approximableEnd, SecurityAttributes securityAttributes)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);
                Element periodElement = DDMSVersion.CurrentVersion.IsAtLeast("4.0.1")
                    ? element
                    : Util.Util.BuildDDMSElement(TIME_PERIOD_NAME, null);
                if (!DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
                {
                    element.Add(periodElement);
                }
                if (!String.IsNullOrEmpty(timePeriodName))
                {
                    _name = timePeriodName;
                }
                Util.Util.AddDDMSChildElement(periodElement, TIME_PERIOD_NAME_NAME, timePeriodName);

                if (approximableStart != null)
                {
                    element.Add(approximableStart.ElementCopy);
                    _approximableStart = approximableStart;
                }
                else
                {
                    startString = (String.IsNullOrEmpty(startString) ? DEFAULT_VALUE : startString);
                    periodElement.Add(Util.Util.BuildDDMSElement(START_NAME, startString));
                }

                if (approximableEnd != null)
                {
                    element.Add(approximableEnd.ElementCopy);
                    _approximableEnd = approximableEnd;
                }
                else
                {
                    endString = (String.IsNullOrEmpty(endString) ? DEFAULT_VALUE : endString);
                    periodElement.Add(Util.Util.BuildDDMSElement(END_NAME, endString));
                }

                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getLocatorSuffix()"></see>
        protected internal override string LocatorSuffix
        {
            get
            {
                return (DDMSVersion.IsAtLeast("4.0.1")
                    ? ""
                    : ValidationMessage.ElementPrefix + Element.GetPrefix() + ":" + TIME_PERIOD_NAME);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(ApproximableStart);
                list.Add(ApproximableEnd);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the element which contains the time period name, start date, and end date. Before DDMS 4.0.1,
        ///     this is a wrapper element called ddms:TimePeriod. Starting in DDMS 4.0.1, it is the ddms:temporalCoverage
        ///     element itself.
        /// </summary>
        private Element TimePeriodElement
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? Element : GetChild(TIME_PERIOD_NAME)); }
        }

        /// <summary>
        ///     Accessor for the TimePeriod name element child text. Note that the getName() accessor will
        ///     return the local name of the temporal coverage element (temporalCoverage).
        /// </summary>
        public string TimePeriodName
        {
            get { return (_name); }
            set { _name = value; }
        }

        /// <summary>
        ///     Accessor for the XML calendar representing the start date
        /// </summary>
        /// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
        /// DateTime is no longer a sufficient representation. This accessor will return
        /// null for dates in the new format. Use
        /// <code>getStartString()</code>
        /// to
        /// access the raw XML format of the date, "Not Applicable", or "Unknown" values instead.
        public DateTime? Start
        {
            get
            {
                try
                {
                    return StartString.ToDDMSNullableDateTime();
                }
                catch (ArgumentException)
                {
                    return (null);
                }
            }
        }

        /// <summary>
        ///     Accessor for the start date as a string. If the value of start cannot be represented by an XML calendar, this
        ///     will return "Not Applicable" or "Unknown". Use <code>getStart</code> to work with this value as a calendar date.
        /// </summary>
        public string StartString
        {
            get
            {
                Element startElement = TimePeriodElement.Element(XName.Get(START_NAME, Namespace));
                if (startElement == null)
                {
                    return ("");
                }
                string value = startElement.Value;
                if (String.IsNullOrEmpty(value))
                {
                    return (DEFAULT_VALUE);
                }
                return (value);
                //		return (Util.isEmpty(value) ? DEFAULT_VALUE : value);
            }
        }

        /// <summary>
        ///     Accessor for the XML calendar representing the end date
        /// </summary>
        /// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
        /// DateTime is no longer a sufficient representation. This accessor will return
        /// null for dates in the new format. Use
        /// <code>getEndString()</code>
        /// to
        /// access the raw XML format of the date, "Not Applicable", or "Unknown" values instead.
        public DateTime? End
        {
            get
            {
                try
                {
                    return EndString.ToDDMSNullableDateTime();
                }
                catch (ArgumentException)
                {
                    return (null);
                }
            }
        }

        /// <summary>
        ///     Accessor for the end date as a string. If the value of end cannot be represented by an XML calendar, this will
        ///     return "Not Applicable" or "Unknown". Use <code>getEnd</code> to work with this value as a calendar date.
        /// </summary>
        public string EndString
        {
            get
            {
                Element endElement = TimePeriodElement.Element(XName.Get(END_NAME, Namespace));
                if (endElement == null)
                {
                    return ("");
                }
                string value = endElement.Value;
                return (String.IsNullOrEmpty(value) ? DEFAULT_VALUE : value);
            }
        }

        /// <summary>
        ///     Accessor for the approximableStart date.
        /// </summary>
        public ApproximableDate ApproximableStart
        {
            get { return (_approximableStart); }
            set { _approximableStart = value; }
        }

        /// <summary>
        ///     Accessor for the approximableStart date.
        /// </summary>
        public ApproximableDate ApproximableEnd
        {
            get { return (_approximableEnd); }
            set { _approximableEnd = value; }
        }

        /// <summary>
        ///     Accessor for the Security Attributes.  Will always be non-null, even if it has no values set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>If start exists, it is a valid date format.</li>
        ///                 <li>If end exists, it is a valid date format.</li>
        ///                 <li>0-1 names, start, end, approximableStart, approximableEnd exist.</li>
        ///                 <li>The SecurityAttributes do not exist until DDMS 3.0 or later.</li>
        ///                 <li>approximableStart and approximableEnd do not exist until DDMS 4.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException">  if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Element periodElement = TimePeriodElement;
            Util.Util.RequireDDMSValue("TimePeriod element", periodElement);
            Util.Util.RequireBoundedChildCount(periodElement, TIME_PERIOD_NAME_NAME, 0, 1);
            Util.Util.RequireBoundedChildCount(periodElement, START_NAME, 0, 1);
            Util.Util.RequireBoundedChildCount(periodElement, END_NAME, 0, 1);
            Util.Util.RequireBoundedChildCount(periodElement, APPROXIMABLE_START_NAME, 0, 1);
            Util.Util.RequireBoundedChildCount(periodElement, APPROXIMABLE_END_NAME, 0, 1);
            if (ApproximableStart == null)
            {
                Util.Util.RequireDDMSValue("start", StartString);
                if (!Util.Util.EXTENDED_DATE_TYPES.Contains(StartString))
                {
                    Util.Util.RequireDDMSDateFormat(StartString, Namespace);
                }
            }
            if (ApproximableEnd == null)
            {
                Util.Util.RequireDDMSValue("end", EndString);
                if (!Util.Util.EXTENDED_DATE_TYPES.Contains(EndString))
                {
                    Util.Util.RequireDDMSDateFormat(EndString, Namespace);
                }
            }

            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("3.0") && !SecurityAttributes.Empty)
            {
                throw new InvalidDDMSException(
                    "Security attributes cannot be applied to this component until DDMS 3.0 or later.");
            }
            if (!DDMSVersion.IsAtLeast("4.1") && (ApproximableStart != null || ApproximableEnd != null))
            {
                throw new InvalidDDMSException("Approximable dates cannot be used until DDMS 4.1 or later.");
            }

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
        ///                 <li>A ddms:name element was found with no value.</li>
        ///                 <li>A ddms:approximableStart or ddms:approximableEnd element may cause issues for DDMS 4.0 records.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            Element periodElement = TimePeriodElement;
            Element timePeriodName =
                periodElement.Element(XName.Get(TIME_PERIOD_NAME_NAME, periodElement.Name.NamespaceName));
            if (timePeriodName != null && String.IsNullOrEmpty(timePeriodName.Value))
            {
                AddWarning("A ddms:name element was found with no value. Defaulting to \"" + DEFAULT_VALUE + "\".");
            }
            if (ApproximableStart != null || ApproximableEnd != null)
            {
                AddDdms40Warning("ddms:approximableStart or ddms:approximableEnd element");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            if (!DDMSVersion.IsAtLeast("4.0.1"))
            {
                localPrefix += TIME_PERIOD_NAME + ".";
            }
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + TIME_PERIOD_NAME_NAME, TimePeriodName));
            text.Append(BuildOutput(isHtml, localPrefix + START_NAME, StartString));
            text.Append(BuildOutput(isHtml, localPrefix + END_NAME, EndString));
            if (ApproximableStart != null)
            {
                text.Append(ApproximableStart.GetOutput(isHtml, localPrefix, ""));
            }
            if (ApproximableEnd != null)
            {
                text.Append(ApproximableEnd.GetOutput(isHtml, localPrefix, ""));
            }
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is TemporalCoverage))
            {
                return (false);
            }
            var test = (TemporalCoverage) obj;
            return (TimePeriodName.Equals(test.TimePeriodName) && StartString.Equals(test.StartString) &&
                    EndString.Equals(test.EndString));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + TimePeriodName.GetHashCode();
            result = 7*result + StartString.GetHashCode();
            result = 7*result + EndString.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("temporalCoverage");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            
            internal ApproximableDate.Builder _approximableEnd;
            internal ApproximableDate.Builder _approximableStart;
            internal string _endString;
            internal SecurityAttributes.Builder _securityAttributes;
            internal string _startString;
            internal string _timePeriodName;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(TemporalCoverage coverage)
            {
                TimePeriodName = coverage.TimePeriodName;
                StartString = coverage.StartString;
                EndString = coverage.EndString;
                if (coverage.ApproximableStart != null)
                {
                    ApproximableStart = new ApproximableDate.Builder(coverage.ApproximableStart);
                }
                if (coverage.ApproximableEnd != null)
                {
                    ApproximableEnd = new ApproximableDate.Builder(coverage.ApproximableEnd);
                }
                SecurityAttributes = new SecurityAttributes.Builder(coverage.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the TimePeriod name element child text.
            /// </summary>
            public virtual string TimePeriodName
            {
                get { return _timePeriodName; }
                set { _timePeriodName = value; }
            }


            /// <summary>
            ///     Builder accessor for the start date as a string.
            /// </summary>
            public virtual string StartString
            {
                get { return _startString; }
                set { _startString = value; }
            }


            /// <summary>
            ///     Builder accessor for the end date as a string.
            /// </summary>
            public virtual string EndString
            {
                get { return _endString; }
                set { _endString = value; }
            }


            /// <summary>
            ///     Builder accessor for the approximableStart
            /// </summary>
            public virtual ApproximableDate.Builder ApproximableStart
            {
                get
                {
                    if (_approximableStart == null)
                    {
                        _approximableStart = new ApproximableDate.Builder();
                        _approximableStart.Name = APPROXIMABLE_START_NAME;
                    }
                    return _approximableStart;
                }
                set { _approximableStart = value; }
            }


            /// <summary>
            ///     Builder accessor for the approximableEnd
            /// </summary>
            public virtual ApproximableDate.Builder ApproximableEnd
            {
                get
                {
                    if (_approximableEnd == null)
                    {
                        _approximableEnd = new ApproximableDate.Builder();
                        _approximableEnd.Name = APPROXIMABLE_END_NAME;
                    }
                    return _approximableEnd;
                }
                set { _approximableEnd = value; }
            }


            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes
            {
                get
                {
                    if (_securityAttributes == null)
                    {
                        _securityAttributes = new SecurityAttributes.Builder();
                    }
                    return _securityAttributes;
                }
                set { _securityAttributes = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                if (!ApproximableStart.Empty && !String.IsNullOrEmpty(StartString))
                {
                    throw new InvalidDDMSException("Only 1 of start or approximableStart can be used.");
                }
                if (!ApproximableEnd.Empty && !String.IsNullOrEmpty(EndString))
                {
                    throw new InvalidDDMSException("Only 1 of end or approximableEnd can be used.");
                }
                return
                    (new TemporalCoverage(TimePeriodName, StartString, (ApproximableDate) ApproximableStart.Commit(),
                        EndString, (ApproximableDate) ApproximableEnd.Commit(), SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(TimePeriodName) && String.IsNullOrEmpty(StartString) &&
                            String.IsNullOrEmpty(EndString) && ApproximableStart.Empty && ApproximableEnd.Empty &&
                            SecurityAttributes.Empty);
                }
            }
        }
    }
}