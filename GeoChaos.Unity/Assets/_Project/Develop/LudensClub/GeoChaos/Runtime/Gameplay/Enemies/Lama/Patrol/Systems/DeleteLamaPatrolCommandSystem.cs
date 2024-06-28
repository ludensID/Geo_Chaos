using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol
{
  public class DeleteLamaPatrolCommandSystem : Delete<PatrolCommand, GameWorldWrapper>
  {
    public DeleteLamaPatrolCommandSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}