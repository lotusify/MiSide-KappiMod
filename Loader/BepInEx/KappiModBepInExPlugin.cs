#if BIE
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using KappiMod.Config;
using KappiMod.Properties;
using UnityEngine;

namespace KappiMod.Loader.BepInEx;

[BepInPlugin(BuildInfo.GUID, BuildInfo.NAME, BuildInfo.VERSION)]
public class KappiModBepInExPlugin : BasePlugin, IKappiModLoader
{
    public static KappiModBepInExPlugin Instance = null!;

    public string KappiModFolderDestination => Paths.PluginPath;
    public string UnhollowedModulesFolder => Path.Combine(Paths.BepInExRootPath, "interop");

    private BepInExConfigHandler _configHandler = null!;
    public ConfigHandler ConfigHandler => _configHandler;

    private static readonly Harmony _harmony = new(BuildInfo.GUID);
    public Harmony HarmonyInstance => _harmony;

    public event Action? Update;
    public event Action<int, string>? SceneWasLoaded;
    public event Action<int, string>? SceneWasInitialized;

    public Action<object> OnLogMessage => Log.LogMessage;
    public Action<object> OnLogWarning => Log.LogWarning;
    public Action<object> OnLogError => Log.LogError;

    public void OnUpdate() => Update?.Invoke();

    public void OnSceneWasLoaded(int buildIndex, string sceneName) =>
        SceneWasLoaded?.Invoke(buildIndex, sceneName);

    public void OnSceneWasInitialized(int buildIndex, string sceneName) =>
        SceneWasInitialized?.Invoke(buildIndex, sceneName);

    public override void Load()
    {
        Instance = this;
        _configHandler = new BepInExConfigHandler();
        HarmonyInstance.PatchAll(typeof(BepInExPatches));
        IL2CPPChainloader.AddUnityComponent(typeof(BepInExEventProxy));
        KappiModCore.Init(this);
    }

    private class BepInExEventProxy : MonoBehaviour
    {
        private void Update() => Instance?.OnUpdate();
    }
}

#endif
