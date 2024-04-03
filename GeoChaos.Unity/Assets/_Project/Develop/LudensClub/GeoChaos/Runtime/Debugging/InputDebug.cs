#if UNITY_EDITOR
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class InputDebug : MonoBehaviour
  {
    private IInputDataProvider _inputProvider;
    [ShowInInspector, InlineProperty] public InputData Data { get; set; }

    [Inject]
    public void Construct(IInputDataProvider inputProvider)
    {
      _inputProvider = inputProvider;
    }

    private void Update()
    {
      Data = _inputProvider.Data;
      EditorUtility.SetDirty(this);
    }
  }
}
#endif