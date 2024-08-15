using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol
{
  public class DeleteLamaPatrolCommandSystem : DeleteSystem<PatrolCommand>
  {
    public DeleteLamaPatrolCommandSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}