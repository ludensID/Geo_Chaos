#if UNITY_EDITOR
namespace LudensClub.GeoChaos.Runtime
{
  public interface IProfilerService
  {
    string GetPrettyName(object context, string methodName);
    string GetPrettyName(object context);
  }
}
#endif