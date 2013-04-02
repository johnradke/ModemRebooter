ModemRebooter
======

Periodically checks that an Internet connection is available. If not, turn off an X10 controller (hard coded to house J, device 11) and back on. The idea is that the router is connected to that X10 controller, and so it will be rebooted and the connection restored.

This was useful a few years ago when my router and/or modem were flakey. Rather than having to run down to the basement every couple of days, this program would take care of it for me.

Depends on a .NET X10 serial interface library (CraigsCreations.com.X10) created by Craig Hamilton, which you should be able to acquire here: http://www.craigscreations.com/projects.html#X10Lib
