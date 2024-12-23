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

    public static void UnlockConsole()
    {
        try
        {
            if (ConsoleMain.liteVersion)
            {
                ConsoleMain.liteVersion = false;

                KappiModCore.Log($"[{nameof(ConsoleUnlocker)}] Console successfully unlocked!");
            }
            else
            {
                KappiModCore.Log($"[{nameof(ConsoleUnlocker)}] Console is already unlocked!");
            }
        }
        catch (Exception e)
        {
            KappiModCore.LogError($"[{nameof(ConsoleUnlocker)}] {e.Message}");
        }
    }

    private static void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (sceneName != ObjectNames.MAIN_MENU_SCENE)
        {
            return;
        }

        UnlockConsole();
    }
}
