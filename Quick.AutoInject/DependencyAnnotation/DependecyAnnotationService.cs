using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quick.AutoInject.Services;

namespace Quick.AutoInject.DependencyAnnotation;

public static class DependencyAnnotationService
{
    private static readonly Dictionary<Type, Action<IServiceCollection, Type, Type>> _lifetimeMap = new()
    {
        [typeof(ScopeService)] = static (s, iface, impl) => s.AddScoped(iface, impl),
        [typeof(SingletonService)] = static (s, iface, impl) => s.AddSingleton(iface, impl),
        [typeof(TransientService)] = static (s, iface, impl) => s.AddTransient(iface, impl),
    };

    public static IServiceCollection AddQuickAutoInject(this IServiceCollection services)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        foreach (var type in GetLoadableTypes(assembly))
            RegisterType(services, type);

        return services;
    }

    private static void RegisterType(IServiceCollection services, Type type)
    {
        foreach (var (attributeType, register) in _lifetimeMap)
        {
            if (!type.IsDefined(attributeType, true)) continue;

            var interfaces = type.GetInterfaces();

            if (interfaces.Length > 0)
                foreach (var iface in interfaces)
                    register(services, iface, type);
            else
                register(services, type, type);

            break;
        }
    }

    private static Type[] GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return [.. ex.Types.Where(t => t != null)!];
        }
        catch
        {
            return [];
        }
    }
}