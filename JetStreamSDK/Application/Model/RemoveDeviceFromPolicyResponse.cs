﻿﻿/*
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
namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// RemoveDeviceFromPolicy removes the device from the list of devices 
    /// to monitor for compliance with your policy. Once removed from the 
    /// policy all settings will be managed using GetConfigValuesCommand 
    /// and SetConfigValuesCommand manually. 
    ///
    /// Response object for the Jetstream version 1.1 RemoveDeviceFromPolicy endpoint.
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class RemoveDeviceFromPolicyResponse : JetstreamResponse
    {

    }
}
