#if UNITY_EDITOR
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Runtime
{
  public interface IEditorContainer
  {
    InputActionAsset InputAsset { get; set; }
    InputActionMap Map { get; }
    InputActionNameMap NameMap { get; set; }
    IProfilerService ProfilerService { get; set; }
    ITypeCache TypeCache { get; set; }
  }
}
#endif