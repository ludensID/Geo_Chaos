using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalViewOffsetSetter : IHeroBindable, ITickable
  {
    private readonly EcsEntity _hero = new EcsEntity();
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;

    private float _target;

    public bool IsBound { get; set; }

    public VerticalViewOffsetSetter(VirtualCameraModel model, IConfigProvider configProvider)
    {
      _model = model;
      _config = configProvider.Get<CameraConfig>();
    }
    
    public void BindHero(EcsEntity hero)
    {
      _hero.Copy(hero);
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