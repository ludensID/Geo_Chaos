using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  public class DrawFrogTongueSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _tongues;
    private readonly EcsEntity _cachedTongue;
    private readonly EcsEntities _frogs;

    public DrawFrogTongueSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _frogs = _game
        .Filter<FrogTag>()
        .Collect();

      _cachedTongue = new EcsEntity();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _frogs)
      {
        LineRenderer line = frog.Get<TongueLineRef>().Line;
        if (frog.Has<ThrownTongue>() && frog.Get<ThrownTongue>().Tongue.TryUnpackEntity(_game, _cachedTongue)
          && _cachedTongue.Has<ViewRef>())
        {
          line.positionCount = 2;
          line.SetPositions(new[]
          {
            frog.Get<TonguePointRef>().Point.position,
            _cachedTongue.Get<ViewRef>().View.transform.position
          });
        }
        else
        {
          line.positionCount = 0;
        }
      }
    }
  }
}