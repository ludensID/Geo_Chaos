using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction
{
  public class LeafySpiritRetractionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractingSpirits;
    private readonly EcsEntities _leaves;

    public LeafySpiritRetractionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _retractingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<RetractCommand>()
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
        spirit
          .Del<RetractCommand>()
          .Add<Retracting>();

        foreach (EcsEntity leaf in _leaves
          .Check<Owner>(x => x.Entity.EqualsTo(spirit.PackedEntity)))
        {
          leaf.Add<RetractCommand>();
        }
      }
    }
  }
}