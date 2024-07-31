using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit
{
  public class CheckForLeafySpiritHasLeavesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractingSpirits;
    private readonly EcsEntities _leaves;

    public CheckForLeafySpiritHasLeavesSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _retractingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Collect();

      _leaves = _game
        .Filter<LeafTag>()
        .Inc<Owner>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _retractingSpirits)
      {
        spirit.Has<Discharged>(_leaves.Check<Owner>(x => x.Entity.EqualsTo(spirit.PackedEntity)).Any());
      }
    }
  }
}