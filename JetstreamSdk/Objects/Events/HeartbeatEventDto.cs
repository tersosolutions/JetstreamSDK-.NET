using System;
using Newtonsoft.Json.Converters;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// Empty DTO, but uses inheritance to carry fields.
    /// Used for typing/casting of events.
    /// </summary>
    public class HeartbeatEventDto : DeviceEventDto
    {
    }
}
