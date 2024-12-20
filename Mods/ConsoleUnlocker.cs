#if ML
using Il2Cpp;
#endif
#if BIE
using BepInEx.IL2CPP;
#endif

namespace ModSide.Mods;

public static class ConsoleUnlocker
{
    public static void Init()
    {
        ModSideCore.Loader.SceneWasInitialized += OnSceneWasInitialized;

        ModSideCore.Log($"[{nameof(ConsoleUnlocker)}] Initialized");
    }

    private static void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        try
        {
            switch (sceneName)
            {
                case ObjectNames.MAIN_MENU_SCENE:
                    ConsoleMain.liteVersion = false;

                    ModSideCore.Log($"[{nameof(ConsoleUnlocker)}] Console successfully unlocked!");
                    break;

                default:
                    break;
            }
        }
        catch (Exception e)
        {
            ModSideCore.LogError($"[{nameof(ConsoleUnlocker)}] {e.Message}");
        }
    }
}
