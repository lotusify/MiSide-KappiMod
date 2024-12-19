#if ML
using MelonLoader;
using ModSide.Config;

namespace ModSide.Loader.MelonLoader;

public class MelonLoaderConfigHandler : ConfigHandler
{
    internal const string CFG_NAME = BuildInfo.Name;

    internal MelonPreferences_Category prefCategory = null!;

    public override void Init()
    {
        prefCategory = MelonPreferences.CreateCategory(CFG_NAME, $"{CFG_NAME} Configuration");
    }

    public override void LoadConfig()
    {
        foreach (var element in ConfigManager.ConfigElements)
        {
            var key = element.Key;
            if (prefCategory.GetEntry(key) is not null)
            {
                var config = element.Value;
                config.BoxedValue = config.GetLoaderConfigValue();
            }
        }
    }

    public override void SaveConfig() => MelonPreferences.Save();

    public override void OnAnyConfigChanged() => MelonPreferences.Save();

    public override void RegisterConfigElement<T>(ConfigElement<T> element)
    {
        var entry = prefCategory.CreateEntry(
            element.Name,
            element.Value,
            null,
            element.Description,
            false,
            false
        );
        _ = new EntryDelegateWrapper<T>(entry, element);
    }

    public override T GetConfigValue<T>(ConfigElement<T> element)
    {
        if (prefCategory.GetEntry(element.Name) is MelonPreferences_Entry<T> entry)
        {
            return entry.Value;
        }
        return default!;
    }

    public override void SetConfigValue<T>(ConfigElement<T> element, T value)
    {
        if (prefCategory.GetEntry(element.Name) is MelonPreferences_Entry<T> entry)
        {
            entry.Value = value;
        }
    }

    private class EntryDelegateWrapper<T>
    {
        public MelonPreferences_Entry<T> entry;
        public ConfigElement<T> config;

        public EntryDelegateWrapper(MelonPreferences_Entry<T> entry, ConfigElement<T> config)
        {
            this.entry = entry;
            this.config = config;

            var evt = entry.GetType().GetEvent("OnValueChangedUntyped");
            var thisMethod = GetType().GetMethod("OnChanged");
            if (evt is null or { EventHandlerType: null } || thisMethod is null)
            {
                return;
            }

            evt.AddEventHandler(
                entry,
                Delegate.CreateDelegate(evt.EventHandlerType, this, thisMethod)
            );
        }

        public void OnChanged()
        {
            if (
                entry is { Value: null }
                || config is { Value: null }
                || config.Value.Equals(entry.Value)
            )
            {
                return;
            }
            config.Value = entry.Value;
        }
    }
}

#endif
