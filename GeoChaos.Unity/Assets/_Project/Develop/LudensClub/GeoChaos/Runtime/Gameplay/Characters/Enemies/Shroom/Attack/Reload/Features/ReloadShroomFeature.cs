using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload
{
  public class ReloadShroomFeature : EcsFeature
  {
    public ReloadShroomFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReloadShroomSystem>());
      
      Add(systems.Create<DeleteShroomReloadingFinishedEventSystem>());
      Add(systems.Create<FinishReloadShroomSystem>());
    }
  }
}