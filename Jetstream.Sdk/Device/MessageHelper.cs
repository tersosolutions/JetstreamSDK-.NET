﻿/*
     Copyright 2015 Terso Solutions, Inc.

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

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TersoSolutions.Jetstream.SDK.Device
{
    /// <summary>
    /// Exception class used to return any non HTTP 200 responses from Jetstream
    /// </summary>
    public class JetstreamResponseException : Exception
    {
        internal JetstreamResponseException(int statusCode, string statusCodeDescription, string request, string response)
            : base("Jetstream returned an error status code " + statusCode + " (" + statusCodeDescription + ")")
        {
            StatusCode = statusCode;
            StatusCodeDescription = statusCodeDescription;
            Request = request;
            Response = response;
        }

        /// <summary>
        /// The HTTP status code returned from Jetstream
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// A description of the HTTP status code returned from Jetstream
        /// </summary>
        public string StatusCodeDescription { get; set; }

        /// <summary>
        /// The HTTP request URL
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// The raw HTTP response body returned from Jetstream
        /// </summary>
        public string Response { get; set; }
    }

    // Helper classes should be instantiated as static
    internal static class MessageHelper
    {
        internal static string SendToJetStream(String url, String body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                // create the new httpwebrequest with the uri for Jetstream
                request.ContentLength = 0;
                // set the method to POST
                request.Method = "POST";

                if (!String.IsNullOrEmpty(body))
                {
                    request.ContentType = "application/xml; charset=utf-8";
                    byte[] postData = Encoding.Default.GetBytes(body);
                    request.ContentLength = postData.Length;
                    using (Stream s = request.GetRequestStream())
                    {
                        s.Write(postData, 0, postData.Length);
                    }
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new JetstreamResponseException(((int)response.StatusCode),
                            response.StatusCode.ToString(), request.RequestUri.ToString(),
                            responseString);
                    }
                    return responseString;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = response as HttpWebResponse;
                    if (httpResponse != null)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            data.Position = 0;
                            throw new JetstreamResponseException(((int)httpResponse.StatusCode),
                                    httpResponse.StatusCode.ToString(), request.RequestUri.ToString(),
                                    new StreamReader(data).ReadToEnd()
                                );
                        }
                    }
                    throw;
                }
            }
        }

        internal static string SerializeObject(Type type, object request)
        {
            // serialize the request object into XML using XML serializer
            XmlSerializer serializer = new XmlSerializer(type);
            string message;

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true });
                serializer.Serialize(writer, request);
                ms.Position = 0;
                message = new StreamReader(ms).ReadToEnd();
            }

            return message;
        }

    }
}
