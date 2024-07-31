using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class RememberAimedLeafySpiritSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimedSpirits;

    public RememberAimedLeafySpiritSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _aimedSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<Aimed>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _aimedSpirits)
      {
        spirit.Add<WasAimed>();
      }
    }
  }
}