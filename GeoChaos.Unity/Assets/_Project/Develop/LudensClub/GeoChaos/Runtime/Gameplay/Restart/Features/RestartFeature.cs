using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Restart
{
  public class RestartFeature : EcsFeature
  {
    public RestartFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FinishRestartSystem>());
      
      Add(systems.Create<DeleteSystem<AfterRestartMessage, MessageWorldWrapper>>());
      Add(systems.Create<CreateEntitiesOnRestartSystem>());
      Add(systems.Create<CleanSceneBeforeRestartSystem>());
    } 
  }
}