using System.Collections.Generic;
using System.IO;

namespace TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetPoliciesResponse
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.5/GetPoliciesResponse")]
    public partial class JetstreamGetPoliciesResponsePolicy
    {

        private List<JetstreamGetPoliciesResponsePolicyParameter> parameterListField;

        private List<JetstreamGetPoliciesResponsePolicyLogicalDevice> logicalDeviceListField;

        private List<System.Xml.XmlElement> anyField;

        private string idField;

        private string nameField;

        private string deviceDefinitionIdField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        public JetstreamGetPoliciesResponsePolicy()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.anyField = new List<System.Xml.XmlElement>();
            this.logicalDeviceListField = new List<JetstreamGetPoliciesResponsePolicyLogicalDevice>();
            this.parameterListField = new List<JetstreamGetPoliciesResponsePolicyParameter>();
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Parameter", IsNullable = false)]
        public List<JetstreamGetPoliciesResponsePolicyParameter> ParameterList
        {
            get
            {
                return this.parameterListField;
            }
            set
            {
                this.parameterListField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("LogicalDevice", IsNullable = false)]
        public List<JetstreamGetPoliciesResponsePolicyLogicalDevice> LogicalDeviceList
        {
            get
            {
                return this.logicalDeviceListField;
            }
            set
            {
                this.logicalDeviceListField = value;
            }
        }

        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public List<System.Xml.XmlElement> Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DeviceDefinitionId
        {
            get
            {
                return this.deviceDefinitionIdField;
            }
            set
            {
                this.deviceDefinitionIdField = value;
            }
        }

        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public List<System.Xml.XmlAttribute> AnyAttr
        {
            get
            {
                return this.anyAttrField;
            }
            set
            {
                this.anyAttrField = value;
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetPoliciesResponsePolicy));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetPoliciesResponsePolicy object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize()
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                Serializer.Serialize(memoryStream, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes workflow markup into an JetstreamGetPoliciesResponsePolicy object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetPoliciesResponsePolicy object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetPoliciesResponsePolicy obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetPoliciesResponsePolicy);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out JetstreamGetPoliciesResponsePolicy obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetPoliciesResponsePolicy Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetPoliciesResponsePolicy)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current JetstreamGetPoliciesResponsePolicy object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize();
                System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an JetstreamGetPoliciesResponsePolicy object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetPoliciesResponsePolicy object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetPoliciesResponsePolicy obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetPoliciesResponsePolicy);
            try
            {
                obj = LoadFromFile(fileName);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out JetstreamGetPoliciesResponsePolicy obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetPoliciesResponsePolicy LoadFromFile(string fileName)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

    }
}