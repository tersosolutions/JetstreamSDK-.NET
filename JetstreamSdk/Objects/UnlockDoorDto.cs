/*
    Copyright 2018 Terso Solutions, Inc.

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

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Passed as a parameter to the 
    /// UnlockDoor command for a device 
    /// to provide a security context for
    /// unlocking an enclosure
    /// </summary>
    public class UnlockDoorDto
    {
        public string AccessToken { get; set; }
    }
}
