namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    public interface IJetstreamServiceClient
    {
        /// <summary>
        /// Calls the  AddLogicalDevice REST endpoint and adds a device to your application. 
        /// https://www.jetstreamrfid.com/Documentation/AddLogicalDevice
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        AddLogicalDeviceResponse AddLogicalDevice(AddLogicalDeviceRequest request);

        /// <summary>
        /// Calls the GetConfiguration REST endpoint
        /// https://www.jetstreamrfid.com/Documentation/GetConfiguration
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        GetConfigurationResponse GetConfiguration(GetConfigurationRequest request);

        /// <summary>
        /// Calls the GetDeviceDefinitions REST endpoint
        /// https://www.jetstreamrfid.com/Documentation/GetDeviceDefinitions
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        GetDeviceDefinitionsResponse GetDeviceDefinitions(GetDeviceDefinitionsRequest request);

        /// <summary>
        /// Calls the RemoveLogicalDevice REST endpoint and removes a device from your application.
        /// https://www.jetstreamrfid.com/Documentation/RemoveLogicalDevice
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        RemoveLogicalDeviceResponse RemoveLogicalDevice(RemoveLogicalDeviceRequest request);

        /// <summary>
        /// Calls the GetEPCListCommand REST endpoint to queue a command
        /// https://www.jetstreamrfid.com/Documentation/GetEPCListCommand
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>       
        GetEpcListCommandResponse GetEpcListCommand(GetEpcListCommandRequest request);

        /// <summary>
        /// Calls the DeviceSpecificCommand REST endpoint to queue a command specific to an individual device type
        /// https://www.jetstreamrfid.com/Documentation/DeviceSpecificCommand
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        DeviceSpecificCommandResponse DeviceSpecificCommand(DeviceSpecificCommandRequest request);

        /// <summary>
        /// Calls the ResetCommand REST endpoint to queue a command
        /// https://www.jetstreamrfid.com/Documentation/ResetCommand
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        ResetCommandResponse ResetCommand(ResetCommandRequest request);

        /// <summary>
        /// Calls the GetConfigValuesCommand REST endpoint to queue a command
        /// https://www.jetstreamrfid.com/Documentation/GetConfigValuesCommand
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        GetConfigValuesCommandResponse GetConfigValuesCommand(GetConfigValuesCommandRequest request);

        /// <summary>
        /// Calls the SetConfigValuesCommand REST endpoint to queue a command
        /// https://www.jetstreamrfid.com/Documentation/SetConfigValuesCommand
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        SetConfigValuesCommandResponse SetConfigValuesCommand(SetConfigValuesCommandRequest request);

        /// <summary>
        /// Calls the UpdateFirmwareCommand REST endpoint to queue a command
        /// https://www.jetstreamrfid.com/Documentation/UpdateFirmwareCommand
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        UpdateFirmwareCommandResponse UpdateFirmwareCommand(UpdateFirmwareCommandRequest request);

        /// <summary>
        /// Calls the AddDeviceToPolicy REST endpoint and adds a device to one of your application's policies.
        /// https://www.jetstreamrfid.com/Documentation/AddDeviceToPolicy
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        AddDeviceToPolicyResponse AddDeviceToPolicy(AddDeviceToPolicyRequest request);

        /// <summary>
        /// Calls the AddPolicy REST endpoint and adds a new policy to your application.
        /// https://www.jetstreamrfid.com/Documentation/AddPolicy
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        AddPolicyResponse AddPolicy(AddPolicyRequest request);

        /// <summary>
        /// Calls the GetPolicies REST endpoint and adds a new policy to your application.
        /// https://www.jetstreamrfid.com/Documentation/GetPolicies
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        GetPoliciesResponse GetPolicies(GetPoliciesRequest request);

        /// <summary>
        /// Calls the RemovePolicy REST endpoint and removes an existing policy from your application.
        /// https://www.jetstreamrfid.com/Documentation/RemovePolicy
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        RemovePolicyResponse RemovePolicy(RemovePolicyRequest request);

        /// <summary>
        /// Calls the RemoveDeviceFromPolicy REST endpoint and removes an existing device from a policy
        /// https://www.jetstreamrfid.com/Documentation/RemoveDeviceFromPolicy
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        RemoveDeviceFromPolicyResponse RemoveDeviceFromPolicy(RemoveDeviceFromPolicyRequest request);

        /// <summary>
        /// Calls the GetEvents REST endpoint to fetch a series of events
        /// https://www.jetstreamrfid.com/Documentation/GetEvents
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        GetEventsResponse GetEvents(GetEventsRequest request);

        /// <summary>
        /// Calls the RemoveEvents REST endpoint to remove events based on the batch id or array of event ids
        /// https://www.jetstreamrfid.com/Documentation/RemoveEvents
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        RemoveEventsResponse RemoveEvents(RemoveEventsRequest request);
    }
}