using System.Reflection;
using EFCoreCustomFields.Tests.Unit.Data;

namespace EFCoreCustomFields.Tests.Unit;

public class AutoPropertiesTests
{
    [Theory]
    [ClassData(typeof(AutoPropertyTestDataGenerator<IEFCoreCustomFieldsMarker>))]
    public void TestAutoProperties<T>(T model)
    {
        try
        {
            TestGettersAndSetters<T>(model);

            Assert.True(true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    private static void TestGettersAndSetters<T>(T model)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.GetGetMethod(true) is not null)
            {
                property.GetValue(model);
            }

            if (property.GetSetMethod(true) is not null)
            {
                property.SetValue(model, default);
            }
        }
    }
}
