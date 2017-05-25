using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// DTO used for proprietary commands
    /// specified by the user in the device
    /// policy
    /// </summary>
    public class ProprietaryCommandDto
    {
        /// <summary>
        /// The parameters to pass to the device
        /// </summary>
        public IList<KeyValuePair<string, string>> Parameters { get; set; }
    }
}
