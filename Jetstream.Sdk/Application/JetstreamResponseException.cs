﻿/*
     Copyright 2015 Terso Solutions, Inc.

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

namespace TersoSolutions.Jetstream.SDK.Application
{
    /// <summary>
    /// Custom exception class for non HTTP 200 responses from Jetstream 
    /// </summary>
    public class JetstreamResponseException : Exception
    {
        internal JetstreamResponseException(int statusCode, string statusCodeDescription, string request, string response)
            : base("Jetstream returned an error status code " + statusCode + " (" + statusCodeDescription + ") " + response)
        {
            StatusCode = statusCode;
            StatusCodeDescription = statusCodeDescription;
            Request = request;
            Response = response;
        }

        /// <summary>
        /// The HTTP status code returned from Jetstream
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Description of the HTTP status code returned from Jetstream
        /// </summary>
        public string StatusCodeDescription { get; set; }

        /// <summary>
        /// The raw HTTP request URL
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// The raw HTTP response body
        /// </summary>
        public string Response { get; set; }
    }

}
