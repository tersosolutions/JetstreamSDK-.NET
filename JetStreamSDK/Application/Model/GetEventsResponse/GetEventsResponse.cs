

namespace TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetEventsResponse
{
    using System.Collections.Generic;
    using System.IO;
    
    /// <summary>
    /// Root node for all Jetstream messages
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.2/GetEventsResponse")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://Jetstream.TersoSolutions.com/v1.2/GetEventsResponse", IsNullable = false)]

    public partial class Jetstream
    {

        private JetstreamHeader headerField;

        private JetstreamGetEventsResponse getEventsResponseField;

        private List<System.Xml.XmlElement> anyField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// Jetstream class constructor
        /// </summary>
        public Jetstream()
        {
            this.anyField = new List<System.Xml.XmlElement>();
            this.getEventsResponseField = new JetstreamGetEventsResponse();
            this.headerField = new JetstreamHeader();
        }

        /// <summary>
        /// Header for GetEvents Response
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public JetstreamHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <summary>
        /// Wrapper node for all GetDeviceDefinitionResponse data
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public JetstreamGetEventsResponse GetEventsResponse
        {
            get
            {
                return this.getEventsResponseField;
            }
            set
            {
                this.getEventsResponseField = value;
            }
        }

        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 2)]
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

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(Jetstream));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current Jetstream object into an XML document
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
        /// Deserializes workflow markup into an Jetstream object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output Jetstream object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out Jetstream obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Jetstream);
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

        public static bool Deserialize(string xml, out Jetstream obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static Jetstream Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((Jetstream)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current Jetstream object into file
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
        /// Deserializes xml markup from file into an Jetstream object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output Jetstream object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out Jetstream obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Jetstream);
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

        public static bool LoadFromFile(string fileName, out Jetstream obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static Jetstream LoadFromFile(string fileName)
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

    /// <summary>
    /// Header for GetEvents Response
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamHeader
    {

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamHeader class constructor
        /// </summary>
        public JetstreamHeader()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamHeader));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamHeader object into an XML document
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
        /// Deserializes workflow markup into an JetstreamHeader object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamHeader object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamHeader obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamHeader);
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

        public static bool Deserialize(string xml, out JetstreamHeader obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamHeader Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamHeader)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamHeader object into file
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
        /// Deserializes xml markup from file into an JetstreamHeader object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamHeader object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamHeader obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamHeader);
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

        public static bool LoadFromFile(string fileName, out JetstreamHeader obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamHeader LoadFromFile(string fileName)
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

    /// <summary>
    /// Wrapper node for all GetDeviceDefinitionResponse data
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponse
    {

        private List<JetstreamGetEventsResponseDeviceDefinition> deviceDefinitionListField;

        private List<System.Xml.XmlElement> anyField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponse class constructor
        /// </summary>
        public JetstreamGetEventsResponse()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.anyField = new List<System.Xml.XmlElement>();
            this.deviceDefinitionListField = new List<JetstreamGetEventsResponseDeviceDefinition>();
        }

        /// <summary>
        /// List of all Events
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("DeviceDefinition", IsNullable = false)]
        public List<JetstreamGetEventsResponseDeviceDefinition> DeviceDefinitionList
        {
            get
            {
                return this.deviceDefinitionListField;
            }
            set
            {
                this.deviceDefinitionListField = value;
            }
        }

        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 1)]
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponse));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponse object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponse object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponse object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponse obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponse);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponse obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponse Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponse)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponse object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponse object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponse object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponse obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponse);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponse obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponse LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseDeviceDefinition
    {

        private JetstreamGetEventsResponseDeviceDefinitionCommandList commandListField;

        private JetstreamGetEventsResponseEventsensorReadingMeasureList sensorReadingMeasureListField;

        private List<System.Xml.XmlElement> anyField;

        private string idField;

        private string firmwareVersionField;

        private string manufacturerField;

        private string nameField;

        private string uRLField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseDeviceDefinition class constructor
        /// </summary>
        public JetstreamGetEventsResponseDeviceDefinition()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.anyField = new List<System.Xml.XmlElement>();
            this.sensorReadingMeasureListField = new JetstreamGetEventsResponseEventsensorReadingMeasureList();
            this.commandListField = new JetstreamGetEventsResponseDeviceDefinitionCommandList();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public JetstreamGetEventsResponseDeviceDefinitionCommandList CommandList
        {
            get
            {
                return this.commandListField;
            }
            set
            {
                this.commandListField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public JetstreamGetEventsResponseEventsensorReadingMeasureList SensorReadingMeasureList
        {
            get
            {
                return this.sensorReadingMeasureListField;
            }
            set
            {
                this.sensorReadingMeasureListField = value;
            }
        }

        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 2)]
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
        public string FirmwareVersion
        {
            get
            {
                return this.firmwareVersionField;
            }
            set
            {
                this.firmwareVersionField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
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

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URL
        {
            get
            {
                return this.uRLField;
            }
            set
            {
                this.uRLField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseDeviceDefinition));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseDeviceDefinition object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseDeviceDefinition object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinition object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinition obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinition);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinition obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinition Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseDeviceDefinition)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseDeviceDefinition object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseDeviceDefinition object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinition object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinition obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinition);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinition obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinition LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseDeviceDefinitionCommandList
    {

        private JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList deviceSpecificCommandListField;

        private JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList configParameterListField;

        private List<System.Xml.XmlElement> anyField;

        private bool getConfigValuesCommandField;

        private bool getEPCListCommandField;

        private bool killCommandField;

        private bool lockCommandField;

        private bool readTagDataCommandField;

        private bool resetCommandField;

        private bool setConfigValuesCommandField;

        private bool unlockCommandField;

        private bool updateFirmwareCommandField;

        private bool writeTagDataCommandField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseDeviceDefinitionCommandList class constructor
        /// </summary>
        public JetstreamGetEventsResponseDeviceDefinitionCommandList()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.anyField = new List<System.Xml.XmlElement>();
            this.configParameterListField = new JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList();
            this.deviceSpecificCommandListField = new JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList DeviceSpecificCommandList
        {
            get
            {
                return this.deviceSpecificCommandListField;
            }
            set
            {
                this.deviceSpecificCommandListField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList ConfigParameterList
        {
            get
            {
                return this.configParameterListField;
            }
            set
            {
                this.configParameterListField = value;
            }
        }

        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 2)]
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
        public bool GetConfigValuesCommand
        {
            get
            {
                return this.getConfigValuesCommandField;
            }
            set
            {
                this.getConfigValuesCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool GetEPCListCommand
        {
            get
            {
                return this.getEPCListCommandField;
            }
            set
            {
                this.getEPCListCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool KillCommand
        {
            get
            {
                return this.killCommandField;
            }
            set
            {
                this.killCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool LockCommand
        {
            get
            {
                return this.lockCommandField;
            }
            set
            {
                this.lockCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ReadTagDataCommand
        {
            get
            {
                return this.readTagDataCommandField;
            }
            set
            {
                this.readTagDataCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ResetCommand
        {
            get
            {
                return this.resetCommandField;
            }
            set
            {
                this.resetCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool SetConfigValuesCommand
        {
            get
            {
                return this.setConfigValuesCommandField;
            }
            set
            {
                this.setConfigValuesCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool UnlockCommand
        {
            get
            {
                return this.unlockCommandField;
            }
            set
            {
                this.unlockCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool UpdateFirmwareCommand
        {
            get
            {
                return this.updateFirmwareCommandField;
            }
            set
            {
                this.updateFirmwareCommandField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool WriteTagDataCommand
        {
            get
            {
                return this.writeTagDataCommandField;
            }
            set
            {
                this.writeTagDataCommandField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseDeviceDefinitionCommandList));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandList object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseDeviceDefinitionCommandList object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandList);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandList obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandList Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseDeviceDefinitionCommandList)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandList object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseDeviceDefinitionCommandList object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandList);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandList obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandList LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList
    {

        private List<JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand> deviceSpecificCommandField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList class constructor
        /// </summary>
        public JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.deviceSpecificCommandField = new List<JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand>();
        }

        [System.Xml.Serialization.XmlElementAttribute("DeviceSpecificCommand", Order = 0)]
        public List<JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand> DeviceSpecificCommand
        {
            get
            {
                return this.deviceSpecificCommandField;
            }
            set
            {
                this.deviceSpecificCommandField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandList LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand
    {

        private string commandNameField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand class constructor
        /// </summary>
        public JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CommandName
        {
            get
            {
                return this.commandNameField;
            }
            set
            {
                this.commandNameField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListDeviceSpecificCommandListDeviceSpecificCommand LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList
    {

        private List<JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter> configParameterField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList class constructor
        /// </summary>
        public JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.configParameterField = new List<JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter>();
        }

        [System.Xml.Serialization.XmlElementAttribute("ConfigParameter", Order = 0)]
        public List<JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter> ConfigParameter
        {
            get
            {
                return this.configParameterField;
            }
            set
            {
                this.configParameterField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterList LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter
    {

        private string nameField;

        private JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameterType typeField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter class constructor
        /// </summary>
        public JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
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
        public JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameterType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameter LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public enum JetstreamGetEventsResponseDeviceDefinitionCommandListConfigParameterListConfigParameterType
    {

        /// <remarks/>
        Int,

        /// <remarks/>
        String,

        /// <remarks/>
        Bool,

        /// <remarks/>
        DateTime,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseEventsensorReadingMeasureList
    {

        private List<JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure> sensorReadingMeasureField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseEventsensorReadingMeasureList class constructor
        /// </summary>
        public JetstreamGetEventsResponseEventsensorReadingMeasureList()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
            this.sensorReadingMeasureField = new List<JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure>();
        }

        [System.Xml.Serialization.XmlElementAttribute("SensorReadingMeasure", Order = 0)]
        public List<JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure> SensorReadingMeasure
        {
            get
            {
                return this.sensorReadingMeasureField;
            }
            set
            {
                this.sensorReadingMeasureField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseEventsensorReadingMeasureList));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseEventsensorReadingMeasureList object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseEventsensorReadingMeasureList object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseEventsensorReadingMeasureList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseEventsensorReadingMeasureList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseEventsensorReadingMeasureList);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseEventsensorReadingMeasureList obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseEventsensorReadingMeasureList Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseEventsensorReadingMeasureList)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseEventsensorReadingMeasureList object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseEventsensorReadingMeasureList object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseEventsensorReadingMeasureList object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseEventsensorReadingMeasureList obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseEventsensorReadingMeasureList);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseEventsensorReadingMeasureList obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseEventsensorReadingMeasureList LoadFromFile(string fileName)
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://Jetstream.TersoSolutions.com/v1.0/GetDeviceDefinitionResponse")]
    public partial class JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure
    {

        private string nameField;

        private string unitOfMeasureField;

        private List<System.Xml.XmlAttribute> anyAttrField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        /// <summary>
        /// JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure class constructor
        /// </summary>
        public JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure()
        {
            this.anyAttrField = new List<System.Xml.XmlAttribute>();
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
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                this.unitOfMeasureField = value;
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure object into an XML document
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
        /// Deserializes workflow markup into an JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure);
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

        public static bool Deserialize(string xml, out JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure object into file
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
        /// Deserializes xml markup from file into an JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure obj, out System.Exception exception)
        {
            exception = null;
            obj = default(JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure);
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

        public static bool LoadFromFile(string fileName, out JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static JetstreamGetEventsResponseEventsensorReadingMeasureListSensorReadingMeasure LoadFromFile(string fileName)
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
