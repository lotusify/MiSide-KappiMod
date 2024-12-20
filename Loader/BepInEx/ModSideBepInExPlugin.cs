#if BIE
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using ModSide.Config;
using ModSide.Properties;

namespace ModSide.Loader.BepInEx;

[BepInPlugin(
    ModSide.Properties.BuildInfo.GUID,
    ModSide.Properties.BuildInfo.NAME,
    ModSide.Properties.BuildInfo.VERSION
)]
public class ModSideBepInPlugin : BasePlugin, IModSideLoader
{
    private const string IL2CPP_LIBS_FOLDER = "interop";

    public static ModSideBepInPlugin Instance = null!;

    public string ModSideFolderDestination => Paths.PluginPath;
    public string UnhollowedModulesFolder =>
        Path.Combine(Paths.BepInExRootPath, IL2CPP_LIBS_FOLDER);

    private BepInExConfigHandler _configHandler = null!;
    public ConfigHandler ConfigHandler => _configHandler;

    private static readonly Harmony _harmony = new(ModSide.Properties.BuildInfo.GUID);
    public Harmony HarmonyInstance => _harmony;

    public event Action<object>? Update;
    public event Action<int, string>? SceneWasLoaded;
    public event Action<int, string>? SceneWasInitialized;

    private ManualLogSource LogSource => Log;
    public Action<object> OnLogMessage => LogSource.LogMessage;
    public Action<object> OnLogWarning => LogSource.LogWarning;
    public Action<object> OnLogError => LogSource.LogError;

    private void Init()
    {
        Instance = this;
        _configHandler = new BepInExConfigHandler();
        ModSideCore.Init(this);
    }

    public override void Load()
    {
        Init();
    }
}

#endif
