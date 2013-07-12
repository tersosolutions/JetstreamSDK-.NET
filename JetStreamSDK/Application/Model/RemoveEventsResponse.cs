﻿/*
     Copyright 2012 Terso Solutions

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
using CR = TersoSolutions.Jetstream.Application.Model.Deserialized.CommandResponse;

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// RemoveEvents removes event messages associated with a batch of events from the user’s queue.
    /// 
    /// Response object for Jetstream version 1.2 RemoveEvents endpoint.
    /// </summary>
    /// <remarks>Author Mark Bailey</remarks>
    public class RemoveEventsResponse : JetstreamResponse
    {
        private CR.Jetstream _deserializedResponse = null;

        /// <summary>
        /// The access key the device should use for access to the Jetstream device endpoints
        /// </summary>
        public String CommandId
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Body))
                {
                    _deserializedResponse = _deserializedResponse ?? CR.Jetstream.Deserialize(this.Body);
                    return _deserializedResponse.CommandResponse.CommandId;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
