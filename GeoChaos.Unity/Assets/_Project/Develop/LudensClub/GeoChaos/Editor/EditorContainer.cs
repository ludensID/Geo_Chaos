using LudensClub.GeoChaos.Runtime;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Editor
{
  public class EditorContainer : IEditorContainer
  {
    public InputActionAsset InputAsset { get; set; }
    public InputActionMap Map => InputAsset ? InputAsset.FindActionMap("Gameplay") : null;
    public IProfilerService ProfilerService { get; set; }
    public ITypeCache TypeCache { get; set; }
  }
}