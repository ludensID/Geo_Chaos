using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera.Swap
{
  [AddComponentMenu(ACC.Names.CAMERA_TRIGGER_DETECTOR)]
  public class CameraTriggerDetector : HeroDetector
  {
    [SerializeField]
    private VirtualCameraView _camera;
    
    private IVirtualCameraManager _manager;

    [Inject]
    public void Construct(IVirtualCameraManager manager, IHeroBinder heroBinder)
    {
      _manager = manager;
      base.Construct(heroBinder);
    }
      
    public override void OnHeroEnter()
    {
        _manager.SetCamera(_camera);
    }

    public override void OnHeroExit()
    {
        _manager.UnsetCamera(_camera);
    }
  }
}