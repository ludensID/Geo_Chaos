#if UNITY_EDITOR
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
  }
}
#endif