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
namespace DDMSSense.DDMS.ResourceElements {


	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using Description = DDMSSense.DDMS.Summary.Description;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:taskingInfo.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:requesterInfo</u>: Information about the requester of the production of this resource (0-many optional), 
	/// implemented as a <seealso cref="RequesterInfo"/>.<br />
	/// <u>ddms:addressee</u>: The addressee for this tasking (0-many optional), implemented as a <seealso cref="Addressee"/>
	/// <br />
	/// <u>ddms:description</u>: A description of this tasking (0-1, optional), implemented as a <seealso cref="Description"/><br />
	/// <u>ddms:taskID</u>: The task ID for this tasking (required), implemented as a <seealso cref="TaskID"/><br /> 
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	///  <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class TaskingInfo : AbstractBaseComponent {

		private List<RequesterInfo> _requesterInfos = null;
		private List<Addressee> _addressees = null;
		private Description _description = null;
		private TaskID _taskID = null;
		private SecurityAttributes _securityAttributes = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public TaskingInfo(Element element) {
			try {
				Util.RequireDDMSValue("element", element);
				SetXOMElement(element, false);

				_requesterInfos = new List<RequesterInfo>();
				IEnumerable<Element> infos = element.Elements(XName.Get(RequesterInfo.GetName(DDMSVersion), Namespace));
				foreach(var info in infos)
					_requesterInfos.Add(new RequesterInfo(info));

				_addressees = new List<Addressee>();
				IEnumerable<Element> addressees = element.Elements(XName.Get(Addressee.GetName(DDMSVersion), Namespace));
				foreach(var addressee in addressees)
					_addressees.Add(new Addressee(addressee));

				Element description = element.Element(XName.Get(Description.GetName(DDMSVersion), Namespace));
				if (description != null) {
					_description = new Description(description);
				}
				Element taskID = element.Element(XName.Get(TaskID.GetName(DDMSVersion), Namespace));
				if (taskID != null) {
					_taskID = new TaskID(taskID);
				}
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
		/// <param name="requesterInfos"> list of requestors (optional) </param>
		/// <param name="addressees"> list of addressee (optional) </param>
		/// <param name="description"> description of tasking (optional) </param>
		/// <param name="taskID"> taskID for tasking (required) </param>
		/// <param name="securityAttributes"> any security attributes (required) </param>
		/// <exception cref="InvalidDDMSException"> </exception>


		public TaskingInfo(List<RequesterInfo> requesterInfos, List<Addressee> addressees, Description description, TaskID taskID, SecurityAttributes securityAttributes) {
			try {
				if (requesterInfos == null) {
					requesterInfos = new List<RequesterInfo>();
				}
				if (addressees == null) {
					addressees = new List<Addressee>();
				}

				Element element = Util.BuildDDMSElement(TaskingInfo.GetName(DDMSVersion.GetCurrentVersion()), null);
				SetXOMElement(element, false);
				foreach (RequesterInfo info in requesterInfos) {
					element.Add(info.XOMElementCopy);
				}
				foreach (Addressee addressee in addressees) {
					element.Add(addressee.XOMElementCopy);
				}
				if (description != null) {
					element.Add(description.XOMElementCopy);
				}
				if (taskID != null) {
					element.Add(taskID.XOMElementCopy);
				}

				_requesterInfos = requesterInfos;
				_addressees = addressees;
				_description = description;
				_taskID = taskID;
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				Validate();
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
		/// <li>A TaskID exists.</li>
		/// <li>Exactly 1 taskID, and 0-1 descriptions exist.</li>
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// <li>This component cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, TaskingInfo.GetName(DDMSVersion));
			Util.RequireDDMSValue("taskID", TaskID);
			Util.RequireBoundedChildCount(Element, Description.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, TaskID.GetName(DDMSVersion), 1, 1);
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
			text.Append(BuildOutput(isHTML, localPrefix, RequesterInfos));
			text.Append(BuildOutput(isHTML, localPrefix, Addressees));
			if (Description != null) {
				text.Append(Description.GetOutput(isHTML, localPrefix, ""));
			}
			text.Append(TaskID.GetOutput(isHTML, localPrefix, ""));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(RequesterInfos);
				list.AddRange(Addressees);
				list.Add(Description);
				list.Add(TaskID);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is TaskingInfo)) {
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
			return ("taskingInfo");
		}

		/// <summary>
		/// Accessor for the requesterInfos
		/// </summary>
		public List<RequesterInfo> RequesterInfos {
			get {
				return _requesterInfos;
			}
		}

		/// <summary>
		/// Accessor for the addressees
		/// </summary>
		public List<Addressee> Addressees {
			get {
				return _addressees;
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
		/// Accessor for the taskID
		/// </summary>
		public TaskID TaskID {
			get {
				return _taskID;
			}
			set {
					_taskID = value;
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
			internal List<RequesterInfo.Builder> _requesterInfos;
			internal List<Addressee.Builder> _addressees;
			internal Description.Builder _description;
			internal TaskID.Builder _taskID;
			internal SecurityAttributes.Builder _securityAttributes;


			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(TaskingInfo info) {
				foreach (RequesterInfo requester in info.RequesterInfos) {
					RequesterInfos.Add(new RequesterInfo.Builder(requester));
				}
				foreach (Addressee addressee in info.Addressees) {
					Addressees.Add(new Addressee.Builder(addressee));
				}
				if (info.Description != null) {
					Description = new Description.Builder(info.Description);
				}
				TaskID = new TaskID.Builder(info.TaskID);
				SecurityAttributes = new SecurityAttributes.Builder(info.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				List<RequesterInfo> requesterInfos = new List<RequesterInfo>();
				foreach (IBuilder builder in RequesterInfos) {
					RequesterInfo component = (RequesterInfo) builder.Commit();
					if (component != null) {
						requesterInfos.Add(component);
					}
				}
				List<Addressee> addressees = new List<Addressee>();
				foreach (IBuilder builder in Addressees) {
					Addressee component = (Addressee) builder.Commit();
					if (component != null) {
						addressees.Add(component);
					}
				}
				return (new TaskingInfo(requesterInfos, addressees, Description.Commit(), TaskID.Commit(), SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in RequesterInfos) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					foreach (IBuilder builder in Addressees) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && Description.Empty && TaskID.Empty && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the requesterInfos
			/// </summary>
			public virtual List<RequesterInfo.Builder> RequesterInfos {
				get {
					if (_requesterInfos == null) {
                        _requesterInfos = new List<RequesterInfo.Builder>();
					}
					return _requesterInfos;
				}
			}

			/// <summary>
			/// Builder accessor for the addressees
			/// </summary>
			public virtual List<Addressee.Builder> Addressees {
				get {
					if (_addressees == null) {
                        _addressees = new List<Addressee.Builder>();
					}
					return _addressees;
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
			/// Builder accessor for the taskID
			/// </summary>
			public virtual TaskID.Builder TaskID {
				get {
					if (_taskID == null) {
						_taskID = new TaskID.Builder();
					}
					return _taskID;
				}
                set { _taskID = value; }
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