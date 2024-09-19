using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class HeroTransporter : IHeroBindable, IHeroTransporter
  {
    private BaseEntityView _view;

    public bool IsBound { get; set; }

    public void BindHero(EcsEntity hero)
    {
      _view = hero.Get<ViewRef>().View;
    }

    public void MoveTo(Vector3 position)
    {
      _view.transform.position = position;
    }
  }
}