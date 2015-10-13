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
    /// GetEvents returns but does not deque the oldest pending event messages 
    /// for the Jetstream user. No more than 100 messages will be returned 
    /// on any given GetEvents call.
    /// 
    /// Request object for Jetstream GetEvents.
    /// </summary>
    /// <remarks>Author Mark Bailey</remarks>
    public class GetEventsRequest : JetstreamRequest
    {

        private const String _getEvents = "v1.5/application/?action=getevents&accesskey={0}&limit={1}";
        private int _limit = 100;

        /// <summary>
        /// The number of messages to be returned
        /// </summary>
        public int Limit
        {
            get
            {
                // if the limit is less than 1 or greater than 512, set it to 100
                if (_limit < 1 || _limit > 512) _limit = 100;
                return _limit;
            }
            set
            {
                if (_limit < 1 || _limit > 512) throw new ArgumentOutOfRangeException("Limit", "Limit must be greater or equal to one and less than or equal to 512.");
                _limit = value;
            }
        }

        /// <summary>
        /// Builds the GetEvents request url
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="accesskey"></param>
        /// <returns></returns>
        internal override string BuildUri(string baseUri, string accesskey)
        {
            if (String.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("baseUri");
            if (String.IsNullOrEmpty(accesskey)) throw new ArgumentNullException("accesskey");

            // build the uri
            return String.Concat(baseUri, String.Format(_getEvents,
                new Object[] { accesskey, HttpUtility.UrlEncode(Limit.ToString()) }));
        }
    }
}
