﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace TersoSolutions.Jetstream.Application.Events.LogicalDeviceAddedEvent {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://Jetstream.TersoSolutions.com/v1.0/LogicalDeviceAddedEvent")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://Jetstream.TersoSolutions.com/v1.0/LogicalDeviceAddedEvent", IsNullable=false)]
    public partial class Jetstream {
        
        private JetstreamHeader headerField;
        
        private JetstreamLogicalDeviceAddedEvent logicalDeviceAddedEventField;
        
        /// <remarks/>
        public JetstreamHeader Header {
            get {
                return this.headerField;
            }
            set {
                this.headerField = value;
            }
        }
        
        /// <remarks/>
        public JetstreamLogicalDeviceAddedEvent LogicalDeviceAddedEvent {
            get {
                return this.logicalDeviceAddedEventField;
            }
            set {
                this.logicalDeviceAddedEventField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://Jetstream.TersoSolutions.com/v1.0/LogicalDeviceAddedEvent")]
    public partial class JetstreamHeader {
        
        private string eventIdField;
        
        private System.DateTime eventTimeField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EventId {
            get {
                return this.eventIdField;
            }
            set {
                this.eventIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime EventTime {
            get {
                return this.eventTimeField;
            }
            set {
                this.eventTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://Jetstream.TersoSolutions.com/v1.0/LogicalDeviceAddedEvent")]
    public partial class JetstreamLogicalDeviceAddedEvent {
        
        private System.Xml.XmlElement[] anyField;
        
        private string deviceDefinitionIdField;
        
        private string deviceSerialNumberField;
        
        private string logicalDeviceIdField;
        
        private JetstreamLogicalDeviceAddedEventRegion regionField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DeviceDefinitionId {
            get {
                return this.deviceDefinitionIdField;
            }
            set {
                this.deviceDefinitionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DeviceSerialNumber {
            get {
                return this.deviceSerialNumberField;
            }
            set {
                this.deviceSerialNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LogicalDeviceId {
            get {
                return this.logicalDeviceIdField;
            }
            set {
                this.logicalDeviceIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public JetstreamLogicalDeviceAddedEventRegion Region {
            get {
                return this.regionField;
            }
            set {
                this.regionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://Jetstream.TersoSolutions.com/v1.0/LogicalDeviceAddedEvent")]
    public enum JetstreamLogicalDeviceAddedEventRegion {
        
        /// <remarks/>
        US,
        
        /// <remarks/>
        EU,
        
        /// <remarks/>
        ASIA,
    }
}
