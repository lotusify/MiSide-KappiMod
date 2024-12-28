using KappiMod.Config;
using UnityEngine;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Mods;

public static class SprintUnlocker
{
    public static bool Enabled
    {
        get => ConfigManager.SprintUnlocker.Value;
        set
        {
            if (value)
            {
                KappiModCore.Loader.Update += OnUpdate;
            }
            else
            {
                KappiModCore.Loader.Update -= OnUpdate;
                if (IsPlayerMoveValid())
                {
                    SetPlayerRunState(false);
                }
            }

            KappiModCore.Log(value ? "Enabled" : "Disabled");

            ConfigManager.SprintUnlocker.Value = value;
        }
    }

    private static PlayerMove? _playerMove;

    public static void Init()
    {
        if (Enabled)
        {
            KappiModCore.Loader.Update += OnUpdate;
        }

        KappiModCore.Log("Initialized");
    }

    public static void SetPlayerRunState(bool value)
    {
        try
        {
            PlayerMove? playerMove = GetPlayerMove();
            if (playerMove != null)
            {
                playerMove.canRun = value;
            }
        }
        catch (Exception e)
        {
            KappiModCore.LogError(e.Message);

            _playerMove = null;
        }
    }

    private static void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetPlayerRunState(true);
        }
    }

    private static PlayerMove? GetPlayerMove()
    {
        if (!IsPlayerMoveValid())
        {
            _playerMove = GameObject.Find("Player")?.GetComponent<PlayerMove>();
        }
        return _playerMove;
    }

    private static bool IsPlayerMoveValid()
    {
        return _playerMove != null && _playerMove.gameObject != null;
    }
}
