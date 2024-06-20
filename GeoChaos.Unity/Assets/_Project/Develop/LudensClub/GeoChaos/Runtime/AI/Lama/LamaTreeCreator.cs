using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class LamaTreeCreator : IBehaviourTreeCreator
  {
    private readonly IBehaviourTreeBuilder _builder;

    public EntityType Id => EntityType.Lama;

    public LamaTreeCreator(IBehaviourTreeBuilder builder)
    {
      _builder = builder;
    }

    public BehaviourTree Create(EcsPackedEntity entity)
    {
      return _builder.Create(entity)
        .AddSequence()
        .ToChild()
        .AddCondition<PlayerInAttackZoneCondition>()
        .End();
    }
  }
}