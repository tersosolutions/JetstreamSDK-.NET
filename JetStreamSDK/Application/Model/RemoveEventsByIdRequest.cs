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

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// RemoveEvents removes event messages associated with a batch of events from the user’s queue.
    /// 
    /// Request object for Jetstream version 1.2 RemoveEvents endpoint.
    /// </summary>
    public class RemoveEventsByIdRequest : JetstreamRequest
    {
        private const String c_removeeventscommand = "v1.2/application/?action=removeeventsbyid&accesskey={0}&eventids={1}";

        /// <summary>
        /// An array of event ids to remove
        /// </summary>
        public string[] EventIds { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // convert the array of event ids to an underscore delimited string
            string idString = String.Join("_",this.EventIds);

            // build the uri
            return String.Concat(baseUri, String.Format(c_removeeventscommand,
                accesskey, HttpUtility.UrlEncode(idString)));
        }
    }
}
