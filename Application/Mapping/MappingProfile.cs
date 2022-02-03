using AutoMapper;
using System.Reflection;

namespace Application.Mapping;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => typeof(IMap).IsAssignableFrom(t) && !t.IsInterface).ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod(nameof(IMap.ConfigureMapping));

            method?.Invoke(instance, new object[] { this });
        }
    }
}
