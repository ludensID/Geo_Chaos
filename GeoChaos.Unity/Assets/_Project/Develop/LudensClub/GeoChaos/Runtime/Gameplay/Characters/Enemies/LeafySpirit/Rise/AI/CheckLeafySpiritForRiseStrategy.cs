using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise
{
  public class CheckLeafySpiritForRiseStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }

    public bool Check()
    {
      return Entity.Has<Aimed>() && !Entity.Has<Risen>() || Entity.Has<Rising>();
    }
  }
}