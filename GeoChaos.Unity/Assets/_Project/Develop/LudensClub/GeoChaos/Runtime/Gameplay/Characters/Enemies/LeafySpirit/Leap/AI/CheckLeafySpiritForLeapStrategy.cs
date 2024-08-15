using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class CheckLeafySpiritForLeapStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }

    public bool Check()
    {
      return !Entity.Has<Risen>() && !Entity.Has<Rising>() && !Entity.Has<OnLeapFinished>() 
        && (!Entity.Has<Aimed>() && !Entity.Has<WaitingTimer>() || Entity.Has<OnRelaxationFinished>()) 
        || Entity.Has<Leaping>();
    }
  }
}