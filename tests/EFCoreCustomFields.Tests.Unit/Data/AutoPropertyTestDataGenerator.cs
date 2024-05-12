using System.Collections;

namespace EFCoreCustomFields.Tests.Unit.Data;

public class AutoPropertyTestDataGenerator<T> : IEnumerable<object[]> where T : class?
{
    private readonly IList<object[]> _autoPropertyModels = [];

    public AutoPropertyTestDataGenerator()
    {
        FindModelsWithAutoProperties();
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        return _autoPropertyModels.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void FindModelsWithAutoProperties()
    {
        var assembly = typeof(T).Assembly;

        var types = assembly.GetTypes()
            .Where(i => i.IsClass && !i.IsAbstract && i.Namespace is not null)
            .Where(i => i.GetConstructor(Type.EmptyTypes) is not null)
            .ToList();

        foreach (var model in types)
        {
            if (!CanCreateInstanceUsingDefaultConstructor(model))
            {
                continue;
            }

            if (model.IsGenericType)
            {
                AddGenericType(model);
            }
            else
            {
                AddType(model);
            }
        }
    }

    private void AddGenericType(Type model)
    {
        var constructed = model.MakeGenericType(typeof(GenericClass));

        AddType(constructed);
    }

    private void AddType(Type model)
    {
        var obj = Activator.CreateInstance(model);
        if (obj is not null)
        {
            _autoPropertyModels.Add([obj]);
        }
    }

    private static bool CanCreateInstanceUsingDefaultConstructor(Type t) =>
        t.IsValueType || !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null;

    public class GenericClass : ICustomFieldEntity<GenericClass>
    {
        public static string EntityName => nameof(GenericClass);

        public int MyProperty { get; set; }

        public ICollection<CustomFieldValue<GenericClass>> CustomFields { get; set; } = new List<CustomFieldValue<GenericClass>>();
    }
}
