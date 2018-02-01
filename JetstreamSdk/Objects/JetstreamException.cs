using System;
using System.Net;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Object that handles holding exception info from Jetstream calls
    /// </summary>
    public class JetstreamException : Exception
    {
        /// <summary>
        /// Status code of exception
        /// </summary>
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// Raw JSON of the request body, if it exists
        /// </summary>
        private readonly string _requestBody;

        /// <summary>
        /// Raw JSON of the response body, if it exists
        /// </summary>
        private readonly string _responseBody;

        /// <summary>
        /// Status code of exception
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        /// <summary>
        /// Raw JSON of the request body, if it exists
        /// </summary>
        public string RequestBody
        {
            get { return _requestBody; }
        }

        /// <summary>
        /// Raw JSON of the response body, if it exists
        /// </summary>
        public string ResponseBody
        {
            get { return _responseBody; }
        }

        /// <summary>
        /// Construct a Jetstream exception. 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="requestBody"></param>
        /// <param name="responseBody"></param>
        /// <param name="formattedMessage">We have to ask for a formatted
        ///            message separately because we need to immediately pass it through
        ///            to the base constructor.</param>
        /// <param name="innerException"></param>
        protected internal JetstreamException(HttpStatusCode statusCode, string requestBody, string responseBody, string formattedMessage,
            WebException innerException)
            : base("Jetstream returned an error: " + statusCode + " - " + formattedMessage, innerException)
        {
            _statusCode = statusCode;
            _requestBody = requestBody;
            _responseBody = responseBody;
        }
    }
}
