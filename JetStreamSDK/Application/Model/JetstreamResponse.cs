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
    /// Abstract class for any Jetstream application response
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public abstract class JetstreamResponse
    {
        /// <summary>
        /// The raw URL used in the request
        /// </summary>
        public String Request { get; internal set; }

        /// <summary>
        /// The raw response from Jetstream
        /// </summary>
        public String Body { get; internal set; }
    }
}
