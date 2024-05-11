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

    public CustomFieldValue(CustomFieldType customFieldType, long customFieldId, object customFieldValue)
    {
        SetCustomFieldValues(customFieldType, customFieldId, customFieldValue);
    }

    private void SetCustomFieldValues(CustomFieldType customFieldType, long customFieldId, object customFieldValue)
    {
        CustomFieldId = customFieldId;

        switch (customFieldType)
        {
            case CustomFieldType.SingleLineText:
            case CustomFieldType.MultiLineText:
            case CustomFieldType.List:
                ValueString = (string)customFieldValue;
                break;

            case CustomFieldType.Number:
                ValueNumber = Convert.ToInt64(customFieldValue, CultureInfo.CurrentCulture);
                break;

            case CustomFieldType.Decimal:
            case CustomFieldType.Currency:
            case CustomFieldType.Percent:
                ValueDecimal = Convert.ToDouble(customFieldValue, CultureInfo.CurrentCulture);
                break;

            case CustomFieldType.CheckBox:
                ValueBoolean = (bool)customFieldValue;
                break;

            case CustomFieldType.DateTime:
            case CustomFieldType.Date:
                ValueDateTime = (DateTime)customFieldValue;
                break;

            case CustomFieldType.Phone:
                if (!IsValidPhoneNumber((string)customFieldValue))
                {
                    throw new ValidationException("Invalid phone number");
                }

                ValueString = (string)customFieldValue;

                break;

            case CustomFieldType.Email:
                if (!IsValidEmail((string)customFieldValue))
                {
                    throw new ValidationException("Invalid email");
                }

                ValueString = (string)customFieldValue;

                break;

            case CustomFieldType.Url:
                if (!IsValidUrl((string)customFieldValue))
                {
                    throw new ValidationException("Invalid url");
                }

                ValueString = (string)customFieldValue;

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
        if (string.IsNullOrWhiteSpace(number))
        {
            return false;
        }

        return PhoneNumberRegex().IsMatch(number);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^(?=(?:\D*\d){10,15}\D*$)\+?[0-9]{1,3}[\s-]?(?:\(0?[0-9]{1,5}\)|[0-9]{1,5})[-\s]?[0-9][\d\s-]{5,7}\s?(?:x[\d-]{0,4})?$")]
    private static partial System.Text.RegularExpressions.Regex PhoneNumberRegex();
}
