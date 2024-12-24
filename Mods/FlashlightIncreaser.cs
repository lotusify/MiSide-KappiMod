using KappiMod.Config;
using UnityEngine;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Mods;

public static class FlashlightIncreaser
{
    private const float NOT_INITIALIZED = -1.0f;
    private const float FLASHLIGHT_RANGE = 1000.0f;
    private const float FLASHLIGHT_SPOT_ANGLE = 70.0f;

    public static bool Enabled
    {
        get => ConfigManager.FlashlightIncreaser.Value;
        set
        {
            ConfigManager.FlashlightIncreaser.Value = value;
            if (!value && _isFlashlightEnabled)
            {
                Toggle();
            }

            KappiModCore.Log(
                $"[{nameof(FlashlightIncreaser)}] " + (value ? "Enabled" : "Disabled")
            );
        }
    }

    private static bool _isFlashlightEnabled = false;

    private static WorldPlayer? _player;

    private static float _savedFlashlightRange = NOT_INITIALIZED;
    private static float _savedFlashlightSpotAngle = NOT_INITIALIZED;

    public static void Init()
    {
        KappiModCore.Loader.Update += OnUpdate;

        KappiModCore.Log($"[{nameof(FlashlightIncreaser)}] Initialized");
    }

    public static bool Toggle()
    {
        _isFlashlightEnabled = !_isFlashlightEnabled;
        if (_isFlashlightEnabled)
        {
            ActivateFlashlightFeatures();
        }
        else
        {
            RevertFlashlightState();
        }

        KappiModCore.Log(
            $"[{nameof(FlashlightIncreaser)}] Flashlight "
                + (_isFlashlightEnabled ? "increased" : "restored")
        );
        return _isFlashlightEnabled;
    }

    private static void OnUpdate()
    {
        if (!Enabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Toggle();
        }
    }

    private static void ActivateFlashlightFeatures()
    {
        try
        {
            _player = UnityEngine.Object.FindObjectOfType<WorldPlayer>();
            if (_player is null)
            {
                KappiModCore.LogError(
                    $"[{nameof(FlashlightIncreaser)}] Object {nameof(WorldPlayer)} not found!"
                );

                _isFlashlightEnabled = false;
                return;
            }

            _savedFlashlightRange = _player.flashLightRange;
            _savedFlashlightSpotAngle = _player.flashLightSpotAngle;

            _player.flashLightRange = FLASHLIGHT_RANGE;
            _player.flashLightSpotAngle = FLASHLIGHT_SPOT_ANGLE;
        }
        catch (Exception e)
        {
            KappiModCore.LogError($"[{nameof(FlashlightIncreaser)}] {e.Message}");

            _savedFlashlightRange = NOT_INITIALIZED;
            _savedFlashlightSpotAngle = NOT_INITIALIZED;
            _isFlashlightEnabled = false;
            _player = null;
        }
    }

    private static void RevertFlashlightState()
    {
        try
        {
            if (_player is not null)
            {
                if (
                    _savedFlashlightRange <= NOT_INITIALIZED
                    || _savedFlashlightSpotAngle <= NOT_INITIALIZED
                )
                {
                    return;
                }

                _player.flashLightRange = _savedFlashlightRange;
                _player.flashLightSpotAngle = _savedFlashlightSpotAngle;
                _player = null;

                _savedFlashlightRange = NOT_INITIALIZED;
                _savedFlashlightSpotAngle = NOT_INITIALIZED;
            }
        }
        catch (Exception e)
        {
            KappiModCore.LogError($"[{nameof(FlashlightIncreaser)}] {e.Message}");

            _savedFlashlightRange = NOT_INITIALIZED;
            _savedFlashlightSpotAngle = NOT_INITIALIZED;
            _isFlashlightEnabled = false;
            _player = null;
        }
    }
}
