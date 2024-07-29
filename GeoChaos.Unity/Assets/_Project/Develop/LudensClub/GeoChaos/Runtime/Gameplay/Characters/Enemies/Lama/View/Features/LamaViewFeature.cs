using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.View
{
  public class LamaViewFeature : EcsFeature
  {
    public LamaViewFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<EnableLamaAttackColliderSystem>());
      Add(systems.Create<DisableLamaAttackColliderSystem>());
      
      Add(systems.Create<SetColorToLamaAttackRenderersSystem>());
    }
  }
}