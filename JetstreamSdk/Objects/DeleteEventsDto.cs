namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// An object used to identify which Jetstream events
    /// to delete
    /// </summary>
    public class DeleteEventsDto
    {
        /// <summary>
        /// The batch id for the list of events
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// The list of event ids to delete
        /// </summary>
        public string[] EventIds { get; set; }

    }
}
