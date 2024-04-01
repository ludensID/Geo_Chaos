using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class InputDebug : MonoBehaviour
  {
    private IInputDataProvider _inputProvider;
    [field: SerializeField] public InputData Data { get; set; }

    [Inject]
    public void Construct(IInputDataProvider inputProvider)
    {
      _inputProvider = inputProvider;
    }

    private void Update()
    {
      Data = _inputProvider.Data;
    }
  }
}