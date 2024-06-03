using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  public class ShootCooldownPresenter : IInitializable, ITickable
  {
    private readonly ShootCooldownView _view;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroCooldowns;

    public ShootCooldownPresenter(ShootCooldownView view, GameWorldWrapper gameWorldWrapper)
    {
      _view = view;
      _game = gameWorldWrapper.World;

      _heroCooldowns = _game
        .Filter<HeroTag>()
        .Inc<ShootCooldown>()
        .Collect();
    }
    
    public void Initialize()
    {
      Reset();
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