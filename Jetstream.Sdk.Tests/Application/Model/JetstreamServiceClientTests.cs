using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TersoSolutions.Jetstream.SDK.Application.Model;
using TersoSolutions.Jetstream.SDK.DeviceSpecificCommands.TersoEnclosures;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Model
{
    [TestClass]
    public class JetstreamServiceClientTests
    {

        public static string LogicalDeviceId;
        private static string _policyId = "";

        public JetstreamServiceClientTests()
        {
            if (String.IsNullOrEmpty(LogicalDeviceId))
            {
                LogicalDeviceId = JetstreamConfiguration.GetLogicalDeviceId();
            }

        }

        /// <summary>
        /// Method used test method GetConfiguration
        /// </summary>
        [TestMethod]
        public void AddLogicalDeviceTest()
        {
            try
            {
                // construct a Jetstream service client
                JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

                // create the request object
                AddLogicalDeviceRequest request = new AddLogicalDeviceRequest();
                request.DeviceSerialNumber = JetstreamConfiguration.GetDeviceSerailNumber();
                request.LogicalDeviceId = LogicalDeviceId;
                request.Region = Regions.US;
                request.DeviceDefinitionId = "5b6da680-fe54-41e3-968a-bd2ba1e23ebf";

                // call the Jetstream AddLogicalDevice ReST endpoint
                AddLogicalDeviceResponse response = client.AddLogicalDevice(request);

                if (response.Id != null)
                {
                    //  logicalDeviceId = response.Id;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method GetConfiguration
        /// </summary>
        [TestMethod]
        public void GetConfigurationTest()
        {
            try
            {
                // construct a Jetstream service client
                JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

                // create the GetConfiguration request
                GetConfigurationRequest request = new GetConfigurationRequest();

                // call the Jetstream GetConfiguration ReST endpoint
                client.GetConfiguration(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method GetDeviceDefinitions
        /// </summary>
        [TestMethod]
        public void GetDeviceDefinitionsTest()
        {
            try
            {   // construct a Jetstream service client
                JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

                // create the GetDeviceDefinitions request
                GetDeviceDefinitionsRequest request = new GetDeviceDefinitionsRequest();

                // call the Jetstream GetDeviceDefinitions ReST endpoint
                GetDeviceDefinitionsResponse response = client.GetDeviceDefinitions(request);

                if (response.Body != null)
                {
                    //contain data
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method GetEPCListCommand
        /// </summary>
        [TestMethod]
        public void GetEpcListCommandTest()
        {
            try
            {
                JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

                // create the GetEPCListCommandRequest request   
                GetEpcListCommandRequest request = new GetEpcListCommandRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                // call the Jetstream GetEPCListCommand ReST endpoint   
                client.GetEpcListCommand(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method DeviceSpecificCommand
        /// </summary>
        [TestMethod]
        public void DeviceSpecificCommandTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                GetPassesRequest request = new GetPassesRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                // call the Jetstream ReST endpoint 
                client.DeviceSpecificCommand(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method ResetCommand
        /// </summary>
        [TestMethod]
        public void ResetCommandTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                ResetCommandRequest request = new ResetCommandRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                // call the Jetstream ReST endpoint 
                client.ResetCommand(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method GetConfigValuesCommand
        /// </summary>
        [TestMethod]
        public void GetConfigValuesCommandTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                GetConfigValuesCommandRequest request = new GetConfigValuesCommandRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                List<String> lstParam = new List<String>();
                lstParam.Add("IP_SubNet_Gateway");
                request.Parameters = lstParam;

                // call the Jetstream ReST endpoint 
               client.GetConfigValuesCommand(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method SetConfigValuesCommand 
        /// </summary>
        [TestMethod]
        public void SetConfigValuesCommandTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                SetConfigValuesCommandRequest request = new SetConfigValuesCommandRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                //Parameters
                var param = new List<Tuple<string, string>>
                    {
                    Tuple.Create( "IP", "10.6.20.241" ),
                    Tuple.Create( "SubNet", "255.255.255.0")    
                    };
                request.Parameters = param;

                // call the Jetstream ReST endpoint 
                client.SetConfigValuesCommand(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method UpdateFirmwareCommand 
        /// </summary>
        [TestMethod]
        public void UpdateFirmwareCommandTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                UpdateFirmwareCommandRequest request = new UpdateFirmwareCommandRequest();
                request.LogicalDeviceId = LogicalDeviceId;
                request.Component = Components.Agent;
                request.NewDeviceDefinitionId = Guid.NewGuid().ToString();

                // call the Jetstream ReST endpoint 
               client.UpdateFirmwareCommand(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method AddPolicy 
        /// </summary>
        [TestMethod]
        public void AddPolicyTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                AddPolicyRequest request = new AddPolicyRequest();
                request.DeviceDefinitionId = "10f2bdb2-aa2f-44ea-8b74-0990f22b71c8";

                request.Name = JetstreamConfiguration.GetPolicyName();
                var param = new List<Tuple<string, string>>
                {
                    Tuple.Create( "aggregateeventscancount", "2" ),
                    Tuple.Create( "aggregateeventscantime", "10"),
                    Tuple.Create( "antenna1rxsensitivity", "50"),
                    Tuple.Create( "antenna1txpower", "30"),

                    Tuple.Create( "antenna2rxsensitivity", "50" ),
                    Tuple.Create( "antenna2txpower", "30"),
                    Tuple.Create( "antenna3rxsensitivity", "50"),
                    Tuple.Create( "antenna3txpower", "30"), 

                    Tuple.Create( "antenna4rxsensitivity", "50" ),
                    Tuple.Create( "antenna4txpower", "30"),
                    Tuple.Create( "commandpollinterval", "60"),
                    Tuple.Create( "dns", "0.0.0.0"),

                    Tuple.Create( "dooropentimelimit", "300" ),
                    Tuple.Create( "gateway", "0.0.0.0"),
                    Tuple.Create( "ip", "0.0.0.0"),
                    Tuple.Create( "jetstreamdeviceurl", "https://us-device.tersosolutions.com/v1.0/device/"),

                    Tuple.Create( "lockdownhightemp", "127" ),
                    Tuple.Create( "lockdownonacpowerfailure", "0"),
                    Tuple.Create( "lockdownonreaderfailure", "0"),
                    Tuple.Create( "lockdownonhightemp", "0"),

                    Tuple.Create( "logentryeventhightemp", "127" ),
                    Tuple.Create( "logentryeventlowtemp", "-128"),
                    Tuple.Create( "numberofantennas", "4"),
                    Tuple.Create( "logentrylevel", "warning"),

                    Tuple.Create( "objecteventscancount", "2" ),
                    Tuple.Create( "objecteventscantime", "10"),
                    Tuple.Create( "Subnet", "0.0.0.0")
                };

                request.Parameters = param;
                // call the Jetstream ReST endpoint 
                AddPolicyResponse response = client.AddPolicy(request);
                _policyId = response.Id;
                // display the successful result
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method GetPolicies 
        /// </summary>
        [TestMethod]
        public void GetPoliciesTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                GetPoliciesRequest request = new GetPoliciesRequest();
                // call the Jetstream ReST endpoint 
                client.GetPolicies(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method AddDeviceToPolicy 
        /// </summary>
        [TestMethod]
        public void AddDeviceToPolicyTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                AddDeviceToPolicyRequest request = new AddDeviceToPolicyRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                request.PolicyId = _policyId;
                var param = new List<Tuple<string, string>>
                {
                    Tuple.Create("DNS", "192.168.92.1"),
                    Tuple.Create("Gateway", "192.168.92.100"),
                    Tuple.Create("IP", "192.168.92.101"),
                    Tuple.Create("Subnet", "255.255.255.0")
                };

                request.OverrideParameters = param;

                // call the Jetstream ReST endpoint 
                client.AddDeviceToPolicy(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method RemoveDeviceFromPolicy 
        /// </summary>
        [TestMethod]
        public void RemoveDeviceFromPolicyTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                RemoveDeviceFromPolicyRequest request = new RemoveDeviceFromPolicyRequest();
                request.LogicalDeviceId = LogicalDeviceId;

                // call the Jetstream ReST endpoint 
                client.RemoveDeviceFromPolicy(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method GetEvents 
        /// </summary>
        [TestMethod]
        public void GetEventsTest()
        {

            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                GetEventsRequest request = new GetEventsRequest();
                request.Limit = 2;
                // call the Jetstream ReST endpoint 
                client.GetEvents(request);
            }
            catch (Exception ex)
            {
               Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method RemoveEvents 
        /// </summary>
        [TestMethod]
        public void RemoveEventsTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                RemoveEventsRequest request = new RemoveEventsRequest();
                //string arerays
                string[] events = { "adf23b38-5d0f-4665-8624-e104ed1456e2", "35a170cc-7a06-41a6-aa60-db1f5f9e2534", "67f6aef6-35a2-46bb-bc7e-836f094bf7c8" };

                request.EventIds = events;

                // call the Jetstream ReST endpoint 
                client.RemoveEvents(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Method used test method RemovePolicy 
        /// </summary>
        [TestMethod]
        public void RemovePolicyTest()
        {
            // construct a Jetstream service client
            JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

            try
            {
                // create and configure the request object
                RemovePolicyRequest request = new RemovePolicyRequest();
                request.PolicyId = _policyId;

                // call the Jetstream ReST endpoint 
                client.RemovePolicy(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        } 

        /// <summary>
        /// Method used test method RemoveLogicalDevice
        /// </summary>
        [TestMethod]
        public void RemoveLogicalDeviceTest()
        {
            try
            {
                JetstreamServiceClient client = new JetstreamServiceClient(JetstreamConfiguration.Url, JetstreamConfiguration.ApplicationAccessKey);

                // create the RemoveLogicalDevice request   
                RemoveLogicalDeviceRequest removeRequest = new RemoveLogicalDeviceRequest();
                removeRequest.LogicalDeviceId = LogicalDeviceId;

                // call the Jetstream RemoveLogicalDevice ReST endpoint   
                client.RemoveLogicalDevice(removeRequest);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

    }
}
