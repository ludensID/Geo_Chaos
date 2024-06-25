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
        .AddCondition<CheckLamaForChaseStrategy>()
        .AddAction<ChaseHeroByLamaStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLamaForSneakStrategy>()
        .AddAction<LamaSneakingStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
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