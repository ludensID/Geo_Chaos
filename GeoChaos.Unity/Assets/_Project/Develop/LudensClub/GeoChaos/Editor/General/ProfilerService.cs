using System;
using System.Collections.Generic;
using Cysharp.Text;
using LudensClub.GeoChaos.Runtime;
using Unity.Profiling;

namespace LudensClub.GeoChaos.Editor.General
{
  public class ProfilerService : IProfilerService
  {
    private const string METHOD_NAME = nameof(ProfilerService) + "." + nameof(GetPrettyMethod) + "()";
    private const string DOUBLE_NAME = nameof(ProfilerService) + "." + nameof(GetPrettyName) + "(object, string, Type)";
    private const string NAME = nameof(ProfilerService) + "." + nameof(GetPrettyName) + "(object, Type)";
    private const string TYPE_NAME = nameof(ProfilerService) + "." + nameof(GetPrettyName) + "(Type, Type)";
    private const string TYPE_NAME_WITHOUT_CONTEXT = nameof(ProfilerService) + "." + nameof(GetPrettyName) + "(Type)";

    private static readonly Dictionary<Type, string> _cachedNames = new Dictionary<Type, string>();
      
    private static readonly Dictionary<Type, Dictionary<Type, string>> _cachedContexts 
      = new Dictionary<Type, Dictionary<Type, string>>();
    
    private static readonly Dictionary<string, string> _cachedMethods = new Dictionary<string, string>();

    private static readonly Dictionary<(string, string), string> _cachedNameMethods =
      new Dictionary<(string, string), string>();

    public string GetPrettyName(object target, string methodName, Type context)
    {
      using (new ProfilerMarker(DOUBLE_NAME).Auto())
      {
        string type = GetPrettyName(target, context);
        if (!_cachedNameMethods.TryGetValue((type, methodName), out string name))
        {
          name = ZString.Concat(type, GetPrettyMethod(methodName));
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
          name = ZString.Concat(".", methodName, "()");
          _cachedMethods.Add(methodName, name);
        }

        return name;
      }
    }

    public string GetPrettyName(object target, Type context)
    {
      using (new ProfilerMarker(NAME).Auto())
      {
        return GetPrettyName(target.GetType(), context);
      }
    }

    public string GetPrettyName(Type type, Type context)
    {
      using (new ProfilerMarker(TYPE_NAME).Auto())
      {
        if (context == null)
          return GetPrettyName(type);
          
        if (_cachedContexts.TryGetValue(context, out Dictionary<Type, string> names))
        {
          if (names.TryGetValue(type, out string name))
            return name;
          
          name = TypeUtils.GetCleanGenericTypeName(type);
          names.Add(type, name);
          return name;
        }
        else
        {
          names = new Dictionary<Type, string>();
          string name = TypeUtils.GetCleanGenericTypeName(type);
          names.Add(type, name);
          _cachedContexts.Add(context, names);
          return name;
        }
      }
    }

    public string GetPrettyName(Type type)
    {
      using (new ProfilerMarker(TYPE_NAME_WITHOUT_CONTEXT).Auto())
      {
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