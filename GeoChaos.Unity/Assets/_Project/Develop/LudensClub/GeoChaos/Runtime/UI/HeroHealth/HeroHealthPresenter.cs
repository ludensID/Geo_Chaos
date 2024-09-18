using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealth
{
  public class HeroHealthPresenter : IHeroHealthPresenter, IInitializable, ITickable
  {
    private readonly EcsEntities _heroes;
    private HeroHealthView _view;

    public HeroHealthPresenter(GameWorldWrapper gameWorldWrapper, InitializableManager initializer,
      TickableManager ticker)
    {
      _heroes = gameWorldWrapper.World
        .Filter<HeroTag>()
        .Collect();

      initializer.Add(this);
      ticker.Add(this);
    }

    public void Initialize()
    {
      Reset();
    }

    public void SetView(HeroHealthView view)
    {
      _view = view;
    }

    public void Tick()
    {
      foreach (EcsEntity hero in _heroes)
      {
        float currentHealth = hero.Get<CurrentHealth>().Health;
        float maxHealth = hero.Get<MaxCurrentHealth>().Health;
        _view.SetText($"{currentHealth:###0}/{maxHealth:###0}");
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("###0"));
    }
  }
}