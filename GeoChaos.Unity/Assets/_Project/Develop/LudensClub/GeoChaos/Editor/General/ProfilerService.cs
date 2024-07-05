using System;
using System.Collections.Generic;
using System.Text;
using LudensClub.GeoChaos.Runtime;
using Unity.Profiling;

namespace LudensClub.GeoChaos.Editor.General
{
  public class ProfilerService : IProfilerService
  {
    private const string METHOD_NAME = nameof(ProfilerService) + "." + nameof(GetPrettyMethod) + "()";
    private const string DOUBLE_NAME = nameof(ProfilerService) + "." + nameof(GetPrettyName) + "(object, string)";
    private const string NAME = nameof(ProfilerService) + "." + nameof(GetPrettyName) + "(object)";
    private static readonly Dictionary<Type, string> _cachedNames = new Dictionary<Type, string>();
    private static readonly Dictionary<string, string> _cachedMethods = new Dictionary<string, string>();
    private static readonly Dictionary<(string, string), string> _cachedNameMethods = new Dictionary<(string, string), string>();
    private static readonly StringBuilder _nameBuilder = new StringBuilder();
    private static readonly StringBuilder _methodBuilder = new StringBuilder();

    public string GetPrettyName(object context, string methodName)
    {
      using (new ProfilerMarker(DOUBLE_NAME).Auto())
      {
        string type = GetPrettyName(context);
        if (!_cachedNameMethods.TryGetValue((type, methodName), out string name))
        {
          name = _nameBuilder
            .Clear()
            .Append(type)
            .Append(GetPrettyMethod(methodName))
            .ToString();
          _cachedNameMethods.Add((type, methodName), name);
        }

        return name;
      }
    }

    private string GetPrettyMethod(string methodName)
    {
      using (new ProfilerMarker(METHOD_NAME).Auto())
      {
        if (!_cachedMethods.TryGetValue(methodName, out string name))
        {
          name = _methodBuilder
            .Clear()
            .Append(".")
            .Append(methodName)
            .Append("()")
            .ToString();
          _cachedMethods.Add(methodName, name);
        }

        return name;
      }
    }

    public string GetPrettyName(object context)
    {
      using (new ProfilerMarker(NAME).Auto())
      {
        var type = context.GetType();
        if (!_cachedNames.TryGetValue(type, out string name))
        {
          name = type.Name;
          _cachedNames.Add(type, name);
        }

        return name;
      }
    }
  }
}