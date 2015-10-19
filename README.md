![Terso Solutions Logo](http://www.tersosolutions.com/wp-content/uploads/2011/11/TERSOvect_GreenBlobWhiteText.png "Terso Solutions, Inc.")

## Jetstream SDK
[Jetstream Documentation and Tools - https://jetstreamrfid.com](https://jetstreamrfid.com)
 
###Microsoft .NET Framework 4.5
The SDK allows has functionality for making application API calls, simulating device calls and receiving messages when implemented in an application (check out our JetstreamSDK Windows Service repo)

###Use the application or device API in a project
Add a reference to the `JetstreamSDK.dll`

###Implement in a Windows Service
1. Open the JetstreamSDK-WindowsService solutions in [this](https://github.com/tersosolutions/JetstreamSDK-WindowsService) repository
2. If you want to view the guts of how events are processed, add this SDK to the service and change the reference
3. Build it
4. Sign-up for Jetstream. You will receive a logon, with an access key that you may use to simulate events.
5. Process those events in the Windows service, managing inventory, access and more

### Change History
* v1.5 - October 14, 2015 - added support for the v1.5 endpoints
* v1.4 - March 3, 2014
  * Added support for limit on GetEvents
  * Added support for RemoveEventsById
* v1.3 - October 29, 2013 - bug fixes
* v1.2 - September 27, 2013 - added support for v1.2 get/remove events methods
* v1.1 - October 31, 2012 - bug fixes

### License
Copyright 2015 Terso Solutions, Inc.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at:
http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
