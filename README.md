# SW.DatabaseCheckout

A functional example of SignalR Client and Server (WebServer) application integrating with Slack Incoming Webhook.
This app controls a Single Phase Lock condition based on a given process executing on the client machine. The server communicates with all connected clients informing the lock phase. Only one connected client can lock the phase in Server and this same client must unlock it so other connected client can lock it again.
I wrote this application to better understand the SignalR library using a remote server and to solve one personal problem.

The condition to Lock and Unlock is specific to my needs, but you can easily change it.

There are 4 Projects within the Solution:

- SW.DatabaseCheckout.Client

  -This is the client application. It connects to the server and informs the lock phase on client and reflects the server lock phase. It was designed as a formless application with a System Tray icon.

- SW.DatabaseCheckout.Server

  -This is NOT the main server, it is an WinForm server which you can run on your machine for testing purpose before publishing the WebServer. It implements the same SingleLockHub as the WebServer. It's currently configured to run at `http://localhost:8080` as in the app.config.

- SW.DatabaseCheckout.Util

  -Contains public Resources (Strings), one method that returns true if a given process is running and one method to post a message to Slack (You must configure you WebHook before using this) See https://api.slack.com/incoming-webhooks for reference.

- SW.DatabaseCheckout.WebServer

  -This is the WebServer, as you'll see, it does nothing more than Mapping SignalR (Startup.cs). To run the WebServer on Windows Server environment, you need to install and enable Web Sockets protocol.

Note: You should restore all packages using NuGet package restore for solution.
