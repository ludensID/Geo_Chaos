﻿#if UNITY_EDITOR
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public interface IEditorContainer
  {
    InputActionAsset InputAsset { get; set; }
    InputActionMap Map { get; }
    InputActionNameMap NameMap { get; set; }
  }
}
#endif