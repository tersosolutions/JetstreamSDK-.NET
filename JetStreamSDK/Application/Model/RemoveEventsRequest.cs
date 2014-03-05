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
using System.Linq;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// RemoveEvents removes event messages associated with a batch of events from the user’s queue.
    /// 
    /// Request object for Jetstream version 1.3 RemoveEvents endpoint.
    /// </summary>
    public class RemoveEventsRequest : JetstreamRequest
    {
        private const String c_removeeventscommand = "v1.3/application/?action=removeevents&accesskey={0}{1}{2}";

        /// <summary>
        /// The batch id of the events to remove
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// An array of event ids to remove
        /// </summary>
        public string[] EventIds { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // assume no batch id nor events
            string usingBatch = String.Empty;
            string usingEvents = String.Empty;

            // if the user has passed a batch id, build that portion of the query string request
            if (!String.IsNullOrEmpty(BatchId))
            {
                usingBatch = String.Format("&batchid={0}", HttpUtility.UrlEncode(BatchId));
            }

            // if the user has passed event ids, build that portion of the query string request
            if (EventIds.Count() != 0)
            {
                // URLEncode each event id
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                foreach (var ev in EventIds)
                {
                    list.Add(HttpUtility.UrlEncode(ev));
                }

                // Build the event ids string
                string idString = String.Join("_", list.ToArray());
                usingEvents = String.Format("&eventids={0}", idString);
            }

            // build the uri
            return String.Concat(baseUri, String.Format(c_removeeventscommand, accesskey, usingBatch, usingEvents));

        }
    }
}
