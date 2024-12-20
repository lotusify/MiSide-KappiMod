using UnityEngine;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Mods;

public static class SitUnlocker
{
    private static bool _enabled = true;
    public static bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            if (!_enabled)
            {
                SetPlayerSitState(false);
            }

            KappiModCore.Log($"[{nameof(SitUnlocker)}] " + (_enabled ? "Enabled" : "Disabled"));
        }
    }

    public static void Init()
    {
        KappiModCore.Loader.Update += OnUpdate;

        KappiModCore.Log($"[{nameof(SitUnlocker)}] Initialized");
    }

    private static void OnUpdate()
    {
        if (!_enabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetPlayerSitState(true);
        }
    }

    private static void SetPlayerSitState(bool value)
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
}
