using ModSide.Config;

namespace ModSide.Loader;

public interface IModSideLoader
{
    string ModSideFolderDestination { get; }
    string UnhollowedModulesFolder { get; }

    ConfigHandler ConfigHandler { get; }

    event Action<object>? Update;
    event Action<int, string>? SceneWasLoaded;
    event Action<int, string>? SceneWasInitialized;

    Action<object> OnLogMessage { get; }
    Action<object> OnLogWarning { get; }
    Action<object> OnLogError { get; }
}
