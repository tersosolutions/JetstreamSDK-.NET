﻿/*
     Copyright 2013 Terso Solutions

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
using System.Web;

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// GetEvents returns but does not dequeue the oldest pending event messages 
    /// for the Jetstream user. No more than 100 messages will be returned 
    /// on any given GetEvents call.
    /// 
    /// Request object for Jetstream version 1.2 GetEvents.
    /// </summary>
    /// <remarks>Author Mark Bailey</remarks>
    public class GetEventsRequest : JetstreamRequest
    {
        private const String c_getevents = "v1.2/application/?action=getevents&accesskey={0}";

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(c_getevents, accesskey));
        }

    }
}
