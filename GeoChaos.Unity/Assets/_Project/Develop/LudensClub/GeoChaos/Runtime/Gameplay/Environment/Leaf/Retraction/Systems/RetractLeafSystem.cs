using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class RetractLeafSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractingLeaves;
    private readonly EcsEntity _cachedSpirit;

    public RetractLeafSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _retractingLeaves = _game
        .Filter<LeafTag>()
        .Inc<RetractCommand>()
        .Inc<Owner>()
        .Collect();

      _cachedSpirit = new EcsEntity(_game);
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _retractingLeaves)
      {
        leaf.Del<RetractCommand>();
        if (leaf.Get<Owner>().Entity.TryUnpackEntity(_game, _cachedSpirit))
        {
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, leaf.PackedEntity, Vector2.one));
          leaf.Add<Retracting>();
        }
      }
    }
  }
}