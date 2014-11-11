ProcessKiller
=============

App to kill unwanted processes. If you would like to use it, simply grab exe file and config file from Binnary directory. Config file is a list of processes to kill with new line separator:

    ﻿#ignored
	tokill;12
	
	totalcmd
	to kill 2

All lines starting from # are comments. Number after ; is delay in seconds before first run, it's optional. If you wan't to kill system processes, you will have to run it as Administrator user. If you would like to run it from auto start, in Windows 8 run:

	%AppData%\Microsoft\Windows\Start Menu\Programs\Startup

this will open up startup folder, just paste a link to application to this folder to run it every time windows starts up. To close app, just double click tray icon.
