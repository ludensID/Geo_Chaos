#if UNITY_EDITOR
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Runtime
{
  public interface IEditorContainer
  {
    InputActionAsset InputAsset { get; set; }
    InputActionMap Map { get; }
    IProfilerService ProfilerService { get; set; }
    ITypeCache TypeCache { get; set; }
  }
}
#endif