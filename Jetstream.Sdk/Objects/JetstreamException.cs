/*
    Copyright 2019 Terso Solutions, Inc.

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
using System.Net;

namespace TersoSolutions.Jetstream.Sdk.Objects
{
    /// <summary>
    /// A data transfer object containing properties for an exception from Jetstream calls.
    /// </summary>
    public class JetstreamException : Exception
    {
        /// <summary>
        /// Status code of exception
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Raw JSON of the request body, if it exists
        /// </summary>
        public string RequestBody { get; }

        /// <summary>
        /// Raw JSON of the response body, if it exists
        /// </summary>
        public string ResponseBody { get; }

        /// <summary>
        /// Construct a Jetstream exception. 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="requestBody"></param>
        /// <param name="responseBody"></param>
        /// <param name="formattedMessage">We have to ask for a formatted message separately because we need to immediately pass it through to the base constructor.</param>
        /// <param name="innerException"></param>
        protected internal JetstreamException(HttpStatusCode statusCode, string requestBody, string responseBody, string formattedMessage, Exception innerException)
            : base("Jetstream returned an error: " + statusCode + " - " + formattedMessage, innerException)
        {
            StatusCode = statusCode;
            RequestBody = requestBody;
            ResponseBody = responseBody;
        }
    }
}
