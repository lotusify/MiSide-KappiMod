using KappiMod.Config;

namespace KappiMod.Loader;

public interface IKappiModLoader
{
    string KappiModFolderDestination { get; }
    string UnhollowedModulesFolder { get; }

    ConfigHandler ConfigHandler { get; }

    event Action? Update;
    event Action<int, string>? SceneWasLoaded;
    event Action<int, string>? SceneWasInitialized;

    Action<object> OnLogMessage { get; }
    Action<object> OnLogWarning { get; }
    Action<object> OnLogError { get; }
}
