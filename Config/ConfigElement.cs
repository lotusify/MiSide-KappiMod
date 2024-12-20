namespace KappiMod.Config;

public class ConfigElement<T> : IConfigElement
{
    public string Name { get; }
    public string Description { get; }

    public object DefaultValue { get; }

    private T _value;
    public T Value
    {
        get => _value!;
        set => SetValue(value);
    }
    object IConfigElement.BoxedValue
    {
        get => _value!;
        set => SetValue((T)value);
    }

    public Action<T>? OnValueChanged;
    public Action? OnValueChangedNotify { get; set; }

    public ConfigHandler Handler => ConfigManager.Handler;

    public ConfigElement(string name, string description, T defaultValue)
    {
        if (defaultValue is null)
        {
            throw new ArgumentNullException(nameof(defaultValue));
        }

        Name = name;
        Description = description;

        DefaultValue = defaultValue;
        _value = defaultValue;

        ConfigManager.RegisterConfigElement(this);
    }

    object IConfigElement.GetLoaderConfigValue() => GetLoaderConfigValue()!;

    public T GetLoaderConfigValue()
    {
        return Handler.GetConfigValue(this);
    }

    public void RevertToDefaultValue()
    {
        Value = (T)DefaultValue;
    }

    private void SetValue(T value)
    {
        if (value is null || value.Equals(_value))
        {
            return;
        }

        _value = value;

        Handler.SetConfigValue(this, value);

        OnValueChanged?.Invoke(value);
        OnValueChangedNotify?.Invoke();

        Handler.OnAnyConfigChanged();
    }
}
