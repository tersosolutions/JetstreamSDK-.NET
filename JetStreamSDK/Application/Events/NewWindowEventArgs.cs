/*
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
using System.Linq;
using System.Text;
using TersoSolutions.Jetstream.SDK.Application.Messages;

namespace TersoSolutions.Jetstream.SDK.Application.Events
{
    /// <summary>
    /// Custom EventArgs class for the NewWindow event.
    /// </summary>
    /// <remarks>Author Mark Neustadt</remarks>
    public class NewWindowEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor for the custom EventArgs.
        /// </summary>
        /// <param name="messages"></param>
        internal NewWindowEventArgs(IEnumerable<JetstreamEvent> messages)
            : base()
        {
            if (messages == null) throw new ArgumentNullException("messages");

            this.Messages = messages;
        }

        /// <summary>
        /// Ordered window of messages received.
        /// </summary>
        public IEnumerable<JetstreamEvent> Messages { get; private set; }
    }
}
