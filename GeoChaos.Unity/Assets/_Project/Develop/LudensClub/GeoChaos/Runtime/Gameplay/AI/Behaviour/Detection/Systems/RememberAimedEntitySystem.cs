using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection
{
  public class RememberAimedEntitySystem<TComponent> : IEcsRunSystem
    where TComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimedEntities;

    public RememberAimedEntitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _aimedEntities = _game
        .Filter<TComponent>()
        .Inc<Aimed>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _aimedEntities)
      {
        entity.Add<WasAimed>();
      }
    }
  }
}