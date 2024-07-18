using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.HealthShard;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealthShard
{
  public class HeroHealthShardPresenter : IInitializable, ITickable
  {
    private readonly HeroHealthShardView _view;
    private readonly EcsEntities _heroes;

    public HeroHealthShardPresenter(HeroHealthShardView view, GameWorldWrapper gameWorldWrapper)
    {
      _view = view;
      _heroes = gameWorldWrapper.World
        .Filter<HeroTag>()
        .Collect();
    }

    public void Initialize()
    {
      Reset();
    }

    public void Tick()
    {
      foreach (EcsEntity hero in _heroes)
      {
        int count = hero.Get<HealthShardCounter>().Count;
        _view.SetText($"{count % 3:###0}/3");
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("###0"));
    }
  }
}