namespace Plugin.MvvmToolkit.Attributes;

/// <summary>
/// Specifies that a property is a navigation property.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class NavigationPropertyAttribute : Attribute
{
    /// <summary>
    /// Gets the default value for the property.
    /// </summary>
    public object? DefaultValue { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationPropertyAttribute"/> class.
    /// </summary>
    public NavigationPropertyAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationPropertyAttribute"/> class.
    /// </summary>
    /// <param name="defaultValue">The default value for the property.</param></param>
    public NavigationPropertyAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }
}