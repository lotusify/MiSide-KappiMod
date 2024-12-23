using KappiMod.Config;
using UnityEngine;

namespace KappiMod.Mods;

public static class FpsLimit
{
    public static void Init()
    {
        KappiModCore.Loader.SceneWasInitialized += OnSceneWasInitialized;

        KappiModCore.Log($"[{nameof(FpsLimit)}] Initialized");
    }

    public static void SetFpsLimit(int fpsLimit)
    {
        try
        {
            Application.targetFrameRate = fpsLimit;

            KappiModCore.Log($"[{nameof(FpsLimit)}] FPS limit set to {fpsLimit}");
        }
        catch (Exception e)
        {
            KappiModCore.LogError($"[{nameof(FpsLimit)}] {e.Message}");
        }
    }

    private static void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (sceneName != ObjectNames.MAIN_MENU_SCENE)
        {
            return;
        }

        SetFpsLimit(ConfigManager.FpsLimit.Value);
    }
}
