using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [AddComponentMenu(ACC.Names.MONO_INJECTOR)]
  public class MonoInjector : MonoBehaviour, IInjectable
  {
    public bool Injected { get; set; }

    [Inject]
    public void Construct()
    {
      Injected = true;
    }

    private void Awake()
    {
      this.EnsureInjection();
    }
  }
}