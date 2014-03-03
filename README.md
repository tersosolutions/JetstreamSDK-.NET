![Terso Solutions Logo](http://www.tersosolutions.com/wp-content/uploads/2011/11/TERSOvect_GreenBlobWhiteText.png "Terso Solutions, Inc.")

## Jetstream SDK
[Jetstream Documentation and Tools - https://jetstreamrfid.com](https://jetstreamrfid.com)
 
###Microsoft .NET Framework 4.0
The SDK allows has functionality for making application API calls, simulating device calls and receiving messages via the Jetstream messaging service.

###Build
1. Open JetstreamSDK solution in Visual Studio 2010 or later
2. Build
3. Enjoy

###Use the application or device API in a project
Add a reference to the `JetstreamSDK.dll`

###TersoSolutions.Jetstream.SDK.Application.Events.JetstreamEventService Setup
1. Create new x86 Windows Service Project
2. Add references to JetstreamSDK and NewtonSoft
3. Change from 4.0 client framework to 4.0 framework
4. Add a reference to the 4.0 System.Configuration
5. Update your Service class to inherit from TersoSolutions.Jetstream.SDK.Application.Events.JetstreamEventService
6. Update the app.config file and add keys and values in the appSettings block for:
   * JetstreamUrl - The Jetstream endpoint
   * UserAccessKey - The user access key assigned to you
   * MessageCheckWindow - The frequency to check for new messages.  e.g. 1.0:0:0 means to check once per hour
7. Override the Process* methods for the events you want to process
8. Create an EventLog source using the command line /runas admin for logging  
 	`eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO JetstreamSDK /D "Created JetstreamSDK Event Log Source"`
9. Use Regedit.exe to set HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\JetstreamSDK\EventMessageFile to C:\Windows\Microsoft.NET\Framework\v4.0.30319\EventLogMessages.dll
10. Use regedit.exe to delete HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\JetstreamSDK\CustomSource
11. Use regedit.exe to delete HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\JetstreamSDK\TypesSupported

### Change History
* v1.4 - March 3, 2014
  * Added support for limit on GetEvents
  * Added support for RemoveEventsById
* v1.3 - October 29, 2013
  * Bug fixes
* v1.2 - September 27, 2013
  * Added support for v1.2 Get/Remove Events methods
* v1.1 - October 31, 2012
  * Bug fixes