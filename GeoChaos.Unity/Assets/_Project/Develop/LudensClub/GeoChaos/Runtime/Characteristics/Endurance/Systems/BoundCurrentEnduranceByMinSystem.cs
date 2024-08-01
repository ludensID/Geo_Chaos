using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Characteristics.Endurance
{
  public class BoundCurrentEnduranceByMinSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _endurances;

    public BoundCurrentEnduranceByMinSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _endurances = _game
        .Filter<CurrentEndurance>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _endurances)
      {
        entity.Change((ref CurrentEndurance endurance) =>
          endurance.Endurance = (int)MathUtils.Clamp(endurance.Endurance, 0));
      }
    }
  }
}