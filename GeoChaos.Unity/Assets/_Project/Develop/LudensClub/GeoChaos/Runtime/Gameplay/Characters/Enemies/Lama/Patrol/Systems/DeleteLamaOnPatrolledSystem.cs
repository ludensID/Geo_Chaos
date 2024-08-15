using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol
{
  public class DeleteLamaOnPatrolledSystem : DeleteSystem<OnPatrolFinished>
  {
    public DeleteLamaOnPatrolledSystem(GameWorldWrapper gameWorldWrapper)
    : base(gameWorldWrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}