namespace TersoSolutions.Jetstream.Sdk
{
    /// <summary>
    /// Types of device credentials allowed
    /// </summary>
    public enum DeviceCredentialTypes
    {
        /// <summary>
        /// A simple identifier that is not tied to an individual
        /// </summary>
        Basic,

        /// <summary>
        /// Consists of a username and password pair
        /// </summary>
        User,

        /// <summary>
        /// Consists of an ID and pin pair
        /// </summary>
        Pin
    }
}
