using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase
{
  public class DeleteLamaChaseCommandSystem : Delete<ChaseCommand, GameWorldWrapper>
  {
    protected DeleteLamaChaseCommandSystem(GameWorldWrapper wrapper) 
      : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}