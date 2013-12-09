#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Util;

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

namespace DDMSSense.DDMS.ResourceElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:resourceManagement.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:recordsManagementInfo</u>: Information about the record keeper and software used to create the
    ///                 object to
    ///                 which this metadata applies (0-1 optional), implemented as a <see cref="RecordsManagementInfo" /><br />
    ///                 <u>ddms:revisionRecall</u>: Details about any revision recalls for this resource (0-1 optional),
    ///                 implemented as a
    ///                 <see cref="RevisionRecall" /><br />
    ///                 <u>ddms:taskingInfo</u>: Information about who requested production of the resource (0-many optional),
    ///                 implemented
    ///                 as a <see cref="TaskingInfo" /><br />
    ///                 <u>ddms:processingInfo</u>: Details about the processing of the resource (0-many optional), implemented
    ///                 as a
    ///                 <see cref="ProcessingInfo" /><br />
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
    ///                 :  The classification and ownerProducer attributes are optional.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 2.0.0
    /// </summary>
    public sealed class ResourceManagement : AbstractBaseComponent
    {
        private readonly List<ProcessingInfo> _processingInfos;
        private readonly List<TaskingInfo> _taskingInfos;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ResourceManagement(Element element)
        {
            RevisionRecall = null;
            RecordsManagementInfo = null;
            try
            {
                SetXOMElement(element, false);
                Element recordsManagementInfo =
                    element.Element(XName.Get(RecordsManagementInfo.GetName(DDMSVersion), Namespace));
                if (recordsManagementInfo != null)
                {
                    RecordsManagementInfo = new RecordsManagementInfo(recordsManagementInfo);
                }
                Element revisionRecall = element.Element(XName.Get(RevisionRecall.GetName(DDMSVersion), Namespace));
                if (revisionRecall != null)
                {
                    RevisionRecall = new RevisionRecall(revisionRecall);
                }
                _taskingInfos = new List<TaskingInfo>();
                IEnumerable<Element> taskingInfos =
                    element.Elements(XName.Get(TaskingInfo.GetName(DDMSVersion), Namespace));
                foreach (var taskingInfo in taskingInfos)
                    _taskingInfos.Add(new TaskingInfo(taskingInfo));

                _processingInfos = new List<ProcessingInfo>();
                IEnumerable<Element> processingInfos =
                    element.Elements(XName.Get(ProcessingInfo.GetName(DDMSVersion), Namespace));
                foreach (var processingInfo in processingInfos)
                    _processingInfos.Add(new ProcessingInfo(processingInfo));

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
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="recordsManagementInfo"> records management info (optional) </param>
        /// <param name="revisionRecall"> information about revision recalls (optional) </param>
        /// <param name="taskingInfos"> list of tasking info (optional) </param>
        /// <param name="processingInfos"> list of processing info (optional) </param>
        /// <param name="securityAttributes"> security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ResourceManagement(RecordsManagementInfo recordsManagementInfo, RevisionRecall revisionRecall,
            List<TaskingInfo> taskingInfos, List<ProcessingInfo> processingInfos, SecurityAttributes securityAttributes)
        {
            RevisionRecall = null;
            RecordsManagementInfo = null;
            try
            {
                if (taskingInfos == null)
                {
                    taskingInfos = new List<TaskingInfo>();
                }
                if (processingInfos == null)
                {
                    processingInfos = new List<ProcessingInfo>();
                }

                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.GetCurrentVersion()), null);
                SetXOMElement(element, false);
                if (recordsManagementInfo != null)
                {
                    element.Add(recordsManagementInfo.XOMElementCopy);
                }
                if (revisionRecall != null)
                {
                    element.Add(revisionRecall.XOMElementCopy);
                }
                foreach (var info in taskingInfos)
                {
                    element.Add(info.XOMElementCopy);
                }
                foreach (var info in processingInfos)
                {
                    element.Add(info.XOMElementCopy);
                }

                RecordsManagementInfo = recordsManagementInfo;
                RevisionRecall = revisionRecall;
                _taskingInfos = taskingInfos;
                _processingInfos = processingInfos;
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(RecordsManagementInfo);
                list.Add(RevisionRecall);
                list.AddRange(TaskingInfos);
                list.AddRange(ProcessingInfos);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the recordsManagementInfo
        /// </summary>
        public RecordsManagementInfo RecordsManagementInfo { get; set; }

        /// <summary>
        ///     Accessor for the revisionRecall
        /// </summary>
        public RevisionRecall RevisionRecall { get; set; }

        /// <summary>
        ///     Accessor for the tasking information
        /// </summary>
        public List<TaskingInfo> TaskingInfos
        {
            get { return _taskingInfos; }
        }

        /// <summary>
        ///     Accessor for the processing information
        /// </summary>
        public List<ProcessingInfo> ProcessingInfos
        {
            get { return _processingInfos; }
        }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
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
        ///                 <li>Only 0-1 recordsManagementInfo or revisionRecall elements exist.</li>
        ///                 <li>This component cannot exist until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQName(Element, GetName(DDMSVersion));
            Util.Util.RequireBoundedChildCount(Element, RecordsManagementInfo.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, RevisionRecall.GetName(DDMSVersion), 0, 1);

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            if (RecordsManagementInfo != null)
            {
                text.Append(RecordsManagementInfo.GetOutput(isHtml, localPrefix, ""));
            }
            if (RevisionRecall != null)
            {
                text.Append(RevisionRecall.GetOutput(isHtml, localPrefix, ""));
            }
            text.Append(BuildOutput(isHtml, localPrefix, TaskingInfos));
            text.Append(BuildOutput(isHtml, localPrefix, ProcessingInfos));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is ResourceManagement))
            {
                return (false);
            }
            return (true);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("resourceManagement");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            internal const long SerialVersionUID = 7851044806424206976L;
            internal List<ProcessingInfo.Builder> _processingInfos;
            internal RecordsManagementInfo.Builder _recordsManagementInfo;
            internal RevisionRecall.Builder _revisionRecall;
            internal SecurityAttributes.Builder _securityAttributes;
            internal List<TaskingInfo.Builder> _taskingInfos;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(ResourceManagement resourceManagement)
            {
                if (resourceManagement.RecordsManagementInfo != null)
                {
                    RecordsManagementInfo = new RecordsManagementInfo.Builder(resourceManagement.RecordsManagementInfo);
                }
                if (resourceManagement.RevisionRecall != null)
                {
                    RevisionRecall = new RevisionRecall.Builder(resourceManagement.RevisionRecall);
                }
                foreach (var info in resourceManagement.TaskingInfos)
                {
                    TaskingInfos.Add(new TaskingInfo.Builder(info));
                }
                foreach (var info in resourceManagement.ProcessingInfos)
                {
                    ProcessingInfos.Add(new ProcessingInfo.Builder(info));
                }
                SecurityAttributes = new SecurityAttributes.Builder(resourceManagement.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the recordsManagementInfo
            /// </summary>
            public virtual RecordsManagementInfo.Builder RecordsManagementInfo
            {
                get
                {
                    if (_recordsManagementInfo == null)
                    {
                        _recordsManagementInfo = new RecordsManagementInfo.Builder();
                    }
                    return _recordsManagementInfo;
                }
                set { _recordsManagementInfo = value; }
            }


            /// <summary>
            ///     Builder accessor for the revisionRecall
            /// </summary>
            public virtual RevisionRecall.Builder RevisionRecall
            {
                get
                {
                    if (_revisionRecall == null)
                    {
                        _revisionRecall = new RevisionRecall.Builder();
                    }
                    return _revisionRecall;
                }
                set { _revisionRecall = value; }
            }


            /// <summary>
            ///     Builder accessor for the taskingInfos
            /// </summary>
            public virtual List<TaskingInfo.Builder> TaskingInfos
            {
                get
                {
                    if (_taskingInfos == null)
                    {
                        _taskingInfos = new List<TaskingInfo.Builder>();
                    }
                    return _taskingInfos;
                }
            }

            /// <summary>
            ///     Builder accessor for the processingInfos
            /// </summary>
            public virtual List<ProcessingInfo.Builder> ProcessingInfos
            {
                get
                {
                    if (_processingInfos == null)
                    {
                        _processingInfos = new List<ProcessingInfo.Builder>();
                    }
                    return _processingInfos;
                }
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
                var taskingInfos = new List<TaskingInfo>();
                foreach (var builder in TaskingInfos)
                {
                    var info = (TaskingInfo) builder.Commit();
                    if (info != null)
                    {
                        taskingInfos.Add(info);
                    }
                }
                var processingInfos = new List<ProcessingInfo>();
                foreach (var builder in ProcessingInfos)
                {
                    var point = (ProcessingInfo) builder.Commit();
                    if (point != null)
                    {
                        processingInfos.Add(point);
                    }
                }
                return
                    (new ResourceManagement((RecordsManagementInfo) RecordsManagementInfo.Commit(),
                        (RevisionRecall) RevisionRecall.Commit(), taskingInfos, processingInfos,
                        SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in ProcessingInfos)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    foreach (IBuilder builder in TaskingInfos)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && RecordsManagementInfo.Empty && RevisionRecall.Empty &&
                            SecurityAttributes.Empty);
                }
            }
        }
    }
}