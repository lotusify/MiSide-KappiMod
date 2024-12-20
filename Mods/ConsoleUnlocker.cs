#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Mods;

public static class ConsoleUnlocker
{
    public static void Init()
    {
        KappiModCore.Loader.SceneWasInitialized += OnSceneWasInitialized;

        KappiModCore.Log($"[{nameof(ConsoleUnlocker)}] Initialized");
    }

    private static void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        try
        {
            switch (sceneName)
            {
                case ObjectNames.MAIN_MENU_SCENE:
                    ConsoleMain.liteVersion = false;

                    KappiModCore.Log($"[{nameof(ConsoleUnlocker)}] Console successfully unlocked!");
                    break;

                default:
                    break;
            }
        }
        catch (Exception e)
        {
            KappiModCore.LogError($"[{nameof(ConsoleUnlocker)}] {e.Message}");
        }
    }
}
