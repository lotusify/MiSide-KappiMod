using ModSide.Properties;
using UnityEngine;
using UniverseLib.UI;

namespace ModSide.UI;

public static class UIManager
{
    public static bool Enabled
    {
        get => UiBase is not null and { Enabled: true };
        set
        {
            if (UiBase is null || UiBase.Enabled == value)
            {
                return;
            }

            UniversalUI.SetUIActive(BuildInfo.GUID, value);
        }
    }

    internal static UIBase? UiBase { get; private set; }
    internal static MainPanel? Panel { get; private set; }

    public static void Init()
    {
        UiBase = UniversalUI.RegisterUI(BuildInfo.GUID, null);

        Panel = new(UiBase);

        ModSideCore.Loader.Update += OnUpdate;
    }

    private static void OnUpdate(object sender)
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Enabled = !Enabled;
        }
    }
}
