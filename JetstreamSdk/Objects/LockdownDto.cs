namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// This DTO is used on the POST method on Lockdown
    /// </summary>
    public class LockdownDto
    {
        /// <summary>
        /// Hours to lockdown the device
        /// </summary>
        public int Hours { get; set; }

    }
}
