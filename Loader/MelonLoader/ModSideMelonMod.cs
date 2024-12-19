using MelonLoader;
using MelonLoader.Utils;
using ModSide.Config;

[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.IL2CPP)]
[assembly: MelonInfo(
    typeof(ModSide.Loader.MelonLoader.ModSideMelonMod),
    ModSide.Properties.BuildInfo.NAME,
    ModSide.Properties.BuildInfo.VERSION,
    ModSide.Properties.BuildInfo.AUTHOR,
    ModSide.Properties.BuildInfo.DOWNLOADLINK
)]
[assembly: MelonOptionalDependencies("UniverseLib")]
[assembly: MelonGame("AIHASTO", "MiSideFull")]
[assembly: MelonColor(255, 196, 21, 169)]
[assembly: MelonAuthorColor(255, 33, 164, 176)]

namespace ModSide.Loader.MelonLoader;

public class ModSideMelonMod : MelonMod, IModSideLoader
{
    public string ModSideFolderDestination => MelonEnvironment.ModsDirectory;
    public string UnhollowedModulesFolder =>
        Path.Combine(
            Path.GetDirectoryName(ModSideFolderDestination) ?? "",
            Path.Combine("MelonLoader", "Il2CppAssemblies")
        );

    private MelonLoaderConfigHandler _configHandler = null!;
    public ConfigHandler ConfigHandler => _configHandler;

    public event Action<object>? Update;
    public event Action<int, string>? SceneWasLoaded;
    public event Action<int, string>? SceneWasInitialized;

    public Action<object> OnLogMessage => MelonLogger.Msg;
    public Action<object> OnLogWarning => MelonLogger.Warning;
    public Action<object> OnLogError => MelonLogger.Error;

    public override void OnUpdate() => Update?.Invoke(this);

    public override void OnSceneWasLoaded(int buildIndex, string sceneName) =>
        SceneWasLoaded?.Invoke(buildIndex, sceneName);

    public override void OnSceneWasInitialized(int buildIndex, string sceneName) =>
        SceneWasInitialized?.Invoke(buildIndex, sceneName);

    public override void OnLateInitializeMelon()
    {
        _configHandler = new MelonLoaderConfigHandler();
        ModSideCore.Init(this);
    }
}
