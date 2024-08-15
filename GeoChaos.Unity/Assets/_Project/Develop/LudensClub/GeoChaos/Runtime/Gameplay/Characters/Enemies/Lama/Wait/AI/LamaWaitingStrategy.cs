using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Wait
{
  public class LamaWaitingStrategy : IActionStrategy, IResetStrategy
  {
    public EcsEntity Entity { get; set; }

    public BehaviourStatus Execute()
    {
      return Node.CONTINUE;
    }

    public void Reset()
    {
      Entity.Del<WaitingTimer>();
    }
  }
}