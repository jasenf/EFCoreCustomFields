namespace EFCoreCustomFields;

/// <summary>
/// For internal use only.  Please implement the generic version of ICustomFieldEntity, <![CDATA[ ICustomFieldEntity<T> ]]>.
/// </summary>
public interface ICustomFieldEntity { }

public interface ICustomFieldEntity<T> : ICustomFieldEntity
    where T : class, ICustomFieldEntity
{
    /// <summary>
    /// Provide an implementation with EntityName => nameof(T)
    /// </summary>
    static abstract string EntityName { get; }

    /// <summary>
    /// Provide a default implementation as a generic list of CustomFieldValue objects, <![CDATA[ ICollection<CustomFieldValue<T>> CustomFields { get; set; } = new List<CustomFieldValue<T>>(); ]]>
    /// </summary>
    ICollection<CustomFieldValue<T>> CustomFields { get; set; }
}
