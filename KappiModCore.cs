using KappiMod.Config;
using KappiMod.Loader;
using KappiMod.Mods;
using KappiMod.Patches;
using KappiMod.Properties;
using KappiMod.UI;
using UnityEngine;
using UniverseLib;

namespace KappiMod;

public static class KappiModCore
{
    public const string MOD_DIRECTORY_NAME = "KappiMod";

    public static IKappiModLoader Loader { get; private set; } = null!;

    public static void Init(IKappiModLoader loader)
    {
        if (Loader is not null)
        {
            throw new Exception($"{BuildInfo.NAME} is already initialized");
        }

        Loader = loader;

        Log($"{BuildInfo.NAME} v{BuildInfo.VERSION} initializing...");

        ConfigManager.Init(Loader.ConfigHandler);

        Universe.Init(
            ConfigManager.StartupDelayTime.Value,
            LateInitUI,
            Log,
            new()
            {
                Disable_EventSystem_Override = ConfigManager.DisableEventSystemOverride.Value,
                Force_Unlock_Mouse = ConfigManager.ForceUnlockMouse.Value,
                Unhollowed_Modules_Folder = Loader.UnhollowedModulesDirectory,
            }
        );

        InitMods();
        InitPatches();
    }

    private static void LateInitUI()
    {
        Log("Loading UI...");

        UIManager.Init();

        Log($"{BuildInfo.NAME} v{BuildInfo.VERSION} initialized!");
    }

    private static void InitMods()
    {
        SprintUnlocker.Init();
        ConsoleUnlocker.Init();
        FlashlightIncreaser.Init();
        FpsLimit.Init();
        SitUnlocker.Init();
        TimeScaleScroller.Init();
    }

    private static void InitPatches()
    {
        DialogueSkipper.Init();
    }

    #region LOGGING

    public static void Log(object message) => Log(message, LogType.Log);

    public static void LogWarning(object message) => Log(message, LogType.Warning);

    public static void LogError(object message) => Log(message, LogType.Error);

    private static void Log(object message, LogType logType)
    {
        string log = message?.ToString() ?? "";

        switch (logType)
        {
            case LogType.Log:
            case LogType.Assert:
                Loader.OnLogMessage(log);
                break;

            case LogType.Warning:
                Loader.OnLogWarning(log);
                break;

            case LogType.Error:
            case LogType.Exception:
                Loader.OnLogError(log);
                break;

            default:
                break;
        }
    }

    #endregion
}
