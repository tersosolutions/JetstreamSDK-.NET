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

using System.Collections.Generic;
using System.Text;
using System.Web;
using TersoSolutions.Jetstream.SDK.Application.Model;

namespace TersoSolutions.Jetstream.SDK.DeviceSpecificCommands.TersoEnclosures
{
    /// <summary>
    /// Request object for the TS-* DeviceSpecificCommand UpdatePasses
    /// </summary>
    public class UpdatePassesRequest : DeviceSpecificCommandRequest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public UpdatePassesRequest()
        {
            Add = new List<string>();
            Remove = new List<string>();
        }

        /// <summary>
        /// List of the pass RFID hex values to add to the device&apos;s access control list
        /// </summary>
        public List<string> Add { get; set; }

        /// <summary>
        /// List of the pass RFID hex values to remove from the device&apos;s access control list
        /// </summary>
        public List<string> Remove { get; set; }

        public override string CommandName
        {
            get { return "UpdatePasses"; }
        }

        public override string CreateParametersStrategy()
        {
            StringBuilder sb = new StringBuilder();
            if (Add.Count > 0)
            {
                sb.Append("&add=");
                for (int i = 0; i < Add.Count; i++)
                {
                    if (i != 0)
                    {
                        sb.Append("_");
                    }
                    sb.Append(HttpUtility.UrlEncode(Add[i]));
                }
            }

            if (Remove.Count > 0)
            {
                sb.Append("&remove=");
                for (int i = 0; i < Remove.Count; i++)
                {
                    if (i != 0)
                    {
                        sb.Append("_");
                    }
                    sb.Append(HttpUtility.UrlEncode(Remove[i]));
                }
            }

            return sb.ToString();
        }
    }
}
