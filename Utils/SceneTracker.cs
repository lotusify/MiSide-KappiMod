namespace KappiMod.Utils;

public static class SceneTracker
{
    private static string _lastSceneName = string.Empty;
    public static string LastSceneName
    {
        get => _lastSceneName;
        private set => _lastSceneName = value;
    }
    private static string _currentSceneName = string.Empty;

    public static void Init()
    {
        KappiModCore.Loader.SceneWasInitialized += OnSceneWasInitialized;
    }

    private static void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        _lastSceneName = _currentSceneName;
        _currentSceneName = sceneName;
    }
}
