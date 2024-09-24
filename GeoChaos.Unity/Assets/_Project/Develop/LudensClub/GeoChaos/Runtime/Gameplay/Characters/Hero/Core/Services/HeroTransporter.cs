using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class HeroTransporter : IHeroTransporter
  {
    private readonly IHeroHolder _heroHolder;
    private readonly EcsEntity _hero;

    public HeroTransporter(IHeroHolder heroHolder)
    {
      _heroHolder = heroHolder;
      _hero = _heroHolder.Hero;
    }

    public void MoveTo(Vector3 position)
    {
      if (_hero.IsAlive())
        _hero.Get<ViewRef>().View.transform.position = position;
    }
  }
}