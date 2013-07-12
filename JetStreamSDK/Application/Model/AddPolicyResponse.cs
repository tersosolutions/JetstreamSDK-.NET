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
using CR = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.ConfigureResponse;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// AddPolicy adds a policy to Jetstream® for managing your device's
    /// parameters. You can define a policy for a device definition and 
    /// assign a device to the policy. Whenever an assigned device's 
    /// parameters do not match the policy a LogEntryEvent of 
    /// type PolicyException is published. 
    ///
    /// Reponse object for Jetstream version 1.1 AddPolicy
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class AddPolicyResponse : JetstreamResponse
    {
        private CR.Jetstream _deserializedResponse = null;

        /// <summary>
        /// The access key the device should use for access to the Jetstream device endpoints
        /// </summary>
        public String Id
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Body))
                {
                    _deserializedResponse = _deserializedResponse ?? CR.Jetstream.Deserialize(this.Body);
                    return _deserializedResponse.ConfigureResponse.Id;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
