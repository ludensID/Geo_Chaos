using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama
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
        .AddCondition<CheckLamaForAttackStrategy>()
        .AddAction<LamaAttackStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLamaForChaseStrategy>()
        .AddAction<ChaseHeroByLamaStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLamaForWatchStrategy>()
        .AddAction<LamaWatchingStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLamaForPatrolStrategy>()
        .AddAction<AddLamaPatrolCommandStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLamaForLookingStrategy>()
        .AddAction<LamaLookingStrategy>()
        .ToParent()
        .End();
    }
  }
}