Using Websockets to transmit orientation data from smartphone to Unity
------------

This repository provides a starting point for Unity projects that need orientation data from the smartphone.

Example of this code is used here to track chair orientation:
https://www.youtube.com/watch?v=mVOCHHVTcis
More info: http://flrnmrr.com/2017/2/28/reducing-vr-locomotion-motion-sickness-mech-cockpit-controls

Install
=======

Host the html files inside the "HTML controller" folder on a webserver. On my mac, I host the file locally by placing the folder in /Library/WebServer/Documents/ and going to http://MY.MACS.IP/controller/ on my phone. I renamed the folder "controller" for this example.

Inside index.html, make sure to change the IP address to the computer hosting the websocket connection. This does not have to be the same as the web server hosting the HTML files. For convenience, the IP address is displayed when running the Unity project.

After running the Unity project, tap "Start" on the phone. You should see the cube in the scene rotate from the phones orientation.

![Alt text](http://i.imgur.com/Nk0kgnL.png "Screenshot of Unity project running")


License
=======

See https://github.com/sta/websocket-sharp# Socketphonecontroller
