namespace Plugin.MvvmToolkit.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class NavigationPropertyAttribute : Attribute
{
    public object? DefaultValue { get; }

    public NavigationPropertyAttribute()
    {
    }

    public NavigationPropertyAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }
}