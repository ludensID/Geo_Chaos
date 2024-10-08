#if UNITY_EDITOR
using System;
using System.Reflection;

namespace LudensClub.GeoChaos.Runtime
{
  public interface ITypeCache
  {
    FieldInfo GetCachedField(Type type, string name, bool isPrivate = false, bool isStatic = false);
    PropertyInfo GetCachedProperty(Type type, string name, bool isPrivate = false, bool isStatic = false);
    MethodInfo GetCachedMethod(Type type, string name, bool isPrivate = false, bool isStatic = false);
  }
}
#endif