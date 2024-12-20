#if BIE
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using KappiMod.Config;
using KappiMod.Properties;

namespace KappiMod.Loader.BepInEx;

[BepInPlugin(
    KappiMod.Properties.BuildInfo.GUID,
    KappiMod.Properties.BuildInfo.NAME,
    KappiMod.Properties.BuildInfo.VERSION
)]
public class KappiModBepInExPlugin : BasePlugin, IKappiModLoader
{
    private const string IL2CPP_LIBS_FOLDER = "interop";

    public static KappiModBepInExPlugin Instance = null!;

    public string KappiModFolderDestination => Paths.PluginPath;
    public string UnhollowedModulesFolder =>
        Path.Combine(Paths.BepInExRootPath, IL2CPP_LIBS_FOLDER);

    private BepInExConfigHandler _configHandler = null!;
    public ConfigHandler ConfigHandler => _configHandler;

    private static readonly Harmony _harmony = new(KappiMod.Properties.BuildInfo.GUID);
    public Harmony HarmonyInstance => _harmony;

    public event Action? Update;
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
        KappiModCore.Init(this);
    }

    public override void Load()
    {
        Init();
    }
}

#endif
