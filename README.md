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
* v1.5 - October 14, 2015
  * Added support for the v1.5 endpoints
* v1.4 - March 3, 2014
  * Added support for limit on GetEvents
  * Added support for RemoveEventsById
* v1.3 - October 29, 2013
  * Bug fixes
* v1.2 - September 27, 2013
  * Added support for v1.2 Get/Remove Events methods
* v1.1 - October 31, 2012
  * Bug fixes
