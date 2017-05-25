using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// This DTO is used on the PUT method on AccessControl
    /// </summary>
    public class PutAccessControlDto
    {
        /// <summary>
        /// Master list of passes for the device
        /// </summary>
        public IEnumerable<string> Passes { get; set; }

    }
}
