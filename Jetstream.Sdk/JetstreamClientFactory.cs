/*
    Copyright 2022 Terso Solutions, Inc.

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

using Microsoft.Extensions.Options;
using System;

namespace TersoSolutions.Jetstream.Sdk
{
    /// <summary>
    /// Factory used to create new instances of Jetstream Client
    /// </summary>
    public class JetstreamClientFactory : IJetstreamClientFactory
    {
        /// <summary>
        /// Creates a new Jetstream Client
        /// </summary>
        /// <param name="accessKey">Access Key used to access Jetstream</param>
        /// <param name="jetstreamUrl">Url of Jetstream</param>
        /// <returns>Jetstream Client</returns>
        public IJetstreamClient Create(string accessKey, string jetstreamUrl)
        {
            return new JetstreamClient(accessKey, jetstreamUrl);
        }

        /// <summary>
        /// Creates a new Jetstream Client
        /// </summary>
        /// <param name="accessKey">Access Key used to access Jetstream</param>
        /// <param name="jetstreamUrl">Url of Jetstream</param>
        /// <returns>Jetstream Client</returns>
        public IJetstreamClient Create(string accessKey, Uri jetstreamUrl)
        {
            return new JetstreamClient(accessKey, jetstreamUrl);
        }

        /// <summary>
        /// Creates a new Jetstream Client
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Jetstream Client</returns>
        public IJetstreamClient Create(IOptionsMonitor<JetstreamClientOptions> options)
        {
            return new JetstreamClient(options);
        }
    }
}
