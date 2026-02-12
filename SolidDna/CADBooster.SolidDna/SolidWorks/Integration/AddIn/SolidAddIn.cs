using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna;

/// <summary>
/// Integrates into SolidWorks as an add-in and registers for callbacks provided by SolidWorks
/// 
/// IMPORTANT: The class that overrides <see cref="ISwAddin"/> MUST be the same class that 
/// contains the ComRegister and ComUnregister functions due to how SolidWorks loads add-ins
/// </summary>
public abstract class SolidAddIn : ISwAddin
{
    #region Protected Members

    /// <summary>
    /// Flag if we have loaded into memory (as ConnectedToSolidWorks can happen multiple times if unloaded/reloaded)
    /// </summary>
    protected bool mLoaded;

    #endregion

    #region Public Properties

    /// <summary>
    /// The command manager
    /// </summary>
    public CommandManager CommandManager { get; private set; }

    /// <summary>
    /// Provides functions related to SolidDna plug-ins
    /// </summary>
    public PlugInIntegration PlugInIntegration { get; private set; } = new();

    /// <summary>
    /// A list of available plug-ins loaded once SolidWorks has connected
    /// </summary>
    public List<SolidPlugIn> PlugIns { get; set; } = [];

    /// <summary>
    /// The title displayed for this SolidWorks Add-in
    /// </summary>
    public string SolidWorksAddInTitle { get; set; } = "CADBooster SolidDna AddIn";

    /// <summary>
    /// The description displayed for this SolidWorks Add-in
    /// </summary>
    public string SolidWorksAddInDescription { get; set; } = "All your pixels are belong to us!";

    /// <summary>
    /// The SolidWorks cookie for this add-in.
    /// This is used by SolidWorks to link an add-in to its command manager and menus.
    /// </summary>
    public AddInCookie SolidWorksCookie { get; private set; }

    #endregion

    #region Public Events

    /// <summary>
    /// Called once SolidWorks has loaded our add-in and is ready.
    /// Now is a good time to create task panes, menu bars or anything else.
    /// </summary>
    public event Action ConnectedToSolidWorks = () => { };

    /// <summary>
    /// Called once SolidWorks has unloaded our add-in.
    /// Now is a good time to clean up task panes, menu bars or anything else.
    /// </summary>
    public event Action DisconnectedFromSolidWorks = () => { };

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public SolidAddIn()
    {
        PlugInIntegration.ParentAddIn = this;
    }

    #endregion

    #region Public Abstract / Virtual Methods

    /// <summary>
    /// Specific application startup code when SolidWorks is connected 
    /// and before any plug-ins or listeners are informed.
    /// Runs after <see cref="PreConnectToSolidWorks"/> and after <see cref="PreLoadPlugIns"/>.
    /// </summary>
    /// <returns></returns>
    public abstract void ApplicationStartup();

    /// <summary>
    /// Runs immediately when <see cref="ConnectToSW(object, int)"/> is called to do any pre-setup.
    /// For example, call <see cref="Logger.AddFileLogger{TAddIn}"/> to add a file logger for SolidDna messages.
    /// Runs before <see cref="PreLoadPlugIns"/> and before <see cref="ApplicationStartup"/>.
    /// </summary>
    public abstract void PreConnectToSolidWorks();

    /// <summary>
    /// Runs before loading plug-ins.
    /// To make your add-in start up faster, choose one of three options:
    /// <list type="number">
    /// <item>
    /// <description>Fastest: Call <see cref="PlugInIntegration.AddPlugInToLoad{TPlugIn}"/> and pass your plugin type to tell SolidDna to load that plug-in.</description>
    /// </item>
    /// <item>
    /// <description>Set <see cref="PlugInIntegration.AutoDiscoverPlugins"/> to false and add the paths of all DLLs that contain plugins to <see cref="PlugInIntegration.PlugInAssemblyPaths"/>.</description>
    /// </item>
    /// <item>
    /// <description>Default: Do nothing and let SolidDna auto-discover all plug-ins in all DLLs in the SolidDNA DLL folder. This is the slowest.</description>
    /// </item>
    /// </list>
    /// Runs after <see cref="PreConnectToSolidWorks"/> and before <see cref="ApplicationStartup"/>.
    /// </summary>
    /// <returns></returns>
    public abstract void PreLoadPlugIns();

    #endregion

    #region SolidWorks Add-in Callbacks

    /// <summary>
    /// Receives all callbacks from command manager items and flyouts. 
    /// We tell SolidWorks to call a method in the <see cref="SolidAddIn"/> class in <see cref="SetUpCallbacks"/>, it will always look for a method named Callback.
    /// We tell it to call this method in <see cref="CommandManagerGroup.AddCommandItem"/> and <see cref="CommandManagerFlyout.AddCommandItem"/>.
    /// We forward this to <see cref="PlugInIntegration.OnCallback"/>, which then finds the correct command manager item or flyout and calls its OnClick method.
    /// </summary>
    /// <param name="callbackId"></param>
    public void Callback(string callbackId)
    {
        // Log it
        Logger.LogDebugSource($"SolidWorks {nameof(Callback)} fired {callbackId}");

        PlugInIntegration.OnCallback(callbackId);
    }

    /// <summary>
    /// Receives state request callbacks from command manager items and flyouts. 
    /// We tell SolidWorks to call a method in the <see cref="SolidAddIn"/> class in <see cref="SetUpCallbacks"/>, it will always look for a method named ItemStateCheck.
    /// We tell it to call this method in <see cref="CommandManagerGroup.AddCommandItem"/> and <see cref="CommandManagerFlyout.AddCommandItem"/>.
    /// We forward this to <see cref="PlugInIntegration.OnItemStateCheck"/>, which then finds the correct command manager item or flyout and calls its OnEnableStateCheck method.
    /// </summary>
    /// <param name="callbackId"></param>
    /// <returns>State of the item</returns>
    public int ItemStateCheck(string callbackId)
    {
        // Log it
        Logger.LogDebugSource($"SolidWorks {nameof(ItemStateCheck)} fired {callbackId}");

        return PlugInIntegration.OnItemStateCheck(callbackId);
    }

    /// <summary>
    /// Called when SolidWorks has loaded our add-in and wants us to do our connection logic
    /// </summary>
    /// <param name="solidWorksApplication">The current SolidWorks instance</param>
    /// <param name="cookieId">The SolidWorks cookie ID for this add-in. Is usually a low number like 8.</param>
    /// <returns></returns>
    public bool ConnectToSW(object solidWorksApplication, int cookieId)
    {
        try
        {
            // Get the current SolidWorks instance
            var solidworks = (SldWorks) solidWorksApplication;

            // Wrap and store the cookie ID that belongs to this add-in.
            SolidWorksCookie = new AddInCookie(cookieId);

            // Add this add-in to the list of currently active add-ins.
            AddInIntegration.AddAddIn(this);

            // Log it
            Logger.LogTraceSource($"Firing PreConnectToSolidWorks...");

            // Fire event
            PreConnectToSolidWorks();

            // Log it. Todo: add-in title is not yet extracted from a plugin here, so it will always be the default title.
            Logger.LogDebugSource($"{SolidWorksAddInTitle} Connected to SolidWorks...");

            // Set up the current SolidWorks instance as a SolidDNA class. 
            // Also sets up the obsolete command manager static class.
            AddInIntegration.ConnectToActiveSolidWorksForAddIn(solidworks, SolidWorksCookie);

            // Create the command manager for this add-in. SolidWorks uses the cookie to link an add-in to its menus.
            CommandManager = new CommandManager(solidworks.GetCommandManager(SolidWorksCookie.Value), SolidWorksCookie);

            // Tell solidworks which method to call when it receives a button click on a command manager item or flyout.
            SetUpCallbacks(solidworks, SolidWorksCookie);

            // Log it
            Logger.LogDebugSource($"Firing PreLoadPlugIns...");

            // If this is the first load
            if (!mLoaded)
            {
                // Any pre-load steps
                PreLoadPlugIns();

                // Log it
                Logger.LogDebugSource($"Configuring PlugIns...");

                // Perform any plug-in configuration
                PlugInIntegration.ConfigurePlugIns(this);

                // Now loaded so don't do it again
                mLoaded = true;
            }

            // Log it
            Logger.LogDebugSource($"Firing ApplicationStartup...");

            // Call the application startup function for an entry point to the application
            ApplicationStartup();

            // Log it
            Logger.LogDebugSource($"Firing ConnectedToSolidWorks...");

            // Inform listeners
            ConnectedToSolidWorks();

            // And plug-in listeners
            PlugInIntegration.ConnectedToSolidWorks(this);

            // Return ok
            return true;
        }
        catch (Exception e)
        {
            // Log it
            Logger.LogCriticalSource($"Unexpected error: {e}");

            return false;
        }
    }

    /// <summary>
    /// Called when SolidWorks is about to unload our add-in and wants us to do our disconnection logic
    /// </summary>
    /// <returns></returns>
    public bool DisconnectFromSW()
    {
        // Log it
        Logger.LogDebugSource($"{SolidWorksAddInTitle} Disconnected from SolidWorks...");

        // Log it
        Logger.LogDebugSource($"Firing DisconnectedFromSolidWorks...");

        // Inform listeners
        DisconnectedFromSolidWorks();

        // And plug-in listeners
        PlugInIntegration.DisconnectedFromSolidWorks(this);

        // Log it
        Logger.LogDebugSource($"Tearing down...");

        // Remove it from the list and tear down SOLIDWORKS when it was the last add-in.
        AddInIntegration.RemoveAddInAndTearDownSolidWorksWhenLast(this);

        // Remove the loggers for this add-in
        Logger.RemoveLoggers(this);

        // Clear our references
        CommandManager?.Dispose();
        CommandManager = null;
        PlugInIntegration = null;

        // Reset mLoaded so we can restart this add-in
        mLoaded = false;

        // Return ok
        return true;
    }

    /// <summary>
    /// Tell SolidWorks that it should call the <see cref="Callback"/> method in this class whenever it receives a Command Manager item or flyout button click.
    /// We forward this to <see cref="PlugInIntegration.OnCallback"/>, which then finds the correct command manager item or flyout and calls its OnClick method.
    /// SolidWorks also calls the <see cref="ItemStateCheck"/> method in this class whenever it asks to know the disabled/enabled state of a command manager item.
    /// This happens after the SolidWorks window becomes active or the active model changes.
    /// </summary>
    /// <param name="solidworks"></param>
    /// <param name="cookie"></param>
    private void SetUpCallbacks(SldWorks solidworks, AddInCookie cookie)
    {
        // Log it
        Logger.LogDebugSource($"Setting add-in callbacks...");

        // ReSharper disable once UnusedVariable
        var ok = solidworks.SetAddinCallbackInfo2(0, this, cookie.Value);
    }

    #endregion

    #region Connected to SolidWorks Event Calls

    /// <summary>
    /// When the add-in has connected to SolidWorks
    /// </summary>
    public void OnConnectedToSolidWorks()
    {
        // Log it
        Logger.LogDebugSource($"Firing ConnectedToSolidWorks event...");

        ConnectedToSolidWorks();
    }

    /// <summary>
    /// When the add-in has disconnected to SolidWorks
    /// </summary>
    public void OnDisconnectedFromSolidWorks()
    {
        // Log it
        Logger.LogDebugSource($"Firing DisconnectedFromSolidWorks event...");

        DisconnectedFromSolidWorks();
    }

    #endregion

    #region Com Registration

    /// <summary>
    /// The COM registration call to add our registry entries to the SolidWorks add-in registry
    /// </summary>
    /// <param name="t"></param>
    [ComRegisterFunction]
    protected static void ComRegister(Type t)
    {
        try
        {
            // Log the assembly name 
            Logger.LogInformationSource($"Registering {t.AssemblyFilePath()}");

            // Create new instance of a blank add-in
            var addIn = new BlankSolidAddIn();

            // Force auto-discovering plug-in during COM registration
            addIn.PlugInIntegration.AutoDiscoverPlugins = true;

            Logger.LogInformationSource("Configuring plugins...");

            // Let plug-ins configure the add-in title and descriptions
            addIn.PlugInIntegration.ConfigurePlugIns(addIn);

            // Format our guid
            var guid = $"{t.GUID:b}";

            // Register our add-in as a COM object
            AddRegistryKeyLocalMachine(guid, addIn);

            // Make our add-in start up when SOLIDWORKS starts
            AddRegistryKeyCurrentUser(guid);

            Logger.LogInformationSource($"COM Registration successful. '{addIn.SolidWorksAddInTitle}' : '{addIn.SolidWorksAddInDescription}'");
        }
        catch (Exception e)
        {
            Debugger.Break();

            // Get the path to this DLL
            var assemblyLocation = typeof(SolidAddIn).AssemblyFilePath();

            // Create a path for a text file. The assembly location is always lowercase.
            var changeExtension = assemblyLocation.Replace(".dll", ".fatal.log.txt");

            // Log an error to a new or existing text file 
            File.AppendAllText(changeExtension, $"\r\nUnexpected error: {e}");

            Logger.LogCriticalSource($"COM Registration error. {e}");
            throw;
        }
    }

    /// <summary>
    /// Add a Registry to register our add-in COM object.
    /// We need to write to Local Machine, so you need to be an admin to perform this.
    /// There is no way around this for SOLIDWORKS add-ins.
    /// </summary>
    /// <param name="addIn"></param>
    /// <param name="guid"></param>
    private static void AddRegistryKeyLocalMachine(string guid, BlankSolidAddIn addIn)
    {
        // Get registry key path in local machine to register the add-in COM object
        var keyPath = $@"SOFTWARE\SolidWorks\AddIns\{guid}";

        // Create our registry folder for the add-in
        using var registryKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(keyPath);

        // Set SolidWorks add-in title and description
        registryKey.SetValue("Title", addIn.SolidWorksAddInTitle);
        registryKey.SetValue("Description", addIn.SolidWorksAddInDescription);
    }

    /// <summary>
    /// Add a Registry key so our add-in starts up when SOLIDWORKS starts.
    /// </summary>
    /// <param name="guid"></param>
    private static void AddRegistryKeyCurrentUser(string guid)
    {
        // Get registry key path in current user so the add-in starts when SolidWorks opens
        var keyPathCurrentUser = $@"SOFTWARE\SolidWorks\AddInsStartup\{guid}";

        // Create our registry folder for the add-in
        using var registryKeyCurrentUser = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyPathCurrentUser);

        // Load add-in when SolidWorks opens
        registryKeyCurrentUser.SetValue(null, 1);
    }

    /// <summary>
    /// The COM unregister call to remove our custom entries we added in the COM register function
    /// </summary>
    /// <param name="t"></param>
    [ComUnregisterFunction]
    protected static void ComUnregister(Type t)
    {
        // Get registry key path
        var keyPath = $@"SOFTWARE\SolidWorks\AddIns\{t.GUID:b}";

        // Remove our registry entry
        Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(keyPath);
    }

    #endregion
}