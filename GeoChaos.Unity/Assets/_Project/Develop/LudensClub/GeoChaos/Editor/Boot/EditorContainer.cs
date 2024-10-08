using LudensClub.GeoChaos.Runtime;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class EditorContainer : IEditorContainer
  {
    public IProfilerService ProfilerService { get; set; }
  }
}