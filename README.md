# ApolloLens

Steps to build project:

1. Start Unity
2. Open the AlphaRelease project folder
3. Open Scenes/Main
4. Open File -> Build Settings
5. Select "Add Open Scenes"
6. Check the box "Unity C# Projects" under Debugging
7. Click build
8. Clear the contents of the Build directory
9. Select this newly cleared directory as the target of the build
10. When the build is done, open the resulting Visual Studio solution file from the build directory
11. Set the build settings to "Release", "x86", and target "Device"
12. Connect the headset via USB
13. Select Debug -> Start without debugging
14. Pair the headset if necessary
15. When the build is done, the app will open in the headset