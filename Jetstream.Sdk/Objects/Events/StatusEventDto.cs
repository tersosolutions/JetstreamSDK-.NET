/*
    Copyright 2023 Terso Solutions, Inc.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

namespace TersoSolutions.Jetstream.Sdk.Objects.Events
{
    /// <summary>
    /// StatusEvent reports several pieces of device information every time the device is powered on, and during it's nightly scan. This
    /// information is particularly helpful to reference if troubleshooting a networking issue.
    /// </summary>
    public class StatusEventDto : DeviceEventDto
    {
        /// <summary>
        /// Hardware running the OS
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// Operating system version
        /// </summary>
        public string OSVersion { get; set; }

        /// <summary>
        /// Firmware version
        /// </summary>
        public string AgentVersion { get; set; }

        /// <summary>
        /// RFID scanner firmware version
        /// </summary>
        public string ScannerVersion { get; set; }

        /// <summary>
        /// SSL version
        /// </summary>
        public string SSLVersion { get; set; }

        /// <summary>
        /// cURL version
        /// </summary>
        public string CurlVersion { get; set; }

        /// <summary>
        /// IP address supplied by DHCP
        /// </summary>
        public string ActualIP { get; set; }

        /// <summary>
        /// Subnet address supplied by DHCP
        /// </summary>
        public string ActualSubnet { get; set; }

        /// <summary>
        /// Gateway address supplied by DHCP
        /// </summary>
        public string ActualGateway { get; set; }

        /// <summary>
        /// Primary DNS address supplied by DHCP
        /// </summary>
        public string ActualDNS { get; set; }

        /// <summary>
        /// MAC address of the hardware
        /// </summary>
        public string MACAddress { get; set; }

        /// <summary>
        /// Percentage of free memory
        /// </summary>
        public string FreeMem { get; set; }

        /// <summary>
        /// Duration of running time in Linux format
        /// </summary>
        public string Uptime { get; set; }

        /// <summary>
        /// Duration of the WebSocket time in Linux format
        /// </summary>
        public string WebsocketUptime { get; set; }

        /// <summary>
        /// Number of WebSocket reconnects
        /// </summary>
        public string WebsocketReconnects { get; set; }

        /// <summary>
        /// Firmware version of the MSP processor
        /// </summary>
        public string MSPVersion { get; set; }
    }
}
