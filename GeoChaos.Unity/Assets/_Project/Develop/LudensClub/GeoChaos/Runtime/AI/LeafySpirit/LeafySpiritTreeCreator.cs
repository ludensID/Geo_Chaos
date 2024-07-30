using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI.Rise;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class LeafySpiritTreeCreator : IBehaviourTreeCreator
  {
    private readonly IBehaviourTreeBuilder _builder;
    public EntityType Id => EntityType.LeafySpirit;

    public LeafySpiritTreeCreator(IBehaviourTreeBuilder builder)
    {
      _builder = builder;
    }

    public BehaviourTree Create(EcsPackedEntity entity)
    {
      return _builder.Create(entity)
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForLeapStrategy>()
        .AddAction<LeafySpiritLeapStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForRiseStrategy>()
        .AddAction<LeafySpiritRiseStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForMovingStrategy>()
        .AddAction<MoveLeafySpiritStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForAttackStrategy>()
        .AddAction<LeafySpiritAttackStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForCorrectionStrategy>()
        .AddAction<LeafySpiritCorrectionStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForWaitingStrategy>()
        .AddAction<LeafySpiritWaitingStrategy>()
        .ToParent()
        .End();
    }
  }
}