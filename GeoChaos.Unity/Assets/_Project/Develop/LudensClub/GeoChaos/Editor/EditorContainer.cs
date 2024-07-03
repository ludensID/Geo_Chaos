using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Debugging;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Editor
{
  public class EditorContainer : IEditorContainer
  {
    public InputActionAsset InputAsset { get; set; }
    public InputActionMap Map => InputAsset ? InputAsset.FindActionMap("Player") : null;
    public InputActionNameMap NameMap { get; set; }
  }
}