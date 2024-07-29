using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  public class DashCooldownPresenter : IInitializable, ITickable
  {
    private readonly DashCooldownView _view;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroCooldowns;

    public DashCooldownPresenter(DashCooldownView view, GameWorldWrapper gameWorldWrapper)
    {
      _view = view;
      _game = gameWorldWrapper.World;

      _heroCooldowns = _game
        .Filter<HeroTag>()
        .Inc<DashCooldown>()
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
        float viewTime = MathUtils.Clamp(heroCooldown.Get<DashCooldown>().TimeLeft, 0);
        _view.SetText(viewTime.ToString("F"));
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("F"));
    }
  }
}