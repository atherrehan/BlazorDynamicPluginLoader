using System.Reflection;

namespace BlazorDynamicPluginLoader.App.Services
{
    public interface IPluginService
    {
        IEnumerable<(Assembly assembly, string dllName)> LoadPlugins(string pluginPath);
        object? CreateControllerInstance(Assembly assembly, string className);
        Type? GetComponentType(Assembly assembly, string componentName);
    }
}
