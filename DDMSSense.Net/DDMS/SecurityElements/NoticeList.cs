using System;
using System.Collections.Generic;
using System.Text;
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
namespace DDMSSense.DDMS.SecurityElements {


	using Element = System.Xml.Linq.XElement;
	
	using Notice = DDMSSense.DDMS.SecurityElements.Ism.Notice;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using DDMSSense.DDMS;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:noticeList.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ISM:Notice</u>: A collection of IC notices (1-to-many required), implemented as <seealso cref="Notice"/>s<br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class NoticeList : AbstractBaseComponent {

		private List<Notice> _notices = null;
		private SecurityAttributes _securityAttributes = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public NoticeList(Element element) {
			try {
				SetXOMElement(element, false);
				_notices = new List<Notice>();
				IEnumerable<Element> notices = element.Elements(XName.Get(Notice.GetName(DDMSVersion), DDMSVersion.IsmNamespace));
				notices.ToList().ForEach(p=> _notices.Add(new Notice(p)));
				
				_securityAttributes = new SecurityAttributes(element);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="notices"> the notices (at least 1 required) </param>
		/// <param name="securityAttributes"> any security attributes (classification and ownerProducer are optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public NoticeList(List<Notice> notices, SecurityAttributes securityAttributes) {
			try {
				if (notices == null) {
					notices = new List<Notice>();
				}

				DDMSVersion version = DDMSVersion.GetCurrentVersion();
				Element element = Util.BuildDDMSElement(NoticeList.GetName(version), null);
				foreach (Notice noticeText in notices) {
					element.Add(noticeText.XOMElementCopy);
				}

				_notices = notices;
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>At least 1 Notice exists.</li>
		/// <li>This component cannot be used until DDMS 4.0.1 or later.</li>
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, NoticeList.GetName(DDMSVersion));

			if (Notices.Count == 0) {
				throw new InvalidDDMSException("At least one ISM:Notice must exist within a ddms:noticeList element.");
			}
			Util.RequireDDMSValue("security attributes", SecurityAttributes);
			SecurityAttributes.RequireClassification();

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, Notices));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(Notices);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is NoticeList)) {
				return (false);
			}
			return (true);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("noticeList");
		}

		/// <summary>
		/// Accessor for the list of Notices.
		/// </summary>
		public List<Notice> Notices {
			get {
				return _notices;
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
		/// </summary>
		public override SecurityAttributes SecurityAttributes {
			get {
				return (_securityAttributes);
			}
			set {
					_securityAttributes = value;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 7750664735441105296L;
			internal List<Notice.Builder> _notices;
			internal SecurityAttributes.Builder _securityAttributes = null;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(NoticeList notice) {
				foreach (Notice noticeText in notice.Notices) {
					Notices.Add(new Notice.Builder(noticeText));
				}
				SecurityAttributes = new SecurityAttributes.Builder(notice.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				List<Notice> notices = new List<Notice>();
				foreach (IBuilder builder in Notices) {
					Notice component = (Notice) builder.Commit();
					if (component != null) {
						notices.Add(component);
					}
				}
				return (new NoticeList(notices, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in Notices) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the notices
			/// </summary>
			public virtual List<Notice.Builder> Notices {
				get {
					if (_notices == null) {
                        _notices = new List<Notice.Builder>();
					}
					return _notices;
				}
			}

			/// <summary>
			/// Builder accessor for the securityAttributes
			/// </summary>
			public virtual SecurityAttributes.Builder SecurityAttributes {
				get {
					if (_securityAttributes == null) {
						_securityAttributes = new SecurityAttributes.Builder();
					}
					return _securityAttributes;
				}
                set { _securityAttributes = value; }
			}

		}
	}
}