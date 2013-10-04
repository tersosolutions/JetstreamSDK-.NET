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
using System.Collections.Generic;
using GP = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetPoliciesResponse;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetPolicies returns all policies you have added to your application. 
    /// 
    /// Response object for the Jetstream version 1.1 GetPolicies.
    /// </summary>
    /// <remarks></remarks>
    public class GetPoliciesResponse : JetstreamResponse
    {
        private GP.Jetstream _deserializedResponse = null;

        /// <summary>
        /// List of all policies you&apos;s defined for your application
        /// </summary>
        public List<GP.JetstreamGetPoliciesResponsePolicy> PolicyList
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Body))
                {
                    _deserializedResponse = _deserializedResponse ?? GP.Jetstream.Deserialize(this.Body);
                    return _deserializedResponse.GetPoliciesResponse.PolicyList;
                }
                else
                {
                    return new List<GP.JetstreamGetPoliciesResponsePolicy>();
                }
            }
        }
    }
}
