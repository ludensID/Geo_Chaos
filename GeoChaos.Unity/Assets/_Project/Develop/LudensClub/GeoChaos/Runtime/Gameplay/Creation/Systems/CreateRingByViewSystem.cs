using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props.Ring;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class CreateRingByViewSystem : IEcsInitSystem
  {
    private readonly List<RingView> _rings;
    private readonly IGameObjectConverter _converter;
    private readonly EcsWorld _game;

    public CreateRingByViewSystem(GameWorldWrapper gameWorldWrapper, List<RingView> rings,
      IGameObjectConverter converter)
    {
      _rings = rings;
      _converter = converter;
      _game = gameWorldWrapper.World;
    }

    public void Init(EcsSystems systems)
    {
      foreach (RingView ringView in _rings)
      {
        EcsEntity ring = _game.CreateEntity()
          .Add<RingTag>()
          .Add((ref EntityId x) => x.Id = EntityType.Ring);

        _converter.Convert(ring, ringView);
      }
    }
  }
}