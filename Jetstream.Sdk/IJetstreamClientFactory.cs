/*
    Copyright 2023 Terso Solutions, Inc.

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
    /// Interface of a factory used to create Jetstream Client instances.
    /// </summary>
    public interface IJetstreamClientFactory
    {
        /// <summary>
        /// Stub method for creating a new Jetstream Client instance
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="jetstreamUrl"></param>
        /// <returns></returns>
        IJetstreamClient Create(string accessKey, string jetstreamUrl);

        /// <summary>
        /// Stub method for creating a new Jetstream Client instance
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="jetstreamUrl"></param>
        /// <returns></returns>
        IJetstreamClient Create(string accessKey, Uri jetstreamUrl);

        /// <summary>
        /// Stub method for creating a new Jetstream Client instance
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IJetstreamClient Create(IOptionsMonitor<JetstreamClientOptions> options);
    }
}
