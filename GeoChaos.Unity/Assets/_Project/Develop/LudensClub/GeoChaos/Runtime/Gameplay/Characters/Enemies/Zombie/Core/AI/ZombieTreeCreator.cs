using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie
{
  public class ZombieTreeCreator : IBehaviourTreeCreator
  {
    private readonly IBehaviourTreeBuilder _builder;
    public EntityType Id => EntityType.Zombie;

    public ZombieTreeCreator(IBehaviourTreeBuilder builder)
    {
      _builder = builder;
    }
    
    public BehaviourTree Create(EcsEntity entity)
    {
      return _builder.Create(entity)
        .End();
    }
  }
}