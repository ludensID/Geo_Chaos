using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalViewOffsetSetter : ITickable
  {
    private readonly EcsEntity _hero;
    private readonly VirtualCameraModel _model;
    private readonly IHeroHolder _heroHolder;
    private readonly CameraConfig _config;

    private float _target;


    public VerticalViewOffsetSetter(VirtualCameraModel model, IConfigProvider configProvider, IHeroHolder heroHolder)
    {
      _model = model;
      _heroHolder = heroHolder;
      _config = configProvider.Get<CameraConfig>();
      _hero = _heroHolder.Hero;
    }
    
    public void Tick()
    {
      if (_hero.IsAlive())
      {
        float viewDirection = _hero.Get<ViewDirection>().Direction.y;
        _model.VerticalViewOffset.Value = _config.VerticalViewOffset * viewDirection;
      }
    }
  }
}