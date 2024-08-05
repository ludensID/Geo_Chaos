using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Debugging
{
  [AddComponentMenu(ACC.Names.INPUT_DEBUG)]
  public class InputDebug : MonoBehaviour
  {
    private EcsWorld _world;

    [field: SerializeField]
    [InlineProperty]
    [HideLabel]
    public InputData Data { get; private set; }

    [Inject]
    public void Construct(InputData data)
    {
      Data = data;
    }
  }
}