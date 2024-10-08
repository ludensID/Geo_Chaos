﻿using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpBack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Stun;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class FrogTreeCreator : IBehaviourTreeCreator
  {
    private readonly IBehaviourTreeBuilder _builder;
    public EntityType Id => EntityType.Frog;

    public FrogTreeCreator(IBehaviourTreeBuilder builder)
    {
      _builder = builder;
    }

    public BehaviourTree Create(EcsEntity entity)
    {
      return _builder.Create(entity)
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForStunnedFrogStrategy>()
        .AddAction<ContinueStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForFrogAttackWaitingStrategy>()
        .AddAction<FrogAttackWaitingStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForFrogJumpBackStrategy>()
        .AddAction<FrogJumpBackStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForFrogPatrollingStrategy>()
        .AddAction<FrogPatrollingStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForFrogChasingStrategy>()
        .AddAction<FrogChasingStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForFrogAttackStrategy>()
        .AddAction<FrogAttackStrategy>()
        .ToParent()
        .AddSequence()
        .ToChild()
        .AddCondition<CheckForFrogWaitingStrategy>()
        .AddAction<FrogWaitingStrategy>()
        .ToParent()
        .End();
    }
  }
}