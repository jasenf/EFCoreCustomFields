namespace EFCoreCustomFields;

public class CustomField
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CustomFieldType CustomFieldType { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsRequired { get; set; }
    public string? DefaultValue { get; set; }

    // the entity that this custom field is for
    public string ForEntity { get; set; }
}