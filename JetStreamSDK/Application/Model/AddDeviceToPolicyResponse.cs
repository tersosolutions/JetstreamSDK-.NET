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
    /// AddDeviceToPolicy adds a device to a policy and will monitor the 
    /// device's configuration values for any descrepencies from the policy.
    /// Jetstream® will automatically schedule a GetConfigValuesCommand 
    /// and compare the results with the policy and overridden device 
    /// parameters. If Jetstream® determines that there is a variance then 
    /// a LogEntryEvent with a type of PolicyError will be published. 
    /// 
    /// Response object for the version 1.0 AddDeviceToPolicy ReST endpoint.
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class AddDeviceToPolicyResponse : JetstreamResponse
    {
    }
}
