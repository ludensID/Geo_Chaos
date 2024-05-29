using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class CreateViewByPrefabSystem : IEcsRunSystem
  {
    private readonly IViewFactory _factory;
    private readonly IGameObjectConverter _converter;
    private readonly EcsWorld _game;
    private readonly EcsFilter _creatables;

    public CreateViewByPrefabSystem(GameWorldWrapper gameWorldWrapper,
      IViewFactory factory,
      IGameObjectConverter converter)
    {
      _factory = factory;
      _converter = converter;
      _game = gameWorldWrapper.World;

      _creatables = _game
        .Filter<ViewPrefab>()
        .Exc<ViewRef>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var creatable in _creatables)
      {
        ref var prefab = ref _game.Get<ViewPrefab>(creatable);
        _converter.Convert(_game, creatable, _factory.Create(prefab.Prefab));
        _game.Add<OnConverted>(creatable);
      }
    }
  }
}