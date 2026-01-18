using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CADBooster.SolidDna;

/// <summary>
/// Provides functions related to SolidDna plug-ins.
/// Every <see cref="SolidAddIn"/> has one of these to manage the plug-ins that are inside that add-in dll.
/// <para>
/// <b>Warning:</b> If SolidWorks loads multiple add-ins that use SolidDna, .NET will reuse the first loaded CADBooster.SolidDna.dll
/// and share its static values (such as static events and fields) across all add-ins in the same process. This is default .NET behavior:
/// only one version of a DLL is loaded per AppDomain, even with strong-name signing. As a result, static state in SolidDna is shared
/// between all add-ins, and only one version of SolidDna can be used per SolidWorks instance.
/// </para>
/// </summary>
public class PlugInIntegration
{
    #region Private Members

    /// <summary>
    /// A list of plugin types to load.
    /// If this contains a plugin, we skip auto-discovery and loading from assembly paths.
    /// </summary>
    private List<Type> PlugInTypesToLoad { get; } = [];

    #endregion

    #region Public Properties

    /// <summary>
    /// Default true. If true, searches in the directory of the application (where CADBooster.SolidDna.dll is) for any dll that
    /// contains any <see cref="SolidPlugIn"/> implementations and adds them to the <see cref="PlugInAssemblyPaths"/>
    /// during the <see cref="ConfigurePlugIns(SolidAddIn)"/> stage.
    /// If false, the user should add paths to all DLLs that contain <see cref="SolidPlugIn"/> types to the list
    /// <see cref="PlugInAssemblyPaths"/> during the <see cref="SolidAddIn.PreLoadPlugIns"/> method.
    /// Setting this to false will make starting up your add-in faster because we don't have the check each DLL.
    /// </summary>
    public bool AutoDiscoverPlugins { get; set; } = true;

    /// <summary>
    /// The add-in that owns this plugin integration.
    /// </summary>
    public SolidAddIn ParentAddIn { get; set; }

    /// <summary>
    /// A list of assembly paths that contain plug-ins that have been added to be loaded. 
    /// Contains the absolute file paths of all DLLs that contain at least one <see cref="SolidPlugIn"/>.
    /// </summary>
    public List<string> PlugInAssemblyPaths { get; } = [];

    #endregion

    #region Public Events

    /// <summary>
    /// Called when a SolidWorks callback is fired
    /// </summary>
    public static event Action<string> CallbackFired = (name) => { };

    /// <summary>
    /// Called when SolidWorks requests command item state
    /// </summary>
    public static event Action<CommandManagerItemStateCheckArgs> ItemStateCheckFired = (args) => { };

    #endregion

    #region Connected to SolidWorks

    /// <summary>
    /// Called when the add-in has connected to SolidWorks.
    /// Calls all its plug-ins.
    /// </summary>
    public void ConnectedToSolidWorks(SolidAddIn solidAddIn)
    {
        // Inform plug-ins
        solidAddIn.PlugIns.ForEach(plugin =>
        {
            // Log it
            Logger.LogDebugSource($"Firing ConnectedToSolidWorks event for plugin `{plugin.AddInTitle}`...");

            plugin.ConnectedToSolidWorks();
        });
    }

    /// <summary>
    /// Called when the add-in has disconnected from SolidWorks.
    /// Calls all its plug-ins.
    /// </summary>
    public void DisconnectedFromSolidWorks(SolidAddIn solidAddIn)
    {
        // Inform plug-ins
        solidAddIn.PlugIns.ForEach(plugin =>
        {
            // Log it
            Logger.LogDebugSource($"Firing DisconnectedFromSolidWorks event for plugin `{plugin.AddInTitle}`...");

            plugin.DisconnectedFromSolidWorks();
        });
    }

    #endregion

    #region Add Plug-in

    /// <summary>
    /// Adds a plug-in based on its <see cref="SolidPlugIn"/> implementation
    /// </summary>
    /// <typeparam name="TPlugIn">Your class that implements <see cref="SolidPlugIn"/></typeparam>
    [Obsolete("Use AddPlugInToLoad<TPlugIn>() instead.")]
    public void AddPlugIn<TPlugIn>()
    {
        // Get the full path to the assembly
        var fullPath = typeof(TPlugIn).AssemblyFilePath();

        // Add the path to list if it isn't in there yet
        if (!PlugInAssemblyPaths.ContainsIgnoreCase(fullPath))
            PlugInAssemblyPaths.Add(fullPath);
    }

    /// <summary>
    /// Call this method from <see cref="SolidAddIn.PreLoadPlugIns"/> and pass your plugin type to tell SolidDna to load that plug-in.
    /// This is the fastest way to start up.
    /// Option 2: Set <see cref="AutoDiscoverPlugins"/> to false and add the paths of all DLLs that contain plugins to <see cref="PlugInAssemblyPaths"/>.
    /// Option 3: Do nothing and let SolidDna auto-discover all plug-ins in all DLLs in the SolidDNA DLL folder (slowest).
    /// </summary>
    /// <typeparam name="TPlugIn">Your class that implements <see cref="SolidPlugIn"/></typeparam>
    public void AddPlugInToLoad<TPlugIn>() where TPlugIn : SolidPlugIn => PlugInTypesToLoad.Add(typeof(TPlugIn));

    #endregion

    #region SolidWorks Callbacks

    /// <summary>
    /// Called by the SolidWorks domain (<see cref="SolidAddIn"/>) when a callback is fired when a user clicks a command manager item or flyout.
    /// </summary>
    /// <param name="callbackId">The parameter passed into the generic callback</param>
    public void OnCallback(string callbackId)
    {
        try
        {
            // Inform listeners
            CallbackFired(callbackId);
        }
        catch (Exception e)
        {
            Debugger.Break();

            // Log it
            Logger.LogCriticalSource($"{nameof(OnCallback)} failed. {e.GetErrorMessage()}");
        }
    }

    /// <summary>
    /// Called by the SolidWorks domain (<see cref="SolidAddIn"/>) before displaying a command manager item or flyout.
    /// </summary>
    /// <param name="callbackId">The parameter passed into the generic callback</param>
    /// <returns>State of the item</returns>
    public int OnItemStateCheck(string callbackId)
    {
        try
        {
            // Create a new arguments object so we can return a value from only the relevant command manager item or flyout.
            var args = new CommandManagerItemStateCheckArgs(callbackId);

            // Inform listeners
            ItemStateCheckFired(args);

            if (args.Result != CommandManagerItemState.DeselectedEnabled)
            {
            }

            // Pass the result on to SolidWorks
            return (int) args.Result;
        }
        catch (Exception e)
        {
            Debugger.Break();

            // Log it
            Logger.LogCriticalSource($"{nameof(OnItemStateCheck)} failed. {e.GetErrorMessage()}");
            return (int) CommandManagerItemState.DeselectedEnabled;
        }
    }

    #endregion

    #region Configure Plug Ins

    /// <summary>
    /// Runs any initialization code required on plug-ins.
    /// This is run for the ComRegister function and on startup.
    /// </summary>
    /// <param name="parentSolidAddIn">The add-in that owns this integration and the plugins inside its dll.</param>
    public void ConfigurePlugIns(SolidAddIn parentSolidAddIn)
    {
        if (PlugInTypesToLoad.Any())
        {
            // Load plug-ins from specified types. This is the fastest way to start up: 3ms to create a plugin during testing.
            parentSolidAddIn.PlugIns = LoadSpecifiedPluginTypes();
        }
        else if (AutoDiscoverPlugins)
        {
            // Load all plug-ins, either by going through all DLLs in the SolidDNA folder (called auto-discovery) or from specified assembly paths.
            // This the slowest way to start up.
            parentSolidAddIn.PlugIns = LoadAutoDiscoveredPlugins(parentSolidAddIn);
        }
        else
        {
            // Load plug-ins from specified assembly paths, set by the user in SolidAddIn.PreLoadPlugIns
            // This is faster than auto-discovery because we skip checking each DLL for plugins.
            parentSolidAddIn.PlugIns = LoadPluginsFromAssemblyPaths();
        }

        // Log the results
        Logger.LogDebugSource($"{parentSolidAddIn.PlugIns.Count} plug-ins found");

        // Find the first plug-in that has a title and use it as the add-in title and description.
        SetAddInTitleToFirstPluginTitle(parentSolidAddIn);
    }

    /// <summary>
    /// Instantiates and loads plugin types specified in <see cref="PlugInTypesToLoad"/>.
    /// </summary>
    /// <returns>A list of successfully loaded SolidPlugIn instances.</returns>
    private List<SolidPlugIn> LoadSpecifiedPluginTypes()
    {
        var loadedPlugins = new List<SolidPlugIn>();

        // For each plugin type found
        foreach (var type in PlugInTypesToLoad)
        {
            // Instantiate it
            TryToInstantiatePlugin(type, loadedPlugins);
        }

        // Clear the list of types to load because we either succeeded or failed to load them all now.
        PlugInTypesToLoad.Clear();

        // Return the plugins that were loaded successfully
        return loadedPlugins;
    }

    /// <summary>
    /// Discover and load all plugins from all DLL files in the add-in directory path.
    /// </summary>
    /// <param name="parentSolidAddIn"></param>
    /// <returns></returns>
    private List<SolidPlugIn> LoadAutoDiscoveredPlugins(SolidAddIn parentSolidAddIn)
    {
        // Get the directory path to the add-in dll
        var addInDirectoryPath = parentSolidAddIn.AssemblyDirectoryPath();

        // Log it
        Logger.LogDebugSource($"Loading auto-discovered plugins from {addInDirectoryPath}...");

        // Create new empty list
        var loadedPlugins = new List<SolidPlugIn>();

        // Find every DLL assembly in the add-in directory
        var dllPaths = Directory.GetFiles(addInDirectoryPath, "*.dll", SearchOption.TopDirectoryOnly);

        // Look inside each DLL for plugins
        foreach (var path in dllPaths)
            FindAndLoadPluginsInDll(path, loadedPlugins);

        // Return the plugins that were loaded successfully
        return loadedPlugins;
    }

    /// <summary>
    /// Find all plugins inside every DLL in the <see cref="PlugInAssemblyPaths"/> list and load them.
    /// </summary>
    /// <returns></returns>
    private List<SolidPlugIn> LoadPluginsFromAssemblyPaths()
    {
        Logger.LogDebugSource($"Loading plugins from {PlugInAssemblyPaths.Count} assembly paths...");

        // Create new empty list
        var loadedPlugins = new List<SolidPlugIn>();

        // For each assembly path
        foreach (var assemblyPath in PlugInAssemblyPaths)
        {
            // Try and find the SolidPlugIn implementation...
            FindAndLoadPluginsInDll(assemblyPath, loadedPlugins);
        }

        // Return the plugins that were loaded successfully
        return loadedPlugins;
    }

    /// <summary>
    /// Find the first plug-in in the list that has a title and use that as the add-in title and description.
    /// </summary>
    /// <param name="parentSolidAddIn"></param>
    private static void SetAddInTitleToFirstPluginTitle(SolidAddIn parentSolidAddIn)
    {
        // Find first plug-in in the list and use that as the title and description (for COM register)
        var firstPlugInWithTitle = parentSolidAddIn.PlugIns.FirstOrDefault(f => !f.AddInTitle.IsNullOrEmpty());

        // If we don't have a title
        if (firstPlugInWithTitle == null)
        {
            Logger.LogDebugSource("No Plugins found with a title.");
            return;
        }

        // If we do have a title, log it
        Logger.LogDebugSource($"Setting Add-In Title:       {firstPlugInWithTitle.AddInTitle}");
        Logger.LogDebugSource($"Setting Add-In Description: {firstPlugInWithTitle.AddInDescription}");

        // Set title and description details
        parentSolidAddIn.SolidWorksAddInTitle = firstPlugInWithTitle.AddInTitle;
        parentSolidAddIn.SolidWorksAddInDescription = firstPlugInWithTitle.AddInDescription;
    }

    /// <summary>
    /// Loads the dll into the current app domain, and finds any <see cref="SolidPlugIn"/> implementations.
    /// </summary>
    /// <param name="assemblyPath">The full path to the plug-in dll to load</param>
    /// <param name="loadedPlugins">Plugins that have been loaded successfully</param>
    private void FindAndLoadPluginsInDll(string assemblyPath, List<SolidPlugIn> loadedPlugins)
    {
        try
        {
            // Get all types in the assembly
            var types = GetTypesInAssembly(assemblyPath);

            // Find all types that implement SolidPlugIn
            var pluginTypes = types.Where(p => typeof(SolidPlugIn).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            // For each plugin type found
            foreach (var pluginType in pluginTypes)
                TryToInstantiatePlugin(pluginType, loadedPlugins);
        }
        catch (Exception e)
        {
            // Log error
            Logger.LogCriticalSource($"Error loading plugins from assembly path {assemblyPath}: {e}");
        }
    }

    /// <summary>
    /// Get all types that are in an assembly DLL.
    /// </summary>
    /// <param name="assemblyPath">The full path to an assembly (.dll) file</param>
    /// <returns></returns>
    private static Type[] GetTypesInAssembly(string assemblyPath)
    {
        try
        {
            // Load the assembly. NOTE: Calling LoadFrom instead of LoadFile will auto-resolve references in that folder, otherwise they won't resolve.
            // For this reason, its important that plug-ins are in the same folder as the CADBooster.SolidDna.dll and all other used references
            var assembly = Assembly.LoadFrom(assemblyPath);

            // If we get to here, we have succeeded at loading the assembly. Find all types in an assembly. 
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException)
        {
            // Catch assemblies that don't allow this.
            return [];
        }
    }

    /// <summary>
    /// Try to load a plug-in type by instantiating it. Logs success and failure and catches exceptions.
    /// </summary>
    /// <param name="pluginType"></param>
    /// <param name="loadedPlugins"></param>
    private void TryToInstantiatePlugin(Type pluginType, List<SolidPlugIn> loadedPlugins)
    {
        try
        {
            // Create a SolidDna plugin class instance
            if (Activator.CreateInstance(pluginType) is SolidPlugIn plugIn)
            {
                // Store the add-in that owns this plugin
                plugIn.ParentAddIn = ParentAddIn;

                // Log it
                Logger.LogDebugSource($"Loaded plugin {plugIn.AddInTitle}");

                // Add it to the list
                loadedPlugins.Add(plugIn);
            }
            else
                Logger.LogCriticalSource($"Failed to load plugin");
        }
        catch (Exception e)
        {
            Logger.LogCriticalSource($"Error loading plugin: {e}");
        }
    }

    #endregion
}