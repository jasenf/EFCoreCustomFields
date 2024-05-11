using System.ComponentModel.DataAnnotations;
using FluentAssertions;

namespace EFCoreCustomFields.Tests.Unit.Entities;

public class CustomFieldValueTests
{
    [Fact]
    public void SetCustomFieldValues_TestIdProperty()
    {
        // Arrange
        var customFieldType = CustomFieldType.SingleLineText;
        var customFieldId = 100;
        var customFieldValue = "Single line of text";

        var id = 1;

        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Act
        sut.Id = id;

        // Assert
        sut.Id.Should().Be(id);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueString_WhenTypeIsSingleLineText()
    {
        // Arrange
        var customFieldType = CustomFieldType.SingleLineText;
        var customFieldId = 100;
        var customFieldValue = "Single line of text";

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueString.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueString_WhenTypeIsMultiLineText()
    {
        // Arrange
        var customFieldType = CustomFieldType.MultiLineText;
        var customFieldId = 100;
        var customFieldValue = "Multiple lines \nof text";

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueString.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueString_WhenTypeIsList()
    {
        // Arrange
        var customFieldType = CustomFieldType.List;
        var customFieldId = 100;
        var customFieldValue = "* One\n*Two\n* Three";

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueString.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueNumber_WhenTypeIsNumber()
    {
        // Arrange
        var customFieldType = CustomFieldType.Number;
        var customFieldId = 100;
        var customFieldValue = 123_456_789;

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueNumber.Should().Be(customFieldValue);
    }

    [Theory]
    [InlineData(CustomFieldType.Decimal)]
    [InlineData(CustomFieldType.Currency)]
    [InlineData(CustomFieldType.Percent)]
    public void SetCustomFieldValues_ShouldSetValueDecimal_WhenTypeIsDecimalOrCurrencyOrPercent(CustomFieldType customFieldType)
    {
        // Arrange
        var customFieldId = 100;
        var customFieldValue = 100_000.00;

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueDecimal.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueBoolean_WhenTypeIsCheckBox()
    {
        // Arrange
        var customFieldType = CustomFieldType.CheckBox;
        var customFieldId = 100;
        var customFieldValue = true;

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueBoolean.Should().BeTrue();
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueDateTime_WhenTypeIsDateTime()
    {
        // Arrange
        var customFieldType = CustomFieldType.DateTime;
        var customFieldId = 100;
        var customFieldValue = DateTime.UtcNow;

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueDateTime.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueDateTime_WhenTypeIsDate()
    {
        // Arrange
        var customFieldType = CustomFieldType.Date;
        var customFieldId = 100;
        var customFieldValue = DateTime.UtcNow.Date;

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueDateTime.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueString_WhenTypeIsPhone()
    {
        // Arrange
        var customFieldType = CustomFieldType.Phone;
        var customFieldId = 100;
        var customFieldValue = "1-800-234-5678";

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueString.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueString_WhenTypeIsEmail()
    {
        // Arrange
        var customFieldType = CustomFieldType.Email;
        var customFieldId = 100;
        var customFieldValue = "valid@testemail.com";

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueString.Should().Be(customFieldValue);
    }

    [Fact]
    public void SetCustomFieldValues_ShouldSetValueString_WhenTypeIsUrl()
    {
        // Arrange
        var customFieldType = CustomFieldType.Url;
        var customFieldId = 100;
        var customFieldValue = "https://www.google.com/";

        // Act
        var sut = new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue);

        // Assert
        sut.CustomFieldId.Should().Be(customFieldId);
        sut.ValueString.Should().Be(customFieldValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("123")]
    public void SetCustomFieldValues_ShouldThrowValidationException_WhenPhoneNumberIsInvalid(string? customFieldValue)
    {
        // Arrange
        var customFieldType = CustomFieldType.Phone;
        var customFieldId = 100;

        // Act
        var act = () => new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue!);

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Invalid phone number");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("invalidemail!notanemailaddress")]
    public void SetCustomFieldValues_ShouldThrowValidationException_WhenEmailIsInvalid(string? customFieldValue)
    {
        // Arrange
        var customFieldType = CustomFieldType.Email;
        var customFieldId = 100;

        // Act
        var act = () => new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue!);

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Invalid email");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("not*a)valid(uri")]
    public void SetCustomFieldValues_ShouldThrowValidationException_WhenUrlIsInvalid(string? customFieldValue)
    {
        // Arrange
        var customFieldType = CustomFieldType.Url;
        var customFieldId = 100;

        // Act
        var act = () => new CustomFieldValue<TestEntity>(customFieldType, customFieldId, customFieldValue!);

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Invalid url");
    }
}

public class TestEntity : ICustomFieldEntity<TestEntity>
{
    public Guid Id { get; set; }

    public static string EntityName => throw new NotImplementedException();

    public ICollection<CustomFieldValue<TestEntity>> CustomFields { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
