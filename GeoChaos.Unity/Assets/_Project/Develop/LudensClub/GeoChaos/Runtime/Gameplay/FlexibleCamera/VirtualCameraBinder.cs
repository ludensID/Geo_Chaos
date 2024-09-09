using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.VIRTUAL_CAMERA_BINDER)]
  public class VirtualCameraBinder : MonoBehaviour, IHeroBindable
  {
    private CinemachineCamera _camera;

    public bool IsBound { get; set; }

    [Inject]
    public void Construct(IHeroBinder binder)
    {
      _camera = GetComponent<CinemachineCamera>();
      binder.Add(this);
    }

    public void BindHero(EcsEntity hero)
    {
      _camera.Follow = ((HeroView)hero.Get<ViewRef>().View).transform;
    }
  }
}