using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Runtime.Loader;

namespace BlazorDynamicPluginLoader.App.Services
{
    public class PluginService : IPluginService
    {
        public object? CreateControllerInstance(Assembly assembly, string className)
        {
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == className);
            return type != null ? Activator.CreateInstance(type) : null;
        }

        public Type? GetComponentType(Assembly assembly, string componentName)
        {
            return assembly.GetTypes().FirstOrDefault(t =>
           typeof(IComponent).IsAssignableFrom(t) && t.Name == componentName);
        }

        public IEnumerable<(Assembly assembly, string dllName)> LoadPlugins(string pluginFolder)
        {
            if (!Directory.Exists(pluginFolder))
                yield break;

            foreach (var dll in Directory.GetFiles(pluginFolder, "*.dll", SearchOption.TopDirectoryOnly))
            {
                Assembly asm;

                asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(dll));
                yield return (asm, Path.GetFileName(dll));

            }
        }
    }
}
