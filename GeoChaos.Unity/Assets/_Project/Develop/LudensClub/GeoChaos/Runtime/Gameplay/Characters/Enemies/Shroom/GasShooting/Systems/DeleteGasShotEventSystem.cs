using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class DeleteGasShotEventSystem : DeleteSystem<OnGasShot>
  {
    protected DeleteGasShotEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<ShroomTag>())
    {
    }
  }
}