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
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// Request object for SetConfigValuesCommand
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class SetConfigValuesCommandRequest : JetstreamRequest
    {
        private const String c_setConfigValuesCommand = "v1.0/application/?action=setconfigvaluescommand&accesskey={0}&logicaldeviceid={1}{2}";
       
        /// <summary>
        /// Default ctor
        /// </summary>
        public SetConfigValuesCommandRequest()
        {
            this.Parameters = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// The LogicalDeviceId to queue a SetConfigValuesCommand for
        /// </summary>
        public String LogicalDeviceId { get; set; }

        /// <summary>
        /// The parameters to set
        /// Item1 = Name of parameter
        /// Item2 = Value of parameter
        /// </summary>
        public List<Tuple<String, String>> Parameters { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < this.Parameters.Count; j++)
            {
                sb.Append("&");
                sb.Append(HttpUtility.UrlEncode(this.Parameters[j].Item1));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(this.Parameters[j].Item2));
            }

            // build the uri
            return String.Concat(baseUri, String.Format(c_setConfigValuesCommand,
                new String[] 
                    { 
                        accesskey, 
                        HttpUtility.UrlEncode(this.LogicalDeviceId),
                        sb.ToString()
                    }));
        }
    }
}
