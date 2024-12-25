using KappiMod.Config;
using UnityEngine;

namespace KappiMod.Mods;

public static class FpsLimit
{
    public static int CurrentFpsLimit
    {
        get => ConfigManager.FpsLimit.Value;
        private set => ConfigManager.FpsLimit.Value = value;
    }

    public static void Init()
    {
        KappiModCore.Loader.SceneWasInitialized += OnSceneWasInitialized;

        KappiModCore.Log($"[{nameof(FpsLimit)}] Initialized");
    }

    public static void SetFpsLimit(int fpsLimit)
    {
        if (CurrentFpsLimit == fpsLimit)
        {
            return;
        }

        try
        {
            int fps = fpsLimit < 0 ? -1 : Mathf.Max(10, fpsLimit);
            Application.targetFrameRate = fps;

            KappiModCore.Log($"[{nameof(FpsLimit)}] FPS limit set to {fps}");

            CurrentFpsLimit = fps;
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
