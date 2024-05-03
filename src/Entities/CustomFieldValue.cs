using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
namespace EFCoreCustomFields;

public class CustomFieldValue<T> where T : class, ICustomFieldEntity
{
    public long Id { get; set; }
    public long CustomFieldId { get; set; }
    // public long CustomFieldEntityId { get; set; }
    
    [MaxLength(2048)]
    public virtual string? ValueString { get; set; }
    public virtual long? ValueNumber { get; set; }
    public virtual double? ValueDecimal { get; set; }
    public virtual bool? ValueBoolean { get; set; }
    public virtual DateTime? ValueDateTime { get; set; }

    public CustomFieldValue()
    {

    }

    public CustomFieldValue(CustomFieldType cfType, long cfId, object value)
    {
       SetCustomFieldValues(cfType, cfId, value);
    }

    private void SetCustomFieldValues(CustomFieldType cfType, long cfId, object value)
    {
        CustomFieldId = cfId;
        switch (cfType)
        {
            case CustomFieldType.SingleLineText:
            case CustomFieldType.MultiLineText:
            case CustomFieldType.List:
                ValueString = (string)value;
                break;
            case CustomFieldType.Number:
                ValueNumber = Convert.ToInt64(value);
                break;
            case CustomFieldType.Decimal:
            case CustomFieldType.Currency:
            case CustomFieldType.Percent:
                ValueDecimal = Convert.ToDouble(value);
                break;
            case CustomFieldType.CheckBox:
                ValueBoolean = (bool)value;
                break;
            case CustomFieldType.DateTime:
            case CustomFieldType.Date:
                ValueDateTime = (DateTime)value;
                break;
            case CustomFieldType.Phone:
                if(IsValidPhoneNumber((string)value))
                    ValueString = (string)value;
                else 
                    throw new ValidationException("Invalid phone number");
                break;
            case CustomFieldType.Email:
                if(IsValidEmail((string)value))
                    ValueString = (string)value;
                else 
                    throw new ValidationException("Invalid email");
                break;
            case CustomFieldType.Url:
                if(IsValidUrl((string)value))
                    ValueString = (string)value;
                else 
                    throw new ValidationException("Invalid url");
                break;

        }
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    private static bool IsValidPhoneNumber(string number)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(number, @"^(?=(?:\D*\d){10,15}\D*$)\+?[0-9]{1,3}[\s-]?(?:\(0?[0-9]{1,5}\)|[0-9]{1,5})[-\s]?[0-9][\d\s-]{5,7}\s?(?:x[\d-]{0,4})?$");
    }

}