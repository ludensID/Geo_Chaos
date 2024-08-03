using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Destroy;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit
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
        .AddCondition<CheckLeafySpiritForCorrectionStrategy>()
        .AddAction<LeafySpiritCorrectionStrategy>()
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
        .AddCondition<CheckLeafySpiritForBidingStrategy>()
        .AddAction<LeafySpiritBidingStrategy>()
        .ToParent()
        .AddAction<LeafySpiritWatchingStrategy>()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForRetractionStrategy>()
        .AddAction<LeafySpiritRetractionStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForDestroyLeavesStrategy>()
        .AddAction<LeafySpiritDestroyLeavesStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckLeafySpiritForRelaxationStrategy>()
        .AddAction<LeafySpiritRelaxationStrategy>()
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