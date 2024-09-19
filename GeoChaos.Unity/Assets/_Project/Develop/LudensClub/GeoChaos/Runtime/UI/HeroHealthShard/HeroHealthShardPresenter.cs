using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.HealthShard;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealthShard
{
  public class HeroHealthShardPresenter : IHeroHealthShardPresenter, IInitializable, ITickable
  {
    private readonly EcsEntities _heroes;
    private HeroHealthShardView _view;

    public HeroHealthShardPresenter(GameWorldWrapper gameWorldWrapper, IExplicitInitializer initializer)
    {
      _heroes = gameWorldWrapper.World
        .Filter<HeroTag>()
        .Collect();

      initializer.Add(this);
    }

    public void Initialize()
    {
      Reset();
    }

    public void SetView(HeroHealthShardView view)
    {
      _view = view;
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