using System;
using System.Collections.Generic;
using System.Text;

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
namespace DDMSSense.DDMS.Metacard {


	using Element = System.Xml.Linq.XElement;

    using Contributor = DDMSSense.DDMS.ResourceElements.Contributor;
    using Creator = DDMSSense.DDMS.ResourceElements.Creator;
    using Dates = DDMSSense.DDMS.ResourceElements.Dates;
    using Identifier = DDMSSense.DDMS.ResourceElements.Identifier;
    using PointOfContact = DDMSSense.DDMS.ResourceElements.PointOfContact;
    using ProcessingInfo = DDMSSense.DDMS.ResourceElements.ProcessingInfo;
    using Publisher = DDMSSense.DDMS.ResourceElements.Publisher;
    using RecordsManagementInfo = DDMSSense.DDMS.ResourceElements.RecordsManagementInfo;
    using RevisionRecall = DDMSSense.DDMS.ResourceElements.RevisionRecall;
	using NoticeList = DDMSSense.DDMS.SecurityElements.NoticeList;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using Access = DDMSSense.DDMS.SecurityElements.Ntk.Access;
	using Description = DDMSSense.DDMS.Summary.Description;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:metacardInfo.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:identifier</u>: (1-many required), implemented as an <seealso cref="Identifier"/><br />
	/// <u>ddms:dates</u>: (exactly 1 required), implemented as an <seealso cref="Dates"/><br />
	/// <u>ddms:contributor</u>: (0-many optional), implemented as a <seealso cref="Contributor"/><br />
	/// <u>ddms:creator</u>: (0-many optional), implemented as a <seealso cref="Creator"/><br />
	/// <u>ddms:pointOfContact</u>: (0-many optional), implemented as a <seealso cref="PointOfContact"/><br />
	/// <u>ddms:publisher</u>: (1-many required), implemented as a <seealso cref="Publisher"/><br />
	/// <u>ddms:description</u>: (0-1 optional), implemented as a <seealso cref="Description"/><br />
	/// <u>ddms:processingInfo</u>: (0-many optional), implemented as a <seealso cref="ProcessingInfo"/><br />
	/// <u>ddms:revisionRecall</u>: (0-1 optional), implemented as a <seealso cref="RevisionRecall"/><br />
	/// <u>ddms:recordsManagementInfo</u>: (0-1 optional), implemented as a <seealso cref="RecordsManagementInfo"/><br />
	/// <u>ddms:noticeList</u>: (0-1 optional), implemented as a <seealso cref="NoticeList"/><br />
	/// <u>ntk:Access</u>: Need-To-Know access information (optional, starting in DDMS 4.1)<br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are optional.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class MetacardInfo : AbstractBaseComponent {

		private List<Identifier> _identifiers = new List<Identifier>();
		private Dates _dates = null;
		private List<Contributor> _contributors = new List<Contributor>();
		private List<Creator> _creators = new List<Creator>();
		private List<PointOfContact> _pointOfContacts = new List<PointOfContact>();
		private List<Publisher> _publishers = new List<Publisher>();
		private Description _description = null;
		private List<ProcessingInfo> _processingInfos = new List<ProcessingInfo>();
		private RevisionRecall _revisionRecall = null;
		private RecordsManagementInfo _recordsManagementInfo = null;
		private NoticeList _noticeList = null;
		private Access _access = null;
		private SecurityAttributes _securityAttributes = null;
		private List<IDDMSComponent> _orderedList = new List<IDDMSComponent>();

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public MetacardInfo(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public MetacardInfo(Element element) {
			try {
				SetXOMElement(element, false);
				DDMSVersion version = DDMSVersion;
				_identifiers = new List<Identifier>();
				IEnumerable<Element> components = element.Elements(XName.Get(Identifier.GetName(version), Namespace));
                foreach(var comp in components) 
                    _identifiers.Add(new Identifier(comp));
				
                Element component = element.Element(XName.Get(Dates.GetName(version), Namespace));
                if (component != null) 
					_dates = new Dates(component);
				
                components = element.Elements(XName.Get(Creator.GetName(version), Namespace));
			    foreach(var comp in components)
					_creators.Add(new Creator(comp));
				
                components = element.Elements(XName.Get(Publisher.GetName(version), Namespace));
				foreach(var comp in components)
					_publishers.Add(new Publisher(comp));
				
                components = element.Elements(XName.Get(Contributor.GetName(version), Namespace));
				foreach(var comp in components)
					_contributors.Add(new Contributor(comp));
				
                components = element.Elements(XName.Get(PointOfContact.GetName(version), Namespace));
				foreach(var comp in components)
					_pointOfContacts.Add(new PointOfContact(comp));
				
                component = element.Element(XName.Get(Description.GetName(version), Namespace));
				if (component != null) {
					_description = new Description(component);
				}
				components = element.Elements(XName.Get(ProcessingInfo.GetName(version), Namespace));
				foreach(var comp in components)
					_processingInfos.Add(new ProcessingInfo(comp));
				
                component = element.Element(XName.Get(RevisionRecall.GetName(DDMSVersion), Namespace));
				if (component != null) 
					_revisionRecall = new RevisionRecall(component);
				
                component = element.Element(XName.Get(RecordsManagementInfo.GetName(DDMSVersion), Namespace));
				if (component != null) 
					_recordsManagementInfo = new RecordsManagementInfo(component);
				
                component = element.Element(XName.Get(NoticeList.GetName(DDMSVersion), Namespace));
				if (component != null) 
					_noticeList = new NoticeList(component);

				component = element.Element(XName.Get(Access.GetName(DDMSVersion), DDMSVersion.NtkNamespace));
				if (component != null) 
					_access = new Access(component);
			
				_securityAttributes = new SecurityAttributes(element);
				PopulatedOrderedList();
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// 
		/// <para>Because there are so many possible components in a MetacardInfo instance, they are passed in as a collection, 
		/// similar to the approach used for top-level components in a Resource. If any component does not belong in a 
		/// MetacardInfo instance, an InvalidDDMSException will be thrown.</para>
		/// 
		/// <para>The order of different types of components does not matter here. However, if multiple instances of the same 
		/// component type exist in the list (such as multiple identifier components), those components will be stored and 
		/// output in the order of the list. If only 1 instance can be supported, the last one in the list will be the 
		/// one used.</para>
		/// </summary>
		/// <param name="childComponents"> any components that belong in this MetacardInfo (required) </param>
		/// <param name="securityAttributes"> security attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public MetacardInfo(java.util.List<DDMSSense.DDMS.IDDMSComponent> childComponents, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public MetacardInfo(List<IDDMSComponent> childComponents, SecurityAttributes securityAttributes) {
			try {
				if (childComponents == null) {
					childComponents = new List<IDDMSComponent>();
				}

				Element element = Util.BuildDDMSElement(MetacardInfo.GetName(DDMSVersion.GetCurrentVersion()), null);
				SetXOMElement(element, false);

				foreach (IDDMSComponent component in childComponents) {
					if (component == null) {
						continue;
					}

					if (component is Identifier) {
						_identifiers.Add((Identifier) component);
					} else if (component is Dates) {
						_dates = (Dates) component;
					} else if (component is Contributor) {
						_contributors.Add((Contributor) component);
					} else if (component is Creator) {
						_creators.Add((Creator) component);
					} else if (component is PointOfContact) {
						_pointOfContacts.Add((PointOfContact) component);
					} else if (component is Publisher) {
						_publishers.Add((Publisher) component);
					} else if (component is Description) {
						_description = (Description) component;
					} else if (component is ProcessingInfo) {
						_processingInfos.Add((ProcessingInfo) component);
					} else if (component is RevisionRecall) {
						_revisionRecall = (RevisionRecall) component;
					} else if (component is RecordsManagementInfo) {
						_recordsManagementInfo = (RecordsManagementInfo) component;
					} else if (component is NoticeList) {
						_noticeList = (NoticeList) component;
					} else if (component is Access) {
						_access = (Access) component;
					} else {
						throw new InvalidDDMSException(component.Name + " is not a valid child component in a metacardInfo element.");
					}
				}
				PopulatedOrderedList();
				foreach (IDDMSComponent component in NestedComponents) {
					element.Add(component.XOMElementCopy);
				}
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Creates an ordered list of all the child components in this MetacardInfo, for ease of traversal.
		/// </summary>
		private void PopulatedOrderedList() {
			_orderedList.AddRange(Identifiers);
			if (Dates != null) {
				_orderedList.Add(Dates);
			}
			_orderedList.AddRange(Publishers);
			_orderedList.AddRange(Contributors);
			_orderedList.AddRange(Creators);
			_orderedList.AddRange(PointOfContacts);
			if (Description != null) {
				_orderedList.Add(Description);
			}
			_orderedList.AddRange(ProcessingInfos);
			if (RevisionRecall != null) {
				_orderedList.Add(RevisionRecall);
			}
			if (RecordsManagementInfo != null) {
				_orderedList.Add(RecordsManagementInfo);
			}
			if (NoticeList != null) {
				_orderedList.Add(NoticeList);
			}
			if (Access != null) {
				_orderedList.Add(Access);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>At least 1 identifier and publisher must exist.</li>
		/// <li>Only 1 dates can exist.</li>
		/// <li>Only 0-1 descriptions, revisionRecalls, recordsManagementInfos, or noticeLists can exist.</li>
		/// <li>This component cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, MetacardInfo.GetName(DDMSVersion));
			if (Identifiers.Count == 0) {
				throw new InvalidDDMSException("At least one ddms:identifier must exist within a ddms:metacardInfo element.");
			}
			if (Publishers.Count == 0) {
				throw new InvalidDDMSException("At least one ddms:publisher must exist within a ddms:metacardInfo element.");
			}
			Util.RequireBoundedChildCount(Element, Dates.GetName(DDMSVersion), 1, 1);
			Util.RequireBoundedChildCount(Element, Description.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, RevisionRecall.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, RecordsManagementInfo.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, NoticeList.GetName(DDMSVersion), 0, 1);

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>An ntk:Access element may cause issues for DDMS 4.0 records.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (Access != null) {
				AddDdms40Warning("ntk:Access element");
			}

			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();

			// Traverse child components, suppressing the resource prefix
			text.Append(BuildOutput(isHTML, localPrefix, Identifiers));
			if (Dates != null) {
				text.Append(Dates.GetOutput(isHTML, localPrefix, ""));
			}
			text.Append(BuildOutput(isHTML, localPrefix, Publishers));
			text.Append(BuildOutput(isHTML, localPrefix, Contributors));
			text.Append(BuildOutput(isHTML, localPrefix, Creators));
			text.Append(BuildOutput(isHTML, localPrefix, PointOfContacts));
			if (Description != null) {
				text.Append(Description.GetOutput(isHTML, localPrefix, ""));
			}
			text.Append(BuildOutput(isHTML, localPrefix, ProcessingInfos));
			if (RevisionRecall != null) {
				text.Append(RevisionRecall.GetOutput(isHTML, localPrefix, ""));
			}
			if (RecordsManagementInfo != null) {
				text.Append(RecordsManagementInfo.GetOutput(isHTML, localPrefix, ""));
			}
			if (NoticeList != null) {
				text.Append(NoticeList.GetOutput(isHTML, localPrefix, ""));
			}
			if (Access != null) {
				text.Append(Access.GetOutput(isHTML, localPrefix, ""));
			}

			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				return (ChildComponents);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is MetacardInfo)) {
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
			return ("metacardInfo");
		}

		/// <summary>
		/// Accessor for an ordered list of the components in this metcardInfo. Components which are missing are not represented
		/// in this list (no null entries).
		/// </summary>
		public List<IDDMSComponent> ChildComponents {
			get {
				return _orderedList;
			}
		}

		/// <summary>
		/// Accessor for a list of all identifiers
		/// </summary>
		public List<Identifier> Identifiers {
			get {
				return _identifiers;
			}
		}

		/// <summary>
		/// Accessor for the dates
		/// </summary>
		public Dates Dates {
			get {
				return _dates;
			}
			set {
					_dates = value;
			}
		}

		/// <summary>
		/// Accessor for a list of all Contributor entities (0-many)
		/// </summary>
		public List<Contributor> Contributors {
			get {
				return _contributors;
			}
		}

		/// <summary>
		/// Accessor for a list of all Creator entities (0-many)
		/// </summary>
		public List<Creator> Creators {
			get {
				return _creators;
			}
		}

		/// <summary>
		/// Accessor for a list of all PointOfContact entities (0-many)
		/// </summary>
		public List<PointOfContact> PointOfContacts {
			get {
				return _pointOfContacts;
			}
		}

		/// <summary>
		/// Accessor for a list of all Publisher entities (0-many)
		/// </summary>
		public List<Publisher> Publishers {
			get {
				return _publishers;
			}
		}

		/// <summary>
		/// Accessor for the description
		/// </summary>
		public Description Description {
			get {
				return _description;
			}
			set {
					_description = value;
			}
		}

		/// <summary>
		/// Accessor for the processing information
		/// </summary>
		public List<ProcessingInfo> ProcessingInfos {
			get {
				return _processingInfos;
			}
		}

		/// <summary>
		/// Accessor for the revisionRecall
		/// </summary>
		public RevisionRecall RevisionRecall {
			get {
				return _revisionRecall;
			}
			set {
					_revisionRecall = value;
			}
		}

		/// <summary>
		/// Accessor for the recordsManagementInfo
		/// </summary>
		public RecordsManagementInfo RecordsManagementInfo {
			get {
				return _recordsManagementInfo;
			}
			set {
					_recordsManagementInfo = value;
			}
		}

		/// <summary>
		/// Accessor for the noticeList
		/// </summary>
		public NoticeList NoticeList {
			get {
				return _noticeList;
			}
			set {
					_noticeList = value;
			}
		}

		/// <summary>
		/// Accessor for the Access. May be null.
		/// </summary>
		public Access Access {
			get {
				return (_access);
			}
			set {
					_access = value;
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
			internal const long SerialVersionUID = 7851044806424206976L;
			internal List<Identifier.Builder> _identifiers;
			internal Dates.Builder _dates;
			internal List<Contributor.Builder> _contributors;
			internal List<Creator.Builder> _creators;
			internal List<PointOfContact.Builder> _pointOfContacts;
			internal List<Publisher.Builder> _publishers;
			internal Description.Builder _description;
			internal List<ProcessingInfo.Builder> _processingInfos;
			internal RevisionRecall.Builder _revisionRecall;
			internal RecordsManagementInfo.Builder _recordsManagementInfo;
			internal NoticeList.Builder _noticeList;
			internal Access.Builder _access;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(MetacardInfo metacardInfo) {
				foreach (IDDMSComponent component in metacardInfo.ChildComponents) {
					if (component is Identifier) {
						Identifiers.Add(new Identifier.Builder((Identifier) component));
					} else if (component is Dates) {
						Dates = new Dates.Builder((Dates) component);
					} else if (component is Creator) {
						Creators.Add(new Creator.Builder((Creator) component));
					} else if (component is Contributor) {
						Contributors.Add(new Contributor.Builder((Contributor) component));
					} else if (component is Publisher) {
						Publishers.Add(new Publisher.Builder((Publisher) component));
					} else if (component is PointOfContact) {
						PointOfContacts.Add(new PointOfContact.Builder((PointOfContact) component));
					} else if (component is Description) {
						Description = new Description.Builder((Description) component);
					} else if (component is ProcessingInfo) {
						ProcessingInfos.Add(new ProcessingInfo.Builder((ProcessingInfo) component));
					} else if (component is RevisionRecall) {
						RevisionRecall = new RevisionRecall.Builder((RevisionRecall) component);
					} else if (component is RecordsManagementInfo) {
						RecordsManagementInfo = new RecordsManagementInfo.Builder((RecordsManagementInfo) component);
					} else if (component is NoticeList) {
						NoticeList = new NoticeList.Builder((NoticeList) component);
					} else if (component is Access) {
						Access = new Access.Builder((Access) component);
					}
				}
				SecurityAttributes = new SecurityAttributes.Builder(metacardInfo.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public MetacardInfo commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual MetacardInfo Commit() {
				if (Empty) {
					return (null);
				}
				List<IDDMSComponent> childComponents = new List<IDDMSComponent>();
				foreach (IBuilder builder in ChildBuilders) {
					IDDMSComponent component = builder.Commit();
					if (component != null) {
						childComponents.Add(component);
					}
				}
				return (new MetacardInfo(childComponents, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in ChildBuilders) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Convenience method to get every child Builder in this Builder.
			/// </summary>
			/// <returns> a list of IBuilders </returns>
			internal virtual List<IBuilder> ChildBuilders {
				get {
					List<IBuilder> list = new List<IBuilder>();
					list.AddRange(Identifiers);
					list.AddRange(Publishers);
					list.AddRange(Contributors);
					list.AddRange(Creators);
					list.AddRange(PointOfContacts);
					list.AddRange(ProcessingInfos);
					list.Add(Dates);
					list.Add(Description);
					list.Add(RevisionRecall);
					list.Add(RecordsManagementInfo);
					list.Add(NoticeList);
					list.Add(Access);
					return (list);
				}
			}

			/// <summary>
			/// Builder accessor for the taskingInfos
			/// </summary>
			public virtual List<Identifier.Builder> Identifiers {
				get {
					if (_identifiers == null) {
						_identifiers = new List<Identifier.Builder>();
					}
					return _identifiers;
				}
			}

			/// <summary>
			/// Builder accessor for the dates
			/// </summary>
			public virtual Dates.Builder Dates {
				get {
					if (_dates == null) {
						_dates = new Dates.Builder();
					}
					return _dates;
				}
                set { _dates = value; }
			}


			/// <summary>
			/// Builder accessor for creators
			/// </summary>
			public virtual List<Creator.Builder> Creators {
				get {
					if (_creators == null) {
						_creators = new List<Creator.Builder>();
					}
					return _creators;
				}
			}

			/// <summary>
			/// Builder accessor for contributors
			/// </summary>
			public virtual List<Contributor.Builder> Contributors {
				get {
					if (_contributors == null) {
                        _contributors = new List<Contributor.Builder>();
					}
					return _contributors;
				}
			}

			/// <summary>
			/// Builder accessor for publishers
			/// </summary>
			public virtual List<Publisher.Builder> Publishers {
				get {
					if (_publishers == null) {
						_publishers = new List<Publisher.Builder>();
					}
					return _publishers;
				}
			}

			/// <summary>
			/// Builder accessor for points of contact
			/// </summary>
			public virtual List<PointOfContact.Builder> PointOfContacts {
				get {
					if (_pointOfContacts == null) {
						_pointOfContacts = new List<PointOfContact.Builder>();
					}
					return _pointOfContacts;
				}
			}

			/// <summary>
			/// Builder accessor for the description
			/// </summary>
			public virtual Description.Builder Description {
				get {
					if (_description == null) {
						_description = new Description.Builder();
					}
					return _description;
				}
                set { _description = value; }
			}


			/// <summary>
			/// Builder accessor for the processingInfos
			/// </summary>
			public virtual List<ProcessingInfo.Builder> ProcessingInfos {
				get {
					if (_processingInfos == null) {
						_processingInfos = new List<ProcessingInfo.Builder>();
					}
					return _processingInfos;
				}
                set { _processingInfos = value; }
			}

			/// <summary>
			/// Builder accessor for the revisionRecall
			/// </summary>
			public virtual RevisionRecall.Builder RevisionRecall {
				get {
					if (_revisionRecall == null) {
						_revisionRecall = new RevisionRecall.Builder();
					}
					return _revisionRecall;
				}
                set { _revisionRecall = value; }
			}


			/// <summary>
			/// Builder accessor for the recordsManagementInfo
			/// </summary>
			public virtual RecordsManagementInfo.Builder RecordsManagementInfo {
				get {
					if (_recordsManagementInfo == null) {
						_recordsManagementInfo = new RecordsManagementInfo.Builder();
					}
					return _recordsManagementInfo;
				}
                set { _recordsManagementInfo = value; }
			}


			/// <summary>
			/// Builder accessor for the noticeList
			/// </summary>
			public virtual NoticeList.Builder NoticeList {
				get {
					if (_noticeList == null) {
						_noticeList = new NoticeList.Builder();
					}
					return _noticeList;
				}
                set { _noticeList = value; }
			}


			/// <summary>
			/// Builder accessor for the access
			/// </summary>
			public virtual Access.Builder Access {
				get {
					if (_access == null) {
						_access = new Access.Builder();
					}
					return _access;
				}
                set { _access = value; }
			}


			/// <summary>
			/// Builder accessor for the Security Attributes
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