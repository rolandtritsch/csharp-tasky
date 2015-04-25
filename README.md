# C# Xamarin Tasky example project (for Android and iOS)

To make this work (on a Mac) you need to ...

* install git (using port)
* install [Xamarin](http://xamarin.com/platform) (and all it needs (e.g. XCode, Android, ...))
* clone the repo to a target directory of your choice
* start Xamarin and open the solution. In the solution you will find ...
    * Core Project - the core files. This is the platform independent *model* (i.e. the ORM layer). This is not a build artifact. Just files.
    * Core.Android - the core lib for Android. The core files are *linked* into this project. A library is build.
    * Android - the Android project. This project implements the Android specific piece of the solution, i.e. all of the GUI (all views and controllers (i.e. all screens)).
    * Core.iOS - the core lib for iOS.
    * iOS - the iOS project.
 * Right click on the iOS project and use `Run with ...` to start an iPhone 6 iOS 8.3 simulator to run the app.
 * when you are done playing ... stop/quit the iOS simulator.
 * create an Android simulator (API Level 21 - no google service needed)
     * you can use the `Tools` drop-down menu to start the Android Device Manager
 * Right click on the Android project and use `Run with ...` to start the simulator you have just created.

 With that you should be in business. Now you have a working cross-platform project.