using UnityEngine;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Mods;

public static class AlwaysRunEnabler
{
    private static bool _enabled = true;
    public static bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;

            KappiModCore.Log(
                $"[{nameof(AlwaysRunEnabler)}] " + (_enabled ? "Enabled" : "Disabled")
            );
        }
    }

    public static void Init()
    {
        KappiModCore.Loader.Update += OnUpdate;

        KappiModCore.Log($"[{nameof(AlwaysRunEnabler)}] Initialized");
    }

    private static void OnUpdate()
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
            KappiModCore.LogError($"[{nameof(AlwaysRunEnabler)}] {e.Message}");
        }
    }
}
