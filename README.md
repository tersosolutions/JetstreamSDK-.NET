![Terso Solutions Logo](https://cdn.tersosolutions.com/github/TersoHorizontal_BlackGreen.png "Terso Solutions, Inc.")

## Jetstream SDK
[Jetstream Documentation - https://jetstreamrfid.com/documentation/applicationapi/3](https://jetstreamrfid.com/documentation/applicationapi/3)
 
### Microsoft .NET Framework 4.7.2 / Standard 2.0
The SDK has functionality for making application API calls, simulating device calls and receiving messages when implemented in an application.

### Use the application or device API in a project
Add a reference to the `JetstreamSDK.dll` or search for Jetstream.Sdk on NuGet

### Change History
* v4.1.0 - June 6, 2024
  * Add option to get detailed policies
  * Add option to send device configuration
* v4.0.0 - June 6, 2024
  * Upgrade to netstandard2.1
* v3.4.3 - July 15, 2022
  * Fix DevicePolicyDto to allow null on LastPolicyUpdate and LastPolicySync
* v3.4.2 - July 14, 2022
  * Add missing policy search parameter for device search
* v3.4.1 - June 29, 2022
  * Add policy ID to PolicyDTO
* v3.4 - June 23, 2022
  * Add sonar ruleset
  * Add client factory for DI purposes
  * Add status event DTO
  * Improve tests
  * Update methods to be async tasks
  * Update library depedencies to latest
  * Remove Framework 4.7 support since standard 2.0 can be used in framework and higher
* v3.2 - July 23, 2020
  * Update calls to be asynchronyous
* v3.1 - April 23, 2019
  * Target .NET standard 2.0 and Framework 4.7.2
  * Added support for deploying to NuGet and GitHub from VSTS
  * Created license file
* v3.0 - February 28, 2019 - added support for Jetstream v3. See API documentation for more details.
* v2.3 - September 12, 2018 - changed SensorReadingEvent and LogEntryEvent fields to be DateTimes instead of strings.
* v2.2 - August 1, 2018 - added Set and Get AppConfigValues and deprecated Proprietary Commands.
* v2.1 - February 1, 2018 - added a JetstreamException class for ease of use, along with other improvements.
* v2.0 - May 25, 2017 - upgraded and redesigned SDK to support Jetstream version 2 endpoints and JSON.
* v1.5 - October 14, 2015 - added support for the v1.5 endpoints
* v1.4 - March 3, 2014
  * Added support for limit on GetEvents
  * Added support for RemoveEventsById
* v1.3 - October 29, 2013 - bug fixes
* v1.2 - September 27, 2013 - added support for v1.2 get/remove events methods
* v1.1 - October 31, 2012 - bug fixes
