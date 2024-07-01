using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class CreateSpikeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _createdSpikes;

    public CreateSpikeSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _createdSpikes = _game
        .Filter<EntityId>()
        .Inc<CreateCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spike in _createdSpikes
        .Where<EntityId>(x => x.Id == EntityType.Spike))
      {
        spike
          .Add<SpikeTag>()
          .Del<CreateCommand>();
      }
    }
  }
}