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
using TersoSolutions.Jetstream.Application.SQS;

namespace TersoSolutions.Jetstream.Application.Events
{
    /// <summary>
    /// IEqualityComparer used for filtering duplicate messages
    /// </summary>
    /// <remarks>Author Mark Neustadt</remarks>

    public class JetstreamEventEqualityComparer : IEqualityComparer<JetstreamEvent>
    {
        /// <summary>
        /// Determines if the <paramref name="x"/> and <paramref name="y"/> are equal
        /// </summary>
        /// <param name="x">Message x</param>
        /// <param name="y">Message y</param>
        /// <returns>
        /// <para>True - when x &amp; y MessageIds are equal</para>
        /// <para>False - when x &amp; y MessageIds are not equal</para>
        /// </returns>
        public bool Equals(JetstreamEvent x, JetstreamEvent y)
        {
            return (String.Compare(x.EventId, y.EventId, false) == 0);
        }

        /// <summary>
        /// returns the Hashcode for the Message
        /// </summary>
        /// <param name="obj">The message to hash</param>
        /// <returns>
        /// The hashed EventId
        /// </returns>
        public int GetHashCode(JetstreamEvent obj)
        {
            return obj.EventId.GetHashCode();
        }
    }
}
