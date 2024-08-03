using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI
{
  public class TreeCreatorService : ITreeCreatorService
  {
    private readonly List<IBehaviourTreeCreator> _creators;

    public TreeCreatorService(List<IBehaviourTreeCreator> creators)
    {
      _creators = creators;
    }

    public BehaviourTree Create(EntityType id, EcsPackedEntity entity)
    {
      return _creators.Find(x => x.Id == id).Create(entity);
    }
  }
}