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
    /// IComparer implementation for sorting messages by SentTimestamp epoch.
    /// </summary>
    /// <remarks>Author Mark Neustadt</remarks>
    public class JetstreamEventTimeStampComparer : IComparer<JetstreamEvent>
    {
        /// <summary>
        /// IComparer implementation that compares the epoch SentTimestamp and MessageId.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(JetstreamEvent x, JetstreamEvent y)
        {
            DateTime sentTimestampx = x.EventTime;
            DateTime sentTimestampy = y.EventTime;

            if ((sentTimestampx == null) |
                (sentTimestampy == null))
            {
                // one of the messages doesn't have a SentTimestamp so throw an Exception
                throw new Exception("Unable to compare Messages because one of the messages did not have a SentTimestamp Attribute"); 
            }

            long epochx = sentTimestampx.Ticks;
            long epochy = sentTimestampy.Ticks;

            int result = epochx.CompareTo(epochy);
            if (result != 0)
            {
                return result;
            }
            else
            {
                // same SentTimestamp so use the messageId for comparison
                return x.EventId.CompareTo(y.EventId);
            }
        }
    }
}
