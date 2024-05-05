using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EFCoreCustomFields;

public partial class CustomFieldValue<T> where T : class, ICustomFieldEntity
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
                ValueNumber = Convert.ToInt64(value, CultureInfo.CurrentCulture);
                break;

            case CustomFieldType.Decimal:
            case CustomFieldType.Currency:
            case CustomFieldType.Percent:
                ValueDecimal = Convert.ToDouble(value, CultureInfo.CurrentCulture);
                break;

            case CustomFieldType.CheckBox:
                ValueBoolean = (bool)value;
                break;

            case CustomFieldType.DateTime:
            case CustomFieldType.Date:
                ValueDateTime = (DateTime)value;
                break;

            case CustomFieldType.Phone:
                if (!IsValidPhoneNumber((string)value))
                {
                    throw new ValidationException("Invalid phone number");
                }

                ValueString = (string)value;

                break;

            case CustomFieldType.Email:
                if (!IsValidEmail((string)value))
                {
                    throw new ValidationException("Invalid email");
                }

                ValueString = (string)value;

                break;

            case CustomFieldType.Url:
                if (!IsValidUrl((string)value))
                {
                    throw new ValidationException("Invalid url");
                }

                ValueString = (string)value;

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
        return PhoneNumberRegex().IsMatch(number);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^(?=(?:\D*\d){10,15}\D*$)\+?[0-9]{1,3}[\s-]?(?:\(0?[0-9]{1,5}\)|[0-9]{1,5})[-\s]?[0-9][\d\s-]{5,7}\s?(?:x[\d-]{0,4})?$")]
    private static partial System.Text.RegularExpressions.Regex PhoneNumberRegex();
}
