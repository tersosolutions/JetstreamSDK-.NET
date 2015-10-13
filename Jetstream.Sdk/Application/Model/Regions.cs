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
namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// Enum for the accepted regions for Jetstream API
    /// </summary>
    /// <remarks></remarks>
    public enum Regions
    {
        /// <summary>
        /// Indicates the device is in use in the United States
        /// </summary>
        US = 0x0,
        /// <summary>
        /// Indicates the device is in use in the European Union
        /// </summary>
        EU = 0x1,
        /// <summary>
        /// Indicates the device is in use in the Asia
        /// </summary>
        AP = 0x2
    }
}