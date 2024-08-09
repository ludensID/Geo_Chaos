using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class SetLeafySpiritMaxVerticalDistanceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _spirits;
    private readonly LeafySpiritConfig _config;

    public SetLeafySpiritMaxVerticalDistanceSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _spirits = _game
        .Filter<LeafySpiritTag>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _spirits)
      {
        spirit.Replace((ref MaxVerticalDistance distance) => distance.Distance = _config.MaxVerticalDistance);
      }
    }
  }
}