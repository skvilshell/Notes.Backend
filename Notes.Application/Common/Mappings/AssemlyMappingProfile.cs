using AutoMapper;
using System.Reflection;

namespace Notes.Application.Common.Mappings
{
  public class AssemlyMappingProfile : Profile
  {
    public AssemlyMappingProfile(Assembly assembly) =>
        ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
      // собирает все возможные типы которые публичны, и имеют интерфейс с дженериком и IMapWith<>. Превращает в List
      var types = assembly.GetExportedTypes()
              .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                   i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
          .ToList();
      // пробегаемч по типам
      foreach (var type in types)
      {
        // создаёт инстанс 
        var instance = Activator.CreateInstance(type);
        var methodInfo = type.GetMethod("Mapping");
        methodInfo?.Invoke(instance, new object[] { this });
      }
    }
  }
}
