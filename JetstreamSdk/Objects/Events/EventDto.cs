/*
    Copyright 2018 Terso Solutions, Inc.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// The base Event DTO that all events inherit.
    /// Handles EventId and EventTime assignment.
    /// </summary>
    public abstract class EventDto
    {
        /// <summary>
        /// Type of Jetstream Event
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A unique identifier assigned by Jetstream
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// Time that the event occurred
        /// </summary>
        public DateTime EventTime { get; set; }
    }

    /// <summary>
    /// This class is a customized JsonConverter from the
    /// Newtonsoft library. This allows us to dynamically
    /// deserialize events to the correct class, using reflection.
    /// Note that this is dependent on EventDto being in the same
    /// namespace as the other event DTOs.
    /// </summary>
    public class EventDtoConverter : JsonConverter
    {
        /// <summary>
        /// Custom method that gets called for each JSON object 
        /// in the Events list in the EventsDto. This is so we can 
        /// deserialize each JSON object into the correct child event, 
        /// instead of them all being EventDto and having data truncated.
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private static EventDto Create(JObject jObject)
        {
            JToken eventType;

            // Try to get the Type field from the JSON.
            // If the field is missing, throw an error.
            if (!jObject.TryGetValue("Type", out eventType) || eventType == null)
                throw new MissingFieldException("Type field not found in JSON of event | " + jObject.ToString(Formatting.None));

            // Try to get the type and create a new instance of that type
            try
            {
                // Make a string with the full type, including namespace
                string fullTypeString = typeof(EventDto).Namespace + "." + eventType.Value<string>() + "Dto";

                // Get the type object from the string
                var fullType = Type.GetType(fullTypeString);

                // ReSharper disable once AssignNullToNotNullAttribute
                // Create an instance of the type. We don't care that fullType
                // can be null since we have a catch statement.
                return (EventDto)Activator.CreateInstance(fullType);
            }
            catch (Exception e)
            {
                // Something went wrong in the reflection,
                // most likely that the type wasn't found.
                throw new ArgumentException("Error in reflection deserialization of event | " + jObject.ToString(Formatting.None), e);
            }
        }

        /// <summary>
        /// Only allow EventDtos as the "result" object
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(EventDto).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// This method is not supported in this converter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This converter is just to read JSON, so it can't write
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// When reading JSON, call our custom Create method
        /// for each event
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="JsonReaderException"><paramref name="reader" /> is not valid JSON.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            EventDto target = Create(jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
