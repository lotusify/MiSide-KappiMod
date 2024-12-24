using KappiMod.Config;
using UnityEngine;
using UnityEngine.UIElements;

namespace KappiMod.Mods;

public static class TimeScaleScroller
{
    public static bool Enabled
    {
        get => ConfigManager.TimeScaleScroller.Value;
        set
        {
            ConfigManager.TimeScaleScroller.Value = value;
            if (!value && !Mathf.Approximately(Time.timeScale, 1.0f))
            {
                ResetTimeScale();
            }

            KappiModCore.Log($"[{nameof(TimeScaleScroller)}] " + (value ? "Enabled" : "Disabled"));
        }
    }

    private static bool _shiftPressed = false;

    public static void Init()
    {
        KappiModCore.Loader.Update += OnUpdate;

        KappiModCore.Log($"[{nameof(TimeScaleScroller)}] Initialized");
    }

    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = Mathf.Max(0.0f, timeScale);

        KappiModCore.Log($"[{nameof(TimeScaleScroller)}] TimeScale: {Time.timeScale}");
    }

    public static void ResetTimeScale()
    {
        SetTimeScale(1.0f);
    }

    private static void OnUpdate()
    {
        if (!Enabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _shiftPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _shiftPressed = false;
        }

        if (_shiftPressed && Input.mouseScrollDelta.y > 0.0f)
        {
            SetTimeScale(Time.timeScale + 0.1f);
        }
        else if (_shiftPressed && Input.mouseScrollDelta.y < 0.0f)
        {
            SetTimeScale(Time.timeScale - 0.1f);
        }
        else if (_shiftPressed && Input.GetMouseButtonDown((int)MouseButton.MiddleMouse))
        {
            SetTimeScale(Mathf.Approximately(Time.timeScale, 1.0f) ? 0.0f : 1.0f);
        }
    }
}
