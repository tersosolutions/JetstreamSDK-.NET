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
    /// RemoveDeviceFromPolicy removes the device from the list of devices 
    /// to monitor for compliance with your policy. Once removed from the 
    /// policy all settings will be managed using GetConfigValuesCommand 
    /// and SetConfigValuesCommand manually. 
    ///
    /// Response object for the Jetstream RemoveDeviceFromPolicy endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class RemoveDeviceFromPolicyResponse : JetstreamResponse
    {

    }
}
