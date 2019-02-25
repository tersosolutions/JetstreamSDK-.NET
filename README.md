![Terso Solutions Logo](https://cdn.tersosolutions.com/github/TersoHorizontal_BlackGreen.png "Terso Solutions, Inc.")

## Jetstream SDK
[Jetstream Documentation - https://jetstreamrfid.com/documentation/applicationapi/2](https://jetstreamrfid.com/documentation/applicationapi/2)
 
### Microsoft .NET Framework 4.5
The SDK has functionality for making application API calls, simulating device calls and receiving messages when implemented in an application (check out our JetstreamSDK Windows Service repo).

### Use the application or device API in a project
Add a reference to the `JetstreamSDK.dll`.

### Implement in a Windows Service
1. Open the JetstreamSDK-WindowsService solutions in [this](https://github.com/tersosolutions/JetstreamSDK-WindowsService) repository.
2. If you want to view how events are processed, add this SDK to the service and change the reference.
3. Build it.
4. [Sign-up](https://www.jetstreamrfid.com/login/signup) for Jetstream. You will receive a login, with an access key that you may use to simulate events.
5. Process those events in the Windows service, managing inventory, access and more.

### Change History
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

### License
Copyright 2018 Terso Solutions, Inc.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at:
[http://www.apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0).

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
