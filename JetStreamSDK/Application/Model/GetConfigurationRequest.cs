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

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetConfiguration returns the current Jetstream® configuration for your application. 
    /// 
    /// Request object for Getting the application configuration for Jetstream.
    /// </summary>
    /// <remarks></remarks>
    public class GetConfigurationRequest : JetstreamRequest
    {
        private const String c_getConfiguration = "v1.2/application/?action=getconfiguration&accesskey={0}";
        
        /// <summary>
        /// builds the request url
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="accesskey"></param>
        /// <returns></returns>
        internal override string BuildUri(string baseUri, string accesskey)
        {
            return String.Concat(baseUri, 
                String.Format(c_getConfiguration, accesskey));
        }
    }
}
