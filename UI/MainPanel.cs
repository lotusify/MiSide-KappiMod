using KappiMod.Mods;
using KappiMod.Patches;
using KappiMod.Properties;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using UniverseLib.UI.Panels;

namespace KappiMod.UI;

public class MainPanel : PanelBase
{
    public MainPanel(UIBase owner)
        : base(owner) { }

    public override string Name => $"{BuildInfo.NAME} v{BuildInfo.VERSION}";
    public override int MinWidth => 600;
    public override int MinHeight => 300;
    public override Vector2 DefaultAnchorMin => new(0.25f, 0.25f);
    public override Vector2 DefaultAnchorMax => new(0.5f, 0.5f);
    public override bool CanDragAndResize => true;

    protected Text StatusBar { get; private set; } = null!;

    private GameObject? _fpsLimitRow;

    protected override void ConstructPanelContent()
    {
        _ = UIFactory.CreateLabel(
            ContentRoot,
            "ToggleModsLabel",
            "Toggle Mods",
            TextAnchor.MiddleLeft,
            fontSize: 18
        );
        CreateSprintUnlockerToggle();
        CreateFlashlightIncreaserToggle();
        CreateSitUnlockerToggle();
        CreateTimeScaleScrollerToggle();
        CreateSkipDialoguesToggle();

        _ = UIFactory.CreateLabel(
            ContentRoot,
            "SettingsModsLabel",
            "Settings Mods",
            TextAnchor.MiddleLeft,
            fontSize: 18
        );
        CreateFpsLimitField();

        CreateStatusBar();

        OnClosePanelClicked();
    }

    protected override void OnClosePanelClicked()
    {
        Owner.Enabled = !Owner.Enabled;
    }

    #region TOGGLE_MODS

    private void CreateSprintUnlockerToggle()
    {
        _ = UIFactory.CreateToggle(
            ContentRoot,
            "SprintUnlockerToggle",
            out Toggle toggle,
            out Text text
        );
        text.text = "Sprint unlocker";
        toggle.isOn = SprintUnlocker.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                SprintUnlocker.Enabled = value;
            }
        );
    }

    private void CreateFlashlightIncreaserToggle()
    {
        _ = UIFactory.CreateToggle(
            ContentRoot,
            "FlashlightIncreaserToggle",
            out Toggle toggle,
            out Text text
        );
        text.text = "Flashlight increaser";
        toggle.isOn = FlashlightIncreaser.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                FlashlightIncreaser.Enabled = value;
            }
        );
    }

    private void CreateSitUnlockerToggle()
    {
        _ = UIFactory.CreateToggle(
            ContentRoot,
            "SitUnlockerToggle",
            out Toggle toggle,
            out Text text
        );
        text.text = "Sit unlocker";
        toggle.isOn = SitUnlocker.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                SitUnlocker.Enabled = value;
            }
        );
    }

    private void CreateTimeScaleScrollerToggle()
    {
        _ = UIFactory.CreateToggle(
            ContentRoot,
            "TimeScaleScrollerToggle",
            out Toggle toggle,
            out Text text
        );
        text.text = "Time scale scroller";
        toggle.isOn = TimeScaleScroller.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                TimeScaleScroller.Enabled = value;
            }
        );
    }

    private void CreateSkipDialoguesToggle()
    {
        _ = UIFactory.CreateToggle(
            ContentRoot,
            "SkipDialoguesToggle",
            out Toggle toggle,
            out Text text
        );
        text.text = "Skip dialogues";
        toggle.isOn = DialogueSkipper.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                DialogueSkipper.Enabled = value;
            }
        );
    }

    #endregion

    #region SETTINGS_MODS

    private void CreateFpsLimitField()
    {
        _fpsLimitRow = UIFactory.CreateHorizontalGroup(
            ContentRoot,
            "FpsLimitRow",
            false,
            true,
            true,
            true,
            2,
            new Vector4(2, 2, 2, 2)
        );
        UIFactory.SetLayoutElement(
            _fpsLimitRow,
            minHeight: 25,
            minWidth: 200,
            flexibleHeight: 0,
            flexibleWidth: 0
        );

        Text fpsLimitLabel = UIFactory.CreateLabel(
            _fpsLimitRow,
            "FpsLimitLabel",
            "Fps limit:",
            TextAnchor.MiddleLeft
        );
        UIFactory.SetLayoutElement(fpsLimitLabel.gameObject, minWidth: 110, flexibleWidth: 50);

        InputFieldRef fpsLimitField = UIFactory.CreateInputField(
            _fpsLimitRow,
            "FpsLimitField",
            "fps"
        );
        UIFactory.SetLayoutElement(
            fpsLimitField.UIRoot,
            minHeight: 25,
            minWidth: 50,
            flexibleHeight: 0,
            flexibleWidth: 0
        );

        fpsLimitField.Text = FpsLimit.CurrentFpsLimit.ToString();

        fpsLimitField.OnValueChanged += (value) =>
        {
            if (int.TryParse(value, out int fpsLimit))
            {
                FpsLimit.SetFpsLimit(fpsLimit);
            }
        };
    }

    #endregion

    private void CreateStatusBar()
    {
        StatusBar = UIFactory.CreateLabel(UIRoot, "StatusBar", "Ready!", TextAnchor.MiddleLeft);
        StatusBar.horizontalOverflow = HorizontalWrapMode.Wrap;
        UIFactory.SetLayoutElement(
            StatusBar.gameObject,
            minHeight: 25,
            flexibleWidth: 9999,
            flexibleHeight: 200
        );
    }
}
