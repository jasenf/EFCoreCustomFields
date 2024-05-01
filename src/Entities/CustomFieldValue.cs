using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
namespace EFCoreCustomFields;

public class CustomFieldValue<T> where T : class, ICustomFieldEntity
{
    public int Id { get; set; }
    public int CustomFieldId { get; set; }
    public int CustomFieldEntityId { get; set; }
    
    [MaxLength(2048)]
    public virtual string? ValueString { get; set; }
    public virtual long? ValueNumber { get; set; }
    public virtual double? ValueDecimal { get; set; }
    public virtual bool? ValueBoolean { get; set; }
    public virtual DateTime? ValueDateTime { get; set; }
}