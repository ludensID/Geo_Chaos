using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class ClearSelectedRingsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _selectedRings;
    private readonly EcsEntities _rings;

    public ClearSelectedRingsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<Selected>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity selected in _selectedRings)
      {
        selected.Del<Selected>();
      }
    }
  }
}