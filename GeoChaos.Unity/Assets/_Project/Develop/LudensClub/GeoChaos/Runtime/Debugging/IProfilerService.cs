#if UNITY_EDITOR
using System;

namespace LudensClub.GeoChaos.Runtime
{
  public interface IProfilerService
  {
    string GetPrettyName(object context, string methodName);
    string GetPrettyName(object context);
    string GetPrettyName(Type type);
  }
}
#endif