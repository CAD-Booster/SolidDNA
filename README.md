# SolidDNA

## A user-friendly framework for SOLIDWORKS add-ins
SolidDNA is a great framework to build SOLIDWORKS add-ins because it acts as a wrapper around the core SOLIDWORKS API. If a SOLIDWORKS API topic is hard to understand or otherwise annoying, we create a more user-friendly version for it. 

We'd love your help to keep expanding and improving this project.

# Getting Started

## NuGet Package
![Nuget](https://img.shields.io/nuget/v/CADBooster.SolidDna)
[CADBooster.SolidDna on NuGet](https://www.nuget.org/packages/CADBooster.SolidDna)

## AngelSix videos
Here are some videos by AngelSix on how to get started with developing your own SOLIDWORKS add-ins with C# and SolidDNA. They are a little dated by now, but we got started this way.

- Channel: https://www.youtube.com/c/angelsix
- Playlist: https://www.youtube.com/playlist?list=PLrW43fNmjaQVMN1-lsB29ECnHRlA4ebYn

## How to create an add-in 
If you are completely new to software development, you first need to download an IDE (a development environment like [Visual Studio Community](https://visualstudio.microsoft.com/downloads/)) and a git client (for version control) like [Gitkraken](https://www.gitkraken.com/git-client) or [Github Desktop](https://desktop.github.com/download/). All have free versions.

Once you have Visual Studio open:
1. Click 'Create a new project':
     1. Select the type "Class library (.NET Framework)
     2. Call it something like YourCompany.MyAddIn and select a location for your files.
     3. Select ".NET Framework 4.8" for the framework.
     4. Visual Studio will also create a new Solution for you, which can contain multiple projects.
     5. If you're creating a small add-in, a single project is enough and it can have the same name as the solution.
3. Add a reference to our NuGet package: 
    1. Go to your project tree, right-click Dependencies and click Manage NuGet Packages:
    2. Go to the Browse tab and search for CADBooster.SolidDNA.
    3. Click Install on the right.
    4. [NuGet](https://www.nuget.org/) is a package manager that lets you add functionality to your product with a few clicks.
4. Add references to the SOLIDWORKS DLL files:
    1. Download the SOLIDWORKS DLL files from our [References](https://github.com/CAD-Booster/solidworks-api/tree/master/References) folder or get them from your SOLIDWORKS folder (C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\api\redist). The version does not really matter, so use the latest version.
    2. Copy all files into your project folder, for example to a References folder.
    3. Go to your project tree, right-click Dependencies and click Add Assembly Reference.
    4. Click Browse and select all 9 SOLIDWORKS DLLs. Only the ones that you really use will get copied to the output folder later on.
    5. Expand Dependencies > Assemblies in the tree, select all SOLIDWORKS DLLs and set their property Embed Interop Types to No.
5. Create a class that implements SolidAddin:
    1. Right-click your project > Add > Class, name it something like MyAddIn.cs.
    2. Make sure the class is marked as "public" because we will need to register our class in the Windows Registry later.
    3. Add a Guid attribute and a ComVisible(true) attribute. [Create a new GUID](https://guidgenerator.com/) and add it, do not copy the example value. This GUID is the unique identifier for your add-in, so it can never ever change. If you become a SOLIDWORKS partner, you give them this GUID as well.
    4. Add " : SolidAddin" after your class name to make your class implement the SolidAddIn class. Visual Studio will tell you that you need to implement three methods if you want to implement SolidAddIn, so do that.
    5. Add a MessageBox.Show line so we can verify that we started up correctly.
    5. The result should be something like this:

![image](https://github.com/user-attachments/assets/7e61fcd3-f592-4f63-88f8-94c8296f02d2)

6. Create a class that implements SolidPlugIn:
    1. Follow the same steps as for the add-in class. I'm not 100% sure this class also needs to be public, but I make it public, add a GUID and a ComVisible(true) attribute anyway.
    2. Add " : SolidPlugIn" after your class name to make your class implement the SolidPlugIn class. Visual Studio will tell you that you need to implement two methods and two properties to implement SolidPlugIn, so do that.
    3. The result should be something like this:

![image](https://github.com/user-attachments/assets/236bf5e7-b874-48ba-b548-eebf59ee3a7d)

7. Build your solution:
    1. Select Debug from the dropdown at the top of Visual Studio.
    2. Click Build > Build Solution or press Ctrl Shift B. You will either see errors or see a message "Build succeeded" in the bottom left.
    3. Your result files will appear in your code folder under "bin/Debug/net48". There will be a DLL file for each of your projects and a copy of the SOLIDWORKS DLL files you used will also appear here.
8. Register your add-in:
    1. A SOLIDWORKS add-in is not an EXE file that you can run. You create a DLL file and tell SOLIDWORKS that it needs to look inside that add-in for a class that implements SolidAddIn. It uses the Windows Registry for this, so we need to add our add-in to the Registry.
    2. Download the code for our [Add-in Installer](https://github.com/CAD-Booster/solidworks-api/tree/master/Tools/Addin%20Installer), open the solution, build it and copy the resulting EXE to a handy location like your desktop or project folder.
    3. Run the EXE
    4. Click the second Browse button and select your project DLL files in "bin/Debug/net48".
    5. Click Install. This runs a tool called RegAsm, which identifies the public classes inside your DLL that need to be added to the Windows Registry. It also adds a key to add your add-in to the list of SOLIDWORKS add-ins. You can read more [here](https://github.com/CAD-Booster/solidworks-api/tree/master/Tools/Addin%20Installer).
    6. Later: click Uninstall to remove your add-in again. Always do this before making big changes like renaming your add-in or plugin or changing a GUID, or you'll mess up your registry and the add-in may not start.
9. Run your add-in:
    1. If you now start SOLIDWORKS, your add-in should start as well! Pretty cool, right?
    2. If the add-in does not start, check the list of add-ins to make sure it's enabled.
    3. If you enable the add-in, close the add-ins window and the checkbox gets unchecked again, there was some sort of error and the add-in did not start correctly.
9. Debug your add-in:
    1. We can also start up SOLIDWORKS from Visual Studio, which lets us debug our add-in and run the code line by line. This is crucial for proper software development.
    2. Go into your project folder, then inside the Properties folder.
    3. Create a file called "launchSettings.json" and add at least one profile. Make sure the path is correct, you can choose the name (like SW2024 here) yourself. I added two SOLIDWORKS versions here.
    4. Open Visual Studio again and select one of your profiles next to the Debug dropdown and the play button.
    5. Set a breakpoint in your add-in or plugin code by clicking left of a relevant line of code. This will highlight the line in red. If we reach that code, the debugger will pause there.
    6. Click play or press F5. This starts the debugger and SOLIDWORKS.
    7. Once you hit your breakpoint, hit F5 to continue until the next breakpoint or hit F10 to go to the next line.
    8. Extra tip: enable [Hot Reload](https://learn.microsoft.com/en-us/visualstudio/debugger/hot-reload?view=vs-2022&pivots=programming-language-dotnetf) so you can make changes and save your code while SOLIDWORKS is running. Very often, you can make quick changes this way without having to restart SOLIDWORKS.
  
![image](https://github.com/user-attachments/assets/f8656a5a-4465-4409-a96f-d9af4984a020)

![image](https://github.com/user-attachments/assets/afa561b1-cb1a-48fe-8226-bb46cbfa754c)

You now have the absolute basics in place for a SOLIDWORKS add-in! 

# Getting started with the SOLIDWORKS API 
We're writing a series of articles about getting started with the SOLIDWORKS API. We're starting with the absolute basics and are trying to include all weird API behaviors beginners may trip over. Because the API docs will not teach you those tricks.

- [The SOLIDWORKS Object Model + API explained](https://cadbooster.com/the-solidworks-object-model-api-explained-part-1/)
- [SOLIDWORKS API: the basics – SldWorks, ModelDoc2](https://cadbooster.com/solidworks-api-basics-sldworks-modeldoc2/)
- [How to work with Features ](https://cadbooster.com/how-to-work-with-features-in-the-solidworks-api/)
- [Persistent ID and sketch segment ID and more](https://cadbooster.com/persistent-id-sketch-segment-id-in-the-solidworks-api/)
- [All identifiers in the SOLIDWORKS API](https://cadbooster.com/all-identifiers-and-ids-in-the-solidworks-api/)
- [About return values](https://cadbooster.com/about-return-values-in-the-solidworks-api-part-6/)
- [Entities and GetCorresponding](https://cadbooster.com/entities-and-getcorresponding-in-the-solidworks-api/)
- Selections, math and custom properties (coming soon)
- How to create task panes and Property Manager Pages (coming soon)
- How to create your own SOLIDWORKS add-in (coming soon)
- Creating add-ins with SolidDNA: the basics (coming soon)

# About this fork
This repository is a fork of [SolidDNA](https://github.com/angelsix/solidworks-api) by AngelSix. Because SolidDNA wasn't actively maintained and our proposed fixes were effectively ignored, we eventually decided to fork it, apply our improvements and publish the results. 

Since then, we fixed a load of bugs, made SolidDNA capable of running multiple add-ins at the same time and strong-name signed the NuGet package. Signing allows multiple versions of SolidDNA to be loaded at the same time.

To achieve that, we removed the dependency injection (because all add-ins run in the same thread), removed running in a separate app domain (because it exposed SOLIDWORKS bugs) and removed the reference to the Dna Framework (so we don't have to strong-name sign it).

We will keep expanding SolidDNA so you and I have to use less and less of the core SOLIDWORKS API. And we'd love your help. Check out the list of issues (or create one yourself) and send us a pull request.

# About CAD Booster
We build intuitive add-ins for SOLIDWORKS to automate the boring bits of engineering. Our main products are [Drew](https://cadbooster.com/solidworks-add-in/drew/) (create 2D drawings twice as fast), [Lightning](https://cadbooster.com/solidworks-add-in/lightning-fastener-filter/) (makes working with fasteners fun again) and [Fastener Models](https://cadbooster.com/fastener-models/) (a great Toolbox alternative).

We use SolidDNA in all of our add-ins. 
