#if UNITY_EDITOR
using System;

namespace LudensClub.GeoChaos.Runtime
{
  public static class EditorContext
  {
    public static IEditorContainer Container;

    public static string GetPrettyName(object context, string methodName)
    {
      return Container.ProfilerService.GetPrettyName(context, methodName);
    }

    public static string GetPrettyName(object context)
    {
      return Container.ProfilerService.GetPrettyName(context);
    }

    public static string GetPrettyName(Type type)
    {
      return Container.ProfilerService.GetPrettyName(type);
    }
  }
}
#endif