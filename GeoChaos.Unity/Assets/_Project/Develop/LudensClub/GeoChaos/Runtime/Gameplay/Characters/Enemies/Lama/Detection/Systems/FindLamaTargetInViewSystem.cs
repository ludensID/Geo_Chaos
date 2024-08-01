using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Detection
{
  public class FindLamaTargetInViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly EcsEntities _heroes;

    public FindLamaTargetInViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Collect();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity lama in _lamas)
      {
        lama.Has<AimInRadius>(lama.Has<Aimed>() || InRadius(hero, lama));
      }
    }

    private bool InRadius(EcsEntity hero, EcsEntity lama)
    {
      Transform originTransform = hero.Get<ViewRef>().View.transform;
      Transform selectionTransform = lama.Get<ViewRef>().View.transform;
      Vector3 originVector = originTransform.position - selectionTransform.position;
      return Vector3.Angle(selectionTransform.right, originVector) <= 90;
    }
  }
}