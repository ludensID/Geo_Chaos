using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  public class DashCooldownPresenter : IInitializable, ITickable
  {
    private readonly DashCooldownView _view;
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroCooldowns;

    public DashCooldownPresenter(DashCooldownView view, GameWorldWrapper gameWorldWrapper)
    {
      _view = view;
      _game = gameWorldWrapper.World;

      _heroCooldowns = _game
        .Filter<HeroTag>()
        .Inc<DashCooldown>()
        .End();
    }

    public void Initialize()
    {
      Reset();
    }

    public void Tick()
    {
      if (_heroCooldowns.GetEntitiesCount() == 0)
        Reset();

      foreach (int heroCooldown in _heroCooldowns)
      {
        ref DashCooldown cooldown = ref _game.Get<DashCooldown>(heroCooldown);
        float viewTime = Mathf.Clamp(cooldown.Timer.TimeLeft, 0, cooldown.Timer.TimeLeft);
        _view.SetText(viewTime.ToString("F"));
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("F"));
    }
  }
}