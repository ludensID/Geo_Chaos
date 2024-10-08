#if UNITY_EDITOR
namespace LudensClub.GeoChaos.Runtime
{
  public interface IEditorContainer
  {
    IProfilerService ProfilerService { get; set; }
  }
}
#endif