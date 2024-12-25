namespace KappiMod.Config;

public static class ConfigManager
{
    internal static readonly Dictionary<string, IConfigElement> ConfigElements = new();

    public static ConfigHandler Handler { get; private set; } = null!;

    public static ConfigElement<float> StartupDelayTime { get; private set; } = null!;
    public static ConfigElement<bool> DisableEventSystemOverride { get; private set; } = null!;
    public static ConfigElement<bool> ForceUnlockMouse { get; private set; } = null!;

    public static ConfigElement<bool> AlwaysRunEnabler { get; private set; } = null!;
    public static ConfigElement<bool> FlashlightIncreaser { get; private set; } = null!;
    public static ConfigElement<bool> SitUnlocker { get; private set; } = null!;
    public static ConfigElement<bool> TimeScaleScroller { get; private set; } = null!;
    public static ConfigElement<bool> DialogueSkipper { get; private set; } = null!;

    public static ConfigElement<int> FpsLimit { get; private set; } = null!;

    public static void Init(ConfigHandler handler)
    {
        Handler = handler;
        Handler.Init();

        CreateConfigElements();

        Handler.LoadConfig();
    }

    internal static void RegisterConfigElement<T>(ConfigElement<T> element)
    {
        Handler.RegisterConfigElement(element);
        ConfigElements.Add(element.Name, element);
    }

    private static void CreateConfigElements()
    {
        StartupDelayTime = new("StartupDelayTime", "Startup delay time", 0.0f);

        DisableEventSystemOverride = new(
            "DisableEventSystemOverride",
            "Disable event system override",
            false
        );

        ForceUnlockMouse = new("ForceUnlockMouse", "Force unlock mouse", true);

        AlwaysRunEnabler = new("AlwaysRunEnabler", "Always run enabler", true);
        FlashlightIncreaser = new("FlashlightIncreaser", "Flashlight increaser", true);
        SitUnlocker = new("SitUnlocker", "Sit unlocker", true);
        TimeScaleScroller = new("TimeScaleScroller", "Time scale scroller", true);
        DialogueSkipper = new("DialogueSkipper", "Dialogue skipper", false);

        FpsLimit = new("FpsLimit", "Fps limit", -1);
    }
}
