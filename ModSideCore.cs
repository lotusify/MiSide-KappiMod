using ModSide.Config;
using ModSide.Loader;
using ModSide.Mods;
using ModSide.Properties;
using ModSide.UI;
using UnityEngine;
using UniverseLib;

namespace ModSide;

public static class ModSideCore
{
    public static IModSideLoader Loader { get; private set; } = null!;

    public static void Init(IModSideLoader loader)
    {
        if (Loader is not null)
        {
            throw new Exception($"{BuildInfo.NAME} is already initialized");
        }

        Loader = loader;

        Log($"{BuildInfo.NAME} v{BuildInfo.VERSION} initializing...");

        ConfigManager.Init(Loader.ConfigHandler);

        Universe.Init(
            0.0f,
            LateInitUI,
            Log,
            new()
            {
                Disable_EventSystem_Override = ConfigManager.DisableEventSystemOverride.Value,
                Force_Unlock_Mouse = ConfigManager.ForceUnlockMouse.Value,
                Unhollowed_Modules_Folder = Loader.UnhollowedModulesFolder,
            }
        );

        InitMods();
    }

    private static void LateInitUI()
    {
        Log("Loading UI...");

        UIManager.Init();

        Log($"{BuildInfo.NAME} v{BuildInfo.VERSION} initialized!");
    }

    private static void InitMods()
    {
        AlwaysRunEnabler.Init();
        ConsoleUnlocker.Init();
        FlashlightIncreaser.Init();
        SitUnlocker.Init();
        TimeScaleScroller.Init();
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
