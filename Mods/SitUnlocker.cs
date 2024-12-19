using Il2Cpp;
using UnityEngine;

namespace ModSide.Mods;

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

            ModSideCore.Log($"[{nameof(SitUnlocker)}] " + (_enabled ? "Enabled" : "Disabled"));
        }
    }

    public static void Init()
    {
        ModSideCore.Loader.Update += OnUpdate;

        ModSideCore.Log($"[{nameof(SitUnlocker)}] Initialized");
    }

    private static void OnUpdate(object sender)
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
            ModSideCore.LogError($"[{nameof(SitUnlocker)}] {e.Message}");
        }
    }
}
