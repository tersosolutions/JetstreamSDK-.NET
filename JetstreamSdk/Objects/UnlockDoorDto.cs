namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Passed as a parameter to the 
    /// UnlockDoor command for a device 
    /// to provide a security context for
    /// unlocking an enclosure
    /// </summary>
    public class UnlockDoorDto
    {
        public string AccessToken { get; set; }
    }
}
