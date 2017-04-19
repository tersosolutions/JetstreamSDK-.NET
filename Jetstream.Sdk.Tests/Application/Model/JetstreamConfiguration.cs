using System;
using System.Configuration;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Model
{
    public static class JetstreamConfiguration
    {
        #region Data

        private static string _applicationAccessKey = "";
        private static string _selectedConfiguration = "";
        private static string _url = "";
        private static string _logicalDeviceId = "";
        private static string _policyId = "";

        #endregion

        #region Property Getters and Setters

        /// <summary>
        /// ApplicationAccessKey
        /// </summary>
        public static string ApplicationAccessKey
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationAccessKey))
                {
                    _applicationAccessKey = GetValueFromConfigFile("ApplicationAccessKey");
                }
                return _applicationAccessKey;
            }
            set
            {
                _applicationAccessKey = value;
                SaveValueToConfigFile("ApplicationAccessKey", value);
            }
        }

        /// <summary>
        /// Selected Configuration
        /// </summary>
        public static string SelectedConfiguration
        {
            get
            {
                if (string.IsNullOrEmpty(_selectedConfiguration))
                {
                    _selectedConfiguration = GetValueFromConfigFile("SelectedConfiguration");
                }
                return _selectedConfiguration;
            }
            set
            {
                _selectedConfiguration = value;
                SaveValueToConfigFile("SelectedConfiguration", value);
            }
        }

        /// <summary>
        /// Base Jetstream URL
        /// </summary>
        public static string Url
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                {
                    _url = GetValueFromConfigFile("URL");
                }
                return _url;
            }
            set
            {
                _url = value;
                SaveValueToConfigFile("URL", value);
            }
        }

        #endregion

        #region Persist Data in Web.Config

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetValueFromConfigFile(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private static void SaveValueToConfigFile(string name, string value)
        {
            ConfigurationManager.AppSettings[name] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLogicalDeviceId() {
            if (_logicalDeviceId == "" || _logicalDeviceId != null)
            {
                return _logicalDeviceId = "UnitTest_" + GetNumber(11111, 999999);
            }
            return _logicalDeviceId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalDeviceIdKey"></param>
        /// <returns></returns>
        public static string GetDeviceDefinitionId(string logicalDeviceIdKey)
        {
            return ConfigurationManager.AppSettings[logicalDeviceIdKey];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSerialNumberKey"></param>
        /// <returns></returns>
        public static string GetDeviceSerialNumber(string deviceSerialNumberKey)
        {
            return ConfigurationManager.AppSettings[deviceSerialNumberKey];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDeviceSerialNumber()
        {
            return "SerialNoTest_" + GetNumber(11111, 999999);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPolicyId() {
            if (String.IsNullOrEmpty(_policyId))
            {
                return _policyId = Guid.NewGuid().ToString();
            }
            return _policyId;
        }

        public static string GetPolicyName()
        {
            return "PolicyName_" + GetNumber(11111, 999999);
        }

        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        public static int GetNumber(int min, int max)
        {
            lock (SyncLock)
            { // synchronize
                return Random.Next(min, max);
            }
        }

        #endregion

    }
}