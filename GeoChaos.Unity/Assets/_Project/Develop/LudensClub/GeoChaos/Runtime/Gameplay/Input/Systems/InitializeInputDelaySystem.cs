using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class InitializeInputDelaySystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly HeroConfig _config;

    public InitializeInputDelaySystem(InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
    {
      _world = inputWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();
    }
    
    public void Init(EcsSystems systems)
    {
      int delay = _world.NewEntity();
      ref InputDelay inputDelay = ref _world.Add<InputDelay>(delay);
      inputDelay.Delay = _config.MovementResponseDelay;
    }
  }
}