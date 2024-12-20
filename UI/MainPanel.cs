#if UI
using ModSide.Mods;
using ModSide.Properties;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using UniverseLib.UI.Panels;

namespace ModSide.UI;

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

    private InputFieldRef? _timeScaleController;

    protected override void ConstructPanelContent()
    {
        _ = UIFactory.CreateLabel(
            ContentRoot,
            BuildInfo.NAME,
            "Toggle Mods",
            TextAnchor.MiddleLeft,
            fontSize: 18
        );
        CreateAlwaysRunEnablerToggle();
        CreateFlashlightIncreaserToggle();
        CreateSitUnlockerToggle();
        CreateTimeScaleScrollerToggle();

        // CreateTimeScaleController();

        // CreateIncreasedFlashlightToggle();

        CreateStatusBar();

        OnClosePanelClicked();
    }

    protected override void OnClosePanelClicked()
    {
        Owner.Enabled = !Owner.Enabled;
    }

    #region TOGGLE_MODS

    private void CreateAlwaysRunEnablerToggle()
    {
        _ = UIFactory.CreateToggle(
            ContentRoot,
            "AlwaysRunEnablerToggle",
            out Toggle toggle,
            out Text text
        );

        text.text = "Always Run Enabler";
        toggle.isOn = AlwaysRunEnabler.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                AlwaysRunEnabler.Enabled = value;
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

        text.text = "Flashlight Increaser";
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

        text.text = "Sit Unlocker";
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

        text.text = "Time Scale Scroller";
        toggle.isOn = TimeScaleScroller.Enabled;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                TimeScaleScroller.Enabled = value;
            }
        );
    }

    #endregion

    private void CreateTimeScaleController()
    {
        _timeScaleController = UIFactory.CreateInputField(
            ContentRoot,
            "TimeScaleController",
            "TimeScale"
        );

        _timeScaleController.OnValueChanged += (value) =>
        {
            if (float.TryParse(value, out float timeScale))
            {
                Time.timeScale = timeScale;
            }
        };

        _timeScaleController.Text = Time.timeScale.ToString();
        _timeScaleController.SetActive(true);
        _timeScaleController.Component.contentType = InputField.ContentType.DecimalNumber;
        _timeScaleController.Component.ActivateInputField();
    }

    private void CreateIncreasedFlashlightToggle()
    {
        GameObject checkbox = UIFactory.CreateToggle(
            ContentRoot,
            "IncreasedFlashlightToggle",
            out Toggle toggle,
            out Text text
        );
        text.text = "Increased Flashlight";
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                FlashlightIncreaser.Toggle();
            }
        );
    }

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

#endif
