using UnityEngine;
#if ML
using Il2Cpp;
#endif
#if BIE
using BepInEx.IL2CPP;
#endif

namespace ModSide.Mods;

public static class AlwaysRunEnabler
{
    private static bool _enabled = true;
    public static bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;

            ModSideCore.Log($"[{nameof(AlwaysRunEnabler)}] " + (_enabled ? "Enabled" : "Disabled"));
        }
    }

    public static void Init()
    {
        ModSideCore.Loader.Update += OnUpdate;

        ModSideCore.Log($"[{nameof(AlwaysRunEnabler)}] Initialized");
    }

    private static void OnUpdate(object sender)
    {
        if (!_enabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetPlayerRunState(true);
        }
    }

    private static void SetPlayerRunState(bool value)
    {
        try
        {
            PlayerMove? playerMove = UnityEngine.Object.FindObjectOfType<PlayerMove>();
            if (playerMove is not null)
            {
                playerMove.canRun = value;
            }
        }
        catch (Exception e)
        {
            ModSideCore.LogError($"[{nameof(AlwaysRunEnabler)}] {e.Message}");
        }
    }
}
