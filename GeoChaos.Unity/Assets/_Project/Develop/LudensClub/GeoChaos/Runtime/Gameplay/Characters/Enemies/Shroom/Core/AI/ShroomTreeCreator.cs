using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom
{
  public class ShroomTreeCreator : IBehaviourTreeCreator
  {
    private readonly IBehaviourTreeBuilder _builder;
    public EntityType Id => EntityType.Shroom;

    public ShroomTreeCreator(IBehaviourTreeBuilder builder)
    {
      _builder = builder;
    }

    public BehaviourTree Create(EcsEntity entity)
    {
      return _builder.Create(entity)
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForShroomPatrolStrategy>()
        .AddAction<ShroomPatrolStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForShroomWaitStrategy>()
        .AddAction<ShroomWaitStrategy>()
        .ToParent()
        .End();
    }
  }
}