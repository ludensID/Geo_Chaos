using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealth
{
  public class HeroHealthPresenter : IInitializable, ITickable
  {
    private readonly HeroHealthView _view;
    private readonly EcsEntities _heroes;

    public HeroHealthPresenter(HeroHealthView view, GameWorldWrapper gameWorldWrapper)
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
        _view.SetText(hero.Get<Health>().Value.ToString("###0"));
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("###0"));
    }
  }
}