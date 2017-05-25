using System.Collections.Generic;
namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// This DTO is used on the POST method on AccessControl
    /// </summary>
    public class PostAccessControlDto
    {
        /// <summary>
        /// Passes to add to the device
        /// </summary>
        public IEnumerable<string> Add { get; set; }

        /// <summary>
        /// Passes to remove from the device
        /// </summary>
        public IEnumerable<string> Remove { get; set; }

    }
}
