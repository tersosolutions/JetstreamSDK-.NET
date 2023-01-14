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

using System;

namespace TersoSolutions.Jetstream.Sdk
{
    /// <summary>
    /// Options for Jetstream client
    /// </summary>
    public class JetstreamClientOptions
    {
        private string _accessKey;
        private Uri _jetstreamUrl;

        /// <summary>
        /// Jetstream SU access key
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException" accessor="set">Jetstream SU access key is <see langword="null"/></exception>
        public string AccessKey
        {
            get => _accessKey;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(AccessKey));
                _accessKey = value.Trim();
            }
        }

        /// <summary>
        /// Jetstream API URL
        /// </summary>
        /// <exception cref="T:System.ArgumentException" accessor="set">Absolute URL is invalid.</exception>
        public Uri JetstreamUrl
        {
            get => _jetstreamUrl;
            set
            {
                if (string.IsNullOrEmpty(value.AbsoluteUri)) throw new ArgumentException(nameof(JetstreamUrl));
                _jetstreamUrl = value;
            }
        }
    }
}
