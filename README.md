<p align="left" bgcolor="grey">
<img src="http://www.tersosolutions.com/wp-content/uploads/2011/11/TERSOvect_GreenBlobWhiteText.png" alt="Terso Solutions, Inc.">
</p>

## Jetstream SDK 
###Microsoft .NET Framework 4.0
The SDK allows has functionality for making application API calls, simulating device calls and receiving messages via Amazon SQS or the local Jetstream Ground messaging service.

###Build
1. Open JetstreamSDK solution in Visual Studio 2010 or later
2. Build
3. Enjoy

###Use the application or device API in a project
Add a reference to the JetstreamSDK.dll

###TersoSolutions.Application.SQS.JetstreamSQSService Setup
1. Create new x86 Windows Service Project
2. Add references to JetstreamSDK, AWSSDK and NewtonSoft
3. Change from 4.0 client framework to 4.0 framework
4. Add a reference to the 4.0 System.Configuration
5. Add AWSAccessKey, AWSSecretAccessKey and QueueUrl to your app.config appSettings keys
6. Update your Service class to inherit from JetstreamSQSService
7. Override the Process* methods for the events you want to process
8. create an EventLog source using the command line /runas admin for logging
	eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO JetstreamSDK /D "Created JetstreamSDK Event Log Source"
9. Use Regedit.exe to set HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\JetstreamSDK\EventMessageFile to C:\Windows\Microsoft.NET\Framework\v4.0.30319\EventLogMessages.dll
10.Use regedit.exe to delete HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\JetstreamSDK\CustomSource
11. Use regedit.exe to delete HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\JetstreamSDK\TypesSupported