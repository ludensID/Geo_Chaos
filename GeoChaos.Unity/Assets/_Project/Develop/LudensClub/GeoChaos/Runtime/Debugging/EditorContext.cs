#if UNITY_EDITOR
using System;

namespace LudensClub.GeoChaos.Runtime
{
  public static class EditorContext
  {
    public static IEditorContainer Container;

    public static string GetPrettyName(object target, string methodName, Type context)
    {
      return Container.ProfilerService.GetPrettyName(target, methodName, context);
    }

    public static string GetPrettyName(object target, Type context)
    {
      return Container.ProfilerService.GetPrettyName(target, context);
    }

    public static string GetPrettyName(Type type, Type context)
    {
      return Container.ProfilerService.GetPrettyName(type, context);
    }
  }
}
#endif