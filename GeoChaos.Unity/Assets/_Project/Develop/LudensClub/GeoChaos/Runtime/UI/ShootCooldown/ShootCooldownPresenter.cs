using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  public class ShootCooldownPresenter : IShootCooldownPresenter, IInitializable, ITickable
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroCooldowns;
    private ShootCooldownView _view;

    public ShootCooldownPresenter(GameWorldWrapper gameWorldWrapper, IExplicitInitializer initializer)
    {
      _game = gameWorldWrapper.World;

      _heroCooldowns = _game
        .Filter<HeroTag>()
        .Inc<ShootCooldown>()
        .Collect();

      initializer.Add(this);
    }
    
    public void Initialize()
    {
      Reset();
    }

    public void SetView(ShootCooldownView view)
    {
      _view = view;
    }

    public void Tick()
    {
      if (!_heroCooldowns.Any())
        Reset();

      foreach (EcsEntity heroCooldown in _heroCooldowns)
      {
        float viewTime = MathUtils.Clamp(heroCooldown.Get<ShootCooldown>().TimeLeft, 0);
        _view.SetText(viewTime.ToString("F"));
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("F"));
    }
  }
}