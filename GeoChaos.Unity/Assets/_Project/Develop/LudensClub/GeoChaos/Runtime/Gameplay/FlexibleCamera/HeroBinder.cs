using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class HeroBinder : IHeroBinder, ITickable
  {
    private readonly List<IHeroBindable> _binds = new List<IHeroBindable>();
    private readonly EcsEntities _heroes;
    private EcsWorld _game;

    public HeroBinder(GameWorldWrapper gameWorldWrapper, List<IHeroBindable> binds)
    {
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
      
      _binds.AddRange(binds);
    }

    public void Add(IHeroBindable bind)
    {
      _binds.Add(bind);
    }

    public void Tick()
    {
      foreach (EcsEntity hero in _heroes)
      {
        while(_binds.Count > 0)
        {
          IHeroBindable bind = _binds[0];
          bind.BindHero(hero);
          bind.IsBound = true;
          _binds.Remove(bind);
        }
      }
    }
  }
}