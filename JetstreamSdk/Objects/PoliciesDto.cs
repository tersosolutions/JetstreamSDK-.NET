using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// A data transfer object that 
    /// defines a policy and its properties
    /// </summary>
    public class PoliciesDto
    {
        /// <summary>
        /// The name of the policy
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Referenced name of the device definition
        /// to which the policy applies
        /// </summary>
        public string DeviceDefinition { get; set; }

        /// <summary>
        /// Configuration parameters and their values
        /// as described in a device definition. 
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// A list of proprietary commands that are applicable
        /// to the device on which this policy is applied
        /// </summary>
        public List<string> ProprietaryCommands { get; set; }
    }
}
