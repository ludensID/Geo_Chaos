using LudensClub.GeoChaos.Runtime;

namespace LudensClub.GeoChaos.Editor
{
  public class EditorContainer : IEditorContainer
  {
    public IProfilerService ProfilerService { get; set; }
    public ITypeCache TypeCache { get; set; }
  }
}