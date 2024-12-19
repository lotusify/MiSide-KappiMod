namespace ModSide.Config;

public interface IConfigElement
{
    string Name { get; }
    string Description { get; }

    object DefaultValue { get; }
    object BoxedValue { get; set; }

    Action? OnValueChangedNotify { get; set; }

    object GetLoaderConfigValue();

    void RevertToDefaultValue();
}
