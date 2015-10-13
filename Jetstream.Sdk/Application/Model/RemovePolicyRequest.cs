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

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// RemovePolicy removes a policy from your application. 
    ///
    /// Request object for the Jetstream RemovePolicy ReST endpoint
    /// </summary>
    /// <remarks></remarks>
    public class RemovePolicyRequest : JetstreamRequest
    {
        private const String _removePolicy = "v1.5/application/?action=removepolicy&accesskey={0}&policyid={1}";

        /// <summary>
        /// The unique identifier for the policy
        /// </summary>
        public string PolicyId { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            return String.Concat(baseUri, String.Format(
                _removePolicy, accesskey, PolicyId));
        }
    }
}
