using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.ImmunityDuration
{
  public class ImmunityDurationPresenter : IInitializable, ITickable
  {
    private readonly ImmunityDurationView _view;
    private readonly EcsWorld _game;
    private readonly EcsEntities _immuneHeroes;

    public ImmunityDurationPresenter(ImmunityDurationView view, GameWorldWrapper gameWorldWrapper)
    {
      _view = view;
      _game = gameWorldWrapper.World;

      _immuneHeroes = _game
        .Filter<HeroTag>()
        .Inc<Immune>()
        .Collect();
    }

    public void Initialize()
    {
      Reset();
    }

    public void Tick()
    {
      if (!_immuneHeroes.Any())
        Reset();

      foreach (EcsEntity hero in _immuneHeroes)
      {
        if (hero.Has<ImmunityTimer>() && hero.Get<Immune>().Owner == MovementType.Bump)
        {
          float viewTime = MathUtils.Clamp(hero.Get<ImmunityTimer>().TimeLeft, 0);
          _view.SetText(viewTime.ToString("F"));
        }
        else
        {
          _view.SetText("\u221e");
        }
      }
    }

    private void Reset()
    {
      _view.SetText(0.ToString("F"));
    }
  }
}