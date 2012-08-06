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

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// Request object for the version 1.1 RemovePolicy ReST endpoint
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class RemovePolicyRequest : JetstreamRequest
    {
        private const String c_removepolicy = "v1.1/application/?action=removepolicy&accesskey={0}&policyid={1}";

        /// <summary>
        /// The unique identifier for the policy
        /// </summary>
        public String PolicyId { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            return String.Concat(baseUri, String.Format(
                c_removepolicy, accesskey, this.PolicyId));
        }
    }
}
