using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Debugging
{
  [AddComponentMenu(ACC.Names.INPUT_DEBUG)]
  public class InputDebug : MonoBehaviour
  {
    private IInputDataProvider _inputProvider;
    private EcsWorld _world;

    [ShowInInspector]
    [InlineProperty]
    [HideLabel]
    public InputData Data { get; set; }

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