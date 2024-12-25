using KappiMod.Config;
using UnityEngine;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Mods;

public static class SitUnlocker
{
    public static bool Enabled
    {
        get => ConfigManager.SitUnlocker.Value;
        set
        {
            if (value)
            {
                KappiModCore.Loader.Update += OnUpdate;
            }
            else
            {
                KappiModCore.Loader.Update -= OnUpdate;
                SetPlayerSitState(false);
            }

            KappiModCore.Log($"[{nameof(SitUnlocker)}] " + (value ? "Enabled" : "Disabled"));

            ConfigManager.SitUnlocker.Value = value;
        }
    }

    public static void Init()
    {
        if (Enabled)
        {
            KappiModCore.Loader.Update += OnUpdate;
        }

        KappiModCore.Log($"[{nameof(SitUnlocker)}] Initialized");
    }

    public static void SetPlayerSitState(bool value)
    {
        try
        {
            PlayerMove? playerMove = UnityEngine.Object.FindObjectOfType<PlayerMove>();
            if (playerMove is not null)
            {
                playerMove.canSit = value;
            }
        }
        catch (Exception e)
        {
            KappiModCore.LogError($"[{nameof(SitUnlocker)}] {e.Message}");
        }
    }

    private static void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetPlayerSitState(true);
        }
    }
}
