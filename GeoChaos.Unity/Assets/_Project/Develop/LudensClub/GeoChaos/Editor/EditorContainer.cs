using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Editor
{
  public class EditorContainer : IEditorContainer
  {
    public InputActionAsset InputAsset { get; set; }
    public InputActionMap Map => InputAsset ? InputAsset.FindActionMap("Player") : null;
    public InputActionNameMap NameMap { get; set; }
    public IProfilerService ProfilerService { get; set; }
  }
}