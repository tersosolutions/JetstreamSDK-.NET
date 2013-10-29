﻿﻿/*
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
    public class RemoveEventsRequest : JetstreamRequest
    {
        private const String c_removeeventscommand = "v1.2/application/?action=removeevents&accesskey={0}&batchid={1}";

        public string BatchId { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(c_removeeventscommand, 
                accesskey, HttpUtility.UrlEncode(this.BatchId)));
        }
    }
}

///// <summary>
///// ResetCommand request object
///// </summary>
///// <remarks></remarks>
//public class ResetCommandRequest : JetstreamRequest
//{
//    private const String c_resetCommand = "v1.2/application/?action=resetcommand&accesskey={0}&logicaldeviceid={1}";

//    /// <summary>
//    /// The LogicalDeviceId to schedule a reset command for
//    /// </summary>
//    public String LogicalDeviceId { get; set; }

//    internal override string BuildUri(string baseUri, string accesskey)
//    {
//        // build the uri
//        return String.Concat(baseUri, String.Format(c_resetCommand,
//            accesskey, HttpUtility.UrlEncode(this.LogicalDeviceId)));
//    }
//}